using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ImageCommunication.Events;
using ImageCommunication.Handler;

namespace ImageCommunication {
    class TcpServer : ITcpServer {
        private int port;
        private string ip;
        private TcpListener listener;


        public TcpListener Listener { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;
        public event EventHandler<DataRecievedEventsArgs> DataSend;

        public TcpServer() {
            ip = ConfigurationManager.AppSettings.Get("IP");
            port = Int32.Parse(ConfigurationManager.AppSettings.Get("Port"));
        }

        public void Start() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");
            IClientHandler ch;
            Task task = new Task(() => {
                while (true) {
                    try {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch = new ClientHandler(client);
                        ch.DataRecieved += this.DataRecieved;


                    } catch (SocketException) {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }

        public void Stop() {
            listener.Stop();
        }

        public void Send(string msg) {
            DataRecievedEventsArgs e = new DataRecievedEventsArgs();
            e.Message = msg;
            this.DataRecieved?.Invoke(this, e);
        }
    }
}
