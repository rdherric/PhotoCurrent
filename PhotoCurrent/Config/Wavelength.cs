using System;
using System.Collections.Generic;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Config
{
    /// <summary>
    /// Wavelength holds the configuration for a Wavelength
    /// Acquisition.
    /// </summary>
    [ConfigurationSection(Wavelength._sectName)]
    public class Wavelength : AcquisitionConfigBase
    {
        #region Member Variables
        private const String _sectName = "wavelength";
        #endregion
    }
}
