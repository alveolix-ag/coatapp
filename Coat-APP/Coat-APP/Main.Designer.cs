namespace OT_APP1
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
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.grpSSHDemo = new System.Windows.Forms.GroupBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.txtSSHConsole = new System.Windows.Forms.TextBox();
            this.btnCoat2 = new System.Windows.Forms.Button();
            this.numChips = new System.Windows.Forms.NumericUpDown();
            this.nChips = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.btnWash = new System.Windows.Forms.Button();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btn_Advanced = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numWash = new System.Windows.Forms.NumericUpDown();
            this.lblCurrentTip = new System.Windows.Forms.Label();
            this.btnTipSet = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SideSel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SpeedSel = new System.Windows.Forms.ComboBox();
            this.btnCoat1 = new System.Windows.Forms.Button();
            this.btnDrop50 = new System.Windows.Forms.Button();
            this.btnDrop300 = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.boxCalOffset = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnXUp = new System.Windows.Forms.Button();
            this.btnYDown = new System.Windows.Forms.Button();
            this.btnXDown = new System.Windows.Forms.Button();
            this.btnYUp = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chkLarge = new System.Windows.Forms.CheckBox();
            this.chkMed = new System.Windows.Forms.CheckBox();
            this.chkSmall = new System.Windows.Forms.CheckBox();
            this.btnSaveCalibration = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.grpHolder = new System.Windows.Forms.GroupBox();
            this.chkLight = new System.Windows.Forms.CheckBox();
            this.numRotateSt = new System.Windows.Forms.NumericUpDown();
            this.grpLabwareSet = new System.Windows.Forms.GroupBox();
            this.btnLabwareSetup = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchForOT2IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateGit = new System.Windows.Forms.ToolStripMenuItem();
            this.getOT2IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btntest = new System.Windows.Forms.Button();
            this.grpHeader.SuspendLayout();
            this.grpSSHDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChips)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWash)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRotateSt)).BeginInit();
            this.grpLabwareSet.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.btnConnect);
            this.grpHeader.Location = new System.Drawing.Point(20, 29);
            this.grpHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpHeader.Size = new System.Drawing.Size(174, 66);
            this.grpHeader.TabIndex = 0;
            this.grpHeader.TabStop = false;
            this.grpHeader.Text = "Connection";
            // 
            // btnConnect
            // 
            this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConnect.Location = new System.Drawing.Point(37, 24);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(98, 31);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(10, 50);
            this.btnTest2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(147, 30);
            this.btnTest2.TabIndex = 29;
            this.btnTest2.Text = "Create New Labware";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.BtnTest2_Click);
            // 
            // grpSSHDemo
            // 
            this.grpSSHDemo.Controls.Add(this.lbStatus);
            this.grpSSHDemo.Controls.Add(this.txtCommand);
            this.grpSSHDemo.Controls.Add(this.txtSSHConsole);
            this.grpSSHDemo.Location = new System.Drawing.Point(20, 303);
            this.grpSSHDemo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSSHDemo.Name = "grpSSHDemo";
            this.grpSSHDemo.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSSHDemo.Size = new System.Drawing.Size(1092, 172);
            this.grpSSHDemo.TabIndex = 1;
            this.grpSSHDemo.TabStop = false;
            this.grpSSHDemo.Text = "SSH";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(7, 147);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(59, 17);
            this.lbStatus.TabIndex = 2;
            this.lbStatus.Text = "Status:";
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(5, 117);
            this.txtCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(1081, 28);
            this.txtCommand.TabIndex = 1;
            this.txtCommand.TextChanged += new System.EventHandler(this.TxtCommand_TextChanged);
            this.txtCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCommand_KeyPress);
            // 
            // txtSSHConsole
            // 
            this.txtSSHConsole.Location = new System.Drawing.Point(6, 20);
            this.txtSSHConsole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSSHConsole.Multiline = true;
            this.txtSSHConsole.Name = "txtSSHConsole";
            this.txtSSHConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSSHConsole.Size = new System.Drawing.Size(1080, 93);
            this.txtSSHConsole.TabIndex = 0;
            // 
            // btnCoat2
            // 
            this.btnCoat2.Location = new System.Drawing.Point(246, 90);
            this.btnCoat2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCoat2.Name = "btnCoat2";
            this.btnCoat2.Size = new System.Drawing.Size(152, 30);
            this.btnCoat2.TabIndex = 5;
            this.btnCoat2.Text = "2. Wash and Coat";
            this.btnCoat2.UseVisualStyleBackColor = true;
            this.btnCoat2.Click += new System.EventHandler(this.BtnCoat_Click);
            // 
            // numChips
            // 
            this.numChips.Location = new System.Drawing.Point(12, 54);
            this.numChips.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numChips.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numChips.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numChips.Name = "numChips";
            this.numChips.Size = new System.Drawing.Size(108, 22);
            this.numChips.TabIndex = 8;
            this.numChips.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nChips
            // 
            this.nChips.AutoSize = true;
            this.nChips.Location = new System.Drawing.Point(7, 31);
            this.nChips.Name = "nChips";
            this.nChips.Size = new System.Drawing.Size(113, 17);
            this.nChips.TabIndex = 9;
            this.nChips.Text = "Number of Chips";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(145, 31);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(49, 17);
            this.lblSpeed.TabIndex = 11;
            this.lblSpeed.Text = "Speed";
            // 
            // btnWash
            // 
            this.btnWash.Location = new System.Drawing.Point(404, 90);
            this.btnWash.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWash.Name = "btnWash";
            this.btnWash.Size = new System.Drawing.Size(135, 30);
            this.btnWash.TabIndex = 12;
            this.btnWash.Text = "3. Final wash";
            this.btnWash.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnWash.UseVisualStyleBackColor = true;
            this.btnWash.Click += new System.EventHandler(this.BtnWash_Click);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btn_Advanced);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.numWash);
            this.grpControl.Controls.Add(this.lblCurrentTip);
            this.grpControl.Controls.Add(this.btnTipSet);
            this.grpControl.Controls.Add(this.btnStop);
            this.grpControl.Controls.Add(this.SideSel);
            this.grpControl.Controls.Add(this.label1);
            this.grpControl.Controls.Add(this.SpeedSel);
            this.grpControl.Controls.Add(this.lblSpeed);
            this.grpControl.Controls.Add(this.btnWash);
            this.grpControl.Controls.Add(this.btnCoat1);
            this.grpControl.Controls.Add(this.btnCoat2);
            this.grpControl.Controls.Add(this.numChips);
            this.grpControl.Controls.Add(this.nChips);
            this.grpControl.Location = new System.Drawing.Point(542, 29);
            this.grpControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpControl.Name = "grpControl";
            this.grpControl.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpControl.Size = new System.Drawing.Size(570, 190);
            this.grpControl.TabIndex = 2;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "Protocols";
            // 
            // btn_Advanced
            // 
            this.btn_Advanced.Location = new System.Drawing.Point(444, 147);
            this.btn_Advanced.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Advanced.Name = "btn_Advanced";
            this.btn_Advanced.Size = new System.Drawing.Size(120, 30);
            this.btn_Advanced.TabIndex = 26;
            this.btn_Advanced.Text = "Advanced ...";
            this.btn_Advanced.UseVisualStyleBackColor = true;
            this.btn_Advanced.Click += new System.EventHandler(this.Btn_Advanced_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(461, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Washing Steps";
            // 
            // numWash
            // 
            this.numWash.Location = new System.Drawing.Point(464, 54);
            this.numWash.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numWash.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numWash.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWash.Name = "numWash";
            this.numWash.Size = new System.Drawing.Size(75, 22);
            this.numWash.TabIndex = 24;
            this.numWash.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCurrentTip
            // 
            this.lblCurrentTip.AutoSize = true;
            this.lblCurrentTip.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTip.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCurrentTip.Location = new System.Drawing.Point(116, 155);
            this.lblCurrentTip.Name = "lblCurrentTip";
            this.lblCurrentTip.Size = new System.Drawing.Size(101, 18);
            this.lblCurrentTip.TabIndex = 23;
            this.lblCurrentTip.Text = "Current Tip:    ";
            this.lblCurrentTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTipSet
            // 
            this.btnTipSet.Location = new System.Drawing.Point(12, 147);
            this.btnTipSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTipSet.Name = "btnTipSet";
            this.btnTipSet.Size = new System.Drawing.Size(93, 30);
            this.btnTipSet.TabIndex = 21;
            this.btnTipSet.Text = "Tip Setup";
            this.btnTipSet.UseVisualStyleBackColor = true;
            this.btnTipSet.Click += new System.EventHandler(this.BtnTipSet_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(21, 90);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(67, 30);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // SideSel
            // 
            this.SideSel.FormattingEnabled = true;
            this.SideSel.Items.AddRange(new object[] {
            "Apical",
            "Basal",
            "Apical and Basal"});
            this.SideSel.Location = new System.Drawing.Point(269, 54);
            this.SideSel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SideSel.Name = "SideSel";
            this.SideSel.Size = new System.Drawing.Size(179, 24);
            this.SideSel.TabIndex = 16;
            this.SideSel.SelectedIndexChanged += new System.EventHandler(this.SideSel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Chip Side";
            // 
            // SpeedSel
            // 
            this.SpeedSel.FormattingEnabled = true;
            this.SpeedSel.Items.AddRange(new object[] {
            "Fast",
            "Medium",
            "Slow"});
            this.SpeedSel.Location = new System.Drawing.Point(143, 54);
            this.SpeedSel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpeedSel.Name = "SpeedSel";
            this.SpeedSel.Size = new System.Drawing.Size(108, 24);
            this.SpeedSel.TabIndex = 14;
            // 
            // btnCoat1
            // 
            this.btnCoat1.Location = new System.Drawing.Point(108, 90);
            this.btnCoat1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCoat1.Name = "btnCoat1";
            this.btnCoat1.Size = new System.Drawing.Size(132, 30);
            this.btnCoat1.TabIndex = 5;
            this.btnCoat1.Text = "1.Initial Coating";
            this.btnCoat1.UseVisualStyleBackColor = true;
            this.btnCoat1.Click += new System.EventHandler(this.BtnCoat1_Click);
            // 
            // btnDrop50
            // 
            this.btnDrop50.Location = new System.Drawing.Point(364, 29);
            this.btnDrop50.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDrop50.Name = "btnDrop50";
            this.btnDrop50.Size = new System.Drawing.Size(164, 29);
            this.btnDrop50.TabIndex = 20;
            this.btnDrop50.Text = "Drop Pipette Tip 50 ul";
            this.btnDrop50.UseVisualStyleBackColor = true;
            this.btnDrop50.Click += new System.EventHandler(this.BtnDrop50_Click);
            // 
            // btnDrop300
            // 
            this.btnDrop300.Location = new System.Drawing.Point(148, 29);
            this.btnDrop300.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDrop300.Name = "btnDrop300";
            this.btnDrop300.Size = new System.Drawing.Size(164, 29);
            this.btnDrop300.TabIndex = 19;
            this.btnDrop300.Text = "Drop Pipette Tip 300 ul";
            this.btnDrop300.UseVisualStyleBackColor = true;
            this.btnDrop300.Click += new System.EventHandler(this.BtnDrop300_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(21, 29);
            this.btnHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(67, 29);
            this.btnHome.TabIndex = 18;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(17, 24);
            this.btnCalibrate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCalibrate.Size = new System.Drawing.Size(78, 30);
            this.btnCalibrate.TabIndex = 22;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.BtnCalibrate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxCalOffset);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnXUp);
            this.groupBox1.Controls.Add(this.btnYDown);
            this.groupBox1.Controls.Add(this.btnXDown);
            this.groupBox1.Controls.Add(this.btnYUp);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkLarge);
            this.groupBox1.Controls.Add(this.chkMed);
            this.groupBox1.Controls.Add(this.chkSmall);
            this.groupBox1.Controls.Add(this.btnSaveCalibration);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnCalibrate);
            this.groupBox1.Location = new System.Drawing.Point(200, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(336, 190);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calibration";
            // 
            // boxCalOffset
            // 
            this.boxCalOffset.AutoSize = true;
            this.boxCalOffset.Location = new System.Drawing.Point(17, 61);
            this.boxCalOffset.Name = "boxCalOffset";
            this.boxCalOffset.Size = new System.Drawing.Size(110, 21);
            this.boxCalOffset.TabIndex = 43;
            this.boxCalOffset.Text = "With Z offset";
            this.boxCalOffset.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 42;
            this.label5.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 41;
            this.label2.Text = "X";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(112, 155);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 26);
            this.button2.TabIndex = 40;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // btnXUp
            // 
            this.btnXUp.Location = new System.Drawing.Point(224, 114);
            this.btnXUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXUp.Name = "btnXUp";
            this.btnXUp.Size = new System.Drawing.Size(35, 31);
            this.btnXUp.TabIndex = 38;
            this.btnXUp.Text = "-->";
            this.btnXUp.UseVisualStyleBackColor = true;
            this.btnXUp.Click += new System.EventHandler(this.BtnXUp_Click);
            // 
            // btnYDown
            // 
            this.btnYDown.Location = new System.Drawing.Point(186, 114);
            this.btnYDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnYDown.Name = "btnYDown";
            this.btnYDown.Size = new System.Drawing.Size(35, 31);
            this.btnYDown.TabIndex = 37;
            this.btnYDown.Text = "||";
            this.btnYDown.UseVisualStyleBackColor = true;
            this.btnYDown.Click += new System.EventHandler(this.BtnYDown_Click);
            // 
            // btnXDown
            // 
            this.btnXDown.Location = new System.Drawing.Point(150, 114);
            this.btnXDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXDown.Name = "btnXDown";
            this.btnXDown.Size = new System.Drawing.Size(35, 31);
            this.btnXDown.TabIndex = 36;
            this.btnXDown.Text = "<--";
            this.btnXDown.UseVisualStyleBackColor = true;
            this.btnXDown.Click += new System.EventHandler(this.BtnXDown_Click);
            // 
            // btnYUp
            // 
            this.btnYUp.Location = new System.Drawing.Point(186, 82);
            this.btnYUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnYUp.Name = "btnYUp";
            this.btnYUp.Size = new System.Drawing.Size(35, 31);
            this.btnYUp.TabIndex = 35;
            this.btnYUp.Text = "||";
            this.btnYUp.UseVisualStyleBackColor = true;
            this.btnYUp.Click += new System.EventHandler(this.BtnYUp_Click);
            // 
            // btnUp
            // 
            this.btnUp.ForeColor = System.Drawing.Color.Black;
            this.btnUp.Location = new System.Drawing.Point(139, 26);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(63, 29);
            this.btnUp.TabIndex = 23;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 33;
            this.label4.Text = "Step Size";
            // 
            // chkLarge
            // 
            this.chkLarge.AutoSize = true;
            this.chkLarge.Location = new System.Drawing.Point(17, 156);
            this.chkLarge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkLarge.Name = "chkLarge";
            this.chkLarge.Size = new System.Drawing.Size(72, 21);
            this.chkLarge.TabIndex = 31;
            this.chkLarge.Text = "10 mm";
            this.chkLarge.UseVisualStyleBackColor = true;
            this.chkLarge.CheckedChanged += new System.EventHandler(this.ChkLarge_CheckedChanged);
            // 
            // chkMed
            // 
            this.chkMed.AutoSize = true;
            this.chkMed.Location = new System.Drawing.Point(17, 133);
            this.chkMed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkMed.Name = "chkMed";
            this.chkMed.Size = new System.Drawing.Size(64, 21);
            this.chkMed.TabIndex = 30;
            this.chkMed.Text = "1 mm";
            this.chkMed.UseVisualStyleBackColor = true;
            this.chkMed.CheckedChanged += new System.EventHandler(this.ChkMed_CheckedChanged);
            // 
            // chkSmall
            // 
            this.chkSmall.AutoSize = true;
            this.chkSmall.Location = new System.Drawing.Point(17, 112);
            this.chkSmall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSmall.Name = "chkSmall";
            this.chkSmall.Size = new System.Drawing.Size(76, 21);
            this.chkSmall.TabIndex = 29;
            this.chkSmall.Text = "0.1 mm";
            this.chkSmall.UseVisualStyleBackColor = true;
            this.chkSmall.CheckedChanged += new System.EventHandler(this.ChkSmall_CheckedChanged);
            // 
            // btnSaveCalibration
            // 
            this.btnSaveCalibration.Location = new System.Drawing.Point(188, 155);
            this.btnSaveCalibration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveCalibration.Name = "btnSaveCalibration";
            this.btnSaveCalibration.Size = new System.Drawing.Size(78, 26);
            this.btnSaveCalibration.TabIndex = 27;
            this.btnSaveCalibration.Text = "Save Calibration";
            this.btnSaveCalibration.UseVisualStyleBackColor = true;
            this.btnSaveCalibration.Click += new System.EventHandler(this.BtnSaveCalibration_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(207, 26);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(63, 28);
            this.btnDown.TabIndex = 24;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(50, 30);
            this.btnRotate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(92, 30);
            this.btnRotate.TabIndex = 32;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.BtnRotate_Click);
            // 
            // grpHolder
            // 
            this.grpHolder.Controls.Add(this.chkLight);
            this.grpHolder.Controls.Add(this.numRotateSt);
            this.grpHolder.Controls.Add(this.btnRotate);
            this.grpHolder.Location = new System.Drawing.Point(200, 221);
            this.grpHolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpHolder.Name = "grpHolder";
            this.grpHolder.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpHolder.Size = new System.Drawing.Size(336, 84);
            this.grpHolder.TabIndex = 24;
            this.grpHolder.TabStop = false;
            this.grpHolder.Text = "Control Hardware";
            // 
            // chkLight
            // 
            this.chkLight.AutoSize = true;
            this.chkLight.Location = new System.Drawing.Point(219, 35);
            this.chkLight.Name = "chkLight";
            this.chkLight.Size = new System.Drawing.Size(68, 21);
            this.chkLight.TabIndex = 34;
            this.chkLight.Text = "Lights";
            this.chkLight.UseVisualStyleBackColor = true;
            this.chkLight.CheckedChanged += new System.EventHandler(this.ChkLight_CheckedChanged);
            // 
            // numRotateSt
            // 
            this.numRotateSt.Location = new System.Drawing.Point(148, 34);
            this.numRotateSt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numRotateSt.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRotateSt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRotateSt.Name = "numRotateSt";
            this.numRotateSt.Size = new System.Drawing.Size(48, 22);
            this.numRotateSt.TabIndex = 33;
            this.numRotateSt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpLabwareSet
            // 
            this.grpLabwareSet.Controls.Add(this.btnLabwareSetup);
            this.grpLabwareSet.Controls.Add(this.btnTest2);
            this.grpLabwareSet.Location = new System.Drawing.Point(20, 99);
            this.grpLabwareSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLabwareSet.Name = "grpLabwareSet";
            this.grpLabwareSet.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLabwareSet.Size = new System.Drawing.Size(174, 153);
            this.grpLabwareSet.TabIndex = 25;
            this.grpLabwareSet.TabStop = false;
            this.grpLabwareSet.Text = "Labware Setup";
            // 
            // btnLabwareSetup
            // 
            this.btnLabwareSetup.Location = new System.Drawing.Point(10, 107);
            this.btnLabwareSetup.Name = "btnLabwareSetup";
            this.btnLabwareSetup.Size = new System.Drawing.Size(147, 27);
            this.btnLabwareSetup.TabIndex = 30;
            this.btnLabwareSetup.Text = "Labware Setup";
            this.btnLabwareSetup.UseVisualStyleBackColor = true;
            this.btnLabwareSetup.Click += new System.EventHandler(this.btnLabwareSetup_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1124, 28);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gitToolStripMenuItem,
            this.searchForOT2IPToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // gitToolStripMenuItem
            // 
            this.gitToolStripMenuItem.Name = "gitToolStripMenuItem";
            this.gitToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.gitToolStripMenuItem.Text = "UpdateGit";
            this.gitToolStripMenuItem.Click += new System.EventHandler(this.UpdateGit_Click);
            // 
            // searchForOT2IPToolStripMenuItem
            // 
            this.searchForOT2IPToolStripMenuItem.Name = "searchForOT2IPToolStripMenuItem";
            this.searchForOT2IPToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.searchForOT2IPToolStripMenuItem.Text = "Search for OT-2 IP";
            this.searchForOT2IPToolStripMenuItem.Click += new System.EventHandler(this.GetOT2IPToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(45, 24);
            this.toolStripMenuItem1.Text = "GIT";
            // 
            // UpdateGit
            // 
            this.UpdateGit.Name = "UpdateGit";
            this.UpdateGit.Size = new System.Drawing.Size(224, 26);
            this.UpdateGit.Text = "Update Git";
            this.UpdateGit.Click += new System.EventHandler(this.UpdateGit_Click);
            // 
            // getOT2IPToolStripMenuItem
            // 
            this.getOT2IPToolStripMenuItem.Name = "getOT2IPToolStripMenuItem";
            this.getOT2IPToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.getOT2IPToolStripMenuItem.Text = "Get OT2 IP";
            this.getOT2IPToolStripMenuItem.Click += new System.EventHandler(this.GetOT2IPToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHome);
            this.groupBox2.Controls.Add(this.btnDrop300);
            this.groupBox2.Controls.Add(this.btnDrop50);
            this.groupBox2.Location = new System.Drawing.Point(542, 223);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(570, 82);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Robot";
            // 
            // btntest
            // 
            this.btntest.Location = new System.Drawing.Point(30, 258);
            this.btntest.Name = "btntest";
            this.btntest.Size = new System.Drawing.Size(75, 23);
            this.btntest.TabIndex = 36;
            this.btntest.Text = "button1";
            this.btntest.UseVisualStyleBackColor = true;
            this.btntest.Click += new System.EventHandler(this.Btntest_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 482);
            this.Controls.Add(this.btntest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpLabwareSet);
            this.Controls.Add(this.grpHolder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.grpSSHDemo);
            this.Controls.Add(this.grpHeader);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.Text = " Coat APP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpHeader.ResumeLayout(false);
            this.grpSSHDemo.ResumeLayout(false);
            this.grpSSHDemo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChips)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWash)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpHolder.ResumeLayout(false);
            this.grpHolder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRotateSt)).EndInit();
            this.grpLabwareSet.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.GroupBox grpSSHDemo;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.TextBox txtSSHConsole;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnCoat2;
        private System.Windows.Forms.NumericUpDown numChips;
        private System.Windows.Forms.Label nChips;
        private System.Windows.Forms.Button btnWash;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.ComboBox SpeedSel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SideSel;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnDrop50;
        private System.Windows.Forms.Button btnDrop300;
        private System.Windows.Forms.Button btnTipSet;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label lblCurrentTip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numWash;
        private System.Windows.Forms.Button btn_Advanced;
        private System.Windows.Forms.Button btnSaveCalibration;
        private System.Windows.Forms.CheckBox chkLarge;
        private System.Windows.Forms.CheckBox chkMed;
        private System.Windows.Forms.CheckBox chkSmall;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpHolder;
        private System.Windows.Forms.GroupBox grpLabwareSet;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem UpdateGit;
        private System.Windows.Forms.NumericUpDown numRotateSt;
        private System.Windows.Forms.ToolStripMenuItem getOT2IPToolStripMenuItem;
        private System.Windows.Forms.Button btnXUp;
        private System.Windows.Forms.Button btnYDown;
        private System.Windows.Forms.Button btnXDown;
        private System.Windows.Forms.Button btnYUp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox boxCalOffset;
        private System.Windows.Forms.Button btnLabwareSetup;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForOT2IPToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkLight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCoat1;
        private System.Windows.Forms.Button btntest;
    }
}

