using System.Collections.Generic;
using System.Linq;
using EduManager.Data;
using EduManager.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EduManager.DAL
{
    /// <summary>
    /// Tầng truy cập dữ liệu: chỉ làm việc với database qua EF Core.
    /// Không kiểm tra nghiệp vụ, không biết gì về giao diện.
    /// Mỗi thao tác dùng một DbContext ngắn hạn (using) để tránh giữ kết nối và cache cũ.
    /// </summary>
    public class MonHocDAL
    {
        public List<MonHoc> GetAll()
        {
            using (var db = new EduManagerContext())
                return db.MonHocs.AsNoTracking().OrderBy(m => m.MaMonHoc).ToList();
        }

        /// <summary>Tìm theo mã hoặc tên (EF dịch Contains thành LIKE, tự escape ký tự đặc biệt).</summary>
        public List<MonHoc> Search(string keyword)
        {
            using (var db = new EduManagerContext())
                return db.MonHocs.AsNoTracking()
                         .Where(m => m.MaMonHoc.Contains(keyword) || m.TenMonHoc.Contains(keyword))
                         .OrderBy(m => m.MaMonHoc)
                         .ToList();
        }

        public bool Exists(string maMonHoc)
        {
            using (var db = new EduManagerContext())
                return db.MonHocs.Any(m => m.MaMonHoc == maMonHoc);
        }

        public void Add(MonHoc monHoc)
        {
            using (var db = new EduManagerContext())
            {
                db.MonHocs.Add(monHoc);
                db.SaveChanges();
            }
        }

        /// <summary>Trả về false nếu không tìm thấy môn học cần sửa.</summary>
        public bool Update(MonHoc monHoc)
        {
            using (var db = new EduManagerContext())
            {
                var entity = db.MonHocs.Find(monHoc.MaMonHoc);
                if (entity == null) return false;

                entity.TenMonHoc = monHoc.TenMonHoc;
                entity.SoTinChi = monHoc.SoTinChi;
                entity.TrangThai = monHoc.TrangThai;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>Trả về false nếu không tìm thấy môn học cần xóa.</summary>
        public bool Delete(string maMonHoc)
        {
            using (var db = new EduManagerContext())
            {
                var entity = db.MonHocs.Find(maMonHoc);
                if (entity == null) return false;

                db.MonHocs.Remove(entity);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>Lỗi khóa ngoại (547): môn học đang được bảng khác tham chiếu.</summary>
        public static bool IsReferenceConflict(DbUpdateException ex) =>
            ex.InnerException is SqlException sql && sql.Number == 547;

        /// <summary>Lỗi trùng khóa chính/unique (2627, 2601).</summary>
        public static bool IsDuplicateKey(DbUpdateException ex) =>
            ex.InnerException is SqlException sql && (sql.Number == 2627 || sql.Number == 2601);
    }
}
