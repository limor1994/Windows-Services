using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace ImageServiceWeb.Models
{

    public class PhotosModel
    {
        public int numberOfPhoto { get; set; }
        private string m_OutputDir;
        private string thumbnailPath;
        public List<Photo> images { get; set; }


        /// <summary>
        /// Update photos list
        /// </summary>
        public void updatePhotoList()
        {

            //Create new images list
            images = new List<Photo>();
            numberOfPhoto = 0;

            //Create new index of output
            int count = m_OutputDir.LastIndexOf("\\");
            //Set new dir
            string name = m_OutputDir.Substring(count + 1);
            if (Directory.Exists(thumbnailPath))
            {
                //For each file, iterate
                foreach (string file in System.IO.Directory.GetFiles(
                    thumbnailPath, "*", SearchOption.AllDirectories))
                {
                    //Use regex for files extension
                    Regex setRegex = new Regex(@"(\.bmp$|\.png$|\.jpg$|\.gif$|\.jpeg$)");
                    Match matchRegex = setRegex.Match(file.ToLower());
                    

                    //Match regex success test
                    if (matchRegex.Success)
                    {
                        Photo photo = new Photo(file, name);
                        photo.ID = numberOfPhoto;
                        images.Add(photo);
                        numberOfPhoto++;
                    }
                }
            }
        }



        /// <summary>
        /// Constuctor setup for thumbnails
        /// </summary>
        /// <param name="OutputDir"></param>
        public PhotosModel(string OutputDir)
        {
            if (OutputDir != null)
            {
                m_OutputDir = OutputDir;
                thumbnailPath = m_OutputDir + "\\" + "Thumbnails";
                updatePhotoList();
            }
        }


    }
}