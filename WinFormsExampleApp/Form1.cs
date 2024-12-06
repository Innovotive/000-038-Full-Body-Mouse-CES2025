using System.IO.Ports;
using System.Diagnostics;
using Hapticore.Device;
using Hapticore.Types;
using DataReceivedEventArgs = Hapticore.Device.DataReceivedEventArgs;
using System.Windows.Forms;
using System;
using System.Threading;
using System.Drawing;

namespace HapticoreExampleApp
{
    public partial class Form1 : Form
    {
        private const int BAUDRATE = 115200;
        private const char SEPARATOR = ' ';

        static SerialPort HapticPort;
        private readonly HapticoreDevice device;
        private readonly System.Timers.Timer reconnectionTimer = new System.Timers.Timer();

        int[,] IntensArrayTACH = new int[3, 4]
        {
            { 35, 42, 60, 90 },{ 30, 35, 47, 60 },{ 35, 43, 58, 70 }
            };
        int[,] IntensArrayLRA = new int[3, 4]
        {
            { 15, 20, 30, 40 },{ 20, 30, 45, 60 },{ 35, 50, 70, 100 }
            };
        int[,] IntensArrayIHD = new int[3, 4]
        {
            { 20, 37, 55, 65 },{ 35, 48, 65, 80 },{ 35, 48, 65, 80 }
            };

        public Form1()
        {

            InitializeComponent();
            device = new HapticoreDevice();

            device.ConnectionChanged += HandleConnectionChanged;
            device.DataReceived += HandleDataReceived;
            device.ErrorOccurred += HandleErrorOccured;

            reconnectionTimer.Interval = 2000;
            reconnectionTimer.Elapsed += (sender, e) => XeelTechConnect();
            reconnectionTimer.Start();

        }

        public void XeelTechConnect()
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
                    Debug.WriteLine($"XeelTech Connected to COM port {comPort} successfully.");
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

        private void playFineTicks(bool state)
        {
            if(state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Tick.SendMode(TickMode.TICK_MODE_1);
            device.Tick.SendAngleCw(7f);
            device.Tick.SendAngleCcw(0f);
            device.Tick.SendDurationMin(0.001f);
            device.Tick.SendDurationMax(0.004f);
            device.Tick.SendCurrent(0.2f);
            device.Tick.SendEnable(true);
        }

        private void playMediumTicks(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Tick.SendMode(TickMode.TICK_MODE_1);
            device.Tick.SendAngleCw(14f);
            device.Tick.SendAngleCcw(0f);
            device.Tick.SendDurationMin(0.001f);
            device.Tick.SendDurationMax(0.004f);
            device.Tick.SendCurrent(0.35f);
            device.Tick.SendEnable(true);
        }

        private void playRoughTicks(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Tick.SendMode(TickMode.TICK_MODE_1);
            device.Tick.SendAngleCw(20f);
            device.Tick.SendAngleCcw(0f);
            device.Tick.SendDurationMin(0.01f);
            device.Tick.SendDurationMax(0.05f);
            device.Tick.SendCurrent(0.4f);
            device.Tick.SendEnable(true);
        }

        private void playBarrier(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Barrier.SendStartAngle(-90f);
            device.Barrier.SendStopAngle(90f);
            device.Barrier.SendCurrent(0.5f);
            device.Barrier.SendEnable(true);
            device.Encoder.SendAngle(0f);
        }

        private void playContrastingTicks(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Tick.SendMode(TickMode.TICK_MODE_1);
            device.Tick.SendAngleCw(20f);
            device.Tick.SendAngleCcw(5f);
            device.Tick.SendDurationMin(0.002f);
            device.Tick.SendDurationMax(0.01f);
            device.Tick.SendCurrent(0.25f);
            device.Tick.SendEnable(true);
        }

        private void playConstantCurrent(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Current.SendStartCurrentCw(0.06f);
            device.Current.SendStopCurrentCw(0.06f);
            device.Current.SendStartCurrentCcw(0.06f);
            device.Current.SendStopCurrentCcw(0.06f);
            device.Current.SendEnable(true);
        }

        private void playTicksWithBarrier(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
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
        }

        private void playClockwise(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Lock.SendDirection(ActiveDirection.CCW);
            device.Lock.SendEnable(true);
        }

        private void playNone(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
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
                connectedlabel.Text = device.Connected ? "Connected: 1" : "Disconnected: 1";
            });
        }

        private void HandleDataReceived(DataReceivedEventArgs args)
        {
            if ((ReplyStatus)args.Status != ReplyStatus.OK)
            {
                // TODO: Handle reply status
                return;
            }

            switch (args.Register)
            {
                case Register.REPORT_ENCODER_ANGLE:
                    {
                        BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"Angle: {args.Value}°"; });
                    }
                    break;

