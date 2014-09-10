using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using RI = RDH2.Instrumentation;
using RDH2.Utilities.Configuration;
using PhotoCurrent.IO.Enums;

namespace PhotoCurrent.Wizards
{
    /// <summary>
    /// The New Spectrum Wizard walks a user through the
    /// process of setting up a new Spectrum.  This Wizard
    /// handles all of the different types of Spectrum.
    /// </summary>
    public partial class NewSpectrum : Form
    {
        #region Member Variables
        private Forms.Spectrum _spectrum = null;
        private String _saveDir = String.Empty;
        private Boolean _hardwareSetup = false;
        private Boolean _positionsSetup = false;
        private Acquisition.SpectrumBase _specBase = null;
        private RI.DAQ.DaqBase _db = null;
        private PhotoCurrent.Scaling.Photocurrent _pcScale = null;
        private System.Threading.Timer _tmrFrequency = null;
        private System.Threading.Timer _tmrLIAValue = null;
        private Int32 _timerPeriod = 1000;

        //Summary Strings
        private String _scanningSummary =
            "Acquisition Summary:\n" + 
            "Scan Wavelength: {0} - {1} nm\n" +
            "Increment: {2} nm\n" + 
            "Acquisition Delay: {3} sec\n" + 
            "Samples to Average: {4}\n" +
            "Potentiostat Current Range: {5} {6}\n" +
            "Lock-In Amp Sensitivity: {7}\n" + 
            "Output File: {8}\n\n" +
            "Scan Duration: {9}";

        private String _timeSummary =
            "Excitation Wavelength: {0} nm\n" +
            "Acquisition Delay: {1} sec\n" +
            "Samples to Average: {2}\n" +
            "Potentiostat Current Range: {3} {4}\n" +
            "Lock-In Amp Sensitivity: {5}\n" +
            "Output File: {6}";

