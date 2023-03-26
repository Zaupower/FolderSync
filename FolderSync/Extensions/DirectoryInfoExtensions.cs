using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void DeepCopy(this DirectoryInfo directory, string destinationDir)
        {
            foreach (string dir in Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories))
            {
                string dirToCreate = dir.Replace(directory.FullName, destinationDir);
                Directory.CreateDirectory(dirToCreate);
            }

            foreach (string newPath in Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(directory.FullName, destinationDir), true);
            }
        }
        public static void DeleteDirectory(this DirectoryInfo directoryInfo, string destination)
        {
            if (directoryInfo.Exists)
            {
                foreach (var file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (var subDirectory in directoryInfo.GetDirectories())
                {
                    subDirectory.Delete(true);
                }

                //directoryInfo.Delete();
            }
        }
    }
}
