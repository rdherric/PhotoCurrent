using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// Position holds the configuration for the 3-D
    /// spectrum.
    /// </summary>
    [ConfigurationSection(Position._sectName)]
    public class Position : AcquisitionConfigBase
    {
        #region Member Variables
        private const String _sectName = "position";
        private const String _xPosStartKey = "xPosStart";
        private const String _xPosEndKey = "xPosEnd";
        private const String _yPosStartKey = "yPosStart";
        private const String _yPosEndKey = "yPosEnd";
        private const String _stepSizeKey = "stepSize";
        #endregion


        #region Public Properties
        /// <summary>
        /// XPosStart defines the starting point of the scan
        /// for the X-Axis.
        /// </summary>
        [ConfigurationProperty(Position._xPosStartKey, DefaultValue = 2000U, IsRequired = false)]
        public UInt32 XPosStart
        {
            get { return Convert.ToUInt32(this[Position._xPosStartKey]); }
            set { this[Position._xPosStartKey] = value; }
        }


        /// <summary>
        /// XPosEnd defines the ending point of the scan
        /// for the X-Axis.
        /// </summary>
        [ConfigurationProperty(Position._xPosEndKey, DefaultValue = 8000U, IsRequired = false)]
        public UInt32 XPosEnd
        {
            get { return Convert.ToUInt32(this[Position._xPosEndKey]); }
            set { this[Position._xPosEndKey] = value; }
        }


        /// <summary>
        /// YPosStart defines the starting point of the scan
        /// for the Y-Axis.
        /// </summary>
        [ConfigurationProperty(Position._yPosStartKey, DefaultValue = 8000U, IsRequired = false)]
        public UInt32 YPosStart
        {
            get { return Convert.ToUInt32(this[Position._yPosStartKey]); }
            set { this[Position._yPosStartKey] = value; }
        }


        /// <summary>
        /// YPosEnd defines the ending point of the scan
        /// for the Y-Axis.
        /// </summary>
        [ConfigurationProperty(Position._yPosEndKey, DefaultValue = 2000U, IsRequired = false)]
        public UInt32 YPosEnd
        {
            get { return Convert.ToUInt32(this[Position._yPosEndKey]); }
            set { this[Position._yPosEndKey] = value; }
        }


        /// <summary>
        /// StepSize defines the size in mV of each step in a 
        /// 3-D Spectrum.
        /// </summary>
        [ConfigurationProperty(Position._stepSizeKey, DefaultValue = 100U, IsRequired = false)]
        public UInt32 StepSize
        {
            get { return Convert.ToUInt32(this[Position._stepSizeKey]); }
            set { this[Position._stepSizeKey] = value; }
        }
        #endregion
    }
}
