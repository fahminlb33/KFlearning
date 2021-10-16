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
        double RAM { get; }
        string CPU { get; }

        void Query();
    }

    public class SystemInfoService : ISystemInfoService
    {
        private bool _isLoaded;

        public string DeviceId { get; private set; } = "";
        public string OS { get; private set; } = "-";
        public string OSVersion { get; private set; } = "-";
        public string Architecture { get; private set; } = "-";
        public double RAM { get; private set; } = 0;
        public string CPU { get; private set; } = "-";

        public void Query()
        {
            if (_isLoaded)
            {
                return;
            }

            // build device ID
            string deviceId = "";

            // OS info
            var queryObj = SearchWmi("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            OS = queryObj["Caption"].ToString();
            OSVersion = queryObj["Version"].ToString();
            Architecture = queryObj["OSArchitecture"].ToString();
            RAM = Convert.ToDouble(queryObj["TotalVisibleMemorySize"].ToString());
            deviceId = queryObj["TotalVisibleMemorySize"].ToString();

            // CPU info
            queryObj = SearchWmi("root\\CIMV2", "SELECT * FROM Win32_Processor");
            CPU = queryObj["Name"].ToString();
            deviceId += queryObj["ProcessorId"].ToString();

            // Logical disk
            queryObj = SearchWmi("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
            deviceId += queryObj["VolumeSerialNumber"].ToString();

            // hash the info
            using (var hasher = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(deviceId);
                var hashed = hasher.ComputeHash(bytes);
                var hex = new StringBuilder(hashed.Length * 2);

                foreach (byte b in hashed)
                {
                    hex.AppendFormat("{0:x2}", b);
                }

                DeviceId = hex.ToString();
            }

            _isLoaded = true;
        }

        private ManagementBaseObject SearchWmi(string scope, string query)
        {
            return new ManagementObjectSearcher(scope, query).Get().Cast<ManagementBaseObject>().First();
        }
    }
}