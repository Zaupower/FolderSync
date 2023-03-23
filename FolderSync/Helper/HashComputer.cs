﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync.Helper
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