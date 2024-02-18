using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gk3
{
    public class MyColor
    {
        public int red;
        public int green;
        public int blue;
        public int level = 0;
        public MyColor(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        public MyColor(int red, int green, int blue, int level)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.level = level;
        }
        public MyColor normalized(int colorCount,int level)
        {
            if (colorCount == 0) colorCount = 1;
            return new MyColor(red/colorCount,green/colorCount,blue/colorCount,level);
        }
        public void add(MyColor color)
        {
            this.red+=color.red;  
            this.green+=color.green;
            this.blue+=color.blue;
        }
        public bool Equals(MyColor other)
        {
            if (other == null)
                return false;

            return this.red == other.red && this.green == other.green && this.blue == other.blue;
        }

        public override int GetHashCode()
        {
            return red.GetHashCode() ^ green.GetHashCode() ^ blue.GetHashCode();
        }
    }
}
