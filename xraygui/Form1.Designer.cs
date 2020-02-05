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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tblblCurrAngle = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.btnAcquireOnly = new System.Windows.Forms.Button();
            this.btnAcq = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.numAcqCount = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.numCtCount = new System.Windows.Forms.NumericUpDown();
            this.numAngleIncr = new System.Windows.Forms.NumericUpDown();
            this.numEndAngle = new System.Windows.Forms.NumericUpDown();
            this.numStartAngle = new System.Windows.Forms.NumericUpDown();
            this.numSettle = new System.Windows.Forms.NumericUpDown();
            this.btnCTAcq = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.lblCurrAngle = new System.Windows.Forms.Label();
            this.btnMove = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.numMoveTo = new System.Windows.Forms.NumericUpDown();
            this.cbMoveType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.sLevel = new System.Windows.Forms.TrackBar();
            this.sWindow = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new AForge.Controls.PictureBox();
            this.tmrPollAngle = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAcqCount)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCtCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngleIncr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSettle)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMoveTo)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWindow)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.tblblCurrAngle});
            this.statusStrip1.Location = new System.Drawing.Point(0, 602);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1163, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(88, 17);
            this.lblStatus.Text = "Status: CLOSED";
            // 
            // tblblCurrAngle
            // 
            this.tblblCurrAngle.Name = "tblblCurrAngle";
            this.tblblCurrAngle.Size = new System.Drawing.Size(84, 17);
            this.tblblCurrAngle.Text = "Current Angle:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 27);
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
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 230);
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
            this.cbPixelCorr.Items.AddRange(new object[] {
            "None",
            "Acquire New",
            "Pull From File"});
            this.cbPixelCorr.Location = new System.Drawing.Point(76, 194);
            this.cbPixelCorr.Name = "cbPixelCorr";
            this.cbPixelCorr.Size = new System.Drawing.Size(121, 21);
            this.cbPixelCorr.TabIndex = 13;
            this.cbPixelCorr.SelectedIndexChanged += new System.EventHandler(this.cbPixelCorr_SelectedIndexChanged);
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
            this.cbGainImage.Items.AddRange(new object[] {
            "None",
            "Acquire New",
            "Pull From File"});
            this.cbGainImage.Location = new System.Drawing.Point(76, 167);
            this.cbGainImage.Name = "cbGainImage";
            this.cbGainImage.Size = new System.Drawing.Size(121, 21);
            this.cbGainImage.TabIndex = 11;
            this.cbGainImage.SelectedIndexChanged += new System.EventHandler(this.cbGainImage_SelectedIndexChanged);
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
            this.cbOffset.Items.AddRange(new object[] {
            "None",
            "Acquire New",
            "Pull From File"});
            this.cbOffset.Location = new System.Drawing.Point(76, 140);
            this.cbOffset.Name = "cbOffset";
            this.cbOffset.Size = new System.Drawing.Size(121, 21);
            this.cbOffset.TabIndex = 9;
            this.cbOffset.SelectedIndexChanged += new System.EventHandler(this.cbOffset_SelectedIndexChanged);
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
            this.cbFOV.SelectedIndexChanged += new System.EventHandler(this.cbFOV_SelectedIndexChanged);
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
            this.cbIntegration.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cbIntegration.Location = new System.Drawing.Point(76, 59);
            this.cbIntegration.Name = "cbIntegration";
            this.cbIntegration.Size = new System.Drawing.Size(121, 21);
            this.cbIntegration.TabIndex = 3;
            this.cbIntegration.SelectedIndexChanged += new System.EventHandler(this.cbIntegration_SelectedIndexChanged);
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
            this.cbGain.SelectedIndexChanged += new System.EventHandler(this.cbGain_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(0, 302);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 297);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnAcquireOnly);
            this.panel4.Controls.Add(this.btnAcq);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.numAcqCount);
            this.panel4.Location = new System.Drawing.Point(204, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(238, 103);
            this.panel4.TabIndex = 5;
            // 
            // btnAcquireOnly
            // 
            this.btnAcquireOnly.Enabled = false;
            this.btnAcquireOnly.Location = new System.Drawing.Point(9, 69);
            this.btnAcquireOnly.Name = "btnAcquireOnly";
            this.btnAcquireOnly.Size = new System.Drawing.Size(75, 23);
            this.btnAcquireOnly.TabIndex = 17;
            this.btnAcquireOnly.Text = "Acquire Only";
            this.btnAcquireOnly.UseVisualStyleBackColor = true;
            this.btnAcquireOnly.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAcq
            // 
            this.btnAcq.Enabled = false;
            this.btnAcq.Location = new System.Drawing.Point(155, 70);
            this.btnAcq.Name = "btnAcq";
            this.btnAcq.Size = new System.Drawing.Size(75, 23);
            this.btnAcq.TabIndex = 16;
            this.btnAcq.Text = "Save To File";
            this.btnAcq.UseVisualStyleBackColor = true;
            this.btnAcq.Click += new System.EventHandler(this.btnAcq_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Number to Acquire";
            // 
            // numAcqCount
            // 
            this.numAcqCount.Location = new System.Drawing.Point(107, 19);
            this.numAcqCount.Name = "numAcqCount";
            this.numAcqCount.Size = new System.Drawing.Size(47, 20);
            this.numAcqCount.TabIndex = 0;
            this.numAcqCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.numCtCount);
            this.panel5.Controls.Add(this.numAngleIncr);
            this.panel5.Controls.Add(this.numEndAngle);
            this.panel5.Controls.Add(this.numStartAngle);
            this.panel5.Controls.Add(this.numSettle);
            this.panel5.Controls.Add(this.btnCTAcq);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Location = new System.Drawing.Point(441, 27);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(388, 103);
            this.panel5.TabIndex = 6;
            // 
            // numCtCount
            // 
            this.numCtCount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCtCount.Location = new System.Drawing.Point(105, 78);
            this.numCtCount.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numCtCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCtCount.Name = "numCtCount";
            this.numCtCount.Size = new System.Drawing.Size(62, 20);
            this.numCtCount.TabIndex = 25;
            this.numCtCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numAngleIncr
            // 
            this.numAngleIncr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numAngleIncr.Location = new System.Drawing.Point(105, 54);
            this.numAngleIncr.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numAngleIncr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numAngleIncr.Name = "numAngleIncr";
            this.numAngleIncr.Size = new System.Drawing.Size(62, 20);
            this.numAngleIncr.TabIndex = 24;
            this.numAngleIncr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numEndAngle
            // 
            this.numEndAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numEndAngle.Location = new System.Drawing.Point(238, 29);
            this.numEndAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numEndAngle.Name = "numEndAngle";
            this.numEndAngle.Size = new System.Drawing.Size(55, 20);
            this.numEndAngle.TabIndex = 23;
            this.numEndAngle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numStartAngle
            // 
            this.numStartAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numStartAngle.Location = new System.Drawing.Point(104, 29);
            this.numStartAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numStartAngle.Name = "numStartAngle";
            this.numStartAngle.Size = new System.Drawing.Size(62, 20);
            this.numStartAngle.TabIndex = 22;
            // 
            // numSettle
            // 
            this.numSettle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSettle.Location = new System.Drawing.Point(104, 3);
            this.numSettle.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numSettle.Name = "numSettle";
            this.numSettle.Size = new System.Drawing.Size(62, 20);
            this.numSettle.TabIndex = 17;
            this.numSettle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnCTAcq
            // 
            this.btnCTAcq.Enabled = false;
            this.btnCTAcq.Location = new System.Drawing.Point(305, 70);
            this.btnCTAcq.Name = "btnCTAcq";
            this.btnCTAcq.Size = new System.Drawing.Size(75, 23);
            this.btnCTAcq.TabIndex = 17;
            this.btnCTAcq.Text = "Acquire";
            this.btnCTAcq.UseVisualStyleBackColor = true;
            this.btnCTAcq.Click += new System.EventHandler(this.btnCTAcq_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "Number of Frames";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Degree Increment";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(176, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "End Angle";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Start Angle";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Settling Time";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btnHome);
            this.panel6.Controls.Add(this.lblCurrAngle);
            this.panel6.Controls.Add(this.btnMove);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Controls.Add(this.numMoveTo);
            this.panel6.Controls.Add(this.cbMoveType);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Location = new System.Drawing.Point(828, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(335, 103);
            this.panel6.TabIndex = 7;
            // 
            // btnHome
            // 
            this.btnHome.Enabled = false;
            this.btnHome.Location = new System.Drawing.Point(223, 5);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(75, 23);
            this.btnHome.TabIndex = 28;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // lblCurrAngle
            // 
            this.lblCurrAngle.AutoSize = true;
            this.lblCurrAngle.Location = new System.Drawing.Point(16, 79);
            this.lblCurrAngle.Name = "lblCurrAngle";
            this.lblCurrAngle.Size = new System.Drawing.Size(74, 13);
            this.lblCurrAngle.TabIndex = 27;
            this.lblCurrAngle.Text = "Current Angle:";
            // 
            // btnMove
            // 
            this.btnMove.Enabled = false;
            this.btnMove.Location = new System.Drawing.Point(223, 70);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 17;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "Move To";
            // 
            // numMoveTo
            // 
            this.numMoveTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMoveTo.Location = new System.Drawing.Point(69, 43);
            this.numMoveTo.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numMoveTo.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numMoveTo.Name = "numMoveTo";
            this.numMoveTo.Size = new System.Drawing.Size(62, 20);
            this.numMoveTo.TabIndex = 25;
            // 
            // cbMoveType
            // 
            this.cbMoveType.FormattingEnabled = true;
            this.cbMoveType.Items.AddRange(new object[] {
            "Relative",
            "Absolute"});
            this.cbMoveType.Location = new System.Drawing.Point(69, 7);
            this.cbMoveType.Name = "cbMoveType";
            this.cbMoveType.Size = new System.Drawing.Size(85, 21);
            this.cbMoveType.TabIndex = 15;
            this.cbMoveType.SelectedIndexChanged += new System.EventHandler(this.cbMoveType_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "Movement";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.sLevel);
            this.panel7.Controls.Add(this.sWindow);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Location = new System.Drawing.Point(204, 129);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(959, 46);
            this.panel7.TabIndex = 6;
            // 
            // sLevel
            // 
            this.sLevel.Location = new System.Drawing.Point(482, 2);
            this.sLevel.Maximum = 65536;
            this.sLevel.Minimum = 1;
            this.sLevel.Name = "sLevel";
            this.sLevel.Size = new System.Drawing.Size(288, 45);
            this.sLevel.TabIndex = 20;
            this.sLevel.Value = 32768;
            this.sLevel.Scroll += new System.EventHandler(this.windowLevelChange);
            // 
            // sWindow
            // 
            this.sWindow.Location = new System.Drawing.Point(68, -1);
            this.sWindow.Maximum = 32768;
            this.sWindow.Minimum = 1;
            this.sWindow.Name = "sWindow";
            this.sWindow.Size = new System.Drawing.Size(288, 45);
            this.sWindow.TabIndex = 19;
            this.sWindow.Value = 16384;
            this.sWindow.Scroll += new System.EventHandler(this.windowLevelChange);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(443, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Level";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Window";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDeviceToolStripMenuItem,
            this.closeDeviceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1163, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openDeviceToolStripMenuItem
            // 
            this.openDeviceToolStripMenuItem.Name = "openDeviceToolStripMenuItem";
            this.openDeviceToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.openDeviceToolStripMenuItem.Text = "Open Device";
            this.openDeviceToolStripMenuItem.Click += new System.EventHandler(this.openDeviceToolStripMenuItem_Click);
            // 
            // closeDeviceToolStripMenuItem
            // 
            this.closeDeviceToolStripMenuItem.Name = "closeDeviceToolStripMenuItem";
            this.closeDeviceToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.closeDeviceToolStripMenuItem.Text = "Close Device";
            this.closeDeviceToolStripMenuItem.Visible = false;
            this.closeDeviceToolStripMenuItem.Click += new System.EventHandler(this.closeDeviceToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(205, 176);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(958, 420);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // tmrPollAngle
            // 
            this.tmrPollAngle.Enabled = true;
            this.tmrPollAngle.Tick += new System.EventHandler(this.tmrPollAngle_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 624);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "XRayGui";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAcqCount)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCtCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngleIncr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSettle)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMoveTo)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWindow)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button btnAcq;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numAcqCount;
        private System.Windows.Forms.NumericUpDown numEndAngle;
        private System.Windows.Forms.NumericUpDown numStartAngle;
        private System.Windows.Forms.NumericUpDown numSettle;
        private System.Windows.Forms.Button btnCTAcq;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar sLevel;
        private System.Windows.Forms.TrackBar sWindow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numAngleIncr;
        private System.Windows.Forms.ToolStripStatusLabel tblblCurrAngle;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numMoveTo;
        private System.Windows.Forms.ComboBox cbMoveType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblCurrAngle;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeDeviceToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numCtCount;
        private AForge.Controls.PictureBox pictureBox1;
        private System.Windows.Forms.Timer tmrPollAngle;
        private System.Windows.Forms.Button btnAcquireOnly;
    }
}

