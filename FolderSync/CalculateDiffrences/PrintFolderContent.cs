using FolderSync.Classes;
using FolderSync.Classses;

namespace FolderSync.CalculateDiffrences
{
    public class FolderDifferences
    {

        public IEnumerable<FolderDifference> GetDifferentSubFolders(FolderPrint source, FolderPrint replica)
        {
            var differentSubFolders = new List<FolderDifference>();
            //Comapre files hashes
            int comparer = source.CompareTo(replica);
            if (comparer != 0)
            {
                differentSubFolders.Add(new FolderDifference(comparer == -1 ? replica:source, comparer==-1?false: true));
                return differentSubFolders;
            }

            if (source.SubFolders == null && replica.SubFolders == null)
            {
                return differentSubFolders;
            }
                
            if (source.SubFolders == null && replica.SubFolders != null)
            {
                differentSubFolders.Add(new FolderDifference(replica, false));
                return differentSubFolders;
            }

            if (source.SubFolders != null && replica.SubFolders == null)
            {
                differentSubFolders.Add(new FolderDifference(source, true));
                return differentSubFolders;
            }
            if(source.SubFolders != null && replica.SubFolders != null)
            {
                var sourceExcept = source.SubFolders.Except(replica.SubFolders);
                var replicaExcept = replica.SubFolders.Except(source.SubFolders);

                if (sourceExcept != null && replicaExcept != null)
                {
                    if (source.SubFolders.Count == replica.SubFolders.Count)
                    {
                        for (int i = 0; i < source.SubFolders.Count; i++)
                        {
                            differentSubFolders.AddRange(GetDifferentSubFolders(source.SubFolders.ToList()[i], replica.SubFolders.ToList()[i]));
                        }
                    }else if (source.SubFolders.Count > replica.SubFolders.Count)
                    {
                        differentSubFolders.Add(new FolderDifference(source, true));
                    }
                    else
                    {
                        differentSubFolders.Add(new FolderDifference(replica, false));
                    }
                }
            }            
            return differentSubFolders;
        }
    }
}
