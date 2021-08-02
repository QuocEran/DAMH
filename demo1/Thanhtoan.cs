using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using demo1.User_forms;
using demo1.Controllers;
using demo1.TempModel;
namespace demo1
{
    public partial class Thanhtoan : Form
    {
        ACIMAppEntities db = new ACIMAppEntities();
        SoundPlayer player = new SoundPlayer();
        List<MauIn> mauIns = new List<MauIn>();
        string fileID;
        Thread tSound = new Thread(new ThreadStart(AlwaysForm));
        Thread fisrt = new Thread(new ThreadStart(A1));
        UserInfoUI userInfoUI = new UserInfoUI();
        public Thanhtoan()
        {
            InitializeComponent();
            ////KHOI TAO CHON MAU DON
            //mauIns = db.MauIns.ToList();
            //fileID = mauIns[1].fileID;
            ////KHOI TAO UserINFO
            //userInfoUI.FullName = "TRAN QUOC CHUONG";
            //userInfoUI.NgaySinh = "19/06/2000";
            //userInfoUI.GioiTinh = "Nam";
            //userInfoUI.MSSV = "1810058";
            //userInfoUI.Khoas = "K18";
            //userInfoUI.Khoa = "Điện - Điện tử";
            //userInfoUI.Bac = "Đại học";
            //userInfoUI.He = "Chính Quy";
            //userInfoUI.Lop = "DD18DV1";
            //userInfoUI.HK = "202";
            //userInfoUI.SDT = "0379734090";
            //userInfoUI.Email = "chuong.tranquoc@hcmut.edu.vn";
            //userInfoUI.DiaChi = "Số 10, Đường 16, Phường 15, Quận 11, TP.HCM";
            //userInfoUI.Day = "01";
            //userInfoUI.Month = "06";
            //userInfoUI.Year = "2021";
            //Chay nhac nen
            tSound.IsBackground = true;
            tSound.Start();
            fisrt.IsBackground = true;
            fisrt.Start();
        }

        public static void A1()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"AudioRecord\Update\4.wav";
            player.Play();
        }
        public static void AlwaysForm()
        {
            Thread.Sleep(16000);
            Application.Run(new SoundEffect(@"AudioRecord\Update\PromoPre.wav"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.SoundLocation = @"AudioRecord\Update\5.wav";
            player.Play();
            CheckCard checkCard = new CheckCard();
            DialogResult dialogResult = checkCard.ShowDialog();
            if (dialogResult == DialogResult.Cancel)
            {
                //this.Close();
            }
            //tSound.Abort();
            player.SoundLocation = @"AudioRecord\Update\PromoPre.wav";
            player.PlayLooping();
        }

        private void RFID_click(object sender, EventArgs e)
        {
            if (fileID == null)
            {
                DialogResult lack_fileID = MessageBox.Show("Vui lòng điền thông tin người dùng");
            }
            else
            {
                RFIDForm rFIDForm = new RFIDForm(fileID, userInfoUI);
                DialogResult dialogResult = rFIDForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    //this.Close();
                    //DialogResult = DialogResult.OK;
                }
            }
            player.SoundLocation = @"AudioRecord\Update\PromoPre.wav";
            player.PlayLooping();
        }

        private void DiscountCode_Click(object sender, EventArgs e)
        {
            if (fileID == null)
            {
                DialogResult lack_fileID = MessageBox.Show("Vui lòng điền thông tin người dùng");
            }
            else
            {
                DiscountForm discountForm = new DiscountForm(fileID, userInfoUI);
                DialogResult dialogResult = discountForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    //this.Close();
                    //DialogResult = DialogResult.OK;
                }
            }
            player.SoundLocation = @"AudioRecord\Update\PromoPre.wav";
            player.PlayLooping();
        }

        private void Momo_click(object sender, EventArgs e)
        {
            MomoForm momoform = new MomoForm();
            DialogResult dialogResult = momoform.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateInfor_Click(object sender, EventArgs e)
        {
            UpdateInfor update_form = new UpdateInfor();
            DialogResult dialogResult = update_form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                fileID = update_form.fileID;
                userInfoUI = update_form.userInfoUI;
            }
            
            player.SoundLocation = @"AudioRecord\Update\PromoPre.wav";
            player.PlayLooping();
        }
    }
}
