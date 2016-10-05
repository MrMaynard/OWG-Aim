﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
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
        public int captureDelay = 30;
        public int actDelay = 25;
        public int shootDelay = 1000;
        public double window = 500;
        private int headOffset = 4;

        public int counter = 0;

        public bool enabled = false;
        public bool running = false;
        public bool live = true;

        public bool killMode = false;
        public bool saveMode = false;
        public bool predictionMode = false;

        private Analyst analyst;
        public MouseMover mouseMover;
        private Point screenSize;

        public Capturer(Analyst analyst, MouseMover mouseMover, Point screenSize, double window, string path, int delay)
        {
            this.analyst = analyst;
            analyst.headOffset = headOffset;
            this.path = path;
            this.captureDelay = delay - 3;
            this.actDelay = delay;
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
            view = CaptureScreen.GetDesktopImage();
            Thread captureThread = new Thread(() => capture());
            running = true;
            while (running)
            {

                System.Threading.Thread.Sleep(actDelay);
                if (!enabled) continue;

                //act in a new thread
                try
                {
                    act(view);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:\t" + ex.Message);
                    MessageBox.Show("FROM:\t" + ex.Source);
                    MessageBox.Show("TRACE:\t" + ex.StackTrace);
                    System.Environment.Exit(0);
                }
                System.Threading.Thread.Sleep(1000);
  
                
            }
            captureThread.Abort();
        }

        private volatile bool viewLock = false;
        volatile Bitmap view;
        private void capture() { 
        while (running)
            {

                //baked-in delay:
                System.Threading.Thread.Sleep(captureDelay);
                if (!live) continue;

                while (viewLock)
                    System.Threading.Thread.Sleep(1);
                
                viewLock = true;
                view = CaptureScreen.GetDesktopImage();
                viewLock = false;
                
            }
        }


        int debugCounter = 0;
        private void act(Bitmap view)
        {
            Console.WriteLine("acting now");
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
            else
            {
                while (viewLock)
                    System.Threading.Thread.Sleep(1);
                viewLock = true;
                Image<Bgr, Byte> img = new Image<Bgr, Byte>(view);
                viewLock = false;
                Console.WriteLine("obtained view");
                //builds List<Silhouette> of targets
                analyst.findSilhouettes(analyst.hsvFilter(img)); 
                Console.WriteLine("found silhouettes");
                try
                {
                    Point target = analyst.findTarget(center);
                    if (target.X < 0 || target.Y < 0)
                    {
                        Console.WriteLine("no suitable target");
                        img.Save("C:\\Users\\Eric\\Desktop\\overwatch\\" + debugCounter + ".bmp");
                        debugCounter++;
                        return;
                    }
                    if (analyst.distance(target, center) > window) return;
                    //debug//Console.WriteLine("moving to " + target.X + "," + target.Y);
                    mouseMover.newMove(target.X, target.Y);
                    MessageBox.Show("moved by\t" + target.X + "\t" + target.Y);
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            if (killMode)
            {
                mouseMover.shoot();
                System.Threading.Thread.Sleep(shootDelay);
            }
        }

    }
}
