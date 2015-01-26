using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class ButtonPosition
    {
        public int x { get; set; }
        public int y { get; set; }
        public int point { get; set; }
        public ButtonPosition(int x , int y)
        {
            this.x = x;
            this.y = y;
            this.point = -2;
        }

        public ButtonPosition(int x, int y, int point)
        {
            this.x = x;
            this.y = y;
            this.point = point;
        }

    }
}
