using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo1.TempModel
{
    public class UserInfoUI
    {
        public UserInfoUI() { }
        public string FullName { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string MSSV { get; set; }
        public string Khoas { get; set; }
        public string Khoa { get; set; }
        public string Bac { get; set; }
        public string He { get; set; }
        public string Lop { get; set; }
        public string HK { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public void ClearInfo()
        {
            this.FullName = "";
            this.NgaySinh = "";
            this.GioiTinh = "";
            this.MSSV = "";
            this.Khoas = "";
            this.Khoa = "";
            this.Bac = "";
            this.He = "";
            this.Lop = "";
            this.HK = "";
            this.SDT = "";
            this.Email = "";
            this.DiaChi = "";
            this.Day = "";
            this.Month = "";
            this.Year = "";
        }
    }
}
