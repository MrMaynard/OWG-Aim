using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchHelper
{
    //a class to represent a bitmap image taken at a particular moment in time
    public class Moment
    {
        public Bitmap image;
        public long timestamp;

        public Moment(Bitmap image, long timestamp)
        {
            this.image = image;
            this.timestamp = timestamp;
        }

    }
}
