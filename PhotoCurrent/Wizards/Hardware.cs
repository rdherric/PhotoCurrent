using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using RI = RDH2.Instrumentation;
using RDH2.Utilities.Configuration;

namespace PhotoCurrent.Wizards
{
    public partial class Hardware : Form
    {
        #region Member variables
        private HardwareType _type = HardwareType.All;
        private const String _noneDAQ = "<none>";
        private List<RI.DAQ.DaqBase> _daqs = new List<RI.DAQ.DaqBase>();
        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor for the Potentiostat Wizard
        /// </summary>
        public Hardware()
        {
            //Let the System do some work
            InitializeComponent();

            //Subscribe to Wizard Events
            this.pageWelcome.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageWelcome_BeforeMoveNext);
            this.pagePurpose.BeforeDisplay += new EventHandler(pagePurpose_BeforeDisplay);
            this.pagePurpose.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pagePurpose_BeforeMoveNext);
            this.pageDAQType.Enter += new EventHandler(pageDAQType_Enter);
            this.pageDAQType.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageDAQType_BeforeMoveNext);
            this.pageDAQCommon.BeforeDisplay += new EventHandler(pageDAQCommon_BeforeDisplay);
            this.pageDAQCommon.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageDAQCommon_BeforeMoveNext);
            this.pageCurrentInputConfig.BeforeDisplay += new EventHandler(pageCurrentInputConfig_BeforeDisplay);
            this.pageCurrentInputConfig.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageCurrentInputConfig_BeforeMoveNext);
            this.pageMCConfig.BeforeDisplay += new EventHandler(pageMCConfig_BeforeDisplay);
            this.pageMCConfig.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pageMCConfig_BeforeMoveNext);
            this.pageMirrorsConfig.BeforeDisplay += new EventHandler(pageMirrorsConfig_BeforeDisplay);
            this.pagePotStatConfig.BeforeDisplay += new EventHandler(pagePotStatConfig_BeforeDisplay);
            this.pagePotStatConfig.BeforeMoveNext += new Divelements.WizardFramework.WizardPageEventHandler(pagePotStatConfig_BeforeMoveNext);
            this.pageComplete.BeforeDisplay += new EventHandler(pageComplete_BeforeDisplay);
            this.hardwareWizard.Finish += new EventHandler(hardwareWizard_Finish);
        }
        #endregion


        #region Wizard Events
        /// <summary>
        /// pageWelcome_BeforeMoveNext sets up the pages that will
        /// be shown in the Wizard.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageWelcome_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Setup the Pages as they should be based on the Type
            this.SetupPageSequence();
        }


        /// <summary>
        /// pagePurpose_BeforeDisplay determines what pages to show
        /// in the rest of the Wizard.
        /// </summary>
        /// <param name="sender">The Page that is going to be displayed</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pagePurpose_BeforeDisplay(object sender, EventArgs e)
        {
            //Setup the CheckBoxes
            this.cb2DAnalysis.Checked = this.TypeContained(RI.Enums.AcquisitionType.TwoD);
            this.cb3DAnalysis.Checked = this.TypeContained(RI.Enums.AcquisitionType.ThreeD);
            this.cbLEGOAnalysis.Checked = this.TypeContained(RI.Enums.AcquisitionType.LEGO);
        }


        /// <summary>
        /// pagePurpose_BeforeMoveNext ensures that at least one kind
        /// of Analysis has been selected prior to moving to the next
        /// Page.
        /// </summary>
        /// <param name="sender">The Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pagePurpose_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //If none of the checkboxes have been checked, popup
            //an error message and cancel the move
            if (this.cb2DAnalysis.Checked == false && this.cb3DAnalysis.Checked == false && this.cbLEGOAnalysis.Checked == false)
            {
                //Popup an error message
                MessageBox.Show(this, "Please select at least one type of Analysis to Configure.", "Wizard Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Cancel the move
                e.Cancel = true;
                return;
            }

            //Persist the Types to the config file
            this.PersistTypes();
        }

        
        /// <summary>
        /// pageDAQType_Enter does the actual work of detecting the 
        /// available DAQ cards and displaying the results in the
        /// Drop-Down list.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageDAQType_Enter(object sender, EventArgs e)
        {
            //If the ComboBox has Items in it, ignore this event
            if (this.cbDAQCards.Items.Count > 0)
                return;

            //Show a wait cursor
            this.Cursor = Cursors.WaitCursor;
            
            //Get all of the installed cards on the computer
            this._daqs = RI.DAQ.DaqFactory.InstalledCards;

            //Iterate through the installed cards and put the names
            //in the Drop-Down list
            foreach (RI.DAQ.DaqBase db in this._daqs)
                this.cbDAQCards.Items.Add(db.ToString());

            //If there are no DAQ cards to show, add the 
            //<none> Type in the Drop-Down list and select it
            if (this.cbDAQCards.Items.Count == 0)
                this.cbDAQCards.Items.Add(Hardware._noneDAQ);

            //Get the ConfiguredCard and select it in the list.  
            //Otherwise, select the first card in the Drop-Down list.
            RI.DAQ.DaqBase configured = RI.DAQ.DaqFactory.ConfiguredCard;

            if (configured != null && this.cbDAQCards.Items.Contains(configured.ToString()) == true)
                this.cbDAQCards.SelectedItem = configured.ToString();
            else
                this.cbDAQCards.SelectedIndex = 0;

            //Kill off the WaitCursor
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// pageDAQType_BeforeMoveNext determines whether further setup of 
        /// the DAQ card can be performed and redirects appropriately.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageDAQType_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Setup the page Sequence
            this.SetupPageSequence();
        }


        /// <summary>
        /// pageDAQCommon_PageSetup retrieves the Configuration and
        /// sets up the controls in the Page
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageDAQCommon_BeforeDisplay(object sender, EventArgs e)
        {
            //Get a config object
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetConfig();

            //Set the values in the controls
            this.ntbSamplingRate.Text = di.SamplingRate.ToString();
        }


        /// <summary>
        /// pageDAQCommon_PageLeave checks to make sure that there
        /// are values in the controls and then persists them to
        /// the app.config.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageDAQCommon_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Declare variables to hold error info
            String errText = String.Empty;
            Control errCtrl = null;

            //Check to make sure there are values in the controls
            if (this.ntbSamplingRate.Text == String.Empty)
            {
                errText = "Please enter a value for Sampling Rate.";
                errCtrl = this.ntbSamplingRate;
            }

            //If an error occurred, show a message and return
            if (errText != String.Empty && errCtrl != null)
            {
                MessageBox.Show(this, errText, "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errCtrl.Focus();
                e.Cancel = true;
                return;
            }
        }


        /// <summary>
        /// pageCurrentInputConfig_PageSetup configures all of the 
        /// properties used to read Photocurrent from the instrument.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageCurrentInputConfig_BeforeDisplay(object sender, EventArgs e)
        {
            //Get the DAQ that was configured
            RI.DAQ.DaqBase db = RI.DAQ.DaqFactory.ConfiguredCard;

            if (db == null)
                db = this._daqs[this.cbDAQCards.SelectedIndex];

            //Clear all of the DDLs
            this.cbAIPort.Items.Clear();
            this.cbFreqInPort.Items.Clear();

            //Fill the DDLs from the DaqBase object
            this.cbAIPort.Items.AddRange(db.AnalogInPorts);
            this.cbFreqInPort.Items.AddRange(db.CounterPorts);

            //Set up Defaults for the values in the DDLs
            String aIn = RI.DAQ.MCC.DefaultAIn;
            String ctrIn = RI.DAQ.MCC.DefaultCounterIn;

            if (db.Type == RI.Enums.DaqType.NI)
            {
                aIn = RI.DAQ.NI.DefaultAIn;
                ctrIn = RI.DAQ.NI.DefaultCounterIn;
            }

            //If there is a configured card, use the values if
            //they can be found
            if (RI.DAQ.DaqFactory.ConfiguredCard != null)
            {
                //Get the Config object
                ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
                RI.Config.DaqInterface di = config.GetConfig();

                //Try to set the values with the configured Card
                if (this.cbAIPort.Items.Contains(di.VoltageInPort) == true)
                    aIn = di.VoltageInPort;

                if (this.cbFreqInPort.Items.Contains(di.InputCounterInPort) == true)
                    ctrIn = di.InputCounterInPort;
            }

            //Finally, set the values in the DDLs
            this.cbAIPort.SelectedItem = aIn;
            this.cbFreqInPort.SelectedItem = ctrIn;

            //Get the LockInAmp configuration
            ConfigHelper<RI.Config.LockInAmp> liaConfig = new ConfigHelper<RI.Config.LockInAmp>(ConfigLocation.AllUsers);
            RI.Config.LockInAmp lia = liaConfig.GetConfig();

            //Set the properties on the controls
            this.ntbLIAFullScale.Text = lia.FullScale.ToString();
        }


        /// <summary>
        /// pageCurrentInputConfig_BeforeMoveNext makes sure that there
        /// are values in the controls.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageCurrentInputConfig_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Make sure there is a value in the TextBox
            if (this.ntbLIAFullScale.Text == String.Empty)
            {
                MessageBox.Show(this, "Please enter a value for the Lock-In Amplifier Full Scale", "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ntbLIAFullScale.Focus();
                e.Cancel = true;
                return;
            }

        }


        /// <summary>
        /// pageMCConfig_PageSetup retrieves the configuration 
        /// and sets up the controls in the Page.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageMCConfig_BeforeDisplay(object sender, EventArgs e)
        {
            //Get the DAQ that was configured
            //Get the DAQ that was configured
            RI.DAQ.DaqBase db = RI.DAQ.DaqFactory.ConfiguredCard;

            if (db == null)
                db = this._daqs[this.cbDAQCards.SelectedIndex];

            //Clear all of the DDLs
            this.cbCounter.Items.Clear();
            this.cbScanDown.Items.Clear();
            this.cbScanUp.Items.Clear();
            this.cbStepperPort.Items.Clear();

            //Fill the DDLs from the DaqBase object
            this.cbStepperPort.Items.AddRange(db.DigitalOutBytes);
            this.cbCounter.Items.AddRange(db.CounterPorts);
            this.cbScanUp.Items.AddRange(db.DigitalOutPorts);
            this.cbScanDown.Items.AddRange(db.DigitalOutPorts);

            //Enable all of the DDLs
            this.cbCounter.Enabled = true;
            this.cbScanDown.Enabled = true;
            this.cbScanUp.Enabled = true;
            this.cbStepperPort.Enabled = true;

            //Disable DDLs based on the Type
            if (db.Type == RI.Enums.DaqType.MCC)
            {
                this.cbCounter.Enabled = false;
                this.cbScanDown.Enabled = false;
                this.cbScanUp.Enabled = false;
            }
            else
                this.cbStepperPort.Enabled = false;

            //Set up Defaults for the values in the DDLs
            String stepOut = RI.DAQ.MCC.DefaultStepperOut;
            String ctrOut = String.Empty;
            String scanDown = String.Empty;
            String scanUp = String.Empty;

            if (db.Type == RI.Enums.DaqType.NI)
            {
                stepOut = String.Empty;
                ctrOut = RI.DAQ.NI.DefaultCounterOut;
                scanDown = RI.DAQ.NI.DefaultScanDownOut;
                scanUp = RI.DAQ.NI.DefaultScanUpOut;
            }

            //If there is a configured card, use the values if
            //they can be found
            if (RI.DAQ.DaqFactory.ConfiguredCard != null)
            {
                //Get the Config object
                ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
                RI.Config.DaqInterface di = config.GetConfig();

                //Try to set the values with the configured Card
                if (this.cbStepperPort.Items.Contains(di.StepperOutPort) == true)
                    stepOut = di.StepperOutPort;

                if (this.cbCounter.Items.Contains(di.ScanCounterPort) == true)
                    ctrOut = di.ScanCounterPort;

                if (this.cbScanUp.Items.Contains(di.ScanUpPort) == true)
                    scanUp = di.ScanUpPort;

                if (this.cbScanDown.Items.Contains(di.ScanDownPort) == true)
                    scanDown = di.ScanDownPort;
            }

            //Finally, set the values in the DDLs
            this.cbStepperPort.SelectedItem = stepOut;
            this.cbCounter.SelectedItem = ctrOut;
            this.cbScanUp.SelectedItem = scanUp;
            this.cbScanDown.SelectedItem = scanDown;

            //Get the Monochromator configuration
            ConfigHelper<RI.Config.Monochromator> mcConfig = new ConfigHelper<RI.Config.Monochromator>(ConfigLocation.AllUsers);
            RI.Config.Monochromator mc = mcConfig.GetConfig();

            //Set the properties on the controls
            this.ntbStepsPerNm.Text = mc.StepsPerNm.ToString();
        }


        /// <summary>
        /// pageMCConfig_PageLeave persists the values in the
        /// Controls to the app.config file.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageMCConfig_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Make sure there is a value in the TextBox
            if (this.ntbStepsPerNm.Text == String.Empty)
            {
                MessageBox.Show(this, "Please enter a value for the Monochromator Steps per nm", "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ntbStepsPerNm.Focus();
                e.Cancel = true;
                return;
            }
        }


        /// <summary>
        /// pageMirrorsConfig_BeforeDisplay sets up the Drop-Down Lists
        /// with the appropriate values.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageMirrorsConfig_BeforeDisplay(object sender, EventArgs e)
        {
            //Get the DAQ that was configured
            RI.DAQ.DaqBase db = RI.DAQ.DaqFactory.ConfiguredCard;

            if (db == null)
                db = this._daqs[this.cbDAQCards.SelectedIndex];

            //Clear all of the DDLs
            this.cbXMirrorPort.Items.Clear();
            this.cbYMirrorPort.Items.Clear();

            //Fill the DDLs from the DaqBase object
            this.cbXMirrorPort.Items.AddRange(db.AnalogOutPorts);
            this.cbYMirrorPort.Items.AddRange(db.AnalogOutPorts);

            //Set up Defaults for the values in the DDLs
            String xOut = RI.DAQ.MCC.DefaultXMirrorOut;
            String yOut = RI.DAQ.MCC.DefaultYMirrorOut;

            if (db.Type == RI.Enums.DaqType.NI)
            {
                xOut = RI.DAQ.NI.DefaultXMirrorOut;
                yOut = RI.DAQ.NI.DefaultYMirrorOut;
            }

            //If there is a configured card, use the values if
            //they can be found
            if (RI.DAQ.DaqFactory.ConfiguredCard != null)
            {
                //Get the Config object
                ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
                RI.Config.DaqInterface di = config.GetConfig();

                //Try to set the values with the configured Card
                if (this.cbXMirrorPort.Items.Contains(di.XOutPort) == true)
                    xOut = di.XOutPort;

                if (this.cbYMirrorPort.Items.Contains(di.YOutPort) == true)
                    yOut = di.YOutPort;
            }

            //Finally, set the values in the DDLs
            this.cbXMirrorPort.SelectedItem = xOut;
            this.cbYMirrorPort.SelectedItem = yOut;
        }


        /// <summary>
        /// pagePotStatConfig_PageSetup retrieves the configuration 
        /// and sets up the controls in the Page.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being Set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pagePotStatConfig_BeforeDisplay(object sender, EventArgs e)
        {
            //Get the Potentiostat configuration
            ConfigHelper<RI.Config.Potentiostat> config = new ConfigHelper<RI.Config.Potentiostat>(ConfigLocation.AllUsers);
            RI.Config.Potentiostat potstat = config.GetConfig();

            //Set the properties on the controls
            this.ntbPotStatFullScale.Text = potstat.FullScale.ToString();
        }


        /// <summary>
        /// pagePotStatConfig_PageLeave persists the values in the
        /// Controls to the app.config file.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being left</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pagePotStatConfig_BeforeMoveNext(object sender, CancelEventArgs e)
        {
            //Make sure there is a value in the TextBox
            if (this.ntbPotStatFullScale.Text == String.Empty)
            {
                MessageBox.Show(this, "Please enter a value for the Potentiostat Full Scale", "Wizard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ntbPotStatFullScale.Focus();
                e.Cancel = true;
                return;
            }
        }


        /// <summary>
        /// pageComplete_PageSetup sets up the Summary message.
        /// </summary>
        /// <param name="sender">The Wizard Page that is being set up</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void pageComplete_BeforeDisplay(object sender, EventArgs e)
        {
            //Set up the messages based on the state of the Wizard
            String pageText = "Hardware Configuration Complete";
            String finishText = "The following Hardware device configurations have been saved:\n";

            //If the user clicked Do Not Configure, just show a cancel message
            if (this.cbDoNotConfig.Checked == true)
            {
                //Set the Do Not Config text
                pageText = "Hardware Configuration Canceled";
                finishText = "You have chosen not to configure hardware at this time.  To return to the configuration Wizard, use the 'Configure' menu.";
            }
            else
            {
                //If the DAQ was configured, add it
                if ((this._type & HardwareType.DaqCard) > 0 && this.DAQSelected == true)
                    finishText += "Data Acquisition Card\n";

                //If the Current Input was configured, add it
                if ((this._type & HardwareType.CurrentInput) > 0 && this.DAQSelected == true)
                    finishText += "Photocurrent Input\n";

                //If the M/C was configured, add it
                if ((this._type & HardwareType.Monochromator) > 0 && this.DAQSelected == true && this.TypeContained(RI.Enums.AcquisitionType.TwoD) == true)
                    finishText += "Monochromator\n";

                //If the Mirrors were configured, add it
                if ((this._type & HardwareType.Mirrors) > 0 && this.DAQSelected == true && this.TypeContained(RI.Enums.AcquisitionType.ThreeD) == true)
                    finishText += "Mirrors\n";

                //If the PotStat was configured, add it
                if ((this._type & HardwareType.Potentiostat) > 0)
                    finishText += "Potentiostat\n";
            }

            //Put the string in the Finish Page
            this.pageComplete.Text = pageText;
            this.pageComplete.FinishText = finishText;
        }

        
        /// <summary>
        /// hardwareWizard_Finish closes the Wizard.
        /// </summary>
        /// <param name="sender">The Wizard whose Finish button was clicked</param>
        /// <param name="e">The EventArgs sent by the System</param>
        void hardwareWizard_Finish(object sender, EventArgs e)
        {
            //Save all of the properties in the Config file
            this.PersistGeneral();

            //If the user configured a DAQ, save the values
            if (this.DAQSelected == true)
            {
                this.PersistDAQ();
                this.PersistCurrentInput();
                this.PersistMonochromator();
                this.PersistMirrors();
            }

            //Persist the PotStat values if any were set
            if (this.cbDoNotConfig.Checked == false)
                this.PersistPotStat();

            //Close the Wizard
            this.Close();
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// HardwareType is used to setup the pages that are
        /// to be shown when the Wizard is opened.
        /// </summary>
        public HardwareType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
        #endregion


        #region Private Properties
        /// <summary>
        /// DAQSelected determines if any DAQ cards have been set up.
        /// </summary>
        private Boolean DAQSelected
        {
            get
            {
                //Declare a variable to return
                Boolean rtn = false;

                //If there is a configured card or one was selected, 
                //return true
                if (RI.DAQ.DaqFactory.ConfiguredCard != null ||
                    (this.cbDAQCards.SelectedItem != null && this.cbDAQCards.SelectedItem.ToString() != Hardware._noneDAQ))
                    rtn = true;

                //Return the result
                return rtn;
            }
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

            //Add the Welcome and Purpose pages, as they are always there
            pages.Add(this.pageWelcome);

            //If the user doesn't want to configure, setup 
            //the Do Not Configure Page and nothing else
            if (this.cbDoNotConfig.Checked == false)
            {
                //Add the Purpose Page if all of the stuff is being
                //configured -- this will allow the user to set 
                //2D or 3-D
                if (this._type == HardwareType.All)
                    pages.Add(this.pagePurpose);

                //If the DAQ card is being configured, add the pages
                if ((this._type & HardwareType.DaqCard) > 0)
                {
                    //Add the DAQ Type page
                    pages.Add(this.pageDAQType);

                    //If the DAQ Type has been chosen, add the DAQ Common page
                    if (this.DAQSelected == true)
                        pages.Add(this.pageDAQCommon);
                }

                //If the Photocurrent Input is being configured, add the page
                if (this.DAQSelected == true && (this._type & HardwareType.CurrentInput) > 0)
                    pages.Add(this.pageCurrentInputConfig);

                //If the Monochromator is being configured, add the page
                if ((this._type & HardwareType.Monochromator) > 0 && this.DAQSelected == true && this.TypeContained(RI.Enums.AcquisitionType.TwoD) == true)
                    pages.Add(this.pageMCConfig);

                //If the Mirrors are being configured, add the page
                if ((this._type & HardwareType.Mirrors) > 0 && this.DAQSelected == true && this.TypeContained(RI.Enums.AcquisitionType.ThreeD) == true)
                    pages.Add(this.pageMirrorsConfig);

                //If the Potentiostat is being configured, add the page
                if ((this._type & HardwareType.Potentiostat) > 0)
                    pages.Add(this.pagePotStatConfig);
            }

            //Finally, add the Complete page
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


        private void PersistTypes()
        {
            //Get a writeable config
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetWriteableConfig();

            //Create a list of AcquisitionTypes
            List<RI.Enums.AcquisitionType> types = new List<RI.Enums.AcquisitionType>();

            //Set up the list
            if (this.cb2DAnalysis.Checked == true)
                types.Add(RI.Enums.AcquisitionType.TwoD);

            if (this.cb3DAnalysis.Checked == true)
                types.Add(RI.Enums.AcquisitionType.ThreeD);

            if (this.cbLEGOAnalysis.Checked == true)
                types.Add(RI.Enums.AcquisitionType.LEGO);

            //Set the values
            di.AcquisitionTypes = types.ToArray();

            //Set the config
            config.SetConfig(di);
        }


        /// <summary>
        /// PersistGeneral persists the General choices of the user
        /// in the Config file.
        /// </summary>
        private void PersistGeneral()
        {
            //Get the General Config properties
            ConfigHelper<RI.Config.General> config = new ConfigHelper<RI.Config.General>(ConfigLocation.AllUsers);
            RI.Config.General gen = config.GetWriteableConfig();

            //Determine if the Configuration has been set 
            Boolean isConfigured = true;
            Boolean doNotConfigure = false;

            if (this.cbDoNotConfig.Checked == true)
            {
                isConfigured = false;
                doNotConfigure = true;
            }

            //Set the configuration
            gen.DoNotConfigure = doNotConfigure;
            gen.IsConfigured = isConfigured;
            config.SetConfig(gen);
        }


        /// <summary>
        /// PersistDAQ persists the DAQ choices of the user
        /// in the Config file.
        /// </summary>
        private void PersistDAQ()
        {
            //If the DAQ isn't being configured, just return
            if ((this.Type & HardwareType.DaqCard) == 0)
                return;

            //Get a writeable config
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetWriteableConfig();

            //Set the values
            di.BoardName = this._daqs[this.cbDAQCards.SelectedIndex].BoardName;
            di.Type = this._daqs[this.cbDAQCards.SelectedIndex].Type;
            di.SamplingRate = Convert.ToUInt32(this.ntbSamplingRate.Text);

            //Save the config
            config.SetConfig(di);
        }


        /// <summary>
        /// PersistCurrentInput persists the Current choices of the user
        /// in the Config file.
        /// </summary>
        private void PersistCurrentInput()
        {
            //If the Current Input isn't being configured, just return
            if ((this.Type & HardwareType.CurrentInput) == 0)
                return;

            //Get a writeable DAQ config
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetWriteableConfig();

            //Set the properties
            if (this.cbAIPort.SelectedItem != null)
                di.VoltageInPort = this.cbAIPort.SelectedItem.ToString();

            if (this.cbFreqInPort.SelectedItem != null)
                di.InputCounterInPort = this.cbFreqInPort.SelectedItem.ToString();

            //Save the configuration
            config.SetConfig(di);

            //Get the LockIn configuration
            ConfigHelper<RI.Config.LockInAmp> liaConfig = new ConfigHelper<RI.Config.LockInAmp>(ConfigLocation.AllUsers);
            RI.Config.LockInAmp lia = liaConfig.GetWriteableConfig();

            //Set the properties on the ConfigurationSection
            lia.FullScale = Convert.ToDouble(this.ntbLIAFullScale.Text);

            //Save the configuration
            liaConfig.SetConfig(lia);
        }


        /// <summary>
        /// PersistMonochromator persists the M/C choices of the user
        /// in the Config file.
        /// </summary>
        private void PersistMonochromator()
        {
            //If the M/C isn't being configured, just return
            if ((this.Type & HardwareType.Monochromator) == 0 || this.TypeContained(RI.Enums.AcquisitionType.TwoD) == false)
                return;

            //Get a writeable config
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetWriteableConfig();

            //Set the properties
            if (this.cbCounter.SelectedItem != null)
                di.ScanCounterPort = this.cbCounter.SelectedItem.ToString();

            if (this.cbScanDown.SelectedItem != null)
                di.ScanDownPort = this.cbScanDown.SelectedItem.ToString();

            if (this.cbScanUp.SelectedItem != null)
                di.ScanUpPort = this.cbScanUp.SelectedItem.ToString();

            if (this.cbStepperPort.SelectedItem != null)
                di.StepperOutPort = this.cbStepperPort.SelectedItem.ToString();

            //Save the configuration
            config.SetConfig(di);

            //Get the Monochromator configuration
            ConfigHelper<RI.Config.Monochromator> mcConfig = new ConfigHelper<RI.Config.Monochromator>(ConfigLocation.AllUsers);
            RI.Config.Monochromator mc = mcConfig.GetWriteableConfig();

            //Set the properties on the ConfigurationSection
            mc.StepsPerNm = Convert.ToDouble(this.ntbStepsPerNm.Text);

            //Save the configuration
            mcConfig.SetConfig(mc);
        }


        /// <summary>
        /// PersistMirrors persists the Mirror choices made by the user
        /// in the Config file.
        /// </summary>
        void PersistMirrors()
        {
            //If the Mirrors aren't being configured, just return
            if ((this.Type & HardwareType.Mirrors) == 0 || this.TypeContained(RI.Enums.AcquisitionType.ThreeD) == false)
                return;

            //Get a writeable config
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface di = config.GetWriteableConfig();

            //Set the values
            if (this.cbXMirrorPort.SelectedItem != null)
                di.XOutPort = this.cbXMirrorPort.SelectedItem.ToString();

            if (this.cbYMirrorPort.SelectedItem != null)
                di.YOutPort = this.cbYMirrorPort.SelectedItem.ToString();

            //Save the configuration
            config.SetConfig(di);
        }


        /// <summary>
        /// PersistPotStat persists the Potentiostat choices made by
        /// the user in the Config file.
        /// </summary>
        private void PersistPotStat()
        {
            //If the PotStat isn't being configured, just return
            if ((this.Type & HardwareType.Potentiostat) == 0)
                return;

            //Get the Potentiostat configuration
            ConfigHelper<RI.Config.Potentiostat> config = new ConfigHelper<RI.Config.Potentiostat>(ConfigLocation.AllUsers);
            RI.Config.Potentiostat potstat = config.GetWriteableConfig();

            //Set the properties on the ConfigurationSection
            potstat.FullScale = Convert.ToDouble(this.ntbPotStatFullScale.Text);

            //Save the configuration
            config.SetConfig(potstat);
        }


        private Boolean TypeContained(RI.Enums.AcquisitionType type)
        {
            //Get the configuration object
            ConfigHelper<RI.Config.DaqInterface> config = new ConfigHelper<RI.Config.DaqInterface>(ConfigLocation.AllUsers);
            RI.Config.DaqInterface daq = config.GetConfig();

            //Get the list of Analysis that has been chosen
            List<RI.Enums.AcquisitionType> types = new List<RI.Enums.AcquisitionType>(daq.AcquisitionTypes);

            //Search for the TwoD Type
            return types.Contains(type);
        }
        #endregion


        #region Enum to determine what pages to show
        /// <summary>
        /// HardwareType is a set of bit flags to determine
        /// which pages to show in the Wizard.
        /// </summary>
        public enum HardwareType
        {
            Invalid = 0,
            DaqCard = 1,
            CurrentInput = 2,
            Monochromator = 4,
            Mirrors = 8,
            Potentiostat = 16,
            All = DaqCard | CurrentInput | Monochromator | Mirrors | Potentiostat
        }
        #endregion
    }
}
