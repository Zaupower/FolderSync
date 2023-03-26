
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
        FolderSyncRoutine syncRoutine = new FolderSyncRoutine();
        syncRoutine.SyncRoutineStart(args[0], args[1], args[2], int.Parse(args[3]) );    
    }
}