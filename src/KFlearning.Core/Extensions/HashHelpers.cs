using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KFlearning.Core.Extensions
{
    public static class HashHelpers
    {
        public static string HashPassword(string text)
        {
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(text)));
            }
        }

        public static bool CompareHash(string plain, string cipher)
        {
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var cipherSeq = Convert.FromBase64String(cipher);
                var plainSeq = sha256.ComputeHash(Encoding.UTF8.GetBytes(plain));

                return cipherSeq.SequenceEqual(plainSeq);
            }
        }
    }
}
