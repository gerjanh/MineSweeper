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
        private bool gameover = false;

        private List<Button> buttons;
        public int fieldSizeX;
        public int fieldSizeY;
        public static int sx=10,sy=10,sbombs=10;
        private int x = sx, y = sy, bombs = sbombs;
        private new Dictionary<int, Color> color;
        public Client()
        {
            InitializeComponent();
            color = new Dictionary<int, Color>();
            color.Add(0, Color.Gray);
            color.Add(1, Color.Green);
            color.Add(2, Color.GreenYellow);
            color.Add(3, Color.Yellow);
            color.Add(4, Color.Orange);
            color.Add(5, Color.OrangeRed);
            color.Add(6, Color.Red);
            color.Add(7, Color.Red);
            color.Add(8, Color.Black);
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
            x = sx;
            y = sy;
            bombs = sbombs;
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
            this.panel1.Controls.Clear();
            gameover = false;
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
                    //button.Click += new EventHandler(this.buttonClick);
                    button.MouseDown += new MouseEventHandler(this.mouseDown);
                }
            }
        }
       
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        void mouseDown(object sender, MouseEventArgs e)
        {
            if (!gameover) {
            Button button = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (button.Text == "")
                {
                    fillButtons(cc.getPosition(button.Location.X / fieldSizeX, button.Location.Y / fieldSizeY));
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (button.Text == "")
                {
                    button.Text = "flagg";
                    button.ForeColor = Color.Red;
                }
                else if (button.Text == "flagg")
                    button.Text = "";
            }
        }
            else {
                MessageBox.Show("je bent gameover en kunt dus niet verder gaan");
            }
            
        }

        public void fillButtons(List<ButtonPosition> knoppen)
        {
            foreach (Button k in buttons)
            {
                foreach (ButtonPosition b in knoppen)
                {
                    if (k.Location.X / fieldSizeX == b.x && k.Location.Y / fieldSizeY == b.y)
                    {
                        if (b.point == -1)
                        {
                            gameover = true;
                        }
                        else if (k.Text!=""){

                        }
                        else
                        {
                            k.Text = b.point + "";
                            k.Enabled = false;
                            k.BackColor= color[b.point];
                        }
                    }
                }
            }
        }

    }
}
