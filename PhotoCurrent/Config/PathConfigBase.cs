using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// PathConfigBase is a ConfigurationSection-derived class for 
    /// storing and retrieving file open and save paths in the 
    /// app.config file.
    /// </summary>
    [ConfigurationGroup(PathConfigBase._groupNameKey)]
    internal abstract class PathConfigBase : ConfigurationSection
    {
        #region Member variables
        private const String _groupNameKey = "paths";
        public const String _pathKey = "path";
        #endregion


        #region Public Properties
        public abstract String Path { get; set; }
        #endregion
    }
}
