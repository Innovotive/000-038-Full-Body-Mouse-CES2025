using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Hapticore.Device;
using Hapticore.Types;
using DataReceivedEventArgs = Hapticore.Device.DataReceivedEventArgs;

namespace HapticoreExampleApp
{
	public partial class MainForm : Form
	{
		private readonly HapticoreDevice device;

		private readonly System.Timers.Timer reconnectionTimer
			= new System.Timers.Timer();


		public MainForm()
		{
			InitializeComponent();

			Image img = Image.FromFile("logo.png");
			pbLogo.Image = img;

			device = new HapticoreDevice();

			device.ConnectionChanged += HandleConnectionChanged;
			device.DataReceived += HandleDataReceived;
			device.ErrorOccurred += HandleErrorOccured;

			reconnectionTimer.Interval = 2000;
			reconnectionTimer.Elapsed += (sender, e) => Connect();
			reconnectionTimer.Start();
		}

		public void Connect()
		{
			if (device.Connected)
			{
				return;
			}

			try
			{
				// Connects to the first available FTDI device
				bool success = device.Connect(out string comPort, out Exception exc);
				if (success)
				{
					Debug.WriteLine($"Connected to COM port {comPort} successfully.");
				}
				else
				{
					Debug.WriteLine("No suitable device found!");
					return;
				}

				// Note: Loading default values is optional!
				device.Commands.SendLoadDefaultValues();

				// Set the reports necessary for this example application
				device.Report.SendFlags(
					ReportFlags.ENCODER_ANGLE
					| ReportFlags.PUSH_PULL_STATE
					| ReportFlags.ENCODER_MULTI_TURN_COUNT
					| ReportFlags.TICK_INDEX
				);

				device.Encoder.SendAngle(0f);
			}
			catch (Exception exc)
			{
				Debug.WriteLine($"Connecting failed with exception={exc}");
			}
		}

		private void DisableHapticFunctions()
		{
			device.Commands.SendDisableAllHapticFunctions();
		}

		private void btnFineTicks_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Tick.SendMode(TickMode.TICK_MODE_1);
			device.Tick.SendAngleCw(7f);
			device.Tick.SendAngleCcw(0f);
			device.Tick.SendDurationMin(0.001f);
			device.Tick.SendDurationMax(0.004f);
			device.Tick.SendCurrent(0.2f);
			device.Tick.SendEnable(true);

			lblTickCounter.Visible = true;
		}

		private void btnMediumTicks_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Tick.SendMode(TickMode.TICK_MODE_1);
			device.Tick.SendAngleCw(14f);
			device.Tick.SendAngleCcw(0f);
			device.Tick.SendDurationMin(0.001f);
			device.Tick.SendDurationMax(0.004f);
			device.Tick.SendCurrent(0.35f);
			device.Tick.SendEnable(true);

