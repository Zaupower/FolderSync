
using FolderSync.CoolWay;
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
        p.PrintFolderC(sourceFolder.SubFolders, replicaFolder.SubFolders);
        var arefileContentEqual = sourceFolder.CompareTo(replicaFolder);

        Console.WriteLine(arefileContentEqual); 
        
    }
}