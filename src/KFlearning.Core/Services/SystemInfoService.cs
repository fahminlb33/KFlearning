// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : SystemInfoService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace KFlearning.Core.Services
{
    public interface ISystemInfoService
    {
        string DeviceId { get; }
        string OS { get; }
        string OSVersion { get; }
        string Architecture { get; }
        string RAM { get; }
        string CPU { get; }

        void Query();
    }

    public class SystemInfoService : ISystemInfoService
    {
        private bool _isLoaded;

        public void Query()
        {
            try
            {
                if (_isLoaded) return;

                // build device ID
                string deviceId = "";

                // OS info
                var queryObj = SearchWmi("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
                OS = queryObj["Caption"].ToString();
                OSVersion = queryObj["Version"].ToString();
                Architecture = queryObj["OSArchitecture"].ToString();
                RAM = queryObj["TotalVisibleMemorySize"].ToString();
                deviceId = queryObj["TotalVisibleMemorySize"].ToString();

                // CPU info
                queryObj = SearchWmi("root\\CIMV2", "SELECT * FROM Win32_Processor");
                CPU = queryObj["Name"].ToString();
                deviceId += queryObj["ProcessorId"].ToString();

                // Logical disk
                queryObj = SearchWmi("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
                deviceId += queryObj["VolumeSerialNumber"].ToString();

                // hash the info
                using (var hasher = new SHA256CryptoServiceProvider())
                {
                    var bytes = Encoding.UTF8.GetBytes(deviceId);
                    var hashed = hasher.ComputeHash(bytes);
                    var hex = new StringBuilder(hashed.Length * 2);

                    foreach (byte b in hashed) hex.AppendFormat("{0:x2}", b);
                    DeviceId = hex.ToString();
                }

                _isLoaded = true;
            }
            catch (Exception)
            {
                // ignore
            }
        }

        private ManagementBaseObject SearchWmi(string scope, string query)
        {
            return new ManagementObjectSearcher(scope, query).Get().Cast<ManagementBaseObject>().First();
        }

        public string DeviceId { get; private set; } = "";
        public string OS { get; private set; } = "-";
        public string OSVersion { get; private set; } = "-";
        public string Architecture { get; private set; } = "-";
        public string RAM { get; private set; } = "-";
        public string CPU { get; private set; } = "-";
    }
}