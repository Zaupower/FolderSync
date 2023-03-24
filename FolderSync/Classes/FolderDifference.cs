using FolderSync.Classses;

namespace FolderSync.Classes
{
    public class FolderDifference
    {
        public FolderPrint Folder { get; set; }
        public bool IsFromSource { get; set; }

        public FolderDifference(FolderPrint folder, bool isFromSource)
        {
            Folder = folder;
            IsFromSource = isFromSource;
        }
    }
}
