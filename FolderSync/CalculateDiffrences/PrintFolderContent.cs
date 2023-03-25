using FolderSync.Classes;
using FolderSync.Classses;

namespace FolderSync.CalculateDiffrences
{
    public class PrintFolderContent
    {

        public IEnumerable<FolderDifference> GetDifferentSubFolders2(FolderPrint source, FolderPrint replica)
        {
            var differentSubFolders = new List<FolderDifference>();
            //Comapre files hashes
            if (!replica.Equals(source))
            {
                differentSubFolders.Add(new FolderDifference(source, true));
                return differentSubFolders;
            }

            if (source.SubFolders == null && replica.SubFolders == null)
            {
                return differentSubFolders;
            }
                
            if (source.SubFolders == null && replica.SubFolders != null)
            {
                differentSubFolders.Add(new FolderDifference(source, false));
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

                if (sourceExcept != null && replicaExcept != null && source.SubFolders.Count == replica.SubFolders.Count)
                {
                    for(int i = 0; i < source.SubFolders.Count; i++)
                    {
                        differentSubFolders.AddRange(GetDifferentSubFolders2(source.SubFolders.ToList()[i], replica.SubFolders.ToList()[i]));
                    }
                    
                }else
                {
                    Console.WriteLine("Bad Condition found, if this condition was printed an unhadled case is happenning!");
                }

            }
            
            return differentSubFolders;
        }
    }
}
