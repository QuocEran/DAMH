using demo1.TempModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.API.Native;

using DevExpress.XtraPrinting.Native;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.FileAttachments.Win;
using System.IO;

using Document = DevExpress.XtraRichEdit.API.Native.Document;
using System.Runtime.InteropServices;
using System.Net.Mail;


namespace demo1.User_forms
{
    public partial class DiscountForm : Form
    {
        ACIMAppEntities db = new ACIMAppEntities();
        MauIn currentMauIn = new MauIn();
        DiscountCode discount = new DiscountCode();
        UserInfo userInfo = new UserInfo();
        LichSuIn lichSuIn = new LichSuIn();
        UserInfoUI userInfoUI = new UserInfoUI();

        SoundPlayer player = new SoundPlayer(); //Biến âm thanh

        string fileID;
        string fileNameDocx;
        string fileNamePDF;
        string fileNameTemp;
        string fileNameSave;
        string fileNameSavePDF;

        string defaultTextbox = "XXX-XXX-XX";
        string currentCode;

        //Constants
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_VER_POSITIVE = 0x00000004;
        const int AW_BLEND = 0X80000;

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        public DiscountForm()
        {
            InitializeComponent();
        }
        //Khởi tạo bằng UserInfoUI và fileid
        public DiscountForm(string fileid, UserInfoUI user)
        {
            InitializeComponent();
            fileID = fileid;
            userInfoUI = user;
            SetFileName(fileID);
            FinalUpdate();
            currentMauIn = db.MauIns.Single<MauIn>(s => s.fileID == fileID);
            txbFreeCode.Text = defaultTextbox;
            btnPrint.Enabled = false;
        }
        // set file name
        private void SetFileName(string filename)
        {
            fileNameDocx = filename + @".docx";
            fileNamePDF = filename + @".pdf";
            fileNameTemp = @"TemplateFormWord\" + fileNameDocx;
            fileNameSave = @"SaveFile\" + fileNameDocx;
            fileNameSavePDF = @"SaveFile\" + fileNamePDF;
        }
        // check discount code
        private void PrefixCode()
        {
            currentCode = txbFreeCode.Text;
            currentCode = currentCode.Trim();
            currentCode = currentCode.Replace(" ", "");
            currentCode = currentCode.ToLower();
        }
        private void CheckDiscountCode()
        {
            int discountNumberUsing = -1;
            try
            {
                discount = db.DiscountCodes.Single(p => p.DiscountCode1 == currentCode);
                if (discount != null && discount.numberUsing == null)
                {
                    discount.numberUsing = 0;
                }
                //discountNumberUsing = discount.numberUsing.Value;
            }
            catch { }

            if (discount == null || discount.Used == true || discount.numberUsing == null)
                BadCode();
            else if (discount.numberUsing.Value < 3)
                GoodCode();
            else
            {
                discount.Used = true;
                db.SaveChanges();
                BadCode();
            }
        }
        // good code
        private void GoodCode()
        {
            player.SoundLocation = @"AudioRecord\Update\10.wav"; player.Play();
            btnPrint.Enabled = true;
        }
        // bad code
        private void BadCode()
        {
            player.SoundLocation = @"AudioRecord\Update\9.wav"; player.Play();

            DialogResult lackMessageBox = MessageBox.Show("Mã không hợp lệ. Mời bạn nhập mã giảm giá mới!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PrintForm()
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
                SendEmailToUser();
                UpdateModel();
            }
        }

        private void UpdateModel()
        {
            discount.numberUsing++;
            try
            {
                userInfo = db.UserInfoes.Single<UserInfo>(s => s.TenNguoiDung == userInfoUI.FullName);
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
                mail.To.Add(userInfo.email);

                mail.Subject = "Máy In Mẫu Đơn Tự Động thông báo kết quả in";

                string mailBody = "Chào bạn " + userInfoUI.FullName + ",\n\nMáy In Mẫu Đơn Tự Động - ACIM xin chân thành cảm ơn bạn đã quan tâm và sử dụng dịch vụ. Hệ thống xin thông báo kết quả của giao dịch:\n\n1. Người dùng: " + userInfoUI.FullName + "\n2. Mẫu đơn: " + currentMauIn.Ten + " - " + currentMauIn.fileID + "\n3. Thời gian: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") + "\n4. Hình thức thanh toán: MÃ KHUYẾN MÃI\n5. Trạng thái: IN THÀNH CÔNG\n\nMột lần nữa, hệ thống Máy In Mẫu Đơn Tự Động – ACIM xin cảm ơn bạn đã sử dụng dịch vụ. Nếu có thắc mắc hoặc góp ý đối với hệ thống, xin vui lòng gửi email đến acimbku@gmail.com hoặc gọi đến số điện thoại (+84) 344 820 033.\n\nTrân trọng,\nNgười quản lý hệ thống";
                mail.Body = mailBody;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("acimbku@gmail.com", "acim2020");
                SmtpServer.Send(mail);
            }
            catch { }

            try
            {
                MailMessage mail1 = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail1.From = new MailAddress("acimbku@gmail.com");
                mail1.To.Add("nguyenhoangan18.25.26@gmail.com");

                mail1.Subject = "[QUẢN LÝ GIAO DỊCH ACIM " + DateTime.Today.ToString("dd/MM/yyyy") + "]";

                string mailBody = "Chào bạn Quản trị viên,\n\nMáy In Mẫu Đơn Tự Động - ACIM thông báo về giao dịch mới:\n\n1. Người dùng: " + userInfoUI.FullName + "\n2. Mẫu đơn: " + currentMauIn.Ten + " - " + currentMauIn.fileID + "\n3. Thời gian: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") + "\n4. Hình thức thanh toán: MÃ KHUYẾN MÃI\n5. Mã thẻ: " + currentCode + "\n\nTrân trọng,\nMáy ACIM";
                mail1.Body = mailBody;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("acimbku@gmail.com", "acim2020");
                SmtpServer.Send(mail1);
            }
            catch { }

        }
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

        // các event 

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DiscountForm_Load(object sender, EventArgs e)
        {
            //player.SoundLocation = @"AudioRecord\5-5_FreeCodeEnter.wav";
            //player.Play();
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

        private void btnApplyCode_Click(object sender, EventArgs e)
        {
            PrefixCode();
            CheckDiscountCode();
        }

        private void txbFreeCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txbFreeCode_KeyDown(object sender, KeyEventArgs e)
        {
            //txbFreeCode.Text = txbFreeCode.Text.Length == 4 ? txbFreeCode.Text + " " : txbFreeCode.Text;
            //txbFreeCode.Text = txbFreeCode.Text.Length == 4 ? txbFreeCode.Text + " " : txbFreeCode.Text;
            if (e.KeyCode == Keys.Enter)
            {
                PrefixCode();
                CheckDiscountCode();
            }

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintForm();
            this.Close();
            DialogResult = DialogResult.OK;
        }
        protected override void OnLoad(EventArgs e)
        {
            //Animate form
            AnimateWindow(this.Handle, 250, AW_BLEND | AW_VER_POSITIVE);
            player.SoundLocation = @"AudioRecord\Update\8.wav";
            player.Play();
        }

  
    }
}


