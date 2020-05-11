// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : RemoteServer.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KFlearning.Core.Services
{
    public interface IRemoteServer : IDisposable
    {
        void Start();
        void SendShutdown();
        void StartServeFile(string path);
        void StopServeFile();

        event EventHandler<RemoteEventArgs> ShutdownRequested;
        event EventHandler<RemoteEventArgs> FileBroadcast;
    }

    public class RemoteServer : IRemoteServer
    {
        public const int ServicePort = 2021;
        public const int FilePort = 2022;

        private const string MessageShutdown = "SDWN";
        private const string MessageFile = "FILE";

        private static readonly IPEndPoint ServiceEndpoint = new IPEndPoint(IPAddress.Any, ServicePort);

        private readonly HttpListener _httpListener = new HttpListener();
        private readonly UdpClient _socket = new UdpClient();
        private MemoryStream _uploadStream;

        public event EventHandler<RemoteEventArgs> ShutdownRequested;
        public event EventHandler<RemoteEventArgs> FileBroadcast;

        public RemoteServer()
        {
            _socket.Client.Bind(ServiceEndpoint);
            _httpListener.Prefixes.Add($"http://*:{FilePort}/serve/");
        }

        #region Public Methods

        public void Start()
        {
            _socket.BeginReceive(ReceiveCallback, null);
        }

        public void SendShutdown()
        {
            Send(MessageShutdown);
        }

        public void StartServeFile(string path)
        {
            _uploadStream = new MemoryStream(File.ReadAllBytes(path));
            _httpListener.Start();
            _httpListener.BeginGetContext(GetContextCallback, null);

            Send(MessageFile);
        }

        public void StopServeFile()
        {
            _httpListener.Stop();
            _uploadStream?.Dispose();
        }

        #endregion

        #region Private Methods (Callbacks)

        private void Send(string text)
        {
            var data = Encoding.ASCII.GetBytes(text);
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

        private void GetContextCallback(IAsyncResult ar)
        {
            var context = _httpListener.EndGetContext(ar);
            var response = context.Response;
            response.StatusCode = 200;
            response.SendChunked = true;
            response.ContentType = "application/octet-stream";

            using (var networkStream = response.OutputStream)
            {
                _uploadStream.Seek(0, SeekOrigin.Begin);
                _uploadStream.CopyTo(networkStream);
            }

            _httpListener.BeginGetContext(GetContextCallback, null);
        }

        private void ProcessMessage(string message, IPAddress server)
        {
            switch (message)
            {
                case MessageShutdown:
                    ShutdownRequested?.Invoke(this, new RemoteEventArgs {Address = server});
                    break;

                case MessageFile:
                    FileBroadcast?.Invoke(this, new RemoteEventArgs {Address = server});
                    break;
            }
        }

        #endregion

        #region IDisposable
        
        public void Dispose()
        {
            ((IDisposable)_httpListener)?.Dispose();
            ((IDisposable)_socket)?.Dispose();
            ((IDisposable)_uploadStream)?.Dispose();
        } 

        #endregion
    }
}
