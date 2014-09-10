namespace PhotoCurrent.Forms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoscale = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResetZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPause = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScaleRAW = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigDAQ = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigPotStat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigMC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHWSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTileHorz = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTileVert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContents = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tbNew = new System.Windows.Forms.ToolStripButton();
            this.tbOpen = new System.Windows.Forms.ToolStripButton();
            this.tbAdd = new System.Windows.Forms.ToolStripButton();
            this.tbPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbAutoscale = new System.Windows.Forms.ToolStripButton();
            this.tbResetZoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tbPause = new System.Windows.Forms.ToolStripButton();
            this.tbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbScale = new System.Windows.Forms.ToolStripButton();
            this.tbExport = new System.Windows.Forms.ToolStripButton();
            this.mnuConfigMirrors = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuPlot,
            this.mnuConfig,
            this.mnuWindow,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.MdiWindowListItem = this.mnuWindow;
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(784, 24);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "Main";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuAdd,
            this.toolStripSeparator2,
            this.mnuPrint,
            this.toolStripSeparator6,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuNew
            // 
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNew.Size = new System.Drawing.Size(209, 22);
            this.mnuNew.Text = "&New Spectrum...";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpen.Size = new System.Drawing.Size(209, 22);
            this.mnuOpen.Text = "&Open Spectrum...";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuAdd.Size = new System.Drawing.Size(209, 22);
            this.mnuAdd.Text = "&Add Spectrum...";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuPrint.Size = new System.Drawing.Size(209, 22);
            this.mnuPrint.Text = "&Print...";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(206, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(209, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuPlot
            // 
            this.mnuPlot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoscale,
            this.mnuResetZoom,
            this.toolStripSeparator7,
            this.mnuPause,
            this.mnuStop,
            this.toolStripSeparator8,
            this.mnuScaleRAW,
            this.mnuExport});
            this.mnuPlot.Name = "mnuPlot";
            this.mnuPlot.Size = new System.Drawing.Size(40, 20);
            this.mnuPlot.Text = "&Plot";
            // 
            // mnuAutoscale
            // 
            this.mnuAutoscale.Name = "mnuAutoscale";
            this.mnuAutoscale.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.mnuAutoscale.Size = new System.Drawing.Size(291, 22);
            this.mnuAutoscale.Text = "&Autoscale Axes";
            this.mnuAutoscale.Click += new System.EventHandler(this.mnuAutoscale_Click);
            // 
            // mnuResetZoom
            // 
            this.mnuResetZoom.Name = "mnuResetZoom";
            this.mnuResetZoom.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuResetZoom.Size = new System.Drawing.Size(291, 22);
            this.mnuResetZoom.Text = "&Reset Zoom";
            this.mnuResetZoom.Click += new System.EventHandler(this.mnuResetZoom_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(288, 6);
            // 
            // mnuPause
            // 
            this.mnuPause.Name = "mnuPause";
            this.mnuPause.Size = new System.Drawing.Size(291, 22);
            this.mnuPause.Text = "Pause Data Collection";
            this.mnuPause.Click += new System.EventHandler(this.mnuPause_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(291, 22);
            this.mnuStop.Text = "Stop Data Collection";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(288, 6);
            // 
            // mnuScaleRAW
            // 
            this.mnuScaleRAW.Name = "mnuScaleRAW";
            this.mnuScaleRAW.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuScaleRAW.Size = new System.Drawing.Size(291, 22);
            this.mnuScaleRAW.Text = "Sca&le Photocurrent Data to IPCE...";
            this.mnuScaleRAW.Click += new System.EventHandler(this.mnuScaleRAW_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuExport.Size = new System.Drawing.Size(291, 22);
            this.mnuExport.Text = "E&xport Data to CSV...";
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // mnuConfig
            // 
            this.mnuConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConfigDAQ,
            this.mnuConfigCurrent,
            this.mnuConfigMC,
            this.mnuConfigMirrors,
            this.mnuConfigPotStat,
            this.toolStripSeparator3,
            this.mnuHWSetup});
            this.mnuConfig.Name = "mnuConfig";
            this.mnuConfig.Size = new System.Drawing.Size(72, 20);
            this.mnuConfig.Text = "&Configure";
            // 
            // mnuConfigDAQ
            // 
            this.mnuConfigDAQ.Name = "mnuConfigDAQ";
            this.mnuConfigDAQ.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuConfigDAQ.Size = new System.Drawing.Size(221, 22);
            this.mnuConfigDAQ.Text = "&DAQ Hardware...";
            this.mnuConfigDAQ.Click += new System.EventHandler(this.mnuConfigDAQ_Click);
            // 
            // mnuConfigPotStat
            // 
            this.mnuConfigPotStat.Name = "mnuConfigPotStat";
            this.mnuConfigPotStat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuConfigPotStat.Size = new System.Drawing.Size(221, 22);
            this.mnuConfigPotStat.Text = "Po&tentiostat...";
            this.mnuConfigPotStat.Click += new System.EventHandler(this.mnuConfigPotStat_Click);
            // 
            // mnuConfigCurrent
            // 
            this.mnuConfigCurrent.Name = "mnuConfigCurrent";
            this.mnuConfigCurrent.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuConfigCurrent.Size = new System.Drawing.Size(221, 22);
            this.mnuConfigCurrent.Text = "Photocurrent Input...";
            this.mnuConfigCurrent.Click += new System.EventHandler(this.mnuConfigLockIn_Click);
            // 
            // mnuConfigMC
            // 
            this.mnuConfigMC.Name = "mnuConfigMC";
            this.mnuConfigMC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuConfigMC.Size = new System.Drawing.Size(221, 22);
            this.mnuConfigMC.Text = "&Monochromator...";
            this.mnuConfigMC.Click += new System.EventHandler(this.mnuConfigMC_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
            // 
            // mnuHWSetup
            // 
            this.mnuHWSetup.Name = "mnuHWSetup";
            this.mnuHWSetup.Size = new System.Drawing.Size(221, 22);
            this.mnuHWSetup.Text = "Hardware Setup Wizard...";
            this.mnuHWSetup.Click += new System.EventHandler(this.mnuHWSetup_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTileHorz,
            this.mnuTileVert,
            this.mnuCascade,
            this.toolStripSeparator5});
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new System.Drawing.Size(63, 20);
            this.mnuWindow.Text = "&Window";
            // 
            // mnuTileHorz
            // 
            this.mnuTileHorz.Name = "mnuTileHorz";
            this.mnuTileHorz.Size = new System.Drawing.Size(160, 22);
            this.mnuTileHorz.Text = "Tile &Horizontally";
            this.mnuTileHorz.Click += new System.EventHandler(this.mnuTileHorz_Click);
            // 
            // mnuTileVert
            // 
            this.mnuTileVert.Name = "mnuTileVert";
            this.mnuTileVert.Size = new System.Drawing.Size(160, 22);
            this.mnuTileVert.Text = "Tile &Vertically";
            this.mnuTileVert.Click += new System.EventHandler(this.mnuTileVert_Click);
            // 
            // mnuCascade
            // 
            this.mnuCascade.Name = "mnuCascade";
            this.mnuCascade.Size = new System.Drawing.Size(160, 22);
            this.mnuCascade.Text = "&Cascade";
            this.mnuCascade.Click += new System.EventHandler(this.mnuCascade_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContents,
            this.toolStripSeparator10,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuContents
            // 
            this.mnuContents.Enabled = false;
            this.mnuContents.Name = "mnuContents";
            this.mnuContents.Size = new System.Drawing.Size(191, 22);
            this.mnuContents.Text = "&Contents...";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(191, 22);
            this.mnuAbout.Text = "&About PhotoCurrent...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbNew,
            this.tbOpen,
            this.tbAdd,
            this.tbPrint,
            this.toolStripSeparator4,
            this.tbAutoscale,
            this.tbResetZoom,
            this.toolStripSeparator9,
            this.tbPause,
            this.tbStop,
            this.toolStripSeparator1,
            this.tbScale,
            this.tbExport});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(784, 25);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "Main";
            // 
            // tbNew
            // 
            this.tbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbNew.Image = global::PhotoCurrent.Properties.Resources.NewSpectrum;
            this.tbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(23, 22);
            this.tbNew.Text = "New Spectrum";
            this.tbNew.Click += new System.EventHandler(this.tbNew_Click);
            // 
            // tbOpen
            // 
            this.tbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbOpen.Image = global::PhotoCurrent.Properties.Resources.OpenSpectrum;
            this.tbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(23, 22);
            this.tbOpen.Text = "Open Spectrum";
            this.tbOpen.Click += new System.EventHandler(this.tbOpen_Click);
            // 
            // tbAdd
            // 
            this.tbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAdd.Image = global::PhotoCurrent.Properties.Resources.AddSpectrum;
            this.tbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(23, 22);
            this.tbAdd.Text = "Add / Remove Spectra";
            this.tbAdd.Click += new System.EventHandler(this.tbAdd_Click);
            // 
            // tbPrint
            // 
            this.tbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPrint.Image = global::PhotoCurrent.Properties.Resources.Print;
            this.tbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(23, 22);
            this.tbPrint.Text = "Print";
            this.tbPrint.Click += new System.EventHandler(this.tbPrint_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tbAutoscale
            // 
            this.tbAutoscale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAutoscale.Image = global::PhotoCurrent.Properties.Resources.Autoscale;
            this.tbAutoscale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAutoscale.Name = "tbAutoscale";
            this.tbAutoscale.Size = new System.Drawing.Size(23, 22);
            this.tbAutoscale.Text = "Autoscale Plot Axes";
            this.tbAutoscale.Click += new System.EventHandler(this.tbAutoscale_Click);
            // 
            // tbResetZoom
            // 
            this.tbResetZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbResetZoom.Image = global::PhotoCurrent.Properties.Resources.ResetZoom;
            this.tbResetZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbResetZoom.Name = "tbResetZoom";
            this.tbResetZoom.Size = new System.Drawing.Size(23, 22);
            this.tbResetZoom.Text = "Reset Zoom";
            this.tbResetZoom.Click += new System.EventHandler(this.tbResetZoom_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // tbPause
            // 
            this.tbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPause.Image = global::PhotoCurrent.Properties.Resources.PauseDataCollection;
            this.tbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPause.Name = "tbPause";
            this.tbPause.Size = new System.Drawing.Size(23, 22);
            this.tbPause.Tag = "P";
            this.tbPause.Text = "Pause Data Collection";
            this.tbPause.Click += new System.EventHandler(this.tbPause_Click);
            // 
            // tbStop
            // 
            this.tbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStop.Image = global::PhotoCurrent.Properties.Resources.StopDataCollection;
            this.tbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbStop.Name = "tbStop";
            this.tbStop.Size = new System.Drawing.Size(23, 22);
            this.tbStop.Text = "Stop Data Collection";
            this.tbStop.Click += new System.EventHandler(this.tbStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbScale
            // 
            this.tbScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbScale.Image = global::PhotoCurrent.Properties.Resources.ScaleToIPCE;
            this.tbScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(23, 22);
            this.tbScale.Text = "Scale Data";
            this.tbScale.ToolTipText = "Scale Data to IPCE";
            this.tbScale.Click += new System.EventHandler(this.tbScale_Click);
            // 
            // tbExport
            // 
            this.tbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbExport.Image = global::PhotoCurrent.Properties.Resources.Export;
            this.tbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExport.Name = "tbExport";
            this.tbExport.Size = new System.Drawing.Size(23, 22);
            this.tbExport.Text = "Export to CSV";
            this.tbExport.Click += new System.EventHandler(this.tbExport_Click);
            // 
            // mnuConfigMirrors
            // 
            this.mnuConfigMirrors.Name = "mnuConfigMirrors";
            this.mnuConfigMirrors.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuConfigMirrors.Size = new System.Drawing.Size(221, 22);
            this.mnuConfigMirrors.Text = "Mirrors...";
            this.mnuConfigMirrors.Click += new System.EventHandler(this.mnuConfigMirrors_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "Main";
            this.Text = "PhotoCurrent";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuAdd;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuConfig;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigDAQ;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuTileHorz;
        private System.Windows.Forms.ToolStripMenuItem mnuTileVert;
        private System.Windows.Forms.ToolStripMenuItem mnuCascade;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuPlot;
        private System.Windows.Forms.ToolStripMenuItem mnuResetZoom;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mnuPause;
        private System.Windows.Forms.ToolStripMenuItem mnuStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mnuScaleRAW;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigPotStat;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigCurrent;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigMC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHWSetup;
        private System.Windows.Forms.ToolStripButton tbNew;
        private System.Windows.Forms.ToolStripButton tbOpen;
        private System.Windows.Forms.ToolStripButton tbAdd;
        private System.Windows.Forms.ToolStripButton tbPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbResetZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tbScale;
        private System.Windows.Forms.ToolStripButton tbPause;
        private System.Windows.Forms.ToolStripButton tbStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuContents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoscale;
        private System.Windows.Forms.ToolStripButton tbAutoscale;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripButton tbExport;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigMirrors;
    }
}

