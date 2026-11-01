namespace StudentReportDemo.Models
{
    /// <summary>Sinh viên dùng cho báo cáo.</summary>
    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public double Diem { get; set; }

        /// <summary>Chỉ đọc: "Đạt" nếu Diem >= 5, ngược lại "Không đạt".</summary>
        public string KetQua => Diem >= 5 ? "Đạt" : "Không đạt";
    }

    /// <summary>Một dòng thống kê theo lớp (kết quả của LINQ GroupBy).</summary>
    public class ThongKeLop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public int SoSV { get; set; }
        public int SoDat { get; set; }
        public int SoKhongDat { get; set; }
        public double DiemTB { get; set; }
        public double DiemCaoNhat { get; set; }
        public double DiemThapNhat { get; set; }
    }
}
