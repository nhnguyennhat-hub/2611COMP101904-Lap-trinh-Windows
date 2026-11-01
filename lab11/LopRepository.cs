using System.Data;
using Microsoft.Data.SqlClient;

namespace ClassManagerApp
{
    /// <summary>Toàn bộ truy cập SQL Server cho bảng LOP. Mọi câu lệnh dùng tham số (chống SQL Injection).</summary>
    public class LopRepository
    {
        private const string SelectSql = @"
            SELECT l.MaLop, l.TenLop, l.KhoaHoc, l.MaKhoa, k.TenKhoa,
                   (SELECT COUNT(*) FROM SINHVIEN sv WHERE sv.MaLop = l.MaLop) AS SoSV
            FROM LOP l
            JOIN KHOA k ON l.MaKhoa = k.MaKhoa";

        private static SqlConnection Open()
        {
            var conn = new SqlConnection(DbConfig.ConnectionString);
            conn.Open();
            return conn;
        }

        public DataTable GetAll()
        {
            using var conn = Open();
            using var da = new SqlDataAdapter(SelectSql + " ORDER BY l.MaLop", conn);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable Search(string keyword)
        {
            using var conn = Open();
            using var da = new SqlDataAdapter(
                SelectSql + " WHERE l.MaLop LIKE @kw OR l.TenLop LIKE @kw ORDER BY l.MaLop", conn);
            da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetKhoaList()
        {
            using var conn = Open();
            using var da = new SqlDataAdapter("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa", conn);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool Exists(string maLop)
        {
            using var conn = Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM LOP WHERE MaLop = @MaLop", conn);
            cmd.Parameters.AddWithValue("@MaLop", maLop);
            return (int)cmd.ExecuteScalar() > 0;
        }

        public int CountStudents(string maLop)
        {
            using var conn = Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM SINHVIEN WHERE MaLop = @MaLop", conn);
            cmd.Parameters.AddWithValue("@MaLop", maLop);
            return (int)cmd.ExecuteScalar();
        }

        public int Insert(string maLop, string tenLop, string khoaHoc, string maKhoa)
        {
            using var conn = Open();
            using var cmd = new SqlCommand(
                "INSERT INTO LOP (MaLop, TenLop, KhoaHoc, MaKhoa) VALUES (@MaLop, @TenLop, @KhoaHoc, @MaKhoa)", conn);
            AddParams(cmd, maLop, tenLop, khoaHoc, maKhoa);
            return cmd.ExecuteNonQuery();
        }

        public int Update(string maLop, string tenLop, string khoaHoc, string maKhoa)
        {
            using var conn = Open();
            using var cmd = new SqlCommand(
                "UPDATE LOP SET TenLop = @TenLop, KhoaHoc = @KhoaHoc, MaKhoa = @MaKhoa WHERE MaLop = @MaLop", conn);
            AddParams(cmd, maLop, tenLop, khoaHoc, maKhoa);
            return cmd.ExecuteNonQuery();
        }

        public int Delete(string maLop)
        {
            using var conn = Open();
            using var cmd = new SqlCommand("DELETE FROM LOP WHERE MaLop = @MaLop", conn);
            cmd.Parameters.AddWithValue("@MaLop", maLop);
            return cmd.ExecuteNonQuery();
        }

        private static void AddParams(SqlCommand cmd, string maLop, string tenLop, string khoaHoc, string maKhoa)
        {
            cmd.Parameters.Add("@MaLop", SqlDbType.VarChar, 15).Value = maLop;
            cmd.Parameters.Add("@TenLop", SqlDbType.NVarChar, 100).Value = tenLop;
            cmd.Parameters.Add("@KhoaHoc", SqlDbType.VarChar, 10).Value =
                string.IsNullOrWhiteSpace(khoaHoc) ? (object)System.DBNull.Value : khoaHoc;
            cmd.Parameters.Add("@MaKhoa", SqlDbType.VarChar, 10).Value = maKhoa;
        }
    }
}
