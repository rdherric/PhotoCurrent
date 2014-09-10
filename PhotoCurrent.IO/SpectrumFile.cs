using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhotoCurrent.IO;
using PhotoCurrent.IO.Enums;


namespace PhotoCurrent.IO
{
    /// <summary>
    /// SpectrumFile encapsulates the basic properties of a spectrum.
    /// It is the class returned by FileManager to create the traces on the 
    /// Spectrum window.
    /// </summary>
    public class SpectrumFile
    {
        #region Member variables
        //Member variables for all files
        private String _filePath = "";
        private SpectrumType _type = SpectrumType.Invalid;
        private DateTime _fileDate = DateTime.MinValue;

        //Member Variables for the RAW files
        private String _comment = String.Empty;
        private UInt32 _start = UInt32.MinValue;
        private UInt32 _end = UInt32.MinValue;
        private UInt32 _increment = UInt32.MinValue;
        private Double _delay = Double.MaxValue;

        //Member variables for CSV files
        private UInt32 _scale = UInt32.MinValue;
        private Double _power = Double.MinValue;

        //Member variables for data for RAW, BKG, CSV
        private List<Double> _xValues = null;
        private List<Double> _yValues = null;

        //Member variables for TXT
        private List<List<Double>> _values3D = null;

        //Writers for the SavePartial method
        Writer.CSV _csv = null;
        Writer.CSV3D _csv3d = null;
        Writer.TXT _txt = null;
        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor to allow a SpectrumFile to be created
        /// without any data in it.
        /// </summary>
        public SpectrumFile()
        {
        }


        /// <summary>
        /// Constructor for the SpectrumFile class.  Takes the file name
        /// and parses the data into the member variables.
        /// </summary>
        /// <param name="filePath">The Path to the File to parse</param>
        public SpectrumFile(String filePath)
        {
            //Save the member variables 
            this._filePath = filePath;
            this._type = SpectrumExtension.ToType(Path.GetExtension(filePath));

            //The file is valid, so parse it
            this.ParseFile();
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// FilePath returns the complete Path to the file 
        /// encapsulated in this object.
        /// </summary>
        public String FilePath
        {
            get { return this._filePath; }
            set { this._filePath = value; }
        }


        /// <summary>
        /// FileCreateDate is the date that the file was created.
        /// </summary>
        public DateTime FileCreateDate
        {
            get { return this._fileDate; }
            set { this._fileDate = value; }
        }


        /// <summary>
        /// Comment returns the comment that has been written to 
        /// the file when the Spectrum was taken.
        /// </summary>
        public String Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }


        /// <summary>
        /// StartWavelength is the beginning wavelength of a scan
        /// or the constant Wavelength of a Time-based scan.
        /// </summary>
        public UInt32 StartWavelength
        {
            get { return this._start; }
            set { this._start = value; }
        }


        /// <summary>
        /// EndWavelength is the end wavelength of a scan
        /// or the constant Wavelength of a Time-based scan.
        /// </summary>
        public UInt32 EndWavelength
        {
            get { return this._end; }
            set { this._end = value; }
        }


        /// <summary>
        /// WavelengthIncrement is the step between successive 
        /// wavelengths in a wavelength-based scan.
        /// </summary>
        public UInt32 WavelengthIncrement
        {
            get { return this._increment; }
            set { this._increment = value; }
        }


        /// <summary>
        /// DelaySeconds is the number of seconds held at a 
        /// wavelength prior to taking data.
        /// </summary>
        public Double DelaySeconds
        {
            get { return this._delay; }
            set { this._delay = value; }
        }


        /// <summary>
        /// Type is the type of file that is being opened.
        /// </summary>
        public Enums.SpectrumType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }


        /// <summary>
        /// XValues is the set of data found in the first
        /// column of a RAW file.
        /// </summary>
        public List<Double> XValues
        {
            get { return this._xValues; }
        }


        /// <summary>
        /// YValues is the set of data found in the first
        /// column of a RAW file.
        /// </summary>
        public List<Double> YValues
        {
            get { return this._yValues; }
        }


        /// <summary>
        /// ThreeDValues returns all of the data that was found
        /// in the 3-D data of a TXT file.
        /// </summary>
        public List<List<Double>> ThreeDValues
        {
            get { return this._values3D; }
        }


        /// <summary>
        /// ScaleWavelength is the wavelength at with a RAW file
        /// was scaled to IPCE.
        /// </summary>
        public UInt32 ScaleWavelength
        {
            get { return this._scale; }
            set { this._scale = value; }
        }


