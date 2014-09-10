namespace PhotoCurrent.Wizards
{
    partial class Scaling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scaling));
            this.scalingWizard = new Divelements.WizardFramework.Wizard();
            this.pageSelectRawSource = new Divelements.WizardFramework.WizardPage();
            this.rbExternal = new System.Windows.Forms.RadioButton();
            this.rbChart = new System.Windows.Forms.RadioButton();
            this.pageListBoxSelectFiles = new Divelements.WizardFramework.WizardPage();
            this.elbRawFiles = new RDH2.Utilities.Controls.EditableListBox();
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.pageFileSaveNames = new Divelements.WizardFramework.WizardPage();
            this.cbOverwriteIPCE = new System.Windows.Forms.CheckBox();
            this.elbSaveFiles = new RDH2.Utilities.Controls.EditableListBox();
            this.pageBackgroundFile = new Divelements.WizardFramework.WizardPage();
            this.lblBkgPath = new System.Windows.Forms.Label();
            this.btnBrowseBkg = new System.Windows.Forms.Button();
            this.tbBkgPath = new System.Windows.Forms.TextBox();
            this.gbLightPower = new System.Windows.Forms.GroupBox();
            this.ntbPower = new RDH2.Utilities.Controls.NumericTextBox();
            this.ntbWavelength = new RDH2.Utilities.Controls.NumericTextBox();
            this.cbPowerUnit = new System.Windows.Forms.ComboBox();
            this.lblPower = new System.Windows.Forms.Label();
            this.lblWavelength = new System.Windows.Forms.Label();
            this.pageComplete = new Divelements.WizardFramework.WizardPage();
            this.cbOpenIPCE = new System.Windows.Forms.CheckBox();
            this.pageProcessScaling = new Divelements.WizardFramework.WizardPage();
            this.lblCurrentTask = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progScale = new System.Windows.Forms.ProgressBar();
            this.lblSummary = new System.Windows.Forms.Label();
            this.scalingWizard.SuspendLayout();
            this.pageSelectRawSource.SuspendLayout();
            this.pageListBoxSelectFiles.SuspendLayout();
            this.pageFileSaveNames.SuspendLayout();
            this.pageBackgroundFile.SuspendLayout();
            this.gbLightPower.SuspendLayout();
            this.pageComplete.SuspendLayout();
            this.pageProcessScaling.SuspendLayout();
            this.SuspendLayout();
            // 
            // scalingWizard
            // 
            this.scalingWizard.BackColor = System.Drawing.SystemColors.Control;
            this.scalingWizard.BannerImage = ((System.Drawing.Image)(resources.GetObject("scalingWizard.BannerImage")));
            this.scalingWizard.Controls.Add(this.pageSelectRawSource);
            this.scalingWizard.Controls.Add(this.pageListBoxSelectFiles);
            this.scalingWizard.Controls.Add(this.pageFileSaveNames);
            this.scalingWizard.Controls.Add(this.pageBackgroundFile);
            this.scalingWizard.Controls.Add(this.pageProcessScaling);
            this.scalingWizard.Controls.Add(this.pageComplete);
            this.scalingWizard.Cursor = System.Windows.Forms.Cursors.Default;
            this.scalingWizard.FinishText = "&Finish";
            this.scalingWizard.Location = new System.Drawing.Point(0, 0);
            this.scalingWizard.Name = "scalingWizard";
            this.scalingWizard.OwnerForm = this;
            this.scalingWizard.Size = new System.Drawing.Size(497, 362);
            this.scalingWizard.TabIndex = 0;
            this.scalingWizard.Text = "Scale to IPCE Wizard";
            this.scalingWizard.UserExperienceType = Divelements.WizardFramework.WizardUserExperienceType.Wizard97;
            // 
            // pageSelectRawSource
            // 
            this.pageSelectRawSource.Controls.Add(this.rbExternal);
            this.pageSelectRawSource.Controls.Add(this.rbChart);
            this.pageSelectRawSource.Description = "Choose the Source of the Photocurrent Data to be scaled to IPCE:  the Active Char" +
                "t window or external RAW / TXT Files on disk.";
            this.pageSelectRawSource.Location = new System.Drawing.Point(11, 71);
            this.pageSelectRawSource.Name = "pageSelectRawSource";
            this.pageSelectRawSource.NextPage = this.pageListBoxSelectFiles;
            this.pageSelectRawSource.Size = new System.Drawing.Size(475, 233);
            this.pageSelectRawSource.TabIndex = 4;
            this.pageSelectRawSource.Text = "Choose the Photocurrent File Source";
            // 
            // rbExternal
            // 
            this.rbExternal.AutoSize = true;
            this.rbExternal.Location = new System.Drawing.Point(99, 125);
            this.rbExternal.Name = "rbExternal";
            this.rbExternal.Size = new System.Drawing.Size(136, 17);
            this.rbExternal.TabIndex = 3;
            this.rbExternal.Text = "Other RAW / TXT Files";
            this.rbExternal.UseVisualStyleBackColor = true;
            // 
            // rbChart
            // 
            this.rbChart.AutoSize = true;
            this.rbChart.Checked = true;
            this.rbChart.Location = new System.Drawing.Point(99, 91);
            this.rbChart.Name = "rbChart";
            this.rbChart.Size = new System.Drawing.Size(181, 17);
            this.rbChart.TabIndex = 2;
            this.rbChart.TabStop = true;
            this.rbChart.Text = "RAW / TXT Data in Active Chart";
            this.rbChart.UseVisualStyleBackColor = true;
            // 
            // pageListBoxSelectFiles
            // 
            this.pageListBoxSelectFiles.Controls.Add(this.elbRawFiles);
            this.pageListBoxSelectFiles.Controls.Add(this.btnOpenFileDialog);
            this.pageListBoxSelectFiles.Controls.Add(this.cbSelectAll);
            this.pageListBoxSelectFiles.Description = "Choose the RAW / TXT Data Files from the Active Chart to scale to IPCE.  Or, chec" +
                "k Select All to scale all of the data.";
            this.pageListBoxSelectFiles.Location = new System.Drawing.Point(11, 71);
            this.pageListBoxSelectFiles.Name = "pageListBoxSelectFiles";
            this.pageListBoxSelectFiles.NextPage = this.pageFileSaveNames;
            this.pageListBoxSelectFiles.PreviousPage = this.pageSelectRawSource;
            this.pageListBoxSelectFiles.Size = new System.Drawing.Size(475, 233);
            this.pageListBoxSelectFiles.TabIndex = 5;
            this.pageListBoxSelectFiles.Text = "Choose the RAW Files to Scale";
            // 
            // elbRawFiles
            // 
            this.elbRawFiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.elbRawFiles.CheckBoxes = true;
            this.elbRawFiles.FullRowSelect = true;
            this.elbRawFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.elbRawFiles.HideSelection = false;
            this.elbRawFiles.LabelEdit = true;
            this.elbRawFiles.LabelWrap = false;
            this.elbRawFiles.Location = new System.Drawing.Point(40, 31);
            this.elbRawFiles.MultiSelect = false;
            this.elbRawFiles.Name = "elbRawFiles";
            this.elbRawFiles.ShowItemToolTips = true;
            this.elbRawFiles.Size = new System.Drawing.Size(394, 198);
            this.elbRawFiles.TabIndex = 5;
            this.elbRawFiles.UseCompatibleStateImageBehavior = false;
            this.elbRawFiles.View = System.Windows.Forms.View.Details;
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileDialog.Image")));
            this.btnOpenFileDialog.Location = new System.Drawing.Point(406, 4);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(29, 23);
            this.btnOpenFileDialog.TabIndex = 4;
            this.btnOpenFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenFileDialog.Click += new System.EventHandler(this.btnOpenFileDialog_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Checked = true;
            this.cbSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelectAll.Location = new System.Drawing.Point(43, 7);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(94, 17);
            this.cbSelectAll.TabIndex = 2;
            this.cbSelectAll.Text = "Select All Files";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // pageFileSaveNames
            // 
            this.pageFileSaveNames.Controls.Add(this.cbOverwriteIPCE);
            this.pageFileSaveNames.Controls.Add(this.elbSaveFiles);
            this.pageFileSaveNames.Description = "Confirm the file names below for the scaled IPCE CSV or TXT files.  If necessary," +
                " change the file names by clicking on them and editing them.";
            this.pageFileSaveNames.Location = new System.Drawing.Point(11, 71);
            this.pageFileSaveNames.Name = "pageFileSaveNames";
            this.pageFileSaveNames.NextPage = this.pageBackgroundFile;
            this.pageFileSaveNames.PreviousPage = this.pageListBoxSelectFiles;
            this.pageFileSaveNames.Size = new System.Drawing.Size(475, 233);
            this.pageFileSaveNames.TabIndex = 6;
            this.pageFileSaveNames.Text = "Confirm the Scaled File Names";
            // 
            // cbOverwriteIPCE
            // 
            this.cbOverwriteIPCE.AutoSize = true;
            this.cbOverwriteIPCE.Checked = true;
            this.cbOverwriteIPCE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOverwriteIPCE.Location = new System.Drawing.Point(43, 7);
            this.cbOverwriteIPCE.Name = "cbOverwriteIPCE";
            this.cbOverwriteIPCE.Size = new System.Drawing.Size(134, 17);
            this.cbOverwriteIPCE.TabIndex = 3;
            this.cbOverwriteIPCE.Text = "Overwrite Existing Files";
            this.cbOverwriteIPCE.UseVisualStyleBackColor = true;
            // 
            // elbSaveFiles
            // 
            this.elbSaveFiles.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.elbSaveFiles.FullRowSelect = true;
            this.elbSaveFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.elbSaveFiles.HideSelection = false;
            this.elbSaveFiles.LabelEdit = true;
            this.elbSaveFiles.LabelWrap = false;
            this.elbSaveFiles.Location = new System.Drawing.Point(40, 31);
            this.elbSaveFiles.MultiSelect = false;
            this.elbSaveFiles.Name = "elbSaveFiles";
            this.elbSaveFiles.ShowItemToolTips = true;
            this.elbSaveFiles.Size = new System.Drawing.Size(394, 198);
            this.elbSaveFiles.TabIndex = 2;
            this.elbSaveFiles.UseCompatibleStateImageBehavior = false;
            this.elbSaveFiles.View = System.Windows.Forms.View.Details;
            // 
            // pageBackgroundFile
            // 
            this.pageBackgroundFile.Controls.Add(this.lblBkgPath);
            this.pageBackgroundFile.Controls.Add(this.btnBrowseBkg);
            this.pageBackgroundFile.Controls.Add(this.tbBkgPath);
            this.pageBackgroundFile.Controls.Add(this.gbLightPower);
            this.pageBackgroundFile.Description = "Select the BKG Background file that contains all of the points for the RAW files " +
                "to scale to IPCE.  Then, set the Light Source Power and Units.";
            this.pageBackgroundFile.Location = new System.Drawing.Point(11, 71);
            this.pageBackgroundFile.Name = "pageBackgroundFile";
            this.pageBackgroundFile.NextPage = this.pageProcessScaling;
            this.pageBackgroundFile.PreviousPage = this.pageFileSaveNames;
            this.pageBackgroundFile.Size = new System.Drawing.Size(475, 233);
            this.pageBackgroundFile.TabIndex = 7;
            this.pageBackgroundFile.Text = "Set the Background Information";
            // 
            // lblBkgPath
            // 
            this.lblBkgPath.AutoSize = true;
            this.lblBkgPath.Location = new System.Drawing.Point(50, 41);
            this.lblBkgPath.Name = "lblBkgPath";
            this.lblBkgPath.Size = new System.Drawing.Size(93, 13);
            this.lblBkgPath.TabIndex = 4;
            this.lblBkgPath.Text = "Background Path:";
            // 
            // btnBrowseBkg
            // 
            this.btnBrowseBkg.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseBkg.Image")));
            this.btnBrowseBkg.Location = new System.Drawing.Point(396, 55);
            this.btnBrowseBkg.Name = "btnBrowseBkg";
            this.btnBrowseBkg.Size = new System.Drawing.Size(29, 23);
            this.btnBrowseBkg.TabIndex = 2;
            this.btnBrowseBkg.UseVisualStyleBackColor = true;
            this.btnBrowseBkg.Click += new System.EventHandler(this.btnBrowseBkg_Click);
            // 
            // tbBkgPath
            // 
            this.tbBkgPath.Location = new System.Drawing.Point(53, 57);
            this.tbBkgPath.Name = "tbBkgPath";
            this.tbBkgPath.ReadOnly = true;
            this.tbBkgPath.Size = new System.Drawing.Size(343, 20);
            this.tbBkgPath.TabIndex = 1;
            // 
            // gbLightPower
            // 
            this.gbLightPower.Controls.Add(this.ntbPower);
            this.gbLightPower.Controls.Add(this.ntbWavelength);
            this.gbLightPower.Controls.Add(this.cbPowerUnit);
            this.gbLightPower.Controls.Add(this.lblPower);
            this.gbLightPower.Controls.Add(this.lblWavelength);
            this.gbLightPower.Location = new System.Drawing.Point(53, 93);
            this.gbLightPower.Name = "gbLightPower";
            this.gbLightPower.Size = new System.Drawing.Size(365, 98);
            this.gbLightPower.TabIndex = 10;
            this.gbLightPower.TabStop = false;
            this.gbLightPower.Text = "Light Source";
            // 
            // ntbPower
            // 
            this.ntbPower.AllowDecimal = true;
            this.ntbPower.AllowNegative = false;
            this.ntbPower.Location = new System.Drawing.Point(162, 53);
            this.ntbPower.Name = "ntbPower";
            this.ntbPower.Size = new System.Drawing.Size(58, 20);
            this.ntbPower.TabIndex = 14;
            // 
            // ntbWavelength
            // 
            this.ntbWavelength.AllowDecimal = false;
            this.ntbWavelength.AllowNegative = false;
            this.ntbWavelength.Location = new System.Drawing.Point(24, 53);
            this.ntbWavelength.Name = "ntbWavelength";
            this.ntbWavelength.Size = new System.Drawing.Size(58, 20);
            this.ntbWavelength.TabIndex = 13;
            // 
            // cbPowerUnit
            // 
            this.cbPowerUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPowerUnit.FormattingEnabled = true;
            this.cbPowerUnit.Items.AddRange(new object[] {
            "Nanowatts",
            "Microwatts",
            "Milliwatts",
            "Watts"});
            this.cbPowerUnit.Location = new System.Drawing.Point(226, 53);
            this.cbPowerUnit.Name = "cbPowerUnit";
            this.cbPowerUnit.Size = new System.Drawing.Size(121, 21);
            this.cbPowerUnit.TabIndex = 5;
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(161, 34);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(37, 13);
            this.lblPower.TabIndex = 12;
            this.lblPower.Text = "Power";
            // 
            // lblWavelength
            // 
            this.lblWavelength.AutoSize = true;
            this.lblWavelength.Location = new System.Drawing.Point(21, 34);
            this.lblWavelength.Name = "lblWavelength";
            this.lblWavelength.Size = new System.Drawing.Size(88, 13);
            this.lblWavelength.TabIndex = 11;
            this.lblWavelength.Text = "Wavelength (nm)";
            // 
            // pageComplete
            // 
            this.pageComplete.Controls.Add(this.lblSummary);
            this.pageComplete.Controls.Add(this.cbOpenIPCE);
            this.pageComplete.Description = "";
            this.pageComplete.Location = new System.Drawing.Point(11, 71);
            this.pageComplete.Name = "pageComplete";
            this.pageComplete.Size = new System.Drawing.Size(475, 233);
            this.pageComplete.TabIndex = 9;
            this.pageComplete.Text = "Scale to IPCE Wizard has Completed";
            // 
            // cbOpenIPCE
            // 
            this.cbOpenIPCE.AutoSize = true;
            this.cbOpenIPCE.Checked = true;
            this.cbOpenIPCE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenIPCE.Location = new System.Drawing.Point(148, 125);
            this.cbOpenIPCE.Name = "cbOpenIPCE";
            this.cbOpenIPCE.Size = new System.Drawing.Size(176, 17);
            this.cbOpenIPCE.TabIndex = 1;
            this.cbOpenIPCE.Text = "Open IPCE Files in new window";
            this.cbOpenIPCE.UseVisualStyleBackColor = true;
            // 
            // pageProcessScaling
            // 
            this.pageProcessScaling.Controls.Add(this.lblCurrentTask);
            this.pageProcessScaling.Controls.Add(this.lblStatus);
            this.pageProcessScaling.Controls.Add(this.progScale);
            this.pageProcessScaling.Description = "The Scaling Wizard is ready to scale the selected RAW Files.  Click Next to begin" +
                " the processing, or clcik Back to change any of the parameters.";
            this.pageProcessScaling.Location = new System.Drawing.Point(11, 71);
            this.pageProcessScaling.Name = "pageProcessScaling";
            this.pageProcessScaling.NextPage = this.pageComplete;
            this.pageProcessScaling.PreviousPage = this.pageBackgroundFile;
            this.pageProcessScaling.Size = new System.Drawing.Size(475, 233);
            this.pageProcessScaling.TabIndex = 8;
            this.pageProcessScaling.Text = "Ready to Scale RAW Files";
            // 
            // lblCurrentTask
            // 
            this.lblCurrentTask.Location = new System.Drawing.Point(63, 68);
            this.lblCurrentTask.Name = "lblCurrentTask";
            this.lblCurrentTask.Size = new System.Drawing.Size(352, 85);
            this.lblCurrentTask.TabIndex = 4;
            this.lblCurrentTask.Text = "Click Next to begin processing...";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(60, 51);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status:";
            // 
            // progScale
            // 
            this.progScale.Location = new System.Drawing.Point(63, 158);
            this.progScale.Name = "progScale";
            this.progScale.Size = new System.Drawing.Size(352, 23);
            this.progScale.TabIndex = 2;
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(145, 40);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(161, 39);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary:\r\n- {0} Files Scaled Successfully\r\n- {1} Files Scaled Unsuccessfully\r\n";
            // 
            // Scaling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 362);
            this.ControlBox = false;
            this.Controls.Add(this.scalingWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Scaling";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scale to IPCE Wizard";
            this.scalingWizard.ResumeLayout(false);
            this.pageSelectRawSource.ResumeLayout(false);
            this.pageSelectRawSource.PerformLayout();
            this.pageListBoxSelectFiles.ResumeLayout(false);
            this.pageListBoxSelectFiles.PerformLayout();
            this.pageFileSaveNames.ResumeLayout(false);
            this.pageFileSaveNames.PerformLayout();
            this.pageBackgroundFile.ResumeLayout(false);
            this.pageBackgroundFile.PerformLayout();
            this.gbLightPower.ResumeLayout(false);
            this.gbLightPower.PerformLayout();
            this.pageComplete.ResumeLayout(false);
            this.pageComplete.PerformLayout();
            this.pageProcessScaling.ResumeLayout(false);
            this.pageProcessScaling.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Divelements.WizardFramework.Wizard scalingWizard;
        private Divelements.WizardFramework.WizardPage pageSelectRawSource;
        private System.Windows.Forms.RadioButton rbExternal;
        private System.Windows.Forms.RadioButton rbChart;
        private Divelements.WizardFramework.WizardPage pageListBoxSelectFiles;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.Button btnOpenFileDialog;
        private Divelements.WizardFramework.WizardPage pageFileSaveNames;
        private RDH2.Utilities.Controls.EditableListBox elbSaveFiles;
        private RDH2.Utilities.Controls.EditableListBox elbRawFiles;
        private System.Windows.Forms.CheckBox cbOverwriteIPCE;
        private Divelements.WizardFramework.WizardPage pageBackgroundFile;
        private System.Windows.Forms.Button btnBrowseBkg;
        private System.Windows.Forms.TextBox tbBkgPath;
        private System.Windows.Forms.Label lblBkgPath;
        private System.Windows.Forms.GroupBox gbLightPower;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label lblWavelength;
        private System.Windows.Forms.ComboBox cbPowerUnit;
        private Divelements.WizardFramework.WizardPage pageProcessScaling;
        private System.Windows.Forms.ProgressBar progScale;
        private System.Windows.Forms.Label lblCurrentTask;
        private System.Windows.Forms.Label lblStatus;
        private Divelements.WizardFramework.WizardPage pageComplete;
        private System.Windows.Forms.CheckBox cbOpenIPCE;
        private RDH2.Utilities.Controls.NumericTextBox ntbPower;
        private RDH2.Utilities.Controls.NumericTextBox ntbWavelength;
        private System.Windows.Forms.Label lblSummary;
    }
}