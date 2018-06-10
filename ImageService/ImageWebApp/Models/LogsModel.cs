using ImageCommunication.Client;
using ImageCommunication.Events;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace ImageWebApp.Models {
    public class LogsModel {
        private bool end_request;
        private IClient m_client;

        public List<Tuple<string, string>> Logs { get; set; }
        public ObservableCollection<LogMessage> LogMessages { get; set; }

        /// <summary>
        /// Constractur.
        /// </summary>
        public LogsModel() {
            Logs = new List<Tuple<string, string>>();
            try {
                m_client = Client.GetInstance;
                m_client.DataRecieved += MessageRecieved;
                m_client.Send((new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null)).ToJson());
            } catch (Exception e) {
                Debug.WriteLine("Connection Failure");
            }
        }

        /// <summary>
        /// Handle the message recieved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageRecieved(object sender, DataRecievedEventsArgs e) {
            Console.Write("Logs Pull");
            this.LogMessages = new ObservableCollection<LogMessage>();
            JObject json = JObject.Parse(e.Message);
            if ((int)json["CommandID"] == (int)CommandEnum.LogCommand) {
                CommandRecievedEventArgs commandLog = CommandRecievedEventArgs.FromJason(e.Message);
                Console.WriteLine(commandLog.Args[0]);
                string[] logs = commandLog.Args[0].Split('|');
                foreach (string log in logs) {
                    if (log != "")
                        this.LogMessages.Add(LogMessage.FromJason(log));
                }
                FromLogMessages();
                end_request = true;
            }
        }

        private void FromLogMessages() {
            Logs = new List<Tuple<string, string>>();
            foreach (LogMessage log in LogMessages) {
                Logs.Add(new Tuple<string, string>(log.Type.ToString(), log.Message));
            }
        }

        public void NewLogsRequest() {
            end_request = false;
            if (!m_client.Connected())
                return;
            m_client.Send((new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null)).ToJson());
            SpinWait.SpinUntil(() => end_request);
        }
    }
}