        /// <summary>
        /// ScalePower is the light power in microWatts used to 
        /// scale the RAW or TXT file to IPCE.
        /// </summary>
        public Double ScalePower
        {
            get { return this._power; }
            set { this._power = value; }
        }
        #endregion


        #region Public Methods for Writing files
        /// <summary>
        /// SetupXYArrays allows the creation of Arrays for 
        /// the sake of writing SpectrumFiles.
        /// </summary>
        /// <param name="length">The Length of the desired Arrays</param>
        public void SetupXYArrays(Int32 length)
        {
            //If there are already created Arrays, throw an Exception
            if (this._xValues != null || this._yValues != null)
                throw new System.InvalidOperationException("X and Y Arrays have already been created.");

            //Create the Arrays
            this._xValues = new List<Double>(length);
            this._yValues = new List<Double>(length);
        }


        /// <summary>
        /// SetupThreeDArray allows the creation of a 3-D Array for
        /// the sake of writing SpectrumFiles.
        /// </summary>
        /// <param name="rows">The number of rows in the Array</param>
        /// <param name="columns">The number of columns in the Array</param>
        public void Setup3DArray(Int32 rows, Int32 columns)
        {
            //If the Array is already created, throw an Exception
            if (this._values3D != null)
                throw new System.InvalidOperationException("3-D Array has already been created.");

            //Create the Array rows
            this._values3D = new List<List<Double>>(rows);

            //Create the first Row
            this._values3D.Add(new List<Double>(columns));
        }


        /// <summary>
        /// Save persists the data in the SpectrumFile to disk.
        /// </summary>
        /// <param name="outputPath">The Path to save the SpectrumFile</param>
        /// <param name="overwrite">Boolean to determine if the file should be overwritten</param>
        public void Save(String outputPath, Boolean overwrite)
        {
            //Check to see if the file exists and can't be overwritten
            if (File.Exists(outputPath) == true && overwrite == false)
                throw new ArgumentException("Output File exists and can not be overwritten.");

            //Save the file path in the member variable
            this._filePath = outputPath;

            //Write the file out to the disk
            this.WriteFile();
        }


        /// <summary>
        /// SavePartial writes the data that is currently in the
        /// Arrays out to disk.  SavePartial is used instead of
        /// Save during data acquisition.
        /// </summary>
        public void SavePartial()
        {
            //Create a Writer based on the type of file
            if ((this._type & SpectrumType.THREED) > 0)
            {
                //If this is a TXT file, save it to a Writer.TXT.
                //Otherwise, save it to a CSV3D.
                if ((this._type & SpectrumType.TABFILE) > 0)
                {
                    //Create a new Writer.TXT to write the file if necessary
                    if (this._txt == null)
                        this._txt = new Writer.TXT(this._filePath);

                    //Create the Lists if necessary
                    for (Int32 rows = this._txt.Values.Count; rows < this._values3D.Count; rows++)
                        this._txt.Values.Add(new List<Double>(this._values3D[0].Count));

                    //Set the 3-D value Arrays from the member variable
                    for (Int32 i = this._txt.Values.Count - 1; i < this._values3D.Count; i++)
                    {
                        for (Int32 j = this._txt.Values[i].Count; j < this._values3D[i].Count; j++)
                            this._txt.Values[i].Add(this._values3D[i][j]);
                    }

                    //Persist the file
                    this._txt.Save();
                }
                else
                {
                    //Create a new Writer.CSV3D to write the file if necessary
                    if (this._csv3d == null)
                    {
                        this._csv3d = new Writer.CSV3D(this._filePath);

                        //Setup the Header properties
                        this.SetupCSV3DHeader(this._csv3d);
                    }

                    //Create the Lists if necessary
                    for (Int32 rows = this._csv3d.Values.Count; rows < this._values3D.Count; rows++)
                        this._csv3d.Values.Add(new List<Double>(this._values3D[0].Count));

                    //Set the 3-D value Arrays from the member variable
                    for (Int32 i = this._csv3d.Values.Count - 1; i < this._values3D.Count; i++)
                    {
                        for (Int32 j = this._csv3d.Values[i].Count; j < this._values3D[i].Count; j++)
                            this._csv3d.Values[i].Add(this._values3D[i][j]);
                    }

                    //Persist the file
                    this._csv3d.Save();
                }
            }
            else
            {
                //Create a new Writer.CSV to write the file if necessary
                if (this._csv == null)
                {
                    //Create the CSV
                    this._csv = new Writer.CSV(this._filePath);

                    //Setup the Header properties
                    this.SetupCSVHeader(this._csv);
                }

                //Iterate through the Arrays and add any
                //that haven't been added yet
                for (Int32 i = this._csv.XValues.Count; i < this._xValues.Count; i++)
                {
                    //Add the values to the CSV Lists
                    this._csv.XValues.Add(this._xValues[i]);
                    this._csv.YValues.Add(this._yValues[i]);
                }

                //Finally, persist the file
                this._csv.Save();
            }
        }
        #endregion


