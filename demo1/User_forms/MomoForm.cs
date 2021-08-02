using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Media;
using System.Runtime.InteropServices;

namespace demo1.User_forms
{
    public partial class MomoForm : Form
    {
        Color color = new Color();
        SoundPlayer player = new SoundPlayer();

        //Constants
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_VER_POSITIVE = 0x00000004;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);
        public MomoForm()
        {
            InitializeComponent();
            color = panel1.BackColor;
        }
        private void MomoForm_Load(object sender, EventArgs e)
        {
            player.SoundLocation = @"AudioRecord\Update\11.wav";
            player.Play();
        }

        protected override void OnLoad(EventArgs e)
        {
            //Animate form  
            AnimateWindow(this.Handle, 250, AW_BLEND | AW_VER_POSITIVE);
            player.SoundLocation = @"AudioRecord\Update\11.wav";
            player.Play();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            player.Stop();
            this.Close();
        }
    }
}
