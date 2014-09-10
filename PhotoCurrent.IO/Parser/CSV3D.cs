using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhotoCurrent.IO.Enums;

namespace PhotoCurrent.IO.Parser
{
    /// <summary>
    /// The CSV3D Parser class opens and parses the values in 
    /// a CSV3D file, which is a format that contains data in
    /// the following CSV-like pattern:  Column, Row, Z-Value.
    /// </summary>
    internal class CSV3D
    {
        #region Member Variables
        private String _filePath = String.Empty;
        private Dictionary<String, String> _headerItems = new Dictionary<String, String>();
        private List<List<Double>> _values = new List<List<Double>>();
        #endregion


        #region Constructor
        /// <summary>
        /// The CSV3D constructor takes a path to the file to be 
        /// parsed.  The contents can be accessed through the properties.
        /// </summary>
        /// <param name="filePath">The Path to the File to Parse</param>
        public CSV3D(String filePath)
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
        /// found in the header of the R3D file.
        /// </summary>
        public Dictionary<String, String> HeaderItems
        {
            get { return this._headerItems; }
        }


        /// <summary>
        /// Values returns all of the 3-D values in the R3D file.
        /// </summary>
        public List<List<Double>> Values
        {
            get { return this._values; }
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
            if (line.Contains(SpectrumKeys.DelayKey) == true ||
                (line.Contains(SpectrumKeys.ScaleKey) && line.Contains(SpectrumKeys.PowerKey)) == true)
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
        /// and puts the values into the List of Arrays.
        /// </summary>
        /// <param name="line">The String to parse</param>
        private Boolean ParseNumericalLine(String line)
        {
            //Declare a variable to return
            Boolean rtn = true;

            //Attempt to Parse all of the values
            try 
            {
                //Split the line on a Tab
                String[] splitLine = line.Split(',');

                //If there are values in the line, parse them
                if (splitLine.Length > 0)
                {
                    //Get the Column, Row and Z values
                    Int32 x = Int32.Parse(splitLine[0]);
                    Int32 y = Int32.Parse(splitLine[1]);
                    Double z = Double.Parse(splitLine[2]);

                    //Insert the value into the 3-D List
                    this.InsertZValue(x, y, z);
                }
            }
            catch 
            {
                //Set the return to false to indicate failure
                //to parse the whole line
                rtn = false;
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// InsertZValue creates blank values if necessary in the List
        /// and inserts the given value at the appropriate place.
        /// </summary>
        /// <param name="x">The Column of the Z-Value</param>
        /// <param name="y">The Row of the Z-Value</param>
        /// <param name="z">The value to insert into the List</param>
        private void InsertZValue(Int32 x, Int32 y, Double z)
        {
            //Add rows to the specified Row
            for (Int32 i = this._values.Count; i <= y; i++)
                this._values.Add(new List<Double>());

            //Add Columns to the specified Column
            for (Int32 j = this._values[y].Count; j < x; j++)
                this.Values[y].Add(0.0);

            //Finally, add the Z-Value
            this._values[y].Add(z);
        }
        #endregion
    }
}
