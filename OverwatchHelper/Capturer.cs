using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverwatchHelper
{
    class Capturer
    {

        public string path = "";
        public int delay = 10;
        public int shootDelay = 1000;
        public double window = 500;
        private int headOffset = 5;

        public int counter = 0;

        public bool enabled = false;
        public bool running = false;
        public bool live = true;

        public bool killMode = false;
        public bool saveMode = false;
        public bool predictionMode = false;
        public bool fastMode = false;
        public bool honeMode = false;

        private Analyst analyst;
        public MouseMover mouseMover;
        private Point screenSize;

        public Capturer(Analyst analyst, MouseMover mouseMover, Point screenSize, double window, string path, int delay)
        {
            this.analyst = analyst;
            analyst.headOffset = headOffset;
            this.path = path;
            this.delay = delay;
            this.mouseMover = mouseMover;
            this.screenSize = screenSize;
            this.window = window;
        }

        public void reset()
        {
            this.counter = 0;
        }

        Point center;
        public void run()
        {
            center = new Point(screenSize.X / 2, screenSize.Y / 2);
            running = true;
            while (running)
            {

                System.Threading.Thread.Sleep(delay);
                if (!enabled) continue;
                //act in a new thread
                try
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Restart();
                    if (fastMode)
                    {
                        act(CaptureScreen.GetDesktopImage(2));
                    }
                    else
                        act(CaptureScreen.GetDesktopImage());
                    Console.WriteLine("act took this many ms:\t" + watch.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    
                }
                
            }
        }



        public double travelTime = 1;//travel time of projectile in ms
        public double safetyMargin = 1;//scaling factor for head movement
        private void act(Bitmap view)
        {
            //move recent screenshot to public view;
            if (saveMode)
            {
                view.Save(path + counter + ".bmp");
                counter++;
            }

            if (predictionMode)
            {
                int scale = (fastMode) ? 2 : 1;

                //find head during the first moment:
                Moment first = CaptureScreen.GetDesktopMoment(scale);
                long firstTime = first.timestamp;
                analyst.findSilhouettes(analyst.hsvFilter(new Image<Bgr, Byte>(first.image)));
                Point firstTarget = analyst.findTarget(center);
                if (firstTarget.X < 0 || firstTarget.Y < 0) return;
                if (analyst.distance(firstTarget, center) > window) return;

                //if that succeeded, find head during the second moment:
                Moment second = CaptureScreen.GetDesktopMoment(scale);
                long secondTime = second.timestamp;
                analyst.findSilhouettes(analyst.hsvFilter(new Image<Bgr, Byte>(second.image)));
                Point secondTarget = analyst.findTarget(center);
                if (secondTarget.X < 0 || secondTarget.Y < 0) return;

                //then extrapolate the position of the head during the current time
                long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                double xDelta = (double)(secondTarget.X - firstTarget.X) / (double)(secondTime - firstTime);//x delta / ms
                double yDelta = (double)(secondTarget.Y - firstTarget.Y) / (double)(secondTime - firstTime);//x delata / ms

                xDelta *= safetyMargin;
                yDelta *= safetyMargin;

                double elapsedTime = travelTime + (currentTime - secondTime);
                int newX = (int)((double)secondTarget.X + (xDelta * elapsedTime));
                int newY = (int)((double)secondTarget.Y + (yDelta * elapsedTime));

                mouseMover.newMove(newX, newY, killMode);//move to calculated position

            }
            else
            {

                //builds List<Silhouette> of targets
                analyst.findSilhouettes(analyst.hsvFilter(new Image<Bgr, Byte>(view))); 
                Point target = analyst.findTarget(center);

                if (target.X < 0 || target.Y < 0) return;
                if (analyst.distance(target, center) > window) return;

                //MessageBox.Show("moved to\t" + target.X + "\t" + target.Y);

                if (fastMode)
                {
                    target.X += center.X / 2;
                    target.Y += center.Y / 2;
                }
                if (honeMode)
                {
                    mouseMover.newMove(target.X, target.Y, false);
                    System.Threading.Thread.Sleep(25);
                    Image<Bgr, Byte> subImage = new Image<Bgr, Byte>(CaptureScreen.GetDesktopImage(4));
                    analyst.findSilhouettesWeak(analyst.hsvFilter(subImage));
                    target = analyst.findTarget(center);
                    target.X += center.X * 3 / 4;
                    target.Y += center.Y * 3 / 4;
                    //MessageBox.Show("wants to go to\t" + target.X + "\t" + target.Y);
                    if (target.X < 0 || target.Y < 0)
                        mouseMover.newMove(center.X, center.Y, killMode);//shoot in place if target isn't found again
                    else
                        mouseMover.newMove(target.X, target.Y, killMode);//otherwise hone in
                }
                else
                {
                    mouseMover.newMove(target.X, target.Y, killMode);
                }
                    
            }

        }

    }
}
