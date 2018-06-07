using ImageCommunication.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageCommunication.Handler {
    class ClientHandler : IClientHandler {
        private TcpClient m_client;
        private BinaryReader reader;
        private BinaryWriter writer;
        private NetworkStream streamer;
        private static Mutex mut = new Mutex();

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ClientHandler(TcpClient client) {
            if (client != null) {
                m_client = client;
                streamer = client.GetStream();
                reader = new BinaryReader(streamer);
                writer = new BinaryWriter(streamer);
            }
        }

        /// <summary>
        /// Handles the client.
        /// </summary>
        public void HandleClient() { 
            string msg;

            try {
                new Task(() => {
                    while (m_client.Connected) {
                        Debug.WriteLine("client handler reading");
                        if ((msg = reader.ReadString()) != null) {
                            DataRecievedEventsArgs dR = new DataRecievedEventsArgs();
                            dR.Message = msg;
                            DataRecieved?.Invoke(this, dR);
                        }
                    }
                }).Start();
            } catch (Exception e) {
                Console.Write(e.ToString());
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close() {
            if (m_client != null) {
                reader.Close();
                writer.Close();
                streamer.Close();
                m_client.Close();
                m_client = null;
            }
        }

        /// <summary>
        /// Sends new data for client.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The data to send.</param>
        public void Send(object sender, DataRecievedEventsArgs e) {
            mut.WaitOne();
            writer.Write(e.Message.Trim());
            writer.Flush();
            mut.ReleaseMutex();
        }
    }
}