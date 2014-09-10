using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoCurrent.Scaling.Enums
{
    /// <summary>
    /// PowerUnit is an enum that defines the units
    /// of the Light Source used to excite the sample
    /// as well as the numerical divisor.
    /// </summary>
    public enum PowerUnit
    {
        Invalid = -1,
        Nanowatts = 0,
        Microwatts = 1,
        Milliwatts = 2,
        Watts = 3
    }


    /// <summary>
    /// PowerDivisor is a class to translate the PowerUnit
    /// to a Double Exponent for scaling.
    /// </summary>
    public class PowerExponent
    {
        #region Const Definitions
        private const Double _nanoWatts = 1E-9;
        private const Double _microWatts = 1E-6;
        private const Double _milliWatts = 1E-3;
        private const Double _watts = 1.0;
        #endregion


        /// <summary>
        /// UnitToDivisor returns the actual value of the 
        /// Enum PowerUnit.  
        /// </summary>
        /// <param name="unit">The Unit to translate</param>
        /// <returns></returns>
        public static Double UnitToDivisor(PowerUnit unit)
        {
            //Declare a variable to return
            Double rtn = -1;

            //Translate the PowerUnit
            switch (unit)
            {
                case PowerUnit.Nanowatts:
                    rtn = PowerExponent._nanoWatts;
                    break;

                case PowerUnit.Microwatts:
                    rtn = PowerExponent._microWatts;
                    break;

                case PowerUnit.Milliwatts:
                    rtn = PowerExponent._milliWatts;
                    break;

                case PowerUnit.Watts:
                    rtn = PowerExponent._watts;
                    break;
            }

            //Return the result
            return rtn;
        }
    }
}
