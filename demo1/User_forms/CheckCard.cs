using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo1.User_forms
{
    public partial class CheckCard : Form
    {
        ACIMAppEntities db = new ACIMAppEntities();
        List<RFID> rfids = new List<RFID>();


        SoundPlayer player = new SoundPlayer();

        //Constants

        const int AW_SLIDE = 0X40000;

        const int AW_HOR_POSITIVE = 0X1;

        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_VER_POSITIVE = 0x00000004;

        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        public CheckCard()
        {
            InitializeComponent();
            txbRFID.Focus();
        }
        private void checkRFID()
        {
            //currentRfid.price = 0;
            string rfidstring;
            rfidstring = txbRFID.Text;
            RFID currentRfid = new RFID();
            //Tìm đối tượng RFID đúng vs thẻ vừa quẹt
            try
            {
                currentRfid = db.RFIDs.Single<RFID>(s => s.rfid1 == rfidstring);
            }
            catch { }

            if (currentRfid.price == null)
            {
                lbMoney.Text = "Bạn chưa đăng ký tài khoản...";
                lbMoney.ForeColor = Color.Red;
                SystemSounds.Asterisk.Play();
            }
            else
            {
                lbMoney.Text = String.Format("{0:### ### ###.##}" + " VND", currentRfid.price);
                lbMoney.ForeColor = Color.Gold;
            }
            txbRFID.Text = "";
            txbRFID.Focus();
        }


        protected override void OnLoad(EventArgs e)
        {
            //Animate form
            AnimateWindow(this.Handle, 250, AW_BLEND | AW_VER_POSITIVE);
            player.SoundLocation = @"AudioRecord\5-1_RFID.wav";
            player.Play();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbRFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                checkRFID();
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
