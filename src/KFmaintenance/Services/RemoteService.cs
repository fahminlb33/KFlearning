using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using KFlearning.Core.API;
using KFlearning.Core.Services;
using KFmaintenance.Properties;

namespace KFmaintenance.Services
{
    public interface IRemoteService
    {
        void Run();
        void StartSendFile(string path);
        void StopSendFile();
        void SendShutdown();
    }

    public class RemoteService : IRemoteService
    {
        private readonly IQbittorrentClient _qbittorrentClient = Program.Container.Resolve<IQbittorrentClient>();
        private readonly IRemoteServer _remoteServer = Program.Container.Resolve<IRemoteServer>();

        private DateTime _lastReceiveFile, _lastShutdown;
        private bool _sendFile;

        public RemoteService()
        {
            _lastReceiveFile = DateTime.Now;
            _lastShutdown = DateTime.Now;
        }

        public void Run()
        {
            _remoteServer.FileBroadcast += RemoteServer_FileBroadcast;
            _remoteServer.ShutdownRequested += RemoteServer_ShutdownRequested;
            _remoteServer.Start();
        }

        public void StartSendFile(string path)
        {
            _sendFile = true;
            _remoteServer.StartServeFile(path);
        }

        public void StopSendFile()
        {
            if (!_sendFile) return;

            _sendFile = false;
            _remoteServer.StopServeFile();
        }

        public void SendShutdown()
        {
            _lastShutdown = DateTime.Now;
            _remoteServer.SendShutdown();
        }

        #region Remote Servicing

        private void RemoteServer_ShutdownRequested(object sender, RemoteEventArgs e)
        {
            if ((_lastShutdown - DateTime.Now).TotalSeconds >= 30 || !Settings.Default.RemoteShutdown) return;
            WindowsHelpers.Shutdown();
        }

        private void RemoteServer_FileBroadcast(object sender, RemoteEventArgs e)
        {
            if (_sendFile) return;
            if ((_lastReceiveFile - DateTime.Now).TotalSeconds <= 30) return;

            Task.Run(() =>
            {
                try
                {
                    using (var wc = new WebClient())
                    {
                        var uri = $"http://{e.Address}:{RemoteServer.FilePort}/serve";
                        var file = Path.ChangeExtension(Path.GetTempFileName(), ".torrent");
                        wc.DownloadFile(uri, file);

                        _lastReceiveFile = DateTime.Now;
                        _qbittorrentClient.AddTorrent(file);
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
            });
        }

        #endregion
    }
}
