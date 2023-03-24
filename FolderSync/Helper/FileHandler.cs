using FolderSync.Classses;
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
        private FilePathReader filePathReader = FilePathReader.Instance;

        public FolderPrint GetFolderPrint(FolderPrint folder, string path)
        {
            //var replicaFiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            //var relicaSubDirectories = Directory.GetDirectories(path);

            var dirFiles = filePathReader.GetAllFiles(path);
            var dirSubDirectories = filePathReader.GetAllFolders(path);

            string folderName = Path.GetFileName(path);


            if (dirFiles.Count < 1 && dirSubDirectories.Count < 1)
            {
                Console.WriteLine("Empty Folder");
            }
            else
            {
                folder = makeBluePrint.MakeBluePrint(dirFiles, folderName, dirSubDirectories);
            }

            return folder;
        }
    }
}
