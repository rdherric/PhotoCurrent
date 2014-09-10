using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// AcquisitionConfigBase is the base class for the
    /// Acquisition Config objects.  This is so that all
    /// of them stay in the same ConfigurationGroup.
    /// </summary>
    [ConfigurationGroup(AcquisitionConfigBase._groupName)]
    public abstract class AcquisitionConfigBase : ConfigurationSection
    {
        #region Member Variables
        private const String _groupName = "acquisition";

        //2-D Spectra Parameters
        private const String _startKey = "startWavelength";
        private const String _endKey = "endWavelength";
        private const String _incrementKey = "increment";
        private const String _delayKey = "acquisitionDelay";
        private const String _samplesKey = "samplesToAvg";

        //Common Spectrum Parameters
        private const String _noDisplayKey = "noDisplay";
        private String _path = String.Empty;
        private String _comment = String.Empty;
        #endregion


        #region Public Properties
        /// <summary>
        /// StartWavelength determines the value of the wavelength at
        /// the start of an Acquisition.
        /// </summary>
        [ConfigurationProperty(AcquisitionConfigBase._startKey, DefaultValue = 750U, IsRequired = false)]
        public UInt32 StartWavelength
        {
            get { return Convert.ToUInt32(this[AcquisitionConfigBase._startKey]); }
            set { this[AcquisitionConfigBase._startKey] = value; }
        }


        /// <summary>
        /// EndWavelength determines the value of the wavelength at
        /// the end of an Acquisition.
        /// </summary>
        [ConfigurationProperty(AcquisitionConfigBase._endKey, DefaultValue = 450U, IsRequired = false)]
        public UInt32 EndWavelength
        {
            get { return Convert.ToUInt32(this[AcquisitionConfigBase._endKey]); }
            set { this[AcquisitionConfigBase._endKey] = value; }
        }


        /// <summary>
        /// WavelengthIncrement determines the value of the nm step 
        /// during an Acquisition.
        /// </summary>
        [ConfigurationProperty(AcquisitionConfigBase._incrementKey, DefaultValue = 2U, IsRequired = false)]
        public UInt32 WavelengthIncrement
        {
            get { return Convert.ToUInt32(this[AcquisitionConfigBase._incrementKey]); }
            set { this[AcquisitionConfigBase._incrementKey] = value; }
        }


        /// <summary>
        /// AcquisitionDelay determines the amount of time to 
        /// wait prior to acquiring data.
        /// </summary>
        [ConfigurationProperty(AcquisitionConfigBase._delayKey, DefaultValue = 5.0, IsRequired = false)]
        public Double AcquisitionDelay
        {
            get { return Convert.ToDouble(this[AcquisitionConfigBase._delayKey]); }
            set { this[AcquisitionConfigBase._delayKey] = value; }
        }


        /// <summary>
        /// SamplesToAverage determines the number of samples to 
        /// acquire and average into one point.
        /// </summary>
        [ConfigurationProperty(AcquisitionConfigBase._samplesKey, DefaultValue = 10U, IsRequired = false)]
        public UInt32 SamplesToAverage
        {
            get { return Convert.ToUInt32(this[AcquisitionConfigBase._samplesKey]); }
            set { this[AcquisitionConfigBase._samplesKey] = value; }
        }


        /// <summary>
        /// NoDisplay determines if the Spectra should be displayed
        /// during acquisition.
        /// </summary>
        [ConfigurationProperty(AcquisitionConfigBase._noDisplayKey, DefaultValue = false, IsRequired = false)]
        public Boolean NoDisplay
        {
            get { return Convert.ToBoolean(this[AcquisitionConfigBase._noDisplayKey]); }
            set { this[AcquisitionConfigBase._noDisplayKey] = value; }
        }


        /// <summary>
        /// Comment returns the comment set by the user.  This is
        /// not persisted in the config file.
        /// </summary>
        public String Comment
        {
            get { return this._comment; }
            set { this._comment = value; }
        }


        /// <summary>
        /// FilePath returns the path set by the user.  This is
        /// not persisted in the config file.
        /// </summary>
        public String FilePath
        {
            get { return this._path; }
            set { this._path = value; }
        }
        #endregion
    }
}
