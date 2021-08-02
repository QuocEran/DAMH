using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using demo1.TempModel;
using System.Media;
using System.Runtime.InteropServices;

namespace demo1.User_forms
{
    public partial class FinalCheckRFID : Form
    {
        RFID currentRfid = new RFID();
        SoundPlayer player = new SoundPlayer();
        string currentMSSV;
        string defaultTextbox = "######";
        //Constants
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_VER_POSITIVE = 0x00000004;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        public FinalCheckRFID()
        {
            InitializeComponent();
        }

        public FinalCheckRFID(RFID rFID)
        {
            InitializeComponent();
            currentRfid = rFID;
            this.BringToFront();
            this.TopMost = true;
        }

        private void txbFreeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PrefixCode();
                CheckMSSV();
            }
        }
        private void PrefixCode()
        {
            currentMSSV = txbFreeCode.Text;
            currentMSSV = currentMSSV.Trim();
            currentMSSV = currentMSSV.Replace(" ", "");
            currentMSSV = currentMSSV.ToLower();
        }
        private void CheckMSSV()
        {
            if (currentMSSV == currentRfid.UserInfo1.MSSV)
            {
                this.Close();
                DialogResult = DialogResult.OK;
            }
            else
            {
                player.SoundLocation = @"AudioRecord\Update\555.wav";
                player.Play();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrefixCode();
            CheckMSSV();
        }
        protected override void OnLoad(EventArgs e)
        {
            //Animate form
            AnimateWindow(this.Handle, 250, AW_BLEND | AW_VER_POSITIVE);
            player.SoundLocation = @"AudioRecord\Update\55.wav";
            player.Play();
        }

        private void txbFreeCode_Enter(object sender, EventArgs e)
        {
            txbFreeCode.Text = txbFreeCode.Text == defaultTextbox ? "" : txbFreeCode.Text;
            this.Location = new Point(this.Location.X, this.Location.Y - 60);
        }

        private void txbFreeCode_Leave(object sender, EventArgs e)
        {
            txbFreeCode.Text = txbFreeCode.Text == "" ? defaultTextbox : txbFreeCode.Text;
            this.Location = new Point(this.Location.X, this.Location.Y + 60);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
  
