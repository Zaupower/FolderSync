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

        private BluePrintFolder _makeBluePrint = BluePrintFolder.Instance;
        private FilePathReader _filePathReader = FilePathReader.Instance;

        public FolderPrint GetFolderPrint(FolderPrint folder, string path)
        {
            var dirFiles = _filePathReader.GetAllFiles(path);
            var dirSubDirectories = _filePathReader.GetAllFolders(path);

            string folderName = Path.GetFileName(path);

            if (dirFiles.Any() || dirSubDirectories.Any())
            {
                folder = _makeBluePrint.MakeBluePrint(dirFiles, folderName, dirSubDirectories);
            }

            return folder;
        }
    }
}
