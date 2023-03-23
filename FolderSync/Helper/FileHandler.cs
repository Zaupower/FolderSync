using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Helper
{
    public class FileHandler
    {
        private static Lazy<FileHandler> _instance = new Lazy<FileHandler>(() => new FileHandler());
        public static FileHandler Instance => _instance.Value;

        //public string CalculateMD5(string filename)
        //{

        //    using var md5 = MD5.Create();

        //    using var stream = File.OpenRead(filename);

        //    var hash = md5.ComputeHash(stream);
        //    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        //}

        public string CalculateMD5(string filename)
        {

            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }

    }
}
