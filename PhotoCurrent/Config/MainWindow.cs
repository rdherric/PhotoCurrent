using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// MainWindow is a custom ConfigurationSection class that
    /// gets and sets the values for the Main form.
    /// </summary>
    [ConfigurationSection(MainWindow._sectName)]
    internal class MainWindow : WindowConfigBase
    {
        #region Member Variables
        private const String _sectName = "mainForm";
        #endregion


        #region Public Properties
        /// <summary>
        /// Width defines the height of the Window when the Config
        /// was saved.
        /// </summary>
        [ConfigurationProperty(WindowConfigBase.HeightKey, DefaultValue = 600, IsRequired = false)]
        public Int32 Height
        {
            get { return Convert.ToInt32(this[WindowConfigBase.HeightKey]); }
            set { this[WindowConfigBase.HeightKey] = value; }
        }


        /// <summary>
        /// Width defines the width of the Window when the Config
        /// was saved.
        /// </summary>
        [ConfigurationProperty(WindowConfigBase.WidthKey, DefaultValue = 800, IsRequired = false)]
        public Int32 Width
        {
            get { return Convert.ToInt32(this[WindowConfigBase.WidthKey]); }
            set { this[WindowConfigBase.WidthKey] = value; }
        }
        #endregion
    }
}
