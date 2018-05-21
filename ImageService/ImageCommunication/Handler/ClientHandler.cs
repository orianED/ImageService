using ImageCommunication.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageCommunication.Handler {
    class ClientHandler : IClientHandler {

        public event EventHandler<DataRecievedEventsArgs> DataRecieved;

        public void HandleClient(TcpClient client) {
            throw new NotImplementedException();
        }
    }
}
