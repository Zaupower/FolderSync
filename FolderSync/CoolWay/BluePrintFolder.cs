﻿using FolderSync.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.CoolWay
{
    public class BluePrintFolder
    {
        private static Lazy<BluePrintFolder> _instance = new Lazy<BluePrintFolder>(() => new BluePrintFolder());
        public static BluePrintFolder Instance => _instance.Value;

        HashComputer hc = HashComputer.Instance;

        public FolderPrint MakeBluePrint(IEnumerable<string> filesPath, string folderPathName, IEnumerable<string> subDirectoriesPath = null)
        {
            FolderPrint fp = new FolderPrint();
            fp.FolderPathName = folderPathName;
            ICollection<string> hashes = new List<string>();
            ICollection<FolderPrint> subs = new List<FolderPrint>();
            

            foreach (var file in filesPath)
            {
                hashes.Add(hc.CalculateMD5(file)) ;
            }
            fp.FileHashes = hashes;

            if (subDirectoriesPath.Count() > 0)
            {
                foreach (var subDir in subDirectoriesPath)
                {
                    var files = Directory.GetFiles(subDir, "*", SearchOption.TopDirectoryOnly);
                    var subDirectories = Directory.GetDirectories(subDir);
                    string folderName = Path.GetFileName(subDir);
                    subs.Add(MakeBluePrint(files, folderPathName +"\\"+folderName, subDirectories));
                }
                fp.SubFolders = subs;
            }
            return fp;
        }
    }
}
