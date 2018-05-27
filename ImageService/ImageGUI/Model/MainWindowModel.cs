using ImageCommunication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.Model {
    class MainWindowModel {
        private IClient m_client;

        public MainWindowModel() {
            m_client = Client.GetInstance;
        }

        public string IsConnected() {
            if (m_client.Connected())
                return "White";
            return "Gray";
        }
    }
}
