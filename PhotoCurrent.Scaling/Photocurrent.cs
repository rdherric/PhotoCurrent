using System;
using System.Collections.Generic;
using System.Text;

using RDH2.Instrumentation.Enums;

namespace PhotoCurrent.Scaling
{
    /// <summary>
    /// Photocurrent encapsulates the functions to 
    /// scale a voltage to a photocurrent.
    /// </summary>
    public class Photocurrent
    {
        #region Member Variables
        private Boolean _liaSetup = false;
        private LockInType _liaType = LockInType.Hardware;
        private UInt32 _sensitivity = UInt32.MinValue;
        private SensitivityUnit _sensUnit = SensitivityUnit.Invalid;
        private Double _liaFullScale = 10.0;
        private Boolean _psSetup = false;
        private Double _currentRange = Double.NaN;
        private CurrentUnit _currUnit = CurrentUnit.Invalid;
        private Double _psFullScale = 10.0;
        #endregion


        #region Setup Methods
        /// <summary>
        /// SetupLockInAmp sets up the variables that affect the
        /// Lock-In Amplifier scaling.
        /// </summary>
        /// <param name="sensitivity">The sensitivity of the Lock-In Amp</param>
        /// <param name="unit">The Units on the sensitivity</param>
        /// <param name="fullScale">The full scale output voltage of the Lock-In Amp</param>
        public void SetupLockInAmp(LockInType type, UInt32 sensitivity, SensitivityUnit unit, Double fullScale)
        {
            //Save the member variables
            this._liaType = type;
            this._sensitivity = sensitivity;
            this._sensUnit = unit;
            this._liaFullScale = fullScale;

            //Set the flag showing that LIA is set up
            this._liaSetup = true;
        }


        /// <summary>
        /// SetupPotentiostat sets up the variables that affect the
        /// Potentiostat scaling.
        /// </summary>
        /// <param name="currentRange">The range of the Potentiostat</param>
        /// <param name="unit">The Units on the range</param>
        /// <param name="fullScale">The full scale output voltage of the Potentiostat</param>
        public void SetupPotentiostat(Double currentRange, CurrentUnit unit, Double fullScale)
        {
            //Save the member variables
            this._currentRange = currentRange;
            this._currUnit = unit;
            this._psFullScale = fullScale;

            //Set the flag showing that the PS is set up
            this._psSetup = true;
        }
        #endregion


        #region Scaling Methods
        /// <summary>
        /// ScaleVoltage does the actual work of scaling the 
        /// voltage to a photocurrent.
        /// </summary>
        /// <param name="voltage">The Voltage to scale</param>
        /// <returns>The photocurrent represented by the voltage</returns>
        public Double ScaleVoltage(Double voltage)
        {
            //If the object hasn't been completely set up, throw 
            //an Exception
            if (this._liaSetup == false || this._psSetup == false)
                throw new InvalidOperationException("Setup the Lock-In Amp and Potentiostat parameters before scaling voltages.");

            //Declare a variable to return
            Double rtn = 0.0;

            //Calculate the voltage detected by the LIA
            Double liaDetected = voltage;
            if (this._liaType == LockInType.Hardware)
                liaDetected = (voltage / this._liaFullScale) * (this._sensitivity * SensitivityExponent.UnitToDivisor(this._sensUnit));
            
            //Calculate the current from the detected voltage
            rtn = (liaDetected / this._psFullScale) * (this._currentRange * CurrentExponent.UnitToDivisor(this._currUnit));

            //Return the result
            return rtn;
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// MaximumPhotocurrent returns the maximum value of the
        /// photocurrent based on the setup of the Lock-In Amp and 
        /// Potentiostat.
        /// </summary>
        public Double MaximumPhotocurrent
        {
            get { return this.ScaleVoltage(this._liaFullScale); }
        }
        #endregion
    }
}
