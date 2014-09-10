namespace PhotoCurrent.Forms
{
    partial class Spectrum
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
            Dundas.Charting.WinControl.ChartArea chartArea1 = new Dundas.Charting.WinControl.ChartArea();
            Dundas.Charting.WinControl.Legend legend1 = new Dundas.Charting.WinControl.Legend();
            Dundas.Charting.WinControl.Title title1 = new Dundas.Charting.WinControl.Title();
            this.chart = new Dundas.Charting.WinControl.Chart();
            this.barStatus = new System.Windows.Forms.StatusStrip();
            this.lblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.barStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.AlwaysRecreateHotregions = true;
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chart.BackGradientEndColor = System.Drawing.Color.White;
            this.chart.BackGradientType = Dundas.Charting.WinControl.GradientType.DiagonalLeft;
            this.chart.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            this.chart.BorderLineStyle = Dundas.Charting.WinControl.ChartDashStyle.Solid;
            this.chart.BorderSkin.FrameBackColor = System.Drawing.Color.CornflowerBlue;
            this.chart.BorderSkin.FrameBackGradientEndColor = System.Drawing.Color.CornflowerBlue;
            this.chart.BorderSkin.PageColor = System.Drawing.SystemColors.Control;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.Margin = false;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MinorTickMark.Size = 2F;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MinorTickMark.Size = 2F;
            chartArea1.AxisY.StartFromZero = false;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            chartArea1.BorderStyle = Dundas.Charting.WinControl.ChartDashStyle.Solid;
            chartArea1.Name = "Default";
            chartArea1.ShadowOffset = 2;
            this.chart.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.White;
            legend1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            legend1.Name = "Default";
            legend1.ShadowOffset = 2;
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Palette = Dundas.Charting.WinControl.ChartColorPalette.Dundas;
            this.chart.Size = new System.Drawing.Size(624, 419);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            title1.Name = "Title1";
            this.chart.Titles.Add(title1);
            // 
            // barStatus
            // 
            this.barStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPosition,
            this.lblTime});
            this.barStatus.Location = new System.Drawing.Point(0, 422);
            this.barStatus.Name = "barStatus";
            this.barStatus.Size = new System.Drawing.Size(624, 22);
            this.barStatus.TabIndex = 1;
            this.barStatus.Text = "Status";
            // 
            // lblPosition
            // 
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(51, 17);
            this.lblPosition.Text = "X: 0  Y: 0";
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(558, 17);
            this.lblTime.Spring = true;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Spectrum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.barStatus);
            this.Controls.Add(this.chart);
            this.Name = "Spectrum";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Spectrum";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.barStatus.ResumeLayout(false);
            this.barStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Dundas.Charting.WinControl.Chart chart;
        private System.Windows.Forms.StatusStrip barStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblPosition;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;



    }
}