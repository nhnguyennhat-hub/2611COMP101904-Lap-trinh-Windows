namespace EduManager.Models
{
    /// <summary>Entity ánh xạ bảng MONHOC.</summary>
    public class MonHoc
    {
        public string MaMonHoc { get; set; }     // PK, nvarchar(10)
        public string TenMonHoc { get; set; }    // NOT NULL, nvarchar(100)
        public int SoTinChi { get; set; }        // CHECK 1..6 (kiểm tra ở BLL và ở database)

        // Mặc định true ở phía C#. Cố ý KHÔNG cấu hình HasDefaultValue(true) trong EF:
        // với kiểu bool, EF sẽ bỏ qua giá trị false khi INSERT và database tự điền 1.
        public bool TrangThai { get; set; } = true;
    }
}
