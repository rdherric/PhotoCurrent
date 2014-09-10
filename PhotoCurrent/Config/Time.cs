using System;
using System.Collections.Generic;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// Time holds the configuration for a Time-based
    /// Acquisition.
    /// </summary>
    [ConfigurationSection(Time._sectName)]
    public class Time : AcquisitionConfigBase
    {
        #region Member Variables
        private const String _sectName = "time";
        #endregion
    }
}
