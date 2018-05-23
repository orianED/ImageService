using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        private StreamReader reader;
        private StreamWriter writer;

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;

        private Client() {
            ip = ConfigurationManager.AppSettings.Get("IP");
            port = Int32.Parse(ConfigurationManager.AppSettings.Get("Port"));
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.streamer = client.GetStream();
            reader = new StreamReader(streamer, Encoding.ASCII);
            writer = new StreamWriter(streamer, Encoding.ASCII);

            try {
                client.Connect(ep);
                Console.Write("Connection Success");
            }catch(Exception e) {
                Console.Write("Connection Failed");
                Close();
            }
        }

        public static IClient GetInstance {
            get {
                if (instance == null)
                    instance = new Client();
                return instance;
            }
        }

        public void Read() {
            new Task(() => {
                while (client.Connected) {
                    string msg;
                    StringBuilder str = new StringBuilder();

                    while ((msg = reader.ReadLine()) != null) {
                        str.AppendLine(msg);
                    }
                    if ((msg = str.ToString()) != null) {
                        DataRecievedEventsArgs dR = new DataRecievedEventsArgs();
                        dR.Message = msg;
                        DataRecieved?.Invoke(this, dR);
                    }
                }
                }).Start();
        }

        public void Send(string msg) {
            try {
                writer.Write(msg.Trim());
                writer.Flush();
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
    }
}
