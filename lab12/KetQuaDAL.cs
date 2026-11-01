using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QLSinhVienLab12.DAL
{
    /// <summary>Truy cập database cho module kết quả học tập. Chỉ gọi stored procedure, luôn dùng parameter.</summary>
    public class KetQuaDAL
    {
        public DataTable GetLopList() => FillTable("sp_Lop_GetAll");

        public DataTable GetMonHocList() => FillTable("sp_MonHoc_GetAll");

        public DataTable GetByLopMon(string maLop, string maMon)
        {
            using (var conn = Database.CreateConnection())
            using (var cmd = new SqlCommand("sp_KetQua_GetByLopMon", conn) { CommandType = CommandType.StoredProcedure })
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@MaLop", SqlDbType.VarChar, 15).Value = maLop;
                cmd.Parameters.Add("@MaMon", SqlDbType.VarChar, 10).Value = maMon;
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Lưu tất cả dòng trong MỘT transaction. Có lỗi ở bất kỳ dòng nào thì rollback toàn bộ.
        /// Trả về số dòng đã lưu. Ném InvalidOperationException (kèm mã SV lỗi) nếu thất bại.
        /// </summary>
        public int SaveAll(DataTable table, string maMon)
        {
            int saved = 0;
            string currentMaSV = null;

            using (var conn = Database.CreateConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("sp_KetQua_Save", conn, tran) { CommandType = CommandType.StoredProcedure })
                        {
                            var pMaSV = cmd.Parameters.Add("@MaSV", SqlDbType.VarChar, 15);
                            var pMaMon = cmd.Parameters.Add("@MaMon", SqlDbType.VarChar, 10);
                            var pDiem = cmd.Parameters.Add(new SqlParameter("@Diem", SqlDbType.Decimal) { Precision = 4, Scale = 2 });
                            var pGhiChu = cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 200);

                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted) continue;

                                currentMaSV = Convert.ToString(row["MaSV"]);
                                pMaSV.Value = currentMaSV;
                                pMaMon.Value = maMon;
                                pDiem.Value = row["Diem"];   // DBNull -> stored procedure sẽ THROW lỗi
                                string gc = Convert.ToString(row["GhiChu"]);
                                pGhiChu.Value = string.IsNullOrWhiteSpace(gc) ? (object)DBNull.Value : gc.Trim();

                                cmd.ExecuteNonQuery();
                                saved++;
                            }
                        }
                        tran.Commit();
                        return saved;
                    }
                    catch (Exception ex)
                    {
                        try { tran.Rollback(); } catch { /* transaction có thể đã bị server hủy */ }
                        throw new InvalidOperationException(
                            $"Lỗi tại sinh viên {currentMaSV}: {ex.Message}", ex);
                    }
                }
            }
        }

        private static DataTable FillTable(string procName)
        {
            using (var conn = Database.CreateConnection())
            using (var cmd = new SqlCommand(procName, conn) { CommandType = CommandType.StoredProcedure })
            using (var da = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
