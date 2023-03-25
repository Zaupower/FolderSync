using FolderSync.Classses;
using FolderSync.MD5Algorithm;

namespace FolderSync.Helper
{
    public class FileHandler
    {
        private static Lazy<FileHandler> _instance = new Lazy<FileHandler>(() => new FileHandler());
        public static FileHandler Instance => _instance.Value;

        private FilePathReader _filePathReader = FilePathReader.Instance;
        private HashComputer _hc = HashComputer.Instance;

        public FolderPrint GetFolderPrint(string path)
        {
            FolderPrint folderPrint = new FolderPrint();
            folderPrint.FolderPathName = path;

            var topFolders = _filePathReader.GetAllTopFolders(path);
            var filesInDir = _filePathReader.GetAllFiles(path);

            var listOfFilesHashes = _hc.GetDirFileHashes(filesInDir);
            folderPrint.FileHashes = listOfFilesHashes.ToList();

            if (topFolders?.Any() == true)
            {
                folderPrint.SubFolders = new List<FolderPrint>();
                foreach (var subDirectory in topFolders)
                {
                    folderPrint.SubFolders.Add( GetFolderPrint(subDirectory));                    
                }

            }
            return folderPrint;
        }
    }
}
