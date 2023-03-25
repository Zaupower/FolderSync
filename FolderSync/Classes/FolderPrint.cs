using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Classses
{
    public class FolderPrint : IComparable<FolderPrint>, IEquatable<FolderPrint>
    {
        public string FolderPathName { get; set; }
        public ICollection<FolderPrint>? SubFolders { get; set; }
        public ICollection<string> FileHashes { get; set; }

        public int CompareTo(FolderPrint? other)
        {
            var result = other.FileHashes.SequenceEqual(FileHashes);
            return result == true ? 0 : -1;
        }

        //TODO:
        //Resolve this equalizer

        public bool Equals(FolderPrint? other)
        {            
            //if(other.SubFolders == null && this.SubFolders == null)
            //{
            //    if(other.FileHashes == null && this.FileHashes == null)
            //    {
            //        return true;
            //    }
            //    return other.FileHashes.SequenceEqual(FileHashes);
            //}
            //if (other.SubFolders != null && this.SubFolders != null)
            //{
            //    if (other.SubFolders.Count == this.SubFolders.Count)
            //    {
            //        return other.FileHashes.SequenceEqual(FileHashes);
            //    }
            //}
            return other.FileHashes.SequenceEqual(FileHashes);
            //return false;
        }
    }
}
