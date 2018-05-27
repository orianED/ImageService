using ImageGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGUI.ViewModel {
    class MainWindowVM {
        private MainWindowModel model;

        public string VM_IsConnected { get { return model.IsConnected(); } }

        public MainWindowVM() {
            model = new MainWindowModel();
        }

    }
}
