using ImageGUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.ViewModel
{
    class LogsVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private ILogsModel logsModel;
        
        public LogsVM() {
            logsModel = new LogsModel();
        }

    }
}
