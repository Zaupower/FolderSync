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
    }
}
