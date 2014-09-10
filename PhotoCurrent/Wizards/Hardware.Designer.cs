namespace PhotoCurrent.Wizards
{
    partial class Hardware
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hardware));
            this.hardwareWizard = new Divelements.WizardFramework.Wizard();
            this.pageWelcome = new Divelements.WizardFramework.IntroductionPage();
            this.cbDoNotConfig = new System.Windows.Forms.CheckBox();
            this.pagePurpose = new Divelements.WizardFramework.WizardPage();
            this.cbLEGOAnalysis = new System.Windows.Forms.CheckBox();
            this.cb3DAnalysis = new System.Windows.Forms.CheckBox();
            this.cb2DAnalysis = new System.Windows.Forms.CheckBox();
            this.pageDAQType = new Divelements.WizardFramework.WizardPage();
            this.cbDAQCards = new System.Windows.Forms.ComboBox();
            this.lblDAQToUse = new System.Windows.Forms.Label();
            this.pageDAQCommon = new Divelements.WizardFramework.WizardPage();
            this.lblSamplingRate = new System.Windows.Forms.Label();
            this.ntbSamplingRate = new RDH2.Utilities.Controls.NumericTextBox();
            this.pageCurrentInputConfig = new Divelements.WizardFramework.WizardPage();
            this.cbFreqInPort = new System.Windows.Forms.ComboBox();
            this.lblSLIAFreq = new System.Windows.Forms.Label();
            this.lblAIPorts = new System.Windows.Forms.Label();
            this.cbAIPort = new System.Windows.Forms.ComboBox();
            this.lblLIAFullScale = new System.Windows.Forms.Label();
            this.ntbLIAFullScale = new RDH2.Utilities.Controls.NumericTextBox();
            this.pageMCConfig = new Divelements.WizardFramework.WizardPage();
            this.lblScanDown = new System.Windows.Forms.Label();
            this.lblScanUp = new System.Windows.Forms.Label();
            this.cbScanDown = new System.Windows.Forms.ComboBox();
            this.cbScanUp = new System.Windows.Forms.ComboBox();
            this.lblCounterOut = new System.Windows.Forms.Label();
            this.cbCounter = new System.Windows.Forms.ComboBox();
            this.lblStepperPort = new System.Windows.Forms.Label();
            this.cbStepperPort = new System.Windows.Forms.ComboBox();
            this.lblStepsPerNm = new System.Windows.Forms.Label();
            this.ntbStepsPerNm = new RDH2.Utilities.Controls.NumericTextBox();
            this.pageMirrorsConfig = new Divelements.WizardFramework.WizardPage();
            this.lblYMirrorPort = new System.Windows.Forms.Label();
            this.cbYMirrorPort = new System.Windows.Forms.ComboBox();
            this.lblXMirrorPort = new System.Windows.Forms.Label();
            this.cbXMirrorPort = new System.Windows.Forms.ComboBox();
            this.pagePotStatConfig = new Divelements.WizardFramework.WizardPage();
            this.ntbPotStatFullScale = new RDH2.Utilities.Controls.NumericTextBox();
            this.lblFullScale = new System.Windows.Forms.Label();
            this.pageComplete = new Divelements.WizardFramework.FinishPage();
            this.hardwareWizard.SuspendLayout();
            this.pageWelcome.SuspendLayout();
            this.pagePurpose.SuspendLayout();
            this.pageDAQType.SuspendLayout();
            this.pageDAQCommon.SuspendLayout();
            this.pageCurrentInputConfig.SuspendLayout();
            this.pageMCConfig.SuspendLayout();
            this.pageMirrorsConfig.SuspendLayout();
            this.pagePotStatConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // hardwareWizard
            // 
            this.hardwareWizard.BackColor = System.Drawing.SystemColors.Control;
            this.hardwareWizard.BannerImage = ((System.Drawing.Image)(resources.GetObject("hardwareWizard.BannerImage")));
            this.hardwareWizard.Controls.Add(this.pageWelcome);
            this.hardwareWizard.Controls.Add(this.pageDAQType);
            this.hardwareWizard.Controls.Add(this.pageMirrorsConfig);
            this.hardwareWizard.Controls.Add(this.pageDAQCommon);
            this.hardwareWizard.Controls.Add(this.pagePotStatConfig);
            this.hardwareWizard.Controls.Add(this.pageCurrentInputConfig);
            this.hardwareWizard.Controls.Add(this.pageMCConfig);
            this.hardwareWizard.Controls.Add(this.pageComplete);
            this.hardwareWizard.Controls.Add(this.pagePurpose);
            this.hardwareWizard.Cursor = System.Windows.Forms.Cursors.Default;
            this.hardwareWizard.FinishText = "&Finish";
            this.hardwareWizard.Location = new System.Drawing.Point(0, 0);
            this.hardwareWizard.Name = "hardwareWizard";
            this.hardwareWizard.OwnerForm = this;
            this.hardwareWizard.Size = new System.Drawing.Size(497, 362);
            this.hardwareWizard.TabIndex = 0;
            this.hardwareWizard.Text = "Hardware Configuration";
            this.hardwareWizard.UserExperienceType = Divelements.WizardFramework.WizardUserExperienceType.Wizard97;
            // 
            // pageWelcome
            // 
            this.pageWelcome.Controls.Add(this.cbDoNotConfig);
            this.pageWelcome.IntroductionText = resources.GetString("pageWelcome.IntroductionText");
            this.pageWelcome.Location = new System.Drawing.Point(175, 71);
            this.pageWelcome.Name = "pageWelcome";
            this.pageWelcome.NextPage = this.pagePurpose;
            this.pageWelcome.ProceedText = "";
            this.pageWelcome.Size = new System.Drawing.Size(300, 233);
            this.pageWelcome.TabIndex = 4;
            this.pageWelcome.Text = "Welcome to the Hardware Configuration Wizard";
            // 
            // cbDoNotConfig
            // 
            this.cbDoNotConfig.AutoSize = true;
            this.cbDoNotConfig.Location = new System.Drawing.Point(23, 175);
            this.cbDoNotConfig.Name = "cbDoNotConfig";
            this.cbDoNotConfig.Size = new System.Drawing.Size(158, 17);
            this.cbDoNotConfig.TabIndex = 7;
            this.cbDoNotConfig.Text = "Do not configure at this time";
            this.cbDoNotConfig.UseVisualStyleBackColor = true;
            // 
            // pagePurpose
            // 
            this.pagePurpose.Controls.Add(this.cbLEGOAnalysis);
            this.pagePurpose.Controls.Add(this.cb3DAnalysis);
            this.pagePurpose.Controls.Add(this.cb2DAnalysis);
            this.pagePurpose.Description = "Select the types of Photocurrent Analysis to perform with this application.  Note" +
    " that all of the hardware must be present on your instrument!";
            this.pagePurpose.Location = new System.Drawing.Point(11, 71);
            this.pagePurpose.Name = "pagePurpose";
            this.pagePurpose.NextPage = this.pageDAQType;
            this.pagePurpose.PreviousPage = this.pageWelcome;
            this.pagePurpose.Size = new System.Drawing.Size(475, 233);
            this.pagePurpose.TabIndex = 1004;
            this.pagePurpose.Text = "Select Analysis Types";
            // 
            // cbLEGOAnalysis
            // 
            this.cbLEGOAnalysis.AutoSize = true;
            this.cbLEGOAnalysis.Enabled = false;
            this.cbLEGOAnalysis.Location = new System.Drawing.Point(95, 130);
            this.cbLEGOAnalysis.Name = "cbLEGOAnalysis";
            this.cbLEGOAnalysis.Size = new System.Drawing.Size(242, 17);
            this.cbLEGOAnalysis.TabIndex = 2;
            this.cbLEGOAnalysis.Text = "3-D Spectra with the SHArK LEGO Instrument";
            this.cbLEGOAnalysis.UseVisualStyleBackColor = true;
            // 
            // cb3DAnalysis
            // 
            this.cb3DAnalysis.AutoSize = true;
            this.cb3DAnalysis.Location = new System.Drawing.Point(95, 87);
            this.cb3DAnalysis.Name = "cb3DAnalysis";
            this.cb3DAnalysis.Size = new System.Drawing.Size(210, 17);
            this.cb3DAnalysis.TabIndex = 1;
            this.cb3DAnalysis.Text = "3-D Spectra (Photocurrent vs. Position)";
            this.cb3DAnalysis.UseVisualStyleBackColor = true;
            // 
            // cb2DAnalysis
            // 
            this.cb2DAnalysis.AutoSize = true;
            this.cb2DAnalysis.Location = new System.Drawing.Point(95, 44);
            this.cb2DAnalysis.Name = "cb2DAnalysis";
            this.cb2DAnalysis.Size = new System.Drawing.Size(269, 17);
            this.cb2DAnalysis.TabIndex = 0;
            this.cb2DAnalysis.Text = "2-D Spectra (Photocurrent vs, Time or Wavelength)";
            this.cb2DAnalysis.UseVisualStyleBackColor = true;
            // 
            // pageDAQType
            // 
            this.pageDAQType.Controls.Add(this.cbDAQCards);
            this.pageDAQType.Controls.Add(this.lblDAQToUse);
            this.pageDAQType.Description = "Select the Data Acquisition card that you would like to use to automate the instr" +
    "ument and collect photocurrent data.";
            this.pageDAQType.Location = new System.Drawing.Point(11, 71);
            this.pageDAQType.Name = "pageDAQType";
            this.pageDAQType.NextPage = this.pageDAQCommon;
            this.pageDAQType.PreviousPage = this.pagePurpose;
            this.pageDAQType.Size = new System.Drawing.Size(475, 233);
            this.pageDAQType.TabIndex = 5;
            this.pageDAQType.Text = "Select the Data Acquisition Card to use";
            // 
            // cbDAQCards
            // 
            this.cbDAQCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDAQCards.FormattingEnabled = true;
            this.cbDAQCards.Location = new System.Drawing.Point(26, 108);
            this.cbDAQCards.Name = "cbDAQCards";
            this.cbDAQCards.Size = new System.Drawing.Size(378, 21);
            this.cbDAQCards.TabIndex = 3;
            // 
            // lblDAQToUse
            // 
            this.lblDAQToUse.Location = new System.Drawing.Point(23, 10);
            this.lblDAQToUse.Name = "lblDAQToUse";
            this.lblDAQToUse.Size = new System.Drawing.Size(391, 77);
            this.lblDAQToUse.TabIndex = 2;
            this.lblDAQToUse.Text = resources.GetString("lblDAQToUse.Text");
            // 
            // pageDAQCommon
            // 
            this.pageDAQCommon.Controls.Add(this.lblSamplingRate);
            this.pageDAQCommon.Controls.Add(this.ntbSamplingRate);
            this.pageDAQCommon.Description = "Set the values in the controls below and click Next.  If you are unsure what valu" +
    "es to use, simply leave the defaults.";
            this.pageDAQCommon.Location = new System.Drawing.Point(11, 71);
            this.pageDAQCommon.Name = "pageDAQCommon";
            this.pageDAQCommon.NextPage = this.pageCurrentInputConfig;
            this.pageDAQCommon.PreviousPage = this.pageDAQType;
            this.pageDAQCommon.Size = new System.Drawing.Size(475, 233);
            this.pageDAQCommon.TabIndex = 12;
            this.pageDAQCommon.Text = "Data Acquisition Properties";
            // 
            // lblSamplingRate
            // 
            this.lblSamplingRate.AutoSize = true;
            this.lblSamplingRate.Location = new System.Drawing.Point(65, 74);
            this.lblSamplingRate.Name = "lblSamplingRate";
            this.lblSamplingRate.Size = new System.Drawing.Size(164, 13);
            this.lblSamplingRate.TabIndex = 14;
            this.lblSamplingRate.Text = "Sampling Rate (samples per sec):";
            // 
            // ntbSamplingRate
            // 
            this.ntbSamplingRate.AllowDecimal = false;
            this.ntbSamplingRate.AllowNegative = false;
            this.ntbSamplingRate.Location = new System.Drawing.Point(232, 70);
            this.ntbSamplingRate.Name = "ntbSamplingRate";
            this.ntbSamplingRate.Size = new System.Drawing.Size(100, 20);
            this.ntbSamplingRate.TabIndex = 15;
            // 
            // pageCurrentInputConfig
            // 
            this.pageCurrentInputConfig.Controls.Add(this.cbFreqInPort);
            this.pageCurrentInputConfig.Controls.Add(this.lblSLIAFreq);
            this.pageCurrentInputConfig.Controls.Add(this.lblAIPorts);
            this.pageCurrentInputConfig.Controls.Add(this.cbAIPort);
            this.pageCurrentInputConfig.Controls.Add(this.lblLIAFullScale);
            this.pageCurrentInputConfig.Controls.Add(this.ntbLIAFullScale);
            this.pageCurrentInputConfig.Description = "Enter the values for the Photocurrent Input Devices below.  These values will be " +
    "used in acquiring and scaling all of the data from the instrument.";
            this.pageCurrentInputConfig.Location = new System.Drawing.Point(11, 71);
            this.pageCurrentInputConfig.Name = "pageCurrentInputConfig";
            this.pageCurrentInputConfig.NextPage = this.pageMCConfig;
            this.pageCurrentInputConfig.PreviousPage = this.pageDAQCommon;
            this.pageCurrentInputConfig.Size = new System.Drawing.Size(475, 233);
            this.pageCurrentInputConfig.TabIndex = 9;
            this.pageCurrentInputConfig.Text = "Configure Photocurrent Input Properties";
            // 
            // cbFreqInPort
            // 
            this.cbFreqInPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFreqInPort.FormattingEnabled = true;
            this.cbFreqInPort.Location = new System.Drawing.Point(232, 93);
            this.cbFreqInPort.Name = "cbFreqInPort";
            this.cbFreqInPort.Size = new System.Drawing.Size(100, 21);
            this.cbFreqInPort.TabIndex = 5;
            // 
            // lblSLIAFreq
            // 
            this.lblSLIAFreq.AutoSize = true;
            this.lblSLIAFreq.Location = new System.Drawing.Point(51, 97);
            this.lblSLIAFreq.Name = "lblSLIAFreq";
            this.lblSLIAFreq.Size = new System.Drawing.Size(178, 13);
            this.lblSLIAFreq.TabIndex = 6;
            this.lblSLIAFreq.Text = "Software Lock-In Frequency In Port:";
            // 
            // lblAIPorts
            // 
            this.lblAIPorts.AutoSize = true;
            this.lblAIPorts.Location = new System.Drawing.Point(123, 55);
            this.lblAIPorts.Name = "lblAIPorts";
            this.lblAIPorts.Size = new System.Drawing.Size(106, 13);
            this.lblAIPorts.TabIndex = 5;
            this.lblAIPorts.Text = "DAQ Voltage In Port:";
            // 
            // cbAIPort
            // 
            this.cbAIPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAIPort.FormattingEnabled = true;
            this.cbAIPort.Location = new System.Drawing.Point(232, 51);
            this.cbAIPort.Name = "cbAIPort";
            this.cbAIPort.Size = new System.Drawing.Size(100, 21);
            this.cbAIPort.TabIndex = 4;
            // 
            // lblLIAFullScale
            // 
            this.lblLIAFullScale.AutoSize = true;
            this.lblLIAFullScale.Location = new System.Drawing.Point(69, 139);
            this.lblLIAFullScale.Name = "lblLIAFullScale";
            this.lblLIAFullScale.Size = new System.Drawing.Size(160, 13);
            this.lblLIAFullScale.TabIndex = 3;
            this.lblLIAFullScale.Text = "Hardware Lock-In Full Scale (V):";
            // 
            // ntbLIAFullScale
            // 
            this.ntbLIAFullScale.AllowDecimal = true;
            this.ntbLIAFullScale.AllowNegative = false;
            this.ntbLIAFullScale.Location = new System.Drawing.Point(232, 135);
            this.ntbLIAFullScale.Name = "ntbLIAFullScale";
            this.ntbLIAFullScale.Size = new System.Drawing.Size(100, 20);
            this.ntbLIAFullScale.TabIndex = 2;
            // 
            // pageMCConfig
            // 
            this.pageMCConfig.Controls.Add(this.lblScanDown);
            this.pageMCConfig.Controls.Add(this.lblScanUp);
            this.pageMCConfig.Controls.Add(this.cbScanDown);
            this.pageMCConfig.Controls.Add(this.cbScanUp);
            this.pageMCConfig.Controls.Add(this.lblCounterOut);
            this.pageMCConfig.Controls.Add(this.cbCounter);
            this.pageMCConfig.Controls.Add(this.lblStepperPort);
            this.pageMCConfig.Controls.Add(this.cbStepperPort);
            this.pageMCConfig.Controls.Add(this.lblStepsPerNm);
            this.pageMCConfig.Controls.Add(this.ntbStepsPerNm);
            this.pageMCConfig.Description = "Enter the values for the Monochromator below.  These values will be used in movin" +
    "g the specified number of nanometers while scanning the instrument.";
            this.pageMCConfig.Location = new System.Drawing.Point(11, 71);
            this.pageMCConfig.Name = "pageMCConfig";
            this.pageMCConfig.NextPage = this.pageMirrorsConfig;
            this.pageMCConfig.PreviousPage = this.pageCurrentInputConfig;
            this.pageMCConfig.Size = new System.Drawing.Size(475, 233);
            this.pageMCConfig.TabIndex = 10;
            this.pageMCConfig.Text = "Configure Monochromator Properties";
            // 
            // lblScanDown
            // 
            this.lblScanDown.AutoSize = true;
            this.lblScanDown.Location = new System.Drawing.Point(140, 156);
            this.lblScanDown.Name = "lblScanDown";
            this.lblScanDown.Size = new System.Drawing.Size(88, 13);
            this.lblScanDown.TabIndex = 19;
            this.lblScanDown.Text = "Scan Down Port:";
            // 
            // lblScanUp
            // 
            this.lblScanUp.AutoSize = true;
            this.lblScanUp.Location = new System.Drawing.Point(154, 110);
            this.lblScanUp.Name = "lblScanUp";
            this.lblScanUp.Size = new System.Drawing.Size(74, 13);
            this.lblScanUp.TabIndex = 18;
            this.lblScanUp.Text = "Scan Up Port:";
            // 
            // cbScanDown
            // 
            this.cbScanDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScanDown.FormattingEnabled = true;
            this.cbScanDown.Location = new System.Drawing.Point(232, 152);
            this.cbScanDown.Name = "cbScanDown";
            this.cbScanDown.Size = new System.Drawing.Size(80, 21);
            this.cbScanDown.TabIndex = 17;
            // 
            // cbScanUp
            // 
            this.cbScanUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScanUp.FormattingEnabled = true;
            this.cbScanUp.Location = new System.Drawing.Point(232, 106);
            this.cbScanUp.Name = "cbScanUp";
            this.cbScanUp.Size = new System.Drawing.Size(80, 21);
            this.cbScanUp.TabIndex = 16;
            // 
            // lblCounterOut
            // 
            this.lblCounterOut.AutoSize = true;
            this.lblCounterOut.Location = new System.Drawing.Point(119, 64);
            this.lblCounterOut.Name = "lblCounterOut";
            this.lblCounterOut.Size = new System.Drawing.Size(109, 13);
            this.lblCounterOut.TabIndex = 15;
            this.lblCounterOut.Text = "Stepper Counter Port:";
            // 
            // cbCounter
            // 
            this.cbCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCounter.FormattingEnabled = true;
            this.cbCounter.Location = new System.Drawing.Point(232, 60);
            this.cbCounter.Name = "cbCounter";
            this.cbCounter.Size = new System.Drawing.Size(80, 21);
            this.cbCounter.TabIndex = 14;
            // 
            // lblStepperPort
            // 
            this.lblStepperPort.AutoSize = true;
            this.lblStepperPort.Location = new System.Drawing.Point(140, 18);
            this.lblStepperPort.Name = "lblStepperPort";
            this.lblStepperPort.Size = new System.Drawing.Size(88, 13);
            this.lblStepperPort.TabIndex = 13;
            this.lblStepperPort.Text = "Stepper DO Port:";
            // 
            // cbStepperPort
            // 
            this.cbStepperPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStepperPort.FormattingEnabled = true;
            this.cbStepperPort.Location = new System.Drawing.Point(232, 14);
            this.cbStepperPort.Name = "cbStepperPort";
            this.cbStepperPort.Size = new System.Drawing.Size(80, 21);
            this.cbStepperPort.TabIndex = 12;
            // 
            // lblStepsPerNm
            // 
            this.lblStepsPerNm.AutoSize = true;
            this.lblStepsPerNm.Location = new System.Drawing.Point(68, 202);
            this.lblStepsPerNm.Name = "lblStepsPerNm";
            this.lblStepsPerNm.Size = new System.Drawing.Size(160, 13);
            this.lblStepsPerNm.TabIndex = 3;
            this.lblStepsPerNm.Text = "Steps per nm (can be fractional):";
            // 
            // ntbStepsPerNm
            // 
            this.ntbStepsPerNm.AllowDecimal = true;
            this.ntbStepsPerNm.AllowNegative = false;
            this.ntbStepsPerNm.Location = new System.Drawing.Point(232, 198);
            this.ntbStepsPerNm.Name = "ntbStepsPerNm";
            this.ntbStepsPerNm.Size = new System.Drawing.Size(100, 20);
            this.ntbStepsPerNm.TabIndex = 2;
            // 
            // pageMirrorsConfig
            // 
            this.pageMirrorsConfig.Controls.Add(this.lblYMirrorPort);
            this.pageMirrorsConfig.Controls.Add(this.cbYMirrorPort);
            this.pageMirrorsConfig.Controls.Add(this.lblXMirrorPort);
            this.pageMirrorsConfig.Controls.Add(this.cbXMirrorPort);
            this.pageMirrorsConfig.Description = "Configure the Data Acquisition card below.  If you are unsure what values to choo" +
    "se, leave the default values.";
            this.pageMirrorsConfig.Location = new System.Drawing.Point(11, 71);
            this.pageMirrorsConfig.Name = "pageMirrorsConfig";
            this.pageMirrorsConfig.NextPage = this.pagePotStatConfig;
            this.pageMirrorsConfig.PreviousPage = this.pageMCConfig;
            this.pageMirrorsConfig.Size = new System.Drawing.Size(475, 233);
            this.pageMirrorsConfig.TabIndex = 13;
            this.pageMirrorsConfig.Text = "Configure Mirror Output Properties";
            // 
            // lblYMirrorPort
            // 
            this.lblYMirrorPort.AutoSize = true;
            this.lblYMirrorPort.Location = new System.Drawing.Point(141, 123);
            this.lblYMirrorPort.Name = "lblYMirrorPort";
            this.lblYMirrorPort.Size = new System.Drawing.Size(88, 13);
            this.lblYMirrorPort.TabIndex = 6;
            this.lblYMirrorPort.Text = "Y-Mirror Out Port:";
            // 
            // cbYMirrorPort
            // 
            this.cbYMirrorPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYMirrorPort.FormattingEnabled = true;
            this.cbYMirrorPort.Location = new System.Drawing.Point(232, 119);
            this.cbYMirrorPort.Name = "cbYMirrorPort";
            this.cbYMirrorPort.Size = new System.Drawing.Size(80, 21);
            this.cbYMirrorPort.TabIndex = 5;
            // 
            // lblXMirrorPort
            // 
            this.lblXMirrorPort.AutoSize = true;
            this.lblXMirrorPort.Location = new System.Drawing.Point(141, 72);
            this.lblXMirrorPort.Name = "lblXMirrorPort";
            this.lblXMirrorPort.Size = new System.Drawing.Size(88, 13);
            this.lblXMirrorPort.TabIndex = 4;
            this.lblXMirrorPort.Text = "X-Mirror Out Port:";
            // 
            // cbXMirrorPort
            // 
            this.cbXMirrorPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbXMirrorPort.FormattingEnabled = true;
            this.cbXMirrorPort.Location = new System.Drawing.Point(232, 68);
            this.cbXMirrorPort.Name = "cbXMirrorPort";
            this.cbXMirrorPort.Size = new System.Drawing.Size(80, 21);
            this.cbXMirrorPort.TabIndex = 2;
            // 
            // pagePotStatConfig
            // 
            this.pagePotStatConfig.Controls.Add(this.ntbPotStatFullScale);
            this.pagePotStatConfig.Controls.Add(this.lblFullScale);
            this.pagePotStatConfig.Description = "Enter the values for the Potentiostat below.  These values will be used in scalin" +
    "g all of the data aquired from the instrument.";
            this.pagePotStatConfig.Location = new System.Drawing.Point(11, 71);
            this.pagePotStatConfig.Name = "pagePotStatConfig";
            this.pagePotStatConfig.NextPage = this.pageComplete;
            this.pagePotStatConfig.PreviousPage = this.pageMirrorsConfig;
            this.pagePotStatConfig.Size = new System.Drawing.Size(475, 233);
            this.pagePotStatConfig.TabIndex = 4;
            this.pagePotStatConfig.Text = "Configure Potentiostat Properties";
            // 
            // ntbPotStatFullScale
            // 
            this.ntbPotStatFullScale.AllowDecimal = true;
            this.ntbPotStatFullScale.AllowNegative = false;
            this.ntbPotStatFullScale.Location = new System.Drawing.Point(232, 69);
            this.ntbPotStatFullScale.Name = "ntbPotStatFullScale";
            this.ntbPotStatFullScale.Size = new System.Drawing.Size(100, 20);
            this.ntbPotStatFullScale.TabIndex = 4;
            // 
            // lblFullScale
            // 
            this.lblFullScale.AutoSize = true;
            this.lblFullScale.Location = new System.Drawing.Point(122, 73);
            this.lblFullScale.Name = "lblFullScale";
            this.lblFullScale.Size = new System.Drawing.Size(107, 13);
            this.lblFullScale.TabIndex = 3;
            this.lblFullScale.Text = "Output Full Scale (V):";
            // 
            // pageComplete
            // 
            this.pageComplete.AllowCancel = false;
            this.pageComplete.FinishText = "The following Hardware device configurations have been saved:\r\n{0}";
            this.pageComplete.Location = new System.Drawing.Point(175, 71);
            this.pageComplete.Name = "pageComplete";
            this.pageComplete.PreviousPage = this.pagePotStatConfig;
            this.pageComplete.ProceedText = "";
            this.pageComplete.Size = new System.Drawing.Size(300, 233);
            this.pageComplete.TabIndex = 8;
            this.pageComplete.Text = "Hardware Configuration Complete";
            // 
            // Hardware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 362);
            this.ControlBox = false;
            this.Controls.Add(this.hardwareWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Hardware";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hardware Configuration";
            this.hardwareWizard.ResumeLayout(false);
            this.pageWelcome.ResumeLayout(false);
            this.pageWelcome.PerformLayout();
            this.pagePurpose.ResumeLayout(false);
            this.pagePurpose.PerformLayout();
            this.pageDAQType.ResumeLayout(false);
            this.pageDAQCommon.ResumeLayout(false);
            this.pageDAQCommon.PerformLayout();
            this.pageCurrentInputConfig.ResumeLayout(false);
            this.pageCurrentInputConfig.PerformLayout();
            this.pageMCConfig.ResumeLayout(false);
            this.pageMCConfig.PerformLayout();
            this.pageMirrorsConfig.ResumeLayout(false);
            this.pageMirrorsConfig.PerformLayout();
            this.pagePotStatConfig.ResumeLayout(false);
            this.pagePotStatConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Divelements.WizardFramework.Wizard hardwareWizard;
        private Divelements.WizardFramework.WizardPage pagePotStatConfig;
        private System.Windows.Forms.Label lblFullScale;
        private RDH2.Utilities.Controls.NumericTextBox ntbPotStatFullScale;
        private Divelements.WizardFramework.IntroductionPage pageWelcome;
        private Divelements.WizardFramework.WizardPage pageDAQType;
        private System.Windows.Forms.Label lblDAQToUse;
        private System.Windows.Forms.ComboBox cbDAQCards;
        private System.Windows.Forms.CheckBox cbDoNotConfig;
        private Divelements.WizardFramework.FinishPage pageComplete;
        private Divelements.WizardFramework.WizardPage pageCurrentInputConfig;
        private System.Windows.Forms.Label lblLIAFullScale;
        private RDH2.Utilities.Controls.NumericTextBox ntbLIAFullScale;
        private Divelements.WizardFramework.WizardPage pageMCConfig;
        private System.Windows.Forms.Label lblStepsPerNm;
        private RDH2.Utilities.Controls.NumericTextBox ntbStepsPerNm;
        private Divelements.WizardFramework.WizardPage pageDAQCommon;
        private RDH2.Utilities.Controls.NumericTextBox ntbSamplingRate;
        private System.Windows.Forms.Label lblSamplingRate;
        private Divelements.WizardFramework.WizardPage pageMirrorsConfig;
        private System.Windows.Forms.Label lblXMirrorPort;
        private System.Windows.Forms.ComboBox cbXMirrorPort;
        private System.Windows.Forms.Label lblYMirrorPort;
        private System.Windows.Forms.ComboBox cbYMirrorPort;
        private System.Windows.Forms.Label lblAIPorts;
        private System.Windows.Forms.ComboBox cbAIPort;
        private System.Windows.Forms.Label lblSLIAFreq;
        private System.Windows.Forms.Label lblScanDown;
        private System.Windows.Forms.Label lblScanUp;
        private System.Windows.Forms.ComboBox cbScanDown;
        private System.Windows.Forms.ComboBox cbScanUp;
        private System.Windows.Forms.Label lblCounterOut;
        private System.Windows.Forms.ComboBox cbCounter;
        private System.Windows.Forms.Label lblStepperPort;
        private System.Windows.Forms.ComboBox cbStepperPort;
        private Divelements.WizardFramework.WizardPage pagePurpose;
        private System.Windows.Forms.CheckBox cbLEGOAnalysis;
        private System.Windows.Forms.CheckBox cb3DAnalysis;
        private System.Windows.Forms.CheckBox cb2DAnalysis;
        private System.Windows.Forms.ComboBox cbFreqInPort;
    }
}