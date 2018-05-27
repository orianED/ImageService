using ImageCommunication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCommunication.Client {
    public interface IClient {
        event EventHandler<DataRecievedEventsArgs> DataRecieved;
        
        void Close();
        void Send(string msg);
        void Read();
    }
}
