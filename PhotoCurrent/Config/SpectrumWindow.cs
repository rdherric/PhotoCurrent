using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// SpectrumWindowConfig is a ConfigurationSection class
    /// used to persist properties of the Spectrum window.
    /// </summary>
    [ConfigurationSection(SpectrumWindow._sectName)]
    internal class SpectrumWindow : WindowConfigBase
    {
        #region Member Variables
        private const String _sectName = "spectrumForm";
        #endregion


        #region Public Properties
        /// <summary>
        /// Width defines the height of the Window when the Config
        /// was saved.
        /// </summary>
        [ConfigurationProperty(WindowConfigBase.HeightKey, DefaultValue = 480, IsRequired = false)]
        public Int32 Height
        {
            get { return Convert.ToInt32(this[WindowConfigBase.HeightKey]); }
            set { this[WindowConfigBase.HeightKey] = value; }
        }


        /// <summary>
        /// Width defines the width of the Window when the Config
        /// was saved.
        /// </summary>
        [ConfigurationProperty(WindowConfigBase.WidthKey, DefaultValue = 640, IsRequired = false)]
        public Int32 Width
        {
            get { return Convert.ToInt32(this[WindowConfigBase.WidthKey]); }
            set { this[WindowConfigBase.WidthKey] = value; }
        }
        #endregion
    }
}
