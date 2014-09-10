using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhotoCurrent.IO.Enums;
using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Dialogs
{
    /// <summary>
    /// SaveFileDialog subclasses the RDH2 SaveFileDialog
    /// so that the directory can be changed to configured
    /// directories when the File Type changes.
    /// </summary>
    public class SaveFileDialog : RDH2.Utilities.Dialogs.SaveFileDialog
    {
        #region Constructor
        /// <summary>
        /// Default Constructor for the OpenFileDialog class.
        /// </summary>
        public SaveFileDialog()
        {
            //Subscribe to the events
            this.FilterTypeAccepted += new RDH2.Utilities.Dialogs.FilterTypeSetEventHandler(FileDialog_TypeAccepted);
            this.FilterTypeChanged += new RDH2.Utilities.Dialogs.FilterTypeSetEventHandler(FileDialog_TypeChanged);
        }
        #endregion


        #region Event Handlers
        /// <summary>
        /// FileDialog_TypeAccepted get the type of File
        /// that was selected and saves the Directory in
        /// the app.config file.
        /// </summary>
        /// <param name="sender">The OpenFileDialog that had OK clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void FileDialog_TypeAccepted(object sender, RDH2.Utilities.Dialogs.FilterTypeSetEventArgs e)
        {
            //Get the Path of the file
            String path = Path.GetDirectoryName(this.FileName);

            //Get the SpectrumType
            SpectrumType specType = SpectrumExtension.ToType(Path.GetExtension(this.FileName));

            //Get the appropriate directory for the Type
            //Get the appropriate directory for the Type
            if ((specType & SpectrumType.BKG) > 0)
            {
                ConfigHelper<Config.BackgroundFile> bkgConfig = new ConfigHelper<Config.BackgroundFile>(ConfigLocation.AllUsers);
                Config.BackgroundFile bkg = bkgConfig.GetWriteableConfig();
                bkg.Path = path;
                bkgConfig.SetConfig(bkg);
            }
            else if ((specType & SpectrumType.IPCE) > 0)
            {
                ConfigHelper<Config.IPCEFile> ipceConfig = new ConfigHelper<Config.IPCEFile>(ConfigLocation.AllUsers);
                Config.IPCEFile ipce = ipceConfig.GetWriteableConfig();
                ipce.Path = path;
                ipceConfig.SetConfig(ipce);
            }
            else if ((specType & SpectrumType.RAW) > 0)
            {
                ConfigHelper<Config.RAWFile> rawConfig = new ConfigHelper<Config.RAWFile>(ConfigLocation.AllUsers);
                Config.RAWFile raw = rawConfig.GetWriteableConfig();
                raw.Path = path;
                rawConfig.SetConfig(raw);
            }
            else if ((specType & SpectrumType.TAB3D) > 0)
            {
                ConfigHelper<Config.TXTFile> txtConfig = new ConfigHelper<Config.TXTFile>(ConfigLocation.AllUsers);
                Config.TXTFile txt = txtConfig.GetWriteableConfig();
                txt.Path = path;
                txtConfig.SetConfig(txt);
            }
        }

        
        /// <summary>
        /// FileDialog_TypeChanged gets the type of File 
        /// that has been selected and changes the Directory 
        /// accordingly.
        /// </summary>
        /// <param name="sender">The FileDialog whose Type has changed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void FileDialog_TypeChanged(object sender, RDH2.Utilities.Dialogs.FilterTypeSetEventArgs e)
        {
            //Get the SpectrumType
            SpectrumType specType = SpectrumExtension.ToType(e.Extension);

            //Get the appropriate directory for the Type
            Config.PathConfigBase pcb = null;
            if ((specType & SpectrumType.BKG) > 0)
            {
                ConfigHelper<Config.BackgroundFile> bkgConfig = new ConfigHelper<Config.BackgroundFile>(ConfigLocation.AllUsers);
                pcb = bkgConfig.GetConfig();
            }
            else if ((specType & SpectrumType.IPCE) > 0)
            {
                ConfigHelper<Config.IPCEFile> ipceConfig = new ConfigHelper<Config.IPCEFile>(ConfigLocation.AllUsers);
                pcb = ipceConfig.GetConfig();
            }
            else if ((specType & SpectrumType.RAW) > 0)
            {
                ConfigHelper<Config.RAWFile> rawConfig = new ConfigHelper<Config.RAWFile>(ConfigLocation.AllUsers);
                pcb = rawConfig.GetConfig();
            }
            else if ((specType & SpectrumType.TAB3D) > 0)
            {
                ConfigHelper<Config.TXTFile> txtConfig = new ConfigHelper<Config.TXTFile>(ConfigLocation.AllUsers);
                pcb = txtConfig.GetConfig();
            }

            //Set the new Directory
            this.NewDirectory = pcb.Path;
        }
        #endregion
    }
}
