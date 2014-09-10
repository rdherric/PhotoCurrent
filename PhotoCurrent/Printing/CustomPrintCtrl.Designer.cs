namespace PhotoCurrent.Printing
{
    partial class CustomPrintCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbTiling = new System.Windows.Forms.GroupBox();
            this.rbPrintCurrent = new System.Windows.Forms.RadioButton();
            this.rbPrintAll = new System.Windows.Forms.RadioButton();
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.lblPerPage = new System.Windows.Forms.Label();
            this.cbPerPage = new System.Windows.Forms.ComboBox();
            this.gbTiling.SuspendLayout();
            this.gbSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTiling
            // 
            this.gbTiling.Controls.Add(this.rbPrintCurrent);
            this.gbTiling.Controls.Add(this.rbPrintAll);
            this.gbTiling.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbTiling.Location = new System.Drawing.Point(0, 0);
            this.gbTiling.Name = "gbTiling";
            this.gbTiling.Size = new System.Drawing.Size(216, 87);
            this.gbTiling.TabIndex = 0;
            this.gbTiling.TabStop = false;
            this.gbTiling.Text = "Charts to print";
            // 
            // rbPrintCurrent
            // 
            this.rbPrintCurrent.AutoSize = true;
            this.rbPrintCurrent.Location = new System.Drawing.Point(15, 52);
            this.rbPrintCurrent.Name = "rbPrintCurrent";
            this.rbPrintCurrent.Size = new System.Drawing.Size(143, 17);
            this.rbPrintCurrent.TabIndex = 6;
            this.rbPrintCurrent.TabStop = true;
            this.rbPrintCurrent.Text = "Print current window only";
            this.rbPrintCurrent.UseVisualStyleBackColor = true;
            // 
            // rbPrintAll
            // 
            this.rbPrintAll.AutoSize = true;
            this.rbPrintAll.Checked = true;
            this.rbPrintAll.Location = new System.Drawing.Point(15, 22);
            this.rbPrintAll.Name = "rbPrintAll";
            this.rbPrintAll.Size = new System.Drawing.Size(133, 17);
            this.rbPrintAll.TabIndex = 5;
            this.rbPrintAll.TabStop = true;
            this.rbPrintAll.Text = "Print all open windows ";
            this.rbPrintAll.UseVisualStyleBackColor = true;
            // 
            // gbSize
            // 
            this.gbSize.Controls.Add(this.lblPerPage);
            this.gbSize.Controls.Add(this.cbPerPage);
            this.gbSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbSize.Location = new System.Drawing.Point(228, 0);
            this.gbSize.Name = "gbSize";
            this.gbSize.Size = new System.Drawing.Size(180, 87);
            this.gbSize.TabIndex = 1;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "Chart size";
            // 
            // lblPerPage
            // 
            this.lblPerPage.AutoSize = true;
            this.lblPerPage.Location = new System.Drawing.Point(73, 24);
            this.lblPerPage.Name = "lblPerPage";
            this.lblPerPage.Size = new System.Drawing.Size(81, 13);
            this.lblPerPage.TabIndex = 10;
            this.lblPerPage.Text = "charts per page";
            // 
            // cbPerPage
            // 
            this.cbPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPerPage.FormattingEnabled = true;
            this.cbPerPage.Items.AddRange(new object[] {
            "1",
            "2",
            "4"});
            this.cbPerPage.Location = new System.Drawing.Point(14, 20);
            this.cbPerPage.Name = "cbPerPage";
            this.cbPerPage.Size = new System.Drawing.Size(52, 21);
            this.cbPerPage.TabIndex = 9;
            // 
            // CustomPrintCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSize);
            this.Controls.Add(this.gbTiling);
            this.Name = "CustomPrintCtrl";
            this.Size = new System.Drawing.Size(408, 87);
            this.gbTiling.ResumeLayout(false);
            this.gbTiling.PerformLayout();
            this.gbSize.ResumeLayout(false);
            this.gbSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTiling;
        private System.Windows.Forms.RadioButton rbPrintCurrent;
        private System.Windows.Forms.RadioButton rbPrintAll;
        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.Label lblPerPage;
        private System.Windows.Forms.ComboBox cbPerPage;
    }
}
