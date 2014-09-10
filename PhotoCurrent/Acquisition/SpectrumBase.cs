using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using PhotoCurrent.IO.Enums;
using PS = PhotoCurrent.Scaling;
using RI = RDH2.Instrumentation;
using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Acquisition
{
    /// <summary>
    /// SpectrumBase is the base class for the Acquisition
    /// Namespace.  It is used to pass New Spectrum info
    /// to the Spectrum window.
    /// </summary>
    public abstract class SpectrumBase
    {
        #region Member Variables
        private SpectrumType _type = SpectrumType.Invalid;
        private Config.AcquisitionConfigBase _config = null;
        private RI.Config.DaqInterface _daqConfig = null;
        private PS.Photocurrent _scaler = null;

        //Scan Variables
        private Boolean _isRunning = false;
        private DateTime _startTime = DateTime.Now;
        private Object _suspendLock = new Object();
        private Boolean _isSuspended = false;
        private Object _stopLock = new Object();
        private Boolean _stop = false;
        private Thread _thread = null;
        private UInt32 _pointIndex = 0;

        //Public DataReady Event
        public event DataReadyEventHandler DataReady;
        public event EventHandler AcquistionStopped;
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for the SpectrumBase object.
        /// </summary>
        /// <param name="config">The Configuration for the Spectrum</param>
        public SpectrumBase(Config.AcquisitionConfigBase config)
        {
            //Save the member variable
            this._config = config;

            //Set the SpectrumType based on the Configuration
            if (this._config is Config.Background)
                this._type = SpectrumType.TWOD | SpectrumType.BKG;
            else if (this._config is Config.Time)
                this._type = SpectrumType.TWOD | SpectrumType.RAW | SpectrumType.Time;
            else if (this._config is Config.Wavelength)
                this._type = SpectrumType.TWOD | SpectrumType.RAW | SpectrumType.Wavelength;
            else if (this._config is Config.Position)
                this._type = SpectrumType.THREED | SpectrumType.RAW;

            //Get the Data Acquisition Config
            ConfigHelper<RI.Config.DaqInterface> daqConfig = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            this._daqConfig = daqConfig.GetConfig();

            //Create a Voltage Scaler
            this._scaler = new PS.Photocurrent();

            //Setup the Lock-In Amp
            ConfigHelper<RI.Config.LockInAmp> liaConfig = new ConfigHelper<RI.Config.LockInAmp>(ConfigLocation.AllUsers);
            RI.Config.LockInAmp lia = liaConfig.GetConfig();
            this._scaler.SetupLockInAmp(lia.Type, lia.Sensitivity, lia.Unit, lia.FullScale);

            //Setup the Potentiostat
            ConfigHelper<RI.Config.Potentiostat> psConfig = new ConfigHelper<RI.Config.Potentiostat>(ConfigLocation.AllUsers);
            RI.Config.Potentiostat ps = psConfig.GetConfig();
            this._scaler.SetupPotentiostat(ps.CurrentRange, ps.Unit, ps.FullScale);
        }
        #endregion


        #region Scan Start, Pause and Stop Methods
        /// <summary>
        /// Start causes the SpectrumBase object to begin
        /// taking data and alerting the Spectrum Window
        /// when a point is ready.
        /// </summary>
        public void Start()
        {
            //Set the flag not to stop acquiring data
            this._stop = false;

            //Set the start time
            this._startTime = DateTime.Now;

            //Begin the Thread going
            this._thread = new Thread(new ThreadStart(this.AcquireData));
            this._thread.Start();

            //Set the flag for the IsRunning property
            this._isRunning = true;
        }


        /// <summary>
        /// Pause causes the SpectrumBase object to pause the 
        /// thread running the scan.
        /// </summary>
        public void Pause()
        {
            //Set the flag to pause acquiring data
            if (this._isRunning == true)
                this.IsSuspended = true;
        }


        /// <summary>
        /// Resume causes the SpectrumBase object to resume the
        /// thread running the scan.
        /// </summary>
        public void Resume()
        {
            //Set the flag to resume acquiring data
            if (this._isRunning == true)
                this.IsSuspended = false;
        }


        /// <summary>
        /// Stop causes the SpectrumBase object to stop the
        /// thread running the scan.
        /// </summary>
        public void Stop()
        {
            //Set the flag to stop acquiring data
            this._stop = true;

            //Set the flag for the IsRunning Property
            this._isRunning = false;
        }


        /// <summary>
        /// AcquireData is implemented by derived classes to 
        /// do the actual work of acquiring the data and 
        /// notifying the Spectrum Window when there is a 
        /// Point available.
        /// </summary>
        protected abstract void AcquireData();


        /// <summary>
        /// OnDataReady allows the derived classes access
        /// to the DataReady event.
        /// </summary>
        /// <param name="e"></param>
        protected void OnDataReady(DataReadyEventArgs e)
        {
            //Fire the event
            if (this.DataReady != null)
                this.DataReady(this, e);

            //Increment the point index
            this._pointIndex++;
        }


        /// <summary>
        /// OnAcquisitionStopped allows the derived classes access
        /// to the AcquisitionStopped event.
        /// </summary>
        protected void OnAcquisitionStopped()
        {
            //Fire the event
            if (this.AcquistionStopped != null)
                this.AcquistionStopped(this, new EventArgs());
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// Type returns the BitFlag type of the Spectrum.
        /// </summary>
        public SpectrumType Type
        {
            get { return this._type; }
        }


        /// <summary>
        /// Config returns the AcquisitionConfigBase object
        /// used to construct the derived classes.
        /// </summary>
        public Config.AcquisitionConfigBase Config
        {
            get { return this._config; }
        }


        /// <summary>
        /// IsRunning determines if the SpectrumBase
        /// object is acquiring data.
        /// </summary>
        public Boolean IsRunning
        {
            get { return this._isRunning; }
        }


        /// <summary>
        /// TimeRemaining is used to return the amount of
        /// time left in either a BKG or Wavelength scan.
        /// </summary>
        public TimeSpan TimeRemaining
        {
            get
            {
                //If the SpectrumType is Time, return Zero.
                if ((this._type & SpectrumType.Time) > 0)
                    return TimeSpan.Zero;

                //Calculate the number of seconds per sample
                Double secPerSample = 1.0 / Convert.ToDouble(this._daqConfig.SamplingRate);

                //Combine the two above values with the number of Samples to get 
                //the total time per point
                Double secPerPoint = secPerSample * Convert.ToDouble(this._config.SamplesToAverage);

                //Now add in the Delay and the number of points
                Double totalSeconds = (secPerPoint + Convert.ToDouble(this._config.AcquisitionDelay)) * Convert.ToDouble(this.PointsRemaining);

                //Return the result as a TimeSpan
                return new TimeSpan(0, 0, Convert.ToInt32(totalSeconds));
            }
        }


        /// <summary>
        /// TimeElapsed is used to return the amount of time
        /// elapsed in a Time scan.
        /// </summary>
        public TimeSpan TimeElapsed
        {
            get { return DateTime.Now - this._startTime; }
        }


        /// <summary>
        /// TotalPoints returns the total number of points in the
        /// Spectrum.
        /// </summary>
        public UInt32 TotalPoints
        {
            get
            {
                //Declare a variable to return
                UInt32 rtn = 0;

                //Calculate the number of Points based on the
                //SpectrumType
                if ((this._type & SpectrumType.TWOD) > 0)
                {
                    //Get the number of points between the Start and End
                    UInt32 rawPoints = Convert.ToUInt32(Math.Abs(
                        Convert.ToInt32(this._config.EndWavelength) - Convert.ToInt32(this._config.StartWavelength)));

                    //Now divide that by the Interval and Convert
                    rtn = Convert.ToUInt32(rawPoints / Convert.ToInt32(this._config.WavelengthIncrement));
                }
                else
                {
                    //Multiply the Rows and Columns and convert
                    rtn = Convert.ToUInt32((this.Rows3D + 1) * (this.Columns3D + 1));
                }

                //Return the result
                return rtn;
            }
        }


        /// <summary>
        /// PointsRemaining returns the number of points in the
        /// spectrum that still need to be acquired.
        /// </summary>
        public UInt32 PointsRemaining
        {
            get
            {
                //Return the Total Points minus the Index
                return this.TotalPoints - this._pointIndex;
            }
        }


        /// <summary>
        /// MaximumCurrent does the calculation for the 
        /// maximum current that can be 
        /// </summary>
        public Double MaximumCurrent
        {
            get { return this._scaler.MaximumPhotocurrent; }
        }


        /// <summary>
        /// Rows3D returns the number of Rows in a 3-D scan.
        /// </summary>
        public UInt32 Rows3D
        {
            get
            {
                //Declare a variable to return
                UInt32 rtn = 0;

                //Cast the Config to a Position
                PhotoCurrent.Config.Position p = this.Config as PhotoCurrent.Config.Position;

                //If the cast was successful, get the rows -- use Start 
                //minus End because the requirement is for Y to increase
                //as the laser moves up
                if (p != null)
                    rtn = Convert.ToUInt32((p.YPosStart - p.YPosEnd) / p.StepSize);

                //Return the result
                return rtn;
            }
        }


        /// <summary>
        /// Columns3D returns the number of Rows in a 3-D scan.
        /// </summary>
        public UInt32 Columns3D
        {
            get
            {
                //Declare a variable to return
                UInt32 rtn = 0;

                //Cast the Config to a Position
                PhotoCurrent.Config.Position p = this.Config as PhotoCurrent.Config.Position;

                //If the cast was successful, get the columns
                if (p != null)
                    rtn = Convert.ToUInt32((p.XPosEnd - p.XPosStart) / p.StepSize);

                //Return the result
                return rtn;
            }
        }
        #endregion


        #region Protected Properties
        /// <summary>
        /// DaqConfig returns the DaqInterface object created
        /// in the constructor to the derived classes.
        /// </summary>
        protected RI.Config.DaqInterface DaqConfig
        {
            get { return this._daqConfig; }
        }


        /// <summary>
        /// Scaler returns the Photocurrent Scaling 
        /// object that has been set up with the data
        /// in the app.config file.
        /// </summary>
        protected PS.Photocurrent Scaler
        {
            get { return this._scaler; }
        }


        /// <summary>
        /// IsSuspended is a property to wrap a Boolean to 
        /// make it thread-safe.  Tells derived classes when
        /// to suspend acquiring data.
        /// </summary>
        protected Boolean IsSuspended
        {
            get
            {
                //Lock on the suspendLock
                lock (this._suspendLock)
                {
                    return this._isSuspended;
                }
            }

            set
            {
                //Lock on the suspendLock
                lock (this._suspendLock)
                {
                    this._isSuspended = value;
                }
            }
        }


        /// <summary>
        /// Stop is a property to wrap a Boolean to make it
        /// thread-safe.  Tells derived classes when to stop
        /// acquiring data.
        /// </summary>
        protected Boolean IsStopped
        {
            get
            {
                //Lock on the stopLock
                lock (this._stopLock)
                {
                    return this._stop;
                }
            }

            set
            {
                //Lock on the stopLock
                lock (this._stopLock)
                {
                    this._stop = value;
                }
            }
        }
        #endregion
    }


    #region EventArgs and Event Declaration
    /// <summary>
    /// DataReadyEventHandler is implemented by the derived
    /// classes to notify the subscribing Spectrum Window
    /// that there is data available.
    /// </summary>
    /// <param name="sender">The SpectrumBase-derived class that is firing the event</param>
    /// <param name="e">The DataReadyEventArgs that describes the data</param>
    public delegate void DataReadyEventHandler(Object sender, DataReadyEventArgs e);


    /// <summary>
    /// DataReadyEventArgs holds the information about
    /// the new data point to be added to the Spectrum.
    /// </summary>
    public class DataReadyEventArgs : EventArgs
    {
        #region Member Variables
        private Double _xVal = Double.NaN;
        private Double _yVal = Double.NaN;
        private Double _zVal = Double.NaN;
        private Int32 _index2D = 0;
        private Int32 _row3D = 0;
        private Int32 _column3D = 0;
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor for the DataReadyEventArgs class that
        /// is used for 2-D spectra.
        /// </summary>
        /// <param name="index">The Index into the Series</param>
        /// <param name="xVal">The X-Axis value</param>
        /// <param name="yVal">The Y-Axis value</param>
        public DataReadyEventArgs(Int32 index, Double xVal, Double yVal)
        {
            //Set the member variables
            this._index2D = index;
            this._xVal = xVal;
            this._yVal = yVal;
        }


        /// <summary>
        /// Constructor for the DataReadyEventArgs class that
        /// is used for 3-D spectra.
        /// </summary>
        /// <param name="row">The Row index into the plot</param>
        /// <param name="column">The Column index into the plot</param>
        /// <param name="xVal">The X-Axis value</param>
        /// <param name="yVal">The Y-Axis value</param>
        /// <param name="zVal">The Z-Axis value</param>
        public DataReadyEventArgs(Int32 row, Int32 column, Double xVal, Double yVal, Double zVal)
        {
            //Set the member variables
            this._row3D = row;
            this._column3D = column;
            this._xVal = xVal;
            this._yVal = yVal;
            this._zVal = zVal;
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// Index2D is the Index of the point into a 2-D Series
        /// </summary>
        public Int32 Index2D
        {
            get { return this._index2D; }
        }


        /// <summary>
        /// Row3D is the row index of the point into a 3-D Series
        /// </summary>
        public Int32 Row3D
        {
            get { return this._row3D; }
        }


        /// <summary>
        /// Column3D is the column index of the point into a 3-D Series
        /// </summary>
        public Int32 Column3D
        {
            get { return this._column3D; }
        }


        /// <summary>
        /// XValue returns the X-Axis Value that should
        /// be entered into the chart.
        /// </summary>
        public Double XValue
        {
            get { return this._xVal; }
        }


        /// <summary>
        /// YValue returns the Y-Axis Value that should
        /// be entered into the chart.
        /// </summary>
        public Double YValue
        {
            get { return this._yVal; }
        }


        /// <summary>
        /// ZValue returns the Z-Axis Value that should
        /// be entered into the chart.
        /// </summary>
        public Double ZValue
        {
            get { return this._zVal; }
        }
        #endregion
    }
    #endregion
}
