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
