using FolderSync.CoolWay;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FolderSync.Helper
{
    public class PrintFolderContent
    {
        public bool PrintFolderC(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub) 
        {
            if (currentSourceSub == null)             
            {
                //delete all in replica

                return false;
            
            }

            var difDirs = replicaSourceSub.Where(p => currentSourceSub.All(p2 => p2 != p)).ToArray();
            //Not present dirs

            //Handle dif dirs
            Console.WriteLine("");
            Console.WriteLine("Difs");
            Console.WriteLine("");
            if (difDirs.Length != 0)
            {
                foreach (var difDir in difDirs)
                {
                    Console.WriteLine(difDir.FolderPathName);
                }
            }
            else
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
