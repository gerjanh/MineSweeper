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
        private ClientConnect cc ;//= new ClientConnect();
        private bool gameover = false, won = false ;

        private List<Button> buttons;
        public int fieldSizeX;
        public int fieldSizeY;
        public static int sx = 10, sy = 10, sbombs = 10;
        private int x = sx, y = sy, bombs = sbombs,flaggs=0,timer=0;
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
            color.Add(8, Color.DarkRed);
            cc= new ClientConnect();
            setCounter();
        }

        public Client(string ipadress)
        {
            if (ipadress != string.Empty)
            {
                cc=new ClientConnect(ipadress);
            }
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
            color.Add(8, Color.DarkRed);
            setCounter();
        }

        public void keepopen()
        {
            Console.ReadLine();
        }

        private void nieuweMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x = sx;
            y = sy;
            bombs = sbombs;
            flaggs = 0;
            cc.newfield(x, y, bombs);
            generateField(x, y);
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

        public void generateField(int x, int y)
        {
            buttons = new List<Button>();
            this.panel1.Controls.Clear();
            gameover = won = false;
            fieldSizeX = 800 / x;
            fieldSizeY = 800 / y;
            for (int i = 0; i < x; i++)
            {
                for (int o = 0; o < y; o++)
                {
                    Button button = new Button();
                    button.Location = new Point(800 / x * i, 800 / y * o);
                    button.Visible = true;
                    button.Size = new Size(fieldSizeX, fieldSizeY);
                    this.panel1.Controls.Add(button);
                    buttons.Add(button);
                    button.MouseDown += new MouseEventHandler(this.mouseDown);
                }
            }
            setBommenLabel();
            timer = 0;
            startCounter();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        void mouseDown(object sender, MouseEventArgs e)
        {
            if (!gameover&&!won)
            {
                Button button = (Button)sender;
                if (e.Button == MouseButtons.Left)
                {
                    
                        if (button.Image == null)
                        {
                            fillButtons(cc.getPosition(button.Location.X / fieldSizeX, button.Location.Y / fieldSizeY));
                        }
                    
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (button.Image == null)
                    {

                        setImage(button, "flagg", false);
                        flaggs++;
                    }
                    else
                    {
                        setImage(button, "", true);
                        flaggs--;
                    }
                    
                }
                checkIfWon();
                setBommenLabel();
            }
        }

        private void checkIfWon()
        {
            won = true;
            foreach (Button b in buttons)
            {
                if (b.Text == "" && b.Image == null)
                {
                    won = false;
                }
            }
        }

        public void setImage(Button b, string imageName, bool remove)
        {
            if (remove)
            {
                b.Image = null;
            }
            else
            {
                string temp = this.GetType().Assembly.Location;
                string[] verdeeld = temp.Split('\\');
                string path = "";
                for (int i = 0; i < verdeeld.LongLength - 3; i++)
                {
                    path += verdeeld[i] + '/';
                }
                path += "images/" + imageName + ".png";
                Image image = Image.FromFile(path);
                Bitmap objBitmap = new Bitmap(image, new Size(fieldSizeX, fieldSizeY));
                b.Image = objBitmap;
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
                            setImage(k, "bomb2", false);
                            gameover = true;
                        }
                        else if (k.Text != ""||k.Image!=null)
                        {

                        }
                        else
                        {
                            k.Text = b.point + "";
                            k.Enabled = false;
                            k.BackColor = color[b.point];
                        }
                    }
                }
            }
            setBommenLabel();
        }

        private void setBommenLabel()
        {
            if (gameover)
            {
                AantalBommen.Text = "Game Over! Start een nieuwe match!";
                timer1.Stop();
            }
            else if (won){
                AantalBommen.Text = "je hebt gewonnen";
                timer1.Stop();
            }
            else
            {
                AantalBommen.Text = "Aantal bommen: "+ (bombs -flaggs);
            }
            
        }
        private void setCounter()
        {
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(OnTimedEvent);
            label1.Text = " seconden";
        }

        private void startCounter()
        {
            label1.Text = " seconden";
            timer1.Start();
        }

        private void OnTimedEvent(Object source, EventArgs e)
        {
            
            label1.Text = timer++ + " seconden";

        }
    }
    
}
