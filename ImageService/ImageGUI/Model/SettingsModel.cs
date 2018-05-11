﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.Model {
    class SettingsModel : ISettingsModel {
        private string m_outputDir;
        private string m_logName;
        private string m_sourceName;
        private string m_thumbnailSize;
        private ObservableCollection<string> m_handlers;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsModel() {
        }

        public string OutputDir { get { return m_outputDir; } set { m_outputDir = value; OnPropertyChanged("OutputDir"); } }
        public string LogName { get { return m_logName; } set { m_outputDir = value; OnPropertyChanged("LogName"); } }
        public string SourceName { get { return m_sourceName; } set { m_outputDir = value; OnPropertyChanged("SourceName"); } }
        public string ThumbnailSize { get { return m_thumbnailSize; } set { m_outputDir = value; OnPropertyChanged("ThumnailSize"); } }
        public ObservableCollection<string> Handlers { get { return m_handlers; } set => throw new NotImplementedException(); }

        protected void OnPropertyChanged(string prop) {
            if (prop != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}