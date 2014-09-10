namespace PhotoCurrent.Wizards
{
    partial class AddRemoveSpectra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRemoveSpectra));
            this.wizardAddRemove = new Divelements.WizardFramework.Wizard();
            this.pageSpectraListBox = new Divelements.WizardFramework.WizardPage();
            this.elbRawFiles = new RDH2.Utilities.Controls.EditableListBox();
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.wizardAddRemove.SuspendLayout();
            this.pageSpectraListBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardAddRemove
            // 
            this.wizardAddRemove.BackColor = System.Drawing.SystemColors.Control;
            this.wizardAddRemove.BannerImage = global::PhotoCurrent.Properties.Resources.PCLogo;
            this.wizardAddRemove.Controls.Add(this.pageSpectraListBox);
            this.wizardAddRemove.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizardAddRemove.FinishText = "&Finish";
            this.wizardAddRemove.Location = new System.Drawing.Point(0, 0);
            this.wizardAddRemove.Name = "wizardAddRemove";
            this.wizardAddRemove.OwnerForm = this;
            this.wizardAddRemove.Size = new System.Drawing.Size(497, 362);
            this.wizardAddRemove.TabIndex = 0;
            this.wizardAddRemove.Text = "Add / Remove Spectra";
            this.wizardAddRemove.UserExperienceType = Divelements.WizardFramework.WizardUserExperienceType.Wizard97;
            // 
            // pageSpectraListBox
            // 
            this.pageSpectraListBox.Controls.Add(this.elbRawFiles);
            this.pageSpectraListBox.Controls.Add(this.btnOpenFileDialog);
            this.pageSpectraListBox.Controls.Add(this.cbSelectAll);
            this.pageSpectraListBox.Description = "Use the File Open button to add Spectra to the Window.  Uncheck Spectra to remove" +
                " them.";
            this.pageSpectraListBox.Location = new System.Drawing.Point(11, 71);
            this.pageSpectraListBox.Name = "pageSpectraListBox";
            this.pageSpectraListBox.Size = new System.Drawing.Size(475, 233);
            this.pageSpectraListBox.TabIndex = 4;
            this.pageSpectraListBox.Text = "Modify the Spectra in the Window";
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
            this.elbRawFiles.TabIndex = 11;
            this.elbRawFiles.UseCompatibleStateImageBehavior = false;
            this.elbRawFiles.View = System.Windows.Forms.View.Details;
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFileDialog.Image")));
            this.btnOpenFileDialog.Location = new System.Drawing.Point(406, 4);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(29, 23);
            this.btnOpenFileDialog.TabIndex = 10;
            this.btnOpenFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenFileDialog.Click += new System.EventHandler(btnOpenFileDialog_Click);
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Checked = true;
            this.cbSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelectAll.Location = new System.Drawing.Point(43, 7);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(94, 17);
            this.cbSelectAll.TabIndex = 9;
            this.cbSelectAll.Text = "Select All Files";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // AddRemoveSpectra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 362);
            this.ControlBox = false;
            this.Controls.Add(this.wizardAddRemove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRemoveSpectra";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add / Remove Spectra";
            this.wizardAddRemove.ResumeLayout(false);
            this.pageSpectraListBox.ResumeLayout(false);
            this.pageSpectraListBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Divelements.WizardFramework.Wizard wizardAddRemove;
        private Divelements.WizardFramework.WizardPage pageSpectraListBox;
        private RDH2.Utilities.Controls.EditableListBox elbRawFiles;
        private System.Windows.Forms.Button btnOpenFileDialog;
        private System.Windows.Forms.CheckBox cbSelectAll;
    }
}