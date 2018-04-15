using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageService.Modal {
    public class ImageServiceModal : IImageServiceModal {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        private static Regex r = new Regex(":");
        #endregion

        public ImageServiceModal(string m_OutputFolder, int m_thumbnailSize) {
            this.m_OutputFolder = m_OutputFolder;
            this.m_thumbnailSize = m_thumbnailSize;
        }

        public string AddFile(string path, out bool result) {
            string newImagePath = "", newThumbImagePath = "";
            string datePath, thumbDatePath;
            string month, year;

            try {
                Directory.CreateDirectory(m_OutputFolder).Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                if (File.Exists(path)) {
                    DateTime date;
                    try {
                        date = GetDateTakenFromImage(path);
                    } catch {
                        date = File.GetCreationTime(path);
                    }
                    month = date.Month.ToString();
                    year = date.Year.ToString();
                    datePath = Path.Combine(m_OutputFolder, year, month);
                    thumbDatePath = Path.Combine(m_OutputFolder, "Thumbnails", year, month);

                    if (!Directory.Exists(datePath)) {
                        Directory.CreateDirectory(datePath);
                    }
                    newImagePath = DuplicateCheck(path, datePath);
                    File.Move(path, newImagePath);

                    if (!Directory.Exists(thumbDatePath)) {
                        Directory.CreateDirectory(thumbDatePath);
                    }
                    newThumbImagePath = DuplicateCheck(newImagePath, thumbDatePath);
                    Image thumb = Image.FromFile(newImagePath);
                    thumb = (Image)(new Bitmap(thumb, new Size(this.m_thumbnailSize, this.m_thumbnailSize)));
                    thumb.Save(newThumbImagePath);
                } else {
                    throw new Exception("File doesn't exists");
                }
            } catch (Exception e) {
                result = false;
                return e.ToString();
            }
            result = true;
            return "File transfer succesfully to: " + path + " and make thumbsnail also!";
        }

        public static DateTime GetDateTakenFromImage(string path) {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false)) {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

        public string DuplicateCheck(string filePath, string newDirPath) {
            int counter = 0;
            string newPath = Path.Combine(newDirPath, Path.GetFileName(filePath));
            string pathWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string ext = Path.GetExtension(filePath);

            while (File.Exists(newPath)) {
                counter++;
                newPath = Path.Combine(newDirPath, pathWithoutExt + "(" + counter.ToString() + ")" + ext);
            }
            return newPath;
        }
    }
}
