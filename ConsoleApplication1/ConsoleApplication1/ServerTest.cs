using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    class ServerTest
    {

      //  Server server = new Server();
        public void start()
        {
            MineSweeperField minefield = new MineSweeperField();
            Random rnd = new Random();
            minefield.newField();
            minefield.getSpot(rnd.Next(8),rnd.Next(8));
        }
    }
}