			lblTickCounter.Visible = true;
		}

		private void btnRoughTicks_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Tick.SendMode(TickMode.TICK_MODE_1);
			device.Tick.SendAngleCw(20f);
			device.Tick.SendAngleCcw(0f);
			device.Tick.SendDurationMin(0.01f);
			device.Tick.SendDurationMax(0.05f);
			device.Tick.SendCurrent(0.4f);
			device.Tick.SendEnable(true);

			lblTickCounter.Visible = true;
		}

		private void btnBarrier_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Barrier.SendStartAngle(-90f);
			device.Barrier.SendStopAngle(90f);
			device.Barrier.SendCurrent(0.5f);
			device.Barrier.SendEnable(true);

			device.Encoder.SendAngle(0f);

			lblTickCounter.Visible = false;
		}

		private void btnContrastingTicks_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Tick.SendMode(TickMode.TICK_MODE_1);
			device.Tick.SendAngleCw(20f);
			device.Tick.SendAngleCcw(5f);
			device.Tick.SendDurationMin(0.002f);
			device.Tick.SendDurationMax(0.01f);
			device.Tick.SendCurrent(0.25f);
			device.Tick.SendEnable(true);

			lblTickCounter.Visible = true;
		}

		private void btnConstantCurrent_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Current.SendStartCurrentCw(0.06f);
			device.Current.SendStopCurrentCw(0.06f);
			device.Current.SendStartCurrentCcw(0.06f);
			device.Current.SendStopCurrentCcw(0.06f);
			device.Current.SendEnable(true);

			lblTickCounter.Visible = false;
		}

		private void btnTicksWithBarrier_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Tick.SendMode(TickMode.TICK_MODE_1);
			device.Tick.SendAngleCw(15f);
			device.Tick.SendAngleCcw(0f);
			device.Tick.SendDurationMin(0.002f);
			device.Tick.SendDurationMax(0.005f);
			device.Tick.SendCurrent(0.7f);
			device.Tick.SendEnable(true);

			device.Barrier.SendStartAngle(-90f);
			device.Barrier.SendStopAngle(90f);
			device.Barrier.SendCurrent(0.4f);
			device.Barrier.SendEnable(true);

			device.Encoder.SendAngle(0f);

			lblTickCounter.Visible = true;
		}

		private void btnClockwise_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();

			device.Lock.SendDirection(ActiveDirection.CCW);
			device.Lock.SendEnable(true);

			lblTickCounter.Visible = false;
		}

		private void btnNone_Click(object sender, EventArgs e)
		{
			DisableHapticFunctions();
		}

		private void btnGetTickAngle_Click(object sender, EventArgs e)
		{
			device.Tick.RequestAngleCw();
			device.Tick.RequestAngleCcw();
		}

		private void HandleConnectionChanged(bool connected)
		{
			if (connected)
			{
				reconnectionTimer.Stop();
			}
			else
			{
				reconnectionTimer.Start();
			}

			// Update UI
			BeginInvoke((MethodInvoker)delegate
			{
				lblConnection.Text = device.Connected ? "Status: Connected" : "Status: Disconnected";
				gbHapticModes.Enabled = device.Connected;
				gbGetExample.Enabled = device.Connected;
			});
		}

		private void HandleDataReceived(DataReceivedEventArgs args)
		{
			if ((ReplyStatus)args.Status != ReplyStatus.OK)
			{
				return;
			}

			switch (args.Register)
			{
				case Register.REPORT_ENCODER_ANGLE:
				{
					BeginInvoke((MethodInvoker)delegate { lblAngle.Text = $"Angle: {args.Value}°"; });
				}
			    break;

				case Register.REPORT_ENCODER_MULTI_TURN_COUNT:
				{
					BeginInvoke((MethodInvoker)delegate { lblMultiTurnCounter.Text = $"Multi Turn Counter: {args.Value}"; });
				}
				break;

				case Register.REPORT_TICK_INDEX:
				{
					BeginInvoke((MethodInvoker)delegate { lblTickCounter.Text = $"Tick Counter: {args.Value}"; });
				}
				break;

				case Register.REPORT_PUSH_PULL_STATE:
				{
					bool pushed = (PushPullState)args.Value == PushPullState.PUSH;
					BeginInvoke((MethodInvoker)delegate()
					{
						string state = pushed ? "PUSH" : "-";
						lblButtonState.Text = $"Button State: {state}";
					});
				}
				break;

				case Register.REG_TICK_ANGLE_CW:
				{
					BeginInvoke((MethodInvoker)delegate { txtTickAngleCW.Text = $"{args.Value}°"; });
				}
				break;

				case Register.REG_TICK_ANGLE_CCW:
				{
					BeginInvoke((MethodInvoker)delegate { txtTickAngleCCW.Text = $"{args.Value}°"; });
				}
				break;
			}
		}

		private void HandleErrorOccured(ErrorOccurredEventArgs args)
		{
			Debug.WriteLine($"Error occurred: {args.Exception}!");

			// Note: The device error can be handled in different ways - It's not mandatory to disconnect!
			// This sample disconnects to show one way of implementing an automated reconnection routine
			if (device.Connected)
			{
				device.Disconnect();
			}

			reconnectionTimer.Start();
		}

		private void HandleFormClosing(object sender, FormClosingEventArgs e)
		{
			device.Disconnect();
		}
	}
}
