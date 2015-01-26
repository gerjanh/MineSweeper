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
              int bombs = 0;

                  while (bombs < numOfBombs)
                  {
                      for (int i = 0; i < numOfBombs; i++)
                      {
                          int randomx = rnd.Next(x);
                              int randomy = rnd.Next(y);
                          field[randomx, randomy] = -1;
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
                          if ((idy + 1 < y))
                          {
                              if ((idx - 1 > 0))
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
                              if ((idx + 1 < x))
                              {
                                  if (field[idx + 1, idy + 1] == -1)
                                  {
                                      numOfBombsAround++;
                                  }
                              }
                          }
                          
                          if ((idx - 1 > 0))
                          {
                              if (field[idx - 1, idy] == -1)
                              {
                                  numOfBombsAround++;
                              }
                          }
                          if ((idx + 1 < x))
                          {
                              if (field[idx + 1, idy] == -1)
                              {
                                  numOfBombsAround++;
                              }
                          }
                          
                          if ((idy -1 > 0))
                          {
                              if ((idx - 1 > 0))
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
                              if ((idx + 1 < x))
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
         public int[,] getfield()
         {
             return field;
         }

         public List<ButtonPosition> getSpot(int x, int y)
          {
              List<ButtonPosition> area = new List<ButtonPosition>();
                      serounding(x,y,area);
              return area;
          }

         private List<ButtonPosition> serounding(int x, int y, List<ButtonPosition> number)
          {
              bool temp = false ;
             foreach (ButtonPosition b in number){
                 if (b.x == x && b.y == y)
                 {
                     temp = true;
                 }
             }
              if (!(temp)){

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
              List<ButtonPosition> numbers = number.Distinct().ToList();

              return numbers;
          }
    } 
}
