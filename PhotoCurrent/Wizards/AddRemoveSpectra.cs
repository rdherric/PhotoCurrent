using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PhotoCurrent.Wizards
{
    public partial class AddRemoveSpectra : Form
    {
        #region Member Variables
        private Forms.Spectrum _spectrum = null;
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor for the AddRemoveSpectra Wizard.
        /// </summary>
        public AddRemoveSpectra()
        {
            //Do the standard setup
            InitializeComponent();

            //Hook up to events
            this.pageSpectraListBox.Enter += new EventHandler(pageSpectraListBox_Enter);
            this.pageSpectraListBox.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageSpectraListBox_BeforeMoveNext);
            this.wizardAddRemove.Finish += new EventHandler(wizardAddRemove_Finish);
        }
        #endregion


        #region Wizard Events
        /// <summary>
        /// pageSpectraListBox_Enter gets the SpectrumFiles
        /// from the ActiveSpectrum and puts them in the ListBox.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being entered</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSpectraListBox_Enter(object sender, EventArgs e)
        {
            //Set the EditableListBox not to be editable 
            this.elbRawFiles.LabelEdit = false;

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

        
        /// <summary>
        /// pageSpectraListBox_PageLeave checks to make sure that
        /// there is at least one spectrum checked in the ListBox.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSpectraListBox_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Iterate through the ListBox and find at least one Checked Item
            Int32 numChecked = this.elbRawFiles.CheckedItems.Count;

            //If no Items are checked, popup an error message
            if (numChecked == 0)
            {
                //Popup an error
                MessageBox.Show(this, "Please select at least one RAW, BKG, or IPCE file to display.",
                    "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Cancel the MoveNext
                e.Cancel = true;
            }
        }

        
        /// <summary>
        /// wizardAddRemove_Finish sets up the Spectrum form
        /// and closes the Wizard.
        /// </summary>
        /// <param name="sender">The Wizard being Finished</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void wizardAddRemove_Finish(object sender, EventArgs e)
        {
            //Get the checked Items from the ListBox
            List<String> filePaths = new List<String>();
            ListView.CheckedListViewItemCollection chkItems = this.elbRawFiles.CheckedItems;

            foreach (ListViewItem lvi in chkItems)
                filePaths.Add(lvi.Text);

            //Setup the Spectrum Window
            this._spectrum.FileManager.SpectrumFiles.Clear();
            this._spectrum.OpenFiles(filePaths.ToArray());

            //Set the DialogResult and Close
            this.DialogResult = DialogResult.OK;
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
            ofd.Filter = PhotoCurrent.IO.Enums.SpectrumExtension.ToString(PhotoCurrent.IO.Enums.SpectrumType.CSV2D);
            ofd.Multiselect = true;
            ofd.Title = "Open BKG / RAW / IPCE Files to Add";

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
    }
}