        private String _positionSummary =
            "X-Axis Start Position: {0}\n" +
            "X-Axis End Position: {1}\n" +
            "Y-Axis Start Position: {2}\n" +
            "Y-Axis End Position: {3}\n" +
            "Step Size: {4}\n" +
            "Acquisition Delay: {5} sec\n" +
            "Samples to Average: {6}\n" +
            "Potentiostat Current Range: {7} {8}\n" +
            "Lock-In Amp Sensitivity: {9}\n" +
            "Output File: {10}\n\n" +
            "Scan Duration: {11}";
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor for the NewSpectrum Wizard.
        /// </summary>
        public NewSpectrum()
        {
            //Do the initial setup
            InitializeComponent();

            //Hook into the events
            this.newSpecWizard.Enter += new EventHandler(newSpecWizard_Enter);
            this.pageSpectrumType.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageSpectrumType_BeforeMoveNext);
            this.pagePositionSetup.BeforeDisplay += new EventHandler(pagePositionSetup_BeforeDisplay);
            this.pagePositionSetup.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pagePositionSetup_BeforeMoveNext);
            this.pageSpectrumSetup.BeforeDisplay += new EventHandler(pageSpectrumSetup_BeforeDisplay);
            this.pageSpectrumSetup.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageSpectrumSetup_BeforeMoveNext);
            this.pageHardwareSetup.BeforeDisplay += new EventHandler(pageHardwareSetup_BeforeDisplay);
            this.pageHardwareSetup.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageHardwareSetup_BeforeMoveNext);
            this.pageSoftwareLIASetup.BeforeDisplay += new EventHandler(pageSoftwareLIASetup_BeforeDisplay);
            this.pageSoftwareLIASetup.Enter += new EventHandler(pageSoftwareLIASetup_Enter);
            this.pageSoftwareLIASetup.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageSoftwareLIASetup_BeforeMoveNext);
            this.pageFileName.BeforeDisplay += new EventHandler(pageFileName_BeforeDisplay);
            this.pageFileName.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageFileName_BeforeMoveNext);
            this.pageComplete.BeforeDisplay += new EventHandler(pageComplete_BeforeDisplay);
            this.newSpecWizard.Finish += new EventHandler(newSpecWizard_Finish);
        }
        #endregion


        #region Wizard Events
        /// <summary>
        /// newSpecWizard_WizardInitialize determines the types of 
        /// spectra can be acquired.  
        /// </summary>
        /// <param name="sender">The Wizard that is being initialized</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void newSpecWizard_Enter(object sender, EventArgs e)
        {
            //Get the RI.Config.DaqInterface config object
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetConfig();

            //Get the AcquisitionTypes
            RI.Enums.AcquisitionType[] acqTypes = di.AcquisitionTypes;

            //Disable all of the RadioButtons
            this.rb3DPosition.Enabled = false;
            this.rbBackground.Enabled = false;
            this.rbLEGO.Enabled = false;
            this.rbTime.Enabled = false;
            this.rbWavelength.Enabled = false;

            //Iterate through the Types and enable the 
            //appropriate RadioButtons
            foreach (RI.Enums.AcquisitionType type in acqTypes)
            {
                switch (type)
                {
                    case RI.Enums.AcquisitionType.LEGO:
                        this.rbLEGO.Enabled = true;
                        break;

                    case RI.Enums.AcquisitionType.ThreeD:
                        this.rb3DPosition.Enabled = true;
                        break;

                    case RI.Enums.AcquisitionType.TwoD:
                        this.rbBackground.Enabled = true;
                        this.rbTime.Enabled = true;
                        this.rbWavelength.Enabled = true;
                        break;
                }
            }

            //Select the first unchecked RadioButton
            if (this.rbWavelength.Enabled == true)
                this.rbWavelength.Checked = true;
            else if (this.rb3DPosition.Enabled == true)
                this.rb3DPosition.Checked = true;
            else if (this.rbLEGO.Enabled == true)
                this.rbLEGO.Checked = true;
        }

        
        /// <summary>
        /// pageSpectrumType_PageLeave checks the type of Spectrum
        /// that is being acquired and changes the Destination Page
        /// accordingly.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSpectrumType_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Setup the page sequence
            this.SetupPageSequence();
        }


        /// <summary>
        /// pagePositionSetup_PageSetup sets the values in the 
        /// controls for the 3-D position setup.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pagePositionSetup_BeforeDisplay(object sender, EventArgs e)
        {
            //If the positions have already been setup, ignore
            //this event
            if (this._positionsSetup == true)
                return;

            //Get the Configuration object
            ConfigHelper<Config.Position> config = new ConfigHelper<Config.Position>(ConfigLocation.AllUsers);
            Config.Position pos = config.GetConfig();

            //Set the values in the Controls
            this.spinXPosStart.Value = Convert.ToDecimal(pos.XPosStart);
            this.spinXPosEnd.Value = Convert.ToDecimal(pos.XPosEnd);
            this.spinYPosStart.Value = Convert.ToDecimal(pos.YPosStart);
            this.spinYPosEnd.Value = Convert.ToDecimal(pos.YPosEnd);
            this.ntbStepSize.Text = pos.StepSize.ToString();

            //Set the flag 
            this._positionsSetup = true;
        }


        /// <summary>
        /// pagePositionSetup_PageLeave checks to make sure that
        /// all of the controls have values in them.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pagePositionSetup_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Declare variables to hold errors
            String errText = String.Empty;
            Control errCtrl = null;

            //Check to make sure all controls have values
            if (this.ntbStepSize.Text == String.Empty)
            {
                errText = "Please enter a value for the Step Size.";
                errCtrl = this.ntbStepSize;
            }

            //If there is any error, popup the message and Cancel
            if (errText != String.Empty && errCtrl != null)
            {
                MessageBox.Show(this, errText, "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errCtrl.Focus();
                e.Cancel = true;
            }
        }

        
        /// <summary>
        /// pageSpectrumSetup_PageSetup enables or disables controls
        /// and enters the default information based on the type of
        /// Spectrum being created.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSpectrumSetup_BeforeDisplay(object sender, EventArgs e)
        {
            //Enable all of the controls
            this.ntbStartWavelength.Enabled = true;
            this.ntbEndWavelength.Enabled = true;
            this.ntbIncrement.Enabled = true;

            //Setup the Page based on the type of spectrum being created
            String pageTitle = String.Empty;
            if (this.rbWavelength.Checked == true)
            {
                //Set the Title
                pageTitle = "Wavelength";

                //Get the Configuration
                ConfigHelper<Config.Wavelength> config = new ConfigHelper<Config.Wavelength>(ConfigLocation.AllUsers);
                Config.Wavelength wl = config.GetConfig();

                //Set the values in the controls
                this.ntbStartWavelength.Text = wl.StartWavelength.ToString();
                this.ntbEndWavelength.Text = wl.EndWavelength.ToString();
                this.ntbIncrement.Text = wl.WavelengthIncrement.ToString();
                this.ntbDelay.Text = wl.AcquisitionDelay.ToString();
                this.ntbSamplesToAvg.Text = wl.SamplesToAverage.ToString();
            }
            else if (this.rbTime.Checked == true)
            {
                //Set the Title
                pageTitle = "Time";

                //Disable the End and Increment controls
                this.ntbEndWavelength.Enabled = false;
                this.ntbIncrement.Enabled = false;

                //Get the Configuration
                ConfigHelper<Config.Time> config = new ConfigHelper<Config.Time>(ConfigLocation.AllUsers);
                Config.Time t = config.GetConfig();

                //Set the values in the controls
                this.ntbStartWavelength.Text = t.StartWavelength.ToString();
                this.ntbEndWavelength.Text = String.Empty;
                this.ntbIncrement.Text = String.Empty;
                this.ntbDelay.Text = t.AcquisitionDelay.ToString();
                this.ntbSamplesToAvg.Text = t.SamplesToAverage.ToString();
            }
            else if (this.rbBackground.Checked == true)
            {
                //Set the Title
                pageTitle = "Background";

                //Get the Configuration
                ConfigHelper<Config.Background> config = new ConfigHelper<Config.Background>(ConfigLocation.AllUsers);
                Config.Background b = config.GetConfig();

                //Set the values in the controls
                this.ntbStartWavelength.Text = b.StartWavelength.ToString();
                this.ntbEndWavelength.Text = b.EndWavelength.ToString();
                this.ntbIncrement.Text = b.WavelengthIncrement.ToString();
                this.ntbDelay.Text = b.AcquisitionDelay.ToString();
                this.ntbSamplesToAvg.Text = b.SamplesToAverage.ToString();
            }
            else if (this.rb3DPosition.Checked == true || this.rbLEGO.Checked == true)
            {
                //Set the Title
                pageTitle = "Position";

                //Disable the controls
                this.ntbStartWavelength.Enabled = false;
                this.ntbEndWavelength.Enabled = false;
                this.ntbIncrement.Enabled = false;

                //Get the Configuration
                ConfigHelper<Config.Position> config = new ConfigHelper<Config.Position>(ConfigLocation.AllUsers);
                Config.Position p = config.GetConfig();

                //Set the values in the controls
                this.ntbStartWavelength.Text = String.Empty;
                this.ntbEndWavelength.Text = String.Empty;
                this.ntbIncrement.Text = String.Empty;
                this.ntbDelay.Text = p.AcquisitionDelay.ToString();
                this.ntbSamplesToAvg.Text = p.SamplesToAverage.ToString();
            }

            //Set the Title on the Wizard Page
            this.pageSpectrumSetup.Text = String.Format("{0} Spectrum Setup", pageTitle);
        }


        /// <summary>
        /// pageSpectrumSetup_PageLeave makes sure that there are 
        /// values in the controls prior to letting the user start
        /// the Spectrum.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSpectrumSetup_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Declare variables to hold errors
            String errText = String.Empty;
            Control errCtrl = null;

            //Check the controls and make sure there is a value
            if (this.rbBackground.Checked == true || this.rbTime.Checked == true || this.rbWavelength.Checked == true)
            {
                if (this.ntbStartWavelength.Text == String.Empty)
                {
                    errText = "Please enter a value for the Starting Wavelength."; 
                    errCtrl = this.ntbStartWavelength;
                }
            }

            if (this.rbBackground.Checked == true || this.rbWavelength.Checked == true)
            {
                if (this.ntbEndWavelength.Text == String.Empty && this.rbTime.Checked == false)
                {
                    errText = "Please enter a value for the Ending Wavelength."; 
                    errCtrl = this.ntbEndWavelength;
                }

                if (this.ntbIncrement.Text == String.Empty && this.rbTime.Checked == false)
                {
                    errText = "Please enter a value for the Wavelength Increment.";
                    errCtrl = this.ntbIncrement;
                }
            }

            if (this.ntbDelay.Text == String.Empty)
            {
                errText = "Please enter a value for the Acquisition Delay."; 
                errCtrl = this.ntbDelay;
            }

            if (this.ntbSamplesToAvg.Text == String.Empty)
            {
                errText = "Please enter a value for the Samples to Average.";
                errCtrl = this.ntbSamplesToAvg;
            }

            //If there is any error, popup the message and Cancel
            if (errText != String.Empty && errCtrl != null)
            {
                MessageBox.Show(this, errText, "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errCtrl.Focus();
                e.Cancel = true;
            }
        }


        /// <summary>
        /// pageHardwareSetup_PageSetup retrieves the configuration
        /// and sets the controls.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageHardwareSetup_BeforeDisplay(object sender, EventArgs e)
        {
            //If the DDLs have already been setup, leave them alone
            if (this._hardwareSetup == true)
                return;

            //Get the RI.Config.Potentiostat config
            ConfigHelper<RI.Config.Potentiostat> potStatConfig = new ConfigHelper<RI.Config.Potentiostat>(ConfigLocation.AllUsers);
            RI.Config.Potentiostat potStat = potStatConfig.GetConfig();
            
            //Get the Lock-In Amp config
            ConfigHelper<RI.Config.LockInAmp> liaConfig = new ConfigHelper<RI.Config.LockInAmp>(ConfigLocation.AllUsers);
            RI.Config.LockInAmp lia = liaConfig.GetConfig();

            //Set the control values
            this.cbCurrentRange.SelectedItem = potStat.CurrentRange.ToString();
            this.cbCurrentUnits.SelectedItem = potStat.Unit.ToString();
            this.cbLIAType.SelectedItem = lia.Type.ToString();
            this.cbSensitivity.SelectedItem = lia.Sensitivity.ToString();
            this.cbSensitivityUnits.SelectedItem = lia.Unit.ToString();

            //Set the flag
            this._hardwareSetup = true;
        }


        /// <summary>
        /// pageHardwareSetup_BeforeMoveNext sets up the next page
        /// based on the value of the Lock-In Type.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageHardwareSetup_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Setup the page sequence
            this.SetupPageSequence();
        }


        /// <summary>
        /// pageSoftwareLIASetup_BeforeDisplay enables the Laser Position
        /// controls as necessary and sets the values on them.
        /// </summary>
        /// <param name="sender">The Page that is about to be displayed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSoftwareLIASetup_BeforeDisplay(object sender, EventArgs e)
        {
            //Disable the controls
            this.spinXAxisPos.Enabled = false;
            this.spinYAxisPos.Enabled = false;

            //If a Position type scan was selected, enable the
            //position controls and set their values
            if (this.rb3DPosition.Checked == true || this.rbLEGO.Checked == true)
            {
                this.spinXAxisPos.Enabled = true;
                this.spinXAxisPos.Value = this.spinXPosStart.Value;
                this.spinXAxisPos.Increment = Convert.ToInt32(this.ntbStepSize.Text);

                this.spinYAxisPos.Enabled = true;
                this.spinYAxisPos.Value = this.spinYPosStart.Value;
                this.spinYAxisPos.Increment = Convert.ToInt32(this.ntbStepSize.Text);
            }
        }

        
        /// <summary>
        /// pageSoftwareLIASetup_Enter does the actual input lock and
        /// acquistion of the signal while displaying messages for the
        /// user.
        /// </summary>
        /// <param name="sender">The Page that was entered</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSoftwareLIASetup_Enter(object sender, EventArgs e)
        {
            //If the Software LIA was chosen, set that value in 
            //the Config file -- this is required for the DaqBase
            //to configure properly.
            ConfigHelper<RI.Config.LockInAmp> liaConfig = new ConfigHelper<RI.Config.LockInAmp>(ConfigLocation.AllUsers);
            RI.Config.LockInAmp lia = liaConfig.GetWriteableConfig();
            lia.Type = RI.Enums.LockInType.Software;
            liaConfig.SetConfig(lia);

            //If this page hasn't been viewed before, nothing will 
            //have been set up, so create the appropriate objects
            //and start the Timers
            if (this._db == null)
                this._db = RI.DAQ.DaqFactory.ConfiguredCard;

            //Create the Scaler if it hasn't been created yet
            if (this._pcScale == null)
                this._pcScale = new PhotoCurrent.Scaling.Photocurrent();

            //Setup the LockIn amp as Software -- other parameters
            //don't matter
            this._pcScale.SetupLockInAmp(RI.Enums.LockInType.Software, 1U, RI.Enums.SensitivityUnit.Invalid, 0.0);

            //Setup the Potentiostat -- use values from controls
            //except FullScale
            ConfigHelper<RI.Config.Potentiostat> psConfig = new ConfigHelper<RI.Config.Potentiostat>(ConfigLocation.AllUsers);
            RI.Config.Potentiostat ps = psConfig.GetConfig();
            this._pcScale.SetupPotentiostat(
                Convert.ToDouble(this.cbCurrentRange.SelectedItem), 
                (RI.Enums.CurrentUnit)Enum.Parse(typeof(RI.Enums.CurrentUnit), this.cbCurrentUnits.SelectedItem.ToString()), 
                ps.FullScale);

            //Create the Timers
            if (this._tmrFrequency == null && this._tmrLIAValue == null)
            {
                this._tmrFrequency = new System.Threading.Timer(new System.Threading.TimerCallback(this.ShowFrequency));
                this._tmrLIAValue = new System.Threading.Timer(new System.Threading.TimerCallback(this.ShowLIAValue));
            }

            //Start the Timers going to update the LIA values
            this._tmrFrequency.Change(this._timerPeriod, this._timerPeriod);
            this._tmrLIAValue.Change(this._timerPeriod, this._timerPeriod);
        }


        /// <summary>
        /// pageSoftwareLIASetup_BeforeMoveNext changes the Timers so 
        /// that they do not update the messages to the user.
        /// </summary>
        /// <param name="sender">The Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageSoftwareLIASetup_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Change the Timers so as not to update the LIA values
            this._tmrFrequency.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            this._tmrLIAValue.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        
        /// <summary>
        /// pageFileName_PageEnter opens the File Save Dialog the
        /// first time the page is entered.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being entered</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageFileName_BeforeDisplay(object sender, EventArgs e)
        {
            //If the user has already set up a file, ignore this event
            if (this.txtFileName.Text != String.Empty)
                return;

            //Call the Click event
            this.btnFileSave_Click(null, null);
        }


        /// <summary>
        /// pageFileName_BeforeMoveNext checks to make sure that 
        /// there is a file name prior to moving on.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageFileName_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //If there is no file name, cancel the move
            if (this.txtFileName.Text == String.Empty)
            {
                //Popup the message
                MessageBox.Show(this, "Please choose a File Name for the new Spectrum.", "Wizard Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Cancel the event
                e.Cancel = true;
            }
        }

        
        /// <summary>
        /// pageComplete_PageSetup displays the Summary string
        /// to the user before beginning the scan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageComplete_BeforeDisplay(object sender, EventArgs e)
        {
            //Set the Summary text based on the type of Spectrum
            if (this.rbWavelength.Checked == true || this.rbBackground.Checked == true)
            {
                //Set the Summary text
                this.lblSummary.Text = String.Format(this._scanningSummary,
                    this.ntbStartWavelength.Text, this.ntbEndWavelength.Text,
                    this.ntbIncrement.Text, this.ntbDelay.Text,
                    this.ntbSamplesToAvg.Text, this.cbCurrentRange.SelectedItem,
                    this.cbCurrentUnits.SelectedItem, this.FormatLIASensitivity(),
                    Path.GetFileName(this.txtFileName.Text), this.Get2DTimeRemainingString());
            }
            else if (this.rbTime.Checked == true)
            {
                //Set the summary text
                this.lblSummary.Text = String.Format(this._timeSummary,
                    this.ntbEndWavelength.Text, this.ntbDelay.Text,
                    this.ntbSamplesToAvg.Text, this.cbCurrentRange.SelectedItem,
                    this.FormatLIASensitivity(), this.cbSensitivityUnits.SelectedItem, 
                    Path.GetFileName(this.txtFileName.Text));
            }
            else if (this.rb3DPosition.Checked == true || this.rbLEGO.Checked == true)
            {
                this.lblSummary.Text = String.Format(this._positionSummary,
                    this.spinXPosStart.Value, this.spinXPosEnd.Value,
                    this.spinYPosStart.Value, this.spinYPosEnd.Value,
                    this.ntbStepSize.Text, this.ntbDelay.Text,
                    this.ntbSamplesToAvg.Text, this.cbCurrentRange.SelectedItem,
                    this.cbCurrentUnits.SelectedItem, this.FormatLIASensitivity(), 
                    Path.GetFileName(this.txtFileName.Text), this.Get3DTimeRemainingString());
            }

            //Enable the Add to Active CheckBox as needed
            if (this._spectrum != null && (this._spectrum.SpectrumType & SpectrumType.THREED) == 0)
                this.cbAddToActive.Enabled = true;

            //Setup the No Display CheckBox 
            Config.AcquisitionConfigBase acb = null;
            if (this.rbWavelength.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Wavelength> wlConfig = new ConfigHelper<Config.Wavelength>(ConfigLocation.AllUsers);
                acb = wlConfig.GetWriteableConfig();
            }
            else if (this.rbTime.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Time> tConfig = new ConfigHelper<Config.Time>(ConfigLocation.AllUsers);
                acb = tConfig.GetWriteableConfig();
            }
            else if (this.rbBackground.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Background> bgConfig = new ConfigHelper<Config.Background>(ConfigLocation.AllUsers);
                acb = bgConfig.GetWriteableConfig();
            }
            else if (this.rb3DPosition.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Position> posConfig = new ConfigHelper<Config.Position>(ConfigLocation.AllUsers);
                acb = posConfig.GetWriteableConfig();
            }

            this.cbNoDisplay.Checked = acb.NoDisplay;
        }


        /// <summary>
        /// newSpecWizard_Finish persists all of the data to the 
        /// app.config file and closes the Dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void newSpecWizard_Finish(object sender, EventArgs e)
        {
            //Save the RI.Config.Potentiostat data
            ConfigHelper<RI.Config.Potentiostat> psConfig = new ConfigHelper<RI.Config.Potentiostat>(ConfigLocation.AllUsers);
            RI.Config.Potentiostat ps = psConfig.GetWriteableConfig();

            ps.CurrentRange = Convert.ToDouble(this.cbCurrentRange.SelectedItem);
            ps.Unit = (RI.Enums.CurrentUnit)Enum.Parse(typeof(RI.Enums.CurrentUnit), this.cbCurrentUnits.SelectedItem.ToString());

            psConfig.SetConfig(ps);

            //Save the Lock-In Amp data
            ConfigHelper<RI.Config.LockInAmp> liaConfig = new ConfigHelper<RI.Config.LockInAmp>(ConfigLocation.AllUsers);
            RI.Config.LockInAmp lia = liaConfig.GetWriteableConfig();

            lia.Type = (RI.Enums.LockInType)Enum.Parse(typeof(RI.Enums.LockInType), this.cbLIAType.SelectedItem.ToString());
            lia.Sensitivity = Convert.ToUInt32(this.cbSensitivity.SelectedItem);
            lia.Unit = (RI.Enums.SensitivityUnit)Enum.Parse(typeof(RI.Enums.SensitivityUnit), this.cbSensitivityUnits.SelectedItem.ToString());

            liaConfig.SetConfig(lia);

            //Save the scan parameters based on the Spectrum type
            if (this.rbWavelength.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Wavelength> wlConfig = new ConfigHelper<Config.Wavelength>(ConfigLocation.AllUsers);
                Config.Wavelength wl = wlConfig.GetWriteableConfig();

                //Set the properties
                wl.AcquisitionDelay = Convert.ToDouble(this.ntbDelay.Text);
                wl.EndWavelength = Convert.ToUInt32(this.ntbEndWavelength.Text);
                wl.SamplesToAverage = Convert.ToUInt32(this.ntbSamplesToAvg.Text);
                wl.StartWavelength = Convert.ToUInt32(this.ntbStartWavelength.Text);
                wl.WavelengthIncrement = Convert.ToUInt32(this.ntbIncrement.Text);
                wl.NoDisplay = this.cbNoDisplay.Checked;

                //Save the parameters
                wlConfig.SetConfig(wl);

                //Set the SpectrumBase
                this._specBase = new Acquisition.Spectrum2D(wl);
            }
            else if (this.rbTime.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Time> tConfig = new ConfigHelper<Config.Time>(ConfigLocation.AllUsers);
                Config.Time t = tConfig.GetWriteableConfig();

                //Set the properties
                t.AcquisitionDelay = Convert.ToDouble(this.ntbDelay.Text);
                t.SamplesToAverage = Convert.ToUInt32(this.ntbSamplesToAvg.Text);
                t.StartWavelength = Convert.ToUInt32(this.ntbStartWavelength.Text);
                t.NoDisplay = this.cbNoDisplay.Checked;

                //Save the parameters
                tConfig.SetConfig(t);

                //Set the SpectrumBase
                this._specBase = new Acquisition.Spectrum2D(t);
            }
            else if (this.rbBackground.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Background> bgConfig = new ConfigHelper<Config.Background>(ConfigLocation.AllUsers);
                Config.Background bg = bgConfig.GetWriteableConfig();

                //Set the properties
                bg.AcquisitionDelay = Convert.ToDouble(this.ntbDelay.Text);
                bg.EndWavelength = Convert.ToUInt32(this.ntbEndWavelength.Text);
                bg.SamplesToAverage = Convert.ToUInt32(this.ntbSamplesToAvg.Text);
                bg.StartWavelength = Convert.ToUInt32(this.ntbStartWavelength.Text);
                bg.WavelengthIncrement = Convert.ToUInt32(this.ntbIncrement.Text);
                bg.NoDisplay = this.cbNoDisplay.Checked;

                //Save the parameters
                bgConfig.SetConfig(bg);

                //Set the SpectrumBase
                this._specBase = new Acquisition.Spectrum2D(bg);
            }
            else if (this.rb3DPosition.Checked == true)
            {
                //Get the config
                ConfigHelper<Config.Position> posConfig = new ConfigHelper<Config.Position>(ConfigLocation.AllUsers);
                Config.Position p = posConfig.GetWriteableConfig();

                //Set the properties
                p.AcquisitionDelay = Convert.ToDouble(this.ntbDelay.Text);
                p.SamplesToAverage = Convert.ToUInt32(this.ntbSamplesToAvg.Text);
                p.StepSize = Convert.ToUInt32(this.ntbStepSize.Text);
                p.XPosStart = Convert.ToUInt32(this.spinXPosStart.Value);
                p.XPosEnd = Convert.ToUInt32(this.spinXPosEnd.Value);
                p.YPosStart = Convert.ToUInt32(this.spinYPosStart.Value);
                p.YPosEnd = Convert.ToUInt32(this.spinYPosEnd.Value);
                p.NoDisplay = this.cbNoDisplay.Checked;

                //Save the parameters
                posConfig.SetConfig(p);

                //Set the SpectrumBase
                this._specBase = new Acquisition.Spectrum3D(p);
            }

            //Save the file path
            this._specBase.Config.FilePath = this.txtFileName.Text;

            //Close the Dialog
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion


        #region Control Events
        /// <summary>
        /// spinXAxis_ValueChanged moves the X-Axis mirror to a
        /// specified position.
        /// </summary>
        /// <param name="sender">The UpDown control whose value changed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void spinXAxis_ValueChanged(object sender, EventArgs e)
        {
            //Cast the sender back to a NumericUpDown Control
            NumericUpDown ctrl = sender as NumericUpDown;

            //If the Start is less than the End, popup a message
            //and set the two equal to each other.  Otherwise, set
            //the value on the Mirror.
            if (this.spinXPosEnd.Value < this.spinXPosStart.Value)
            {
                //Popup a message
                MessageBox.Show(this, "X-Axis End can not be less than X-Axis Start.", 
                    "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Set the changed Value back to the unchanged Value
                if (ctrl.Equals(this.spinXPosStart) == true)
                    this.spinXPosStart.Value = this.spinXPosEnd.Value;
                else
                    this.spinXPosEnd.Value = this.spinXPosStart.Value;
            }
            else
                this.SetMirror(RI.DAQ.DaqBase.Axis.X, Convert.ToUInt32(ctrl.Value));
        }


        /// <summary>
        /// spinYAxis_ValueChanged moves the Y-Axis mirror to a
        /// specified position.
        /// </summary>
        /// <param name="sender">The UpDown control whose value changed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void spinYAxis_ValueChanged(object sender, EventArgs e)
        {
            //Cast the sender back to a NumericUpDown Control
            NumericUpDown ctrl = sender as NumericUpDown;

            //If the End is greater than the Start, popup a message
            //and set the two equal to each other.  Otherwise, set 
            //the value on the Mirror.
            if (this.spinYPosEnd.Value > this.spinYPosStart.Value)
            {
                //Popup a message
                MessageBox.Show(this, "Y-Axis End can not be greater than Y-Axis Start.",
                    "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Set the changed Value back to the unchanged Value
                if (ctrl.Equals(this.spinYPosStart) == true)
                    this.spinYPosStart.Value = this.spinYPosEnd.Value;
                else
                    this.spinYPosEnd.Value = this.spinYPosStart.Value;
            }
            else
                this.SetMirror(RI.DAQ.DaqBase.Axis.Y, Convert.ToUInt32(ctrl.Value));
        }
        
        
        /// <summary>
        /// ntbStepSize_TextChanged sets the increment on the UpDown 
        /// Controls and modifies the Rows and Column measurements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntbStepSize_TextChanged(object sender, EventArgs e)
        {
            //Set the Increment on the UpDown controls
            this.spinXPosStart.Increment = Convert.ToDecimal(this.ntbStepSize.Text);
            this.spinXPosEnd.Increment = Convert.ToDecimal(this.ntbStepSize.Text);
            this.spinYPosStart.Increment = Convert.ToDecimal(this.ntbStepSize.Text);
            this.spinYPosEnd.Increment = Convert.ToDecimal(this.ntbStepSize.Text);

            //Set the Rows / Columns
            this.CalculateRowsColumns();
        }


        /// <summary>
        /// cbLIAType_SelectedIndexChanged enables or disables the
        /// Sensitivity ComboBoxes based on the value of the 
        /// selected item.
        /// </summary>
        /// <param name="sender">The ComboBox whose Index changed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void cbLIAType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Declare a variable to set the enabled flag -- disable
            //the ComboBoxes by default
            Boolean enable = false;

            //Figure out if the ComboBoxes should be enabled
            //or disabled based on the value of the SelectedItem
            if (this.cbLIAType.SelectedItem.ToString() == RI.Enums.LockInType.Hardware.ToString())
                enable = true;

            //Set the value on the ComboBoxes
            this.cbSensitivity.Enabled = enable;
            this.cbSensitivityUnits.Enabled = enable;
        }
        
        
        /// <summary>
        /// btnFileSave_Click opens the File Save Dialog and 
        /// allows the user to enter a file name.
        /// </summary>
        /// <param name="sender">The Button that was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        private void btnFileSave_Click(object sender, EventArgs e)
        {
            //Create a File Save Dialog
            Dialogs.SaveFileDialog sfd = new Dialogs.SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;
            sfd.OverwritePrompt = true;
            sfd.Title = "Enter a Spectrum File Name";

            //If the file name has been defined already, put it
            //in the SaveFileDialog
            if (this.txtFileName.Text != String.Empty)
                sfd.FileName = Path.GetFileName(this.txtFileName.Text);

            //Set the Filter based on the type of Spectrum
            if (this.rbTime.Checked == true || this.rbWavelength.Checked == true)
                sfd.Filter = SpectrumExtension.ToString(SpectrumType.TWOD | SpectrumType.RAW);
            else if (this.rbBackground.Checked == true)
            {
                //Create a file name if one hasn't been put in yet
                if (this.txtFileName.Text == String.Empty)
                    sfd.FileName = DateTime.Now.ToString("MM_dd_yyyy-HH_mm");

                //Set the Filter to BKG
                sfd.Filter = SpectrumExtension.ToString(SpectrumType.TWOD | SpectrumType.BKG);
            }
            else if (rb3DPosition.Checked == true || this.rbLEGO.Checked == true)
                sfd.Filter = SpectrumExtension.ToString(SpectrumType.TAB3D, SpectrumType.THREED | SpectrumType.RAW);

            //Open the Dialog 
            if (sfd.ShowDialog() == DialogResult.OK)
                this.txtFileName.Text = sfd.FileName;
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
        /// SaveDirectory allows the Save Directory to be 
        /// set on the New Spectrum Wizard.
        /// </summary>
        public String SaveDirectory
        {
            get { return this._saveDir; }
            set { this._saveDir = value; }
        }


        /// <summary>
        /// SpectrumBase returns the filled SpectrumBase object
        /// used to perform the actual data acquisition.
        /// </summary>
        public Acquisition.SpectrumBase SpectrumBase
        {
            get { return this._specBase; }
        }


        /// <summary>
        /// AddToActive returns the value of the CheckBox to 
        /// add the new Spectrum to the existing window.
        /// </summary>
        public Boolean AddToActive
        {
            get { return this.cbAddToActive.Checked; }
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// SetupPageSequence puts together the appropriate 
        /// pages based on the context of the Wizard.
        /// </summary>
        private void SetupPageSequence()
        {
            //Create a List to hold the pages that will be
            //included in the Wizard
            List<Divelements.WizardFramework.WizardPageBase> pages = new List<Divelements.WizardFramework.WizardPageBase>();

            //Add the Spectrum Type page -- it is always there
            pages.Add(this.pageSpectrumType);

            //If the spectrum is the Position type, add it
            if (this.rb3DPosition.Checked == true || this.rbLEGO.Checked == true)
                pages.Add(this.pagePositionSetup);

            //Add the Common spectrum page
            pages.Add(this.pageSpectrumSetup);

            //Add the Hardware setup page
            pages.Add(this.pageHardwareSetup);

            //Add the Software Lock-In page if necessary
            if (this.cbLIAType.SelectedItem != null &&
                this.cbLIAType.SelectedItem.ToString() == RI.Enums.LockInType.Software.ToString())
                pages.Add(this.pageSoftwareLIASetup);

            //Add the File Name page
            pages.Add(this.pageFileName);

            //Add the Complete page
            pages.Add(this.pageComplete);

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
        /// SetMirror encapsulates the movement of a 3-D Mirror
        /// and the subsequent calculation of the Rows and Columns.
        /// </summary>
        /// <param name="axis">The Axis that is to be moved</param>
        /// <param name="offset">The offset by which to move the mirror</param>
        private void SetMirror(RI.DAQ.DaqBase.Axis axis, UInt32 offset)
        {
            //If the DaqBase hasn't been set up, create it
            if (this._db == null)
                this._db = RI.DAQ.DaqFactory.ConfiguredCard;

            //Move the mirror
            this._db.MoveMirror(axis, offset);

            //Calculate the rows / columns
            this.CalculateRowsColumns();
        }


        /// <summary>
        /// CalculateRowsColumns takes the increment value of the
        /// UpDown controls and calculates the rows and columns 
        /// that result from the start and end positions.
        /// </summary>
        private void CalculateRowsColumns()
        {
            //Get the value of the Step Size
            Int32 step = 0;
            try
            {
                step = Convert.ToInt32(this.ntbStepSize.Text);
            }
            catch { }

            //If the value is not 0, do the calculation
            if (step != 0)
            {
                //Calculate the Columns - start and end inclusive
                Int32 columns = Convert.ToInt32((this.spinXPosEnd.Value - this.spinXPosStart.Value) / step) + 1;

                //Calculate the Rows - start and end inclusive
                Int32 rows = Convert.ToInt32((this.spinYPosStart.Value - this.spinYPosEnd.Value) / step) + 1;

                //Set the values in the Labels
                this.lblCols.Text = columns.ToString();
                this.lblRows.Text = rows.ToString();
            }
        }


        /// <summary>
        /// FormatLIASensitivity sets the output of the Lock-In
        /// Amp sensitivity based on the selected value of the 
        /// Lock-In Type.
        /// </summary>
        /// <returns></returns>
        private String FormatLIASensitivity()
        {
            //Declare a variable to return -- default to Hardware
            //Lock-In Type
            String rtn = String.Format("{0} {1}", this.cbSensitivity.SelectedItem, this.cbSensitivityUnits.SelectedItem);

            //If the Type is something other than Hardware, set it
            if (this.cbLIAType.SelectedItem.ToString() == RI.Enums.LockInType.None.ToString())
                rtn = "None";
            else if (this.cbLIAType.SelectedItem.ToString() == RI.Enums.LockInType.Software.ToString())
                rtn = "Automatic";

            //Return the result
            return rtn;
        }


        /// <summary>
        /// ShowFrequency queries the Lock-In Amp for the Frequency
        /// of the input signal and sets the value on the control.
        /// </summary>
        /// <param name="state">The state of the Thread -- always NULL</param>
        private void ShowFrequency(Object state)
        {
            //Get and scale the voltage value
            String freq = RI.LockIn.Amplifier.Current.InputFrequency.ToString("f2");

            //If the Label can't be accessed from this thread,
            //Invoke the Label to update
            if (this.lblFrequency.InvokeRequired == true)
                this.lblFrequency.Invoke(new UpdateLabelDelegate(this.UpdateLabel), this.lblFrequency, freq);
        }


        /// <summary>
        /// ShowLIAValue queries the Lock-In Amp for the Value
        /// of the modulated signal and sets the value on the control.
        /// </summary>
        /// <param name="state">The state of the Thread -- always NULL</param>
        private void ShowLIAValue(Object state)
        {
            //Get and scale the voltage value
            Double voltage = this._db.ReadVoltage(Convert.ToInt32(this.ntbSamplesToAvg.Text));
            String scaled = this._pcScale.ScaleVoltage(voltage).ToString("E2");

            //If the Label can't be accessed from this thread,
            //Invoke the Label to update
            if (this.lblLIAValue.InvokeRequired == true)
                this.lblLIAValue.Invoke(new UpdateLabelDelegate(this.UpdateLabel), this.lblLIAValue, scaled);
        }


        /// <summary>
        /// UpdateLabelDelegate is the function declaration for
        /// the Label Text updating from the Timers.
        /// </summary>
        /// <param name="label">The Label to update</param>
        /// <param name="value">The value with with to update it</param>
        private delegate void UpdateLabelDelegate(Label label, String value);


        /// <summary>
        /// UpdateLabel sets the Label.Text with value.
        /// </summary>
        /// <param name="label">The Label on which to set text</param>
        /// <param name="value">The value to set on the Label</param>
        private void UpdateLabel(Label label, String value)
        {
            //Set the value
            label.Text = value;
        }

        
        /// <summary>
        /// Get2DTimeRemainingString get the time that would
        /// be taken by a scan and formats it into a String.
        /// </summary>
        /// <returns>String of the duration of a scan</returns>
        private String Get2DTimeRemainingString()
        {
            //Get the config
            ConfigHelper<Config.Wavelength> wlConfig = new ConfigHelper<Config.Wavelength>(ConfigLocation.AllUsers);
            Config.Wavelength wl = wlConfig.GetWriteableConfig();

            //Set the properties
            wl.AcquisitionDelay = Convert.ToDouble(this.ntbDelay.Text);
            wl.EndWavelength = Convert.ToUInt32(this.ntbEndWavelength.Text);
            wl.SamplesToAverage = Convert.ToUInt32(this.ntbSamplesToAvg.Text);
            wl.StartWavelength = Convert.ToUInt32(this.ntbStartWavelength.Text);
            wl.WavelengthIncrement = Convert.ToUInt32(this.ntbIncrement.Text);

            //Create a SpectrumBase to get the time remaining
            Acquisition.Spectrum2D temp = new Acquisition.Spectrum2D(wl);

            //Return the result
            return RDH2.Utilities.Format.TimeSpanFormatter.ToHMSString(temp.TimeRemaining);
        }


        /// <summary>
        /// Get3DTimeRemainingString get the time that would
        /// be taken by a scan and formats it into a String.
        /// </summary>
        /// <returns>String of the duration of a scan</returns>
        private String Get3DTimeRemainingString()
        {
            //Get the config
            ConfigHelper<Config.Position> pConfig = new ConfigHelper<Config.Position>(ConfigLocation.AllUsers);
            Config.Position p = pConfig.GetWriteableConfig();

            //Set the properties
            p.AcquisitionDelay = Convert.ToDouble(this.ntbDelay.Text);
            p.SamplesToAverage = Convert.ToUInt32(this.ntbSamplesToAvg.Text);
            p.XPosStart = Convert.ToUInt32(this.spinXPosStart.Value);
            p.XPosEnd = Convert.ToUInt32(this.spinXPosEnd.Value);
            p.YPosStart = Convert.ToUInt32(this.spinYPosStart.Value);
            p.YPosEnd = Convert.ToUInt32(this.spinYPosEnd.Value);

            //Create a SpectrumBase to get the time remaining
            Acquisition.Spectrum3D temp = new Acquisition.Spectrum3D(p);

            //Return the result
            return RDH2.Utilities.Format.TimeSpanFormatter.ToHMSString(temp.TimeRemaining);
        }
        #endregion
    }
}
