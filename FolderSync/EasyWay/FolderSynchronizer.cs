using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.EasyWay
{
    public class FolderSynchronizer
    {

        public void SyncFolders(string sourceFolder, string destinationFolder)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourceFolder);
            DirectoryInfo destDir = new DirectoryInfo(destinationFolder);

            // Create destination folder if it doesn't exist
            if (!destDir.Exists)
            {
                destDir.Create();
            }

            // Copy each file in source folder to destination folder
            foreach (FileInfo fileInfo in sourceDir.GetFiles())
            {
                string destFileName = Path.Combine(destDir.FullName, fileInfo.Name);
                fileInfo.CopyTo(destFileName, true);
            }

            // Recursively call this function on each subdirectory in the source folder
            foreach (DirectoryInfo subDirInfo in sourceDir.GetDirectories())
            {
                string destSubDir = Path.Combine(destDir.FullName, subDirInfo.Name);
                SyncFolders(subDirInfo.FullName, destSubDir);
            }
        }
    }
}
