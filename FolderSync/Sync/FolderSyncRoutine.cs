using FolderSync.CalculateDiffrences;
using FolderSync.Classes;
using FolderSync.Extensions;
using FolderSync.Helper;
using FolderSync.LoggerC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Sync
{
    public class FolderSyncRoutine
    {
        private FileHandler _fileHandler = FileHandler.Instance;
        private FolderDifferences _printFolderContent = new FolderDifferences();

        public void SyncRoutineStart(string sourcePath, string replicaPath, Logger logger)
        {
            var sourceFolder = _fileHandler.GetFolderPrint(sourcePath);
            var replicaFolder = _fileHandler.GetFolderPrint(replicaPath);            

            var differences = _printFolderContent.GetDifferentSubFolders(sourceFolder, replicaFolder).ToList();

            foreach (FolderDifference difference in differences) 
            {
                string appendToLog = "";

                var subDirString = difference.Folder.FolderPathName.Replace(sourcePath, "");
                var sourceDir = new DirectoryInfo(sourcePath+subDirString);

                switch (difference.IsFromSource)
                {
                    case true:
                        subDirString = difference.Folder.FolderPathName.Replace(sourcePath, "");
                        sourceDir = new DirectoryInfo(sourcePath + subDirString);
                        sourceDir.DeepCopy(replicaPath + subDirString);
                        appendToLog = DateTime.UtcNow + " :: adding: " + replicaPath + subDirString + " in replica folder";
                        break;
                    case false:
                         subDirString = difference.Folder.FolderPathName.Replace(replicaPath, "");
                         sourceDir = new DirectoryInfo(replicaPath + subDirString);
                        sourceDir.DeleteDirectory(replicaPath + subDirString);
                        appendToLog = DateTime.UtcNow+" :: deleting: " + replicaPath + subDirString+ " in replica folder" ;
                        break;
                }                
                logger.Log(appendToLog);
                Console.WriteLine(appendToLog);
            }
        }

        public void SyncRoutineStart(string sourcePath, string replicaPath,string logFilePath, int thriggerSeconds)
        {
            Logger logger = new Logger(logFilePath);

            while (true)
            {
                SyncRoutineStart(sourcePath, replicaPath, logger);                
                Thread.Sleep(TimeSpan.FromSeconds(thriggerSeconds));
            }
            
        }       
    }
}
