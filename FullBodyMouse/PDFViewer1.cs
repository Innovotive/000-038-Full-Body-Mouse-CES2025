using Hapticore.Device;
using Hapticore.Types;
using DataReceivedEventArgs = Hapticore.Device.DataReceivedEventArgs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace WinFormsExampleApp
{
    public partial class PDFViewer1 : Form
    {
        private readonly HapticoreDevice device;
        private readonly System.Timers.Timer reconnectionTimer = new System.Timers.Timer();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void PDFViewer1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public PDFViewer1()
        {
            InitializeComponent();
            this.pdfDocumentViewer1.MouseDown += PDFViewer1_MouseDown;
            pictureBox1.MouseDown += PDFViewer1_MouseDown;

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
                    /*BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"Angle: {args.Value}°"; });*/
                }
                break;

                case Register.REPORT_ENCODER_MULTI_TURN_COUNT:
                {
                    /*BeginInvoke((MethodInvoker)delegate { /*SerialOut.Text = $"MultiTurn Counter: {args.Value}"; });*/
                }
                break;

                case Register.REPORT_TICK_INDEX:
                {
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
                    /*BeginInvoke((MethodInvoker)delegate { SerialOut.Text = $"{args.Value}°"; });*/
                }
                break;

                case Register.REG_TICK_ANGLE_CCW:
                {
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

        /*private void playContrastingTicks(bool state)
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
        }*/

        private void PDFViewer_Load(object sender, EventArgs e)
        {
            string pdfDoc = @".../.../images/doc.pdf";
            if (File.Exists(pdfDoc))
            {
                this.pdfDocumentViewer1.LoadFromFile(pdfDoc);
            }

            this.pdfDocumentViewer1.MouseWheel += new MouseEventHandler(this.pdfDocumentViewer1_MouseWheel);
        }

        //float currSpeed = 0;
        int previousPage = -1;
        bool hasPlayed = true;
        private void pdfDocumentViewer1_MouseWheel(Object sender, MouseEventArgs args)
        {
            
            //float speed = args.Delta / 50;

            // adjust effects based on the speed or delta
            if (Math.Abs(args.Delta) > 150)
            {
                //MessageBox.Show(">150", "Spire.PdfViewer Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                playRoughTicks(true);
            }
            else if (Math.Abs(args.Delta) > 50 && Math.Abs(args.Delta) < 150)
            {
                playRoughTicks(true);
            }
            else if (Math.Abs(args.Delta) < 50)
            {
                playRoughTicks(true);
            }

            // detects if page changed
            if (pdfDocumentViewer1.CurrentPageNumber != previousPage && previousPage != -1)
            {
                playRoughTicks(true);
                hasPlayed = true;
            }

            // detects if reached to bottom or top
            if ((pdfDocumentViewer1.CurrentPageNumber == pdfDocumentViewer1.PageCount
               || pdfDocumentViewer1.CurrentPageNumber == 1) && hasPlayed)
            {
                playBarrier(false);
                hasPlayed = false;
            }

            previousPage = pdfDocumentViewer1.CurrentPageNumber;

            /*else
            {
                currSpeed += speed;
                this.pdfDocumentViewer1.ScrollTo(0, -currSpeed);
            }
            if (this._isZoomDynamic)
            {
                int wheelValue = (Int32)args.Delta / 24;
                this._zoom += wheelValue;

                if (this._zoom < 0)
                    this._zoom = 0;
                this.pdfDocumentViewer1.ZoomTo(this._zoom);
            }*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            WinHome openForm = new WinHome();
            openForm.Show();
            this.Hide();
        }
    }
}
