using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;



namespace ImageServiceWeb.Models
{
    public class Photo
    {

        public string ThumbPath { get; set; }
        private string m_realNameOfOutputDir;
        public string ObsolutePathThum { get; set; }
        public string ObsolutePathNormal { get; set; }
        public string OriginalPath { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathStr">Path</param>
        /// <param name="realNameOfOutputDir">name of output directory</param>
        public Photo(string pathStr, string realNameOfOutputDir)
        {
            //Absolute path thumbnail
            ObsolutePathThum = pathStr;
            m_realNameOfOutputDir = realNameOfOutputDir;
            //thumbnails path
            string[] regex = Regex.Split(pathStr, "Thumbnails");
            string new_path_after_regex = regex[0].TrimEnd('\\');
            //Show photo specific
            OriginalPath = "..\\..\\" + realNameOfOutputDir + regex[1]; 
            //Delete dir
            ObsolutePathNormal = new_path_after_regex + regex[1];
            DateTime date_time_data = GetDateTakenFromImage(ObsolutePathNormal);
            //Get year month
            Year = date_time_data.Year;
            Month = date_time_data.Month;
            Name = Path.GetFileName(pathStr);
            //Thumbnail path
            ThumbPath = "..\\..\\" + realNameOfOutputDir + "\\Thumbnails" + regex[1]; 
        }



        /// <summary>
        /// Gets the date from the image
        /// </summary>
        /// <param name="path">image path</param>
        /// <returns></returns>
        public DateTime GetDateTakenFromImage(string path)
        {

            //Create regex to get path
            Regex the_regex = new Regex(":");
            //using filestream get path
            using (FileStream file_stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image image_given = Image.FromStream(file_stream, false, false))
            {
                //Create protepry item
                PropertyItem property = null;
                try
                {
                    property = image_given.GetPropertyItem(36867);
                }
                catch { }

                //Make sure property isn't null to replace encoding 
                if (property != null)
                {
                    string dateTaken = the_regex.Replace(Encoding.UTF8.GetString(property.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
                else
                    return new FileInfo(path).LastWriteTime;
            }
        }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ID:")]
        public int ID { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name:")]
        public string Name { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month:")]
        public int Month { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year:")]
        public int Year { get; set; }
    }
}