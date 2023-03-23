using FolderSync.CoolWay;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.AccessControl;

namespace FolderSync.Helper
{
    public class PrintFolderContent
    {
        public bool CheckAndAddModifiedContent(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub) 
        {
            
            var difDirs = currentSourceSub.Where(f1 => replicaSourceSub.All(f2 => f2.CompareTo(f1) == -1)).ToArray();

           // var difDirs2 = currentSourceSub.Where(f1 =>  replicaSourceSub.All(f2 => f2.CompareTo(f1) == -1, f2 => new { Q= f1, v = f2})).ToArray();
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


        public IEnumerable<(FolderPrint folderPrint, bool isCurrent)> GetFolderPrintDifferences(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub)
        {
            var differences = new List<(FolderPrint folderPrint, bool isCurrent)>();

            // Get all objects that are in one collection and not in the other
            var currentOnly = currentSourceSub.Except(replicaSourceSub, new FolderPrintComparer());
            var replicaOnly = replicaSourceSub.Except(currentSourceSub, new FolderPrintComparer());

            differences.AddRange(currentOnly.Select(fp => (fp, true)));
            differences.AddRange(replicaOnly.Select(fp => (fp, false)));

            // Get all objects that are different in both collections
            var common = currentSourceSub.Intersect(replicaSourceSub, new FolderPrintComparer());
            foreach (var folderPrint in common)
            {
                var replicaFolderPrint = replicaSourceSub.First(f => f.FolderPathName == folderPrint.FolderPathName);
                if (!folderPrint.FileHashes.SequenceEqual(replicaFolderPrint.FileHashes))
                {
                    differences.Add((folderPrint, true));
                    differences.Add((replicaFolderPrint, false));
                }
            }

            return differences;
        }

        public class FolderPrintComparer : IEqualityComparer<FolderPrint>
        {
            public bool Equals(FolderPrint x, FolderPrint y)
            {
                if (x == null || y == null)
                {
                    return false;
                }

                return x.FolderPathName == y.FolderPathName;
            }

            public int GetHashCode(FolderPrint obj)
            {
                return obj.FolderPathName.GetHashCode();
            }
        }


    }
}
