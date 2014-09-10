using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using PI = PhotoCurrent.IO;
using PCS = PhotoCurrent.Scaling;
using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Wizards
{
    /// <summary>
    /// The Scaling Wizard walks a user through the process
    /// of scaling RAW files to IPCE.
    /// </summary>
    public partial class Scaling : Form
    {
        #region Member Variables
        private Forms.Spectrum _spectrum = null;
        private String _fileOpenDir = String.Empty;
        private String _bkgDir = String.Empty;
        private String _saveDir = String.Empty;
        private Boolean _fileNamesChanged = false;
        private List<String> _ipceFilePaths = new List<String>();
        #endregion


        #region Constructor
        /// <summary>
        /// The constructor for the Scaling Wizard
        /// </summary>
        public Scaling()
        {
            InitializeComponent();

            //Hook up to Wizard events
            this.scalingWizard.Enter += new EventHandler(scalingWizard_Enter);
            this.pageListBoxSelectFiles.BeforeDisplay += new EventHandler(pageListBoxSelectFiles_BeforeDisplay);
            this.pageListBoxSelectFiles.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageListBoxSelectFiles_BeforeMoveNext);
            this.pageFileSaveNames.BeforeDisplay += new EventHandler(pageFileSaveNames_BeforeDisplay);
            this.pageFileSaveNames.BeforeMoveBack += new Divelements.WizardFramework.WizardPageEventHandler(pageFileSaveNames_BeforeMoveBack);
            this.pageBackgroundFile.BeforeDisplay += new EventHandler(pageBackgroundFile_BeforeDisplay);
            this.pageBackgroundFile.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageBackgroundFile_BeforeMoveNext);
            this.pageProcessScaling.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageProcessScaling_BeforeMoveNext);
            this.pageComplete.BeforeDisplay += new EventHandler(pageComplete_BeforeDisplay);
            this.scalingWizard.Finish += new EventHandler(scalingWizard_Finish);

            //Hook up to Control events
            this.elbSaveFiles.AfterLabelEdit += new LabelEditEventHandler(elbSaveFiles_AfterLabelEdit);
        }
        #endregion


        #region Wizard Events
        /// <summary>
        /// Initialize does the setup for the Wizard prior to showing
        /// it to the User.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void scalingWizard_Enter(object sender, EventArgs e)
        {
            //If the Spectrum is not null and has RAW data, enable the RadioButton
            if (this.ActiveSpectrum != null && (this.ActiveSpectrum.SpectrumType & PhotoCurrent.IO.Enums.SpectrumType.RAW) > 0)
            {
                //Setup the RadioButton as the default
                this.rbChart.Enabled = true;
                this.rbChart.Checked = true;
            }
            else
            {
                //Disable the RadioButton and check the other
                this.rbChart.Enabled = false;
                this.rbExternal.Checked = true;
            }
        }


        /// <summary>
        /// pageListBoxSelectFiles_PageSetup fills the CheckedListBox with
        /// the Files that are in the current ActiveMdiChild.
        /// </summary>
        /// <param name="sender">The Page to be set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageListBoxSelectFiles_BeforeDisplay(object sender, EventArgs e)
        {
            //Set the EditableListBox not to be editable 
            this.elbRawFiles.LabelEdit = false;

            //If the radio button to use the Active Chart is 
            //selected, fill the ListBox from the ActiveMdiChild.
            //Otherwise, open the file dialog to get some files.
            if (this.rbChart.Checked == true)
            {
                //Get the files from the Spectrum window
                foreach (String path in this.ActiveSpectrum.FileManager.FilePaths)
                {
                    //Get the type of the File
                    PI.Enums.SpectrumType type = PI.Enums.SpectrumExtension.ToType(Path.GetExtension(path));
                    
                    //Check the type -- only add RAW and TXT files
                    if ((type & PI.Enums.SpectrumType.RAW) > 0 || (type & PI.Enums.SpectrumType.TAB3D) > 0)
                    {
                        //If the File hasn't been added, add it
                        if (this.elbRawFiles.FindItemWithText(path) == null)
                        {
                            //Create a ListViewItem
                            ListViewItem lvi = new ListViewItem(path);
                            lvi.Checked = true;

                            //Add it to the ListView 
                            this.elbRawFiles.Items.Add(lvi);
                        }
                    }
                }
            }
            else
            {
                //Set the description so that it doesn't mention the Active Chart
                this.pageListBoxSelectFiles.Description =
                    "Choose the RAW / TXT Data Files on disk to scale to IPCE.";

                //Now open the File Dialog to get the files
                this.btnOpenFileDialog_Click(null, null);
            }
        }


        /// <summary>
        /// pageListBoxSelectFiles_PageLeave makes sure that at least
        /// one file has been chosen prior to moving on.
        /// </summary>
        /// <param name="sender">The Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageListBoxSelectFiles_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Iterate through the ListBox and find at least one Checked Item
            Int32 numChecked = this.elbRawFiles.CheckedItems.Count;

            //If no Items are checked, popup an error message
            if (numChecked == 0)
            {
                //Popup an error message
                MessageBox.Show(this, "Select at least one RAW or TXT File to Scale to IPCE.",
                    "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Cancel the event
                e.Cancel = true;
            }

            //Get the first Checked Item type and compare it
            //to all of the others.  If the types don't match,
            //popup an error message
            PI.Enums.SpectrumType firstType = 
                PI.Enums.SpectrumExtension.ToType(Path.GetExtension(this.elbRawFiles.CheckedItems[0].Text));

            for (Int32 i = 1; i < this.elbRawFiles.CheckedItems.Count; i++)
            {
                //Get the Type of the current File
                PI.Enums.SpectrumType currType = 
                    PI.Enums.SpectrumExtension.ToType(Path.GetExtension(this.elbRawFiles.CheckedItems[i].Text));
                
                //If there are TWOD and THREED files in the Wizard, 
                //popup an error
                if (((firstType & PI.Enums.SpectrumType.TWOD) > 0 && (currType & PI.Enums.SpectrumType.THREED) > 0) ||
                    ((firstType & PI.Enums.SpectrumType.THREED) > 0 && (currType & PI.Enums.SpectrumType.TWOD) > 0))
                {
                    //Popup an error message
                    MessageBox.Show(this, "Cannot scale both 2-D and 3-D files at the same time.  Please select one type of file only.",
                        "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Cancel the event
                    e.Cancel = true;
                }
            }
        }


        /// <summary>
        /// pageFileSaveNames_PageSetup creates the Save File names 
        /// and displays them in the ListBox.
        /// </summary>
        /// <param name="sender">The Page to be set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageFileSaveNames_BeforeDisplay(object sender, EventArgs e)
        {
            //First, clear all Items from the ListView.  This is required
            //in case the user went back and unchecked any files.
            this.elbSaveFiles.Items.Clear();

            //Iterate through the Files and change their names
            //and Extensions, then add them to the Save Files list
            //if they are not there already
            foreach (ListViewItem lvi in this.elbRawFiles.Items)
            {
                //If the Item is checked, add it
                if (lvi.Checked == true)
                {
                    //Get the File Path
                    String filePath = lvi.Text;

                    //Change the File Name to have _IPCE after it
                    String ipceName = Path.GetFileNameWithoutExtension(filePath) + "_IPCE";

                    //Get the File Type
                    PI.Enums.SpectrumType type = PI.Enums.SpectrumExtension.ToType(Path.GetExtension(filePath));

                    //Add the Extension based on the type of file 
                    if ((type & PI.Enums.SpectrumType.TWOD) > 0)
                        ipceName += PI.Enums.SpectrumExtension.IpceExt;
                    else if ((type & PI.Enums.SpectrumType.TABFILE) > 0)
                        ipceName += PI.Enums.SpectrumExtension.TxtExt;
                    else if ((type & PI.Enums.SpectrumType.CSVFILE) > 0)
                        ipceName += PI.Enums.SpectrumExtension.Ipce3DExt;

                    //Finally, combine the Directory name with the file name
                    filePath = Path.Combine(Path.GetDirectoryName(filePath), ipceName);

                    //If the new Path isn't already in the ListView, add it
                    if (this.elbSaveFiles.FindItemWithText(filePath) == null)
                        this.elbSaveFiles.Items.Add(new ListViewItem(filePath));
                }
            }
        }


        /// <summary>
        /// pageFileSaveNames_PageLeave checks to make sure that 
        /// all of the file paths have Extensions.
        /// </summary>
        /// <param name="sender">The Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageFileSaveNames_BeforeMoveBack(object sender, CancelEventArgs e)
        {
            //Check to see if any file names changed and alert the user
            if (this._fileNamesChanged == true)
            {
                //Popup a message to indicate that the user will
                //lose information
                MessageBox.Show(this, "IPCE File Names have changed.  By moving to the previous page, these changes will be " +
                    "reset.  Remember to modify the values again when you return to this page.", "Wizard Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Reset the flag
                this._fileNamesChanged = false;
            }
        }


        /// <summary>
        /// pageBackgroundFile_PageSetup does the configuration of the 
        /// last scaling that was performed so that the user doesn't
        /// have to re-enter some information.
        /// </summary>
        /// <param name="sender">The Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageBackgroundFile_BeforeDisplay(object sender, EventArgs e)
        {
            //Get the Background Config Section
            ConfigHelper<PCS.Config.Background> config = new ConfigHelper<PCS.Config.Background>(ConfigLocation.AllUsers);
            PCS.Config.Background bg = config.GetConfig();

            //If the user is scaling THREED files, disable the 
            //File Open button and background path
            PI.Enums.SpectrumType type = 
                PI.Enums.SpectrumExtension.ToType(Path.GetExtension(this.elbSaveFiles.Items[0].Text));
            this.btnBrowseBkg.Enabled = false;
            this.tbBkgPath.Text = String.Empty;

            if ((type & PI.Enums.SpectrumType.TWOD) > 0)
            {
                //Enable the Open button
                this.btnBrowseBkg.Enabled = true;

                //Set the Background file
                this.tbBkgPath.Text = bg.LastBKGPath;
            }

            //Set the remaining controls in the page
            if (bg.LastScaleWavelength > 0)
                this.ntbWavelength.Text = bg.LastScaleWavelength.ToString();

            if (bg.LastScalePower > 0)
                this.ntbPower.Text = bg.LastScalePower.ToString();

            this.cbPowerUnit.Text = bg.LastScalePowerUnit.ToString();
        }


        /// <summary>
        /// pageBackgroundFile_PageLeave makes sure that all of the 
        /// Controls have been filled in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageBackgroundFile_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Declare a String to hold an Error
            String errText = String.Empty;
            Control errCtrl = null;

            //Check to make sure that the controls have values in them
            if (this.tbBkgPath.Text == String.Empty && this.btnBrowseBkg.Enabled == true)
            {
                errText = "Please choose a valid BKG Background File.";
                errCtrl = this.btnBrowseBkg;
            }

            if (this.ntbWavelength.Text == String.Empty)
            {
                errText = "Please choose a valid Power Wavelength.";
                errCtrl = this.ntbWavelength;
            }

            if (this.ntbPower.Text == String.Empty)
            {
                errText = "Please choose a valid Light Power.";
                errCtrl = this.ntbPower;
            }

            if (this.cbPowerUnit.Text == String.Empty)
            {
                errText = "Please choose a valid Power Unit.";
                errCtrl = this.cbPowerUnit;
            }

            //If there was an error, popup the Error and Cancel
            if (errText != String.Empty && errCtrl != null)
            {
                MessageBox.Show(this, errText, "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errCtrl.Focus();
                e.Cancel = true;
                return;
            }

            //Create a ConfigurationSection to save the config
            PCS.Config.Background bg = new PCS.Config.Background();

            //Fill the ConfigurationSection
            bg.LastBKGPath = this.tbBkgPath.Text;
            bg.LastScaleWavelength = Convert.ToUInt32(this.ntbWavelength.Text);
            bg.LastScalePower = Convert.ToDouble(this.ntbPower.Text);
            bg.LastScalePowerUnit = (PCS.Enums.PowerUnit)Enum.Parse(typeof(PCS.Enums.PowerUnit), this.cbPowerUnit.Text);

            //Save the values in the app.config file
            ConfigHelper<PCS.Config.Background> config = new ConfigHelper<PCS.Config.Background>(ConfigLocation.AllUsers);
            config.SetConfig(bg);
        }

        
        /// <summary>
        /// pageProcessScaling_PageLeave performs the actual scaling
        /// to IPCE with the PhotoCurrent.Scaling.IPCE class.
        /// </summary>
        /// <param name="sender">The Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageProcessScaling_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //First, show the Wizard as busy
            this.scalingWizard.UseWaitCursor = true;

            //Do all of the processing in the private function
            this.ProcessRAWFilesToIPCE();
        }


        /// <summary>
        /// pageComplete_PageSetup formats the Summary text.
        /// </summary>
        /// <param name="sender">The Page that is being setup</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageComplete_BeforeDisplay(object sender, EventArgs e)
        {
            //Get rid of the wait cursor
            this.scalingWizard.UseWaitCursor = false;

            //Get the number of files to show
            Int32 successful = this._ipceFilePaths.Count;
            Int32 failure = this.elbSaveFiles.Items.Count - successful;

            //Format the Summary
            this.lblSummary.Text = String.Format(this.lblSummary.Text, successful, failure); 

            //Enable the Checkbox if there are successful files and
            //they are TWOD
            Boolean enableOpen = false;

            if (successful > 0)
            {
                //Get the type of the first file
                PI.Enums.SpectrumType type =
                    PI.Enums.SpectrumExtension.ToType(Path.GetExtension(this._ipceFilePaths[0]));

                //Check the type for 2-D or a single 3-D
                if (((type & PI.Enums.SpectrumType.TWOD) > 0) ||
                    ((type & PI.Enums.SpectrumType.THREED) > 0 && successful == 1))
                    enableOpen = true;
            }

            //Set the CheckBox enabling
            this.cbOpenIPCE.Enabled = enableOpen;
            this.cbOpenIPCE.Checked = enableOpen;
        }


        /// <summary>
        /// scalingWizard_Finish closes the Wizard.
        /// </summary>
        /// <param name="sender">The Finish button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void scalingWizard_Finish(object sender, EventArgs e)
        {
            //Set the OK return
            this.DialogResult = DialogResult.OK;

            //Close this window
            this.Close();
        }
        #endregion


        #region Control Events
        /// <summary>
        /// cbSelectAll_CheckedChanged will select or deselect all of the 
        /// file names that have been added to the ListBox.
        /// </summary>
        /// <param name="sender">The CheckBox that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //Iterate through all of the Items and set the checkbox
            foreach (ListViewItem item in this.elbRawFiles.Items)
                item.Checked = this.cbSelectAll.Checked;
        }


        /// <summary>
        /// btnOpenFileDialog_Click opens the OpenFileDialog so that 
        /// files can be added to the ListBox.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            //Create an OpenFileDialog and set the Properties
            Dialogs.OpenFileDialog ofd = new Dialogs.OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = true;
            ofd.Title = "Open RAW / TXT Files to Scale";

            //Determine which file types to add to the Filter
            ofd.Filter = PhotoCurrent.IO.Enums.SpectrumExtension.ToString(
                PhotoCurrent.IO.Enums.SpectrumType.TWOD | PhotoCurrent.IO.Enums.SpectrumType.RAW,
                PhotoCurrent.IO.Enums.SpectrumType.THREED | PhotoCurrent.IO.Enums.SpectrumType.RAW,
                PhotoCurrent.IO.Enums.SpectrumType.TAB3D);

            //Open the dialog and add files to the ListBox
            //if the user chose any
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (String path in ofd.FileNames)
                {
                    //Add the file if it hasn't been added yet
                    if (this.elbRawFiles.FindItemWithText(path) == null)
                    {
                        //Create a ListViewItem
                        ListViewItem lvi = new ListViewItem(path);
                        lvi.Checked = true;

                        //Add the ListViewItem 
                        this.elbRawFiles.Items.Add(lvi);
                    }
                }

                //Save the Open File directory
                this._fileOpenDir = Path.GetDirectoryName(ofd.FileName);
            }
        }


        /// <summary>
        /// elbSaveFiles_AfterLabelEdit checks to make sure that the
        /// String in the ListView is not blank and has a .CSV extension.
        /// </summary>
        /// <param name="sender">The EditableListBox that is about to be edited</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void elbSaveFiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //If the string is blank, just cancel and return
            if (e.Label.Trim() == String.Empty)
            {
                e.CancelEdit = true;
                return;
            }

            //Set the flag to show that file names have been modified
            this._fileNamesChanged = true;
        }


        /// <summary>
        /// btnBrowseBkg_Click opens a File Dialog to find
        /// a Background file used to scale the RAW data.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void btnBrowseBkg_Click(object sender, EventArgs e)
        {
            //Create an OpenFileDialog and set the Properties
            Dialogs.OpenFileDialog ofd = new Dialogs.OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = PI.Enums.SpectrumExtension.ToString(PI.Enums.SpectrumType.BKG);
            ofd.Multiselect = false;
            ofd.Title = "Select Background File to Scale RAW Data";

            //Open the dialog and add the file to the TextBox
            //if the user chose any
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Set the TextBox path
                this.tbBkgPath.Text = ofd.FileName;

                //Save the Background directory
                this._bkgDir = Path.GetDirectoryName(ofd.FileName);
            }
        }
        #endregion


        #region Public Properties 
        /// <summary>
        /// ActiveSpectrum is set by the Main form so that the
        /// Scaling Wizard can use it for data.
        /// </summary>
        public Forms.Spectrum ActiveSpectrum
        {
            get { return this._spectrum; }
            set { this._spectrum = value; }
        }


        /// <summary>
        /// FileOpenDirectory allows the Main form to set the 
        /// directories for the File Open.
        /// </summary>
        public String FileOpenDirectory
        {
            get { return this._fileOpenDir; }
            set { this._fileOpenDir = value; }
        }

        
        /// <summary>
        /// BackgroundDirectory allows the Main form to set the 
        /// directories for the Background.
        /// </summary>
        public String BackgroundDirectory
        {
            get { return this._bkgDir; }
            set { this._bkgDir = value; }
        }

        
        /// <summary>
        /// FileSaveDirectory allows the Main form to set the 
        /// directories for the File Save.
        /// </summary>
        public String FileSaveDirectory
        {
            get { return this._saveDir; }
            set { this._saveDir = value; }
        }


        /// <summary>
        /// ShowIPCEFiles is an indicator to the Main window
        /// that it needs to create another Spectrum Window
        /// with the scaled IPCE files.
        /// </summary>
        public Boolean ShowIPCEFiles
        {
            get { return this.cbOpenIPCE.Checked; }
        }


        /// <summary>
        /// IPCEFilePaths returns the list of file paths
        /// that were successfully created by the Scaling
        /// Wizard.
        /// </summary>
        public String[] IPCEFileNames
        {
            get { return this._ipceFilePaths.ToArray(); }
        }
        #endregion


        #region Main File Processing Method
        /// <summary>
        /// ProcessRAWFilesToIPCE does the real work of this Wizard:
        /// it processes all of the data into a stack of IPCE Files.
        /// </summary>
        /// <returns>Boolean TRUE if all is successful, FALSE otherwise</returns>
        private Boolean ProcessRAWFilesToIPCE()
        {
            //Clear the Successful Files
            this._ipceFilePaths.Clear();

            //Calculate the number of steps required for the scaling:
            //the # of files to save + 1 for setting up the BKG
            Int32 steps = this.elbSaveFiles.Items.Count + 1;

            //Setup the ProgressBar
            this.progScale.Maximum = steps;
            this.progScale.Minimum = 0;
            this.progScale.Step = 1;
            this.progScale.Value = 0;

            //Set the status to generating Background info
            this.lblCurrentTask.Text = "Processing Background Information into Lookup Table...";

            //Create a new Scaling.IPCE object to do the work
            PCS.IPCE scaler = new PCS.IPCE();

            //Get the Background Config Section
            ConfigHelper<PCS.Config.Background> config = new ConfigHelper<PCS.Config.Background>(ConfigLocation.AllUsers);
            PCS.Config.Background bg = config.GetConfig();

            try
            {
                //Try to setup the BKG information
                scaler.SetupBackground(bg.LastBKGPath, bg.LastScaleWavelength, bg.LastScalePower, bg.LastScalePowerUnit);
            }
            catch (System.Exception e)
            {
                //Popup a message and return false
                MessageBox.Show(this, "Could not generate Background Lookup Table:  " + e.Message, "Wizard Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            //Increment the ProgressBar
            this.progScale.PerformStep();

            //Iterate through the checked files in the RAW ListView
            Int32 saveIndex = 0;
            foreach (ListViewItem rawLVI in this.elbRawFiles.CheckedItems)
            {
                //Get the Save File ListViewItem
                ListViewItem saveLVI = this.elbSaveFiles.Items[saveIndex];

                //Set the Status message
                this.lblCurrentTask.Text = "Scaling file '" + rawLVI.Text + "' to '" + saveLVI.Text + "'...";

                try
                {
                    //Try to create the IPCE file
                    scaler.ScaleFile(rawLVI.Text, saveLVI.Text, this.cbOverwriteIPCE.Checked);

                    //Save the File Path in the member variable
                    this._ipceFilePaths.Add(saveLVI.Text);
                }
                catch (System.Exception e2)
                {
                    //Popup a Question and return false if necessary
                    if (MessageBox.Show(this, "Could not scale file '" + rawLVI.Text + "' to '" + saveLVI.Text + "':  " + 
                        e2.Message + "\n\nContinue scaling remaining files?", "Wizard Error", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        //Set the ProgressBar to full to indicate that stuff is finished
                        this.progScale.Value = steps;
                        return false;
                    }
                }
                
                //Increment the ProgressBar
                this.progScale.PerformStep();

                //Increment the Save File Index
                saveIndex++;
            }

            //Return true to indicate that the files were scaled properly
            return true;
        }
        #endregion
    }
}
