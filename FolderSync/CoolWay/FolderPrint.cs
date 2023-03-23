using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.CoolWay
{
    public class FolderPrint : IComparable<FolderPrint>
    {
        IEnumerable<FolderPrint>? SubFolders { get; set; }
        IEnumerable<string> FileHashes { get; set; }


        public int CompareTo(FolderPrint? other)
        {
            return this.FileHashes.Equals(other.FileHashes) == true ? 0: -1;
        }
    }
}
