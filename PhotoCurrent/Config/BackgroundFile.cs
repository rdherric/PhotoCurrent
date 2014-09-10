using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    [ConfigurationSection(BackgroundFile._sectName)]
    internal class BackgroundFile : PathConfigBase
    {
        #region Member Variables
        private const String _sectName = "bkgFile";
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
                    rtn = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Background");

                //Return the result
                return rtn;
            }
            set { this[PathConfigBase._pathKey] = value; }
        }
        #endregion
    }
}
