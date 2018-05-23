using ImageService.Logging.Modal;

namespace ImageService.Logging {
    public class LogMessage {

        private string message { get; set; }
        private MessageTypeEnum type { get; set; }

        public LogMessage(string message, MessageTypeEnum type) {
            this.message = message;
            this.type = type;
        }
    }
}