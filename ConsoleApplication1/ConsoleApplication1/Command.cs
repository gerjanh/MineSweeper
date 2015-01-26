using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Command
    {
        public Command()
        {
            parameters = new Dictionary<string, int>();
           // buttons = new List<ButtonPosition>();
        }

        public int clientId { get; set; }
        public commands theCommand { get; set; }
        public Dictionary<string, int> parameters { get; set; }

        // public List<ButtonPosition> buttons { get; set; }

    }
}
