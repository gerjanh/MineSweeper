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
                Random rnd = new Random();
                int bombs = 0;

                while (bombs < numOfBombs)
                {
                    bombs = 0;
                    field = new int[x, y];

                    for (int i = 0; i < numOfBombs; i++)
                    {
                        field[rnd.Next(x), rnd.Next(y)] = -1;
                    }

                    for (int idx = 0; idx < x; idx++)
                    {
                        for (int idy = 0; idy < x; idy++)
                        {
                            if (field[idx, idy] == -1)
                                bombs++;
                        }
                    }
                }

                for (int idx = 0 ; idx < x ; idx++){
                    for (int idy = 0 ; idy < x ; idy++){
                        int numOfBombsAround = 0;

                        if (!(field[idx,idy]==-1)){
                            if (!(idy + 1 > y))
                            {
                                if (!(idx - 1 < 0))
                                {
                                    if (field[idx - 1, idy + 1] == -1)
                                    {
                                        numOfBombsAround++;
                                    }
                                }
                                if (field[idx, idy + 1] == -1)
                                {
                                    numOfBombsAround++;
                                }
                                if (!(idx + 1 > x))
                                {
                                    if (field[idx + 1, idy + 1] == -1)
                                    {
                                        numOfBombsAround++;
                                    }
                                }
                            }

                            if (!(idx - 1 < 0))
                            {
                                if (field[idx - 1, idy] == -1)
                                {
                                    numOfBombsAround++;
                                }
                            }
                            if (!(idx + 1 > x))
                            {
                                if (field[idx + 1, idy] == -1)
                                {
                                    numOfBombsAround++;
                                }
                            }

                            if (!(idy -1 < 0))
                            {
                                if (!(idx - 1 < 0))
                                {
                                    if (field[idx - 1, idy - 1] == -1)
                                    {
                                        numOfBombsAround++;
                                    }
                                }
                                if (field[idx, idy - 1] == -1)
                                {
                                    numOfBombsAround++;
                                }
                                if (!(idx + 1 > x))
                                {
                                    if (field[idx + 1, idy - 1] == -1)
                                    {
                                        numOfBombsAround++;
                                    }
                                }
                            }
                            field[idx,idy]=numOfBombsAround;
                        }
                    }
                }

            }

            public  List<int> getSpot(int x, int y)
            {
                List<int> area = new List<int>();
                if (!(field[x, y] == -1)){
                    area.Add(field[x, y] * 100 + x * 100 + y);
                    if (field[x, y] == 0)
                    {
                        serounding(x,y,area);
                    }
                }
                else
                {
                    return null;
                }
                return area;
            }

            private  List<int> serounding(int x, int y, List<int> number)
            {
               // List<int> number = new List<int>();

                if (!(number.Contains(field[x,y]*100+x*100+y))){


                    if (!(x - 1 < 0))
                    {
                        if (field[x - 1, y] == 0)
                        {
                            serounding(x - 1, y, number);
                        }
                        number.Add(field[x - 1, y] * 100 + (x - 1) * 100 + y);
                    }

                    if (!(x + 1 < 0))
                    {
                        if (field[x + 1, y] == 0)
                        {
                            serounding(x + 1, y, number);
                        }
                        number.Add(field[x + 1, y] * 100 + (x + 1) * 100 + y);
                    }

                    if (!(y - 1 < 0))
                    {
                        if (field[x, y - 1] == 0)
                        {
                            serounding(x, y - 1, number);
                        }
                        number.Add(field[x, y - 1] * 100 + x * 100 + (y - 1));
                    }

                    if (!(y + 1 < 0))
                    {
                        if (field[x, y + 1] == 0)
                        {
                            serounding(x, y + 1, number);
                        }
                        number.Add(field[x, y + 1] * 100 + x * 100 + (y + 1));
                    }
                
                }
                List<int> numbers = number.Distinct().ToList();

                return numbers;
            }
    }
}
