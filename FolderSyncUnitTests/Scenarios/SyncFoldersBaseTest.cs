using FolderSync.LoggerC;
using System.IO;

namespace FolderSyncUnitTests.Scenarios
{
    internal class SyncFoldersBaseTest
    {

        //internal string _source = Path.Combine(Environment.CurrentDirectory, @"TestFolders\", "SourceTest");
        //internal string _replica = Path.Combine(Environment.CurrentDirectory, @"TestFolders\", "ReplicaTest");
        //if path inside the project does not work use a custom path
        internal string _source = "C:\\Users\\marce\\Documents\\Test\\source";
        internal string _replica = "C:\\Users\\marce\\Documents\\Test\\replica";
        internal Logger _logger = new Logger("C:\\Users\\marce\\Documents\\Test\\logs\\logs.txt");

        public IEnumerable<string> GetSubdirectories(string path)
        {
            IEnumerable<string> result = Directory.GetDirectories(path);
            return result.Select(i=> Path.GetRelativePath(path,i));
        }

        public IEnumerable<string> GetFiles(string path)
        {
            IEnumerable<string> result = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            return result.Select(i => Path.GetRelativePath(path,i));
        }
    }
}
