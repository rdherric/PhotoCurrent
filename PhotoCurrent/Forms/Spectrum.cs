using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using PhotoCurrent.Config;
using PhotoCurrent.IO;
using PhotoCurrent.IO.Enums;
using RDH2.Utilities.Configuration;
using RDH2.Utilities.Format;
using DCW = Dundas.Charting.WinControl;

namespace PhotoCurrent.Forms
{
    /// <summary>
    /// Spectrum is the window that will hold all types 
    /// of spectra.  This is the main class for opening
    /// and viewing spectra.
    /// </summary>
    public partial class Spectrum : Form
    {
        #region Member Variables
        private SpectrumType _specType = SpectrumType.Invalid;
        private FileManager _fileMgr = new FileManager();
        private SpectrumFile _acqSpecFile = null;
        private Acquisition.SpectrumBase _acqSpecBase = null;
        private Int32 _acqMultiplier = 0;
        private DCW.Series _acqSeries = null;
        private const Int32 _titleLength = 40;
        private const Single _maxChartSize = 90.0F;
        private const Single _minChartSize = 20.0F;
        private const String _defChartAreaKey = "Default";

        //Properties for the Toolbar / Menu
        private Boolean _runningSpec = false;
        private Boolean _zoomedIn = false;

        //Delegates for the Window real-time invocation
        private delegate void InvalidateChartDelegate();
        private delegate void SetToolStripStatusLabelTextDelegate(ToolStripStatusLabel lbl, String newText);
        private delegate void SetupWindowAfterAcquisition(SpectrumFile sf);
        #endregion


        #region Constructor
        public Spectrum()
        {
            InitializeComponent();

            //Setup the Show event -- this is where the window will get sized
            this.Shown += new EventHandler(Spectrum_Shown);
            
            //Setup the Close event
            this.FormClosing += new FormClosingEventHandler(Spectrum_FormClosing);

            //Setup the chart MouseMove event
            this.chart.MouseMove += new MouseEventHandler(chart_MouseMove);

            //Subscribe to the Zoom event
            this.chart.SelectionRangeChanged += new Dundas.Charting.WinControl.Chart.CursorEventHandler(chart_SelectionRangeChanged);
        }
        #endregion


        #region Spectrum Window Events
        //Event Definitions
        public event EventHandler ZoomChanged;
        public event EventHandler AcquisitionStopped;


        /// <summary>
        /// Resizes the window when it is Shown
        /// </summary>
        /// <param name="sender">The Window that is Shown</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void Spectrum_Shown(object sender, EventArgs e)
        {
            //Get the Config from the app.config
            ConfigHelper<SpectrumWindow> config = new ConfigHelper<SpectrumWindow>(ConfigLocation.AllUsers);
            SpectrumWindow swc = config.GetConfig();
            
            //Declare variables to hold the values
            Int32 heightVal = swc.Height;
            Int32 widthVal = swc.Width;

            //If the window is not maximized, do some spiffy calcs to
            //make sure the window fits
            if (swc.State != FormWindowState.Maximized)
            {
                //Iterate through all of the controls on the MdiParent
                foreach (Control cont in this.MdiParent.Controls)
                {
                    //If the MDIControl is found, set the height and
                    //width appropriately
                    if (cont is MdiClient)
                    {
                        //Calculate the Window size if necessary so that the 
                        //child windows never are bigger than the parent
                        if (heightVal >= cont.Height)
                            heightVal = Convert.ToInt32(cont.Height * (Spectrum._maxChartSize / 100));

                        if (widthVal >= cont.Width)
                            widthVal = Convert.ToInt32(cont.Width * (Spectrum._minChartSize / 100));
                    }
                }
            }

            //Setup the window
            this.Height = heightVal;
            this.Width = widthVal;
            this.WindowState = swc.State;
        }

        
        /// <summary>
        /// Spectrum_FormClosing simply saves the Window Properties 
        /// in the app.config file
        /// </summary>
        /// <param name="sender">The Window being disposed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void Spectrum_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Get the ConfigSection
            ConfigHelper<SpectrumWindow> config = new ConfigHelper<SpectrumWindow>(ConfigLocation.AllUsers);
            SpectrumWindow swc = config.GetWriteableConfig();

            //Set the properties
            swc.Height = this.Height;
            swc.Width = this.Width;
            swc.State = this.WindowState;

            //Save the properties in the app.config
            config.SetConfig(swc);

            //If the form was acquiring data, stop it
            if (this._acqSpecBase != null)
                this._acqSpecBase.Stop();
        }
        #endregion


        #region File Methods and Properties
        /// <summary>
        /// OpenFiles takes an Array of File Paths, turns them
        /// into SpectrumFile objects, and adds them to the 
        /// current Chart.
        /// </summary>
        /// <param name="filePaths">The Files to open in this Window</param>
        /// <returns>Boolean TRUE to indicate that it can be shown, FALSE otherwise</returns>
        public Boolean OpenFiles(String[] filePaths)
        {
            //Parse the files in the file Paths
            try
            {
                this._fileMgr.AddSpectrumFiles(filePaths);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(this, e.Message);
                return false;
            }

            //Clear the Chart Series so that all of the Series can
            //be added back to it
            this.chart.Series.Clear();
            this.ChartArea.AxisY.SubAxes.Clear();

            //Setup the Chart based on the Type of the first
            //SpectrumFile in the FileManager
            if (this._fileMgr.SpectrumFiles[0].Type != SpectrumType.THREED)
                this.Set2DChartBaseProperties();
            else
                this.Set3DChartBaseProperties();

            //Reset the SpectrumType 
            this._specType = SpectrumType.Invalid;

            //Now add the SpectrumFiles to the Chart
            foreach (SpectrumFile sf in this._fileMgr.SpectrumFiles)
                this.AddSpectrum(sf);

            //Apply the multipliers based on the type of file
            if ((this._fileMgr.SpectrumFiles[0].Type & SpectrumType.TWOD) > 0)
                this.ApplyTypeMultipliers();

            //Return true
            return true;
        }


