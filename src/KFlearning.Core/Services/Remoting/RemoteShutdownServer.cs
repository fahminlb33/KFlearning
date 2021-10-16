using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KFlearning.Core.Remoting
{
    public interface IRemoteShutdownServer : IDisposable
    {
        void Listen();
        void SendShutdown(string cluster);

        event EventHandler<ShutdownRequestedEventArgs> ShutdownRequested;
    }

    public class RemoteShutdownServer : IRemoteShutdownServer
    {
        public const int ServicePort = 2021;
        private const string MessageShutdown = "SDWN";

        private static readonly IPEndPoint ServiceEndpoint = new IPEndPoint(IPAddress.Any, ServicePort);

        private readonly UdpClient _socket = new UdpClient();

        public event EventHandler<ShutdownRequestedEventArgs> ShutdownRequested;

        public RemoteShutdownServer()
        {
            _socket.Client.Bind(ServiceEndpoint);
        }

        #region Public Methods

        public void Listen()
        {
            _socket.BeginReceive(ReceiveCallback, null);
        }

        public void SendShutdown(string cluster)
        {
            Send(MessageShutdown, cluster);
        }

        #endregion

        #region Private Methods (Callbacks)

        private void Send(string message, string body)
        {
            var data = Encoding.ASCII.GetBytes($"{message}|{body}");
            _socket.BeginSend(data, data.Length, ServiceEndpoint, SendCallback, null);
        }

        private void SendCallback(IAsyncResult ar)
        {
            _socket.EndSend(ar);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var epFrom = new IPEndPoint(IPAddress.Any, ServicePort);
            var bytes = _socket.EndReceive(ar, ref epFrom);
            var message = Encoding.ASCII.GetString(bytes);
            ProcessMessage(message, epFrom.Address);

            _socket.BeginReceive(ReceiveCallback, null);
        }

        private void ProcessMessage(string message, IPAddress server)
        {
            if (!message.StartsWith(MessageShutdown))
            {
                return;
            }

            var cluster = message.Split('|')[1];
            ShutdownRequested?.Invoke(this, new ShutdownRequestedEventArgs { Address = server, Cluster = cluster });
        }

        #endregion

        #region IDisposable
        
        public void Dispose()
        {
            ((IDisposable)_socket)?.Dispose();
        } 

        #endregion
    }
}
