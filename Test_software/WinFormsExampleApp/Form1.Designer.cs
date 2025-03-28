using System.Drawing;
using System.Windows.Forms;

namespace HapticoreExampleApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            ComportBox = new NumericUpDown();
            label15 = new Label();
            ConnectButton = new Button();
            DisconnectButton = new Button();
            panel4 = new Panel();
            button1 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            toolTip1 = new ToolTip(components);
            connectedlabel = new Label();
            ShowPortButton = new Button();
            Zone1 = new Panel();
            Zone2 = new Panel();
            numericUpDown4 = new NumericUpDown();
            Zone3 = new Panel();
            label4 = new Label();
            intensitynumber = new NumericUpDown();
            ZoneFS = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label6 = new Label();
            tempbutton = new Button();
            Mediumbutton = new Button();
            HardButton = new Button();
            SerialOut = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ComportBox).BeginInit();
            panel4.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            Zone2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)intensitynumber).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            //pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new Point(328, 65);
            pictureBox1.Margin = new Padding(4, 2, 4, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(724, 171);
            pictureBox1.TabIndex = 27;
            pictureBox1.TabStop = false;
            // 
            // ComportBox
            // 
            ComportBox.Location = new Point(245, 308);
            ComportBox.Margin = new Padding(4, 2, 4, 2);
            ComportBox.Name = "ComportBox";
            ComportBox.Size = new Size(234, 43);
            ComportBox.TabIndex = 43;
            ComportBox.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(74, 317);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(123, 37);
            label15.TabIndex = 40;
            label15.Text = "Comport";
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(688, 308);
            ConnectButton.Margin = new Padding(4, 2, 4, 2);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(182, 50);
            ConnectButton.TabIndex = 44;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // DisconnectButton
            // 
            DisconnectButton.Location = new Point(884, 308);
            DisconnectButton.Margin = new Padding(4, 2, 4, 2);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(182, 50);
            DisconnectButton.TabIndex = 45;
            DisconnectButton.Text = "Disconnect";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(button1);
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(200, 100);
            panel4.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(247, 141);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(81, 22);
            button1.TabIndex = 39;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
            tableLayoutPanel2.Controls.Add(numericUpDown1, 1, 3);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.Size = new Size(200, 100);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(110, 1);
            numericUpDown1.Margin = new Padding(2, 1, 2, 1);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(104, 43);
            numericUpDown1.TabIndex = 10;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(2, 1);
            numericUpDown2.Margin = new Padding(2, 1, 2, 1);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(104, 43);
            numericUpDown2.TabIndex = 9;
            // 
            // connectedlabel
            // 
            connectedlabel.AutoSize = true;
            connectedlabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            connectedlabel.ForeColor = Color.Red;
            connectedlabel.Location = new Point(1080, 317);
            connectedlabel.Margin = new Padding(4, 0, 4, 0);
            connectedlabel.Name = "connectedlabel";
            connectedlabel.Size = new Size(190, 37);
            connectedlabel.TabIndex = 61;
            connectedlabel.Text = "Disconnected";
            // 
            // ShowPortButton
            // 
            ShowPortButton.Location = new Point(493, 308);
            ShowPortButton.Margin = new Padding(4, 2, 4, 2);
            ShowPortButton.Name = "ShowPortButton";
            ShowPortButton.Size = new Size(182, 50);
            ShowPortButton.TabIndex = 64;
            ShowPortButton.Text = "Show Ports";
            ShowPortButton.UseVisualStyleBackColor = true;
            ShowPortButton.Click += ShowPortButton_Click;
            // 
            // Zone1
            // 
            Zone1.BackColor = SystemColors.ControlLight;
            Zone1.BorderStyle = BorderStyle.FixedSingle;
            Zone1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Zone1.ForeColor = SystemColors.ActiveCaptionText;
            Zone1.Location = new Point(884, 466);
            Zone1.Margin = new Padding(7, 7, 7, 7);
            Zone1.Name = "Zone1";
            Zone1.Size = new Size(402, 101);
            Zone1.TabIndex = 140;
            Zone1.Enter += Zone1_Enter;
            Zone1.Leave += Zone1_Leave;
            Zone1.MouseEnter += Zone1_MouseEnter;
            Zone1.MouseLeave += Zone1_MouseLeave;
            // 
            // Zone2
            // 
            Zone2.BackColor = SystemColors.ControlDarkDark;
            Zone2.BorderStyle = BorderStyle.FixedSingle;
            Zone2.Controls.Add(numericUpDown4);
            Zone2.ForeColor = SystemColors.ButtonFace;
            Zone2.Location = new Point(884, 655);
            Zone2.Margin = new Padding(7, 7, 7, 7);
            Zone2.Name = "Zone2";
            Zone2.Size = new Size(402, 112);
            Zone2.TabIndex = 157;
            Zone2.Enter += Zone2_Enter;
            Zone2.Leave += Zone2_Leave;
            Zone2.MouseEnter += Zone2_MouseEnter;
            Zone2.MouseLeave += Zone2_MouseLeave;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(1476, 664);
            numericUpDown4.Margin = new Padding(4, 2, 4, 2);
            numericUpDown4.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(128, 43);
            numericUpDown4.TabIndex = 155;
            numericUpDown4.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // Zone3
            // 
            Zone3.BackColor = SystemColors.ActiveCaptionText;
            Zone3.BorderStyle = BorderStyle.FixedSingle;
            Zone3.Location = new Point(884, 864);
            Zone3.Margin = new Padding(7, 7, 7, 7);
            Zone3.Name = "Zone3";
            Zone3.Size = new Size(402, 121);
            Zone3.TabIndex = 142;
            Zone3.Enter += Zone3_Enter;
            Zone3.Leave += Zone3_Leave;
            Zone3.MouseEnter += Zone3_MouseEnter;
            Zone3.MouseLeave += Zone3_MouseLeave;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(61, 556);
            label4.Margin = new Padding(7, 0, 7, 0);
            label4.Name = "label4";
            label4.Size = new Size(117, 37);
            label4.TabIndex = 158;
            label4.Text = "Intensity";
            // 
            // intensitynumber
            // 
            intensitynumber.Location = new Point(245, 547);
            intensitynumber.Margin = new Padding(4, 2, 4, 2);
            intensitynumber.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            intensitynumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            intensitynumber.Name = "intensitynumber";
            intensitynumber.Size = new Size(234, 43);
            intensitynumber.TabIndex = 159;
            intensitynumber.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // ZoneFS
            // 
            ZoneFS.BackColor = SystemColors.ActiveCaption;
            ZoneFS.BorderStyle = BorderStyle.FixedSingle;
            ZoneFS.Location = new Point(142, 686);
            ZoneFS.Margin = new Padding(4, 4, 4, 4);
            ZoneFS.Name = "ZoneFS";
            ZoneFS.Size = new Size(106, 90);
            ZoneFS.TabIndex = 162;
            ZoneFS.MouseEnter += ZoneFS_MouseEnter;
            ZoneFS.MouseLeave += ZoneFS_MouseLeave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1040, 400);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 37);
            label1.TabIndex = 163;
            label1.Text = "Soft";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1012, 601);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(116, 37);
            label2.TabIndex = 164;
            label2.Text = "Medium";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1033, 812);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(75, 37);
            label3.TabIndex = 165;
            label3.Text = "Hard";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(61, 634);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(223, 37);
            label6.TabIndex = 166;
            label6.Text = "Frequency Sweep";
            // 
            // tempbutton
            // 
            tempbutton.Location = new Point(603, 493);
            tempbutton.Margin = new Padding(4, 2, 4, 2);
            tempbutton.Name = "tempbutton";
            tempbutton.Size = new Size(223, 50);
            tempbutton.TabIndex = 168;
            tempbutton.Text = "Button Soft";
            tempbutton.UseVisualStyleBackColor = true;
            tempbutton.Visible = false;
            tempbutton.Click += tempbutton_Click;
            tempbutton.MouseDown += tempbutton_MouseDown;
            // 
            // Mediumbutton
            // 
            Mediumbutton.Location = new Point(603, 686);
            Mediumbutton.Margin = new Padding(4, 2, 4, 2);
            Mediumbutton.Name = "Mediumbutton";
            Mediumbutton.Size = new Size(223, 50);
            Mediumbutton.TabIndex = 169;
            Mediumbutton.Text = "Button Medium";
            Mediumbutton.UseVisualStyleBackColor = true;
            Mediumbutton.Visible = false;
            Mediumbutton.MouseDown += Mediumbutton_MouseDown;
            // 
            // HardButton
            // 
            HardButton.Location = new Point(603, 880);
            HardButton.Margin = new Padding(4, 2, 4, 2);
            HardButton.Name = "HardButton";
            HardButton.Size = new Size(223, 50);
            HardButton.TabIndex = 170;
            HardButton.Text = "Button Hard";
            HardButton.UseVisualStyleBackColor = true;
            HardButton.Visible = false;
            HardButton.MouseDown += HardButton_MouseDown;
            // 
            // SerialOut
            // 
            SerialOut.AutoSize = true;
            SerialOut.Location = new Point(74, 812);
            SerialOut.Margin = new Padding(4, 0, 4, 0);
            SerialOut.Name = "SerialOut";
            SerialOut.Size = new Size(79, 37);
            SerialOut.TabIndex = 171;
            SerialOut.Text = "serial";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(216F, 216F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1382, 1071);
            Controls.Add(SerialOut);
            Controls.Add(HardButton);
            Controls.Add(Mediumbutton);
            Controls.Add(tempbutton);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ZoneFS);
            Controls.Add(intensitynumber);
            Controls.Add(label4);
            Controls.Add(Zone2);
            Controls.Add(Zone3);
            Controls.Add(Zone1);
            Controls.Add(ShowPortButton);
            Controls.Add(connectedlabel);
            Controls.Add(DisconnectButton);
            Controls.Add(ConnectButton);
            Controls.Add(label15);
            Controls.Add(ComportBox);
            Controls.Add(pictureBox1);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 2, 4, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ComportBox).EndInit();
            panel4.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            Zone2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)intensitynumber).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private NumericUpDown ComportBox;
        private Label label15;
        private Button ConnectButton;
        private Button DisconnectButton;
        private Panel panel4;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel2;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private ToolTip toolTip1;
        private Label connectedlabel;
        private Button ShowPortButton;
        private NumericUpDown pulsedMinimum;
        private Panel Zone1;
        private Panel Zone2;
        private NumericUpDown numericUpDown4;
        private Panel Zone3;
        private Label label4;
        private NumericUpDown intensitynumber;
        private NumericUpDown mousenumber;
        private Panel ZoneFS;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label6;
        private Button tempbutton;
        private Button Mediumbutton;
        private Button HardButton;
        private Label SerialOut;
    }
}
