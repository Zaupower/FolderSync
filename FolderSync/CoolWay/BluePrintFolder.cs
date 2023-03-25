using FolderSync.Classses;
using FolderSync.Helper;
using FolderSync.MD5Algorithm;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace FolderSync.CoolWay
{
    public class BluePrintFolder
    {
        private static Lazy<BluePrintFolder> _instance = new Lazy<BluePrintFolder>(() => new BluePrintFolder());
        public static BluePrintFolder Instance => _instance.Value;

        private HashComputer _hc = HashComputer.Instance;
        private FilePathReader _filePathReader = FilePathReader.Instance;

        public FolderPrint MakeBluePrint(IEnumerable<string> filesPath, string folderPathName, IEnumerable<string> subDirectoriesPath)
        {
            var fp = new FolderPrint
            {
                FolderPathName = folderPathName,
                FileHashes = filesPath.Select(file => _hc.CalculateMD5(file)).ToList()
            };

            if (subDirectoriesPath?.Any() == true)
            {
                
                List<TestProp> subs = new List<TestProp>();

                foreach (var subDirectory in subDirectoriesPath) 
                {

                    TestProp tp = new TestProp
                    {
                        //filesMD5 = GetDirFileHashes(subDirectory),
                        PathName = folderPathName,
                    };
                }


                //fp.SubFolders = subDirectoriesPath.Select(subDir =>
                //{
                //    var files = _filePathReader.GetAllFiles(subDir);
                //    var subDirectories = _filePathReader.GetAllFolders(subDir);
                //    string folderName = _filePathReader.GetFileName(subDir);

                //    return MakeBluePrint(files, subDir, subDirectories);
                //}).ToList();


                //var files = _filePathReader.GetAllFiles(folderPathName);
                //var subDirectories = _filePathReader.GetAllFolders(folderPathName);
                //string folderName = _filePathReader.GetFileName(folderPathName);

                //return MakeBluePrint(files, folderPathName, subDirectories);
            }
            return fp;
        }

    }
}
