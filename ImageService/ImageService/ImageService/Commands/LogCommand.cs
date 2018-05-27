using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Commands {
    public class LogCommand : ICommand {

        private ILoggingService loggingService;

        public LogCommand(ILoggingService logger) {
            this.loggingService = logger;
        }

        public string Execute(string[] args, out bool result) {
            try {
                string[] logs = new string[1];
                StringBuilder builder = new StringBuilder();
                foreach (LogMessage tempLog in this.loggingService.Logger) {
                    builder.Append(tempLog.ToJson());
                    builder.Append('|');
                }
                logs[0] = builder.ToString();
                CommandRecievedEventArgs commandLog = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, logs, null);
                result = true;
                return commandLog.ToJson().ToString().Replace(Environment.NewLine, " ");
            } catch {
                result = false;
                return "LogCommand execute Failed!";
            }
        }
    }
}