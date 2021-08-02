using demo1.TempModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace demo1.User_forms
{
    public partial class UpdateInfor : Form
    {
        ACIMAppEntities db = new ACIMAppEntities();
        List<MauIn> mauIns = new List<MauIn>();
        public string fileID { get; set; }
        public UserInfoUI userInfoUI = new UserInfoUI();
        public UpdateInfor()
        {
            InitializeComponent();
            //mauIns = db.MauIns.ToList();
            //fileID = mauIns[1].fileID;
            DateTime date = DateTime.Now;
            txb_hoten.Text = "TRAN QUOC CHUONG";
            txb_ngaysinh.Text = "19/06/2000";
            txb_gioitinh.Text = "Nam";
            txb_MSSV.Text = "1810058";
            txb_khoas.Text = "K18";
            txb_khoa.Text = "Điện - Điện tử";
            txb_bac.Text = "Đại học";
            txb_he.Text= "Chính Quy";
            txb_lop.Text = "DD18DV1";
            txb_hk.Text= "202";
            txb_sdt.Text = "0379734090";
            txb_mail.Text = "chuong.tranquoc@hcmut.edu.vn";
            txb_diachi.Text = "Số 10, Đường 16, Phường 15, Quận 11, TP.HCM";
            txb_ngay.Text = Convert.ToString(date.Day);
            txb_thang.Text = Convert.ToString(date.Month);
            txb_nam.Text = Convert.ToString(date.Year);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            update_infor();
            mauIns = db.MauIns.ToList();
            if (rb1.Checked)
            {
                fileID = mauIns[0].fileID;
            }
            else if (rb2.Checked)
            {
                fileID = mauIns[1].fileID;
            }
            else if (rb3.Checked)
            {
                fileID = mauIns[2].fileID;
            }
            else if (rb4.Checked)
            {
                fileID = mauIns[3].fileID;
            }
            else if (rb5.Checked)
            {
                fileID = mauIns[4].fileID;
            }
            else if (rb6.Checked)
            {
                fileID = mauIns[5].fileID;
            }
        }

        public void update_infor()
        {
            userInfoUI.FullName = txb_hoten.Text;
            userInfoUI.NgaySinh = txb_ngaysinh.Text;
            userInfoUI.GioiTinh = txb_gioitinh.Text;
            userInfoUI.MSSV =     txb_MSSV.Text;
            userInfoUI.Khoas =    txb_khoas.Text;
            userInfoUI.Khoa =     txb_khoa.Text;
            userInfoUI.Bac =      txb_bac.Text;
            userInfoUI.He =       txb_he.Text;
            userInfoUI.Lop =      txb_lop.Text;
            userInfoUI.HK =       txb_hk.Text;
            userInfoUI.SDT =      txb_sdt.Text;
            userInfoUI.Email =    txb_mail.Text;
            userInfoUI.DiaChi =   txb_diachi.Text;
            userInfoUI.Day =      txb_ngay.Text;
            userInfoUI.Month =    txb_thang.Text;
            userInfoUI.Year =     txb_nam.Text;
        }
    }
}
