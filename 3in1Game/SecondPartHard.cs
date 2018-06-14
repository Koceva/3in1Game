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
    public partial class SecondPartHard : Form
    {
        int inNullSliceIndex;
        ThirdPartHard thirdForm;
        public bool isHit { get; set; }
        public List<Bitmap> lstOriginalPicturesList = new List<Bitmap>();
        public List<Bitmap> images = new List<Bitmap>();
        public List<Bitmap> images1 = new List<Bitmap>();
        public List<Bitmap> images2 = new List<Bitmap>();
        public List<Bitmap> images3 = new List<Bitmap>();
        Random r = new Random();
        public List<string> correct = new List<string>();
        public SecondPartHard()
        {
            InitializeComponent();
            isHit = false;
            lblPoints2.Text = FirstPartHard.points.ToString();
            correct.Add("Angelina Jolie");
            correct.Add("Brad Pitt");
            correct.Add("Ryan Reynolds");    
            correct.Add("Blake Lively");   

            images.AddRange(new Bitmap[]{
                Properties.Resources.image8_part_001, Properties.Resources.image8_part_002, Properties.Resources.image8_part_003,
                 Properties.Resources.image8_part_004, Properties.Resources.image8_part_005, Properties.Resources.image8_part_006,
                  Properties.Resources.image8_part_007, Properties.Resources.image8_part_008, Properties.Resources.image8_part_009,
                  Properties.Resources.image8_part_010,Properties.Resources.image8_part_011,Properties.Resources.image8_part_012,
                Properties.Resources.image8_part_013,Properties.Resources.image8_part_014,
                Properties.Resources.image8_part_015,Properties.Resources.image8_part_016, Properties.Resources._null,
            });
            images1.AddRange(new Bitmap[]{
             Properties.Resources.image9_part_001, Properties.Resources.image9_part_002, Properties.Resources.image9_part_003,
                 Properties.Resources.image9_part_004, Properties.Resources.image9_part_005, Properties.Resources.image9_part_006,
                  Properties.Resources.image9_part_007, Properties.Resources.image9_part_008, Properties.Resources.image9_part_009,
                  Properties.Resources.image9_part_010,Properties.Resources.image9_part_011,Properties.Resources.image9_part_012,
                Properties.Resources.image9_part_013,Properties.Resources.image9_part_014,
                Properties.Resources.image9_part_015,Properties.Resources.image9_part_016,
                Properties.Resources._null
            });
            images2.AddRange(new Bitmap[]{
                 Properties.Resources.image10_part_001, Properties.Resources.image10_part_002, Properties.Resources.image10_part_003,
                 Properties.Resources.image10_part_004, Properties.Resources.image10_part_005, Properties.Resources.image10_part_006,
                  Properties.Resources.image10_part_007, Properties.Resources.image10_part_008, Properties.Resources.image10_part_009,
                  Properties.Resources.image10_part_010,Properties.Resources.image10_part_011,Properties.Resources.image10_part_012,
                Properties.Resources.image10_part_013,Properties.Resources.image10_part_014,
                Properties.Resources.image10_part_015,Properties.Resources.image10_part_016,
                Properties.Resources._null
            });
            images3.AddRange(new Bitmap[]{
                Properties.Resources.image11_part_001, Properties.Resources.image11_part_002, Properties.Resources.image11_part_003,
                 Properties.Resources.image11_part_004, Properties.Resources.image11_part_005, Properties.Resources.image11_part_006,
                  Properties.Resources.image11_part_007, Properties.Resources.image11_part_008, Properties.Resources.image11_part_009,
                  Properties.Resources.image11_part_010,Properties.Resources.image11_part_011,Properties.Resources.image11_part_012,
                Properties.Resources.image11_part_013,Properties.Resources.image11_part_014,
                Properties.Resources.image11_part_015,Properties.Resources.image11_part_016,
                Properties.Resources._null
            });

            int ra = r.Next(0, 4);
            if (ra == 0)
            {
                lstOriginalPicturesList = images;
                radioButton1.Text = "Angelina Jolie";
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
                radioButton1.Text = "Brad Pitt";
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
                FirstPartHard.points += 10;
            }
            else
            {
                label6.Visible = true;
                DialogResult dialog = MessageBox.Show("You lost", "Try again", MessageBoxButtons.OK);
                Login loginForm = new Login();
                this.Close();
                loginForm.Show();
            }
            lblPoints2.Text = FirstPartHard.points.ToString();
        }

        private void SecondPartHard_Load(object sender, EventArgs e)
        {
            Shuffle();
        }
        public void Shuffle()
        {
           do
           {
                int j;
                List<int> indexes = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 });
                Random r = new Random();
                for (int i = 0; i < 16; i++)
                {
                    

                        /* ((PictureBox)groupBox1.Controls[15 - i]).Image = lstOriginalPicturesList[i];
                           ((PictureBox)groupBox1.Controls[15 - i]).Image.Tag = i;*/
                            indexes.Remove((j = indexes[r.Next(0, indexes.Count)]));
                           if (j == 16)
                           {
                               inNullSliceIndex = 15 - i;
                               ((PictureBox)groupBox1.Controls[15 - i]).Image = lstOriginalPicturesList[16];
                               ((PictureBox)groupBox1.Controls[15 - i]).Image.Tag = j;
                           }
                           else
                           {
                               ((PictureBox)groupBox1.Controls[15 - i]).Image = lstOriginalPicturesList[j];
                               ((PictureBox)groupBox1.Controls[15 - i]).Image.Tag = j; //prvo
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
                      inPictureBoxIndex-9,inPictureBoxIndex-10,inPictureBoxIndex-11,inPictureBoxIndex-12,
                        inPictureBoxIndex-13,inPictureBoxIndex-14,inPictureBoxIndex-15,
                    inPictureBoxIndex +1,inPictureBoxIndex+2,inPictureBoxIndex+3,inPictureBoxIndex+4,
                    inPictureBoxIndex+5,inPictureBoxIndex+6,inPictureBoxIndex+7,inPictureBoxIndex+8,
                    inPictureBoxIndex+9,inPictureBoxIndex+10,inPictureBoxIndex+11,inPictureBoxIndex+12,
                        inPictureBoxIndex+13,inPictureBoxIndex+14,inPictureBoxIndex+15
                });


                if (FourBrothers.Contains(inNullSliceIndex))
                {
                    ((PictureBox)groupBox1.Controls[inNullSliceIndex]).Image = ((PictureBox)groupBox1.Controls[inPictureBoxIndex]).Image;

                    ((PictureBox)groupBox1.Controls[inPictureBoxIndex]).Image = lstOriginalPicturesList[16];
                    ((PictureBox)groupBox1.Controls[inPictureBoxIndex]).Image.Tag = 16;

                    inNullSliceIndex = inPictureBoxIndex;
                    if (CheckWin())
                    {

                        (groupBox1.Controls[inNullSliceIndex] as PictureBox).Image = lstOriginalPicturesList[15];
                        FirstPartHard.points += 30;
                        button2.Enabled = true;
                        lblPoints2.Text = FirstPartHard.points.ToString();
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
            for (i = 15; i > 0; i--)
            {
                if ((Convert.ToInt32((groupBox1.Controls[i] as PictureBox).Image.Tag) != 15 - i))
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
            thirdForm = new ThirdPartHard();
            thirdForm.Show();
            thirdForm.Location = this.Location;
            this.Close();
        }
        
    }
}
