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

        public Bitmap view;

        private Analyst analyst;

        public Capturer(Analyst analyst)
        {
            this.analyst = analyst;
        }

        public Capturer(Analyst analyst, int delay)
        {
            this.analyst = analyst;
            this.delay = delay;
        }

        public Capturer(Analyst analyst, string path)
        {
            this.analyst = analyst;
            this.path = path;
        }

        public Capturer(Analyst analyst, string path, int delay)
        {
            this.analyst = analyst;
            this.path = path;
            this.delay = delay;
        }

        public void reset()
        {
            this.counter = 0;
        }

        public void run(bool saveMode)
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

                analyst.findSilhouettes(
                    analyst.morphologicalOperations(
                    analyst.hsvFilter(new Image<Bgr, Byte>(view))));

            }
        }

    }
}
