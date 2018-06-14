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
    public partial class FirstPartEasy : Form
    {
        Login loginForm;
        bool allowClick = false;
        PictureBox firstGuess;
        Random r = new Random();
        int time = 60;
        SecondPartEasy secondForm;
        public static int points;

        bool click;

        public FirstPartEasy()
        {
            InitializeComponent();
            setRandomImages();
            HideImages();
            points = 0;
            lbwelcome.Visible = false;
            click = false;
            loginForm = new Login();
            lbwelcome.Text = "Welcome " + Login.i.Ime;
            lbwelcome.Visible = true;
            var serializer = new XmlSerializer(FirstPart._highScores.GetType(), "HighScores.Scores");
            object obj;
            using (var reader = new StreamReader("highscores.xml"))
            {
                obj = serializer.Deserialize(reader.BaseStream);
            }
            FirstPart._highScores = (List<Player>)obj;

        }

        private PictureBox[] pictureBoxes
        {

            get { return Controls.OfType<PictureBox>().ToArray(); }

        }

        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[] {
             Properties.Resources._0, Properties.Resources._1,Properties.Resources._4, Properties.Resources._5,
            };
            }
        }

        public void ResetImage()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }
            HideImages();
            setRandomImages();
            time = 60;

            //timer1.Start();
        }
        public void HideImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Image = Properties.Resources.Question1;
            }
        }


        private PictureBox getFreeSlot()
        {
            int num;
            do
            {
                num = r.Next(0, pictureBoxes.Count());
            }
            while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }
        private void setRandomImages()
        {
            foreach (var image in images)
            {
                getFreeSlot().Tag = image;
                getFreeSlot().Tag = image;
            }
        }

        private void clickImage(object sender, EventArgs e)
        {
            if (click)
            {
                var pic = (PictureBox)sender;
                if (!allowClick) return;
                if (firstGuess == null)
                {
                    firstGuess = pic;
                    pic.Image = (Image)pic.Tag;
                    return;
                }
                pic.Image = (Image)pic.Tag;
                if (pic.Image == firstGuess.Image && pic != firstGuess)
                {
                    pic.Visible = firstGuess.Visible = false;

                    {
                        firstGuess = pic;
                    }
                    HideImages();
                }
                else
                {
                    allowClick = false;
                    timer2.Start();
                }
                firstGuess = null;
                if (pictureBoxes.Any(p => p.Visible)) return;
                points = time;
                timer1.Stop();
                label3.Visible = true;
                button2.Visible = true;
                button1.Visible = false;
                //if (pictureBoxes.Count() != 0)
                //ResetImage();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            HideImages();
            allowClick = true;
            timer2.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time < 0)
            {
                timer1.Stop();
                MessageBox.Show("Out of time");
                ResetImage();
                timer1.Start();
            }
            else if (time.ToString().Length == 1)
            {
                label1.Text = "00:0" + time.ToString();
            }
            else
            {
                label1.Text = "00: " + time.ToString();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            secondForm = new SecondPartEasy();
            secondForm.Show();
            secondForm.Location = this.Location;
            this.Hide();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            click = true;
            allowClick = true;
            timer1.Start();

            button1.Visible = false;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            click = false;
            allowClick = false;
            timer1.Stop();
            button1.Visible = true;
            button3.Visible = false;
        }

        private void FirstPartEasy_Load(object sender, EventArgs e)
        {

        }
    }
}

