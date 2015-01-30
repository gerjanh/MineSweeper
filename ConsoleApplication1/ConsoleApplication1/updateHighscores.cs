using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MineSweeper
{
    class UpdateHighscores
    {
        public UpdateHighscores(int bommen, int grootte, int tijd)
        {

            File.AppendAllText("C://ProgramData//highscoresMineSweeper.txt", string.Format("{0}"+ bommen+"-"+grootte+"-"+tijd, Environment.NewLine));
        }
    }
}
