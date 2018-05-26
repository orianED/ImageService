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
    public class TcpServer : ITcpServer {
        private int port;
        private string ip;
        private TcpListener listener;


        public TcpListener Listener { get { return this.listener; } set { this.listener = value; } }

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;
        public event EventHandler<DataRecievedEventsArgs> DataSend;

        public TcpServer() {
            //ip = ConfigurationManager.AppSettings.Get("IP");
            // port = Int32.Parse(ConfigurationManager.AppSettings.Get("Port"));
            ip = "127.0.0.1";
            port = 8443;
        }

        public void Start() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.listener = new TcpListener(ep);

            this.listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true) {
                    try {
                        IClientHandler ch;
                        TcpClient client = this.listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch = new ClientHandler(client);
                        ch.DataRecieved += this.DataRecieved;
                        this.DataSend += ch.Send;
                        ch.HandleClient();
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

        public void Send(Object sender, DataRecievedEventsArgs e) {
            this.DataSend?.Invoke(this, e);
        }
    }
}
