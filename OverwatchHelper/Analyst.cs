using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchHelper
{
    class Analyst
    {

        //hsv filter values:
        public int hueMin = 2;
        public int hueMax = 6;
        public int satMin = 165;
        public int satMax = 256;
        public int valMin = 133;
        public int valMax = 256;

        //morphological operation values:
        public int erodeCycles = 0;
        public int erodeCycles2 = 6;
        public int dilateCycles = 13;
        public int mCycles = 1;

        //holder for silhouettes:
        public List<Silhouette> silhouettes = new List<Silhouette>();
        public int minPixels = 10;
        public float minLinearness = .5f;
        public float minGappiness = 1.2f;
        public int headOffset = 0;

        public Image<Gray, Byte> hsvFilter(Image<Bgr, Byte> input){

            Image<Gray, Byte>[] results = new Image<Gray, Byte>[3];
            Image<Gray, Byte>[] channels = input.Convert<Hsv, Byte>().Split();

            results[0] = channels[0].InRange(new Gray(hueMin), new Gray(hueMax));
            results[1] = channels[1].InRange(new Gray(satMin), new Gray(satMax));
            results[2] = channels[2].InRange(new Gray(valMin), new Gray(valMax));

            return results[0].And(results[1]).And(results[2]);
        }

        public Image<Gray, Byte> morphologicalOperations(Image<Gray, Byte> input)
        {

            for (int i = 0; i < mCycles; i++)
            {
                input = input.Erode(erodeCycles);
                input = input.Dilate(dilateCycles);
                input = input.Erode(erodeCycles2);
            }
                
            return input;
        }

        int numTargets = 0;
        public void findSilhouettes(Image<Gray, Byte> input){

            Image<Gray, Byte> fatInput = morphologicalOperations(input);
            List<Silhouette> silhouettes = new List<Silhouette>();
            numTargets = 0;

            var inputSize = input.Size;

            //contours method:
            Image<Gray, Byte> filter = new Image<Gray, byte>(inputSize);
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(fatInput, contours, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);
            for (int i = 0; i < contours.Size; i++) {
                
                double a = CvInvoke.ContourArea(contours[i]);  //  Find the area of contour
                if (a < minPixels) continue;
                
                MCvScalar white = new MCvScalar(255,255,255);
                filter = new Image<Gray, Byte>(inputSize);
                CvInvoke.DrawContours(filter, contours, i, white, -1);
                Silhouette temp = new Silhouette(input.Copy(filter), 0);
                temp.compute();
                if (temp.count < minPixels) continue;
                temp.linearness /= temp.count;
                if (temp.linearness < minLinearness) continue;
                if (temp.gappiness / temp.count < minGappiness) continue;

                var moment = CvInvoke.Moments(contours[i], true);
                temp.centroid.X = (int)(moment.M10 / moment.M00);
                temp.centroid.Y = (int)(moment.M01 / moment.M00);
                temp.centroid = temp.findTop();
                silhouettes.Add(temp);
                numTargets++;
            }

            ////recursive method:
            //for (int i = input.Rows - 1; i >= 0; i--)
            //{
            //    for (int j = input.Cols - 1; j >= 0; j--)
            //    {
            //        if (inputData[i, j, 0] == 255)
            //        {
            //            count = 0;
            //            Image<Gray, Byte> temp = new Image<Gray, Byte>(inputSize);
            //            outputData = temp.Data;

            //            crawlFrom(i, j);
            //            if (count > minPixels)
            //            {
            //                Silhouette s = new Silhouette(temp, count);
            //                if (s.linearness() / (float)s.count > minLinearness)
            //                    silhouettes.Add(s);
            //            }
            //        }
            //    }
            //}

            //filter out low linearness silhouettes:
            //silhouettes = silhouettes.Where(s => s.linearness() / (float)s.count > minLinearness).ToList();

            //move local variable to global:
            this.silhouettes = silhouettes;
        }

        //private void crawlFrom(int i, int j)
        //{
        //    outputData[i, j, 0] = 255;//else add pixel to output
        //    count++;//increment count
        //    inputData[i, j, 0] = 0;//blacken input image
            
        //    //recursively call on neighbors:
        //    if(i + 1 < height)
        //        if (inputData[i + 1, j, 0] == 255)
        //            crawlFrom(i + 1, j);
            
        //    if (i - 1 >= 0)
        //        if (inputData[i - 1, j, 0] == 255) 
        //            crawlFrom(i - 1, j);

        //    if (j + 1 < width)
        //        if (inputData[i, j + 1, 0] == 255) 
        //            crawlFrom(i, j + 1);

        //    if (j - 1 >= 0)
        //        if (inputData[i, j - 1, 0] == 255) 
        //            crawlFrom(i, j - 1);

        //}

        private Point findHead(Silhouette input)
        {
            return new Point(input.top.X, input.top.Y - headOffset);//todo improve this?
        }

        public double distance(Point a, Point b)
        {
            return Math.Sqrt(( a.X - b.X ) * (a.X - b.X ) + (a.Y - b.Y ) * (a.Y - b.Y));
        }

        public Point findTarget(Point center)
        {
            if (numTargets < 1) return new Point(Int32.MinValue, Int32.MinValue);
            List<Point> heads = silhouettes.Select(s => findHead(s)).ToList();//find head in each silhouette
            return heads.Aggregate((c, d) => distance(c, center) < distance(d, center) ? c : d);//find the closest head to the center of the screen
        }

    }
}
