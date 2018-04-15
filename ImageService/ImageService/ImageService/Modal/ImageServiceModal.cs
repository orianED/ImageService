using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ImageService.Modal {
    public class ImageServiceModal : IImageServiceModal {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        private static Regex r = new Regex(":");
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServiceModal"/> class.
        /// </summary>
        /// <param name="m_OutputFolder">The m output folder.</param>
        /// <param name="m_thumbnailSize">Size of the m thumbnail.</param>
        public ImageServiceModal(string m_OutputFolder, int m_thumbnailSize) {
            this.m_OutputFolder = m_OutputFolder;
            this.m_thumbnailSize = m_thumbnailSize;
        }

        /// <summary>
        /// The Function Addes A file to the the ouput folder by using interior folders of the cration/taken date.
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <param name="result">variable to recognized if the function suceed or not</param>
        /// <returns>
        /// Indication if the Addition Was Successful
        /// </returns>
        /// <exception cref="Exception">File doesn't exists</exception>
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

                    //adding to regular date path
                    if (!Directory.Exists(datePath)) {
                        Directory.CreateDirectory(datePath);
                    }
                    newImagePath = DuplicateCheck(path, datePath);
                    File.Move(path, newImagePath);

                    //adding to thumbnails folder
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

        /// <summary>
        /// Gets the creation/taken date from image.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static DateTime GetDateTakenFromImage(string path) {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false)) {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

        /// <summary>
        /// check if in the new directory has an image with the same name and change the name if need so by adding a number.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="newDirPath">The new directory path.</param>
        /// <returns></returns>
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
