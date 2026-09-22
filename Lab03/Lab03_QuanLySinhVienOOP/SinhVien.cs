using System;

namespace Lab03_QuanLySinhVienOOP
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }

        private double diemTrungBinh;

        public double DiemTrungBinh
        {
            get
            {
                return diemTrungBinh;
            }
            set
            {
                if (value >= 0 && value <= 10)
                    diemTrungBinh = value;
                else
                    throw new ArgumentException("Điểm phải nằm trong khoảng 0 đến 10.");
            }
        }

        public string MaLop { get; set; }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh,
                        string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8)
                return "Giỏi";
            else if (DiemTrungBinh >= 6.5)
                return "Khá";
            else if (DiemTrungBinh >= 5)
                return "Trung bình";
            else
                return "Yếu";
        }

        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien} | Họ tên: {HoTen} | " +
                   $"Ngày sinh: {NgaySinh:dd/MM/yyyy} | Lớp: {MaLop} | " +
                   $"Điểm: {DiemTrungBinh:F2} | Xếp loại: {XepLoai()}";
        }
    }
}
