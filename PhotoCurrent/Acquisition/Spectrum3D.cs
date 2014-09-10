using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using RID = RDH2.Instrumentation.DAQ;

namespace PhotoCurrent.Acquisition
{
    /// <summary>
    /// Spectrum3D represents the data acquisition object used
    /// to create a 3-D scan.  
    /// </summary>
    public class Spectrum3D : SpectrumBase
    {
        #region Constructor
        /// <summary>
        /// Default constructor for the Spectrum3D class.
        /// </summary>
        /// <param name="config">The ConfigurationSection that defines this scan</param>
        public Spectrum3D(Config.AcquisitionConfigBase config) : base(config) { }
        #endregion


        #region AcquireData Method
        /// <summary>
        /// AcquireData does the actual work of scanning a sample
        /// in 3-D and saving the data to a file.
        /// </summary>
        protected override void AcquireData()
        {
            //Get the Configured card from the DaqFactory
            RID.DaqBase db = RID.DaqFactory.ConfiguredCard;

            //Cast the Config to a Position class
            PhotoCurrent.Config.Position p = this.Config as PhotoCurrent.Config.Position;

            //Determine the parameters for the scan
            UInt32 xStart = p.XPosStart;
            UInt32 xEnd = p.XPosEnd;
            UInt32 yStart = p.YPosStart;
            UInt32 yEnd = p.YPosEnd;
            UInt32 step = p.StepSize;
            Int32 delay = Convert.ToInt32(p.AcquisitionDelay * 1000);

            //Iterate through all of the points and fire
            //the event to notify the Spectrum Window
            UInt32 xPos = xStart;
            UInt32 yPos = yStart;

            while (this.IsStopped == false)
            {
                //Move the mirror in the Y-Axis direction
                db.MoveMirror(RID.DaqBase.Axis.Y, yPos);

                //Move the mirror in the X-Axis direction
                db.MoveMirror(RID.DaqBase.Axis.X, xPos);

                //Wait for the specified delay
                Thread.Sleep(delay);

                //Acquire and scale the data
                Double photocurrent = this.Scaler.ScaleVoltage(db.ReadVoltage(Convert.ToInt32(this.Config.SamplesToAverage)));

                //Calculate all of the values
                Double xVal = Convert.ToDouble((xPos - xStart) / step);
                Double yVal = Convert.ToDouble((yStart - yPos) / step);
                Int32 xIndex = Convert.ToInt32(xVal);
                Int32 yIndex = Convert.ToInt32(yVal);

                //Fire the event
                this.OnDataReady(new DataReadyEventArgs(yIndex, xIndex, xVal, yVal, photocurrent));

                //Update the positions as necessary
                xPos += step;
                if (xPos > xEnd)
                {
                    xPos = xStart;
                    yPos -= step;
                }

                //If the last line was run, stop the acquisition
                if (yPos < yEnd)
                    this.Stop();

                //If the user has Paused the acquisition, 
                //loop and Sleep until resumed
                while (this.IsSuspended == true && this.IsStopped == false)
                    Thread.Sleep(500);
            }

            //Call the Stopped event
            this.OnAcquisitionStopped();
        }
        #endregion
    }
}
