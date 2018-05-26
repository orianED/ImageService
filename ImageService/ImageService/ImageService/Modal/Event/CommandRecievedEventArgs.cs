﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal {
    public class CommandRecievedEventArgs : EventArgs {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }
        public string RequestDirPath { get; set; }  // The Request Directory

        public CommandRecievedEventArgs(int id, string[] args, string path) {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }

        public string ToJson() {
            try {
                JObject j = new JObject();
                j["CommandID"] = CommandID;
                j["RequestDirPath"] = RequestDirPath;
                j["Args"] = new JArray(Args);
                return j.ToString().Replace(Environment.NewLine, " ");
            } catch (Exception e) {
                System.Console.WriteLine(e.ToString());
                return "";
            }
        }

        public static CommandRecievedEventArgs FromJason(string s) {
            try {
                System.Console.WriteLine(s);
                JObject j = JObject.Parse(s);
                JArray cmdArg = (JArray)j["Args"];
                string[] args = cmdArg.Select(c => (string)c).ToArray();
                CommandRecievedEventArgs cArgs = new CommandRecievedEventArgs((int)j["CommandID"], args, null);
                return cArgs;
            } catch (Exception e) {
                System.Console.WriteLine(e.ToString());
                return null;
            }
        }

    }
}
