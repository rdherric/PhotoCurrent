using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Scaling.Config
{
    /// <summary>
    /// The Background class contains the configuration information
    /// required for the Background scaling to IPCE.
    /// </summary>
    [ConfigurationSection(Background._sectName)]
    public class Background : ScalingConfigBase
    {
        #region Const Definitions
        private const String _sectName = "background";
        private const String _lastBKGPathKey = "lastBKGPath";
        private const String _lastScaleWavelengthKey = "lastScaleWavelength";
        private const String _lastScalePowerKey = "lastScalePower";
        private const String _lastScalePowerUnitKey = "lastScalePowerUnit";
        #endregion


        #region Public Configuration Properties
        /// <summary>
        /// LastBKGPath defines the last Background File that 
        /// was used to scale to IPCE.
        /// </summary>
        [ConfigurationProperty(Background._lastBKGPathKey, DefaultValue = "", IsRequired = false)]
        public String LastBKGPath 
        {
            get { return this[Background._lastBKGPathKey].ToString(); }
            set { this[Background._lastBKGPathKey] = value; }
        }


        /// <summary>
        /// LastScaleWavelength defines the last scaling wavelength that 
        /// was used to scale to IPCE.
        /// </summary>
        [ConfigurationProperty(Background._lastScaleWavelengthKey, DefaultValue = 600U, IsRequired = false)]
        public UInt32 LastScaleWavelength
        {
            get { return Convert.ToUInt32(this[Background._lastScaleWavelengthKey]); }
            set { this[Background._lastScaleWavelengthKey] = value; }
        }


        /// <summary>
        /// LastScalePower defines the last scaling power that 
        /// was used to scale to IPCE.
        /// </summary>
        [ConfigurationProperty(Background._lastScalePowerKey, DefaultValue = 15.0, IsRequired = false)]
        public Double LastScalePower
        {
            get { return Convert.ToDouble(this[Background._lastScalePowerKey]); }
            set { this[Background._lastScalePowerKey] = value; }
        }


        /// <summary>
        /// PowerUnit defines the last scaling PowerUnit enum
        /// that was used to scale to IPCE.
        /// </summary>
        [ConfigurationProperty(Background._lastScalePowerUnitKey, DefaultValue = Enums.PowerUnit.Invalid, IsRequired = false)]
        public Enums.PowerUnit LastScalePowerUnit
        {
            get { return (Enums.PowerUnit)Enum.Parse(typeof(Enums.PowerUnit), this[Background._lastScalePowerUnitKey].ToString()); }
            set { this[Background._lastScalePowerUnitKey] = value; }
        }
        #endregion
    }
}
