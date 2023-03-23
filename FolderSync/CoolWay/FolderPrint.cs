using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.CoolWay
{
    public class FolderPrint : IComparable<FolderPrint>
    {
       public string FolderPathName { get; set; }
       public ICollection<FolderPrint>? SubFolders { get; set; }
       public ICollection<string> FileHashes { get; set; }      

       public int CompareTo(FolderPrint? other)
       {
            
            //var result = this.FileHashes.Except(other.FileHashes).ToList();
            var result = other.FileHashes.SequenceEqual(this.FileHashes);
            return result == true? 0: -1;
       }
    }
}
