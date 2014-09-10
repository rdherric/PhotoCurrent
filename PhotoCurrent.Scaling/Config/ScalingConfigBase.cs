using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Scaling.Config
{
    /// <summary>
    /// ScalingConfigBase is the base class for all of the
    /// Scaling ConfigurationSections.  This is used 
    /// primarily so that the Group Name is set automatically.
    /// </summary>
    [ConfigurationGroup(ScalingConfigBase._groupName)]
    public class ScalingConfigBase : ConfigurationSection
    {
        #region Member Variables
        private const String _groupName = "scaling";
        #endregion
    }
}
