using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper.forms
{

    public partial class Options : Form
    {
        private int kolommen;
        private int rijen;
        private int bommen;
        private int max_size = 30;

        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            //KOLOMMEN OPTIE
            //Create a new label and text box
            Label kolommenLabel = new Label();
            TextBox kolommenTextBox = new TextBox();

            //Initialize label's property
            kolommenLabel.Text = "Aantal kolommen: ";
            kolommenLabel.Location = new Point(10, 10);
            kolommenLabel.AutoSize = true;

            //Initialize textBoxes Property
            int afstand = kolommenLabel.Width + 100;
            kolommenTextBox.Location = new Point(afstand, kolommenLabel.Top - 3);
            kolommenTextBox.TextChanged += new EventHandler(this.kolommenTextBox_TextChanged);
            kolommenTextBox.MaxLength = 2;

            //Add the labels and text box to the form
            this.Controls.Add(kolommenLabel);
            this.Controls.Add(kolommenTextBox);

            //RIJEN OPTIE
            //Create a new label and text box
            Label rijenLabel = new Label();
            TextBox rijenTextBox = new TextBox();

            //Initialize label's property
            rijenLabel.Text = "Aantal rijen: ";
            rijenLabel.Location = new Point(10, 30);
            rijenLabel.AutoSize = true;

            //Initialize textBoxes Property
            rijenTextBox.Location = new Point(afstand, rijenLabel.Top - 3);
            rijenTextBox.TextChanged += new EventHandler(this.rijenTextBox_TextChanged);
            rijenTextBox.MaxLength = 2;

            //Add the labels and text box to the form
            this.Controls.Add(rijenLabel);
            this.Controls.Add(rijenTextBox);

            //BOMMEN OPTIE
            //Create a new label and text box
            Label bommenLabel = new Label();
            TextBox bommenTextBox = new TextBox();

            //Initialize label's property
            bommenLabel.Text = "Aantal bommen: ";
            bommenLabel.Location = new Point(10, 50);
            bommenLabel.AutoSize = true;

            //Initialize textBoxes Property
            bommenTextBox.Location = new Point(afstand, bommenLabel.Top - 3);
            bommenTextBox.TextChanged += new EventHandler(this.bommenTextBox_TextChanged);
            bommenTextBox.MaxLength = 3;

            //Add the labels and text box to the form
            this.Controls.Add(bommenLabel);
            this.Controls.Add(bommenTextBox);

            Button startButton = new Button();
            startButton.Location = new Point(afstand, 70);
            startButton.Visible = true;
            startButton.Text = "Start";
            startButton.Size = new Size(100, 20);
            this.Controls.Add(startButton);
            startButton.Click += new EventHandler(this.startButtonClick);
        }

        void startButtonClick(object sender, EventArgs e)
        {
            Boolean kolom = false;
            Boolean rij = false;
            Boolean bom = false;

            if (kolommen <= max_size && kolommen > 0)
            {
                kolom = true;
            }
            if (rijen <= max_size && rijen > 0)
            {
                rij = true;
            }
            if (bommen < (max_size * max_size - 1) && bommen > 0 && bommen < kolommen * rijen)
            {
                bom = true;
            }

            if (kolom == true && rij == true)
            {
                if (bommen >= kolommen * rijen)
                {
                    MessageBox.Show("Er bevinden zich meer bommen dan velden in het spel!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // verzend kolommen + rijen + bommen

                   // MessageBox.Show("verzend kolommen + rijen + bommen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Client.sbombs = bommen;
                    Client.sx = kolommen;
                    Client.sy = rijen;
                    this.Close();
                }
            }
            else
            {
                string melding = String.Format("Let erop dat de waarden van de kolommen en rijen zich tussen de 1 en {0} moeten bevinden en van de bommen tussen 1 en {1}.",max_size,max_size*max_size-1);
                MessageBox.Show(melding, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kolommenTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (String.IsNullOrEmpty(t.Text))
            {
                kolommen = 0;
            }
            else
            {
                try
                {
                    kolommen = Convert.ToInt32(t.Text);
                    kolommen = int.Parse(t.Text);

                    if (kolommen <= max_size && kolommen > 0)
                    {
                    }
                    else
                    {
                        string melding = String.Format("De waarden van kolommen ligt tussen 1 en {0}",max_size);
                        MessageBox.Show(melding, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {

                    MessageBox.Show("Alleen getallen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void rijenTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (String.IsNullOrEmpty(t.Text))
            {
                rijen = 0;
            }
            else
            {
                try
                {
                    rijen = Convert.ToInt32(t.Text);
                    rijen = int.Parse(t.Text);

                    if (rijen <= max_size && rijen > 0)
                    {
                    }
                    else
                    {
                        string melding = String.Format("De waarden van rijen ligt tussen 1 en {0}", max_size);
                        MessageBox.Show(melding, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {

                    MessageBox.Show("Alleen getallen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void bommenTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (String.IsNullOrEmpty(t.Text))
            {
                bommen = 0;
            }
            else
            {
                try
                {
                    bommen = Convert.ToInt32(t.Text);
                    bommen = int.Parse(t.Text);
                    int aantalVelden = kolommen * rijen;
                    if (bommen < (max_size * max_size-1) && bommen > 0)
                    {
                        if (bommen >= aantalVelden)
                        {
                            MessageBox.Show("Er zijn meer bommen in het veld dan dat er velden zijn verlaag het aantal bommen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        string melding = String.Format("De waarden van het aantal bommen ligt tussen 1 en {0}",max_size*max_size-1);
                        MessageBox.Show(melding, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {

                    MessageBox.Show("Alleen getallen!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
