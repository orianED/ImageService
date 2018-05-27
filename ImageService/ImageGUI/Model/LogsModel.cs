using ImageCommunication.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCommunication.Events;
using System.Diagnostics;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System.Collections.ObjectModel;
using ImageService.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Data;

namespace ImageGUI.Model {
    class LogsModel : ILogsModel {
        private IClient m_client;
        private ObservableCollection<LogMessage> logs;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LogMessage> LogMessages { get { return this.logs; } set { this.logs = value; OnPropertyChanged("LogMessage"); } }

        public LogsModel() {
            try {
                m_client = Client.GetInstance;
                m_client.DataRecieved += MessageRecieved;
                m_client.Send((new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null)).ToJson());
            } catch (Exception e) {
                Debug.WriteLine("Connection Failure");
            }
        }

        protected void OnPropertyChanged(string prop) {
            if (prop != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void MessageRecieved(object sender, DataRecievedEventsArgs e) {
            Console.Write("Logs Pull");
            this.LogMessages = new ObservableCollection<LogMessage>();
            JObject json = JObject.Parse(e.Message);
            if ((int)json["CommandID"] == (int)CommandEnum.LogCommand) {
                CommandRecievedEventArgs commandLog = CommandRecievedEventArgs.FromJason(e.Message);
                Console.WriteLine(commandLog.Args[0]);
                string[] logs = commandLog.Args[0].Split('|');
                foreach (string log in logs) {
                    if (log != "")
                        this.LogMessages.Add(LogMessage.FromJason(log));
                }
            }
        }
    }
}