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
        private List<IClientHandler> chList;

        public TcpListener Listener { get { return this.listener; } set { this.listener = value; } }

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;
        public event EventHandler<DataRecievedEventsArgs> DataSend;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpServer"/> class.
        /// </summary>
        public TcpServer() {
            //ip = ConfigurationManager.AppSettings.Get("IP");
            // port = Int32.Parse(ConfigurationManager.AppSettings.Get("Port"));
            ip = "127.0.0.1";
            port = 8443;
            chList = new List<IClientHandler>();
        }
        /// <summary>
        /// Start the server and wait for client connect.
        /// </summary>
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
                        chList.Add(ch);
                        ch.HandleClient();
                    } catch (SocketException) {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void Stop() {
            listener.Stop();
        }

        /// <summary>
        /// Sends the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The data.</param>
        public void Send(Object sender, DataRecievedEventsArgs e) {
            this.DataSend?.Invoke(this, e);
        }

        /// <summary>
        /// Notifies all clients if something changed.
        /// </summary>
        /// <param name="e">The changed data</param>
        public void NotifyAll(DataRecievedEventsArgs e) {
            foreach (ClientHandler handler in chList) {
                handler.Send(this, e);
            }
        }
    }
}