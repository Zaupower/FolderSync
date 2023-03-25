
using FolderSync.CalculateDiffrences;
using FolderSync.Classes;
using FolderSync.Classses;
using FolderSync.Helper;
using FolderSync.Sync;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Xml.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        string sourcePath = $"C:\\Users\\marce\\Documents\\Test\\source\\";
        string replicaPath = $"C:\\Users\\marce\\Documents\\Test\\replica\\";

        SyncRoutine syncRoutine = new SyncRoutine();
        syncRoutine.SyncRoutineStart(sourcePath, replicaPath, 0);

            
    }
}