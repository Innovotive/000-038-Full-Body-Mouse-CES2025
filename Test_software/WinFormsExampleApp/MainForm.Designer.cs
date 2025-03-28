namespace HapticoreExampleApp
{
	partial class MainForm
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
			this.btnMediumTicks = new System.Windows.Forms.Button();
			this.gbHapticModes = new System.Windows.Forms.GroupBox();
			this.btnNone = new System.Windows.Forms.Button();
			this.btnClockwise = new System.Windows.Forms.Button();
			this.btnConstantCurrent = new System.Windows.Forms.Button();
			this.btnFineTicks = new System.Windows.Forms.Button();
			this.btnTicksWithBarrier = new System.Windows.Forms.Button();
			this.btnContrastingTicks = new System.Windows.Forms.Button();
			this.btnBarrier = new System.Windows.Forms.Button();
			this.btnRoughTicks = new System.Windows.Forms.Button();
			this.lblAngle = new System.Windows.Forms.Label();
			this.lblConnection = new System.Windows.Forms.Label();
			this.lblTickCounter = new System.Windows.Forms.Label();
			this.lblMultiTurnCounter = new System.Windows.Forms.Label();
			this.gbGetExample = new System.Windows.Forms.GroupBox();
			this.txtTickAngleCCW = new System.Windows.Forms.TextBox();
			this.lblTickAngleCCW = new System.Windows.Forms.Label();
			this.txtTickAngleCW = new System.Windows.Forms.TextBox();
			this.lblTickAngleCW = new System.Windows.Forms.Label();
			this.btnGetTickAngle = new System.Windows.Forms.Button();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblButtonState = new System.Windows.Forms.Label();
			this.gbHapticModes.SuspendLayout();
			this.gbGetExample.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			// btnMediumTicks
			//
			this.btnMediumTicks.Location = new System.Drawing.Point(126, 19);
			this.btnMediumTicks.Name = "btnMediumTicks";
			this.btnMediumTicks.Size = new System.Drawing.Size(112, 45);
			this.btnMediumTicks.TabIndex = 20;
			this.btnMediumTicks.Text = "Medium Ticks";
			this.btnMediumTicks.UseVisualStyleBackColor = true;
			this.btnMediumTicks.Click += new System.EventHandler(this.btnMediumTicks_Click);
			//
			// gbHapticModes
			//
			this.gbHapticModes.Controls.Add(this.btnNone);
			this.gbHapticModes.Controls.Add(this.btnClockwise);
			this.gbHapticModes.Controls.Add(this.btnConstantCurrent);
			this.gbHapticModes.Controls.Add(this.btnFineTicks);
			this.gbHapticModes.Controls.Add(this.btnTicksWithBarrier);
			this.gbHapticModes.Controls.Add(this.btnContrastingTicks);
			this.gbHapticModes.Controls.Add(this.btnBarrier);
			this.gbHapticModes.Controls.Add(this.btnMediumTicks);
			this.gbHapticModes.Controls.Add(this.btnRoughTicks);
			this.gbHapticModes.Enabled = false;
			this.gbHapticModes.Location = new System.Drawing.Point(15, 86);
			this.gbHapticModes.Name = "gbHapticModes";
			this.gbHapticModes.Size = new System.Drawing.Size(365, 191);
			this.gbHapticModes.TabIndex = 14;
			this.gbHapticModes.TabStop = false;
			this.gbHapticModes.Text = "Haptic Mode Samples";
			//
			// btnNone
			//
			this.btnNone.Location = new System.Drawing.Point(244, 121);
			this.btnNone.Name = "btnNone";
			this.btnNone.Size = new System.Drawing.Size(112, 45);
			this.btnNone.TabIndex = 120;
			this.btnNone.Text = "None";
			this.btnNone.UseVisualStyleBackColor = true;
			this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
			//
			// btnClockwise
			//
			this.btnClockwise.Location = new System.Drawing.Point(244, 70);
			this.btnClockwise.Name = "btnClockwise";
			this.btnClockwise.Size = new System.Drawing.Size(112, 45);
			this.btnClockwise.TabIndex = 100;
			this.btnClockwise.Text = "Clockwise";
			this.btnClockwise.UseVisualStyleBackColor = true;
			this.btnClockwise.Click += new System.EventHandler(this.btnClockwise_Click);
			//
			// btnConstantCurrent
			//
			this.btnConstantCurrent.Location = new System.Drawing.Point(126, 70);
			this.btnConstantCurrent.Name = "btnConstantCurrent";
			this.btnConstantCurrent.Size = new System.Drawing.Size(112, 45);
			this.btnConstantCurrent.TabIndex = 90;
			this.btnConstantCurrent.Text = "Constant Current";
			this.btnConstantCurrent.UseVisualStyleBackColor = true;
			this.btnConstantCurrent.Click += new System.EventHandler(this.btnConstantCurrent_Click);
			//
			// btnFineTicks
			//
			this.btnFineTicks.Location = new System.Drawing.Point(6, 19);
			this.btnFineTicks.Name = "btnFineTicks";
			this.btnFineTicks.Size = new System.Drawing.Size(112, 45);
			this.btnFineTicks.TabIndex = 10;
			this.btnFineTicks.Text = "Fine Ticks";
			this.btnFineTicks.UseVisualStyleBackColor = true;
			this.btnFineTicks.Click += new System.EventHandler(this.btnFineTicks_Click);
			//
			// btnTicksWithBarrier
			//
			this.btnTicksWithBarrier.Location = new System.Drawing.Point(126, 121);
			this.btnTicksWithBarrier.Name = "btnTicksWithBarrier";
			this.btnTicksWithBarrier.Size = new System.Drawing.Size(112, 45);
			this.btnTicksWithBarrier.TabIndex = 50;
			this.btnTicksWithBarrier.Text = "Ticks + Barrier";
			this.btnTicksWithBarrier.UseVisualStyleBackColor = true;
			this.btnTicksWithBarrier.Click += new System.EventHandler(this.btnTicksWithBarrier_Click);
			//
			// btnContrastingTicks
			//
			this.btnContrastingTicks.Location = new System.Drawing.Point(6, 121);
			this.btnContrastingTicks.Name = "btnContrastingTicks";
			this.btnContrastingTicks.Size = new System.Drawing.Size(112, 45);
			this.btnContrastingTicks.TabIndex = 40;
			this.btnContrastingTicks.Text = "Contrasting Ticks";
			this.btnContrastingTicks.UseVisualStyleBackColor = true;
			this.btnContrastingTicks.Click += new System.EventHandler(this.btnContrastingTicks_Click);
			//
			// btnBarrier
			//
			this.btnBarrier.Location = new System.Drawing.Point(6, 70);
			this.btnBarrier.Name = "btnBarrier";
			this.btnBarrier.Size = new System.Drawing.Size(112, 45);
			this.btnBarrier.TabIndex = 80;
			this.btnBarrier.Text = "Barrier";
			this.btnBarrier.UseVisualStyleBackColor = true;
			this.btnBarrier.Click += new System.EventHandler(this.btnBarrier_Click);
			//
			// btnRoughTicks
			//
			this.btnRoughTicks.Location = new System.Drawing.Point(244, 19);
			this.btnRoughTicks.Name = "btnRoughTicks";
			this.btnRoughTicks.Size = new System.Drawing.Size(112, 45);
			this.btnRoughTicks.TabIndex = 30;
			this.btnRoughTicks.Text = "Rough Ticks";
			this.btnRoughTicks.UseVisualStyleBackColor = true;
			this.btnRoughTicks.Click += new System.EventHandler(this.btnRoughTicks_Click);
			//
			// lblAngle
			//
			this.lblAngle.AutoSize = true;
			this.lblAngle.Location = new System.Drawing.Point(6, 36);
			this.lblAngle.Name = "lblAngle";
			this.lblAngle.Size = new System.Drawing.Size(65, 13);
			this.lblAngle.TabIndex = 13;
			this.lblAngle.Text = "Angle: 0.00°";
			//
			// lblConnection
			//
			this.lblConnection.AutoSize = true;
			this.lblConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblConnection.Location = new System.Drawing.Point(12, 291);
			this.lblConnection.Name = "lblConnection";
			this.lblConnection.Size = new System.Drawing.Size(129, 13);
			this.lblConnection.TabIndex = 16;
			this.lblConnection.Text = "Status: Disconnected";
			//
			// lblTickCounter
			//
			this.lblTickCounter.AutoSize = true;
			this.lblTickCounter.Location = new System.Drawing.Point(6, 78);
			this.lblTickCounter.Name = "lblTickCounter";
			this.lblTickCounter.Size = new System.Drawing.Size(79, 13);
			this.lblTickCounter.TabIndex = 20;
			this.lblTickCounter.Text = "Tick counter: 0";
			//
			// lblMultiTurnCounter
			//
			this.lblMultiTurnCounter.AutoSize = true;
			this.lblMultiTurnCounter.Location = new System.Drawing.Point(6, 57);
			this.lblMultiTurnCounter.Name = "lblMultiTurnCounter";
			this.lblMultiTurnCounter.Size = new System.Drawing.Size(101, 13);
			this.lblMultiTurnCounter.TabIndex = 22;
			this.lblMultiTurnCounter.Text = "Multi turn counter: 0";
			//
			// gbGetExample
			//
			this.gbGetExample.Controls.Add(this.txtTickAngleCCW);
			this.gbGetExample.Controls.Add(this.lblTickAngleCCW);
			this.gbGetExample.Controls.Add(this.txtTickAngleCW);
			this.gbGetExample.Controls.Add(this.lblTickAngleCW);
			this.gbGetExample.Controls.Add(this.btnGetTickAngle);
			this.gbGetExample.Enabled = false;
			this.gbGetExample.Location = new System.Drawing.Point(386, 86);
			this.gbGetExample.Name = "gbGetExample";
			this.gbGetExample.Size = new System.Drawing.Size(124, 191);
			this.gbGetExample.TabIndex = 23;
			this.gbGetExample.TabStop = false;
			this.gbGetExample.Text = "Get Example";
			//
			// txtTickAngleCCW
			//
			this.txtTickAngleCCW.Location = new System.Drawing.Point(6, 153);
			this.txtTickAngleCCW.Name = "txtTickAngleCCW";
			this.txtTickAngleCCW.ReadOnly = true;
			this.txtTickAngleCCW.Size = new System.Drawing.Size(112, 20);
			this.txtTickAngleCCW.TabIndex = 4;
			//
			// lblTickAngleCCW
			//
			this.lblTickAngleCCW.AutoSize = true;
			this.lblTickAngleCCW.Location = new System.Drawing.Point(6, 137);
			this.lblTickAngleCCW.Name = "lblTickAngleCCW";
			this.lblTickAngleCCW.Size = new System.Drawing.Size(86, 13);
			this.lblTickAngleCCW.TabIndex = 3;
			this.lblTickAngleCCW.Text = "Tick Angle CCW";
			//
			// txtTickAngleCW
			//
			this.txtTickAngleCW.Location = new System.Drawing.Point(6, 102);
			this.txtTickAngleCW.Name = "txtTickAngleCW";
			this.txtTickAngleCW.ReadOnly = true;
			this.txtTickAngleCW.Size = new System.Drawing.Size(112, 20);
			this.txtTickAngleCW.TabIndex = 2;
			//
			// lblTickAngleCW
			//
			this.lblTickAngleCW.AutoSize = true;
			this.lblTickAngleCW.Location = new System.Drawing.Point(6, 86);
			this.lblTickAngleCW.Name = "lblTickAngleCW";
			this.lblTickAngleCW.Size = new System.Drawing.Size(79, 13);
			this.lblTickAngleCW.TabIndex = 1;
			this.lblTickAngleCW.Text = "Tick Angle CW";
			//
			// btnGetTickAngle
			//
			this.btnGetTickAngle.Location = new System.Drawing.Point(9, 19);
			this.btnGetTickAngle.Name = "btnGetTickAngle";
			this.btnGetTickAngle.Size = new System.Drawing.Size(112, 45);
			this.btnGetTickAngle.TabIndex = 0;
			this.btnGetTickAngle.Text = "Get Tick Angle";
			this.btnGetTickAngle.UseVisualStyleBackColor = true;
			this.btnGetTickAngle.Click += new System.EventHandler(this.btnGetTickAngle_Click);
			//
			// pbLogo
			//
			this.pbLogo.ImageLocation = "";
			this.pbLogo.Location = new System.Drawing.Point(15, 17);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(255, 51);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogo.TabIndex = 24;
			this.pbLogo.TabStop = false;
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.lblButtonState);
			this.groupBox1.Controls.Add(this.lblAngle);
			this.groupBox1.Controls.Add(this.lblMultiTurnCounter);
			this.groupBox1.Controls.Add(this.lblTickCounter);
			this.groupBox1.Location = new System.Drawing.Point(516, 85);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(127, 192);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Report Example";
			//
			// lblButtonState
			//
			this.lblButtonState.AutoSize = true;
			this.lblButtonState.Location = new System.Drawing.Point(6, 99);
			this.lblButtonState.Name = "lblButtonState";
			this.lblButtonState.Size = new System.Drawing.Size(73, 13);
			this.lblButtonState.TabIndex = 23;
			this.lblButtonState.Text = "Button state: -";
			//
			// MainForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(653, 317);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.gbGetExample);
			this.Controls.Add(this.gbHapticModes);
			this.Controls.Add(this.lblConnection);
			this.Name = "MainForm";
			this.Text = "Hapticore Example App V3";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
			this.gbHapticModes.ResumeLayout(false);
			this.gbGetExample.ResumeLayout(false);
			this.gbGetExample.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnMediumTicks;
		private System.Windows.Forms.GroupBox gbHapticModes;
		private System.Windows.Forms.Button btnBarrier;
		private System.Windows.Forms.Button btnRoughTicks;
		private System.Windows.Forms.Label lblAngle;
		private System.Windows.Forms.Button btnTicksWithBarrier;
		private System.Windows.Forms.Button btnConstantCurrent;
		private System.Windows.Forms.Button btnContrastingTicks;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.Button btnNone;
		private System.Windows.Forms.Button btnClockwise;
		private System.Windows.Forms.Button btnFineTicks;
		private System.Windows.Forms.Label lblTickCounter;
		private System.Windows.Forms.Label lblMultiTurnCounter;
		private System.Windows.Forms.GroupBox gbGetExample;
		private System.Windows.Forms.TextBox txtTickAngleCCW;
		private System.Windows.Forms.Label lblTickAngleCCW;
		private System.Windows.Forms.TextBox txtTickAngleCW;
		private System.Windows.Forms.Label lblTickAngleCW;
		private System.Windows.Forms.Button btnGetTickAngle;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblButtonState;
	}
}

