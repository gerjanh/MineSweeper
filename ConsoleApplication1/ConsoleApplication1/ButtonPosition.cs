using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class ButtonPosition
    {
        private int x;
        private int y;
        private int point;

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

        public int getY() { return y; }
        public int getX() { return x; }
        public int getPint() { return point; }
    }
}
