using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzelGameC_
{
    public partial class Form1 : Form
    {
        int inNullSliceIndex,inMoves = 0;
        List<Bitmap> lstOrignalPicList = new List<Bitmap>();
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        public Form1()
        {
            InitializeComponent();
            lstOrignalPicList.AddRange(new Bitmap[] {Properties.Resources._1, Properties.Resources._2, Properties.Resources._3,
            Properties.Resources._4,Properties.Resources._5,Properties.Resources._6,Properties.Resources._7,
            Properties.Resources._8,Properties.Resources._9,Properties.Resources._null});
            lbMovesMade.Text += inMoves;
        }
        void Shuffel()
        {
            do
            {
                int j;
                List<int> Index = new List<int>(new int[] {0,1,2,3,4,5,6,7,9});
                Random r= new Random();
                for (int i = 0; i < 9; i++)
                {
                    Index.Remove((j = Index[r.Next(0, Index.Count)]));
                    ((PictureBox)gbPuzzelBox.Controls[i]).Image = lstOrignalPicList[j];
                    if (j == 9) inNullSliceIndex = i;
                }
            }
            while (CheakWin());
        }

        private void btnShuffel_Click(object sender, EventArgs e)
        {
            DialogResult YesNo = new DialogResult();
            if (lbTimeLabs.Text != "00:00:00")
            {
                YesNo = MessageBox.Show("Are you sure to Restart ? ", "Pabiit Puzzel", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            }
            if (lbTimeLabs.Text == "00:00:00" || YesNo == DialogResult.Yes)
            {
                Shuffel();
                timer.Reset();
                lbTimeLabs.Text = "00:00:00";
                inMoves = 0;
                lbMovesMade.Text = "Moves Made : 0";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Shuffel();
        }

        private void SwitchPictureBox(object sender, EventArgs e)
        {
            if (lbTimeLabs.Text == "00:00:00")
            {
                timer.Start();
            }
            int inPictureBoxIndex = gbPuzzelBox.Controls.IndexOf(sender as Control);
            if (inNullSliceIndex != inPictureBoxIndex)
            {
                List<int> FourBrothers = new List<int>(new int[] { ((inPictureBoxIndex %3 ==0)? -1 : inPictureBoxIndex-1), 
                    inPictureBoxIndex - 3, ((inPictureBoxIndex%3==2)?-1:inPictureBoxIndex+1), inPictureBoxIndex + 3 });
                if (FourBrothers.Contains(inNullSliceIndex))
                {
                    ((PictureBox)gbPuzzelBox.Controls[inNullSliceIndex]).Image = ((PictureBox)gbPuzzelBox.Controls[inPictureBoxIndex]).Image;
                    ((PictureBox)gbPuzzelBox.Controls[inPictureBoxIndex]).Image = lstOrignalPicList[9];
                    inNullSliceIndex = inPictureBoxIndex;
                    lbMovesMade.Text = "Moves Made : " + (++inMoves);
                    if (CheakWin())
                    {
                        timer.Stop();
                        (gbPuzzelBox.Controls[8] as PictureBox).Image = lstOrignalPicList[8];
                        MessageBox.Show("Congratualtions...\nyour Rabit Is happy\nTime Elapsed : " + timer.Elapsed.ToString().Remove(8) +
                            "\nTotal moves : " + inMoves, "Rabbit Puzzel");
                        inMoves = 0;
                        lbMovesMade.Text = "Moves made : 0";
                        lbTimeLabs.Text = "00:00:00";
                        timer.Reset();
                        Shuffel();
                    }
                }
            }
        }

        private void timElapse_Tick(object sender, EventArgs e)
        {
            if (timer.Elapsed.ToString()!="00:00:00")
            {
                lbTimeLabs.Text = timer.Elapsed.ToString().Remove(8);
            }
            if (timer.Elapsed.ToString()=="00:00:00")
            {
                btnPause.Enabled = false;
            }
            else
            {
                btnPause.Enabled=true;
            }
            if (timer.Elapsed.ToString() == "1")
            {
                timer.Reset();
                lbTimeLabs.Text = "00:00:00";
                lbMovesMade.Text = "Movies made : 0";
                inMoves = 0;
                btnPause.Enabled = false;
                MessageBox.Show("Time is up \nTry again", "Rabbit Puzzel");
                Shuffel() ;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text=="Pause")
            {
                gbPuzzelBox.Visible = false;
                btnPause.Text = "Resuame";
                timer.Stop();
            }
            else
            {
                gbPuzzelBox.Visible = true;
                btnPause.Text = "Pause";
                timer.Start();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want exit ?","Exits",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Close();
            }
        }

        bool CheakWin()
        {
            int i;
            for (i = 0; i < 8; i++)
            {
                if ((gbPuzzelBox.Controls[i] as PictureBox).Image != lstOrignalPicList[i]) break;
            }
            if (i == 8) return true;
            else return false;
        }
    }
}
