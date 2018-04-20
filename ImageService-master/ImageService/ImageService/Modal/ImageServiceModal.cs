using System;
using System.Drawing;
using System.IO;

namespace ImageService.Modal
{
    /// <summary>IImageServiceModal Implementation</summary>
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        #endregion

		
        public string OutputFolder
        {
            get
            {
                return this.m_OutputFolder;
            }
            set
            {
                this.m_OutputFolder = value;
            }
        }


        // Thumbnail Size
        public int ThumbnailSize
        {
            get
            {
                return this.m_thumbnailSize;
            }
            set
            {
                this.m_thumbnailSize = value;
            }
        }
        /// <summary>Create a Year & Month folders in the output folder</summary>
        /// <param name="path">Directory path.</param>
        /// <param name="ImgYear">ImgYear</param>
        /// <param name="ImgMonth">ImgMonth</param>
        /// <returns>string</returns>
        private string CreateFolders(string path, string ImgYear, string ImgMonth)
        {
			//Trying to create the folders
            try
            {
				//Create year folder
                Directory.CreateDirectory(path + "\\" + ImgYear);
				//Create month folder
                Directory.CreateDirectory(path + "\\" + ImgYear + "\\" + ImgMonth);
				//No error, so return nothing
                return string.Empty;
				
            } catch (Exception e)
            {
				//Catches to return the error if didn't create folders
                return e.ToString();
            }        
        }
        
		
		/// <summary>Receive the new path</summary>
        /// <param name="currentPath">The current path</param>
		/// <param name="currentFolderPath">The folder</param>
        /// <returns>The new current path</returns>
        private string newPathGiven(string currentPath, string currentFolderPath)
        {
			//Sets up the counter
            int counter = 1;
			
			//Getting file name and extension
			string fileEnd = Path.GetExtension(currentPath);
            string name = Path.GetFileNameWithoutExtension(currentPath);
			
			//Checks if files exist
            while (File.Exists((currentPath = currentFolderPath + name +" ("+ counter.ToString()+")" + fileEnd)))
            {
                counter++;
            }
            return currentPath;
        }
		
		
		/// <summary>Returns the current date</summary>
        /// <param name="file">The file to check</param>
        /// <returns>The date time</returns>
        static DateTime getDate(string file)
        {
			//Creates a varialble with the current time.
            DateTime cuurentTime = DateTime.Now;
			//adjust to local time
            TimeSpan offSet = cuurentTime - cuurentTime.ToUniversalTime();
            return File.GetLastWriteTimeUtc(file) + offSet;
        }

		
		
		/// <summary>Adds the file given</summary>
        /// <param name="filePath">File path</param>
		/// <param name="creationResult"></param>
        /// <returns></returns>
        public string AddFile(string filePath, out bool creationResult)
        {
            try
            {
				//Create empty strings read to be filled with the details
				string logMessaage = String.Empty;
				string ImgMonth = String.Empty;
                string ImgYear = String.Empty;

				//Checks if the file exists
                if (File.Exists(filePath))
                {
                    // Get file creation time : ImgYear and ImgMonth
                    DateTime currentDate = getDate(filePath);
					ImgMonth = currentDate.Month.ToString();
                    ImgYear = currentDate.Year.ToString();
					//Creates the output directory
                    DirectoryInfo output = Directory.CreateDirectory(m_OutputFolder);
                    // make the output directory hidden
                    output.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    // Create the next folders inside output
                    Directory.CreateDirectory(m_OutputFolder + "\\" + "ImageThumbnails");
                    string folderMessage = this.CreateFolders(m_OutputFolder, ImgYear, ImgMonth);
                    string thumbailMessage = this.CreateFolders(m_OutputFolder + "\\" + "ImageThumbnails", ImgYear, ImgMonth);
					//Checks if a thumbnail was created
                    if (folderMessage != string.Empty || thumbailMessage != string.Empty)
                    {
                        throw new Exception("Folder Creation Error");
                    }
                    string currentFolderPath = m_OutputFolder + "\\" + ImgYear + "\\" + ImgMonth + "\\";
					
                    // Create Thumbnail (Making sure the dile isn't there)
                    string currentStartPath = currentFolderPath + Path.GetFileName(filePath);
                    if (!File.Exists(currentStartPath))
                    {
                        //Chenging the current path
                        currentStartPath = this.newPathGiven(currentStartPath, currentFolderPath);
						
                    }
					//Moving the file
					File.Move(filePath, currentStartPath);
					//The return message
					logMessaage = "Add now " + Path.GetFileName(currentStartPath) + " to " + currentFolderPath;
						
					
                    // Thumbnail creation
					string currentPath = m_OutputFolder + "\\" + "ImageThumbnails" + "\\" + ImgYear + "\\" + ImgMonth + "\\" + Path.GetFileName(filePath);
                    if (File.Exists(currentPath))
                    {
						currentPath = this.newPathGiven(currentPath, m_OutputFolder + "\\" + "ImageThumbnails" + "\\" + ImgYear + "\\" + ImgMonth + "\\");
					}
					
					//Create the image thumbnail
					Image imgThumbnail = Image.FromFile(currentStartPath);
					
					//Using bitmap
					imgThumbnail = (Image)(new Bitmap(imgThumbnail, new Size(this.m_thumbnailSize, this.m_thumbnailSize)));
					
					//Saving the image
					imgThumbnail.Save(currentPath);
					
					//Saving the return message
					logMessaage += " add Image Thumbnail " + Path.GetFileName(filePath);

						
					//Mark the creation as successful
                    creationResult = true;
                    return logMessaage;
                }
                else
                {
					//throw no file error
                    throw new Exception("Error: The file doesn't exist");
                }

            }
            catch (Exception e)
            {
				//Return the error message
                creationResult = false;
                return e.ToString();
            }
        }

    }
}
