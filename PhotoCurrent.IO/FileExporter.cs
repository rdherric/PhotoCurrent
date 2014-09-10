using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhotoCurrent.IO
{
    /// <summary>
    /// FileExporter takes a list of files and compiles
    /// them together into a single CSV file with a single
    /// header line.
    /// </summary>
    public class FileExporter
    {
        #region Member Variables
        private List<Double> _xValues = new List<Double>();
        private List<SpectrumFile> _files = new List<SpectrumFile>();
        #endregion


        #region File Processing Methods
        /// <summary>
        /// AddFiles adds the Array of Files represented by the Paths to
        /// the List of SpectrumFiles to be persisted to disk.
        /// </summary>
        /// <param name="inputPaths">The Array of File Paths to add</param>
        public void AddFiles(String[] inputPaths)
        {
            //Iterate through the File Paths and add them
            foreach (String path in inputPaths)
                this.AddFile(path);
        }


        /// <summary>
        /// AddFile adds the File represented by the Path to the 
        /// List of SpectrumFiles that are to be persisted to disk.
        /// </summary>
        /// <param name="inputPath">The File Path to add</param>
        public void AddFile(String inputPath)
        {
            //Create a SpectrumFile with the Path
            SpectrumFile sf = new SpectrumFile(inputPath);

            //Put the XValues into the master List
            this.AddXValues(sf);

            //Add the SpectrumFile to the List
            this._files.Add(sf);
        }


        /// <summary>
        /// Save does the actual work of persisting all of the 
        /// SpectrumFiles to disk in a multiplexed file.
        /// </summary>
        /// <param name="outputPath">The Path to which to save the Spectrum Files</param>
        public void Save(String outputPath)
        {
            //Sort the XValue List
            this._xValues.Sort();

            //Declare the StreamWriter
            StreamWriter sw = null;

            try
            {
                //Create a TextWriter on the output Path
                sw = File.CreateText(outputPath);

                //Write the Header Line to the file
                sw.WriteLine(this.CreateHeaderString());

                //Iterate through all of the XValues and add 
                //the value strings to the file
                foreach (Double d in this._xValues)
                    sw.WriteLine(this.CreateValueString(d));
            }
            finally
            {
                //Close the StreamWriter if necessary
                if (sw != null)
                    sw.Close();
            }
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// AddXValues iterates through the XValues in the current
        /// SpectrumFile and adds them to the Master XValue list if
        /// they do not exist in the List already.
        /// </summary>
        /// <param name="sf">The SpectrumFile to add</param>
        private void AddXValues(SpectrumFile sf)
        {
            //Iterate through the XValues and add them to the 
            //Master XValue list if they aren't there already.
            foreach (Double d in sf.XValues)
            {
                if (this._xValues.Contains(d) == false)
                    this._xValues.Add(d);
            }
        }


        /// <summary>
        /// CreateHeaderString puts together Header line
        /// for the CSV file.
        /// </summary>
        /// <returns></returns>
        private String CreateHeaderString()
        {
            //Declare variables to hold the titles
            String yTitles = String.Empty;
            Enums.SpectrumType totalType = Enums.SpectrumType.Invalid;

            //Iterate through all of the SpectrumFiles and
            //add the File Name
            foreach (SpectrumFile sf in this._files)
            {
                //Set the Y-Axis Title
                yTitles += "," + Path.GetFileName(sf.FilePath);

                //Save the Type in the variable
                totalType |= sf.Type;
            }

            //Finally, figure out the X-Axis Title
            String xTitle = String.Empty;
            if ((totalType & Enums.SpectrumType.Time) > 0 && (totalType & Enums.SpectrumType.Wavelength) > 0)
                xTitle = "Wavelength / Time";
            else if ((totalType & Enums.SpectrumType.Wavelength) > 0)
                xTitle = "Wavelength (nm)";
            else if ((totalType & Enums.SpectrumType.Time) > 0)
                xTitle = "Time (s)";
            
            //Return the result
            return xTitle + yTitles;
        }


        /// <summary>
        /// CreateValueString iterates through all of the SpectrumFiles
        /// and creates a String of existing YValues.
        /// </summary>
        /// <param name="xVal">The XValue to use for the first value</param>
        /// <returns>The String of Values to add to the file</returns>
        private String CreateValueString(Double xVal)
        {
            //Declare a variable to return
            String rtn = xVal.ToString();

            //Iterate through the SpectrumFiles and add the
            //YValue if it exists
            foreach (SpectrumFile sf in this._files)
            {
                //Add a comma to the String -- this is done with or
                //without a YValue
                rtn += ",";

                //Get the index of the current XValue
                Int32 index = sf.XValues.IndexOf(xVal);

                //If the value was found, add the YValue
                if (index > -1)
                    rtn += sf.YValues[index].ToString();
            }

            //Return the result
            return rtn;
        }
        #endregion
    }
}
