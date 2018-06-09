using ImageCommunication.Client;
using ImageCommunication.Events;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ImageWebApp.Models {
    public class ConfigModel {
        private IClient m_client;
        private ObservableCollection<string> m_handlers;

        public ConfigModel() {
            try {
                m_client = Client.GetInstance;
                m_client.DataRecieved += MessageRecieved;
                m_handlers = new ObservableCollection<string>();
                m_client.Send((new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null)).ToJson());
            } catch (Exception e) {
                Debug.WriteLine("Connection Failure");
            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputDir:")]
        public string OutputDir { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName:")]
        public string LogName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sourcename:")]
        public string SourceName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize:")]
        public string ThumbnailSize { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers:")]
        public ObservableCollection<string> Handlers { get; set; }


        public void MessageRecieved(object sender, DataRecievedEventsArgs e) {
            JObject json = JObject.Parse(e.Message);
            if ((int)CommandEnum.GetConfigCommand == Int32.Parse((string)json["CommandID"])) {
                Console.Write("Config Pull");
                OutputDir = (string)json["OutputDir"];
                LogName = (string)json["LogName"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = (string)json["ThumbnailSize"];
                string[] arr = ((string)json["Handler"]).Split(';');
                for (int i = 0; i < arr.Length; i++) {
                    m_handlers.Add(arr[i]);
                }
                Handlers = m_handlers;
            }
        }

        /// <summary>
        /// Send message when handler remove.
        /// </summary>
        /// <param name="path"></param>
        public void OnRemove(string path) {
            string[] args = new String[1];
            Handlers.Remove(path);
            args[0] = path;
            m_client.Send(new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args, null).ToJson());
        }
    }
}