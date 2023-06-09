﻿namespace FolderSync.Helper
{
    internal class FilePathReader
    {
        private static Lazy<FilePathReader> _instance = new Lazy<FilePathReader>(() => new FilePathReader());
        public static FilePathReader Instance => _instance.Value;

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
        public List<string> GetAllFolders(string path)
        {
            var folders = new List<string>();
            try
            {
                folders.AddRange(Directory.GetDirectories(path));
                foreach (var directory in Directory.GetDirectories(path))
                {
                    folders.AddRange(GetAllFolders(directory));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return folders;
        }
        public List<string> GetAllTopFolders(string path)
        {
            var folders = new List<string>();
            try
            {
                folders.AddRange(Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly));                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return folders;
        }
        public List<string> GetAllFiles(string path)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path));               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return files;
        }
    }
}