                case Register.REPORT_ENCODER_MULTI_TURN_COUNT:
                    {
                        BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"MultiTurn Counter: {args.Value}"; });
                    }
                    break;

                case Register.REPORT_TICK_INDEX:
                    {
                        BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"Tick Cnt: {args.Value}"; });
                    }
                    break;

                case Register.REPORT_PUSH_PULL_STATE:
                    {
                        bool pushed = (PushPullState)args.Value == PushPullState.PUSH;
                        BeginInvoke((MethodInvoker)delegate ()
                        {
                            string state = pushed ? "PUSH" : "-";
                            SerialOut.Text = $"Btn State: {state}";
                        });
                    }
                    break;

                case Register.REG_TICK_ANGLE_CW:
                    {
                        BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"{args.Value}°"; });
                    }
                    break;

                case Register.REG_TICK_ANGLE_CCW:
                    {
                        BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"{args.Value}°"; });
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


        public void sendbyte(byte[] msg)
        {

            if (null == HapticPort)
                return;

            try
            {
                if (HapticPort.IsOpen)
                {
                    if (msg != null)
                    {

                        HapticPort.Write(msg, 0, msg.Length);
                        for (int i = 0; i < msg.Length; i++)
                        {
                            Debug.Write(msg[i].ToString());
                            Debug.Write(" ");
                        }
                        Debug.Write("\r\n");

                    }
                }
            }
            catch (Exception ex)
            {
                HapticPort.Close();
                HapticPort.Open();
            }
            Thread.Sleep(1);
        }
        public void initialize(int portIndex)
        {
            string comport = "COM" + portIndex.ToString();
            HapticPort = new SerialPort(comport, 115200);
            HapticPort.DataBits = 8;
            HapticPort.StopBits = StopBits.One;
            HapticPort.Handshake = Handshake.None;
            HapticPort.Parity = Parity.None;
            HapticPort.Open();
            connectedlabel.Text = "Connected: 2";
            connectedlabel.ForeColor = Color.Green;
            HapticPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Read the incoming data
                string data = HapticPort.ReadExisting();
                //  Console.WriteLine($"Data received: {data}");
                UpdateTextBox(data);
                // Handle the received serial command

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
            }
        }

        public void UpdateTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTextBox), new object[] { value });
                return;
            }
            SerialOut.Text = value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            initialize(Convert.ToInt32(ComportBox.Value));

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            HapticPort.Close();
            connectedlabel.Text = "Disconnected: 2";
            connectedlabel.ForeColor = Color.Red;
        }

        private void ShowPortButton_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            string message = "These ports were found: \n";

            // Display each port name to the console.
            foreach (string port in ports)
            {
                message = message + port + " \n";
            }

            MessageBox.Show(message);
        }






        private void Zone1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Zone1_Enter(object sender, EventArgs e)
        {

        }

        private void Zone1_Leave(object sender, EventArgs e)
        {

        }
        private void Zone2_Enter(object sender, EventArgs e)
        {

        }

        private void Zone2_Leave(object sender, EventArgs e)
        {

        }

        private void Zone3_Enter(object sender, EventArgs e)
        {

        }

        private void Zone3_Leave(object sender, EventArgs e)
        {

        }

        private void Zone1_MouseEnter(object sender, EventArgs e)
        {
            playFineTicks(true);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[0, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[0, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[0, Convert.ToInt16(intensitynumber.Value - 1)];

            byte Effect = (byte)(1);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void Zone1_MouseLeave(object sender, EventArgs e)
        {
            playFineTicks(true);
            playBarrier(false);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[0, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[0, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[0, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(1);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void Zone2_MouseEnter(object sender, EventArgs e)
        {
            playMediumTicks(true);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[1, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[1, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void Zone2_MouseLeave(object sender, EventArgs e)
        {
            playMediumTicks(true);
            playBarrier(false);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[1, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[1, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void Zone3_MouseEnter(object sender, EventArgs e)
        {
            playRoughTicks(true);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[2, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[2, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[2, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(3);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void Zone3_MouseLeave(object sender, EventArgs e)
        {
            playRoughTicks(true);
            playBarrier(false);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[2, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[2, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[2, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(3);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void ZoneFS_MouseEnter(object sender, EventArgs e)
        {
            playContrastingTicks(true);

            byte Effect = 0;
            byte Intensity = (byte)(30 * Convert.ToInt16(intensitynumber.Value));
            //if (MouseSelection.SelectedIndex == 0)
            //    Effect = 10;
            //else if (MouseSelection.SelectedIndex == 1)
            Effect = 12;
            //else if (MouseSelection.SelectedIndex == 2)
            //    Effect = 12;

            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void ZoneFS_MouseLeave(object sender, EventArgs e)
        {
            playContrastingTicks(true);
            playBarrier(false);

            byte Effect = 0;
            byte Intensity = (byte)(30 * Convert.ToInt16(intensitynumber.Value));
            //if (MouseSelection.SelectedIndex == 0)
            //    Effect = 10;
            //else if (MouseSelection.SelectedIndex == 1)
            Effect = 12;
            //else if (MouseSelection.SelectedIndex == 2)
            //    Effect = 12;
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void tempbutton_Click(object sender, EventArgs e)
        {

        }

        private void tempbutton_MouseDown(object sender, MouseEventArgs e)
        {
            playFineTicks(true);
            playClockwise(false);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[0, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[0, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[0, Convert.ToInt16(intensitynumber.Value - 1)];

            byte Effect = (byte)(1);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void Mediumbutton_MouseDown(object sender, MouseEventArgs e)
        {
            playMediumTicks(true);
            playClockwise(false);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[1, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[1, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void HardButton_MouseDown(object sender, MouseEventArgs e)
        {
            playRoughTicks(true);
            playClockwise(false);

            byte Intensity = 0;
            //if (MouseSelection.SelectedIndex == 0)
            //    Intensity = (byte)IntensArrayTACH[2, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 1)
            Intensity = (byte)IntensArrayLRA[2, Convert.ToInt16(intensitynumber.Value - 1)];
            //else if (MouseSelection.SelectedIndex == 2)
            //    Intensity = (byte)IntensArrayIHD[2, Convert.ToInt16(intensitynumber.Value - 1)];
            byte Effect = (byte)(3);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

    }
}
