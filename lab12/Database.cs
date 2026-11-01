using Microsoft.Data.SqlClient;

namespace QLSinhVienLab12.DAL
{
    /// <summary>Cấu hình kết nối tập trung một chỗ.</summary>
    public static class Database
    {
        // Dùng SQL Express thì đổi thành: .\SQLEXPRESS
        public const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=QLSinhVienLab12;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection CreateConnection() => new SqlConnection(ConnectionString);
    }
}
