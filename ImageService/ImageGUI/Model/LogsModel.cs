using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.Model
{
    class LogsModel : ILogsModel
    {
        public LogsModel() { }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
