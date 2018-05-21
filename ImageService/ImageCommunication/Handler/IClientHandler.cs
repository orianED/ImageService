using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCommunication.Handler {
    interface IClientHandler {
        void HandleClient(TcpClient client);
    }
}
