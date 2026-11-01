namespace ClassManagerApp
{
    /// <summary>Chuỗi kết nối tập trung một chỗ. Đổi Server nếu bạn dùng SQL Express: .\SQLEXPRESS</summary>
    public static class DbConfig
    {
        public const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=EduManagerDB;Integrated Security=True;TrustServerCertificate=True;";
    }
}
