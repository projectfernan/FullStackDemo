using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDemo.Common.Utilities
{
    public static class HashingHelper
    {
        public static string Hash(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return "";
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] hash = sha256.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte bit in hash)
            {
                hashString += String.Format("{0:x2}", bit);
            }
            return hashString.ToUpper();
        }
    }
}
