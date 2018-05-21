using ImageGUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.ViewModel {
    class LogsVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private ILogsModel logsModel;

        public LogsVM() {
            logsModel = new LogsModel();
            logsModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        public void NotifyPropertyChanged(string s) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(s));
        }
    }
}
