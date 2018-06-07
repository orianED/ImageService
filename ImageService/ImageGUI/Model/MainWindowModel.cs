using ImageCommunication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.Model {
    class MainWindowModel {
        private IClient m_client;

        /// <summary>
        /// Constractur.
        /// </summary>
        public MainWindowModel() {
            m_client = Client.GetInstance;
        }

        /// <summary>
        /// return white if connected and gray otherwise.
        /// </summary>
        /// <returns></returns>
        public string IsConnected() {
            if (m_client.Connected())
                return "White";
            return "Gray";
        }
    }
}