        /// <summary>
        /// SpectrumType lets the main window know what kinds of 
        /// spectra can be added to this window.
        /// </summary>
        public SpectrumType SpectrumType
        {
            get { return this._specType; }
        }
        #endregion


        #region Main AddSpectrum Methods
        /// <summary>
        /// AddSpectrum adds the current SpectrumFile to the chart.
        /// </summary>
        /// <param name="sf">The SpectrumFile object to add to the Chart</param>
        private void AddSpectrum(SpectrumFile sf)
        {
            //Add and setup the Series
            this.SetupSeries(sf, null);
        }


        /// <summary>
        /// AddSpectrum adds the SpectrumBase data acquisition
        /// object to the Chart.
        /// </summary>
        /// <param name="sb">The SpectrumBase object to use to begin acquiring data</param>
        /// <returns>Boolean TRUE if the Types match, FALSE otherwise</returns>
        public Boolean AddSpectrum(Acquisition.SpectrumBase sb)
        {
            //Declare a variable to return
            Boolean rtn = false;

            //Try to add the SpectrumFile
            try
            {
                //Create a SpectrumFile object and set it up
                this._acqSpecFile = new SpectrumFile();
                this._acqSpecFile.Comment = sb.Config.Comment;
                this._acqSpecFile.DelaySeconds = sb.Config.AcquisitionDelay;
                this._acqSpecFile.EndWavelength = sb.Config.EndWavelength;
                this._acqSpecFile.FileCreateDate = DateTime.Now;
                this._acqSpecFile.FilePath = sb.Config.FilePath;
                this._acqSpecFile.StartWavelength = sb.Config.StartWavelength;
                this._acqSpecFile.Type = SpectrumExtension.ToType(Path.GetExtension(sb.Config.FilePath));
                this._acqSpecFile.WavelengthIncrement = sb.Config.WavelengthIncrement;

                //Add the SpectrumFile to the FileManager
                this._fileMgr.AddNewSpectrum(this._acqSpecFile);

                //Setup the data Arrays based on the Type
                if ((sb.Type & SpectrumType.TWOD) > 0)
                    this._acqSpecFile.SetupXYArrays(Convert.ToInt32(sb.TotalPoints));
                else if ((sb.Type & SpectrumType.THREED) > 0)
                    this._acqSpecFile.Setup3DArray(Convert.ToInt32(sb.Rows3D), Convert.ToInt32(sb.Columns3D));

                //If the chart is supposed to be shown, set it up.
                //Otherwise, hide the control for now.
                if (sb.Config.NoDisplay == false)
                {
                    //Setup the Chart based on the Type of the first
                    //SpectrumFile in the FileManager
                    if (this._fileMgr.SpectrumFiles[0].Type != SpectrumType.THREED)
                        this.Set2DChartBaseProperties();
                    else
                        this.Set3DChartBaseProperties();

                    //Turn the SpectrumFile into a Series and save the 
                    //Series in the member variable
                    this._acqSeries = this.SetupSeries(this._acqSpecFile, sb);

                    //Setup the chart Axes with the SpectrumBase
                    this.SetupNewSpectrumAxes(sb);
                }
                else
                {
                    //Hide the chart
                    this.chart.Visible = false;
                }

                //Save the SpectrumBase in the member variable
                this._acqSpecBase = sb;

                //Subscribe the SpectrumBase DataReady event and start it going
                sb.DataReady += new Acquisition.DataReadyEventHandler(sb_DataReady);
                sb.AcquistionStopped += new EventHandler(sb_AcquistionStopped);
                sb.Start();

                //Set the flag to show the Form is acquiring data
                this._runningSpec = true;

                //Set the return to true
                rtn = true;
            }
            catch (System.Exception e)
            {
                //Popup an error message
                MessageBox.Show(e.Message);

                //Set the return value
                rtn = false;
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// SetupSeries does the manual labor of putting the Series
        /// into the plot.
        /// </summary>
        /// <param name="sf">The SpectrumFile to turn into a Series</param>
        /// <param name="sb">The SpectrumBase that represents this data collection</param>
        private DCW.Series SetupSeries(SpectrumFile sf, Acquisition.SpectrumBase sb)
        {
            //Create the Series for the Spectrum
            DCW.Series rtn = this.chart.Series.Add(Path.GetFileName(sf.FilePath));

            //Set the Series default properties
            if ((sf.Type & SpectrumType.TWOD) > 0)
                this.Set2DSeriesProperties(rtn, sf);
            else
                this.Set3DSeriesProperties(rtn, sf, sb);

            //Set the value of the SpectrumType for use
            //by the Main window
            this._specType |= sf.Type;

            //Format the Title of the window
            this.FormatWindowTitle();

            //Set the titles on the Axes
            this.FormatAxisTitles();

            //Return the result
            return rtn;
        }


        /// <summary>
        /// SetupNewSpectrumAxes does the setup of the Axes on the
        /// Chart based on the specified SpectrumBase.
        /// </summary>
        /// <param name="sb">The SpectrumBase used to set up the Axes</param>
        private void SetupNewSpectrumAxes(Acquisition.SpectrumBase sb)
        {
            //Declare variables to hold the Min and Max
            Double xMin = Double.MinValue;
            Double xMax = Double.MaxValue;
            Double yMin = Double.MinValue;
            Double yMax = Double.MaxValue;

            //Set the Min and Max based on the type of spectrum being
            //created in the window
            if ((sb.Type & SpectrumType.TWOD) > 0)
            {
                //Figure out X-Axis min / max from the type of Spectrum
                if ((sb.Type & SpectrumType.Time) > 0)
                {
                    //Set the values to 0 - Autoscale for time
                    xMin = 0;
                    xMax = Double.NaN;
                }
                else
                {
                    //Figure out the X-Axis min and max from the SpectrumBase
                    xMin = Convert.ToDouble(sb.Config.StartWavelength);
                    xMax = Convert.ToDouble(sb.Config.EndWavelength);

                    if (xMin > xMax)
                    {
                        xMin = Convert.ToDouble(sb.Config.EndWavelength);
                        xMax = Convert.ToDouble(sb.Config.StartWavelength);
                    }
                }

                //Calculate the Y-Axis min / max real values
                yMin = sb.MaximumCurrent * -0.05;
                yMax = sb.MaximumCurrent;

                //Get the base type of the SpectrumBase
                SpectrumType baseType = SpectrumExtension.ToType(Path.GetExtension(sb.Config.FilePath));

                //Get the Multiplier for the current Chart
                Int32 multiplier = this.CalculateTypeMultiplier(baseType);
                
                //Get the multiplier for the Y-Axis Max if an Invalid
                //value was returned
                if (multiplier == Int32.MinValue)
                    multiplier = this.CalculateValueMultiplier(yMax);

                //Multiply the Y-Axis values
                Double power = Math.Pow(10.0, Convert.ToDouble(multiplier * -1.0));
                yMin *= power;
                yMax *= power;

                //Save the power value in the member variable
                this._acqMultiplier = multiplier;
            }
            else if ((sb.Type & SpectrumType.THREED) > 0)
            {
                //Set the values from Rows and Columns
                xMin = 0.0;
                xMax = Convert.ToDouble(sb.Columns3D);
                yMin = 0.0;
                yMax = Convert.ToDouble(sb.Rows3D);
            }

            //Finally, set the Mins and Maxes
            if (Double.IsNaN(this.ChartArea.AxisX.Minimum) == true || this.ChartArea.AxisX.Minimum > xMin)
                this.ChartArea.AxisX.Minimum = xMin;

            if (Double.IsNaN(this.ChartArea.AxisX.Maximum) == true || this.ChartArea.AxisX.Maximum < xMax)
                this.ChartArea.AxisX.Maximum = xMax;

            if (Double.IsNaN(this.ChartArea.AxisY.Minimum) == true || this.ChartArea.AxisY.Minimum > yMin)
                this.ChartArea.AxisY.Minimum = yMin;

            if (Double.IsNaN(this.ChartArea.AxisY.Maximum) == true || this.ChartArea.AxisY.Maximum < yMax)
                this.ChartArea.AxisY.Maximum = yMax;
        }


        /// <summary>
        /// FormatWindowTitle generates a valid title for the window
        /// from the FileNames of the open files
        /// </summary>
        private void FormatWindowTitle()
        {
            //Declare a variable for the Title
            String title = String.Empty;

            //Iterate through the Files in the FileManager
            //and generate an abbreviated Title
            foreach (SpectrumFile sf in this._fileMgr.SpectrumFiles)
            {
                //If there are already files in the Title, add a separator
                if (title != String.Empty)
                    title += " / ";

                //Add the current Title
                title += Path.GetFileName(sf.FilePath);

                //If the title is longer than 25 chars, truncate
                //and add some periods
                if (title.Length > Spectrum._titleLength)
                {
                    title = title.Substring(0, Spectrum._titleLength) + "...";
                    break;
                }
            }

            //If there still isn't a Title, set a default
            if (title == String.Empty)
                title = "No Files Open";

            //Finally, set the Title on the Window
            this.Text = title;
        }


        /// <summary>
        /// FormatAxisTitles checks the Type of the SpectrumFiles
        /// and Axes to set the Titles appropriately.
        /// </summary>
        private void FormatAxisTitles()
        {
            //Set the X-Axis title from the Type
            String xAxisTitle = "Wavelength (nm)";
            if ((this._specType & SpectrumType.Time) > 0)
                xAxisTitle = "Time (seconds)";
            else if ((this._specType & SpectrumType.THREED) > 0)
                xAxisTitle = "Position (X)";

            this.ChartArea.AxisX.Title = xAxisTitle;

            //Set the Main Y-Axis title
            this.ChartArea.AxisY.Title = this.YAxisTitleFromName(this.ChartArea.AxisY.Name);

            //Iterate through the SubYAxes and set the titles
            //on those
            foreach (DCW.SubAxis sa in this.ChartArea.AxisY.SubAxes)
                sa.Title = this.YAxisTitleFromName(sa.Name);
        }


        /// <summary>
        /// YAxisTitleFromName takes the SpectrumType of the Y-Axis
        /// and turns it into a Title.
        /// </summary>
        /// <param name="name">The Name of the Y-Axis</param>
        /// <returns>The Title that makes sense for the Type</returns>
        private String YAxisTitleFromName(String name)
        {
            //Declare a variable to return
            String rtn = "Arbitrary Units";

            //Parse the Name to an Enum
            SpectrumType type = (SpectrumType)Enum.Parse(typeof(SpectrumType), name);

            //Calculate the maximum Type Multiplier
            Int32 multiplier = this.CalculateTypeMultiplier(type);

            //Return the Title based on the Type
            if ((type & SpectrumType.BKG) > 0)
                rtn = "Thermopile Output (AU)";
            else if ((type & SpectrumType.IPCE) > 0)
            {
                //Setup the base string
                rtn = "IPCE";

                //Add the multiplier if necessary
                if (multiplier < -1)
                    rtn += " * E" + multiplier.ToString();
            }
            else if ((type & SpectrumType.RAW) > 0)
            {
                //If the multiplier is Int32.MinValue, then this
                //is a first-run spectrum, so use the SpectrumBase
                //to get the multiplier
                if (multiplier == Int32.MinValue && this._acqSpecBase != null)
                    multiplier = this.CalculateValueMultiplier(this._acqSpecBase.MaximumCurrent);

                //Format the string
                rtn = "Photocurrent (A * E" + multiplier.ToString() + ")";
            }
            else if ((type & SpectrumType.THREED) > 0)
                rtn = "Position (Y)";

            //Return the result
            return rtn;
        }


        /// <summary>
        /// CalculateTypeMultiplier iterates through all of the SpectrumFiles
        /// of the particular type and gets the maximum multiplier for it.
        /// </summary>
        /// <param name="type">The SpectrumType for which to get the multiplier</param>
        /// <returns>The Int32 multiplier of the SpectrumFile</returns>
        private Int32 CalculateTypeMultiplier(SpectrumType type)
        {
            //Declare a variable to return
            Int32 rtn = Int32.MinValue;

            //Iterate through the SpectrumFiles and get the Maximum
            foreach (SpectrumFile sf in this._fileMgr.SpectrumFiles)
            {
                //If the type doesn't match, continue
                if ((sf.Type & type) == 0)
                    continue;
                
                //Get the Series Multiplier
                Int32 seriesMult = this.CalculateSpectrumMultiplier(sf);

                //If the current multiplier is greater than rtn, save it
                if (seriesMult < 0 && seriesMult > rtn)
                    rtn = seriesMult;
                else if (seriesMult > 0 && seriesMult < rtn)
                    rtn = seriesMult;
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// CalculateSpectrumMultiplier gets the maximum multiplier
        /// for the current SpectrumFile.
        /// </summary>
        /// <param name="s">The SpectrumFile for which to calculate the multiplier</param>
        /// <returns>Int32 multiplier value</returns>
        private Int32 CalculateSpectrumMultiplier(SpectrumFile sf)
        {
            //Declare a variable to return
            Int32 rtn = Int32.MinValue;

            //Get the Maximum of the SpectrumFile based on the type 
            //of Spectrum
            Double max = Double.MinValue;
            if (sf.YValues != null)
            {
                //Iterate through the Y-Values
                foreach (Double d in sf.YValues)
                {
                    if (d > max)
                        max = d;
                }
            }
            else if (sf.ThreeDValues != null)
            {
                //Iterate through the Lists and get the Maximum
                foreach (List<Double> ld in sf.ThreeDValues)
                {
                    foreach (Double d in ld)
                    {
                        if (d > max)
                            max = d;
                    }
                }
            }

            //If the max value was found, use it
            if (max != Double.MinValue)
                rtn = this.CalculateValueMultiplier(max);

            //Return the result
            return rtn;
        }


        /// <summary>
        /// CalculateValueMultiplier takes a Double and gets the
        /// Int32 Exponent value.
        /// </summary>
        /// <param name="val">The Double to get the Multiplier</param>
        /// <returns>The multiplier of the Double</returns>
        private Int32 CalculateValueMultiplier(Double val)
        {
            //Declare a variable to return
            Int32 rtn = 0;

            //Figure out the multiplier -- positive or negative
            if (val < 1)
            {
                //Find the negative exponent
                while (val < 1)
                {
                    val *= 10;
                    rtn--;
                }
            }
            else
            {
                //Find the positive exponent
                while (val > 10)
                {
                    val /= 10;
                    rtn++;
                }
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// ApplyMultiplier retrieves and calculates the multipliers 
        /// on a per-SpectrumType, per-Series basis.
        /// </summary>
        private void ApplyTypeMultipliers()
        {
            //Create a SpectrumType to hold the Types that have 
            //had the multiplier applied
            SpectrumType processedFlag = SpectrumType.Invalid;

            //Iterate through all of the SpectrumFiles in the FileManager
            for (Int32 i = 0; i < this._fileMgr.SpectrumFiles.Count; i++)
            {
                //If this type has been processed, just continue
                if ((this._fileMgr.SpectrumFiles[i].Type & processedFlag) > 0)
                    continue;

                //Get the base type for the SpectrumFile
                SpectrumType type = SpectrumExtension.ToType(Path.GetExtension(this._fileMgr.SpectrumFiles[i].FilePath));

                //Get the multiplier for the Type
                Int32 multiplier = this.CalculateTypeMultiplier(type);

                //Finally, apply the multiplier to the Series
                if (multiplier != Int32.MinValue && (multiplier < -1 || multiplier > 1))
                {
                    //Calculate the actual exponent value
                    Double power = Math.Pow(10.0, Convert.ToDouble(multiplier * -1));

                    //Iterate through the SpectrumFiles again and get the 
                    //Series connected to them.  Apply the multiplier.
                    for (Int32 j = 0; j < this._fileMgr.SpectrumFiles.Count; j++)
                    {
                        //If the Types don't match, just continue
                        if (this._fileMgr.SpectrumFiles[i].Type != this._fileMgr.SpectrumFiles[j].Type)
                            continue;

                        //Get the Series connected to this SpectrumFile
                        DCW.Series s = this.chart.Series[Path.GetFileName(this._fileMgr.SpectrumFiles[j].FilePath)];

                        //Finally, apply the multiplier
                        for (Int32 k = 0; k < s.Points.Count; k++)
                            s.Points[k].YValues[0] = this._fileMgr.SpectrumFiles[j].YValues[k] * power;
                    }
                }

                //Add the Type to the Processed flag
                processedFlag |= this._fileMgr.SpectrumFiles[i].Type;
            }
        }


        /// <summary>
        /// sb_DataReady accepts the new data and puts it in 
        /// the Chart as well as persists it to the file.
        /// </summary>
        /// <param name="sender">The SpectrumBase that fired the event</param>
        /// <param name="e">The EventArgs that hold the data</param>
        void sb_DataReady(object sender, Acquisition.DataReadyEventArgs e)
        {
            //Create a DataPoint and put it in the plot if this is supposed
            //to be shown
            if (this._acqSpecBase.Config.NoDisplay == false)
            {
                DCW.DataPoint dp = new DCW.DataPoint();
                dp.XValue = e.XValue;

                //Set the Y-Values based on whether there is a Z
                List<Double> l = new List<Double>();
                l.Add(e.YValue);

                //If there is a Z, this is 3-D.  Otherwise, apply
                //the multiplier to the point.
                if (Double.IsNaN(e.ZValue) == false)
                {
                    //Reverse the Row so that the spectrum begins
                    //in the upper left
                    l[0] = this._acqSpecBase.Rows3D - l[0];

                    //Now add the Z-Value
                    l.Add(e.ZValue);
                }
                else
                {
                    //Calculate the power of the Y-Value
                    Double power = Math.Pow(10.0, (Convert.ToDouble(this._acqMultiplier * -1.0)));
                    l[0] *= power;
                }

                //Set the Y-Values
                dp.YValues = l.ToArray();

                //If this is a 3-D plot, set the color
                if (Double.IsNaN(e.ZValue) == false)
                    dp.Color = this.CalculateColor(this._acqSpecBase.MaximumCurrent, this._acqSpecBase.MaximumCurrent * -1.0, e.ZValue);

                //Set the DataPoint in the Series
                this._acqSeries.Points.Add(dp);

                //Invoke the chart to show the new data
                if (this.chart.InvokeRequired == true)
                    this.chart.Invoke(new InvalidateChartDelegate(this.ThreadSafeInvalidate));
            }

            //Set the Elapsed and Remaining times
            String tsString = String.Format("Elapsed: {0}", TimeSpanFormatter.ToHMSString(this._acqSpecBase.TimeElapsed));

            if ((this._specType & SpectrumType.Time) == 0)
                tsString += String.Format("  Remaining: {0}", TimeSpanFormatter.ToHMSString(this._acqSpecBase.TimeRemaining));

            if (this.barStatus.InvokeRequired == true)
                this.barStatus.Invoke(new SetToolStripStatusLabelTextDelegate(this.ThreadSafeSetToolStripStatusLabelText), this.lblTime, tsString);

            //Save the values in the SpectrumFile based on the
            //ZValue in the EventArgs
            if (Double.IsNaN(e.ZValue) == true)
            {
                this._acqSpecFile.XValues.Add(e.XValue);
                this._acqSpecFile.YValues.Add(e.YValue);
            }
            else
            {
                //If the row doesn't exist yet, create it
                if (this._acqSpecFile.ThreeDValues.Count <= e.Row3D)
                    this._acqSpecFile.ThreeDValues.Add(new List<Double>());

                //Now set the value in the List
                this._acqSpecFile.ThreeDValues[e.Row3D].Add(e.ZValue);
            }

            //Save the file out to disk
            this._acqSpecFile.SavePartial();
        }


        /// <summary>
        /// sb_AcquisitionStopped alerts the Main Window that 
        /// data acquisition has stopped.
        /// </summary>
        /// <param name="sender">The SpectrumBase that has stopped acquiring data</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void sb_AcquistionStopped(object sender, EventArgs e)
        {
            //If the chart was not being shown, show it now
            if (this._acqSpecBase.Config.NoDisplay == true)
            {
                //Open the file using the Thread-safe methods
                if (this.InvokeRequired == true)
                    this.Invoke(new SetupWindowAfterAcquisition(this.ThreadSafeSetupWindow), this._acqSpecFile);
            }

            //Clean up the member variables
            this._acqSpecBase = null;
            this._acqSpecFile = null;

            //Set the flag
            this._runningSpec = false;

            //Fire the event
            if (this.AcquisitionStopped != null)
                this.AcquisitionStopped(this, new EventArgs());
        }

        
        /// <summary>
        /// ThreadSafeInvalidate is a way to allow the chart
        /// thread to repaint the chart in thread-safe manner.
        /// </summary>
        private void ThreadSafeInvalidate()
        {
            //Tell the chart to repaint
            this.chart.Invalidate();
        }


        /// <summary>
        /// ThreadSafeSetLabelText sets the text on a Label
        /// even if the Label is not being shown.
        /// </summary>
        /// <param name="lbl">The Label on which to set the Text</param>
        /// <param name="newText">The text to set on the Label</param>
        private void ThreadSafeSetToolStripStatusLabelText(ToolStripStatusLabel lbl, String newText)
        {
            lbl.Text = newText;
        }


        /// <summary>
        /// ThreadSafeSetupWindow does the setup of the Spectrum
        /// Form after the Acquisition has completed.
        /// </summary>
        /// <param name="sf">The SpectrumFile to show in the Spectrum Window</param>
        private void ThreadSafeSetupWindow(SpectrumFile sf)
        {
        	//Setup the chart and show it
            this.AddSpectrum(sf);
            this.chart.Visible = true;
        }


        /// <summary>
        /// Pause pauses the Data Acquistion.
        /// </summary>
        public void PauseDataAcquisition()
        {
            //If the Spectrum isn't even acquiring data, just return
            if (this._runningSpec == false)
                return;

            //Pause the data acquisition
            if (this._acqSpecBase != null)
                this._acqSpecBase.Pause();
        }


        /// <summary>
        /// Resume resumes the Data Acquisition.
        /// </summary>
        public void ResumeDataAcquisition()
        {
            //If the Spectrum isn't even acquiring data, just return
            if (this._runningSpec == false)
                return;

            //Resume the data acquisition
            if (this._acqSpecBase != null)
                this._acqSpecBase.Resume();
        }


        /// <summary>
        /// StopDataAcquisition stops the Data Acquisition.
        /// </summary>
        public void StopDataAcquisition()
        {
            //If the Spectrum isn't even acquiring data, just return
            if (this._runningSpec == false)
                return;

            //Stop the data acquisition
            if (this._acqSpecBase != null)
                this._acqSpecBase.Stop();
        }
        #endregion


        #region Plot Manipulation Methods and Events
        /// <summary>
        /// SetChartBaseProperties sets all of the UI elements in the
        /// plot so that it looks really good.
        /// </summary>
        private void Set2DChartBaseProperties()
        {
            //Set the basic Chart Properties
            this.chart.BackColor = Color.White;
            this.chart.Enabled = true;

            //Get the ChartArea and set some properties
            DCW.ChartArea ca = this.ChartArea;
            ca.BackColor = Color.White;

            //Set up the X-Axis
            ca.AxisX.LineWidth = 2;
            ca.CursorX.Interval = 0.1;
            ca.CursorX.LineColor = Color.Transparent;
            ca.CursorX.SelectionColor = Color.LightGray;
            ca.CursorX.UserEnabled = true;
            ca.CursorX.UserSelection = true;
            ca.AxisX.View.MinSize = 0.1;
            ca.AxisX.View.Zoomable = true;

            //Set up the Main Y-Axis
            ca.AxisY.LineWidth = 2;
            ca.CursorY.Interval = 0.0;
            ca.CursorY.LineColor = Color.Transparent;
            ca.CursorY.SelectionColor = Color.LightGray;
            ca.CursorY.UserEnabled = true;
            ca.CursorY.UserSelection = true;
            ca.AxisY.View.MinSize = 0.0;
            ca.AxisY.View.Zoomable = true;
        }


        /// <summary>
        /// Set2DSeriesProperties sets up the line types and 
        /// widths for each new Series.
        /// </summary>
        /// <param name="s">The Series to format</param>
        /// <param name="sf">The SpectrumFile that is being plotted</param>
        private void Set2DSeriesProperties(DCW.Series s, SpectrumFile sf)
        {
            //Set basic properties
            s.BorderWidth = 2;

            //Set the Type to Line
            s.Type = DCW.SeriesChartType.Line;

            //Get the basic type of the SpectrumFile.  This is used
            //to set the type of Axis below.
            SpectrumType type = SpectrumExtension.ToType(Path.GetExtension(sf.FilePath));

            //Check the Type so that a new Axis can be created
            //if necessary
            if (this._specType == SpectrumType.Invalid)
            {
                //Set the name and then don't do anything else.
                //That will make the current file go to the default
                //Y-Axis
                this.ChartArea.AxisY.Name = type.ToString();
            }
            else
            {
                //If the Main YAxis type is not the same as the
                //new spectrum, check the SubAxes
                if (this.ChartArea.AxisY.Name != type.ToString())
                {
                    //Iterate through the YSubAxes to see if a new
                    //one needs to be made
                    Boolean makeAxis = true;
                    foreach (DCW.SubAxis sa in this.ChartArea.AxisY.SubAxes)
                    {
                        if (sa.Name == type.ToString())
                        {
                            makeAxis = false;
                            break;
                        }
                    }

                    //Create a new SubYAxis with the new Type if necessary
                    if (makeAxis == true)
                    {
                        //Make the new AxisY and set some properties
                        DCW.SubAxis newY = new DCW.SubAxis(type.ToString());
                        newY.View.MinSize = 0.0;
                        newY.View.Zoomable = true;

                        //Add the new Axis
                        this.ChartArea.AxisY.SubAxes.Add(newY);
                    }
                }
            }

            //Now set the Y-Axis based on the type
            if (this.ChartArea.AxisY.Name != type.ToString())
                s.YSubAxisName = type.ToString();

            //Finally, add the data to the Series
            for (Int32 i = 0; i < sf.XValues.Count; i++)
                s.Points.AddXY(sf.XValues[i], sf.YValues[i]);
        }


        /// <summary>
        /// Set3DChartBaseProperties sets the properties that
        /// will be common to every 3D chart.
        /// </summary>
        private void Set3DChartBaseProperties()
        {
            //Set the Axes so that they only show a few numbers on them
            DCW.ChartArea ca = this.ChartArea;
            ca.AxisX.LabelStyle.Interval = 25;
            ca.AxisX.MajorTickMark.Interval = 25;
            ca.AxisY.LabelStyle.Interval = 25;
            ca.AxisY.MajorTickMark.Interval = 25;

            //Setup the Zooming 
            ca.CursorX.LineColor = Color.Transparent;
            ca.CursorX.SelectionColor = Color.LightGray;
            ca.CursorX.UserEnabled = true;
            ca.CursorX.UserSelection = true;
            ca.AxisX.View.Zoomable = true;
            ca.CursorY.LineColor = Color.Transparent;
            ca.CursorY.SelectionColor = Color.LightGray;
            ca.CursorY.UserEnabled = true;
            ca.CursorY.UserSelection = true;
            ca.AxisY.View.Zoomable = true;

            //Set the name on the Y-Axis so that it can be Titled
            ca.AxisY.Name = SpectrumType.THREED.ToString();
        }


        /// <summary>
        /// Set3DSeriesBaseProperties sets the properties that
        /// will be common to every 3D Series.
        /// </summary>
        /// <param name="s">The Series that was created</param>
        /// <param name="sf">The SpectrumFile from which the Series is created</param>
        /// <param name="sb">The SpectrumBase that represents this data collection</param>
        private void Set3DSeriesProperties(DCW.Series s, SpectrumFile sf, Acquisition.SpectrumBase sb)
        {
            //Setup the chart as a Point Map
            s.Type = DCW.SeriesChartType.Point;
            s.ChartArea = this.ChartArea.Name;

            //Since there will only ever be one Series, don't
            //show a Legend
            s.ShowInLegend = false;

            //Get the data from the SpectrumFile and perform a 
            //vertical flip on it.  This is to make the lower
            //left point the Origin when the origin of the file
            //is technically the upper left point.  That was a 
            //user requirement.
            List<List<Double>> threeD = sf.ThreeDValues;
            threeD.Reverse();

            //Iterate through all of the data and add it to 
            //the plot
            for (Int32 i = 0; i < threeD.Count; i++)
            {
                for (Int32 j = 0; j < threeD[i].Count; j++)
                {
                    //Add the data point to the Series.  Use Y = i, 
                    //X = j, Y1 as Z-axis = photocurrent value.
                    DCW.DataPoint point = new DCW.DataPoint();
                    point.XValue = j;
                    point.YValues = new Double[] { i, threeD[i][j] };
                    s.Points.Add(point);
                }
            }

            //Get the Min / Max values of the Points
            Double maxX = Double.MaxValue;
            Double maxY1 = Double.MaxValue;
            Double minY2 = Double.MinValue;
            Double maxY2 = Double.MaxValue;

            if (s.Points != null && s.Points.Count > 0)
            {
                maxX = s.Points.FindMaxValue(DCW.AxisName.X.ToString()).XValue;
                maxY1 = s.Points.FindMaxValue(DCW.AxisName.Y.ToString()).YValues[0];
                maxY2 = s.Points.FindMaxValue(DCW.AxisName.Y2.ToString()).YValues[1];
                minY2 = s.Points.FindMinValue(DCW.AxisName.Y2.ToString()).YValues[1];
            }
            else if (sb != null)
            {
                maxX = sb.Columns3D;
                maxY1 = sb.Rows3D;
                maxY2 = sb.MaximumCurrent;
                minY2 = sb.MaximumCurrent * -1.0;
            }

            //Set the Axis ranges
            this.ChartArea.AxisX.Minimum = 0;
            this.ChartArea.AxisX.Maximum = maxX;
            this.ChartArea.AxisY.Minimum = 0;
            this.ChartArea.AxisY.Maximum = maxY1;

            //Set the Y-Axis Name
            this.ChartArea.AxisY.Name = SpectrumType.THREED.ToString();

            //Figure out the smaller side and set the size of the 
            //Plot Area directly -- default to setting Width to
            //static and scaling height (Width > Height)
            Single width = Spectrum._maxChartSize;
            Single height = Convert.ToSingle(((maxY1 + 1) / (maxX + 1)) * Spectrum._maxChartSize);

            //If Height > Width, set Height to static and scale Width
            if (maxY1 > maxX)
            {
                //Set the Height to 100 percent and scale the Width
                height = Spectrum._maxChartSize;
                width = Convert.ToSingle(((maxX + 1) / (maxY1 + 1)) * Spectrum._maxChartSize);
            }

            //If the height or width is going to be too small to 
            //see even the Axes, resize them
            if (width < Spectrum._minChartSize)
                width = Spectrum._minChartSize;

            if (height < Spectrum._minChartSize)
                height = Spectrum._minChartSize;

            //Set the Chart size and position
            this.ChartArea.Position.Height = height;
            this.ChartArea.Position.Width = width;
            this.ChartArea.Position.Y = 5.0F;
            this.ChartArea.Position.X = 5.0F;

            //Create the color ranges
            foreach (DCW.DataPoint dp in s.Points)
                dp.Color = this.CalculateColor(maxY2, minY2, dp.YValues[1]);
        }


        /// <summary>
        /// CalculateColor takes the max value of the Series and
        /// figures out the color of the point based on the sign
        /// and relative magnitude.
        /// </summary>
        /// <param name="max">The Maximum that the value can take</param>
        /// <param name="min">The Minimum that the value can take</param>
        /// <param name="curr">The value of the current point</param>
        /// <returns>Color structure that defines the actual Color of the point</returns>
        private Color CalculateColor(Double max, Double min, Double curr)
        {
            //Declare a variable to return
            Color rtn = Color.Black;

            //If the current point is positive, then the color should range
            //from Black to Green to Blue.  Otherwise, the color
            //should range from Black to Yellow to Red.
            if (curr > 0)
            {
                //Calculate the number of gradients of Color.  This should
                //be between 0 and 510 -- two ranges of color.
                Double maxStep = max / 510;

                //Calculate the magnitude of the color
                Int32 posMagnitude = Convert.ToInt32(curr / maxStep);

                //If the magnitude is < 256, set the color just to Green.
                //Otherwise, set the color to a mix of Green and Blue.
                if (posMagnitude < 256)
                    rtn = Color.FromArgb(0, posMagnitude, 0);
                else
                    rtn = Color.FromArgb(0, 510 - posMagnitude, posMagnitude - 255);
            }
            else if (curr < 0)
            {
                //Calculate the number of gradients of Color.  This should
                //be between 0 and 510 -- two ranges of color.
                Double minStep = min / 510;

                //Calculate the magnitude of the color
                Int32 negMagnitude = Convert.ToInt32(curr / minStep);

                //If the magnitude is < 256, set the color just to Yellow.
                //Otherwise, set the color to a mix of Yellow and Red.
                if (negMagnitude < 256)
                    rtn = Color.FromArgb(negMagnitude, negMagnitude, 0);
                else
                    rtn = Color.FromArgb(255, 510 - negMagnitude, 0);
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// chart_SelectionRangeChanged notifies the Spectrum Window
        /// when the cursors have been used to select a region for
        /// Zooming.  This is used to notify the Main window to update
        /// the Menu and Toolbar.
        /// </summary>
        /// <param name="sender">The Chart that was Zoomed</param>
        /// <param name="e">The EventArgs sent by the Chart</param>
        void chart_SelectionRangeChanged(object sender, DCW.CursorEventArgs e)
        {
            //Set the value of the Boolean member variable
            this._zoomedIn = true;

            //Call the event
            if (this.ZoomChanged != null)
                this.ZoomChanged(this, new EventArgs());
        }


        /// <summary>
        /// AutoscaleAxes sets the axes on the plot to the best
        /// scale for the data.
        /// </summary>
        public void AutoscaleAxes()
        {
            //First, reset the Zoom
            this.ResetZoom();

            //Get the ChartArea
            DCW.ChartArea ca = this.ChartArea;

            //Reset the values of the Axis min / max so that
            //the chart autoscales
            ca.AxisX.Minimum = Double.NaN;
            ca.AxisX.Maximum = Double.NaN;

            //Set the Main Y-Axis title
            ca.AxisY.Minimum = Double.NaN;
            ca.AxisY.Maximum = Double.NaN;

            //Iterate through the SubYAxes and set the titles
            //on those
            foreach (DCW.SubAxis sa in ca.AxisY.SubAxes)
            {
                sa.Minimum = Double.NaN;
                sa.Maximum = Double.NaN;
            }
        }


        /// <summary>
        /// ResetZoom resets the chart completely to its zoomed-out state
        /// </summary>
        public void ResetZoom()
        {
            //Get the ChartArea
            DCW.ChartArea ca = this.ChartArea;

            //Reset the zoom all around
            ca.AxisX.View.ZoomReset(0);
            ca.AxisY.View.ZoomReset(0);

            //Reset the zoom member variables
            this._zoomedIn = false;

            //Call the ZoomChange event to update the Main window
            if (this.ZoomChanged != null)
                this.ZoomChanged(this, new EventArgs());
        }

        
        /// <summary>
        /// chart_MouseMove gets the coordinates of the cursor 
        /// and displays them on the StatusStrip.
        /// </summary>
        /// <param name="sender">The Chart over which the mouse moved</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void chart_MouseMove(object sender, MouseEventArgs e)
        {
            //If there is a button pressed, then the user is 
            //probably Zooming, so ignore this
            if (e.Button != MouseButtons.None)
                return;

            try
            {
                //Check for a HitTest on the ChartArea
                DCW.HitTestResult result = this.chart.HitTest(e.X, e.Y);

                //If the chart is being moused over, set the values
                if (result.ChartArea != null && result.ChartArea.Equals(this.ChartArea))
                {
                    //If this is a 2-D chart, just get X / Y coordinates
                    if ((this.SpectrumType & SpectrumType.TWOD) > 0)
                    {
                        //Get the values from the Chart
                        Double xVal = result.ChartArea.AxisX.PixelPositionToValue(e.X);
                        Double yVal = result.ChartArea.AxisY.PixelPositionToValue(e.Y);

                        //Set the Position String
                        this.lblPosition.Text = String.Format("X: {0:F2}  Y: {1:E2}", xVal, yVal);
                    }
                    else if ((this.SpectrumType & SpectrumType.THREED) > 0)
                    {
                        //Try to get the DataPoint
                        if (result.ChartElementType == DCW.ChartElementType.DataPoint)
                        {
                            //Get the DataPoint
                            DCW.DataPoint point = result.Series.Points[result.PointIndex];

                            //Set the Position String
                            this.lblPosition.Text = String.Format("X: {0:F0}  Y: {1:F0}  Z: {2:E2}",
                                point.XValue, point.YValues[0], point.YValues[1]);
                        }
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// Wrapper function to get the ChartArea quickly and painlessly
        /// </summary>
        private DCW.ChartArea ChartArea
        {
            get { return this.chart.ChartAreas[Spectrum._defChartAreaKey]; }
        }
        #endregion


        #region Printing Methods
        /// <summary>
        /// GetPrintDocument sets up the PrintDocument for the
        /// basic printing of the Spectrum Window
        /// </summary>
        /// <returns>PrintDocument that describes how the Plot should be printed</returns>
        public System.Drawing.Printing.PrintDocument PrintDocument
        {
            get { return this.chart.Printing.PrintDocument; }
        }


        /// <summary>
        /// PrintFullSize sets up the PrintDocument to make the 
        /// Plot fill the page, then prints it.
        /// </summary>
        public void PrintFullSize()
        {
            //Set the Document name
            this.chart.Printing.PrintDocument.DocumentName = this.Text;

            //Setup the PrintDocument for Landscape and fill page
            this.chart.Printing.PrintDocument.DefaultPageSettings.Landscape = true;

            //Print the document
            this.chart.Printing.PrintDocument.Print();
        }


        /// <summary>
        /// PrintToRegion prints the chart to the designated
        /// Rectangle.
        /// </summary>
        /// <param name="rect">The Region to which to print the chanrt</param>
        public void PrintToRegion(Graphics g, Rectangle rect)
        {
            //Print to the Rectangle
            this.chart.Printing.PrintPaint(g, rect);
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// FileManager exposes the actual data that is being used
        /// by the Spectrum form.
        /// </summary>
        public FileManager FileManager
        {
            get { return this._fileMgr; }
        }


        /// <summary>
        /// IsRunningSpectrum determines if the window has an
        /// active spectrum running.
        /// </summary>
        public Boolean IsRunningSpectrum
        {
            get { return this._runningSpec; }
        }


        /// <summary>
        /// IsZoomedIn determines if the Chart has been Zoomed.
        /// </summary>
        public Boolean IsZoomedIn
        {
            get { return this._zoomedIn; }
        }
        #endregion
    }
}
