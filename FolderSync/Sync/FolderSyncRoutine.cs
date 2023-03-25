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
        private Logger _logger;


        public void SyncRoutineStart(string sourcePath, string replicaPath, Logger _logger)
        {
            var sourceFolder = _fileHandler.GetFolderPrint(sourcePath);
            var replicaFolder = _fileHandler.GetFolderPrint(replicaPath);            

            var differences = _printFolderContent.GetDifferentSubFolders(sourceFolder, replicaFolder).ToList();
            foreach (FolderDifference difference in differences) 
            {
                var subDirString = difference.Folder.FolderPathName.Replace(sourcePath, "");
                var sourceDir = new DirectoryInfo(sourcePath+subDirString);
                //var replicaDir = new DirectoryInfo(replicaPath);

                sourceDir.DeepCopy(replicaPath + subDirString);
                Console.WriteLine("I will update folder: "+ replicaPath+ subDirString);
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
