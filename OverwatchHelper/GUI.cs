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

        private Capturer capturer;
        private Analyst analyst;
        private MouseMover mover;

        Thread captureThread;

        string path = "C:\\Users\\Eric\\Desktop\\overwatch\\images\\";

        public GUI()
        {
            InitializeComponent();
            initialize();
            start();
        }

        Stopwatch stopwatch;
        private void initialize()
        {
            stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            analyst = new Analyst(stopwatch);
            capturer = new Capturer(analyst, path, 1000);
            mover = new MouseMover(new Point(1920, 1080));

            captureThread = new Thread(() => { capturer.run(true); }, 1000000000);
            
        }

        private void start()
        {
            captureThread.Start();
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

            stopwatch.Restart();
            
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

            stopwatch.Stop();
            Console.WriteLine("total:\t" + stopwatch.ElapsedMilliseconds);
            
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
    }
}
