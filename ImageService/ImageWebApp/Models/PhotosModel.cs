
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ImageWebApp.Models {
    public class PhotosModel {
        public string OutputDir { get; set; }
        public string ThumbnailsDir { get; set; }
        public string[] Paths { get; set; }
        public List<Dictionary<string, string>> Pictures;

        public PhotosModel(string Output, string ThumbnailSize) {
            Pictures = new List<Dictionary<string, string>>();
            try {
                OutputDir = Output;
                ThumbnailsDir = Path.Combine(Output, "Thumbnails");
                Paths = Directory.GetFiles(ThumbnailsDir, "*", SearchOption.AllDirectories);
                string tempStr;
                foreach (string str in Paths) {
                    tempStr = str.Replace(ThumbnailsDir, "Images");
                    Pictures.Add(new Dictionary<string, string> {
                    {"thumb",str.Replace(OutputDir, "Images") }, {"picture", tempStr}
                });
                }
            } catch (Exception e) {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
