namespace ClassManagerApp
{
    /// <summary>Kiểm tra dữ liệu nhập. Tách riêng, không phụ thuộc giao diện nên dễ kiểm thử.</summary>
    public static class LopValidator
    {
        /// <summary>Trả về thông báo lỗi, hoặc null nếu hợp lệ.</summary>
        public static string Validate(string maLop, string tenLop, string khoaHoc, object maKhoa)
        {
            maLop = (maLop ?? "").Trim();
            tenLop = (tenLop ?? "").Trim();
            khoaHoc = (khoaHoc ?? "").Trim();

            if (maLop.Length == 0) return "Mã lớp không được để trống.";
            if (maLop.Length > 15) return "Mã lớp tối đa 15 ký tự.";
            if (maLop.Contains(" ")) return "Mã lớp không được chứa khoảng trắng.";
            if (tenLop.Length == 0) return "Tên lớp không được để trống.";
            if (tenLop.Length > 100) return "Tên lớp tối đa 100 ký tự.";
            if (khoaHoc.Length > 10) return "Khóa học tối đa 10 ký tự.";
            if (maKhoa == null) return "Vui lòng chọn khoa.";
            return null;
        }
    }
}
