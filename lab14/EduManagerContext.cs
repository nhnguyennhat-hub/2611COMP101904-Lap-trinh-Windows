using EduManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Data
{
    /// <summary>DbContext của ứng dụng EduManager.</summary>
    public class EduManagerContext : DbContext
    {
        // Dùng SQL Express thì đổi Server thành .\SQLEXPRESS
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=EduManagerDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<MonHoc> MonHocs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonHoc>(e =>
            {
                e.ToTable("MONHOC");
                e.HasKey(x => x.MaMonHoc);
                e.Property(x => x.MaMonHoc).HasMaxLength(10).IsRequired();
                e.Property(x => x.TenMonHoc).HasMaxLength(100).IsRequired();
                e.Property(x => x.SoTinChi).IsRequired();
                e.Property(x => x.TrangThai).IsRequired();
            });
        }
    }
}
