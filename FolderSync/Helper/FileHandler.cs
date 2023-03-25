using FolderSync.Classses;
using FolderSync.CoolWay;
using FolderSync.MD5Algorithm;
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
        private HashComputer _hc = HashComputer.Instance;

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

        //public FolderPrint folderPrint = new FolderPrint();
        public FolderPrint GetFolderPrint2(string path)
        {
            FolderPrint folderPrint = new FolderPrint();
            folderPrint.FolderPathName = path;
            //All folders paths in folder
            var topFolders = _filePathReader.GetAllTopFolders(path);
            var filesInDir = _filePathReader.GetAllFiles(path);

            var listOfFilesHashes = _hc.GetDirFileHashes(filesInDir);
            folderPrint.FileHashes = listOfFilesHashes.ToList();

            //Console.WriteLine("File Hashes");
            //PrintStringList(listOfFilesHashes.ToList());

            if (topFolders?.Any() == true)
            {
                folderPrint.SubFolders = new List<FolderPrint>();
                //Console.WriteLine("Folder Sub Dirs");
                foreach (var subDirectory in topFolders)
                {
                    //Console.WriteLine(subDirectory);
                    folderPrint.SubFolders.Add( GetFolderPrint2(subDirectory));
                    
                }

            }

            return folderPrint;
        }

        private void PrintStringList(List<string> list)
        {
            foreach (var l in list)
            {
                Console.WriteLine($"{l}");
            }
        }

    }
}
