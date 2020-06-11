// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : Helpers.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using KFlearning.Core.API;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KFlearning.Core
{
    public static class Helpers
    {
        private const string CodeName = "Hagane no Moonsault";
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        public static readonly JsonSerializerSettings SerializeSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            },
        };

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

        public static void EnableTls()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12;
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return dateTime.Subtract(UnixEpoch).TotalSeconds;
        }

        public static string GetVersionString()
        {
            var version = Assembly.GetCallingAssembly().GetName().Version;
            return $"v{version.Major}.{version.Minor}.{version.Build} build \"{CodeName}\"";
        }

        public static string TrimLongText(this string path, int maxLength = 40)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            return path.Length <= maxLength ? path : path.Substring(0, maxLength) + "...";
        }

        public static int GetIntValue(this RegistryKey key, string name, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                return defaultValue;
            }

            return key == null ? defaultValue : Convert.ToInt32(key.GetValue(name, defaultValue));
        }

        public static string GetStringValue(this RegistryKey key, string name, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return defaultValue;
            }

            return key == null ? defaultValue : key.GetValue(name, defaultValue).ToString();
        }
    }
}