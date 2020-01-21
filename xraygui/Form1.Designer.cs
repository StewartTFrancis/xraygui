namespace xraygui
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.acquireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cbPixelCorr = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbGainImage = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbOffset = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbFOV = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBinning = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIntegration = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGain = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acquireToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1139, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // acquireToolStripMenuItem
            // 
            this.acquireToolStripMenuItem.Name = "acquireToolStripMenuItem";
            this.acquireToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.acquireToolStripMenuItem.Text = "Acquire";
            this.acquireToolStripMenuItem.Click += new System.EventHandler(this.acquireToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1139, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(88, 17);
            this.lblStatus.Text = "Status: CLOSED";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 46);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cbPixelCorr);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cbGainImage);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cbOffset);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbFOV);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cbBinning);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbIntegration);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbGain);
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 288);
            this.panel2.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Pixel Corr";
            // 
            // cbPixelCorr
            // 
            this.cbPixelCorr.FormattingEnabled = true;
            this.cbPixelCorr.Location = new System.Drawing.Point(76, 194);
            this.cbPixelCorr.Name = "cbPixelCorr";
            this.cbPixelCorr.Size = new System.Drawing.Size(121, 21);
            this.cbPixelCorr.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Gain Image";
            // 
            // cbGainImage
            // 
            this.cbGainImage.FormattingEnabled = true;
            this.cbGainImage.Location = new System.Drawing.Point(76, 167);
            this.cbGainImage.Name = "cbGainImage";
            this.cbGainImage.Size = new System.Drawing.Size(121, 21);
            this.cbGainImage.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Offset Image";
            // 
            // cbOffset
            // 
            this.cbOffset.FormattingEnabled = true;
            this.cbOffset.Location = new System.Drawing.Point(76, 140);
            this.cbOffset.Name = "cbOffset";
            this.cbOffset.Size = new System.Drawing.Size(121, 21);
            this.cbOffset.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "FOV Mode";
            // 
            // cbFOV
            // 
            this.cbFOV.FormattingEnabled = true;
            this.cbFOV.Location = new System.Drawing.Point(76, 113);
            this.cbFOV.Name = "cbFOV";
            this.cbFOV.Size = new System.Drawing.Size(121, 21);
            this.cbFOV.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Binning";
            // 
            // cbBinning
            // 
            this.cbBinning.FormattingEnabled = true;
            this.cbBinning.Items.AddRange(new object[] {
            "No binning",
            "2x2 binning",
            "4x4 binning (3x3 R&F)",
            "1x2 binning",
            "1x4 binning"});
            this.cbBinning.Location = new System.Drawing.Point(76, 86);
            this.cbBinning.Name = "cbBinning";
            this.cbBinning.Size = new System.Drawing.Size(121, 21);
            this.cbBinning.TabIndex = 5;
            this.cbBinning.SelectedIndexChanged += new System.EventHandler(this.cbBinning_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Integration";
            // 
            // cbIntegration
            // 
            this.cbIntegration.FormattingEnabled = true;
            this.cbIntegration.Location = new System.Drawing.Point(76, 59);
            this.cbIntegration.Name = "cbIntegration";
            this.cbIntegration.Size = new System.Drawing.Size(121, 21);
            this.cbIntegration.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gain";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Settings";
            // 
            // cbGain
            // 
            this.cbGain.FormattingEnabled = true;
            this.cbGain.Location = new System.Drawing.Point(76, 32);
            this.cbGain.Name = "cbGain";
            this.cbGain.Size = new System.Drawing.Size(121, 21);
            this.cbGain.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(0, 360);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 137);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(204, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(238, 103);
            this.panel4.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(441, 27);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(388, 103);
            this.panel5.TabIndex = 6;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(828, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(311, 103);
            this.panel6.TabIndex = 7;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(204, 129);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(935, 34);
            this.panel7.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 522);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acquireToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbPixelCorr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbGainImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbFOV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBinning;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbIntegration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGain;
    }
}

