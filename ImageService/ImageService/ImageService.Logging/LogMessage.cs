using ImageService.Logging.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ImageService.Logging {
    public class LogMessage {

        public string Message { get; set; }
        public MessageTypeEnum Type { get; set; }

        public LogMessage(string message, MessageTypeEnum type) {
            this.Message = message;
            this.Type = type;
        }
        public LogMessage(string message, int type) {
            this.Message = message;
            this.Type = (MessageTypeEnum)type;
        }

        public string ToJson() {
            try {
                string temp;
                JObject j = new JObject();
                j["Message"] = this.Message;
                j["Type"] = (int)this.Type;
                temp = j.ToString().Replace(Environment.NewLine, " ");
                return temp;
            } catch (Exception e) {
                System.Console.WriteLine(e.ToString());
                return "";
            }
        }
        public static LogMessage FromJason(string s) {
            try {
                JObject j = JObject.Parse(s);
                return new LogMessage((string)j["Message"], (int)j["Type"]);
            } catch (Exception e) {
                System.Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}