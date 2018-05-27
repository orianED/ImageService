using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImageCommunication.Events;

namespace ImageCommunication.Client {
    public class Client : IClient {
        private static Client instance;
        private int port;
        private string ip;
        private TcpClient client;
        private IPEndPoint ep;
        private NetworkStream streamer;
        private BinaryReader reader;
        private BinaryWriter writer;

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;

        private Client() {
            //ip = ConfigurationManager.AppSettings.Get("IP");
            ip = "127.0.0.1";
            //port = Int32.Parse(ConfigurationManager.AppSettings.Get("Port"));
            port = 8443;
            ep = new IPEndPoint(IPAddress.Parse(ip), port);

            client = new TcpClient();
            try {
                client.Connect(ep);

                this.streamer = client.GetStream();
                this.reader = new BinaryReader(streamer);
                this.writer = new BinaryWriter(streamer);
                Console.WriteLine("Connection Success");

            }catch(Exception e) {
                Console.WriteLine("Connection Failed");
                Close();
            }
        }

        public static IClient GetInstance {
            get {
                if (instance == null) {
                    instance = new Client();
                    instance.Read();
                }
                return instance;
            }
        }

        public void Read() {
            new Task(() => {
                while (client.Connected) {
                    string msg;
                    
                    if ((msg = reader.ReadString()) != null) {
                        DataRecievedEventsArgs dR = new DataRecievedEventsArgs();
                        dR.Message = msg;
                        DataRecieved?.Invoke(this, dR);
                    }
                }
                }).Start();
        }

        public void Send(string msg) {
            Console.WriteLine("send to server");
            try {
                this.writer.Write(msg.Trim());
                this.writer.Flush();
            }catch(Exception e) {
                Console.Write(e.ToString());
            }   
        }

        public void Close() {
            if(instance!= null) {
                streamer.Close();
                client.Close();
                client = null;
            }
        }

        public bool Connected() {
            if (client == null)
                return false;
            return client.Connected;
        }
    }
}
