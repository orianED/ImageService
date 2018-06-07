using ImageGUI.Model;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.ViewModel {
    class LogsVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private ILogsModel logsModel;

        public ObservableCollection<LogMessage> VM_logs { get { return logsModel.LogMessages; } }

        /// <summary>
        /// Constractur.
        /// </summary>
        public LogsVM() {
            this.logsModel = new LogsModel();
            this.logsModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        /// <summary>
        /// Notify when property changed.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName) {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
            this.PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }
    }
}