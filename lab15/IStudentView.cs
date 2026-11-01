using System.Collections.Generic;
using StudentMvp.Models;

namespace StudentMvp.Views
{
    /// <summary>
    /// Hợp đồng giữa Presenter và giao diện. Presenter chỉ biết interface này,
    /// không biết TextBox, DataGridView hay MessageBox.
    /// Dữ liệu nhập được để dạng chuỗi: việc kiểm tra/đổi kiểu là nhiệm vụ của Presenter.
    /// </summary>
    public interface IStudentView
    {
        // Dữ liệu nhập
        string MaSV { get; set; }
        string HoTen { get; set; }
        string Diem { get; set; }
        string TuKhoa { get; }

        // Hiển thị
        void ShowStudents(IReadOnlyList<Student> students);
        void SetStatus(string text);
        void ClearInputs();

        // Thông báo
        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);
    }
}
