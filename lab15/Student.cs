namespace StudentMvp.Models
{
    /// <summary>Model: chỉ chứa dữ liệu, không biết gì về giao diện hay nơi lưu trữ.</summary>
    public class Student
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public double Diem { get; set; }

        /// <summary>Chỉ đọc: xếp loại theo điểm.</summary>
        public string XepLoai =>
            Diem >= 8 ? "Giỏi" :
            Diem >= 6.5 ? "Khá" :
            Diem >= 5 ? "Trung bình" : "Yếu";
    }
}
