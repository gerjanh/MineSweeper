﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Server server = new Server();
            Thread newThread = new Thread(new ThreadStart(server.keepopen));
            newThread.Start(); 


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Client());


            
        }
    }
}
