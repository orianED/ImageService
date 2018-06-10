using ImageCommunication.Client;
using ImageCommunication.Events;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
using System.Linq;
using System.Web;
=======
using System.IO;
>>>>>>> didi

namespace ImageWebApp.Models {
    public class PhotosModel {
        public string OutputDir { get; set; }
        public string ThumbnailsDir { get; set; }
        public string[] Paths { get; set; }
        public List<Dictionary<string, string>> Pictures;

        public PhotosModel(string Output, string ThumbnailSize) {
            OutputDir = Output;
            ThumbnailsDir = Path.Combine(Output, "Thumbnails");
            Paths = Directory.GetFiles(ThumbnailsDir, "*", SearchOption.AllDirectories);
            string tempStr;
            Pictures = new List<Dictionary<string, string>>();
<<<<<<< HEAD
            foreach (string str in Paths) {
                tempStr = str.Replace(ThumbnailsDir, "Images");
                Pictures.Add(new Dictionary<string, string> {
                    {"thumb",str.Replace(OutputDir, "Images") }, {"picture", tempStr}
=======
>>>>>>> didi
                });
            }
        }
    }
