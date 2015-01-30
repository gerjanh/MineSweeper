using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MineSweeper.forms
{
    public partial class Highscores : Form
    {
        private DataTable table;
        public Highscores()
        {
            InitializeComponent();

            string lastLine = File.ReadLines("C://ProgramData//highscoresMineSweeper.txt").Last();
            char[] delimiters = new char[] { '-' };
            string[] parts = lastLine.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            bommenLabel.Text = "Aantal bommen: " + parts[0];
            veldGrootteLabel.Text = "Grootte van het veld: " + parts[1] + " vakjes";
            tijdLabel.Text ="Je hebt dit level voltooid in: " + parts[2] + " seconden";
            //table = new DataTable();
        }


        /*
        public DataTable dataTable()
        {
            table.Columns.Add("Bommen", typeof(int));
            table.Columns.Add("Grootte", typeof(int));
            table.Columns.Add("Tijd", typeof(int));

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C://ProgramData//highscoresMineSweeper.txt");
                //Read the first line of text
                line = sr.ReadLine();
                char[] delimiters = new char[] {'-'};


                //Continue to read until you reach end of file
                while (line != null)
                {
                    string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    table.Rows.Add(parts[0], parts[1], parts[2]);
                    Console.WriteLine(parts[0], parts[1], parts[2]);
                    //Read the next line
                    line = sr.ReadLine();
                }
                    //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            return table;
        }*/
    }
}
