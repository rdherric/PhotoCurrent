using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhotoCurrent.IO.Enums;


namespace PhotoCurrent.IO.Parser
{
    /// <summary>
    /// The CSV class opens a RAW, BKG, or CSV file, detects the number
    /// of text lines at the top, and returns the contents as two
    /// arrays of data and a Dictionary of text lines.
    /// </summary>
    internal class CSV
    {
        #region Member Variables
        private String _filePath = String.Empty;
        private Dictionary<String, String> _headerItems = new Dictionary<String, String>();
        private List<Double> _xValues = new List<Double>();
        private List<Double> _yValues = new List<Double>();
        #endregion


        #region Constructor
        /// <summary>
        /// The CSVParser constructor takes a path to the file to be 
        /// parsed.  The contents can be accessed through the properties.
        /// </summary>
        /// <param name="filePath">The Path to the File to Parse</param>
        public CSV(String filePath)
        {
            //Initialize the member variables
            this._filePath = filePath;

            //Parse the file
            this.Parse();
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
        /// XValues returns the values in the CSV file that were in 
        /// the first column.
        /// </summary>
        public List<Double> XValues
        {
            get { return this._xValues; }
        }


        /// <summary>
        /// YValues returns the values in the CSV file that were in 
        /// the second column.
        /// </summary>
        public List<Double> YValues
        {
            get { return this._yValues; }
        }
        #endregion


        #region Helper Functions
        /// <summary>
        /// The Parse function does the actual dirty work of opening
        /// the file, reading the header, and parsing the contents.
        /// </summary>
        private void Parse()
        {
            //Try to open the file.  If the file doesn't open, the 
            //Exception will be caught by the next higher up level.
            StreamReader sr = File.OpenText(this._filePath);

            //Iterate through all of the lines in the file 
            //and parse them
            while (sr.EndOfStream == false)
            {
                //Read a line
                String line = sr.ReadLine();

                //If the line is a header line, parse it into the
                //HeaderItems dict.  Otherwise, parse it into the
                //Double Arrays.
                if (this.ParseNumericalLine(line) == false)
                    this.ParseHeaderLine(line);
            }

            //Close the StreamReader
            sr.Close();
        }


        /// <summary>
        /// ParseHeaderLine pulls apart the header line and 
        /// puts it into the Dictionary.
        /// </summary>
        /// <param name="line">The String to parse</param>
        private void ParseHeaderLine(String line)
        {
            //If the line parses to a valid date, save it
            try
            {
                //Parse the DateTime
                DateTime fileDate = DateTime.Parse(line);

                //Put the value in the Dictionary
                this._headerItems[SpectrumKeys.FileDateKey] = line;

                //Return as the parse was successful
                return;
            }
            catch { }

            //If the line contains the scan parameters, parse them out.
            //If it contains the scaling parameters, parse those out.
            //Otherwise, this must be the comment line.
            if ((line.Contains(SpectrumKeys.StartKey) && line.Contains(SpectrumKeys.EndKey) && line.Contains(SpectrumKeys.IncrementKey) &&
                 line.Contains(SpectrumKeys.DelayKey) && line.Contains(SpectrumKeys.DataTypeKey)) ||
                (line.Contains(SpectrumKeys.ScaleKey) && line.Contains(SpectrumKeys.PowerKey)))
                this.ParseParamLine(line);
            else
                this._headerItems[SpectrumKeys.CommentKey] = line;
        }


        /// <summary>
        /// ParseParamLine parses any parameters in the line into the 
        /// Dictionary along with the keys.
        /// </summary>
        /// <param name="line"></param>
        private void ParseParamLine(String line)
        {
            //Split the line on commas
            String[] splitLine = line.Split(',');

            //Iterate through the Array and put the values
            //in the Dictionary
            foreach (String keyValPair in splitLine)
            {
                //Split the line on a colon
                String[] vals = keyValPair.Split(':');

                //Put the values in the Dictionary
                this._headerItems[vals[0]] = vals[1];
            }
        }


        /// <summary>
        /// ParseNumericalLine pulls apart the numerical line
        /// and puts the values into the X / Y Arrays.
        /// </summary>
        /// <param name="line">The String to parse</param>
        private Boolean ParseNumericalLine(String line)
        {
            //Declare a variable to return
            Boolean rtn = false;

            //Split the line on a comma
            String[] splitLine = line.Split(',');

            //If there are two values, parse them
            if (splitLine.Length == 2)
            {
                //Try to parse the numbers
                try
                {
                    //Parse the numbers
                    Double x = Double.Parse(splitLine[0]);
                    Double y = Double.Parse(splitLine[1]);

                    //Put the numbers into the Arrays
                    this._xValues.Add(x);
                    this._yValues.Add(y);

                    //Set the return to true
                    rtn = true;
                }
                catch { }
            }

            //Return the result
            return rtn;
        }
        #endregion
    }
}
