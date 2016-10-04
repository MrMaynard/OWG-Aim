using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchHelper
{
    class Capturer
    {

        public string path = "";
        public int delay = 10;
        public int shootDelay = 110;
        public double window = 500;

        public int counter = 0;

        public bool enabled = false;
        public bool running = false;
        public bool live = true;

        public bool killMode = false;
        public bool saveMode = false;
        public bool predictionMode = false;

        public Bitmap view;

        private Analyst analyst;
        private MouseMover mouseMover;
        private Point screenSize;

        public Capturer(Analyst analyst, MouseMover mouseMover, Point screenSize, double window, string path, int delay)
        {
            this.analyst = analyst;
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



        public void run()
        {
            Point center = new Point(screenSize.X / 2, screenSize.Y / 2);
            running = true;
            while (running)
            {

                //baked-in delay:
                System.Threading.Thread.Sleep(delay);
                if (!live) continue;

                //get recent image
                view = CaptureScreen.GetDesktopImage();

                if (!enabled) continue;

                //move recent screenshot to public view;
                if (saveMode)
                {
                    view.Save(path + counter + ".bmp");
                    counter++;
                }

                if (predictionMode)
                {
                    //todo
                }
                else{
                    //builds List<Silhouette> of targets
                    analyst.findSilhouettes(analyst.hsvFilter(new Image<Bgr, Byte>(view)));
                    Point target = analyst.findTarget(center);
                    if (analyst.distance(target, center) > window) continue;
                    mouseMover.moveMouse(target.X, target.Y);
                }
                
                if (killMode)
                {
                    mouseMover.shoot();
                    System.Threading.Thread.Sleep(shootDelay);
                }

                
            }
        }

    }
}
