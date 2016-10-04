using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace OverwatchHelper
{
    public partial class GUI : Form
    {

        private Dictionary<string, Keys> keyTable = new Dictionary<string, Keys>();
        private void setupTable()
        {
            //standard letters:
            keyTable["A"] = Keys.A;
            keyTable["B"] = Keys.B;
            keyTable["C"] = Keys.C;
            keyTable["D"] = Keys.D;
            keyTable["E"] = Keys.F;
            keyTable["G"] = Keys.H;
            keyTable["I"] = Keys.J;
            keyTable["K"] = Keys.K;
            keyTable["L"] = Keys.L;
            keyTable["M"] = Keys.M;
            keyTable["N"] = Keys.N;
            keyTable["O"] = Keys.O;
            keyTable["P"] = Keys.P;
            keyTable["Q"] = Keys.Q;
            keyTable["R"] = Keys.R;
            keyTable["S"] = Keys.S;
            keyTable["T"] = Keys.T;
            keyTable["U"] = Keys.U;
            keyTable["V"] = Keys.V;
            keyTable["W"] = Keys.W;
            keyTable["X"] = Keys.X;
            keyTable["Y"] = Keys.Y;
            keyTable["Z"] = Keys.Z;

            //numbers:
            keyTable["1"] = Keys.D1;
            keyTable["2"] = Keys.D2;
            keyTable["3"] = Keys.D3;
            keyTable["4"] = Keys.D4;
            keyTable["5"] = Keys.D5;
            keyTable["6"] = Keys.D6;
            keyTable["7"] = Keys.D7;
            keyTable["8"] = Keys.D8;
            keyTable["9"] = Keys.D9;
            keyTable["0"] = Keys.D0;

            //symbols:
            keyTable[";"] = Keys.OemSemicolon;
            keyTable["\\"] = Keys.OemBackslash;
            keyTable[" "] = Keys.Space;
            keyTable["\""] = Keys.OemQuotes;
            keyTable["."] = Keys.OemPeriod;
            keyTable["|"] = Keys.OemPipe;
            keyTable["-"] = Keys.OemMinus;
            keyTable[","] = Keys.Oemcomma;
            keyTable["?"] = Keys.OemQuestion;
            keyTable["+"] = Keys.Oemplus;
            keyTable["["] = Keys.OemOpenBrackets;
            keyTable["]"] = Keys.OemCloseBrackets;

            //mouse buttons:
            keyTable["MB1"] = Keys.LButton;
            keyTable["MB2"] = Keys.RButton;
            keyTable["MB3"] = Keys.MButton;

        }

        private Capturer capturer;
        private Analyst analyst;
        private MouseMover mover;
        private Hooker hooker;

        Thread captureThread;
        Thread hookerThread;

        string path = "C:\\Users\\Eric\\Desktop\\overwatch\\images\\";

        public GUI()
        {
            InitializeComponent();
            initialize();
            setupTable();
            start();
        }

        private void initialize()
        {
            analyst = new Analyst();
            mover = new MouseMover(new Point(1920, 1080), 8.0f);
            capturer = new Capturer(analyst, mover, path, 1000);
            hooker = new Hooker(capturer, Keys.C);

            captureThread = new Thread(() => { capturer.run(); }, 1000000000);
            hookerThread = new Thread(() => { hooker.run(); });
            
            aimBox.DropDownStyle = ComboBoxStyle.DropDownList;
            aimBox.SelectedText = "2";
        }

        private void start()
        {
            captureThread.Start();
            hookerThread.Start();
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if (capturer.enabled)
            {
                capturer.enabled = false;
                captureButton.Text = "Capture";
            }
            else
            {
                capturer.reset();
                capturer.enabled = true;
                captureButton.Text = "Stop capturing";
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            
            //interesting files: 31 57 58 60 62
            string debugpath = "C:\\Users\\Eric\\Desktop\\overwatch\\images\\testrun0\\58.bmp";
            //var debug =
                analyst.findSilhouettes(
                    analyst.hsvFilter(
                    new Image<Bgr, Byte>(CaptureScreen.GetDesktopImage())
                    )
                    )
                    ;
            //CvInvoke.Imshow("debug",debug);
            //CvInvoke.WaitKey(0);
            //return;
            //analyst.silhouettes.ForEach(s => {
            //    Image<Bgr, Byte> result = s.image.Convert<Bgr, Byte>();
            //    var marker = new Bgr(0, 0, 255);
            //    s.centroid.Y += 4;
            //    //demo to show found top:
            //    if (s.centroid.X != -1)
            //    {
            //        result[s.centroid.Y, s.centroid.X] = marker;
            //        result[s.centroid.Y + 1, s.centroid.X] = marker;
            //        result[s.centroid.Y - 1, s.centroid.X] = marker;
            //        result[s.centroid.Y, s.centroid.X + 1] = marker;
            //        result[s.centroid.Y, s.centroid.X - 1] = marker;

            //    }
            //        CvInvoke.Imshow("Stats (C, L, G):\t" + s.count + ",\t" + s.linearness / s.count + ",\t" + s.gappiness / s.count, result);
            //        CvInvoke.WaitKey(0);
                
            //});
            
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void GUI_Closing(object sender, EventArgs e)
        {
            capturer.enabled = false;
            captureThread.Abort();
            Application.Exit();
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(xBox.Text);
            int y = Int32.Parse(yBox.Text);
            mover.moveMouse(x, y);
        }

        private void sensButton_Click(object sender, EventArgs e)
        {
            mover.update(Single.Parse(sensBox.Text));
        }

        private void predictionBox_CheckedChanged(object sender, EventArgs e)
        {
            capturer.predictionMode = predictionBox.Checked;
        }

        private void shootBox_CheckedChanged(object sender, EventArgs e)
        {
            capturer.killMode = shootBox.Checked;
        }

        private void aimBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hooker.aimKey = keyTable[(string)aimBox.SelectedItem];
        }
    }
}
