using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCommunication.Client;
using ImageCommunication.Events;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ImageGUI.Model {
    class SettingsModel : ISettingsModel {
        private IClient m_client;
        private string m_outputDir;
        private string m_logName;
        private string m_sourceName;
        private string m_thumbnailSize;
        private ObservableCollection<string> m_handlers;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsModel() {
            try {
                m_client = Client.GetInstance;
                m_client.DataRecieved += MessageRecieved;
                m_client.Send((new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null)).ToJson());
            } catch (Exception e) {
                Debug.WriteLine("Connection Failure");
            }
        }

        public string OutputDir { get { return m_outputDir; } set { m_outputDir = value; OnPropertyChanged("OutputDir"); } }
        public string LogName { get { return m_logName; } set { m_logName = value; OnPropertyChanged("LogName"); } }
        public string SourceName { get { return m_sourceName; } set { m_sourceName = value; OnPropertyChanged("SourceName"); } }
        public string ThumbnailSize { get { return m_thumbnailSize; } set { m_thumbnailSize = value; OnPropertyChanged("ThumnailSize"); } }
        public ObservableCollection<string> Handlers { get { return m_handlers; } set { m_handlers = value; OnPropertyChanged("Handlers"); } }

        protected void OnPropertyChanged(string prop) {
            if (prop != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void MessageRecieved(object sender, DataRecievedEventsArgs e) {
            JObject json = JObject.Parse(e.Message);
            if ((int)CommandEnum.GetConfigCommand == Int32.Parse((string)json["CommandID"])) {
                Console.Write("Config Pull");
                OutputDir = (string)json["OutputDir"];
                LogName = (string)json["LogName"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = (string)json["ThumbnailSize"];
                Handlers = new ObservableCollection<string>(((string)json["Handler"]).Split(';'));
            }
        }

        public void OnRemove(string path) {
            string[] args = new String[1];
            args[0] = path;
            m_client.Send(new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args, null).ToJson());
        }

        public string GetColor() {
            if (m_client != null && m_client.Connect())
                return "Blue";
            return "Gray";
        }
    }
}