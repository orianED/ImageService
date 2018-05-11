using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.Model
{
    interface ISettingsModel : INotifyPropertyChanged {
        #region property
        string OutputDir { get; set; }
        string LogName { get; set; }
        string SourceName { get; set; }
        string ThumbnailSize { get; set; }
        ObservableCollection<string> Handlers { get; set; }
        #endregion
    }
}