
using FolderSync.Helper;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        FileHandler fh = FileHandler.Instance;

        string sourcePath = @"C:\\Users\\marce\\Documents\\Test\\source\\";
        string replicaPath = @"C:\\Users\\marce\\Documents\\Test\\replica\\";

        string[] entries = Directory.GetFileSystemEntries(sourcePath, "*", SearchOption.TopDirectoryOnly);

        //print all files in source folder
        if (entries.Length < 1)
        {
            Console.WriteLine("Empty Folder");
        }
        else
        {
            foreach (var path in entries)
            {

                Console.WriteLine(fh.CalculateMD5(@path));// full path
                //Console.WriteLine(Path.GetFileName(path)); // file name
            }
        }

        //get checksum of all folder content

    }
}