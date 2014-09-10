using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using PhotoCurrent.IO;

namespace PhotoCurrent.Wizards
{
    /// <summary>
    /// The Export Wizard allows a user to export multiple files
    /// into a single file with no headers except File Names and
    /// all of the data multiplexed.
    /// </summary>
    public partial class Export : Form
    {
        #region Member Variables
        private Forms.Spectrum _spectrum = null;
        private Boolean _fileNamesChanged = false;
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor for the Export Wizard
        /// </summary>
        public Export()
        {
            //Do the standard setup
            InitializeComponent();

            //Hook up the Wizard Events\
            this.exportWizard.Enter += new EventHandler(exportWizard_Enter);
            this.pageListBoxSelectFiles.BeforeDisplay += new EventHandler(pageListBoxSelectFiles_BeforeDisplay);
            this.pageListBoxSelectFiles.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageListBoxSelectFiles_BeforeMoveNext);
            this.pageSelectOutput.BeforeDisplay += new EventHandler(pageSelectOutput_BeforeDisplay);
            this.pageSelectOutput.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageSelectOutput_BeforeMoveNext);
            this.pageFileSaveNames.BeforeDisplay += new EventHandler(pageFileSaveNames_BeforeDisplay);
            this.pageFileSaveNames.BeforeMoveBack += new Divelements.WizardFramework.WizardPageEventHandler(pageFileSaveNames_BeforeMoveBack);
            this.exportWizard.Finish += new EventHandler(exportWizard_Finish);

