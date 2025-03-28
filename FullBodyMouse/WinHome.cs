using Hapticore.Device;
using Hapticore.Types;
using DataReceivedEventArgs = Hapticore.Device.DataReceivedEventArgs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsExampleApp
{
    public partial class WinHome : Form
    {

        private readonly HapticoreDevice device;
        private readonly System.Timers.Timer reconnectionTimer = new System.Timers.Timer();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private const int BAUDRATE = 115200;
        private const char SEPARATOR = ' ';

        private int tickIndex = 0;
        private float wheelAngle = 0;
        private float previousWheelAngle = 0;

        static SerialPort HapticPort;
        private bool isDragAndDrop = false;

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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void playFineTicks(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Tick.SendMode(TickMode.TICK_MODE_1);
            device.Tick.SendAngleCw(7f);
            device.Tick.SendAngleCcw(0f);
            device.Tick.SendDurationMin(0.001f);
            device.Tick.SendDurationMax(0.004f);
            device.Tick.SendCurrent(0.2f);
            device.Tick.SendEnable(true);
        }

        private void playBarrier(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Barrier.SendStartAngle(-90f);
            device.Barrier.SendStopAngle(90f);
            device.Barrier.SendCurrent(1f);
            device.Barrier.SendEnable(true);
            device.Encoder.SendAngle(0f);
        }


        private void playRoughTicks(bool state)
        {
            if (state)
                device.Commands.SendDisableAllHapticFunctions();
            device.Tick.SendMode(TickMode.TICK_MODE_1);
            device.Tick.SendAngleCw(20f);
            device.Tick.SendAngleCcw(0f);
            device.Tick.SendDurationMin(0.01f);
            device.Tick.SendDurationMax(0.03f);
            device.Tick.SendCurrent(0.4f);
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
            device.Tick.SendDurationMax(0.001f);
            device.Tick.SendCurrent(0.5f);
            device.Tick.SendEnable(true);
        }

        private void WinHome_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public WinHome()
        {
            InitializeComponent();
            this.MouseDown += WinHome_MouseDown;
            pictureBox5.MouseMove += pictureBox5_MouseMove;

            string[] ports = SerialPort.GetPortNames();
            if(ports.Length >= 1)
               initialize(ports[ports.Length-1]);

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
                    MessageBox.Show("XeelTech Connected to COM port {comPort} successfully.", "Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //playBarrier(true);
               
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Connecting failed with exception={exc}");
            }
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
            /*BeginInvoke((MethodInvoker)delegate
            {
                connectedlabel.Text = device.Connected ? "Connected: 1" : "Disconnected: 1";
            });*/
        }


        private int  previousIndex = 0;
        private bool isPlaying1 = false;
        private bool isPlaying2 = false;
        private int  idx = -1;

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
                        wheelAngle = (float)args.Value;
                        /*BeginInvoke((MethodInvoker)delegate
                        {
                            if (wheelAngle > previousWheelAngle)
                            {
                                if (idx >= 7 && idx <= 8)
                                {
                                    playBarrier(true);
                                    //MessageBox.Show("UP Wheel Angle : " + args.Value, "Wheel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if(wheelAngle - previousWheelAngle <= 90)
                                {
                                    playMediumTicks(true);
                                }
                            }
                            else
                            {
                                if (idx <= 0 && idx > -1)
                                {
                                    playBarrier(true);
                                    //MessageBox.Show("DOWN Wheel Angle : " + args.Value, "Wheel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (wheelAngle - previousWheelAngle <= 90)
                                {
                                    playMediumTicks(true);
                                }
                            }
                            previousWheelAngle = wheelAngle;
                            //MessageBox.Show("Wheel Angle : " + args.Value, "Wheel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });*/
                        /*if (this.contextMenuStrip1.IsDropDown)
                        {
                            if ((wheelAngle - previousWheelAngle) >= 90)
                            {
                                //this.Focus();
                                SendKeys.SendWait("{UP}");
                                byte Intensity = 0;
                                Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                                byte Effect = (byte)(2);
                                byte[] message = { Effect, 0, Intensity };
                                sendbyte(message);
                            }
                            else if ((wheelAngle - previousWheelAngle) <= 90)
                            {
                                SendKeys.SendWait("{DOWN}");
                                byte Intensity = 0;
                                Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                                byte Effect = (byte)(2);
                                byte[] message = { Effect, 0, Intensity };
                                sendbyte(message);
                            }

                            previousWheelAngle = wheelAngle;
                        }*/
                        /*BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"Angle: {args.Value}°"; });*/
                    }
                    break;

                case Register.REPORT_ENCODER_MULTI_TURN_COUNT:
                    {
                        //tickIndex = (int)args.Value;
                        //MessageBox.Show("Multicount: " + args.Value, "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                            /*tickIndex = (int)args.Value;
                            MessageBox.Show("Multi count " + tickIndex, "Spire.PdfViewer Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            BeginInvoke((MethodInvoker)delegate
                            {
                                if (this.contextMenuStrip1.IsDropDown)
                                {
                                    MessageBox.Show("Multicount " + tickIndex, "Spire.PdfViewer Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if (tickIndex > previousIndex)
                                    {

                                        this.contextMenuStrip1.Items[IdCount % 7].Select();
                                        SendKeys.SendWait("{UP}");
                                        byte Intensity = 0;
                                        Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                                        byte Effect = (byte)(2);
                                        byte[] message = { Effect, 0, Intensity };
                                        sendbyte(message);

                                        IdCount++;
                                    }
                                    else
                                    {
                                        this.contextMenuStrip1.Items[IdCount % 7].Select();
                                        SendKeys.SendWait("{DOWN}");
                                        byte Intensity = 0;
                                        Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                                        byte Effect = (byte)(2);
                                        byte[] message = { Effect, 0, Intensity };
                                        sendbyte(message);

                                        IdCount--;
                                    }

                                    previousIndex = tickIndex;
                                }
                            });*/
                            /*BeginInvoke((MethodInvoker)delegate { /*SerialOut.Text = $"MultiTurn Counter: {args.Value}"; });*/
                        }
                    break;

                case Register.REPORT_TICK_INDEX:
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            tickIndex = Convert.ToInt16(args.Value);
                            idx = tickIndex % 8;
                            if((idx == 0 && tickIndex == 0) || (idx == 0 && tickIndex > 0))
                            {
                                playBarrier(true);
                            }
                            if (this.contextMenuStrip1.IsDropDown) {
                                if (tickIndex > previousIndex)
                                {
                                    if (idx >= 0 && idx <= 7) { 
                                       this.contextMenuStrip1.Items[idx].Select();
                                    }
                                    byte Intensity = 0;
                                    Intensity = 100;
                                    byte Effect = (byte)(2);
                                    byte[] message = { Effect, 0, Intensity };
                                    sendbyte(message);
                                   
                                }
                                else
                                {
                                    if (idx >= 0 && idx <= 7)
                                    {
                                        this.contextMenuStrip1.Items[idx].Select();
                                    }

                                    byte Intensity = 0;
                                    Intensity = 100;
                                    byte Effect = (byte)(2);
                                    byte[] message = { Effect, 0, Intensity };
                                    sendbyte(message);
                                }

                                if (idx < 0 || idx > 7)
                                {
                                    //if (isPlaying1)
                                    //{
                                    //    isPlaying2 = false;
                                    //}
                                }
                                else
                                {
                                    //if (isPlaying2)
                                    //{
                                    //    isPlaying1 = false;
                                    //}

                                    //if (!isPlaying1)
                                    //{
                                    if ((idx == 0 && tickIndex == 0) || (idx == 0 && tickIndex > 0))
                                    {
                                        playMediumTicks(false);
                                    }
                                    else
                                    {
                                        playMediumTicks(true);
                                    }
                                    //    Thread.Sleep(1);
                                    //    isPlaying1 = true;
                                    //}
                                }

                                previousIndex = tickIndex;
                            }
                            /*if (this.contextMenuStrip1.IsDropDown)
                            {
                                //MessageBox.Show("tick count " + tickIndex, "Spire.PdfViewer Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (tickIndex > previousIndex)
                                {

                                    this.contextMenuStrip1.Items[IdCount % 7].Select();
                                    SendKeys.SendWait("{UP}");
                                    byte Intensity = 0;
                                    Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                                    byte Effect = (byte)(2);
                                    byte[] message = { Effect, 0, Intensity };
                                    sendbyte(message);

                                    IdCount++;
                                }
                                else
                                {
                                    this.contextMenuStrip1.Items[IdCount % 7].Select();
                                    SendKeys.SendWait("{DOWN}");
                                    byte Intensity = 0;
                                    Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                                    byte Effect = (byte)(2);
                                    byte[] message = { Effect, 0, Intensity };
                                    sendbyte(message);

                                    IdCount--;
                                }

                                previousIndex = tickIndex;
                            }*/
                        });
                        /*BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"Tick Cnt: {args.Value}"; });*/
                    }
                    break;

                case Register.REPORT_PUSH_PULL_STATE:
                    {
                        /*bool pushed = (PushPullState)args.Value == PushPullState.PUSH;
                        BeginInvoke((MethodInvoker)delegate ()
                        {
                            string state = pushed ? "PUSH" : "-";
                            SerialOut.Text = $"Btn State: {state}";
                        });*/
                    }
                    break;

                case Register.REG_TICK_ANGLE_CW:
                    {
                        //tickIndex = (long)args.Value;
                        /*BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Tick CW count: ", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });*/
                            /*BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"{args.Value}°"; });*/
                    }
                    break;

                case Register.REG_TICK_ANGLE_CCW:
                    {
                        //tickIndex = (long)args.Value;
                        /*BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Tick CCW count: ", "Tick", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });*/
                        /*BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"{args.Value}°"; });*/
                    }
                    break;
            }
        }

        private void HandleErrorOccured(ErrorOccurredEventArgs args)
        {
            Debug.WriteLine($"Error occurred: {args.Exception}!");

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
        public void initialize(string comport)
        {
            HapticPort = new SerialPort(comport, 115200);
            HapticPort.DataBits = 8;
            HapticPort.StopBits = StopBits.One;
            HapticPort.Handshake = Handshake.None;
            HapticPort.Parity = Parity.None;
            HapticPort.Open();
            HapticPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Read the incoming data
                string data = HapticPort.ReadExisting();
                // Console.WriteLine($"Data received: {data}");
                // UpdateTextBox(data);
                // Handle the received serial command
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
            }
        }

        /*public void UpdateTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTextBox), new object[] { value });
                return;
            }
            SerialOut.Text = value;
        }*/


        private void contextMenuStrip1_MouseWheel(object sender, MouseEventArgs e)
        {
            //SendKeys.SendWait(e.Delta > 0 ? "{UP}" : "{DOWN}");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (isDragAndDrop)
            {
                byte Intensity = 0;
                Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                byte Effect = (byte)(3);
                byte[] message = { Effect, 0, Intensity };
                sendbyte(message);
                isDragAndDrop = false;
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            if (isDragAndDrop)
            {
                byte Intensity = 0;
                Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                byte Effect = (byte)(3);
                byte[] message = { Effect, 0, Intensity };
                sendbyte(message);
                isDragAndDrop = false;
            }
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            if (isDragAndDrop)
            {
                byte Intensity = 0;
                Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                byte Effect = (byte)(3);
                byte[] message = { Effect, 0, Intensity };
                sendbyte(message);
                isDragAndDrop = false;
            }
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            //if (isDragAndDrop)
            //{
                byte Intensity = 0;
                Intensity = 100; //(byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
                byte Effect = (byte)(2);
                byte[] message = { Effect, 0, Intensity };
                sendbyte(message);
                //isDragAndDrop = false;
            //}
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(1);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            
            var pb = (PictureBox)sender;

            if ((MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                pb.Location = PointToClient(Cursor.Position);
                pb.BackColor = Color.Transparent;
                isDragAndDrop = true;
            }
        }

        private void WinHome_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(3);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);

            PDFViewer1 openForm = new PDFViewer1();
            openForm.Show();
            this.Hide();
        }

        private void toolStripMenuItem1_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(3);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void toolStripMenuItem2_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void refreshToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void newToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void displayToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void personnalizeToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void openTerminalToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }

        private void showMoreOptionsToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            /*byte Intensity = 0;
            Intensity = (byte)IntensArrayLRA[1, Convert.ToInt16(4 - 1)];
            byte Effect = (byte)(2);
            byte[] message = { Effect, 0, Intensity };
            sendbyte(message);*/
        }
    }
}
