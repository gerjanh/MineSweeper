using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.forms;

namespace MineSweeper
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void nieuweMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.ShowDialog();
        }

        private void spelregelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.ShowDialog();
        }

        private void colofonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colofon colofon = new Colofon();
            colofon.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Client_Load(object sender, EventArgs e)
        {
        }
    }
}
