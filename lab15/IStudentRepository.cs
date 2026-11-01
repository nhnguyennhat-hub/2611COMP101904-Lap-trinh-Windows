using System.Collections.Generic;
using StudentMvp.Models;

namespace StudentMvp.Repositories
{
    /// <summary>Nơi lưu/truy xuất dữ liệu. Có thể thay bằng SQL Server mà không đổi Presenter.</summary>
    public interface IStudentRepository
    {
        IReadOnlyList<Student> GetAll();
        IReadOnlyList<Student> Search(string keyword);
        Student GetById(string maSV);
        bool Exists(string maSV);
        void Add(Student student);
        bool Delete(string maSV);
    }
}