            //Hook up the Saved File Names changed event
            this.elbSaveFiles.AfterLabelEdit += new LabelEditEventHandler(elbSaveFiles_AfterLabelEdit);
        }
        #endregion 


        #region Wizard Events
        /// <summary>
        /// exportWizard_Enter checks the Active Chart and
        /// sets up the RadioButtons as necessary.
        /// </summary>
        /// <param name="sender">The Wizard that is being initialized</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void exportWizard_Enter(object sender, EventArgs e)
        {
            //If the Spectrum is not null, enable the appropriate
            //RadioButton
            if (this.ActiveSpectrum != null)
            {
                //If the Spectrum contains TWOD data, enable and 
                //check the 2-D RB.  Otherwise, enable and check
                //the 3-D button.
                if ((this.ActiveSpectrum.SpectrumType & PhotoCurrent.IO.Enums.SpectrumType.TWOD) > 0)
                {
                    this.rbChart2D.Enabled = true;
                    this.rbChart2D.Checked = true;
                    this.rbChart3D.Enabled = false;
                    this.rbChart3D.Checked = false;
                }
                else
                {
                    this.rbChart3D.Enabled = true;
                    this.rbChart3D.Checked = true;
                    this.rbChart2D.Enabled = false;
                    this.rbChart2D.Checked = false;
                }
            }
            else
            {
                //Disable the RadioButtons and check the Other 2-D 
                this.rbChart2D.Enabled = false;
                this.rbChart3D.Enabled = false;
                this.rbOther2D.Checked = true;
            }
        }


        /// <summary>
        /// pageListBoxSelectFiles_BeforeDisplay pulls the files from the
        /// Active Chart if any exist.  Otherwise, the File Open box
        /// is created and opened.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageListBoxSelectFiles_BeforeDisplay(object sender, EventArgs e)
        {
            //Set the EditableListBox not to be editable 
            this.elbRawFiles.LabelEdit = false;

            //If the radio button to use the Active Chart is 
            //selected, fill the ListBox from the ActiveMdiChild.
            //Otherwise, open the file dialog to get some files.
            if (this.rbChart2D.Checked == true || this.rbChart3D.Checked == true)
            {
                //Get the files from the Spectrum window
                foreach (String path in this.ActiveSpectrum.FileManager.FilePaths)
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
            else
            {
                //Set the description so that it doesn't mention the Active Chart
                this.pageListBoxSelectFiles.Description =
                    "Choose the BKG / RAW / IPCE Data Files on disk to Export.";

                //Now open the File Dialog to get the files
                this.btnOpenFileDialog_Click(null, null);
            }
        }


        /// <summary>
        /// pageListBoxSelectFiles_BeforeMoveNext ensures that at least
        /// one file has been selected to Export.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageListBoxSelectFiles_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Iterate through the ListBox and find at least one Checked Item
            Int32 numChecked = this.elbRawFiles.CheckedItems.Count;

            //If no Items are checked, popup an error message
            if (numChecked == 0)
            {
                //Show an error
                MessageBox.Show(this, "Select at least one BKG, RAW, or IPCE File to Export.",
                    "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Set the EventArgs to Cancel the Move Next
                e.Cancel = true; 
            }

            //Get the first Checked Item type and compare it
            //to all of the others.  If the types don't match,
            //popup an error message
            PhotoCurrent.IO.Enums.SpectrumType firstType =
                PhotoCurrent.IO.Enums.SpectrumExtension.ToType(Path.GetExtension(this.elbRawFiles.CheckedItems[0].Text));

            for (Int32 i = 1; i < this.elbRawFiles.CheckedItems.Count; i++)
            {
                //Get the Type of the current File
                PhotoCurrent.IO.Enums.SpectrumType currType =
                    PhotoCurrent.IO.Enums.SpectrumExtension.ToType(Path.GetExtension(this.elbRawFiles.CheckedItems[i].Text));

                //If there are TWOD and THREED files in the Wizard, 
                //popup an error
                if (((firstType & PhotoCurrent.IO.Enums.SpectrumType.TWOD) > 0 && 
                     (currType & PhotoCurrent.IO.Enums.SpectrumType.THREED) > 0) ||
                    ((firstType & PhotoCurrent.IO.Enums.SpectrumType.THREED) > 0 && 
                     (currType & PhotoCurrent.IO.Enums.SpectrumType.TWOD) > 0))
                {
                    //Popup an error message
                    MessageBox.Show(this, "Cannot scale both 2-D and 3-D files at the same time.  Please select one type of file only.",
                        "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Cancel the event
                    e.Cancel = true;
                }
            }

            //Setup the Page Sequence
            this.SetupPageSequence();
        }


        /// <summary>
        /// pageSelectOutput_BeforeDisplay opens the File Open 
        /// Dialog so that the user can select an Output file.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSelectOutput_BeforeDisplay(object sender, EventArgs e)
        {
            //If the Output File hasn't been selected, pop up the 
            //File Dialog box
            if (this.txtOutputFile.Text == String.Empty)
                this.btnBrowseOutput_Click(null, null);
        }


        /// <summary>
        /// pageSelectOutput_BeforeMoveNext checks to make sure 
        /// that an output file has been chosen.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSelectOutput_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //If the Output File hasn't been selected, cancel with Error
            if (this.txtOutputFile.Text == String.Empty)
            {
                //Popup an error message
                MessageBox.Show(this, "Please select an Output CSV File.", "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Set the event to cancel
                e.Cancel = true;
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
                    //Get the File Name
                    String r3dFile = Path.GetFileNameWithoutExtension(lvi.Text) + 
                        PhotoCurrent.IO.Enums.SpectrumExtension.Raw3DExt;

                    //Create the new File Path
                    String filePath = Path.Combine(Path.GetDirectoryName(lvi.Text), r3dFile);

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
                MessageBox.Show(this, "3-D File Names have changed.  By moving to the previous page, these changes will be " +
                    "reset.  Remember to modify the values again when you return to this page.", "Wizard Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Reset the flag
                this._fileNamesChanged = false;
            }
        }


        /// <summary>
        /// exportWizard_Finish does the actual work of exporting
        /// the Files and closing the dialog.
        /// </summary>
        /// <param name="sender">The Wizard Page that is finished</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void exportWizard_Finish(object sender, EventArgs e)
        {
            //Process the input based on the type of File
            if (this.rbChart2D.Checked == true || this.rbOther2D.Checked == true)
                this.DialogResult = this.ExportFilesToMaster();
            else
                this.DialogResult = this.SaveFilesAsR3D();

            //Finally, close the Dialog
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

            //Set the UI based on the type of File being exported:
            //2-D or 3-D
            if (this.rbChart2D.Checked == true || this.rbOther2D.Checked == true)
            {
                ofd.Filter = PhotoCurrent.IO.Enums.SpectrumExtension.ToString(PhotoCurrent.IO.Enums.SpectrumType.CSV2D);
                ofd.Title = "Open BKG / RAW / IPCE Files to Export";
            }
            else
            {
                ofd.Filter = PhotoCurrent.IO.Enums.SpectrumExtension.ToString(PhotoCurrent.IO.Enums.SpectrumType.TAB3D);
                ofd.Title = "Open TXT Files to Export";
            }

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
            }
        }


        /// <summary>
        /// btnBrowseOutput_Click opens the File Save Dialog 
        /// so that the user can select the Export Output file.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            //Create an OpenSaveDialog and set the Properties
            Dialogs.SaveFileDialog sfd = new Dialogs.SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;
            sfd.Filter = PhotoCurrent.IO.Enums.SpectrumExtension.IpceFilter;
            sfd.OverwritePrompt = true;
            sfd.Title = "Select Export CSV File";

            //Open the dialog and add file to the TextBox
            if (sfd.ShowDialog() == DialogResult.OK)
                this.txtOutputFile.Text = sfd.FileName;
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
        #endregion


        #region Helper Methods
        /// <summary>
        /// SetupPageSequence configures all of the WizardPages 
        /// based on the HardwareType of the Wizard.
        /// </summary>
        private void SetupPageSequence()
        {
            //Create a List to hold the pages that will be
            //included in the Wizard
            List<Divelements.WizardFramework.WizardPageBase> pages = new List<Divelements.WizardFramework.WizardPageBase>();

            //Add the front page
            pages.Add(this.pageSelectSource);

            //Add the Select ListBox page
            pages.Add(this.pageListBoxSelectFiles);

            //If the user selected 2-D files, go to the single 
            //CSV file output page.  Otherwise, go to the Save 
            //ListBox page.
            if (this.rbChart2D.Checked == true || this.rbOther2D.Checked == true)
                pages.Add(this.pageSelectOutput);
            else
                pages.Add(this.pageFileSaveNames);

            //Add the remaining pages
            pages.Add(this.pageProgress);

            //Iterate through the Pages and set up the Next and
            //Previous Pages
            for (Int32 i = 0; i < pages.Count; i++)
            {
                //Set the PreviousPage if there is one
                if (i > 0)
                    pages[i].PreviousPage = pages[i - 1];

                //Set the NextPage if there is one
                if (i < pages.Count - 1)
                    pages[i].NextPage = pages[i + 1];
            }
        }


        /// <summary>
        /// ExportFiles does the actual moving of CSV files to a 
        /// master file using the FileExporter object.
        /// </summary>
        private DialogResult ExportFilesToMaster()
        {
            //Declare a value to return
            DialogResult rtn = DialogResult.OK;

            //Calculate the number of steps required for the scaling:
            //the # of files to save + 1 for the Save
            Int32 steps = this.elbRawFiles.Items.Count + 1;

            //Setup the ProgressBar
            this.progExport.Maximum = steps;
            this.progExport.Minimum = 0;
            this.progExport.Step = 1;
            this.progExport.Value = 0;

            //Create an Exporter to do the Export
            FileExporter fe = new FileExporter();

            //Iterate through the file names in the EditableListBox
            //and add them to the Exporter
            foreach (ListViewItem lvi in this.elbRawFiles.Items)
            {
                //Set the status message
                this.lblCurrentTask.Text = "Processing File '" + lvi.Text + "'...";

                //Add the file to the FileExporter
                try
                {
                    fe.AddFile(lvi.Text);
                }
                catch (Exception exc)
                {
                    //Popup a Question and return false if necessary
                    if (MessageBox.Show(this, "Could not export file '" + lvi.Text + "':  " + exc.Message +
                        "\n\nContinue exporting remaining files?", "Export File Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        //Set the ProgressBar to full to indicate that stuff is finished
                        this.progExport.Value = steps;
                        rtn = DialogResult.Abort;
                    }
                }

                //Increment the ProgressBar
                this.progExport.PerformStep();
            }

            //Save out the result to disk
            try
            {
                //Set the progress text
                this.lblCurrentTask.Text = "Saving Exported File to '" + this.txtOutputFile.Text + "'...";

                //Save out the file
                fe.Save(this.txtOutputFile.Text);

                //Perform that last step
                this.progExport.PerformStep();
            }
            catch (Exception exc2)
            {
                //Popup an error box
                MessageBox.Show(this, "Could not save Export file '" + this.txtOutputFile.Text + "':" + exc2.Message,
                    "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Set the DialogResult
                rtn = DialogResult.Abort;
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// SaveFilesAsR3D takes all of the files that have 
        /// been selected and turns them into R3D files.
        /// </summary>
        private DialogResult SaveFilesAsR3D()
        {
            //Declare a variable to return
            DialogResult rtn = DialogResult.OK;

            //Calculate the number of steps required for the export:
            //the # of files to save 
            Int32 steps = this.elbRawFiles.Items.Count;

            //Setup the ProgressBar
            this.progExport.Maximum = steps;
            this.progExport.Minimum = 0;
            this.progExport.Step = 1;
            this.progExport.Value = 0;

            //Iterate through the file names in the EditableListBox
            //and save them as R3D files
            Int32 saveIndex = 0;
            foreach (ListViewItem txtLVI in this.elbRawFiles.CheckedItems)
            {
                //Get the Save File ListViewItem
                ListViewItem saveLVI = this.elbSaveFiles.Items[saveIndex];

                //Set the Status message
                this.lblCurrentTask.Text = "Processing file '" + txtLVI.Text + "' to '" + saveLVI.Text + "'...";

                //Save the file as an R3D
                try
                {
                    //Parse the data out of the TXT file
                    SpectrumFile txt = new SpectrumFile(txtLVI.Text);

                    //Create a new R3D SpectrumFile
                    SpectrumFile r3d = new SpectrumFile();
                    r3d.Comment = String.Empty;
                    r3d.DelaySeconds = 0.0;
                    r3d.FileCreateDate = File.GetCreationTime(txtLVI.Text);
                    r3d.Type = PhotoCurrent.IO.Enums.SpectrumType.THREED | 
                               PhotoCurrent.IO.Enums.SpectrumType.CSVFILE | 
                               PhotoCurrent.IO.Enums.SpectrumType.RAW;

                    //Create the Double Arrays in the SpectrumFile
                    r3d.Setup3DArray(txt.ThreeDValues.Count, txt.ThreeDValues[0].Count);

                    //Iterate through the file and add them to the R3D
                    for (Int32 i = 0; i < txt.ThreeDValues.Count; i++)
                    {
                        //If the row doesn't exist yet, create it
                        if (r3d.ThreeDValues.Count <= i)
                            r3d.ThreeDValues.Add(new List<Double>());

                        //Copy the data to the new List
                        for (Int32 j = 0; j < txt.ThreeDValues[0].Count; j++)
                            r3d.ThreeDValues[i].Add(txt.ThreeDValues[i][j]);
                     }

                    //Save the file out using the Save Path
                    r3d.Save(saveLVI.Text, this.cbOverwriteR3D.Checked);
                }
                catch (Exception exc)
                {
                    //Popup a Question and return false if necessary
                    if (MessageBox.Show(this, "Could not export file '" + txtLVI.Text + "':  " + exc.Message +
                        "\n\nContinue exporting remaining files?", "Export File Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        //Set the ProgressBar to full to indicate that stuff is finished
                        this.progExport.Value = steps;
                        this.DialogResult = DialogResult.Abort;
                    }
                }

                //Increment the ProgressBar
                this.progExport.PerformStep();

                //Increment the Save File Index
                saveIndex++;
            }

            //Return the result
            return rtn;
        }
        #endregion
    }
}
