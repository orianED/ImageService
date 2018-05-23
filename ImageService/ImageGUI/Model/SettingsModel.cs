using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCommunication.Client;
using ImageCommunication.Events;
using Newtonsoft.Json.Linq;

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
            } catch (Exception e) {

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
            if (e.Message.Contains("Config")) {
                JObject json = JObject.Parse(e.Message);
                OutputDir = (string)json["OutputDir"];
                LogName = (string)json["LogName"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = (string)json["ThumbnailSize"];
                Handlers=new ObservableCollection<string>(((string)json["Handler"]).Split(';'));
            }
        }
    }
}