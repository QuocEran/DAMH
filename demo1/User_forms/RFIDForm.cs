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

using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.API.Native;
using Document = DevExpress.XtraRichEdit.API.Native.Document;


using DevExpress.XtraPrinting.Native;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.FileAttachments.Win;
using System.IO;

using demo1.TempModel;
using System.Threading;
using System.Runtime.InteropServices;

using System.Net;
using System.Net.Mail;

using System.Collections;
using demo1.User_forms;
using demo1.Controllers;

namespace demo1.User_forms
{
    public partial class RFIDForm : Form
    {
        ACIMAppEntities db = new ACIMAppEntities();
        List<RFID> rfids = new List<RFID>();
        MauIn currentMauIn = new MauIn(); //cần xác định mẫu in hiện tại để có price
        RFID currentRfid = new RFID();
        LichSuIn lichSuIn = new LichSuIn();
        UserInfoUI userInfoUI = new UserInfoUI();
        UserInfo userInfo = new UserInfo();
        string fileID;
        string fileNameDocx;
        string fileNamePDF;
        string fileNameTemp;
        string fileNameSave;
        string fileNameSavePDF;
        Image IdleRFID = Image.FromFile(@"TemplateImage\RFID\IdleRFID.png");
        Image OkRFID = Image.FromFile(@"TemplateImage\RFID\OkRFID.png");
        Image WrongRFID = Image.FromFile(@"TemplateImage\RFID\WrongRFID.png");

        SoundPlayer player = new SoundPlayer();

        //Constants
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_VER_POSITIVE = 0x00000004;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);
        public RFIDForm()
        {
            InitializeComponent();
            txbRFID.Focus();
        }
        public RFIDForm(string fileid, UserInfoUI user)
        {
            InitializeComponent();
            userInfoUI = user;
            fileID = fileid;
            SetFileName(fileID);
            FinalUpdate();
            currentMauIn = db.MauIns.Single<MauIn>(s => s.fileID == fileID); //take current Mẫu đơn
            btnPrint.Enabled = false;
            txbRFID.Text = null;

            //this.BringToFront();
            //this.TopMost = true;
            //this.Activate();
            //this.Focus();
        }
        private void SetFileName(string filename)
        {
            fileNameDocx = filename + @".docx";
            fileNamePDF = filename + @".pdf";
            fileNameTemp = @"TemplateFormWord\" + fileNameDocx;
            fileNameSave = @"SaveFile\" + fileNameDocx;
            fileNameSavePDF = @"SaveFile\" + fileNamePDF;
        }
        //public void StartForm()
        //{
        //    Application.Run(new SplashForm());
        //}

