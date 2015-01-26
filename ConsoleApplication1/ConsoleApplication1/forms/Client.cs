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
        private ClientConnect cc = new ClientConnect();

        private List<Button> buttons;
        public int fieldSizeX;
        public int fieldSizeY;
        public static int x=10,y=10,bombs=10;
        public Client()
        {
            InitializeComponent();
        }

        public void keepopen()
        {
            Console.ReadLine();
        }

        public void newboard(int x , int y , int bombs)
        {
            cc.newfield(x,y,bombs);
        }

        private void nieuweMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cc.newfield(x,y,bombs);
            generateField(x,y);
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

        public void generateField(int x,int y)
        {
            buttons = new List<Button>();
            fieldSizeX = 800/x;
            fieldSizeY = 800/y;
            for (int i = 0; i < x; i++)
            {
                for(int o =0;o< y; o++)
                {
                    Button button = new Button();
                    button.Location = new Point(800/x*i, 800/y*o);
                    button.Visible = true;
                    button.Size = new Size(fieldSizeX, fieldSizeY);
                    this.panel1.Controls.Add(button);
                    buttons.Add(button);
                    button.Click += new EventHandler(this.buttonClick);
                }
            }
        }
        void buttonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show(""+button.Location.X/fieldSizeX +" "+ button.Location.Y/fieldSizeY );
            cc.getPosition(button.Location.X / fieldSizeX, button.Location.Y / fieldSizeY);
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
