using FolderSync.CoolWay;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FolderSync.Helper
{
    public class PrintFolderContent
    {
        public bool CheckAndAddModifiedContent(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub) 
        {
            
            var difDirs = currentSourceSub.Where(f1 => replicaSourceSub.All(f2 => f2.CompareTo(f1) == -1)).ToArray();
            //Not present dirs

            if (difDirs.Length != 0)
            {
                foreach (var difDir in difDirs)
                {
                    Console.WriteLine("Add All from: "+difDir.FolderPathName);
                }
            } else if(currentSourceSub.FirstOrDefault().SubFolders != null && replicaSourceSub.FirstOrDefault().SubFolders != null)
            {                
                var joined = currentSourceSub.Zip(replicaSourceSub,
                    (first, second) => new { First = first, Second = second });
                
                foreach (var pair in joined)
                {
                    CheckAndAddModifiedContent(pair.First.SubFolders, pair.Second.SubFolders);
                }
            }

            return true;
        }


    }
}
