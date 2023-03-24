
using FolderSync.CalculateDiffrences;
using FolderSync.Classes;
using FolderSync.Classses;
using FolderSync.Helper;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Xml.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {

        //
        FolderPrint sourceFolder = new FolderPrint();
        FolderPrint replicaFolder = new FolderPrint();

        FileHandler fh = FileHandler.Instance;

        string sourcePath = $"C:\\Users\\marce\\Documents\\Test\\source\\";
        string replicaPath = $"C:\\Users\\marce\\Documents\\Test\\replica\\";

        //Handle replica
        sourceFolder = fh.GetFolderPrint(sourceFolder, sourcePath);
        replicaFolder = fh.GetFolderPrint(replicaFolder, replicaPath);
       
        PrintFolderContent p = new PrintFolderContent();


        List<FolderDifference> differences = new List<FolderDifference>();

        //Handle subfolders
        //if (sourceFolder.SubFolders != null && replicaFolder.SubFolders != null)
        //{

        //    //differences = p.FindDifferences(sourceFolder.SubFolders, replicaFolder.SubFolders);
        //    differences = p.GetFolderPrintDifferences(sourceFolder.SubFolders, replicaFolder.SubFolders);

        //    var toRemoveInReplica = differences.Where(i=> i.IsFromSource == false).ToList();
        //    var toAddToReplica = differences.Where(i => i.IsFromSource == true).ToList();

        //    Console.WriteLine("Folders to remove in replica:");
        //    foreach (var toRemove in toRemoveInReplica) 
        //    {
        //        Console.WriteLine(toRemove.Folder.FolderPathName);
        //    }

        //    Console.WriteLine("Folders to add in replica");
        //    foreach (var toAdd in toAddToReplica)
        //    {
        //        Console.WriteLine(toAdd.Folder.FolderPathName);
        //    }

        //}
        //else if(sourceFolder.SubFolders == null)
        //{
        //    if(replicaFolder.SubFolders != null)
        //    {
        //        Console.WriteLine("Remove every subfolder from replica");
        //    }
        //}else if(sourceFolder.SubFolders != null && replicaFolder.SubFolders == null)
        //{
        //    Console.WriteLine("Copy every subfolder from source to replica");
        //}
        if (sourceFolder.SubFolders != null && replicaFolder.SubFolders != null)
        {
            
            //differences = p.GetFolderPrintDifferences(sourceFolder.SubFolders, replicaFolder.SubFolders);
            differences = p.FindDifferences(sourceFolder.SubFolders, replicaFolder.SubFolders);
            var toRemoveInReplica = differences.Where(i => i.IsFromSource).ToList();
            var toAddToReplica = differences.Where(i => !i.IsFromSource).ToList();

            Console.WriteLine("Folders to remove in replica:");
            foreach (var toRemove in toRemoveInReplica)
            {
                Console.WriteLine(toRemove.Folder.FolderPathName);
            }

            Console.WriteLine("Folders to add in replica");
            foreach (var toAdd in toAddToReplica)
            {
                Console.WriteLine(toAdd.Folder.FolderPathName);
            }
        }
        else if (sourceFolder.SubFolders == null && replicaFolder.SubFolders != null)
        {
            Console.WriteLine("Remove every subfolder from replica");
        }
        else if (sourceFolder.SubFolders != null && replicaFolder.SubFolders == null)
        {
            Console.WriteLine("Copy every subfolder from source to replica");
        }

        //Handle files
        int arefileContentEqual = -2;

        if (sourceFolder.FileHashes != null && replicaFolder.FileHashes != null)
        {
            arefileContentEqual = sourceFolder.CompareTo(replicaFolder);
        }else if(sourceFolder.FileHashes == null)
        {
            if(replicaFolder.FileHashes != null)
            {
                Console.WriteLine("Remove every file from replica");
            }
        }else if(sourceFolder.FileHashes != null && replicaFolder.FileHashes == null)
        {
            Console.WriteLine("Copy every file from source to replica");
        }
        
        

       // Console.WriteLine("Are file contente equal? "+arefileContentEqual); 
        
    }
}