using FolderSync.CoolWay;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FolderSync.Helper
{
    public class PrintFolderContent
    {
        public bool PrintFolderC(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub) 
        {
            
            var difDirs = replicaSourceSub.Where(f1 => currentSourceSub.All(f2 => f2.CompareTo(f1) == -1)).ToArray();
            //Not present dirs

            if (difDirs.Length != 0)
            {
                foreach (var difDir in difDirs)
                {
                    Console.WriteLine(difDir.FolderPathName);
                }
            } else if(currentSourceSub.FirstOrDefault().SubFolders != null && replicaSourceSub.FirstOrDefault().SubFolders != null)
            {                
                var joined = currentSourceSub.Zip(replicaSourceSub,
                    (first, second) => new { First = first, Second = second });
                
                foreach (var pair in joined)
                {
                    PrintFolderC(pair.First.SubFolders, pair.Second.SubFolders);
                }
            }

            //if (item.SubFolders.Count() > 0)
            //{
            //    PrintFolderC(item.SubFolders);
            //}
            //for (int i = 0; i < cRS.Length; i++)
            //{
            //    Console.WriteLine(cRS[i].FolderPathName);
            //}
            //foreach (var item in currentSourceSub)
            //{
            //    Console.WriteLine(item.FolderPathName);
            //    if(item.SubFolders != null)
            //    {

            //    }                
            //}
            return true;
        }
    }
}
