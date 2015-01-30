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
            fillTextBox();
        }

        private void fillTextBox()
        {
            List<highscoreScore> temp = new List<highscoreScore>();
            string line; 
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C://ProgramData//highscoresMineSweeper.txt");
                //Read the first line of text
                line = sr.ReadLine();


                //Continue to read until you reach end of file
                while (line != null)
                {
                    if (line!=""){
                string[] parts = line.Split('-');
                    highscoreScore score = new highscoreScore();
                    int bombs;
                    int field;
                    int time;
                    int.TryParse(parts[0],out bombs);
                    int.TryParse(parts[1],out field);
                    int.TryParse(parts[2],out time);
                    score.bombs=bombs;
                    score.fields=field;
                    score.time=time;
                    temp.Add(score);
                    }
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                temp.Sort((x, y) => x.time.CompareTo(y.time));
                temp.Sort((x, y) => y.bombs.CompareTo(x.bombs));
                temp.Sort((x, y) => y.fields.CompareTo(x.fields));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            highscoreScore score1= new highscoreScore();
            int count = 0;
            foreach (highscoreScore s in temp)
            {
                if (score1.fields != s.fields || score1.bombs != s.bombs)
                {
                    textBox1.Text += "\r\n";
                    count = 0;
                }
                if (count < 5) { 
                    textBox1.Text += "veld van " + s.fields + " met " + s.bombs+ " bommen met een tijd van " + s.time + "\r\n";
                    count++;
                }
                score1 = s;
            }
        }

        
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
        }
    }
}
