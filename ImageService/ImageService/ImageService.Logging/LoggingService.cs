using ImageService.Logging.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging {
    public class LoggingService : ILoggingService {

        private ObservableCollection<LogMessage> logger;

        /// <summary>
        /// Occurs when [message recieved].
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        public ObservableCollection<LogMessage> Logger { get { return this.logger; } set { this.logger = value; } }

        public LoggingService() {
            this.logger = new ObservableCollection<LogMessage>();
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type of the message.</param>
        public void Log(string message, MessageTypeEnum type) {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
            this.logger.Add(new LogMessage(message, type));
        }
    }
}