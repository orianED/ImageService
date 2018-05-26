using ImageCommunication.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageCommunication.Handler {
    class ClientHandler : IClientHandler {
        private TcpClient m_client;
        private BinaryReader reader;
        private BinaryWriter writer;
        private NetworkStream streamer;

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;

        public ClientHandler(TcpClient client) {
            if (client != null) {
                m_client = client;
                streamer = client.GetStream();
                reader = new BinaryReader(streamer);
                writer = new BinaryWriter(streamer);
            }
        }

        public void HandleClient() { 
            string msg;

            try {
                while (m_client.Connected) {
                    Debug.WriteLine("client handler reading");
                    if ((msg = reader.ReadString()) != null) {
                        DataRecievedEventsArgs dR = new DataRecievedEventsArgs();
                        dR.Message = msg;
                        DataRecieved?.Invoke(this, dR);
                    }
                }
            } catch (Exception e) {
                Console.Write(e.ToString());
            }
        }

        public void Close() {
            if (m_client != null) {
                reader.Close();
                writer.Close();
                streamer.Close();
                m_client.Close();
                m_client = null;
            }
        }

        public void Send(object sender, DataRecievedEventsArgs e) {
            writer.Write(e.Message.Trim());
            writer.Flush();
        }
    }
}