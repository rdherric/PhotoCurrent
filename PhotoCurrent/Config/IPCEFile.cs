using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// IPCEFile represents the section in the app.config
    /// file that holds the last IPCE Data file Path.
    /// </summary>
    [ConfigurationSection(IPCEFile._sectName)]
    internal class IPCEFile : PathConfigBase
    {
        #region Member Variables
        private const String _sectName = "ipceFile";
        #endregion


        #region Public Properties
        /// <summary>
        /// Path returns the last path used.
        /// </summary>
        [ConfigurationProperty(PathConfigBase._pathKey, DefaultValue = "", IsRequired = false)]
        public override String Path
        {
            get
            {
                //Get the configured property
                String rtn = this[PathConfigBase._pathKey].ToString();

                //If the path is blank, return the real default
                if (rtn == String.Empty)
                    rtn = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Return the result
                return rtn;
            }
            set { this[PathConfigBase._pathKey] = value; }
        }
        #endregion
    }
}
