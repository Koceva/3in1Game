using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3in1Game
{
    public partial class SecondPart : Form
    {
        int inNullSliceIndex;
        ThirdPart thirdForm;
        public bool isHit { get; set; }
        public List<Bitmap> lstOriginalPicturesList = new List<Bitmap>();
        public List<Bitmap> images = new List<Bitmap>();
        public List<Bitmap> images1 = new List<Bitmap>();
        public List<Bitmap> images2 = new List<Bitmap>();
        public List<Bitmap> images3 = new List<Bitmap>();
        Random r = new Random();
        public List<string> correct = new List<string>();
        public SecondPart()
        {
            InitializeComponent();
            this.Size = new Size(950, 700);
            isHit = false;
            lblPoints2.Text = FirstPart.points.ToString();
            correct.Add("Will Smith");
            correct.Add("William Levy");
            correct.Add("Kate Winslet");
            correct.Add("Eva Longoria");

            images.AddRange(new Bitmap[]{
                Properties.Resources.image_part_001, Properties.Resources.image_part_002, Properties.Resources.image_part_003,
                 Properties.Resources.image_part_004, Properties.Resources.image_part_005, Properties.Resources.image_part_006,
                  Properties.Resources.image_part_007, Properties.Resources.image_part_008, Properties.Resources.image_part_009,
                   Properties.Resources._null,
            });
            images1.AddRange(new Bitmap[]{
                Properties.Resources.image1_part_001,Properties.Resources.image1_part_002,Properties.Resources.image1_part_003,
                Properties.Resources.image1_part_004,Properties.Resources.image1_part_005,Properties.Resources.image1_part_006,
                Properties.Resources.image1_part_007,Properties.Resources.image1_part_008,Properties.Resources.image1_part_009,
                Properties.Resources._null
            });
            images2.AddRange(new Bitmap[]{
                Properties.Resources.image2_part_001,Properties.Resources.image2_part_002,Properties.Resources.image2_part_003,
                Properties.Resources.image2_part_004,Properties.Resources.image2_part_005,Properties.Resources.image2_part_006,
                Properties.Resources.image2_part_007,Properties.Resources.image2_part_008,Properties.Resources.image2_part_009,
                Properties.Resources._null
            });
            images3.AddRange(new Bitmap[]{
                Properties.Resources.image3_part_001,Properties.Resources.image3_part_002,Properties.Resources.image3_part_003,
                Properties.Resources.image3_part_004,Properties.Resources.image3_part_005,Properties.Resources.image3_part_006,
                Properties.Resources.image3_part_007,Properties.Resources.image3_part_008,Properties.Resources.image3_part_009,
                Properties.Resources._null
            });

            int ra = r.Next(0, 4);
           if (ra == 0)
            {
                lstOriginalPicturesList = images;
                radioButton1.Text = "Emma Stone";
                radioButton2.Text = "Kate Winslet";
                radioButton3.Text = "Sarah Jessica Parker";
            }
            else if (ra == 1)
            {
                lstOriginalPicturesList = images1;
                radioButton1.Text = "Eva Longoria";
                radioButton2.Text = "Blake Lively";
                radioButton3.Text = "Scarlett Johansson";
            }
            else if (ra == 2)
            {
                lstOriginalPicturesList = images2;
                radioButton1.Text = "Tom Cruise";
                radioButton2.Text = "William Levy";
                radioButton3.Text = "Ryan Reynolds";

            }
            else if (ra == 3)
            {
                lstOriginalPicturesList = images3;
                radioButton1.Text = "Tom Cruise";
                radioButton2.Text = "Leonardno DiCaprio";
                radioButton3.Text = "Will Smith";
            }
        }
        public void Hit()
        {
            string name = null;
            if (radioButton1.Checked)
            {
                name = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                name = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                name = radioButton3.Text;
            }
            if (correct.Contains(name))
            {
                label5.Visible = true;
                label3.Visible = true;
                FirstPart.points += 10;
            }
            else
            {
                label6.Visible = true;
                DialogResult dialog = MessageBox.Show("You lost", "Try again", MessageBoxButtons.OK);
                Login loginForm = new Login();
                this.Close();
                loginForm.Show();
            }
            lblPoints2.Text = FirstPart.points.ToString();
        }

        private void SecondPart_Load(object sender, EventArgs e)
        {
            Shuffle();
        }
        public void Shuffle()
        {
            do
            {
                int j;
                List<int> indexes = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 9 });
                Random r = new Random();
                for (int i = 0; i < 9; i++)
                {
                    indexes.Remove((j = indexes[r.Next(0, indexes.Count)]));
                    ((PictureBox)groupBox1.Controls[8-i]).Image = lstOriginalPicturesList[j];
                   ((PictureBox)groupBox1.Controls[8-i]).Image.Tag = j; //prvo
                    if (j == 9)
                    {
                        inNullSliceIndex = 8-i;
                    }

                }
            }
            while (CheckWin());
        }
        private void SwitchPictureBox(object sender, EventArgs e)
        {

            int inPictureBoxIndex = groupBox1.Controls.IndexOf(sender as Control);

            if (inNullSliceIndex != inPictureBoxIndex)
            {
                List<int> FourBrothers = new List<int>(new int[] {
                    inPictureBoxIndex-1,inPictureBoxIndex-2,inPictureBoxIndex-3,inPictureBoxIndex-4,
                    inPictureBoxIndex-5,inPictureBoxIndex-6,inPictureBoxIndex-7,inPictureBoxIndex-8,
                    inPictureBoxIndex +1,inPictureBoxIndex+2,inPictureBoxIndex+3,inPictureBoxIndex+4,
                    inPictureBoxIndex+5,inPictureBoxIndex+6,inPictureBoxIndex+7,inPictureBoxIndex+8
                });


                if (FourBrothers.Contains(inNullSliceIndex))
                {
                    ((PictureBox)groupBox1.Controls[inNullSliceIndex]).Image = ((PictureBox)groupBox1.Controls[inPictureBoxIndex]).Image;

                    ((PictureBox)groupBox1.Controls[inPictureBoxIndex]).Image = lstOriginalPicturesList[9];
                   ((PictureBox)groupBox1.Controls[inPictureBoxIndex]).Image.Tag = 9;

                    inNullSliceIndex = inPictureBoxIndex;
                    if (CheckWin())
                    {

                        (groupBox1.Controls[inNullSliceIndex] as PictureBox).Image = lstOriginalPicturesList[8];
                        FirstPart.points += 30;
                        button2.Enabled = true;
                        lblPoints2.Text = FirstPart.points.ToString();
                        //Shuffle();
                        label8.Visible = true;
                        button1.Enabled = false;
                        button2.Enabled = true;
                    }
                }
            }
        }
        public bool CheckWin()
        {
            int i;
            for (i = 8; i >0; i--)
            {
               if((Convert.ToInt32((groupBox1.Controls[i] as PictureBox).Image.Tag) != 8-i)) 
                {
                    break;
                }
            }
             if (i == 0) return true;
            else return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
            {
                Hit();
                button1.Enabled = false;
                button2.Enabled = true;
                groupBox1.Visible = false;
            }
            else
            {
                MessageBox.Show("You haven't selected any of the answers.\nTry again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thirdForm = new ThirdPart();
            thirdForm.Show();
            thirdForm.Location = this.Location;
            this.Close();
        }
    }
}
