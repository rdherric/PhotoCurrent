using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhotoCurrent.IO.Writer
{
    /// <summary>
    /// The CSV class takes the input and writes a valid
    /// RAW, BKG, or CSV file to disk.
    /// </summary>
    public class CSV : IDisposable
    {
        #region Member Variables
        private String _filePath = String.Empty;
        private Dictionary<String, String> _headerItems = new Dictionary<String, String>();
        private List<Double> _xValues = new List<Double>();
        private List<Double> _yValues = new List<Double>();

        //File writer variables
        private Boolean _saved = false;
        private Boolean _headerWritten = false;
        private Int32 _pointsWritten = 0;
        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor for the Writer.CSV object.
        /// </summary>
        /// <param name="outputPath">The File Path to which to write</param>
        public CSV(String outputPath)
        {
            //Save the path in the Member variable
            this._filePath = outputPath;

            //Initialize the file
            this.Initialize();
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// FilePath is the actual Path that gets written
        /// </summary>
        public String FilePath
        {
            get { return this._filePath; }
        }


        /// <summary>
        /// HeaderItems is a catch-all for the information that
        /// is stored in the header of a CSV file.
        /// </summary>
        public Dictionary<String, String> HeaderItems
        {
            get { return this._headerItems; }
        }


        /// <summary>
        /// XValues is where the X-Axis values get stored 
        /// before writing the file. 
        /// </summary>
        public List<Double> XValues
        {
            get { return this._xValues; }
        }

        
        /// <summary>
        /// YValues is where the Y-Axis values get stored 
        /// before writing the file. 
        /// </summary>
        public List<Double> YValues
        {
            get { return this._yValues; }
        }
        #endregion


        #region Methods to persist data
        /// <summary>
        /// Initialize attempts to create the file.
        /// </summary>
        public void Initialize()
        {
            //First delete the file if it exists
            if (File.Exists(this._filePath) == true)
                File.Delete(this._filePath);

            //Declare a StreamWriter to write to the file
            StreamWriter sw = null;

            try
            {
                //Create the file
                sw = File.CreateText(this._filePath);
            }
            finally
            {
                //Close the StreamWriter if it got opened
                if (sw != null)
                    sw.Close();
            }
        }


        /// <summary>
        /// Save ensures that all of the data has been
        /// flushed to disk and the File object closed properly.
        /// </summary>
        public void Save()
        {
            //If the Header hasn't been written, write it
            if (this._headerWritten == false)
                this.WriteHeader();

            //If the number of points written is less than
            //the points in the Arrays, write the data
            if (this._pointsWritten < this._xValues.Count)
            {
                for (Int32 i = this._pointsWritten; i < this._xValues.Count; i++)
                    this.WriteDataPoint(this._xValues[i], this._yValues[i]);
            }

            //Save the number of Points written
            this._pointsWritten = this._xValues.Count;
        }


        /// <summary>
        /// WriteHeader writes all of the HeaderItems out to 
        /// a comma-delimited format.  Based on the Type of
        /// the SpectrumFile, this will have more or less info
        /// in it.
        /// </summary>
        private void WriteHeader()
        {
            //Get the Type of the file from the extension
            Enums.SpectrumType type = Enums.SpectrumExtension.ToType(Path.GetExtension(this._filePath));

            //Declare a variable to hold the StreamWriter
            StreamWriter sw = null;

            try
            {
                //Open the file for writing
                sw = File.AppendText(this._filePath);

                //Add the file date if it exists, DateTime.Now otherwise
                DateTime fileDate = DateTime.Now;
                if (this._headerItems.ContainsKey(Enums.SpectrumKeys.FileDateKey) == true)
                    fileDate = Convert.ToDateTime(this._headerItems[Enums.SpectrumKeys.FileDateKey]);

                sw.WriteLine(fileDate);

                //Write the other data to the file if necessary
                if ((type & Enums.SpectrumType.BKG) == 0)
                {
                    //Write the Comment
                    sw.WriteLine(this._headerItems[Enums.SpectrumKeys.CommentKey]);

                    //Generate the RAW header format string
                    String rawFormat =
                        Enums.SpectrumKeys.StartKey + ":{0}," +
                        Enums.SpectrumKeys.EndKey + ":{1}," +
                        Enums.SpectrumKeys.IncrementKey + ":{2}," +
                        Enums.SpectrumKeys.DelayKey + ":{3}," +
                        Enums.SpectrumKeys.DataTypeKey + ":{4}";

                    //Generate the RAW header
                    String rawParams = String.Format(rawFormat,
                        this._headerItems[Enums.SpectrumKeys.StartKey],
                        this._headerItems[Enums.SpectrumKeys.EndKey],
                        this._headerItems[Enums.SpectrumKeys.IncrementKey],
                        this._headerItems[Enums.SpectrumKeys.DelayKey],
                        this._headerItems[Enums.SpectrumKeys.DataTypeKey]);

                    //Write the RAW header
                    sw.WriteLine(rawParams);

                    //Write the IPCE header if necessary
                    if ((type & Enums.SpectrumType.IPCE) > 0)
                    {
                        //Generate the IPCE header format string
                        String ipceFormat =
                            Enums.SpectrumKeys.ScaleKey + ":{0}," +
                            Enums.SpectrumKeys.PowerKey + ":{1}";

                        //Generate the RAW header
                        String ipceParams = String.Format(ipceFormat,
                            this._headerItems[Enums.SpectrumKeys.ScaleKey],
                            this._headerItems[Enums.SpectrumKeys.PowerKey]);

                        //Write the IPCE header
                        sw.WriteLine(ipceParams);
                    }
                }
            }
            finally
            {
                //Close the StreamWriter if it got created
                if (sw != null)
                    sw.Close();
            }

            //Set the flag
            this._headerWritten = true;
        }


        /// <summary>
        /// WriteDataPoint writes a single comma-delimited set of 
        /// data points to the file.
        /// </summary>
        /// <param name="x">The X-Axis value</param>
        /// <param name="y">The Y-Axis value</param>
        private void WriteDataPoint(Double x, Double y)
        {
            //Declare a variable to hold the StreamWriter
            StreamWriter sw = null;

            try
            {
                //Open the file to append a line
                sw = File.AppendText(this._filePath);

                //Write the line to the file
                sw.WriteLine(String.Format("{0},{1:E2}", x, y));

                //Increment the Points Written
                this._pointsWritten++;
            }
            finally
            {
                //Close the StreamWriter if it got created
                if (sw != null)
                    sw.Close();
            }
        }
        #endregion


        #region IDisposable Members
        /// <summary>
        /// Dispose makes sure that the CSV class has been 
        /// Finalized.
        /// </summary>
        public void Dispose()
        {
            //If this object hasn't been finalized, do it
            if (this._saved == false)
                this.Save();
        }
        #endregion
    }
}
