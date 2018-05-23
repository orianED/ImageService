using ImageCommunication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageCommunication {
    interface ITcpServer {
        event EventHandler<DataRecievedEventsArgs> DataRecieved;
        event EventHandler<DataRecievedEventsArgs> DataSend;

        TcpListener Listener { get; set; }

        void Start();
        void Stop();
        void Send(string msg);
    }
}
