using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using StudentMvp.Models;

namespace StudentMvp.Repositories
{
    /// <summary>Repository lưu trong bộ nhớ (List&lt;Student&gt;), có sẵn dữ liệu mẫu để chạy là thấy ngay.</summary>
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new List<Student>
        {
            new Student { MaSV = "SV001", HoTen = "Nguyễn Văn An",    Diem = 8.5 },
            new Student { MaSV = "SV002", HoTen = "Trần Thị Bình",    Diem = 7.0 },
            new Student { MaSV = "SV003", HoTen = "Lê Hoàng Cường",   Diem = 4.5 },
            new Student { MaSV = "SV004", HoTen = "Phạm Thu Dung",    Diem = 9.2 },
            new Student { MaSV = "SV005", HoTen = "Hoàng Minh Đức",   Diem = 5.0 },
            new Student { MaSV = "SV006", HoTen = "Vũ Ngọc Hà",       Diem = 6.5 },
            new Student { MaSV = "SV007", HoTen = "Đỗ Quang Huy",     Diem = 3.8 },
            new Student { MaSV = "SV008", HoTen = "Bùi Thanh Lan",    Diem = 7.8 }
        };

        public IReadOnlyList<Student> GetAll() => _students.OrderBy(s => s.MaSV).ToList();

        /// <summary>Tìm theo mã hoặc họ tên, không phân biệt hoa thường và dấu tiếng Việt.</summary>
        public IReadOnlyList<Student> Search(string keyword)
        {
            string kw = Normalize(keyword);
            return _students
                .Where(s => Normalize(s.MaSV).Contains(kw) || Normalize(s.HoTen).Contains(kw))
                .OrderBy(s => s.MaSV)
                .ToList();
        }

        public Student GetById(string maSV) =>
            _students.FirstOrDefault(s => string.Equals(s.MaSV, maSV, StringComparison.OrdinalIgnoreCase));

        public bool Exists(string maSV) => GetById(maSV) != null;

        public void Add(Student student) => _students.Add(student);

        public bool Delete(string maSV)
        {
            var s = GetById(maSV);
            return s != null && _students.Remove(s);
        }

        private static string Normalize(string text)
        {
            var sb = new StringBuilder();
            foreach (char c in (text ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark) sb.Append(c);
            return sb.ToString().Replace('đ', 'd').Replace('Đ', 'D').ToLowerInvariant();
        }
    }
}
