using FolderSync.CoolWay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Helper
{
    public class FileHandler
    {
        private static Lazy<FileHandler> _instance = new Lazy<FileHandler>(() => new FileHandler());
        public static FileHandler Instance => _instance.Value;

        private BluePrintFolder makeBluePrint = BluePrintFolder.Instance;

        public FolderPrint GetFolderPrint(FolderPrint folder, string path)
        {
            var replicaFiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            var relicaSubDirectories = Directory.GetDirectories(path);
            string folderName = Path.GetFileName(path);


            if (replicaFiles.Length < 1)
            {
                Console.WriteLine("Empty Folder");
            }
            else
            {
                folder = makeBluePrint.MakeBluePrint(replicaFiles, folderName, relicaSubDirectories);
            }

            return folder;
        }     
    }
}
