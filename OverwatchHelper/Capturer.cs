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
        public int counter = 0;

        public bool enabled = false;
        public bool running = false;

        public bool killMode = false;
        public bool saveMode = false;
        public bool predictionMode = false;

        public Bitmap view;

        private Analyst analyst;
        MouseMover mouseMover;

        public Capturer(Analyst analyst, MouseMover mouseMover)
        {
            this.analyst = analyst;
            this.mouseMover = mouseMover;
        }

        public Capturer(Analyst analyst, MouseMover mouseMover, int delay)
        {
            this.analyst = analyst;
            this.delay = delay;
            this.mouseMover = mouseMover;
        }

        public Capturer(Analyst analyst, MouseMover mouseMover, string path)
        {
            this.analyst = analyst;
            this.path = path;
            this.mouseMover = mouseMover;
        }

        public Capturer(Analyst analyst, MouseMover mouseMover, string path, int delay)
        {
            this.analyst = analyst;
            this.path = path;
            this.delay = delay;
            this.mouseMover = mouseMover;
        }

        public void reset()
        {
            this.counter = 0;
        }

        public void run()
        {
            running = true;
            while (running)
            {

                //baked-in delay:
                System.Threading.Thread.Sleep(delay);
                if (!enabled) continue;

                //get recent image
                view = CaptureScreen.GetDesktopImage();

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

                //todo acquire target.

                if (killMode)
                {
                    mouseMover.shoot();
                }

                analyst.findSilhouettes(
                    analyst.morphologicalOperations(
                    analyst.hsvFilter(new Image<Bgr, Byte>(view))));

            }
        }

    }
}
