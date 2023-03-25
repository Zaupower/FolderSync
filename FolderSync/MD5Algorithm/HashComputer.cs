using FolderSync.Helper;
using System.Security.Cryptography;

namespace FolderSync.MD5Algorithm
{
    public class HashComputer
    {
        private static Lazy<HashComputer> _instance = new Lazy<HashComputer>(() => new HashComputer());
        public static HashComputer Instance => _instance.Value;
        public string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }
        public IEnumerable<string> GetDirFileHashes(IEnumerable<string> files)
        {
            List<string> hashes = new List<string>();

            foreach (var file in files)
            {
                hashes.Add(CalculateMD5(file));
            }
            return hashes;
        }
    }
}
