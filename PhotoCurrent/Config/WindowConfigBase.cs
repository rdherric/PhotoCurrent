using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// WindowConfigBase is a ConfigurationSection-derived
    /// class that retrieves Window Properties from the 
    /// app.config file.
    /// </summary>
    [ConfigurationGroup(WindowConfigBase._groupName)]
    internal abstract class WindowConfigBase : ConfigurationSection
    {
        #region Const and Member variables
        private const String _groupName = "winProps";
        public const String WidthKey = "width";
        public const String HeightKey = "height";
        private const String _windowStateKey = "windowState";
        #endregion


        #region Public Properties
        /// <summary>
        /// State defines the state of the Window when the Config
        /// was saved -- Normal or Maximized.
        /// </summary>
        [ConfigurationProperty(WindowConfigBase._windowStateKey, DefaultValue = FormWindowState.Normal, IsRequired = false)]
        public FormWindowState State
        {
            get
            {
                //Get the value from the config
                FormWindowState fws = (FormWindowState)Enum.Parse(typeof(FormWindowState), this[WindowConfigBase._windowStateKey].ToString());

                //If the value is Minimized, turn it to Normal
                if (fws == FormWindowState.Minimized)
                    fws = FormWindowState.Normal;

                //Return the result
                return fws;
            }

            set
            {
                //If the value is Minimized, turn it to Normal
                if (value == FormWindowState.Minimized)
                    value = FormWindowState.Normal;

                //Save the value
                this[WindowConfigBase._windowStateKey] = value;
            }
        }
        #endregion
    }
}
