using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.ImageService.Commands {
    class GetConfigCommand : ICommand {

        public string Execute(string[] args, out bool result) {
            Console.WriteLine("execute...");
            try {
                JObject j = new JObject();
                j["CommandID"] = (int)CommandEnum.GetConfigCommand;
                j["OutputDir"] = ConfigurationManager.AppSettings["OutputDir"];
                j["LogName"] = ConfigurationManager.AppSettings["LogName"];
                j["SourceName"] = ConfigurationManager.AppSettings["SourceName"];
                j["ThumbnailSize"] = ConfigurationManager.AppSettings["ThumbnailSize"];
                j["Handler"] = ConfigurationManager.AppSettings["Handler"];
                result = true;
                return j.ToString().Replace(Environment.NewLine, " ");
            } catch (Exception e) {
                result = false;
                return e.ToString();
            }
        }
    }
}
