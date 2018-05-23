using ImageCommunication.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ImageCommunication.Handler {
    class ClientHandler : IClientHandler {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private NetworkStream streamer;

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;

        public ClientHandler(TcpClient client) {
            if (client != null) {
                this.client = client;
                streamer = client.GetStream();
                reader = new StreamReader(streamer, Encoding.ASCII);
                writer = new StreamWriter(streamer, Encoding.ASCII);
            }
        }

        public void Start() {
            string msg;

            try {
                StringBuilder str = new StringBuilder();
                while((msg = reader.ReadLine()) != null) {
                    str.AppendLine(msg);
                }
                if((msg = str.ToString()) != null) {
                    DataRecievedEventsArgs dR = new DataRecievedEventsArgs();
                    dR.Message = msg;
                    DataRecieved?.Invoke(this, dR);
                }

            } catch (Exception e) {
                Console.Write(e.ToString());
            }
        }

        public void Close() {
            if(client != null) {
                reader.Close();
                writer.Close();
                streamer.Close();
                client.Close();
                client = null;
            }
        }

        public void Send(object sender, DataRecievedEventsArgs e) {
            writer.Write(e.Message.Trim());
            writer.Flush();
        }
    }
}