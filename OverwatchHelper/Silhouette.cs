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
    class Silhouette
    {

        //data:
        public Image<Gray, Byte> image;
        public byte[, ,] data;

        //metadata:
        public int count = 1;
        public float linearness = 0f;
        public float gappiness = 0f;

        //bounds:
        public Point top = new Point(Int32.MaxValue, Int32.MaxValue);

        public Point centroid = new Point(0, 0);

        //function to compute the linearness of this silhouette:
        public void compute()
        {
            var data = this.image.Data;
            linearness = 0f;
            gappiness = 0f;
            count = 0;
            int width = image.Width;
            int height = image.Height;
            
            bool hitred = false;

            for (int j = image.Cols - 1; j >= 0; j--)
            {
                hitred = false;
                short gapsize = 1;
                short holder = 0;
                short tempCount = 0;
                short temp = 0;
                for (int i = image.Rows - 1; i >= 0; i--)
                {
                    if (data[i, j, 0] == 255)
                    {
                        gapsize = 1;
                        tempCount++;//count white pixels

                        //linearness goes up for each black/white border:
                        if (i + 1 < height) if (data[i + 1, j, 0] == 0) linearness++;
                        if (i - 1 >= 0)     if (data[i - 1, j, 0] == 0) linearness++;
                        if (j + 1 < width)  if (data[i, j + 1, 0] == 0) linearness++;
                        if (j - 1 >= 0)     if (data[i, j - 1, 0] == 0) linearness++;

                        hitred = true;//flag to know we are in a gap on black pixels
                        holder += temp;
                        temp = 0;

                        if (i < top.Y)
                        {
                            top = new Point(j, i);
                        }

                    }
                    else if (hitred) temp += gapsize++;//use black pixels to compute gappiness
                }
                if (tempCount == 0) continue;
                count += tempCount;
                gappiness += holder / tempCount;
            }
            linearness /= count;
            gappiness /= count;
        }

        //centroid method
        public Point findTop(bool c)
        {
            var data = this.image.Data;
            for (int j = centroid.X; j >= 0; j--)
            {
                for (int i = centroid.Y; i >= 0; i--)
                {
                    if (data[i, j, 0] != 0)//if this is a white pixel set it to centroid
                    {
                        //non-normalized:
                        //return new Point(j, i);
                        //normalized logic below:

                        for(int temp = 0; temp < 20; temp++){
                            if(data[i, j, 0] != 0)
                                i++;//go back down so you hit a black again
                            else 
                                break;
                        }

                        int min = j;//farthest left point
                        int goodDown = 0;
                        int down;
                        for (down = 0; down < 2 && i + down > image.Rows; down++)//go as low as you can
                        {
                            while (data[i + down, j, 0] == 0){//go all the way to the left in the black
                                if (data[i, j, 0] == 0)
                                    if (j < min)
                                    {
                                        goodDown = down;
                                        min = j;
                                    }
                                j--;
                                if(j < 0) break;
                            }
                        }
                        return new Point(min, i + goodDown);
                    }
                }
            }
            Console.WriteLine("head logic completely failed!");
            return new Point(Int32.MinValue, Int32.MinValue);
        }

        //top method
        public Point findTop()
        {
            return top;
        }

        public Silhouette(Image<Gray, Byte> image, int count)
        {
            this.image = image;
            this.count = count;
            this.data = this.image.Data;
        }

    }
}
