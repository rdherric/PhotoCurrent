namespace PhotoCurrent.Wizards
{
    partial class Export
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Export));
            this.exportWizard = new Divelements.WizardFramework.Wizard();
            this.pageFileSaveNames = new Divelements.WizardFramework.WizardPage();
            this.cbOverwriteR3D = new System.Windows.Forms.CheckBox();
            this.elbSaveFiles = new RDH2.Utilities.Controls.EditableListBox();
            this.pageListBoxSelectFiles = new Divelements.WizardFramework.WizardPage();
            this.elbRawFiles = new RDH2.Utilities.Controls.EditableListBox();
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.pageSelectOutput = new Divelements.WizardFramework.WizardPage();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.pageProgress = new Divelements.WizardFramework.WizardPage();
            this.lblCurrentTask = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progExport = new System.Windows.Forms.ProgressBar();
            this.pageSelectSource = new Divelements.WizardFramework.WizardPage();
            this.rbChart3D = new System.Windows.Forms.RadioButton();
            this.rbOther3D = new System.Windows.Forms.RadioButton();
            this.rbOther2D = new System.Windows.Forms.RadioButton();
            this.rbChart2D = new System.Windows.Forms.RadioButton();
            this.exportWizard.SuspendLayout();
            this.pageFileSaveNames.SuspendLayout();
            this.pageListBoxSelectFiles.SuspendLayout();
            this.pageSelectOutput.SuspendLayout();
            this.pageProgress.SuspendLayout();
            this.pageSelectSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // exportWizard
            // 
            this.exportWizard.BackColor = System.Drawing.SystemColors.Control;
            this.exportWizard.BannerImage = global::PhotoCurrent.Properties.Resources.PCLogo;
            this.exportWizard.Controls.Add(this.pageSelectSource);
            this.exportWizard.Controls.Add(this.pageFileSaveNames);
            this.exportWizard.Controls.Add(this.pageListBoxSelectFiles);
            this.exportWizard.Controls.Add(this.pageSelectOutput);
            this.exportWizard.Controls.Add(this.pageProgress);
            this.exportWizard.Cursor = System.Windows.Forms.Cursors.Default;
            this.exportWizard.FinishText = "&Finish";
            this.exportWizard.Location = new System.Drawing.Point(0, 0);
            this.exportWizard.Name = "exportWizard";
            this.exportWizard.OwnerForm = this;
            this.exportWizard.Size = new System.Drawing.Size(497, 362);
            this.exportWizard.TabIndex = 0;
            this.exportWizard.Text = "Export to CSV File";
            this.exportWizard.UserExperienceType = Divelements.WizardFramework.WizardUserExperienceType.Wizard97;
            // 
            // pageFileSaveNames
            // 
            this.pageFileSaveNames.Controls.Add(this.cbOverwriteR3D);
            this.pageFileSaveNames.Controls.Add(this.elbSaveFiles);
            this.pageFileSaveNames.Description = "Confirm the file names below for the exported TXT files.  If necessary, change th" +
                "e file names by clicking on them and editing them.";
            this.pageFileSaveNames.Location = new System.Drawing.Point(11, 71);
            this.pageFileSaveNames.Name = "pageFileSaveNames";
            this.pageFileSaveNames.NextPage = this.pageProgress;
            this.pageFileSaveNames.PreviousPage = this.pageSelectSource;
            this.pageFileSaveNames.Size = new System.Drawing.Size(475, 233);
            this.pageFileSaveNames.TabIndex = 1007;
            this.pageFileSaveNames.Text = "Confirm the Exported File Names";
            // 
            // cbOverwriteR3D
            // 
            this.cbOverwriteR3D.AutoSize = true;
            this.cbOverwriteR3D.Checked = true;
            this.cbOverwriteR3D.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOverwriteR3D.Location = new System.Drawing.Point(43, 7);
            this.cbOverwriteR3D.Name = "cbOverwriteR3D";
            this.cbOverwriteR3D.Size = new System.Drawing.Size(134, 17);
            this.cbOverwriteR3D.TabIndex = 3;
            this.cbOverwriteR3D.Text = "Overwrite Existing Files";
            this.cbOverwriteR3D.UseVisualStyleBackColor = true;
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
            // pageListBoxSelectFiles
            // 
            this.pageListBoxSelectFiles.Controls.Add(this.elbRawFiles);
            this.pageListBoxSelectFiles.Controls.Add(this.btnOpenFileDialog);
            this.pageListBoxSelectFiles.Controls.Add(this.cbSelectAll);
            this.pageListBoxSelectFiles.Description = "Choose the BKG, RAW, or IPCE files to Export from the Active Chart.  Or, check Se" +
                "lect All Files to Export all data.";
            this.pageListBoxSelectFiles.Location = new System.Drawing.Point(11, 71);
            this.pageListBoxSelectFiles.Name = "pageListBoxSelectFiles";
            this.pageListBoxSelectFiles.NextPage = this.pageSelectOutput;
            this.pageListBoxSelectFiles.PreviousPage = this.pageSelectSource;
            this.pageListBoxSelectFiles.Size = new System.Drawing.Size(475, 233);
            this.pageListBoxSelectFiles.TabIndex = 5;
            this.pageListBoxSelectFiles.Text = "Choose the Data Files to Export";
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
            this.elbRawFiles.TabIndex = 8;
            this.elbRawFiles.UseCompatibleStateImageBehavior = false;
            this.elbRawFiles.View = System.Windows.Forms.View.Details;
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileDialog.Image")));
            this.btnOpenFileDialog.Location = new System.Drawing.Point(406, 4);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(29, 23);
            this.btnOpenFileDialog.TabIndex = 7;
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
            this.cbSelectAll.TabIndex = 6;
            this.cbSelectAll.Text = "Select All Files";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // pageSelectOutput
            // 
            this.pageSelectOutput.Controls.Add(this.txtOutputFile);
            this.pageSelectOutput.Controls.Add(this.lblOutputPath);
            this.pageSelectOutput.Controls.Add(this.btnBrowseOutput);
            this.pageSelectOutput.Description = "Choose the Output CSV file path to use to Export the selected files.";
            this.pageSelectOutput.Location = new System.Drawing.Point(11, 71);
            this.pageSelectOutput.Name = "pageSelectOutput";
            this.pageSelectOutput.NextPage = this.pageFileSaveNames;
            this.pageSelectOutput.PreviousPage = this.pageListBoxSelectFiles;
            this.pageSelectOutput.Size = new System.Drawing.Size(475, 233);
            this.pageSelectOutput.TabIndex = 1006;
            this.pageSelectOutput.Text = "Choose the File to which to Export";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(51, 74);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.ReadOnly = true;
            this.txtOutputFile.Size = new System.Drawing.Size(345, 20);
            this.txtOutputFile.TabIndex = 8;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(48, 58);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(67, 13);
            this.lblOutputPath.TabIndex = 7;
            this.lblOutputPath.Text = "Output Path:";
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseOutput.Image")));
            this.btnBrowseOutput.Location = new System.Drawing.Point(397, 72);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(29, 23);
            this.btnBrowseOutput.TabIndex = 6;
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // pageProgress
            // 
            this.pageProgress.Controls.Add(this.lblCurrentTask);
            this.pageProgress.Controls.Add(this.lblStatus);
            this.pageProgress.Controls.Add(this.progExport);
            this.pageProgress.Description = "Click the Finish button to begin the Export of files to the Output File.";
            this.pageProgress.Location = new System.Drawing.Point(11, 71);
            this.pageProgress.Name = "pageProgress";
            this.pageProgress.PreviousPage = this.pageSelectOutput;
            this.pageProgress.Size = new System.Drawing.Size(475, 233);
            this.pageProgress.TabIndex = 7;
            this.pageProgress.Text = "Click Finish to Continue";
            // 
            // lblCurrentTask
            // 
            this.lblCurrentTask.Location = new System.Drawing.Point(63, 68);
            this.lblCurrentTask.Name = "lblCurrentTask";
            this.lblCurrentTask.Size = new System.Drawing.Size(352, 85);
            this.lblCurrentTask.TabIndex = 7;
            this.lblCurrentTask.Text = "Click Finish to begin processing...";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(60, 51);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            // 
            // progExport
            // 
            this.progExport.Location = new System.Drawing.Point(63, 158);
            this.progExport.Name = "progExport";
            this.progExport.Size = new System.Drawing.Size(352, 23);
            this.progExport.TabIndex = 5;
            // 
            // pageSelectSource
            // 
            this.pageSelectSource.Controls.Add(this.rbChart3D);
            this.pageSelectSource.Controls.Add(this.rbOther3D);
            this.pageSelectSource.Controls.Add(this.rbOther2D);
            this.pageSelectSource.Controls.Add(this.rbChart2D);
            this.pageSelectSource.Description = "Choose the Source of the data to be exported:  the Active Chart or external BKG, " +
                "RAW, or IPCE files on disk.";
            this.pageSelectSource.Location = new System.Drawing.Point(11, 71);
            this.pageSelectSource.Name = "pageSelectSource";
            this.pageSelectSource.NextPage = this.pageListBoxSelectFiles;
            this.pageSelectSource.Size = new System.Drawing.Size(475, 233);
            this.pageSelectSource.TabIndex = 1005;
            this.pageSelectSource.Text = "Choose the Input File Source";
            // 
            // rbChart3D
            // 
            this.rbChart3D.AutoSize = true;
            this.rbChart3D.Location = new System.Drawing.Point(93, 128);
            this.rbChart3D.Name = "rbChart3D";
            this.rbChart3D.Size = new System.Drawing.Size(140, 17);
            this.rbChart3D.TabIndex = 7;
            this.rbChart3D.TabStop = true;
            this.rbChart3D.Text = "3-D Data in Active Chart";
            this.rbChart3D.UseVisualStyleBackColor = true;
            // 
            // rbOther3D
            // 
            this.rbOther3D.AutoSize = true;
            this.rbOther3D.Location = new System.Drawing.Point(93, 169);
            this.rbOther3D.Name = "rbOther3D";
            this.rbOther3D.Size = new System.Drawing.Size(119, 17);
            this.rbOther3D.TabIndex = 6;
            this.rbOther3D.TabStop = true;
            this.rbOther3D.Text = "Other 3-D TXT Files";
            this.rbOther3D.UseVisualStyleBackColor = true;
            // 
            // rbOther2D
            // 
            this.rbOther2D.AutoSize = true;
            this.rbOther2D.Location = new System.Drawing.Point(93, 87);
            this.rbOther2D.Name = "rbOther2D";
            this.rbOther2D.Size = new System.Drawing.Size(121, 17);
            this.rbOther2D.TabIndex = 5;
            this.rbOther2D.Text = "Other 2-D Data Files";
            this.rbOther2D.UseVisualStyleBackColor = true;
            // 
            // rbChart2D
            // 
            this.rbChart2D.AutoSize = true;
            this.rbChart2D.Checked = true;
            this.rbChart2D.Location = new System.Drawing.Point(93, 46);
            this.rbChart2D.Name = "rbChart2D";
            this.rbChart2D.Size = new System.Drawing.Size(140, 17);
            this.rbChart2D.TabIndex = 4;
            this.rbChart2D.TabStop = true;
            this.rbChart2D.Text = "2-D Data in Active Chart";
            this.rbChart2D.UseVisualStyleBackColor = true;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 362);
            this.ControlBox = false;
            this.Controls.Add(this.exportWizard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Export";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export to CSV File";
            this.exportWizard.ResumeLayout(false);
            this.pageFileSaveNames.ResumeLayout(false);
            this.pageFileSaveNames.PerformLayout();
            this.pageListBoxSelectFiles.ResumeLayout(false);
            this.pageListBoxSelectFiles.PerformLayout();
            this.pageSelectOutput.ResumeLayout(false);
            this.pageSelectOutput.PerformLayout();
            this.pageProgress.ResumeLayout(false);
            this.pageProgress.PerformLayout();
            this.pageSelectSource.ResumeLayout(false);
            this.pageSelectSource.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Divelements.WizardFramework.Wizard exportWizard;
        private Divelements.WizardFramework.WizardPage pageSelectSource;
        private System.Windows.Forms.RadioButton rbOther2D;
        private System.Windows.Forms.RadioButton rbChart2D;
        private Divelements.WizardFramework.WizardPage pageListBoxSelectFiles;
        private RDH2.Utilities.Controls.EditableListBox elbRawFiles;
        private System.Windows.Forms.Button btnOpenFileDialog;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private Divelements.WizardFramework.WizardPage pageSelectOutput;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Button btnBrowseOutput;
        private Divelements.WizardFramework.WizardPage pageProgress;
        private System.Windows.Forms.Label lblCurrentTask;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progExport;
        private System.Windows.Forms.RadioButton rbOther3D;
        private System.Windows.Forms.RadioButton rbChart3D;
        private Divelements.WizardFramework.WizardPage pageFileSaveNames;
        private System.Windows.Forms.CheckBox cbOverwriteR3D;
        private RDH2.Utilities.Controls.EditableListBox elbSaveFiles;
    }
}