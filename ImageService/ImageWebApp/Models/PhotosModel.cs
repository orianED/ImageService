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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
>>>>>>> didi

namespace ImageWebApp.Models {
    public class PhotosModel {
        private static Regex r = new Regex(":");
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
            try {
                OutputDir = Output;
                ThumbnailsDir = Path.Combine(Output, "Thumbnails");
                Paths = Directory.GetFiles(ThumbnailsDir, "*", SearchOption.AllDirectories);
                string tempStr;
                foreach (string str in Paths) {
                    tempStr = str.Replace(ThumbnailsDir, "Images");
                    Pictures.Add(new Dictionary<string, string> {
                    {"thumb",str.Replace(OutputDir, "Images") }, {"picture", tempStr},
                        { "name",  Path.GetFileNameWithoutExtension(tempStr)}, {"date", DateFromPath(tempStr) }
>>>>>>> didi
                });
            }
        }

        public string DateFromPath(string path) {
            try {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false)) {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken).ToString();
                }
            } catch (Exception e) {
                return File.GetCreationTime(path).ToString();
            }
        }
    }
}