        #region Helper Functions
        /// <summary>
        /// Method to figure out what the file is and Parse
        /// it using the appropriate Parser class.
        /// </summary>
        private void ParseFile()
        {
            //Get the file extension from the Path
            String fileExt = Path.GetExtension(this._filePath);

            //Create a Parser based on the type of file
            try 
            {
                if ((this._type & SpectrumType.THREED) > 0)
                {
                    //If the file is a TXT, parse it with Parser.TXT.
                    //Otherwise, parse with CSV3D.
                    if ((this._type & SpectrumType.TABFILE) > 0)
                    {
                        //Create a TXT Parser and get the values from it
                        Parser.TXT txt = new Parser.TXT(this._filePath);

                        //Save the values in the member variable
                        this._values3D = txt.Values;
                    }
                    else
                    {
                        //Create a CSV 3D Parser
                        Parser.CSV3D csv3d = new Parser.CSV3D(this._filePath);

                        //Set the Property Member Variables
                        this._fileDate = Convert.ToDateTime(csv3d.HeaderItems[Enums.SpectrumKeys.FileDateKey]);
                        this._comment = csv3d.HeaderItems[Enums.SpectrumKeys.CommentKey];
                        this._delay = Convert.ToDouble(csv3d.HeaderItems[Enums.SpectrumKeys.DelayKey]);

                        //Set the values for an IPCE file
                        if ((this._type & SpectrumType.IPCE) > 0)
                        {
                            this._scale = Convert.ToUInt32(csv3d.HeaderItems[Enums.SpectrumKeys.ScaleKey]);
                            this._power = Convert.ToDouble(csv3d.HeaderItems[Enums.SpectrumKeys.PowerKey]);
                        }

                        //Save the values in the member variable
                        this._values3D = csv3d.Values;
                    }
                }
                else
                {
                    //Create a Parser
                    Parser.CSV csv = new Parser.CSV(this._filePath);

                    //Set the Properties for all files
                    this._fileDate = Convert.ToDateTime(csv.HeaderItems[Enums.SpectrumKeys.FileDateKey]);

                    //Set the Properties for a RAW and IPCE files
                    if ((this._type & SpectrumType.RAW) > 0 ||
                        (this._type & SpectrumType.IPCE) > 0)
                    {
                        this._comment = csv.HeaderItems[Enums.SpectrumKeys.CommentKey];
                        this._start = Convert.ToUInt32(csv.HeaderItems[Enums.SpectrumKeys.StartKey]);
                        this._end = Convert.ToUInt32(csv.HeaderItems[Enums.SpectrumKeys.EndKey]);
                        this._increment = Convert.ToUInt32(csv.HeaderItems[Enums.SpectrumKeys.IncrementKey]);
                        this._delay = Convert.ToUInt32(csv.HeaderItems[Enums.SpectrumKeys.DelayKey]);

                        //Set the actual SpectrumType based on the value in "Data Type"
                        if (csv.HeaderItems[Enums.SpectrumKeys.DataTypeKey].ToUpper() == "W")
                            this._type |= SpectrumType.Wavelength;
                        else
                            this._type |= SpectrumType.Time;
                    }

                    //Set the values for an IPCE file
                    if ((this._type & SpectrumType.IPCE) > 0)
                    {
                        this._scale = Convert.ToUInt32(csv.HeaderItems[Enums.SpectrumKeys.ScaleKey]);
                        this._power = Convert.ToDouble(csv.HeaderItems[Enums.SpectrumKeys.PowerKey]);
                    }

                    //Set the Values arrays
                    this._xValues = csv.XValues;
                    this._yValues = csv.YValues;
                }
            }
            catch
            {
                throw new ArgumentException("File '" + this._filePath + "' is not a valid '" + fileExt + "' file.");
            }
        }


