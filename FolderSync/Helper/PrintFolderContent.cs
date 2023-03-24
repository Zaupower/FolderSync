using FolderSync.Classes;
using FolderSync.Classses;
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


        public List<FolderDifference> GetFolderPrintDifferences(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub)
        {
            var differences = new List<FolderDifference>();

            // Get all objects that are in one collection and not in the other
            var currentOnly = currentSourceSub.Except(replicaSourceSub, new FolderPrintComparer());
            var replicaOnly = replicaSourceSub.Except(currentSourceSub, new FolderPrintComparer());
            
            differences.AddRange(currentOnly.Select(fp => new FolderDifference(fp, true)));
            differences.AddRange(replicaOnly.Select(fp => new FolderDifference(fp, false)));

            // Get all objects that are different in both collections
            var common = currentSourceSub.Intersect(replicaSourceSub, new FolderPrintComparer());
            foreach (var folderPrint in common)
            {
                var replicaFolderPrint = replicaSourceSub.First(f => f.FolderPathName == folderPrint.FolderPathName);
                if (!folderPrint.FileHashes.SequenceEqual(replicaFolderPrint.FileHashes))
                { 
                    differences.Add(new FolderDifference(folderPrint, true));
                    differences.Add(new FolderDifference(replicaFolderPrint, false));
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

        public List<FolderDifference> FindDifferences(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub)
        {
            var differences = new List<FolderDifference>();
            foreach (var folder in currentSourceSub)
            {
                if (!replicaSourceSub.Contains(folder))
                {
                    differences.Add(new FolderDifference(folder, true));
                }
            }
            foreach (var folder in replicaSourceSub)
            {
                if (!currentSourceSub.Contains(folder))
                {
                    differences.Add(new FolderDifference(folder, false));
                }
            }
            return differences;
        }
    }
}
