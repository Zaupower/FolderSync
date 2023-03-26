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
            if (!result) 
            {
                return this.FileHashes.Count > other.FileHashes.Count == true? 1: -1;
            }
            return 0;
        }

        public bool Equals(FolderPrint? other)
        {                       
            return other.FileHashes.SequenceEqual(FileHashes);
        }
    }
}
