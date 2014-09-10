using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using PhotoCurrent.IO.Enums;
using PS = PhotoCurrent.Scaling;
using RID = RDH2.Instrumentation.DAQ;
using RIC = RDH2.Instrumentation.Config;
using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Acquisition
{
    /// <summary>
    /// Spectra2D acquires data for a 2-dimensional Spectrum
    /// and passes it to the Spectrum window.
    /// </summary>
    public class Spectrum2D : SpectrumBase
    {
        #region Constructor
        /// <summary>
        /// Default constructor for the Spectrum2D class.
        /// </summary>
        /// <param name="config">The ConfigurationSection that defines this scan</param>
        public Spectrum2D(Config.AcquisitionConfigBase config) : base(config) { }
        #endregion


        #region AcquireData Method
        /// <summary>
        /// AcquireData does the actual work of iterating 
        /// through the data acquisition and notifying the
        /// subscribers when there is data available.
        /// </summary>
        protected override void AcquireData()
        {
            //Get the Configured card from the DaqFactory
            RID.DaqBase db = RID.DaqFactory.ConfiguredCard;

            //If the card is null, something has gone very wrong
            //so popup a message
            if (db == null)
                throw new System.InvalidOperationException("Configured Card could not be created.");

            //Determine the parameters of the scan -- different
            //for Time and Wavelength
            UInt32 startWL = this.Config.StartWavelength;
            UInt32 endWL = this.Config.EndWavelength;
            Int32 step = Convert.ToInt32(this.Config.WavelengthIncrement);
            Int32 delay = Convert.ToInt32(this.Config.AcquisitionDelay * 1000);

            if ((this.Type & SpectrumType.Time) > 0)
            {
                endWL = this.Config.EndWavelength;
                step = 0;
            }

            if (startWL > endWL)
                step *= -1;

            //Setup the monochromator
            db.SetupMonochromator(step);

            //Iterate through all of the points and fire
            //the event to notify the Spectrum Window
            Int32 currWL = Convert.ToInt32(startWL);
            Int32 count = 0;
            while (this.IsStopped == false)
            {
                //Wait for the specified delay
                Thread.Sleep(delay);

                //Acquire and scale the data
                Double photocurrent = this.Scaler.ScaleVoltage(db.ReadVoltage(Convert.ToInt32(this.Config.SamplesToAverage)));

                //Package the data up in an EventArgs based on the
                //type of data that is being acquired
                Double xVal = Convert.ToDouble(currWL);
                if ((this.Type & SpectrumType.Time) > 0)
                    xVal = this.TimeElapsed.TotalSeconds;

                //Fire the event
                this.OnDataReady(new DataReadyEventArgs(count, xVal, photocurrent));

                //Adjust the wavelength accordingly
                currWL += step;

                //If the scan is a Wavelength-based one, check the
                //values and Stop if necessary.  Otherwise, move
                //the monochromator to the next value.
                if ((this.Type & SpectrumType.Time) == 0)
                {
                    if ((currWL < endWL) && (startWL > endWL) ||
                        (currWL > endWL) && (startWL < endWL))
                        this.Stop();
                    else
                        db.MoveMonochromator(step);
                }

                //If the user has Paused the acquisition, 
                //loop and Sleep until resumed
                while (this.IsSuspended == true && this.IsStopped == false)
                    Thread.Sleep(500);
            }

            //Shutdown the monochromator
            db.ShutdownMonochromator();

            //Fire the AcquisitionStopped event
            this.OnAcquisitionStopped();
        }
        #endregion
    }
}
