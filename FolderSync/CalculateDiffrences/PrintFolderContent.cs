using FolderSync.Classes;
using FolderSync.Classses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.AccessControl;

namespace FolderSync.CalculateDiffrences
{
    public class PrintFolderContent
    {
        public List<FolderDifference> FindDifferences(ICollection<FolderPrint> currentSourceSub, ICollection<FolderPrint> replicaSourceSub)
        {
            var differences = new List<FolderDifference>();
            //foreach (var folder in currentSourceSub)
            //{
            //    if (!replicaSourceSub.Contains(folder))
            //    {
            //        differences.Add(new FolderDifference(folder, true));
            //    }
            //}
            foreach (var folder in replicaSourceSub)
            {
                if (!currentSourceSub.Contains(folder))
                {

                    differences.Add(new FolderDifference(folder, false));
                }
                if (folder.SubFolders.Any()) 
                {
                    foreach (var subfolder in folder.SubFolders)
                    {

                    }
                }
            }
            return differences;
        }

        public List<string> GetDifferentProperties(FolderPrint obj1, FolderPrint obj2)
        {
            var differentProperties = new List<string>();

            if (!obj1.FileHashes.SequenceEqual(obj2.FileHashes))
                differentProperties.Add(nameof(FolderPrint.FileHashes));

            if (obj1.SubFolders == null && obj2.SubFolders == null)
                return differentProperties;

            if (obj1.SubFolders == null || obj2.SubFolders == null || obj1.SubFolders.Count != obj2.SubFolders.Count)
            {
                differentProperties.Add(nameof(FolderPrint.SubFolders));
                return differentProperties;
            }

            foreach (var subFolder1 in obj1.SubFolders)
            {
                if (!obj2.SubFolders.Any(subFolder2 => subFolder2.Equals(subFolder1)))
                {
                    differentProperties.Add(nameof(FolderPrint.SubFolders));
                    break;
                }
            }

            return differentProperties;
        }


        public List<FolderPrint> GetDifferentSubFolders(FolderPrint obj1, FolderPrint obj2)
        {
            var differentSubFolders = new List<FolderPrint>();

            if (obj1.SubFolders == null || obj2.SubFolders == null)
                return differentSubFolders;

            //foreach (var subFolder1 in obj1.SubFolders)
            //{
            //    if (!obj2.SubFolders.Any(subFolder2 => subFolder2.Equals(subFolder1)))
            //        differentSubFolders.Add(subFolder1);
            //    else
            //    {
            //        var matchingSubFolder = obj2.SubFolders.First(subFolder2 => subFolder2.Equals(subFolder1));
            //        differentSubFolders.AddRange(GetDifferentSubFolders(subFolder1, matchingSubFolder));
            //    }
            //}

            foreach (var subFolder2 in obj2.SubFolders)
            {
                if (!obj1.SubFolders.Any(subFolder1 => subFolder1.Equals(subFolder2)))
                    differentSubFolders.Add(subFolder2);
                else
                {
                    var matchingSubFolder = obj1.SubFolders.First(subFolder1 => subFolder1.Equals(subFolder2));
                    differentSubFolders.AddRange(GetDifferentSubFolders(matchingSubFolder, subFolder2));
                }
            }

            return differentSubFolders;
        }

        public IEnumerable<FolderDifference> GetDifferentSubFolders2(FolderPrint source, FolderPrint replica)
        {
            var differentSubFolders = new List<FolderDifference>();
            //Comapre files hashes
            if (!replica.Equals(source))
            {
                var newReplica = new FolderPrint();
                newReplica.FolderPathName = source.FolderPathName;
                differentSubFolders.Add(new FolderDifference(newReplica, true));
                return differentSubFolders;
            }

            if (source.SubFolders == null && replica.SubFolders == null)
                return differentSubFolders;
            if (source.SubFolders == null && replica.SubFolders != null)
            {
                var newReplica = new FolderPrint();
                newReplica.FolderPathName = source.FolderPathName;
                differentSubFolders.Add(new FolderDifference(newReplica, false));
                return differentSubFolders;
            }

            if (source.SubFolders != null && replica.SubFolders == null)
            {
                var newReplica = new FolderPrint();
                newReplica.FolderPathName = source.FolderPathName;
                differentSubFolders.Add(new FolderDifference(newReplica, true));
                return differentSubFolders;
            }
            if(source.SubFolders != null && replica.SubFolders != null)
            {
                var sourceExcept = source.SubFolders.Except(replica.SubFolders);
                var replicaExcept = replica.SubFolders.Except(source.SubFolders);

                if (sourceExcept != null && replicaExcept != null && source.SubFolders.Count == replica.SubFolders.Count)
                {
                    for(int i = 0; i < source.SubFolders.Count; i++)
                    {
                        differentSubFolders.AddRange(GetDifferentSubFolders2(source.SubFolders.ToList()[i], replica.SubFolders.ToList()[i]));
                    }
                    
                }else
                {
                    Console.WriteLine("Bad Condition");
                }

            }
            
            return differentSubFolders;
        }
    }
}
