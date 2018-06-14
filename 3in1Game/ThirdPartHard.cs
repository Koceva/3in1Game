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
    public partial class ThirdPartHard : Form
    {
        public List<Words> words { get; set; }
        public Words word { get; set; }
        public Words selectWord { get; set; }
        public bool clicked { get; set; }
        public FirstPartHard form1 { get; set; }
        public Login loginForm;

        int time = 25;
        Timer timer = new Timer
        {
            Interval = 1000
        };

        public ThirdPartHard()
        {
            InitializeComponent();
            words = new List<Words>();
            word = new Words();
         lblPoints.Text = FirstPartHard.points.ToString();
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

            var player = new Player() { Poeni = FirstPartHard.points, Ime = Login.i.Ime, Mode = Login.i.Mode };
            FirstPart._highScores.Add(player);

            var serializer = new XmlSerializer(FirstPart._highScores.GetType(), "HighScores.Scores");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, FirstPart._highScores);
            }

            bestPlayers();
            DialogResult dialog = MessageBox.Show("End of the game\n\nTotal points: " + FirstPartHard.points + " ", "Do you want to play again?", MessageBoxButtons.YesNoCancel);
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
            word.movieAssociation.Add("Noxious");
            word.movieAssociation.Add("Sleeping Beauty");
            word.movieAssociation.Add("Vicious");
            word.movieAssociation.Add("Hostile");
            word.image = Properties.Resources.Maleficent;
            words.Add(word);

            word = new Words();
            word.MovieName = "GOSSIP GIRL";
            word.movieAssociation.Add("Blake Lively");
            word.movieAssociation.Add("Empire State");
            word.movieAssociation.Add("Chatter");
            word.movieAssociation.Add("Female");
            word.movieAssociation.Add("Series");
            word.movieAssociation.Add("Defamation");
            word.image = Properties.Resources.Gossip_girl;
            words.Add(word);

            word = new Words();
            word.MovieName = "DIVERGENT";
            word.movieAssociation.Add("Kate Winslet");
            word.movieAssociation.Add("Science fiction trilogy");
            word.movieAssociation.Add("Dividing people");
            word.movieAssociation.Add("Fractions based on human virtues");
            word.movieAssociation.Add("Tris & Four");
            word.movieAssociation.Add("Different");
            word.image = Properties.Resources.divergent;
            words.Add(word);

            word = new Words();
            word.MovieName = "MAZE RUNNER";
            word.movieAssociation.Add("Run");
            word.movieAssociation.Add("Labyrinth");
            word.movieAssociation.Add("Waking up without memory");
            word.movieAssociation.Add("Finding a way out");
            word.movieAssociation.Add("Science fiction");
            word.movieAssociation.Add("Dylan O'Brien");
            word.image = Properties.Resources.maze;
            words.Add(word);

            word = new Words();
            word.MovieName = "DEADPOOL";
            word.movieAssociation.Add("Ryan Reynolds");
            word.movieAssociation.Add("Superhero");
            word.movieAssociation.Add("Revenge");
            word.movieAssociation.Add("Healing powers");
            word.movieAssociation.Add("Oscar");
            word.movieAssociation.Add("Dying");
            word.image = Properties.Resources.Deadpool;
            words.Add(word);

            word = new Words();
            word.MovieName = "DESPERATE HOUSEWIVES";
            word.movieAssociation.Add("Eva Longoria");
            word.movieAssociation.Add("Comedy series");
            word.movieAssociation.Add("Neighbours");
            word.movieAssociation.Add("The life of a group of women");
            word.movieAssociation.Add("Affairs");
            word.movieAssociation.Add("Homekeepers");
            word.image = Properties.Resources.dd;
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

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            lbl3.Visible = true;
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            lbl2.Visible = true;
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
                    FirstPartHard.points += 5;
                }
                else
                {
                    if (lbl2.Visible == false)
                    {
                        FirstPartHard.points += 5;
                    }
                    if (lbl3.Visible == false)
                    {
                        FirstPartHard.points += 5;
                    }
                    if (clicked == false)
                    {
                        FirstPartHard.points += 10;
                    }
                }
                string s1 = "";
                Guess.Enabled = false;
                s1 += "Congratulations " + Login.i.Ime + ", you hit the movie!";
                label9.Visible = false;
                label7.Text = s1;
                label7.Visible = true;
                lblPoints.Text = FirstPartHard.points.ToString();
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
                s += Login.igrachi.ElementAt(i).Ime + "\t Total points: " + Login.igrachi.ElementAt(i).Poeni + "\t Mode: " + Login.igrachi.ElementAt(i).Mode + "\n";

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

        private void lblText_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}