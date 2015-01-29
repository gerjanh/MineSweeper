using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper.forms
{
    public partial class MineSweeperStart : Form
    {

        public MineSweeperStart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            serverForm b = new serverForm();
            b.ShowDialog();
            this.Close(); 
        }

        private void join_Click(object sender, EventArgs e)
        {
            IPAddress ipAdress;
            if (IPAddress.TryParse(textBox1.Text, out ipAdress))
            {
                this.Hide();
                Client b = new Client(textBox1.Text);
                b.ShowDialog();

            }
            else {
                MessageBox.Show("Dit is geen ip-address een ip-address heeft het formaat van 123.456.789.101 vul voor je eigen computen 127.0.0.1 in bij ip-address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

    }
}
