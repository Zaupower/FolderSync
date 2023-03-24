using FolderSync.Classses;
using FolderSync.Helper;
using FolderSync.MD5Algorithm;

namespace FolderSync.CoolWay
{
    public class BluePrintFolder
    {
        private static Lazy<BluePrintFolder> _instance = new Lazy<BluePrintFolder>(() => new BluePrintFolder());
        public static BluePrintFolder Instance => _instance.Value;

        private HashComputer _hc = HashComputer.Instance;
        private FilePathReader _filePathReader = FilePathReader.Instance;

        public FolderPrint MakeBluePrint(IEnumerable<string> filesPath, string folderPathName, IEnumerable<string> subDirectoriesPath = null)
        {
            var fp = new FolderPrint
            {
                FolderPathName = folderPathName,
                FileHashes = filesPath.Select(file => _hc.CalculateMD5(file)).ToList()
            };

            if (subDirectoriesPath?.Any() == true)
            {
                fp.SubFolders = subDirectoriesPath.Select(subDir =>
                {
                    var files = _filePathReader.GetAllFiles(subDir);
                    var subDirectories = _filePathReader.GetAllFolders(subDir);
                    string folderName = _filePathReader.GetFileName(subDir);

                    return MakeBluePrint(files, subDir, subDirectories);
                }).ToList();
            }
            return fp;
        }
    }
}
