using System;
using System.Collections.Generic;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// Background holds the configuration for the Background
    /// Acquisition.  
    /// </summary>
    [ConfigurationSection(Background._sectName)]
    public class Background : AcquisitionConfigBase
    {
        #region Member Variables
        private const String _sectName = "background";
        #endregion
    }
}
