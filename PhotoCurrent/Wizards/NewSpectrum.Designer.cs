namespace PhotoCurrent.Wizards
{
    partial class NewSpectrum
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSpectrum));
            this.newSpecWizard = new Divelements.WizardFramework.Wizard();
            this.pageSpectrumType = new Divelements.WizardFramework.WizardPage();
            this.rbLEGO = new System.Windows.Forms.RadioButton();
            this.rb3DPosition = new System.Windows.Forms.RadioButton();
            this.rbBackground = new System.Windows.Forms.RadioButton();
            this.rbTime = new System.Windows.Forms.RadioButton();
            this.rbWavelength = new System.Windows.Forms.RadioButton();
            this.pagePositionSetup = new Divelements.WizardFramework.WizardPage();
            this.lblRows = new System.Windows.Forms.Label();
            this.lblRowsLabel = new System.Windows.Forms.Label();
            this.lblStepSize = new System.Windows.Forms.Label();
            this.ntbStepSize = new RDH2.Utilities.Controls.NumericTextBox();
            this.lblXPosStart = new System.Windows.Forms.Label();
            this.spinXPosStart = new System.Windows.Forms.NumericUpDown();
            this.lblYPosStart = new System.Windows.Forms.Label();
            this.spinYPosStart = new System.Windows.Forms.NumericUpDown();
            this.lblCols = new System.Windows.Forms.Label();
            this.lblColsLabel = new System.Windows.Forms.Label();
            this.lblXPosEnd = new System.Windows.Forms.Label();
            this.spinXPosEnd = new System.Windows.Forms.NumericUpDown();
            this.lblYPosEnd = new System.Windows.Forms.Label();
            this.spinYPosEnd = new System.Windows.Forms.NumericUpDown();
            this.pageSpectrumSetup = new Divelements.WizardFramework.WizardPage();
            this.gbMonochromator = new System.Windows.Forms.GroupBox();
            this.ntbIncrement = new RDH2.Utilities.Controls.NumericTextBox();
            this.lblIncrement = new System.Windows.Forms.Label();
            this.ntbStartWavelength = new RDH2.Utilities.Controls.NumericTextBox();
            this.ntbEndWavelength = new RDH2.Utilities.Controls.NumericTextBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.gbDAQ = new System.Windows.Forms.GroupBox();
            this.lblDelay = new System.Windows.Forms.Label();
            this.lblSamples = new System.Windows.Forms.Label();
            this.ntbDelay = new RDH2.Utilities.Controls.NumericTextBox();
            this.ntbSamplesToAvg = new RDH2.Utilities.Controls.NumericTextBox();
            this.pageHardwareSetup = new Divelements.WizardFramework.WizardPage();
            this.bgPotStat = new System.Windows.Forms.GroupBox();
            this.cbCurrentUnits = new System.Windows.Forms.ComboBox();
            this.cbCurrentRange = new System.Windows.Forms.ComboBox();
            this.lblCurrentRange = new System.Windows.Forms.Label();
            this.gbLockIn = new System.Windows.Forms.GroupBox();
            this.cbLIAType = new System.Windows.Forms.ComboBox();
            this.lblLIAType = new System.Windows.Forms.Label();
            this.cbSensitivityUnits = new System.Windows.Forms.ComboBox();
            this.cbSensitivity = new System.Windows.Forms.ComboBox();
            this.lblSensitivity = new System.Windows.Forms.Label();
            this.pageSoftwareLIASetup = new Divelements.WizardFramework.WizardPage();
            this.gbLaserPosition = new System.Windows.Forms.GroupBox();
            this.lblYAxisPos = new System.Windows.Forms.Label();
            this.spinYAxisPos = new System.Windows.Forms.NumericUpDown();
            this.lblXAxisPos = new System.Windows.Forms.Label();
            this.spinXAxisPos = new System.Windows.Forms.NumericUpDown();
            this.gbLIAOutput = new System.Windows.Forms.GroupBox();
            this.lblLIAValue = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblLIAValueLabel = new System.Windows.Forms.Label();
            this.lblFrequencyLabel = new System.Windows.Forms.Label();
            this.pageFileName = new Divelements.WizardFramework.WizardPage();
            this.btnFileSave = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.pageComplete = new Divelements.WizardFramework.WizardPage();
            this.cbAddToActive = new System.Windows.Forms.CheckBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.cbNoDisplay = new System.Windows.Forms.CheckBox();
            this.newSpecWizard.SuspendLayout();
            this.pageSpectrumType.SuspendLayout();
            this.pagePositionSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinXPosStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYPosStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinXPosEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYPosEnd)).BeginInit();
            this.pageSpectrumSetup.SuspendLayout();
            this.gbMonochromator.SuspendLayout();
            this.gbDAQ.SuspendLayout();
            this.pageHardwareSetup.SuspendLayout();
            this.bgPotStat.SuspendLayout();
            this.gbLockIn.SuspendLayout();
            this.pageSoftwareLIASetup.SuspendLayout();
            this.gbLaserPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinYAxisPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinXAxisPos)).BeginInit();
            this.gbLIAOutput.SuspendLayout();
            this.pageFileName.SuspendLayout();
            this.pageComplete.SuspendLayout();
            this.SuspendLayout();
            // 
            // newSpecWizard
            // 
            this.newSpecWizard.BackColor = System.Drawing.SystemColors.Control;
            this.newSpecWizard.BannerImage = ((System.Drawing.Image)(resources.GetObject("newSpecWizard.BannerImage")));
            this.newSpecWizard.Controls.Add(this.pageSpectrumType);
            this.newSpecWizard.Controls.Add(this.pagePositionSetup);
            this.newSpecWizard.Controls.Add(this.pageSpectrumSetup);
            this.newSpecWizard.Controls.Add(this.pageHardwareSetup);
            this.newSpecWizard.Controls.Add(this.pageFileName);
            this.newSpecWizard.Controls.Add(this.pageComplete);
            this.newSpecWizard.Controls.Add(this.pageSoftwareLIASetup);
            this.newSpecWizard.Cursor = System.Windows.Forms.Cursors.Default;
            this.newSpecWizard.FinishText = "&Finish";
            this.newSpecWizard.Location = new System.Drawing.Point(0, 0);
            this.newSpecWizard.Name = "newSpecWizard";
            this.newSpecWizard.OwnerForm = this;
            this.newSpecWizard.Size = new System.Drawing.Size(497, 362);
            this.newSpecWizard.TabIndex = 0;
            this.newSpecWizard.Text = "New Spectrum";
            this.newSpecWizard.UserExperienceType = Divelements.WizardFramework.WizardUserExperienceType.Wizard97;
            // 
            // pageSpectrumType
            // 
            this.pageSpectrumType.Controls.Add(this.rbLEGO);
            this.pageSpectrumType.Controls.Add(this.rb3DPosition);
            this.pageSpectrumType.Controls.Add(this.rbBackground);
            this.pageSpectrumType.Controls.Add(this.rbTime);
            this.pageSpectrumType.Controls.Add(this.rbWavelength);
            this.pageSpectrumType.Description = "Select the type of Spectrum that you would like to take from the radio buttons be" +
    "low.";
            this.pageSpectrumType.Location = new System.Drawing.Point(11, 71);
            this.pageSpectrumType.Name = "pageSpectrumType";
            this.pageSpectrumType.NextPage = this.pagePositionSetup;
            this.pageSpectrumType.Size = new System.Drawing.Size(475, 233);
            this.pageSpectrumType.TabIndex = 4;
            this.pageSpectrumType.Text = "Select Spectrum Type";
            // 
            // rbLEGO
            // 
            this.rbLEGO.AutoSize = true;
            this.rbLEGO.Location = new System.Drawing.Point(90, 229);
            this.rbLEGO.Name = "rbLEGO";
            this.rbLEGO.Size = new System.Drawing.Size(273, 17);
            this.rbLEGO.TabIndex = 6;
            this.rbLEGO.TabStop = true;
            this.rbLEGO.Text = "Position:  LEGO X / Y Scanner, Photocurrent Output";
            this.rbLEGO.UseVisualStyleBackColor = true;
            // 
            // rb3DPosition
            // 
            this.rb3DPosition.AutoSize = true;
            this.rb3DPosition.Location = new System.Drawing.Point(79, 157);
            this.rb3DPosition.Name = "rb3DPosition";
            this.rb3DPosition.Size = new System.Drawing.Size(270, 17);
            this.rb3DPosition.TabIndex = 5;
            this.rb3DPosition.Text = "Position:  Laser X / Y Scanner; Photocurrent Output";
            this.rb3DPosition.UseVisualStyleBackColor = true;
            // 
            // rbBackground
            // 
            this.rbBackground.AutoSize = true;
            this.rbBackground.Location = new System.Drawing.Point(79, 124);
            this.rbBackground.Name = "rbBackground";
            this.rbBackground.Size = new System.Drawing.Size(307, 17);
            this.rbBackground.TabIndex = 4;
            this.rbBackground.Text = "Background:  Scanning Monochromator; Thermopile Output";
            this.rbBackground.UseVisualStyleBackColor = true;
            // 
            // rbTime
            // 
            this.rbTime.AutoSize = true;
            this.rbTime.Location = new System.Drawing.Point(79, 91);
            this.rbTime.Name = "rbTime";
            this.rbTime.Size = new System.Drawing.Size(283, 17);
            this.rbTime.TabIndex = 3;
            this.rbTime.Text = "Time:  Stationary Monochromator; Photocurrent Output";
            this.rbTime.UseVisualStyleBackColor = true;
            // 
            // rbWavelength
            // 
            this.rbWavelength.AutoSize = true;
            this.rbWavelength.Checked = true;
            this.rbWavelength.Location = new System.Drawing.Point(79, 58);
            this.rbWavelength.Name = "rbWavelength";
            this.rbWavelength.Size = new System.Drawing.Size(316, 17);
            this.rbWavelength.TabIndex = 2;
            this.rbWavelength.TabStop = true;
            this.rbWavelength.Text = "Wavelength:  Scanning Monochromator; Photocurrent Output";
            this.rbWavelength.UseVisualStyleBackColor = true;
            // 
            // pagePositionSetup
            // 
            this.pagePositionSetup.Controls.Add(this.lblRows);
            this.pagePositionSetup.Controls.Add(this.lblRowsLabel);
            this.pagePositionSetup.Controls.Add(this.lblStepSize);
            this.pagePositionSetup.Controls.Add(this.ntbStepSize);
            this.pagePositionSetup.Controls.Add(this.lblXPosStart);
            this.pagePositionSetup.Controls.Add(this.spinXPosStart);
            this.pagePositionSetup.Controls.Add(this.lblYPosStart);
            this.pagePositionSetup.Controls.Add(this.spinYPosStart);
            this.pagePositionSetup.Controls.Add(this.lblCols);
            this.pagePositionSetup.Controls.Add(this.lblColsLabel);
            this.pagePositionSetup.Controls.Add(this.lblXPosEnd);
            this.pagePositionSetup.Controls.Add(this.spinXPosEnd);
            this.pagePositionSetup.Controls.Add(this.lblYPosEnd);
            this.pagePositionSetup.Controls.Add(this.spinYPosEnd);
            this.pagePositionSetup.Description = "Use the X and Y Position controls to set the start and end points for the scan.  " +
    "Set the Step Size to modify the distance moved per click.";
            this.pagePositionSetup.Location = new System.Drawing.Point(11, 71);
            this.pagePositionSetup.Name = "pagePositionSetup";
            this.pagePositionSetup.NextPage = this.pageSpectrumSetup;
            this.pagePositionSetup.PreviousPage = this.pageSpectrumType;
            this.pagePositionSetup.Size = new System.Drawing.Size(475, 233);
            this.pagePositionSetup.TabIndex = 9;
            this.pagePositionSetup.Text = "Position Start and End Setup";
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(194, 189);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(13, 13);
            this.lblRows.TabIndex = 9;
            this.lblRows.Text = "0";
            // 
            // lblRowsLabel
            // 
            this.lblRowsLabel.AutoSize = true;
            this.lblRowsLabel.Location = new System.Drawing.Point(93, 189);
            this.lblRowsLabel.Name = "lblRowsLabel";
            this.lblRowsLabel.Size = new System.Drawing.Size(95, 13);
            this.lblRowsLabel.TabIndex = 8;
            this.lblRowsLabel.Text = "Rows (calculated):";
            // 
            // lblStepSize
            // 
            this.lblStepSize.AutoSize = true;
            this.lblStepSize.Location = new System.Drawing.Point(94, 138);
            this.lblStepSize.Name = "lblStepSize";
            this.lblStepSize.Size = new System.Drawing.Size(55, 13);
            this.lblStepSize.TabIndex = 7;
            this.lblStepSize.Text = "Step Size:";
            // 
            // ntbStepSize
            // 
            this.ntbStepSize.AllowDecimal = false;
            this.ntbStepSize.AllowNegative = false;
            this.ntbStepSize.Location = new System.Drawing.Point(152, 134);
            this.ntbStepSize.Name = "ntbStepSize";
            this.ntbStepSize.Size = new System.Drawing.Size(62, 20);
            this.ntbStepSize.TabIndex = 5;
            this.ntbStepSize.TextChanged += new System.EventHandler(this.ntbStepSize_TextChanged);
            // 
            // lblXPosStart
            // 
            this.lblXPosStart.AutoSize = true;
            this.lblXPosStart.Location = new System.Drawing.Point(86, 54);
            this.lblXPosStart.Name = "lblXPosStart";
            this.lblXPosStart.Size = new System.Drawing.Size(64, 13);
            this.lblXPosStart.TabIndex = 3;
            this.lblXPosStart.Text = "X-Axis Start:";
            // 
            // spinXPosStart
            // 
            this.spinXPosStart.Location = new System.Drawing.Point(153, 50);
            this.spinXPosStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinXPosStart.Name = "spinXPosStart";
            this.spinXPosStart.Size = new System.Drawing.Size(62, 20);
            this.spinXPosStart.TabIndex = 1;
            this.spinXPosStart.ValueChanged += new System.EventHandler(this.spinXAxis_ValueChanged);
            // 
            // lblYPosStart
            // 
            this.lblYPosStart.AutoSize = true;
            this.lblYPosStart.Location = new System.Drawing.Point(85, 96);
            this.lblYPosStart.Name = "lblYPosStart";
            this.lblYPosStart.Size = new System.Drawing.Size(64, 13);
            this.lblYPosStart.TabIndex = 5;
            this.lblYPosStart.Text = "Y-Axis Start:";
            // 
            // spinYPosStart
            // 
            this.spinYPosStart.Location = new System.Drawing.Point(152, 92);
            this.spinYPosStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinYPosStart.Name = "spinYPosStart";
            this.spinYPosStart.Size = new System.Drawing.Size(62, 20);
            this.spinYPosStart.TabIndex = 3;
            this.spinYPosStart.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinYPosStart.ValueChanged += new System.EventHandler(this.spinYAxis_ValueChanged);
            // 
            // lblCols
            // 
            this.lblCols.AutoSize = true;
            this.lblCols.Location = new System.Drawing.Point(368, 189);
            this.lblCols.Name = "lblCols";
            this.lblCols.Size = new System.Drawing.Size(13, 13);
            this.lblCols.TabIndex = 11;
            this.lblCols.Text = "0";
            // 
            // lblColsLabel
            // 
            this.lblColsLabel.AutoSize = true;
            this.lblColsLabel.Location = new System.Drawing.Point(257, 189);
            this.lblColsLabel.Name = "lblColsLabel";
            this.lblColsLabel.Size = new System.Drawing.Size(108, 13);
            this.lblColsLabel.TabIndex = 10;
            this.lblColsLabel.Text = "Columns (calculated):";
            // 
            // lblXPosEnd
            // 
            this.lblXPosEnd.AutoSize = true;
            this.lblXPosEnd.Location = new System.Drawing.Point(264, 54);
            this.lblXPosEnd.Name = "lblXPosEnd";
            this.lblXPosEnd.Size = new System.Drawing.Size(61, 13);
            this.lblXPosEnd.TabIndex = 13;
            this.lblXPosEnd.Text = "X-Axis End:";
            // 
            // spinXPosEnd
            // 
            this.spinXPosEnd.Location = new System.Drawing.Point(328, 50);
            this.spinXPosEnd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinXPosEnd.Name = "spinXPosEnd";
            this.spinXPosEnd.Size = new System.Drawing.Size(62, 20);
            this.spinXPosEnd.TabIndex = 2;
            this.spinXPosEnd.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinXPosEnd.ValueChanged += new System.EventHandler(this.spinXAxis_ValueChanged);
            // 
            // lblYPosEnd
            // 
            this.lblYPosEnd.AutoSize = true;
            this.lblYPosEnd.Location = new System.Drawing.Point(263, 96);
            this.lblYPosEnd.Name = "lblYPosEnd";
            this.lblYPosEnd.Size = new System.Drawing.Size(61, 13);
            this.lblYPosEnd.TabIndex = 15;
            this.lblYPosEnd.Text = "Y-Axis End:";
            // 
            // spinYPosEnd
            // 
            this.spinYPosEnd.Location = new System.Drawing.Point(327, 92);
            this.spinYPosEnd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinYPosEnd.Name = "spinYPosEnd";
            this.spinYPosEnd.Size = new System.Drawing.Size(62, 20);
            this.spinYPosEnd.TabIndex = 4;
            this.spinYPosEnd.ValueChanged += new System.EventHandler(this.spinYAxis_ValueChanged);
            // 
            // pageSpectrumSetup
            // 
            this.pageSpectrumSetup.Controls.Add(this.gbMonochromator);
            this.pageSpectrumSetup.Controls.Add(this.gbDAQ);
            this.pageSpectrumSetup.Description = "Enter the parameters for the Spectrum below and click Next.";
            this.pageSpectrumSetup.Location = new System.Drawing.Point(11, 71);
            this.pageSpectrumSetup.Name = "pageSpectrumSetup";
            this.pageSpectrumSetup.NextPage = this.pageHardwareSetup;
            this.pageSpectrumSetup.PreviousPage = this.pageSpectrumType;
            this.pageSpectrumSetup.Size = new System.Drawing.Size(475, 233);
            this.pageSpectrumSetup.TabIndex = 5;
            this.pageSpectrumSetup.Text = "{0} Spectrum Setup";
            // 
            // gbMonochromator
            // 
            this.gbMonochromator.Controls.Add(this.ntbIncrement);
            this.gbMonochromator.Controls.Add(this.lblIncrement);
            this.gbMonochromator.Controls.Add(this.ntbStartWavelength);
            this.gbMonochromator.Controls.Add(this.ntbEndWavelength);
            this.gbMonochromator.Controls.Add(this.lblEnd);
            this.gbMonochromator.Controls.Add(this.lblStart);
            this.gbMonochromator.Location = new System.Drawing.Point(57, 12);
            this.gbMonochromator.Name = "gbMonochromator";
            this.gbMonochromator.Size = new System.Drawing.Size(361, 112);
            this.gbMonochromator.TabIndex = 10;
            this.gbMonochromator.TabStop = false;
            this.gbMonochromator.Text = "Monochromator";
            // 
            // ntbIncrement
            // 
            this.ntbIncrement.AllowDecimal = false;
            this.ntbIncrement.AllowNegative = false;
            this.ntbIncrement.Location = new System.Drawing.Point(185, 82);
            this.ntbIncrement.Name = "ntbIncrement";
            this.ntbIncrement.Size = new System.Drawing.Size(100, 20);
            this.ntbIncrement.TabIndex = 9;
            // 
            // lblIncrement
            // 
            this.lblIncrement.AutoSize = true;
            this.lblIncrement.Location = new System.Drawing.Point(18, 86);
            this.lblIncrement.Name = "lblIncrement";
            this.lblIncrement.Size = new System.Drawing.Size(141, 13);
            this.lblIncrement.TabIndex = 8;
            this.lblIncrement.Text = "Wavelength Increment (nm):";
            // 
            // ntbStartWavelength
            // 
            this.ntbStartWavelength.AllowDecimal = false;
            this.ntbStartWavelength.AllowNegative = false;
            this.ntbStartWavelength.Location = new System.Drawing.Point(185, 22);
            this.ntbStartWavelength.Name = "ntbStartWavelength";
            this.ntbStartWavelength.Size = new System.Drawing.Size(100, 20);
            this.ntbStartWavelength.TabIndex = 6;
            // 
            // ntbEndWavelength
            // 
            this.ntbEndWavelength.AllowDecimal = false;
            this.ntbEndWavelength.AllowNegative = false;
            this.ntbEndWavelength.Location = new System.Drawing.Point(185, 52);
            this.ntbEndWavelength.Name = "ntbEndWavelength";
            this.ntbEndWavelength.Size = new System.Drawing.Size(100, 20);
            this.ntbEndWavelength.TabIndex = 7;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(18, 56);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(127, 13);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "Ending Wavelength (nm):";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(18, 26);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(130, 13);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "Starting Wavelength (nm):";
            // 
            // gbDAQ
            // 
            this.gbDAQ.Controls.Add(this.lblDelay);
            this.gbDAQ.Controls.Add(this.lblSamples);
            this.gbDAQ.Controls.Add(this.ntbDelay);
            this.gbDAQ.Controls.Add(this.ntbSamplesToAvg);
            this.gbDAQ.Location = new System.Drawing.Point(57, 132);
            this.gbDAQ.Name = "gbDAQ";
            this.gbDAQ.Size = new System.Drawing.Size(361, 88);
            this.gbDAQ.TabIndex = 11;
            this.gbDAQ.TabStop = false;
            this.gbDAQ.Text = "Data Acquisition";
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(18, 28);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(105, 13);
            this.lblDelay.TabIndex = 4;
            this.lblDelay.Text = "Acquisition Delay (s):";
            // 
            // lblSamples
            // 
            this.lblSamples.AutoSize = true;
            this.lblSamples.Location = new System.Drawing.Point(18, 59);
            this.lblSamples.Name = "lblSamples";
            this.lblSamples.Size = new System.Drawing.Size(105, 13);
            this.lblSamples.TabIndex = 5;
            this.lblSamples.Text = "Samples to Average:";
            // 
            // ntbDelay
            // 
            this.ntbDelay.AllowDecimal = true;
            this.ntbDelay.AllowNegative = false;
            this.ntbDelay.Location = new System.Drawing.Point(185, 24);
            this.ntbDelay.Name = "ntbDelay";
            this.ntbDelay.Size = new System.Drawing.Size(100, 20);
            this.ntbDelay.TabIndex = 8;
            // 
            // ntbSamplesToAvg
            // 
            this.ntbSamplesToAvg.AllowDecimal = false;
            this.ntbSamplesToAvg.AllowNegative = false;
            this.ntbSamplesToAvg.Location = new System.Drawing.Point(185, 55);
            this.ntbSamplesToAvg.Name = "ntbSamplesToAvg";
            this.ntbSamplesToAvg.Size = new System.Drawing.Size(100, 20);
            this.ntbSamplesToAvg.TabIndex = 9;
            // 
            // pageHardwareSetup
            // 
            this.pageHardwareSetup.Controls.Add(this.bgPotStat);
            this.pageHardwareSetup.Controls.Add(this.gbLockIn);
            this.pageHardwareSetup.Description = "Select the Potentiostat and Lock-In Amplifier parameters from the drop-down lists" +
    " below.";
            this.pageHardwareSetup.Location = new System.Drawing.Point(11, 71);
            this.pageHardwareSetup.Name = "pageHardwareSetup";
            this.pageHardwareSetup.NextPage = this.pageSoftwareLIASetup;
            this.pageHardwareSetup.PreviousPage = this.pageSpectrumSetup;
            this.pageHardwareSetup.Size = new System.Drawing.Size(475, 233);
            this.pageHardwareSetup.TabIndex = 6;
            this.pageHardwareSetup.Text = "Hardware Setup";
            // 
            // bgPotStat
            // 
            this.bgPotStat.Controls.Add(this.cbCurrentUnits);
            this.bgPotStat.Controls.Add(this.cbCurrentRange);
            this.bgPotStat.Controls.Add(this.lblCurrentRange);
            this.bgPotStat.Location = new System.Drawing.Point(57, 24);
            this.bgPotStat.Name = "bgPotStat";
            this.bgPotStat.Size = new System.Drawing.Size(361, 67);
            this.bgPotStat.TabIndex = 2;
            this.bgPotStat.TabStop = false;
            this.bgPotStat.Text = "Potentiostat";
            // 
            // cbCurrentUnits
            // 
            this.cbCurrentUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentUnits.FormattingEnabled = true;
            this.cbCurrentUnits.Items.AddRange(new object[] {
            "Nanoamps",
            "Microamps",
            "Milliamps",
            "Amps"});
            this.cbCurrentUnits.Location = new System.Drawing.Point(215, 26);
            this.cbCurrentUnits.Name = "cbCurrentUnits";
            this.cbCurrentUnits.Size = new System.Drawing.Size(121, 21);
            this.cbCurrentUnits.TabIndex = 2;
            // 
            // cbCurrentRange
            // 
            this.cbCurrentRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentRange.FormattingEnabled = true;
            this.cbCurrentRange.Items.AddRange(new object[] {
            "0.02",
            "0.05",
            "0.1",
            "0.2",
            "0.5",
            "1",
            "2",
            "5",
            "10"});
            this.cbCurrentRange.Location = new System.Drawing.Point(107, 26);
            this.cbCurrentRange.Name = "cbCurrentRange";
            this.cbCurrentRange.Size = new System.Drawing.Size(92, 21);
            this.cbCurrentRange.TabIndex = 1;
            // 
            // lblCurrentRange
            // 
            this.lblCurrentRange.AutoSize = true;
            this.lblCurrentRange.Location = new System.Drawing.Point(21, 30);
            this.lblCurrentRange.Name = "lblCurrentRange";
            this.lblCurrentRange.Size = new System.Drawing.Size(79, 13);
            this.lblCurrentRange.TabIndex = 0;
            this.lblCurrentRange.Text = "Current Range:";
            // 
            // gbLockIn
            // 
            this.gbLockIn.Controls.Add(this.cbLIAType);
            this.gbLockIn.Controls.Add(this.lblLIAType);
            this.gbLockIn.Controls.Add(this.cbSensitivityUnits);
            this.gbLockIn.Controls.Add(this.cbSensitivity);
            this.gbLockIn.Controls.Add(this.lblSensitivity);
            this.gbLockIn.Location = new System.Drawing.Point(57, 102);
            this.gbLockIn.Name = "gbLockIn";
            this.gbLockIn.Size = new System.Drawing.Size(361, 106);
            this.gbLockIn.TabIndex = 3;
            this.gbLockIn.TabStop = false;
            this.gbLockIn.Text = "Lock-In Amplifier";
            // 
            // cbLIAType
            // 
            this.cbLIAType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLIAType.FormattingEnabled = true;
            this.cbLIAType.Items.AddRange(new object[] {
            "None",
            "Hardware",
            "Software"});
            this.cbLIAType.Location = new System.Drawing.Point(107, 27);
            this.cbLIAType.Name = "cbLIAType";
            this.cbLIAType.Size = new System.Drawing.Size(229, 21);
            this.cbLIAType.TabIndex = 4;
            this.cbLIAType.SelectedIndexChanged += new System.EventHandler(this.cbLIAType_SelectedIndexChanged);
            // 
            // lblLIAType
            // 
            this.lblLIAType.AutoSize = true;
            this.lblLIAType.Location = new System.Drawing.Point(66, 31);
            this.lblLIAType.Name = "lblLIAType";
            this.lblLIAType.Size = new System.Drawing.Size(34, 13);
            this.lblLIAType.TabIndex = 3;
            this.lblLIAType.Text = "Type:";
            // 
            // cbSensitivityUnits
            // 
            this.cbSensitivityUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSensitivityUnits.FormattingEnabled = true;
            this.cbSensitivityUnits.Items.AddRange(new object[] {
            "Nanovolts",
            "Microvolts",
            "Millivolts",
            "Volts"});
            this.cbSensitivityUnits.Location = new System.Drawing.Point(215, 64);
            this.cbSensitivityUnits.Name = "cbSensitivityUnits";
            this.cbSensitivityUnits.Size = new System.Drawing.Size(121, 21);
            this.cbSensitivityUnits.TabIndex = 2;
            // 
            // cbSensitivity
            // 
            this.cbSensitivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSensitivity.FormattingEnabled = true;
            this.cbSensitivity.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "20",
            "50",
            "100",
            "200",
            "500"});
            this.cbSensitivity.Location = new System.Drawing.Point(107, 64);
            this.cbSensitivity.Name = "cbSensitivity";
            this.cbSensitivity.Size = new System.Drawing.Size(92, 21);
            this.cbSensitivity.TabIndex = 1;
            // 
            // lblSensitivity
            // 
            this.lblSensitivity.AutoSize = true;
            this.lblSensitivity.Location = new System.Drawing.Point(43, 68);
            this.lblSensitivity.Name = "lblSensitivity";
            this.lblSensitivity.Size = new System.Drawing.Size(57, 13);
            this.lblSensitivity.TabIndex = 0;
            this.lblSensitivity.Text = "Sensitivity:";
            // 
            // pageSoftwareLIASetup
            // 
            this.pageSoftwareLIASetup.Controls.Add(this.gbLaserPosition);
            this.pageSoftwareLIASetup.Controls.Add(this.gbLIAOutput);
            this.pageSoftwareLIASetup.Description = "Set the Monochromator to the peak Wavelength of the spectrum in order to acquire " +
    "a lock on the signal.";
            this.pageSoftwareLIASetup.Location = new System.Drawing.Point(11, 71);
            this.pageSoftwareLIASetup.Name = "pageSoftwareLIASetup";
            this.pageSoftwareLIASetup.NextPage = this.pageFileName;
            this.pageSoftwareLIASetup.PreviousPage = this.pageHardwareSetup;
            this.pageSoftwareLIASetup.Size = new System.Drawing.Size(475, 233);
            this.pageSoftwareLIASetup.TabIndex = 1004;
            this.pageSoftwareLIASetup.Text = "Software Lock-In Amplifier Setup";
            // 
            // gbLaserPosition
            // 
            this.gbLaserPosition.Controls.Add(this.lblYAxisPos);
            this.gbLaserPosition.Controls.Add(this.spinYAxisPos);
            this.gbLaserPosition.Controls.Add(this.lblXAxisPos);
            this.gbLaserPosition.Controls.Add(this.spinXAxisPos);
            this.gbLaserPosition.Location = new System.Drawing.Point(34, 132);
            this.gbLaserPosition.Name = "gbLaserPosition";
            this.gbLaserPosition.Size = new System.Drawing.Size(408, 77);
            this.gbLaserPosition.TabIndex = 2;
            this.gbLaserPosition.TabStop = false;
            this.gbLaserPosition.Text = "Laser Position";
            // 
            // lblYAxisPos
            // 
            this.lblYAxisPos.AutoSize = true;
            this.lblYAxisPos.Location = new System.Drawing.Point(231, 36);
            this.lblYAxisPos.Name = "lblYAxisPos";
            this.lblYAxisPos.Size = new System.Drawing.Size(39, 13);
            this.lblYAxisPos.TabIndex = 0;
            this.lblYAxisPos.Text = "Y-Axis:";
            // 
            // spinYAxisPos
            // 
            this.spinYAxisPos.Location = new System.Drawing.Point(276, 32);
            this.spinYAxisPos.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinYAxisPos.Name = "spinYAxisPos";
            this.spinYAxisPos.Size = new System.Drawing.Size(62, 20);
            this.spinYAxisPos.TabIndex = 2;
            this.spinYAxisPos.ValueChanged += new System.EventHandler(this.spinYAxis_ValueChanged);
            // 
            // lblXAxisPos
            // 
            this.lblXAxisPos.AutoSize = true;
            this.lblXAxisPos.Location = new System.Drawing.Point(42, 36);
            this.lblXAxisPos.Name = "lblXAxisPos";
            this.lblXAxisPos.Size = new System.Drawing.Size(39, 13);
            this.lblXAxisPos.TabIndex = 0;
            this.lblXAxisPos.Text = "X-Axis:";
            // 
            // spinXAxisPos
            // 
            this.spinXAxisPos.Location = new System.Drawing.Point(87, 32);
            this.spinXAxisPos.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinXAxisPos.Name = "spinXAxisPos";
            this.spinXAxisPos.Size = new System.Drawing.Size(62, 20);
            this.spinXAxisPos.TabIndex = 1;
            this.spinXAxisPos.ValueChanged += new System.EventHandler(this.spinXAxis_ValueChanged);
            // 
            // gbLIAOutput
            // 
            this.gbLIAOutput.Controls.Add(this.lblLIAValue);
            this.gbLIAOutput.Controls.Add(this.lblFrequency);
            this.gbLIAOutput.Controls.Add(this.lblLIAValueLabel);
            this.gbLIAOutput.Controls.Add(this.lblFrequencyLabel);
            this.gbLIAOutput.Location = new System.Drawing.Point(34, 20);
            this.gbLIAOutput.Name = "gbLIAOutput";
            this.gbLIAOutput.Size = new System.Drawing.Size(408, 88);
            this.gbLIAOutput.TabIndex = 1;
            this.gbLIAOutput.TabStop = false;
            this.gbLIAOutput.Text = "Software Lock-In Amp Output";
            // 
            // lblLIAValue
            // 
            this.lblLIAValue.AutoSize = true;
            this.lblLIAValue.Location = new System.Drawing.Point(172, 56);
            this.lblLIAValue.Name = "lblLIAValue";
            this.lblLIAValue.Size = new System.Drawing.Size(13, 13);
            this.lblLIAValue.TabIndex = 0;
            this.lblLIAValue.Text = "0";
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(172, 28);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(13, 13);
            this.lblFrequency.TabIndex = 0;
            this.lblFrequency.Text = "0";
            // 
            // lblLIAValueLabel
            // 
            this.lblLIAValueLabel.AutoSize = true;
            this.lblLIAValueLabel.Location = new System.Drawing.Point(36, 56);
            this.lblLIAValueLabel.Name = "lblLIAValueLabel";
            this.lblLIAValueLabel.Size = new System.Drawing.Size(131, 13);
            this.lblLIAValueLabel.TabIndex = 0;
            this.lblLIAValueLabel.Text = "Input Signal Value (Amps):";
            // 
            // lblFrequencyLabel
            // 
            this.lblFrequencyLabel.AutoSize = true;
            this.lblFrequencyLabel.Location = new System.Drawing.Point(26, 28);
            this.lblFrequencyLabel.Name = "lblFrequencyLabel";
            this.lblFrequencyLabel.Size = new System.Drawing.Size(141, 13);
            this.lblFrequencyLabel.TabIndex = 0;
            this.lblFrequencyLabel.Text = "Input Signal Frequency (Hz):";
            // 
            // pageFileName
            // 
            this.pageFileName.Controls.Add(this.btnFileSave);
            this.pageFileName.Controls.Add(this.txtFileName);
            this.pageFileName.Description = "Enter the File Name to be used to store the acquired data and click Next.";
            this.pageFileName.Location = new System.Drawing.Point(11, 71);
            this.pageFileName.Name = "pageFileName";
            this.pageFileName.NextPage = this.pageComplete;
            this.pageFileName.PreviousPage = this.pageSoftwareLIASetup;
            this.pageFileName.Size = new System.Drawing.Size(475, 233);
            this.pageFileName.TabIndex = 7;
            this.pageFileName.Text = "Enter File Name";
            // 
            // btnFileSave
            // 
            this.btnFileSave.Image = global::PhotoCurrent.Properties.Resources.OpenSpectrum;
            this.btnFileSave.Location = new System.Drawing.Point(376, 54);
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(31, 23);
            this.btnFileSave.TabIndex = 3;
            this.btnFileSave.UseVisualStyleBackColor = true;
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(68, 56);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(307, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // pageComplete
            // 
            this.pageComplete.Controls.Add(this.cbNoDisplay);
            this.pageComplete.Controls.Add(this.cbAddToActive);
            this.pageComplete.Controls.Add(this.lblSummary);
            this.pageComplete.Description = "Click the Finish button to begin Data Acquisition.";
            this.pageComplete.Location = new System.Drawing.Point(11, 71);
            this.pageComplete.Name = "pageComplete";
            this.pageComplete.PreviousPage = this.pageFileName;
            this.pageComplete.Size = new System.Drawing.Size(475, 233);
            this.pageComplete.TabIndex = 8;
            this.pageComplete.Text = "Ready to Acquire New Spectrum";
            // 
            // cbAddToActive
            // 
            this.cbAddToActive.AutoSize = true;
            this.cbAddToActive.Enabled = false;
            this.cbAddToActive.Location = new System.Drawing.Point(139, 189);
            this.cbAddToActive.Name = "cbAddToActive";
            this.cbAddToActive.Size = new System.Drawing.Size(205, 17);
            this.cbAddToActive.TabIndex = 1;
            this.cbAddToActive.Text = "Add New Spectrum to Active Window";
            this.cbAddToActive.UseVisualStyleBackColor = true;
            // 
            // lblSummary
            // 
            this.lblSummary.Location = new System.Drawing.Point(136, 7);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(331, 176);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary:";
            // 
            // cbNoDisplay
            // 
            this.cbNoDisplay.AutoSize = true;
            this.cbNoDisplay.Location = new System.Drawing.Point(139, 211);
            this.cbNoDisplay.Name = "cbNoDisplay";
            this.cbNoDisplay.Size = new System.Drawing.Size(208, 17);
            this.cbNoDisplay.TabIndex = 3;
            this.cbNoDisplay.Text = "Display Spectrum only after Acquisition";
            this.cbNoDisplay.UseVisualStyleBackColor = true;
            // 
            // NewSpectrum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 362);
            this.ControlBox = false;
            this.Controls.Add(this.newSpecWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewSpectrum";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Spectrum";
            this.newSpecWizard.ResumeLayout(false);
            this.pageSpectrumType.ResumeLayout(false);
            this.pageSpectrumType.PerformLayout();
            this.pagePositionSetup.ResumeLayout(false);
            this.pagePositionSetup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinXPosStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYPosStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinXPosEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYPosEnd)).EndInit();
            this.pageSpectrumSetup.ResumeLayout(false);
            this.gbMonochromator.ResumeLayout(false);
            this.gbMonochromator.PerformLayout();
            this.gbDAQ.ResumeLayout(false);
            this.gbDAQ.PerformLayout();
            this.pageHardwareSetup.ResumeLayout(false);
            this.bgPotStat.ResumeLayout(false);
            this.bgPotStat.PerformLayout();
            this.gbLockIn.ResumeLayout(false);
            this.gbLockIn.PerformLayout();
            this.pageSoftwareLIASetup.ResumeLayout(false);
            this.gbLaserPosition.ResumeLayout(false);
            this.gbLaserPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinYAxisPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinXAxisPos)).EndInit();
            this.gbLIAOutput.ResumeLayout(false);
            this.gbLIAOutput.PerformLayout();
            this.pageFileName.ResumeLayout(false);
            this.pageFileName.PerformLayout();
            this.pageComplete.ResumeLayout(false);
            this.pageComplete.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Divelements.WizardFramework.Wizard newSpecWizard;
        private Divelements.WizardFramework.WizardPage pageSpectrumType;
        private System.Windows.Forms.RadioButton rb3DPosition;
        private System.Windows.Forms.RadioButton rbBackground;
        private System.Windows.Forms.RadioButton rbTime;
        private System.Windows.Forms.RadioButton rbWavelength;
        private Divelements.WizardFramework.WizardPage pageSpectrumSetup;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblSamples;
        private System.Windows.Forms.Label lblDelay;
        private RDH2.Utilities.Controls.NumericTextBox ntbSamplesToAvg;
        private RDH2.Utilities.Controls.NumericTextBox ntbDelay;
        private System.Windows.Forms.GroupBox gbMonochromator;
        private RDH2.Utilities.Controls.NumericTextBox ntbStartWavelength;
        private RDH2.Utilities.Controls.NumericTextBox ntbEndWavelength;
        private RDH2.Utilities.Controls.NumericTextBox ntbIncrement;
        private System.Windows.Forms.Label lblIncrement;
        private System.Windows.Forms.GroupBox gbDAQ;
        private Divelements.WizardFramework.WizardPage pageHardwareSetup;
        private System.Windows.Forms.GroupBox bgPotStat;
        private System.Windows.Forms.GroupBox gbLockIn;
        private System.Windows.Forms.ComboBox cbCurrentUnits;
        private System.Windows.Forms.ComboBox cbCurrentRange;
        private System.Windows.Forms.Label lblCurrentRange;
        private System.Windows.Forms.Label lblSensitivity;
        private System.Windows.Forms.ComboBox cbSensitivityUnits;
        private System.Windows.Forms.ComboBox cbSensitivity;
        private Divelements.WizardFramework.WizardPage pageFileName;
        private System.Windows.Forms.Button btnFileSave;
        private System.Windows.Forms.TextBox txtFileName;
        private Divelements.WizardFramework.WizardPage pageComplete;
        private System.Windows.Forms.CheckBox cbAddToActive;
        private Divelements.WizardFramework.WizardPage pagePositionSetup;
        private System.Windows.Forms.NumericUpDown spinXPosStart;
        private RDH2.Utilities.Controls.NumericTextBox ntbStepSize;
        private System.Windows.Forms.Label lblXPosStart;
        private System.Windows.Forms.Label lblYPosStart;
        private System.Windows.Forms.NumericUpDown spinYPosStart;
        private System.Windows.Forms.Label lblStepSize;
        private System.Windows.Forms.Label lblXPosEnd;
        private System.Windows.Forms.NumericUpDown spinXPosEnd;
        private System.Windows.Forms.Label lblYPosEnd;
        private System.Windows.Forms.NumericUpDown spinYPosEnd;
        private System.Windows.Forms.RadioButton rbLEGO;
        private System.Windows.Forms.ComboBox cbLIAType;
        private System.Windows.Forms.Label lblLIAType;
        private System.Windows.Forms.Label lblSummary;
        private Divelements.WizardFramework.WizardPage pageSoftwareLIASetup;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Label lblRowsLabel;
        private System.Windows.Forms.Label lblCols;
        private System.Windows.Forms.Label lblColsLabel;
        private System.Windows.Forms.Label lblFrequencyLabel;
        private System.Windows.Forms.GroupBox gbLIAOutput;
        private System.Windows.Forms.Label lblLIAValueLabel;
        private System.Windows.Forms.Label lblLIAValue;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.GroupBox gbLaserPosition;
        private System.Windows.Forms.NumericUpDown spinXAxisPos;
        private System.Windows.Forms.Label lblYAxisPos;
        private System.Windows.Forms.NumericUpDown spinYAxisPos;
        private System.Windows.Forms.Label lblXAxisPos;
        private System.Windows.Forms.CheckBox cbNoDisplay;
    }
}