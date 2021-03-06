﻿using ImageService.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.Model {
    interface ILogsModel : INotifyPropertyChanged {
        ObservableCollection<LogMessage> LogMessages { get; set; }
    }
}
