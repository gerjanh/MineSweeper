using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class MineSweeperField
    {
        private static int[,] field;
        private int hight, width;

        public MineSweeperField()
        {
            newField();
        }

        public void newField()
        {
            newField(8, 8, 10);
        }
        public void newField(int x, int y, int numOfBombs)
        {
            field = new int[x, y];
            this.width = x;
            this.hight = y;

            Random rnd = new Random();
            int bombs = numOfBombs;
            while (bombs > 0)
            {
                int randomx = rnd.Next(x);
                int randomy = rnd.Next(y);
                if (field[randomx, randomy] != -1)
                {
                    field[randomx, randomy] = -1;
                    bombs--;

                    if (randomx - 1 >= 0)
                    {
                        if (field[randomx - 1, randomy] != -1)
                        {
                            field[randomx - 1, randomy]++;
                        }
                        if (randomy - 1 >= 0)
                        {
                            if (field[randomx - 1, randomy - 1] != -1)
                            {
                                field[randomx - 1, randomy - 1]++;
                            }
                        }
                        if (randomy + 1 < y)
                        {
                            if (field[randomx - 1, randomy + 1] != -1)
                            {
                                field[randomx - 1, randomy + 1]++;
                            }
                        }
                    }

                    if (randomx + 1 < x)
                    {
                        if (field[randomx + 1, randomy] != -1)
                        {
                            field[randomx + 1, randomy]++;
                        }
                        if (randomy - 1 >= 0)
                        {
                            if (field[randomx + 1, randomy - 1] != -1)
                            {
                                field[randomx + 1, randomy - 1]++;
                            }
                        }
                        if (randomy + 1 < y)
                        {
                            if (field[randomx + 1, randomy + 1] != -1)
                            {
                                field[randomx + 1, randomy + 1]++;
                            }
                        }
                    }

                    if (randomy - 1 >= 0)
                    {
                        if (field[randomx, randomy - 1] != -1)
                        {
                            field[randomx, randomy - 1]++;
                        }
                    }
                    if (randomy + 1 < y)
                    {
                        if (field[randomx, randomy + 1] != -1)
                        {
                            field[randomx, randomy + 1]++;
                        }
                    }
                }
            }




        }
        public int[,] getfield()
        {
            return field;
        }

        public List<ButtonPosition> getSpot(int x, int y)
        {
            List<ButtonPosition> area = new List<ButtonPosition>();
            serounding(x, y, area);
            return area;
        }

        private List<ButtonPosition> serounding(int x, int y, List<ButtonPosition> number)
        {
            bool temp = false;
            foreach (ButtonPosition b in number)
            {
                if (b.x == x && b.y == y)
                {
                    temp = true;
                }
            }
            if (!(temp))
            {

                number.Add(new ButtonPosition(x, y, field[x, y]));

                if (field[x, y] == 0)
                {
                    if (!(x - 1 < 0))
                    {
                        serounding(x - 1, y, number);
                    }

                    if (!(x + 1 >= width))
                    {
                        serounding(x + 1, y, number);
                    }

                    if (!(y - 1 < 0))
                    {
                        serounding(x, y - 1, number);
                    }

                    if (!(y + 1 >= hight))
                    {
                        serounding(x, y + 1, number);
                    }
                }
            }
            return number;
        }
    }
}
