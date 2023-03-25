using FolderSync.CalculateDiffrences;
using FolderSync.Classes;
using FolderSync.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Sync
{
    public class SyncRoutine
    {
        private FileHandler _fileHandler = FileHandler.Instance;
        private PrintFolderContent _printFolderContent = new PrintFolderContent();
        public void SyncRoutineStart(string sourcePath, string replicaPath, int thriggerSeconds)
        {
            var sourceFolder = _fileHandler.GetFolderPrint2(sourcePath);
            var replicaFolder = _fileHandler.GetFolderPrint2(replicaPath);            

            List<FolderDifference> differences = new List<FolderDifference>();

            differences = _printFolderContent.GetDifferentSubFolders2(sourceFolder, replicaFolder).ToList();
            foreach (FolderDifference difference in differences) 
            {
                Console.WriteLine("I will update folder: "+difference.Folder.FolderPathName);
            }
        }
    }
}
