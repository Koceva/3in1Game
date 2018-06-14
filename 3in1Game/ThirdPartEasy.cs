using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace _3in1Game
{
    public partial class ThirdPartEasy : Form
    {
        public List<Words> words { get; set; }
        public Words word { get; set; }
        public Words selectWord { get; set; }
        public bool clicked { get; set; }
        public FirstPartEasy form1 { get; set; }
        public Login loginForm;

        int time = 25;
        Timer timer = new Timer
        {
            Interval = 1000
        };

        public ThirdPartEasy()
        {
            InitializeComponent();
            words = new List<Words>();
            word = new Words();
            lblPoints.Text = FirstPartEasy.points.ToString();
            Fill();
            HideLabels();
            Selected();
            startGameTimer();
        }
        public void startGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time <= 0)
                {
                    timer.Stop();
                    label8.Visible = true;
                    label9.Visible = false;
                    lblText.Text = "00:00";
                    GameOver();

                }
               else if (time.ToString().Length == 1)
                {
                    lblText.Text = "00:0" + time.ToString();
                }
                else
                {
                    lblText.Text = "00: " + time.ToString();
                }
            };
        }
        public void GameOver()
        {
            timer.Stop();

            var player = new Player() { Poeni = FirstPartEasy.points, Ime = Login.i.Ime, Mode = Login.i.Mode };
            FirstPart._highScores.Add(player);

            var serializer = new XmlSerializer(FirstPart._highScores.GetType(), "HighScores.Scores");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, FirstPart._highScores);
            }

            bestPlayers();
            DialogResult dialog = MessageBox.Show("End of the game\n\nTotal points: " + FirstPartEasy.points + " ", "Do you want to play again?", MessageBoxButtons.YesNoCancel);
            if (dialog == DialogResult.Yes)
            {

                loginForm = new Login();
                this.Close();
                loginForm.Show();
            }
            else if (dialog == DialogResult.No)
            {
                Application.Exit();
            }
            else if (dialog == DialogResult.Cancel)
            {
                lblText.Text = "00: " + "00";
            }


        }

        public void HideLabels()
        {
            lbl2.Visible = false;
            lbl3.Visible = false;
            pictureBox1.Image = Properties.Resources.MovieQuestion;
        }

        public void Fill()
        {
            word.MovieName = "MALEFICENT";
            word.movieAssociation.Add("Angelina Jolie");
            word.movieAssociation.Add("Evil");
            word.movieAssociation.Add("Infant princess");
            word.movieAssociation.Add("Sleeping Beauty");
            word.movieAssociation.Add("Ruthless betrayal");
            word.movieAssociation.Add("Aurora");
            word.image = Properties.Resources.Maleficent;
            words.Add(word);

            word = new Words();
            word.MovieName = "HARRY POTTER";
            word.movieAssociation.Add("Emma Watson");
            word.movieAssociation.Add("Three best friends");
            word.movieAssociation.Add("School of Witchcraft");
            word.movieAssociation.Add("Fantasy");
            word.movieAssociation.Add("Wizards");
            word.movieAssociation.Add("Voldemort");
            word.image = Properties.Resources.hp;
            words.Add(word);

            word = new Words();
            word.MovieName = "LA LA LAND";
            word.movieAssociation.Add("Ryan Gosling");
            word.movieAssociation.Add("Jazz pianist");
            word.movieAssociation.Add("Love story");
            word.movieAssociation.Add("Emma Stone");
            word.movieAssociation.Add("Musical");
            word.movieAssociation.Add("Los Angeles");
            word.image = Properties.Resources.lalaLand;
            words.Add(word);

            word = new Words();
            word.MovieName = "TITANIC";
            word.movieAssociation.Add("Leonardo DiCaprio");
            word.movieAssociation.Add("My heart will go on");
            word.movieAssociation.Add("Iceberg");
            word.movieAssociation.Add("Sink");
            word.movieAssociation.Add("Kate Winslet");
            word.movieAssociation.Add("Ocean");
            word.image = Properties.Resources.Titanic;
            words.Add(word);

            word = new Words();
            word.MovieName = "DEADPOOL";
            word.movieAssociation.Add("Ryan Reynolds");
            word.movieAssociation.Add("Superhero");
            word.movieAssociation.Add("Revenge");
            word.movieAssociation.Add("Healing powers");
            word.movieAssociation.Add("Mutant");
            word.movieAssociation.Add("Wade Wilson");
            word.image = Properties.Resources.Deadpool;
            words.Add(word);

            word = new Words();
            word.MovieName = "WONDER WOMAN";
            word.movieAssociation.Add("Gal Gadot");
            word.movieAssociation.Add("Diana");
            word.movieAssociation.Add("Amazonian warrior");
            word.movieAssociation.Add("Fighting in war");
            word.movieAssociation.Add("Hidden island");
            word.movieAssociation.Add("DC Comics character");
            word.image = Properties.Resources.wonder;
            words.Add(word);


        }

        public void Selected()
        {
            Random r = new Random();
            int indeks = r.Next(0, 6);
            selectWord = words.ElementAt(indeks);
            lbl1.Text = selectWord.movieAssociation.ElementAt(r.Next(0, 6));
            lbl2.Text = selectWord.movieAssociation.ElementAt(r.Next(0, 6));
            lbl3.Text = selectWord.movieAssociation.ElementAt(r.Next(0, 6));
            while (lbl2.Text == lbl1.Text)
            {
                lbl2.Text = selectWord.movieAssociation.ElementAt(r.Next(0, 6));
            }
            while (lbl3.Text == lbl2.Text || lbl3.Text == lbl1.Text)
            {
                lbl3.Text = selectWord.movieAssociation.ElementAt(r.Next(0, 6));

            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            lbl1.Visible = true;
        }

      

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = selectWord.image;
            clicked = true;
        }
        public void GuessWord()
        {

            if (txtName.Text.ToUpper() == selectWord.MovieName)
            {
                if (lbl2.Visible == true && lbl3.Visible == true && clicked == true)
                {
                    FirstPartEasy.points += 5;
                }
                else
                {
                    if (lbl2.Visible == false)
                    {
                        FirstPartEasy.points += 5;
                    }
                    if (lbl3.Visible == false)
                    {
                        FirstPartEasy.points += 5;
                    }
                    if (clicked == false)
                    {
                        FirstPartEasy.points += 10;
                    }
                }
                string s1 = "";
                Guess.Enabled = false;
                s1 += "Congratulations " + Login.i.Ime + ", you hit the movie!";
                label9.Visible = false;
                label7.Text = s1;
                label7.Visible = true;
                lblPoints.Text = FirstPartEasy.points.ToString();
                GameOver();
            }
            else
            {
                label9.Visible = true;
            }

        }
        public void bestPlayers()
        {

            Login.igrachi = FirstPart._highScores.OrderByDescending(x => x.Poeni).ToList();

            string s = "";
            int br = Login.igrachi.Count;

            if (br > 3)
            {
                br = 3;
            }
            for (int i = 0; i < br; i++)
            {
                s += Login.igrachi.ElementAt(i).Ime + "\t Total points: " + Login.igrachi.ElementAt(i).Poeni +"\t Mode: "+Login.igrachi.ElementAt(i).Mode +"\n";

            }

            MessageBox.Show(s, "Best players: ");

        }

        private void Guess_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "") GuessWord();
            else MessageBox.Show("Enter movie name!");
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (txtName.Text == "")
            {
                errorProvider1.SetError(txtName, "Enter movie name!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void bestPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bestPlayers();
        }

        private void playAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();

            DialogResult dialog = MessageBox.Show("Are you sure?", "Do you want to play again?", MessageBoxButtons.YesNoCancel);
            if (dialog == DialogResult.Yes)
            {

                loginForm = new Login();
                this.Close();
                loginForm.Show();
            }
            else if (dialog == DialogResult.No)
            {
                Application.Exit();
            }

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            lbl2.Visible = true;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            lbl3.Visible = true;
        }
    }
}