        /// <summary>
        /// WriteFile figures out what kind of file to write and then 
        /// uses a Writer to persist it.
        /// </summary>
        private void WriteFile()
        {
            //Create a Writer based on the type of file
            if ((this._type & SpectrumType.THREED) > 0)
            {
                //If the file is a TAB file, write with Writer.TXT.
                //Otherwise, write with Writer.CSV3D.
                if ((this._type & SpectrumType.TABFILE) > 0)
                {
                    //Create a new Writer.TXT to write the file
                    Writer.TXT txt = new Writer.TXT(this._filePath);

                    //Set the 3-D value Arrays from the member variable
                    foreach (List<Double> row in this._values3D)
                        txt.Values.Add(new List<Double>(row));

                    //Persist the file
                    txt.Save();
                }
                else
                {
                    //Create a new Writer.R3D to write the file
                    Writer.CSV3D csv3d = new Writer.CSV3D(this._filePath);

                    //Setup the Header properties
                    this.SetupCSV3DHeader(csv3d);

                    //Set the 3-D value Arrays from the member variable
                    foreach (List<Double> row in this._values3D)
                        csv3d.Values.Add(new List<Double>(row));

                    //Persist the file
                    csv3d.Save();
                }
            }
            else
            {
                //Create a new Writer.CSV to write the file
                Writer.CSV csv = new Writer.CSV(this._filePath);

                //Setup the Header properties
                this.SetupCSVHeader(csv);

                //Set the Values arrays
                csv.XValues.AddRange(this._xValues);
                csv.YValues.AddRange(this._yValues);

                //Finally, persist the file
                csv.Save();
            }
        }


        /// <summary>
        /// SetupCSVHeader puts the values from the member variables
        /// into a CSV writer.
        /// </summary>
        /// <param name="csv">The CSV Writer to set header properties</param>
        private void SetupCSVHeader(Writer.CSV csv)
        {
            //Set the Properties for all files
            csv.HeaderItems[SpectrumKeys.FileDateKey] = DateTime.Now.ToString();

            //Set the Properties for RAW and IPCE files
            if ((this._type & SpectrumType.RAW) > 0 ||
                (this._type & SpectrumType.IPCE) > 0)
            {
                csv.HeaderItems[SpectrumKeys.CommentKey] = this._comment;
                csv.HeaderItems[SpectrumKeys.StartKey] = this._start.ToString();
                csv.HeaderItems[SpectrumKeys.EndKey] = this._end.ToString();
                csv.HeaderItems[SpectrumKeys.IncrementKey] = this._increment.ToString();
                csv.HeaderItems[SpectrumKeys.DelayKey] = this._delay.ToString();

                //Get the SpectrumType string
                String dataType = "W";
                if ((this._type & SpectrumType.Time) > 0)
                    dataType = "T";

                //Set the SpectrumType
                csv.HeaderItems[SpectrumKeys.DataTypeKey] = dataType;
            }

            //Set the values for an IPCE file
            if ((this._type & SpectrumType.IPCE) > 0)
            {
                csv.HeaderItems[SpectrumKeys.ScaleKey] = this._scale.ToString();
                csv.HeaderItems[SpectrumKeys.PowerKey] = this._power.ToString();
            }
        }


        /// <summary>
        /// SetupCSV3DHeader puts the values from the member variables
        /// into a CSV3D writer.
        /// </summary>
        /// <param name="csv3d">The CSV3D Writer to set header properties</param>
        private void SetupCSV3DHeader(Writer.CSV3D csv3d)
        {
            //Set the Properties for all files
            csv3d.HeaderItems[SpectrumKeys.FileDateKey] = DateTime.Now.ToString();

            //Set the Properties for RAW and IPCE files
            if ((this._type & SpectrumType.RAW) > 0 ||
                (this._type & SpectrumType.IPCE) > 0)
            {
                csv3d.HeaderItems[SpectrumKeys.CommentKey] = this._comment;
                csv3d.HeaderItems[SpectrumKeys.DelayKey] = this._delay.ToString();
            }

            //Set the values for an IPCE file
            if ((this._type & SpectrumType.IPCE) > 0)
            {
                csv3d.HeaderItems[SpectrumKeys.ScaleKey] = this._scale.ToString();
                csv3d.HeaderItems[SpectrumKeys.PowerKey] = this._power.ToString();
            }
        }
        #endregion
    }
}
