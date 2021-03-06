﻿using ImageGUI.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageGUI.ViewModel {
    class SettingsVM : INotifyPropertyChanged {
        private ISettingsModel model;
        private string selectedHandler;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constractur.
        /// </summary>
        public SettingsVM() {
            model = new SettingsModel();
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            this.RemoveHandlerCommand = new DelegateCommand<object>(OnRemoveHandler, this.CanRemove);
            PropertyChanged += RemoveSelectedHandler;
        }

        /// <summary>
        /// Notify when property changed.
        /// </summary>
        /// <param name="propName"></param>
        private void NotifyPropertyChanged(string propName) {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
            this.PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }

        /// <summary>
        /// Remove the selected Handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSelectedHandler(object sender, PropertyChangedEventArgs e) {
            var command = this.RemoveHandlerCommand as DelegateCommand<object>;
            command?.RaiseCanExecuteChanged();
        }

        public string VM_OutputDir {
            get { return model.OutputDir; }
        }
        public string VM_LogName {
            get { return model.LogName; }
        }
        public string VM_SourceName {
            get { return model.SourceName; }
        }
        public string VM_ThumbnailSize {
            get { return model.ThumbnailSize; }
        }
        public ObservableCollection<string> VM_Handlers {
            get { return model.Handlers; }
        }

        public ICommand RemoveHandlerCommand { get; private set; }

        /// <summary>
        /// Select the handler.
        /// </summary>
        public string SelectedHandler {
            get { return selectedHandler; }
            set {
                selectedHandler = value;
                NotifyPropertyChanged("SelectedHandler");
            }
        }

        /// <summary>
        /// Remove the handler.
        /// </summary>
        /// <param name="obj"></param>
        public void OnRemoveHandler(Object obj) {
            model.OnRemove(selectedHandler);
            model.Handlers.Remove(selectedHandler);
            selectedHandler = null;
        }

        /// <summary>
        /// Check if can remove the handler.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanRemove(Object obj) {
            return (SelectedHandler != null);
        }
    }
}