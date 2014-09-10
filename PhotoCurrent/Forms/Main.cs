using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using PhotoCurrent.Config;
using PhotoCurrent.IO.Enums;
using RI = RDH2.Instrumentation;
using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Forms
{
    public partial class Main : Form
    {
        #region Member Variables
        private String _openDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private String _bkgDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private String _saveDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        //Printing variables
        private Int32 _printIndex = 0;
        private Int32 _chartsPerPage = 4;
        private System.Drawing.Printing.PrintDocument _document = new System.Drawing.Printing.PrintDocument();
        private List<Form> _mdiChildren = new List<Form>();

        //Thread-safe delegate
        private delegate void ActivateDelegate(Object sender, EventArgs e);
        #endregion


        #region Constructor
        public Main()
        {
            InitializeComponent();

            //Get the Config from the app.config
            ConfigHelper<MainWindow> config = new ConfigHelper<MainWindow>(ConfigLocation.AllUsers);
            MainWindow mwc = config.GetConfig();

            //Do the setup on the Window 
            this.Height = mwc.Height;
            this.Width = mwc.Width;
            this.WindowState = mwc.State;

            //Setup the Shown event so that the Hardware
            //Wizard can be opened if necessary
            this.Shown += new EventHandler(Main_Shown);
            
            //Setup the MdiChildActivate event
            this.MdiChildActivate += new EventHandler(Main_MdiChildActivate);

            //Call the MdiChildActivate event to set up the 
            //buttons initially
            this.Main_MdiChildActivate(null, null);

            //Setup the Close event
            this.FormClosing += new FormClosingEventHandler(Main_FormClosing);
        }
        #endregion


        #region Main Window Events
        /// <summary>
        /// Main_Shown checks the configuration file to determine
        /// if the Hardware Wizard should be shown.
        /// </summary>
        /// <param name="sender">The Form that is being Shown</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void Main_Shown(object sender, EventArgs e)
        {
            //Get the General Hardware configuration
            ConfigHelper<RI.Config.General> config = new ConfigHelper<RI.Config.General>(ConfigLocation.AllUsers);
            RI.Config.General gen = config.GetConfig();

            //If the hardware has not been configured, show
            //the Hardware Wizard
            if (gen.IsConfigured == false && gen.DoNotConfigure == false)
                this.mnuHWSetup_Click(null, null);
        }

        
        /// <summary>
        /// MdiChildActivate is called when an MDI Child either is
        /// activated or closed.
        /// </summary>
        /// <param name="sender">The Main window that has a Child activate / close</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void Main_MdiChildActivate(object sender, EventArgs e)
        {
            //Get the Active Child
            Forms.Spectrum child = this.ActiveMdiChild as Forms.Spectrum;

            //Setup the Menu and Toolbar based on the properties
            //of the active Child

            //Do the File Menu:  first, get the DaqInterface Config to 
            //see if any data acquisition types have been setup
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetConfig();

            Boolean enableNew = false;
            if (Array.IndexOf(di.AcquisitionTypes, RI.Enums.AcquisitionType.Invalid) == -1)
                enableNew = true;

            if (child != null && (child.SpectrumType & SpectrumType.THREED) == 0)
                enableNew = true;

            foreach (Form frm in this.MdiChildren)
            {
                //Cast the Form
                Spectrum spec = frm as Spectrum;

                //If the Spectrum is acquiring data, disable the
                //New Spectrum button
                if (spec.IsRunningSpectrum == true)
                {
                    enableNew = false;
                    break;
                }
            }

            this.mnuNew.Enabled = enableNew;
            this.tbNew.Enabled = enableNew;

            Boolean enableAdd = false;
            if (child != null && (child.SpectrumType & SpectrumType.THREED) == 0)
                enableAdd = true;

            this.mnuAdd.Enabled = enableAdd;
            this.tbAdd.Enabled = enableAdd;

            //Do the Print functions
            Boolean enablePrint = false;
            if (child != null)
                enablePrint = true;

            this.mnuPrint.Enabled = enablePrint;
            this.tbPrint.Enabled = enablePrint;

            //Do the Plot menu
            Boolean enableAutoscale = false;
            if (child != null)
                enableAutoscale = true;

            this.mnuAutoscale.Enabled = enableAutoscale;
            this.tbAutoscale.Enabled = enableAutoscale;

            Boolean enableZoomReset = false;
            if (child != null && child.IsZoomedIn == true)
                enableZoomReset = true;

            this.mnuResetZoom.Enabled = enableZoomReset;
            this.tbResetZoom.Enabled = enableZoomReset;

            Boolean enableRunCtrls = false;
            if (child != null && child.IsRunningSpectrum == true)
                enableRunCtrls = true;

            this.mnuPause.Enabled = enableRunCtrls;
            this.tbPause.Enabled = enableRunCtrls;
            this.mnuStop.Enabled = enableRunCtrls;
            this.tbStop.Enabled = enableRunCtrls;

            //Do the Configure Menu
            Boolean enableConfigure = false;
            if (RI.DAQ.DaqFactory.ConfiguredCard != null)
                enableConfigure = true;

            this.mnuConfigCurrent.Enabled = enableConfigure;

            List<RI.Enums.AcquisitionType> types = new List<RI.Enums.AcquisitionType>(di.AcquisitionTypes);
            this.mnuConfigMC.Enabled = types.Contains(RI.Enums.AcquisitionType.TwoD);
            this.mnuConfigMirrors.Enabled = types.Contains(RI.Enums.AcquisitionType.ThreeD);
        }


        /// <summary>
        /// Spectrum_ZoomChanged alerts the Main window to update
        /// the Toolbar when Zooms occur.
        /// </summary>
        /// <param name="sender">The Spectrum window that was zoomed</param>
        /// <param name="e">The EventArgs sent by the Spectrum window</param>
        void Spectrum_ZoomChanged(Object sender, EventArgs e)
        {
            //Update the Toolbar by calling the Main_MdiChildActivate method
            this.Main_MdiChildActivate(null, null);
        }


        /// <summary>
        /// Spectrum_AcquisitionStopped alerts the Main window to update
        /// the Toolbar when acquisition stops.
        /// </summary>
        /// <param name="sender">The Spectrum Window on which acquisition stopped</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void addSpec_AcquisitionStopped(object sender, EventArgs e)
        {
            //Update the Toolbar by calling the Main_MdiChildActivate method
            if (this.InvokeRequired == true)
                this.Invoke(new ActivateDelegate(this.Main_MdiChildActivate), null, null);
        }

        
        /// <summary>
        /// Main_FormClosing simply saves the Window Properties 
        /// in the app.config file
        /// </summary>
        /// <param name="sender">The Window being disposed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Get the ConfigSection
            ConfigHelper<MainWindow> config = new ConfigHelper<MainWindow>(ConfigLocation.AllUsers);
            MainWindow mwc = config.GetWriteableConfig();

            //Set the properties
            mwc.Height = this.Height;
            mwc.Width = this.Width;
            mwc.State = this.WindowState;

            //Save the properties in the app.config
            config.SetConfig(mwc);
        }
        #endregion


        #region ToolbarItem Events
        /// <summary>
        /// tbNew_Click opens the New Spectrum Wizard and allows
        /// the user to begin the process of acquiring a new 
        /// Spectrum.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbNew_Click(object sender, EventArgs e)
        {
            //Get the Active MDI Child
            Forms.Spectrum child = this.ActiveMdiChild as Forms.Spectrum;

            //Create and open the New Spectrum Wizard
            Wizards.NewSpectrum ns = new Wizards.NewSpectrum();
            ns.ActiveSpectrum = child;
            ns.SaveDirectory = this._saveDir;

            if (ns.ShowDialog(this) == DialogResult.OK)
            {
                //Declare a Spectrum Form variable
                Spectrum addSpec = this.ActiveMdiChild as Spectrum;
                if (addSpec == null || ns.AddToActive == false)
                    addSpec = new Spectrum();

                //Attempt to add the Spectrum
                if (addSpec.AddSpectrum(ns.SpectrumBase) == true)
                {
                    //Set the MDI Parent
                    addSpec.MdiParent = this;

                    //Subscribe to the ZoomChanged event
                    addSpec.ZoomChanged += new EventHandler(Spectrum_ZoomChanged);

                    //Subscribe to the AcquisitionStopped event
                    addSpec.AcquisitionStopped += new EventHandler(addSpec_AcquisitionStopped);

                    //Open the Spectrum Window
                    addSpec.Show();

                    //Persist the Save Directory
                    this._saveDir = ns.SaveDirectory;
                }
            }
        }


        /// <summary>
        /// tbOpen_Click opens a File Open Dialog and allows the 
        /// user to open a Spectrum in the Spectrum Window.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbOpen_Click(object sender, EventArgs e)
        {
            //Pop up an Open File box
            Dialogs.OpenFileDialog ofd = new Dialogs.OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.Filter = SpectrumExtension.ToString();
            ofd.Multiselect = true;
            ofd.Title = "Select PhotoCurrent File to Open";

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                //Make a Spectrum Window and tell it to add the spectra
                Forms.Spectrum openSpec = new Forms.Spectrum();
                if (openSpec.OpenFiles(ofd.FileNames) == true)
                {
                    //Set the MDI Parent
                    openSpec.MdiParent = this;

                    //Subscribe to the ZoomChanged event
                    openSpec.ZoomChanged += new EventHandler(Spectrum_ZoomChanged);

                    //Open the Spectrum Window
                    openSpec.Show();

                    //Save the current directory in the Open member variable
                    this._openDir = Path.GetDirectoryName(ofd.FileName);
                }
            }
        }


        /// <summary>
        /// tbAdd_Click allows the user to add and remove Spectra to the
        /// Active window.  
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbAdd_Click(object sender, EventArgs e)
        {
            //Get the Active MDI Child
            Forms.Spectrum child = this.ActiveMdiChild as Forms.Spectrum;

            //If the child is not found, popup an error message
            if (child == null)
                MessageBox.Show("No Active Window to which to add files.");

            //Pop up an AddRemoveSpectra Wizard
            Wizards.AddRemoveSpectra ars = new Wizards.AddRemoveSpectra();
            ars.ActiveSpectrum = child;
            ars.ShowDialog();
        }


        /// <summary>
        /// tbPrint_Click prints the ActiveMdiChild.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbPrint_Click(object sender, EventArgs e)
        {
            //Get the ActiveMdiChild
            Forms.Spectrum child = this.ActiveMdiChild as Forms.Spectrum;

            //If the child was retrieved, popup a PrintDialog
            if (child != null)
            {
                //Popup a PrintDialog
                RDH2.Utilities.Dialogs.PrintDialog pd = new RDH2.Utilities.Dialogs.PrintDialog();
                pd.DisablePageNums = true;
                pd.DisableSelection = true;

                //Create a new CustomPrintCtrl
                Printing.CustomPrintCtrl ctrl = new Printing.CustomPrintCtrl();
                ctrl.ChartsPerPage = this._chartsPerPage;
                pd.CustomControl = ctrl;

                //Set the PrintDocument
                pd.Document = this._document;

                //Show the dialog and print if necessary
                if (pd.ShowDialog(this) == DialogResult.OK)
                {
                    //Reset the Print Index and Charts per page
                    this._printIndex = 0;
                    this._chartsPerPage = ctrl.ChartsPerPage;

                    //Set up the MdiChildren to be printed based on the 
                    //number of copies being made
                    this._mdiChildren.Clear();

                    for (Int32 i = 0; i < pd.Copies; i++)
                    {
                        if (ctrl.PrintAll == true)
                            this._mdiChildren.AddRange((this.MdiChildren));
                        else
                            this._mdiChildren.Add(child);
                    }
                    
                    //Setup the member variable PrintDocument for printing
                    this._document.DocumentName = "PhotoCurrent Chart (" + this._chartsPerPage.ToString() + " charts per page)";

                    if (ctrl.ChartsPerPage == 2)
                        this._document.DefaultPageSettings.Landscape = false;
                    else
                        this._document.DefaultPageSettings.Landscape = true;

                    //Hook up the PrintPage event on the PrintDocument
                    this._document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(document_PrintPage);

                    //Print the document -- this will iterate through all of
                    //the Windows and print them to a particular region
                    this._document.Print();
                }
            }
        }


        /// <summary>
        /// document_PrintPage sets up a Region on the Graphics object
        /// and has the current Spectrum form print to it.
        /// </summary>
        /// <param name="sender">The PrintDocument that is being printed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Calculate the number of columns and rows that there 
            //will be based on the Charts per page -- default to
            //printing full page.
            Int32 columns = 1;
            Int32 rows = 1;

            switch (this._chartsPerPage)
            {
                case 2:
                    columns = 1;
                    rows = 2;
                    break;

                case 4:
                    columns = 2;
                    rows = 2;
                    break;
            }

            //Calculate the Width of a chart based on the number set up per page
            Double plotWidthF = Math.Round(Convert.ToSingle(e.MarginBounds.Width) / Convert.ToSingle(columns));

            //If the number of Charts per page is greater than 1, add some 
            //padding between the charts
            if (columns > 1)
                plotWidthF -= 25;

            //Calculate the plot Height from the Width to look like
            //a piece of paper
            Double plotHeightF = plotWidthF * 0.7;

            //Iterate through the Child Windows
            //and render them to the Graphics
            for (Int32 i = 0; (i < columns) && (this._printIndex < this._mdiChildren.Count); i++)
            {
                for (Int32 j = 0; (j < rows) && (this._printIndex < this._mdiChildren.Count); j++, this._printIndex++)
                {
                    //Calculate the X and Y from the row and column
                    Double plotXF = (i * plotWidthF) + e.MarginBounds.Left;
                    Double plotYF = (j * plotHeightF) + e.MarginBounds.Top;

                    //Create a Rectangle from the data
                    Rectangle plotRect = new Rectangle(
                        Convert.ToInt32(plotXF), Convert.ToInt32(plotYF),
                        Convert.ToInt32(plotWidthF), Convert.ToInt32(plotHeightF));

                    //Get the child Form as a Spectrum
                    Spectrum child = this._mdiChildren[this._printIndex] as Spectrum;

                    //Render the plot
                    if (child != null)
                        child.PrintToRegion(e.Graphics, plotRect);
                }
            }

            //If there are still plots to print, set the EventArgs
            if (this._printIndex < (this._mdiChildren.Count))
                e.HasMorePages = true;
        }


        /// <summary>
        /// tbAutoscale_Click resets the Axes on the currently
        /// active Specrum Window.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbAutoscale_Click(object sender, EventArgs e)
        {
            //Get the Active MDI Child
            Forms.Spectrum child = this.ActiveMdiChild as Forms.Spectrum;

            //If the child is not found, popup an error message
            if (child == null)
                MessageBox.Show("No Active Window to reset zoom.");
            else
                child.AutoscaleAxes();
        }

        
        /// <summary>
        /// tbResetZoom_Click resets the zoom on the currently active Spectrum Window
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbResetZoom_Click(object sender, EventArgs e)
        {
            //Get the Active MDI Child
            Forms.Spectrum child = this.ActiveMdiChild as Forms.Spectrum;

            //If the child is not found, popup an error message
            if (child == null)
                MessageBox.Show("No Active Window to reset zoom.");
            else
                child.ResetZoom();
        }

        
        /// <summary>
        /// Pauses the Data Collection in the current Window
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbPause_Click(object sender, EventArgs e)
        {
            //Get the active Spectrum window
            Spectrum child = this.ActiveMdiChild as Spectrum;

            //If there is no active Window, popup a message
            if (child == null)
            {
                MessageBox.Show(this, "No child window to Pause Data Acquisition.");
                return;
            }

            //Pause or Resume the Data Acquisition based on
            //the Tag of the button
            if (this.tbPause.Tag.ToString() == "P")
                child.PauseDataAcquisition();
            else
                child.ResumeDataAcquisition();

            //Check the tag and change the icon and ToolTip as needed
            String toolTip = "Pause Data Collection";
            String imageRes = "PauseDataCollection";
            String tag = "P";

            if (this.tbPause.Tag.ToString() == "P")
            {
                toolTip = "Continue Data Collection";
                imageRes = "ContinueDataCollection";
                tag = "C";
            }

            //Set the ToolTip and Tag
            this.tbPause.ToolTipText = toolTip;
            this.tbPause.Tag = tag;

            //Get a ResourceManager and change the icon
            ComponentResourceManager resources = new 
                ComponentResourceManager(typeof(PhotoCurrent.Properties.Resources));
            this.tbPause.Image = ((System.Drawing.Image)(resources.GetObject(imageRes)));
        }


        /// <summary>
        /// tbStop_Click stops the active Spectrum window from 
        /// acquiring any more data.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbStop_Click(object sender, EventArgs e)
        {
            //Get the active Spectrum window
            Spectrum child = this.ActiveMdiChild as Spectrum;

            //If there is no active Window, popup a message
            if (child == null)
            {
                MessageBox.Show(this, "No child window to Stop Data Acquisition.");
                return;
            }

            //Stop the data acquisition
            child.StopDataAcquisition();

            //Update the toolbar
            this.Main_MdiChildActivate(null, null);
        }


        /// <summary>
        /// tbScale_Click opens the Scale to IPCE Wizard to allow the user 
        /// to scale RAW files to IPCE.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbScale_Click(object sender, EventArgs e)
        {
            //Create the Wizard and set the Properties
            Wizards.Scaling scale = new Wizards.Scaling();
            scale.ActiveSpectrum = this.ActiveMdiChild as Forms.Spectrum;
            scale.FileOpenDirectory = this._openDir;
            scale.BackgroundDirectory = this._bkgDir;
            scale.FileSaveDirectory = this._saveDir;

            //If the user finishes the Wizard, pass the values
            //to the Scaling object
            if (scale.ShowDialog(this) == DialogResult.OK)
            {
                //If the user wants to open the IPCE files, do it
                if (scale.ShowIPCEFiles == true)
                {
                    //Make a Spectrum Window and tell it to add the spectra
                    Forms.Spectrum openSpec = new Forms.Spectrum();
                    if (openSpec.OpenFiles(scale.IPCEFileNames) == true)
                    {
                        //Set the MDI Parent
                        openSpec.MdiParent = this;

                        //Subscribe to the ZoomChanged event
                        openSpec.ZoomChanged += new EventHandler(Spectrum_ZoomChanged);

                        //Open the Spectrum Window
                        openSpec.Show();
                    }
                }
            }
        }


        /// <summary>
        /// tbExport_Click opens the Export to CSV Wizard to allow the
        /// user to export SpectrumType.CSV files to an easy-to-open
        /// CSV file format without headers.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void tbExport_Click(object sender, EventArgs e)
        {
            //Create the Wizard and set the Properties
            Wizards.Export export = new Wizards.Export();
            export.ActiveSpectrum = this.ActiveMdiChild as Forms.Spectrum;

            //Open the Wizard.  It does the rest.
            export.ShowDialog(this);
        }
        #endregion


        #region MenuItem Events
        /// <summary>
        /// Opens the New Spectrum Wizard and starts a Spectrum going
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuNew_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbNew_Click(sender, e);
        }


        /// <summary>
        /// Opens a new Spectrum Window with the selected files
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs that was sent by the System</param>
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbOpen_Click(sender, e);
        }


        /// <summary>
        /// Adds a Spectrum to the current window
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuAdd_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbAdd_Click(sender, e);
        }

        
        /// <summary>
        /// Prints the active window
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuPrint_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbPrint_Click(sender, e);
        }

        
        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            //Close the application
            Application.Exit();
        }


        /// <summary>
        /// mnuAutoscale_Click resets the axes on the active plot.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuAutoscale_Click(object sender, EventArgs e)
        {
            this.tbAutoscale_Click(sender, e);
        }


        /// <summary>
        /// Completely zooms out the currently active Spectrum
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuResetZoom_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbResetZoom_Click(sender, e);
        }

        
        /// <summary>
        /// Pauses the current Spectrum window's data collection
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuPause_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbPause_Click(sender, e);
        }


        /// <summary>
        /// Stops the current Spectrum window's data collection
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuStop_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbStop_Click(sender, e);
        }


        /// <summary>
        /// Opens the Scaling Wizard to scale RAW data to IPCE
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuScaleRAW_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbScale_Click(sender, e);
        }


        /// <summary>
        /// Opens the Export Wizard to export data to an easier
        /// file format without headers.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuExport_Click(object sender, EventArgs e)
        {
            //Forward to the Toolbar
            this.tbExport_Click(sender, e);
        }

        
        /// <summary>
        /// Opens the Configure DAQ Card Wizard.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuConfigDAQ_Click(object sender, EventArgs e)
        {
            //Open the DAQ Card Wizard
            this.ShowConfigurationWizard(Wizards.Hardware.HardwareType.DaqCard);
        }


        /// <summary>
        /// Opens the Configure Current Input Wizard.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuConfigLockIn_Click(object sender, EventArgs e)
        {
            //Open the Current Input Wizard
            this.ShowConfigurationWizard(Wizards.Hardware.HardwareType.CurrentInput);
        }


        /// <summary>
        /// Opens the Configure Monochromator Wizard.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuConfigMC_Click(object sender, EventArgs e)
        {
            //Open the Monochromator Wizard
            this.ShowConfigurationWizard(Wizards.Hardware.HardwareType.Monochromator);
        }


        /// <summary>
        /// Opens the Configure Mirrors Wizard.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuConfigMirrors_Click(object sender, EventArgs e)
        {
            //Open the Mirrors Wizard
            this.ShowConfigurationWizard(Wizards.Hardware.HardwareType.Mirrors);
        }


        /// <summary>
        /// Opens the Configure Potentiostat Wizard.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuConfigPotStat_Click(object sender, EventArgs e)
        {
            //Open the Potentiostat Wizard
            this.ShowConfigurationWizard(Wizards.Hardware.HardwareType.Potentiostat);
        }


        /// <summary>
        /// Opens the complete Hardware Setup Wizard.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuHWSetup_Click(object sender, EventArgs e)
        {
            //Open the complete Wizard
            this.ShowConfigurationWizard(Wizards.Hardware.HardwareType.All);
        }


        /// <summary>
        /// Tiles the open windows Horizontally
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuTileHorz_Click(object sender, EventArgs e)
        {
            //Tile Horizontal
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }


        /// <summary>
        /// Tiles the open windows Vertically
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs passed by the System</param>
        private void mnuTileVert_Click(object sender, EventArgs e)
        {
            //Tile Vertically
            this.LayoutMdi(MdiLayout.TileVertical);
        }


        /// <summary>
        /// Tiles the open windows in a Cascade
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuCascade_Click(object sender, EventArgs e)
        {
            //Tile Cascade
            this.LayoutMdi(MdiLayout.Cascade);
        }

        
        /// <summary>
        /// mnuAbout_Click opens the About Box.
        /// </summary>
        /// <param name="sender">The MenuItem that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            //Open the About box
            About about = new About();
            about.Show(this);
        }
        #endregion


        #region Helper Functions
        /// <summary>
        /// Creates a Hardware Wizard, configures it to show the 
        /// appropriate pages, and shows it.
        /// </summary>
        /// <param name="type">The Type of Wizard to create</param>
        private void ShowConfigurationWizard(Wizards.Hardware.HardwareType type)
        {
            //Open the Hardware Wizard.  It takes care of the rest.
            Wizards.Hardware wizard = new Wizards.Hardware();
            wizard.Type = type;
            wizard.ShowDialog(this);

            //Make the Activate code run again so that any 
            //disabled icons are enabled if necessary
            this.Main_MdiChildActivate(null, null);
        }
        #endregion
    }
}