        private void checkRFID()
        {
            //currentRfid.price = 0;
            string rfidstring;
            rfidstring = txbRFID.Text;
            //Tìm đối tượng RFID đúng vs thẻ vừa quẹt
            try
            {
                currentRfid = db.RFIDs.Single<RFID>(s => s.rfid1 == rfidstring);
            }
            catch { }

            if (currentRfid == null)
                NullRFID();
            else if (currentRfid.price < float.Parse(currentMauIn.price) || currentRfid.price == null)
                BadRFID();
            else
                GoodRFID();
        }
        private void BadRFID()
        {
            player.SoundLocation = @"AudioRecord\Update\6.wav";
            player.Play();
            DialogResult lackMessageBox = MessageBox.Show("Tài khoản trong thẻ không đủ, vui lòng nạp thêm tiền hoặc dùng thẻ khác!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (lackMessageBox == DialogResult.OK)
            {
                panelImage.BackgroundImage = WrongRFID;
                txbAmount.Text = String.Format("{0:### ### ###.##}", currentRfid.price);
                txbRFID.Text = null;
                txbRFID.Focus();
            }
        }

        private void GoodRFID()
        {

            btnPrint.Enabled = true;
            panelImage.BackgroundImage = OkRFID;
            txbAmount.Text = String.Format("{0:### ### ###.##}", currentRfid.price);

            player.SoundLocation = @"AudioRecord\Update\7.wav";
            player.Play();
        }
        private void NullRFID()
        {
            player.SoundLocation = @"AudioRecord\5-4_WrongRFID.wav"; player.Play();
            DialogResult lackMessageBox = MessageBox.Show("Mã thẻ không tồn tại, vui lòng đăng ký thẻ hoặc đổi thẻ thanh toán khác!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (lackMessageBox == DialogResult.OK)
            {
                panelImage.BackgroundImage = WrongRFID;
                txbAmount.Text = "000";
                txbRFID.Text = null;
                txbRFID.Focus();
            }
        }
        private void Print()
        {
            //Thực hiện in
            using (RichEditDocumentServer srv = new RichEditDocumentServer())
            {
                if (srv.LoadDocument(fileNameTemp, DocumentFormat.OpenXml))
                {
                    Document doc = srv.Document;
                    //tenNguoiDung
                    DocumentRange[] ranges = doc.FindAll("<tenNguoiDung>", SearchOptions.None);
                    for (int i = 0; i < ranges.Length; i++) doc.Replace(ranges[i], userInfoUI.FullName);
                    //ngaySinh
                    DocumentRange[] range1s = doc.FindAll("<ngaySinh>", SearchOptions.None);
                    for (int i = 0; i < range1s.Length; i++) doc.Replace(range1s[i], userInfoUI.NgaySinh);
                    //gioiTinh
                    DocumentRange[] range2s = doc.FindAll("<gioiTinh>", SearchOptions.None);
                    for (int i = 0; i < range2s.Length; i++) doc.Replace(range2s[i], userInfoUI.GioiTinh);
                    //MSSV
                    DocumentRange[] range3s = doc.FindAll("<MSSV>", SearchOptions.None);
                    for (int i = 0; i < range3s.Length; i++) doc.Replace(range3s[i], userInfoUI.MSSV);
                    //Khóa
                    DocumentRange[] range31s = doc.FindAll("<khoas>", SearchOptions.None);
                    for (int i = 0; i < range31s.Length; i++) doc.Replace(range31s[i], userInfoUI.Khoas);
                    //khoa
                    DocumentRange[] range4s = doc.FindAll("<khoa>", SearchOptions.None);
                    for (int i = 0; i < range4s.Length; i++) doc.Replace(range4s[i], userInfoUI.Khoa);
                    //Bậc
                    DocumentRange[] range41s = doc.FindAll("<bac>", SearchOptions.None);
                    for (int i = 0; i < range41s.Length; i++) doc.Replace(range41s[i], userInfoUI.Bac);
                    //Hệ
                    DocumentRange[] range42s = doc.FindAll("<heDaoTao>", SearchOptions.None);
                    for (int i = 0; i < range42s.Length; i++) doc.Replace(range42s[i], userInfoUI.He);
                    //Lớp
                    DocumentRange[] range43s = doc.FindAll("<lop>", SearchOptions.None);
                    for (int i = 0; i < range43s.Length; i++) doc.Replace(range43s[i], userInfoUI.Lop);
                    //Học kì
                    DocumentRange[] range44s = doc.FindAll("<hk>", SearchOptions.None);
                    for (int i = 0; i < range44s.Length; i++) doc.Replace(range44s[i], userInfoUI.HK);
                    //SDT
                    DocumentRange[] range5s = doc.FindAll("<SDT>", SearchOptions.None);
                    for (int i = 0; i < range5s.Length; i++) doc.Replace(range5s[i], userInfoUI.SDT);
                    //email
                    DocumentRange[] range6s = doc.FindAll("<email>", SearchOptions.None);
                    for (int i = 0; i < range6s.Length; i++) doc.Replace(range6s[i], userInfoUI.Email);
                    //diaChi
                    DocumentRange[] range7s = doc.FindAll("<diaChi>", SearchOptions.None);
                    for (int i = 0; i < range7s.Length; i++) doc.Replace(range7s[i], userInfoUI.DiaChi);
                    //Hệ
                    DocumentRange[] range8s = doc.FindAll("<heDaoTao>", SearchOptions.None);
                    for (int i = 0; i < range8s.Length; i++) doc.Replace(range8s[i], userInfoUI.He);
                    //Ngay
                    DocumentRange[] range9s = doc.FindAll("<ngay>", SearchOptions.None);
                    for (int i = 0; i < range9s.Length; i++) doc.Replace(range9s[i], userInfoUI.Day);
                    //Tháng
                    DocumentRange[] range10s = doc.FindAll("<thang>", SearchOptions.None);
                    for (int i = 0; i < range10s.Length; i++) doc.Replace(range10s[i], userInfoUI.Month);
                    //Năm
                    DocumentRange[] range11s = doc.FindAll("<nam>", SearchOptions.None);
                    for (int i = 0; i < range11s.Length; i++) doc.Replace(range11s[i], userInfoUI.Year);
                }
                srv.SaveDocument(fileNameSave, DocumentFormat.OpenXml);
                //PrintingSettings printing= new PrintingSettings()
                srv.Print();

            }
            SendEmailToUser();
            UpdateModel();

        }
        private void UpdateModel()
        {
            currentRfid.price = currentRfid.price - float.Parse(currentMauIn.price);
            try
            {
                userInfo = db.UserInfoes.Single<UserInfo>(s => s.MSSV == currentRfid.UserInfo1.MSSV);
            }
            catch
            {
                userInfo.TenNguoiDung = "No name";
            }
            //cập nhật lịch sử in
            lichSuIn.UserInfo = userInfo;
            lichSuIn.MauIn1 = currentMauIn;
            lichSuIn.ngayCapNhat = DateTime.Now;


            currentMauIn.LichSuIns.Add(lichSuIn);
            db.SaveChanges();
        }
        private void SendEmailToUser()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("acimbku@gmail.com");

                mail.To.Add(currentRfid.UserInfo1.email);

                string userNameMail;
                if (userInfoUI.FullName == "")
                {
                    userNameMail = currentRfid.UserInfo1.TenNguoiDung;
                }
                else userNameMail = userInfoUI.FullName;

                mail.Subject = "Máy In Mẫu Đơn Tự Động thông báo kết quả in";

                string mailBody = "Chào bạn " + userNameMail + ",\n\nMáy In Mẫu Đơn Tự Động - ACIM xin chân thành cảm ơn bạn đã quan tâm và sử dụng dịch vụ. Hệ thống xin thông báo kết quả của giao dịch:\n\n1. Người dùng: " + userNameMail + "\n2. Mẫu đơn: " + currentMauIn.Ten + " - " + currentMauIn.fileID + "\n3. Thời gian: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") + "\n4. Hình thức thanh toán: THẺ SINH VIÊN\n5. Trạng thái: IN THÀNH CÔNG\n\nMột lần nữa, hệ thống Máy In Mẫu Đơn Tự Động – ACIM xin cảm ơn bạn đã sử dụng dịch vụ. Nếu có thắc mắc hoặc góp ý đối với hệ thống, xin vui lòng gửi email đến acimbku@gmail.com hoặc gọi đến số điện thoại (+84) 344 820 033.\n\nTrân trọng,\nNgười quản lý hệ thống";

                mail.Body = mailBody;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("acimbku@gmail.com", "acim2020");
                //SmtpServer.DeliveryMethod(false);

                SmtpServer.Send(mail);
            }
            catch { }
            try
            {
                MailMessage mail1 = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail1.From = new MailAddress("acimbku@gmail.com");
                mail1.To.Add("nguyenhoangan18.25.26@gmail.com");

                mail1.Subject = "[QUẢN LÝ GIAO DỊCH]x[" + DateTime.Today.ToString("dd/MM/yyyy") + "]";

                string mailBody = "Chào bạn Quản trị viên,\n\nMáy In Mẫu Đơn Tự Động - ACIM thông báo về giao dịch mới:\n\n1. Người dùng: " + userInfoUI.FullName + "\n2. Mẫu đơn: " + currentMauIn.Ten + " - " + currentMauIn.fileID + "\n3. Thời gian: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") + "\n4. Hình thức thanh toán: THẺ SINH VIÊN\n5. Trạng thái: IN THÀNH CÔNG\n\nTrân trọng,\nMáy ACIM";
                mail1.Body = mailBody;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("acimbku@gmail.com", "acim2020");
                SmtpServer.Send(mail1);
            }
            catch { }
        }
        //sửa thông tin người dùng
        private void FinalUpdate()
        {
            DateTime today = DateTime.Today;
            userInfoUI.FullName = userInfoUI.FullName == "" ? "" : userInfoUI.FullName;
            userInfoUI.GioiTinh = userInfoUI.GioiTinh == "" ? "" : userInfoUI.GioiTinh;
            userInfoUI.NgaySinh = userInfoUI.NgaySinh == "" ? "" : userInfoUI.NgaySinh;
            userInfoUI.MSSV = userInfoUI.MSSV == "" ? "" : userInfoUI.MSSV;
            userInfoUI.Khoa = userInfoUI.Khoa == "" ? "___" : userInfoUI.Khoa;
            userInfoUI.Khoas = userInfoUI.Khoas == "" ? "_____" : userInfoUI.Khoas;
            userInfoUI.Bac = userInfoUI.Bac == "" ? "" : userInfoUI.Bac;
            userInfoUI.He = userInfoUI.He == "" ? "" : userInfoUI.He;
            userInfoUI.Lop = userInfoUI.Lop == "" ? "" : userInfoUI.Lop;
            userInfoUI.HK = userInfoUI.HK == "" ? "___" : userInfoUI.HK;
            userInfoUI.SDT = userInfoUI.SDT == "" ? "" : userInfoUI.SDT;
            userInfoUI.Email = userInfoUI.Email == "" ? "" : userInfoUI.Email;
            userInfoUI.DiaChi = userInfoUI.DiaChi == "" ? "" : userInfoUI.DiaChi;
            userInfoUI.Day = userInfoUI.Day == "" ? "___" : userInfoUI.Day;
            userInfoUI.Month = userInfoUI.Month == "" ? "___" : userInfoUI.Month;
            userInfoUI.Year = userInfoUI.Year == "" ? "______" : userInfoUI.Year;
        }

        private void btnForwardPage_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbRFID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                checkRFID();
        }

        private void RFIDForm_Load(object sender, EventArgs e)
        {
            player.SoundLocation = @"AudioRecord\5-1_RFID.wav";
            player.Play();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            FinalCheckRFID finalCheck = new FinalCheckRFID(currentRfid);
            DialogResult dialogResult = finalCheck.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                player.SoundLocation = @"AudioRecord\Update\12.wav";
                player.Play();
                Print();
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            player.Stop();
            this.Close();
        }
        protected override void OnLoad(EventArgs e)
        {
            //Animate form
            AnimateWindow(this.Handle, 250, AW_BLEND | AW_VER_POSITIVE);
            player.SoundLocation = @"AudioRecord\Update\5.wav";
            player.Play();
        }

    }
}
