using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhotoCurrent.IO.Writer
{
    /// <summary>
    /// The TXT class is used to write out a TXT file to 
    /// disk.
    /// </summary>
    internal class TXT
    {
        #region Member Variables
        private String _filePath = String.Empty;
        private Dictionary<String, String> _headerItems = new Dictionary<String, String>();
        private List<List<Double>> _values = new List<List<Double>>();

        //File writer variables
        private Boolean _saved = false;
        private Boolean _headerWritten = false;
        private Int32 _pointsWritten = 0;
        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor for the Writer.TXT object.
        /// </summary>
        /// <param name="outputPath">The File Path to which to write</param>
        public TXT(String outputPath)
        {
            //Save the path in the Member variable
            this._filePath = outputPath;

            //Initialize the file
            this.Initialize();
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// HeaderItems contains the list of items that were
        /// found in the header of the CSV file.
        /// </summary>
        public Dictionary<String, String> HeaderItems
        {
            get { return this._headerItems; }
        }


        /// <summary>
        /// Values returns the Double list for entry into the Lists
        /// </summary>
        public List<List<Double>> Values
        {
            get { return this._values; }
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
            if (this._pointsWritten < (this._values.Count * this._values[0].Count))
            {
                //Figure out the Row and Column from the number of
                //Points Written
                Int32 currRow = Convert.ToInt32(this._pointsWritten / this.Values[0].Count);
                Int32 currColumn = this._pointsWritten % this.Values[0].Count;

                //Iterate through the rest of the points 
                //and add them to the file
                for (Int32 i = currRow; i < this._values.Count; i++)
                {
                    for (Int32 j = currColumn; j < this._values[i].Count; j++)
                        this.WriteDataPoint(j, this._values[i][j]);
                }
            }
        }


        /// <summary>
        /// WriteHeader writes all of the HeaderItems out to 
        /// a TAB-delimited format.  Based on the Type of
        /// the SpectrumFile, this will have more or less info
        /// in it.
        /// </summary>
        private void WriteHeader()
        {
            //Set the variable to true to indicate that the Header
            //has been written
            this._headerWritten = true;
        }


        /// <summary>
        /// WriteDataPoint writes a single comma-delimited set of 
        /// data points to the file.
        /// </summary>
        /// <param name="column">The Column of the data to write</param>
        /// <param name="z">The Z-Axis value</param>
        private void WriteDataPoint(Int32 column, Double z)
        {
            //Declare a variable to hold the StreamWriter
            StreamWriter sw = null;

            try
            {
                //Open the file to write data
                sw = File.AppendText(this._filePath);

                //If the current column is 0 and this is not the
                //beginning of the file, write a newline.  Otherwise,
                //write a tab
                if (column == 0)
                {
                    if (sw.BaseStream.Length > 0)
                        sw.Write(sw.NewLine);
                }
                else
                    sw.Write('\t');

                //Write the value to the file
                sw.Write(String.Format("{0:E2}", z));

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
