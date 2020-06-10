using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace KFlearning.Core.Services
{
    public interface IKFserverService : IDisposable
    {
        bool IsRunning { get; }

        ServerInfo GetInfo();
        void Start(string servePath);
        void Stop();
    }

    public class KFserverService : IKFserverService
    {
        private const int ServerPort = 21002;
        private const string KFserverProcessName = "kfserver";

        private readonly IPathManager _pathManager;

        public bool IsRunning => Process.GetProcessesByName(KFserverProcessName).Length > 0;


        public KFserverService(IPathManager pathManager)
        {
            _pathManager = pathManager;
        }

        public void Start(string servePath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _pathManager.GetPath(PathKind.KFserverExecutable),
                Arguments = servePath,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(startInfo);
        }

        public void Stop()
        {
            try
            {
                foreach (var process in Process.GetProcessesByName(KFserverProcessName))
                {
                    process.Kill();
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }

        public ServerInfo GetInfo()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new KFlearningException("Komputer ini tidak terhubung ke jaringan.");

            var hosts = Dns.GetHostEntry(Dns.GetHostName());
            var address = hosts.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            if (address == null)
                throw new KFlearningException("Tidak dapat menemukan IP komputer.");

            return new ServerInfo
            {
                IP = address.ToString(),
                Port = ServerPort,
                Link = $"http://{address}:{ServerPort}"
            };
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                Stop();
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~KFserverService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
