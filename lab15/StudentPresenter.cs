using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using StudentMvp.Models;
using StudentMvp.Repositories;
using StudentMvp.Views;

namespace StudentMvp.Presenters
{
    /// <summary>
    /// Presenter: toàn bộ logic của màn hình. Chỉ làm việc với IStudentView và IStudentRepository,
    /// KHÔNG tham chiếu System.Windows.Forms => có thể kiểm thử bằng một View giả.
    /// </summary>
    public class StudentPresenter
    {
        private readonly IStudentView _view;
        private readonly IStudentRepository _repository;

        public StudentPresenter(IStudentView view, IStudentRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Hiển thị toàn bộ danh sách.</summary>
        public void LoadStudents() => Run(() => HienThi(_repository.GetAll(), "Đã tải danh sách"));

        /// <summary>Thêm sinh viên từ dữ liệu người dùng đang nhập.</summary>
        public void AddStudent() => Run(() =>
        {
            if (!TryBuildStudent(out Student student, out string error)) { _view.ShowError(error); return; }

            if (_repository.Exists(student.MaSV))
            {
                _view.ShowError($"Mã sinh viên '{student.MaSV}' đã tồn tại. Vui lòng nhập mã khác.");
                return;
            }

            _repository.Add(student);
            _view.ShowInfo($"Đã thêm sinh viên {student.MaSV} - {student.HoTen}.");
            _view.ClearInputs();
            LoadStudents();
        });

        /// <summary>Xóa sinh viên theo mã đang nhập (hoặc đã chọn trên lưới).</summary>
        public void DeleteStudent() => Run(() =>
        {
            string ma = (_view.MaSV ?? "").Trim();
            if (ma.Length == 0) { _view.ShowError("Vui lòng nhập hoặc chọn mã sinh viên cần xóa."); return; }

            var student = _repository.GetById(ma);
            if (student == null) { _view.ShowError($"Không tìm thấy sinh viên có mã '{ma}'."); return; }

            if (!_view.Confirm($"Bạn có chắc muốn xóa sinh viên {student.MaSV} - {student.HoTen}?")) return;

            _repository.Delete(student.MaSV);
            _view.ShowInfo($"Đã xóa sinh viên {student.MaSV}.");
            _view.ClearInputs();
            LoadStudents();
        });

        /// <summary>Tìm theo mã hoặc tên. Từ khóa rỗng = hiện tất cả.</summary>
        public void SearchStudents() => Run(() =>
        {
            string kw = (_view.TuKhoa ?? "").Trim();
            if (kw.Length == 0) { LoadStudents(); return; }

            var ketQua = _repository.Search(kw);
            HienThi(ketQua, $"Tìm '{kw}'");
            if (ketQua.Count == 0) _view.ShowInfo($"Không tìm thấy sinh viên nào phù hợp với '{kw}'.");
        });

        /// <summary>Người dùng chọn một dòng trên lưới: đổ dữ liệu lên ô nhập.</summary>
        public void SelectStudent(string maSV) => Run(() =>
        {
            var s = _repository.GetById(maSV);
            if (s == null) return;
            _view.MaSV = s.MaSV;
            _view.HoTen = s.HoTen;
            _view.Diem = s.Diem.ToString("0.##", CultureInfo.InvariantCulture);
        });

        /// <summary>Làm mới: xóa ô nhập và tải lại toàn bộ.</summary>
        public void Reset()
        {
            _view.ClearInputs();
            LoadStudents();
        }

        // ---------- Hàm hỗ trợ ----------

        private void HienThi(IReadOnlyList<Student> ds, string prefix)
        {
            _view.ShowStudents(ds);
            _view.SetStatus($"{prefix}: {ds.Count} sinh viên.");
        }

        /// <summary>Kiểm tra rỗng, sai kiểu, ngoài khoảng. Trả về false kèm thông báo lỗi.</summary>
        private bool TryBuildStudent(out Student student, out string error)
        {
            student = null;
            string ma = (_view.MaSV ?? "").Trim();
            string ten = (_view.HoTen ?? "").Trim();

            if (ma.Length == 0) { error = "Mã sinh viên không được để trống."; return false; }
            if (ma.Length > 15) { error = "Mã sinh viên tối đa 15 ký tự."; return false; }
            if (ma.Any(char.IsWhiteSpace)) { error = "Mã sinh viên không được chứa khoảng trắng."; return false; }
            if (ten.Length == 0) { error = "Họ tên không được để trống."; return false; }
            if (ten.Length > 100) { error = "Họ tên tối đa 100 ký tự."; return false; }
            if (!TryParseDiem(_view.Diem, out double diem, out error)) return false;

            student = new Student { MaSV = ma, HoTen = ten, Diem = diem };
            return true;
        }

        /// <summary>Chấp nhận "7.5" và "7,5". Từ chối rỗng, chữ, NaN/Infinity và điểm ngoài 0..10.</summary>
        public static bool TryParseDiem(string text, out double diem, out string error)
        {
            diem = 0; error = null;
            text = (text ?? "").Trim();

            if (text.Length == 0) { error = "Điểm không được để trống."; return false; }

            if (!double.TryParse(text.Replace(',', '.'), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                                 CultureInfo.InvariantCulture, out diem) || double.IsNaN(diem) || double.IsInfinity(diem))
            { error = "Điểm phải là số (ví dụ 7.5)."; return false; }

            if (diem < 0 || diem > 10) { error = "Điểm phải nằm trong khoảng từ 0 đến 10."; return false; }
            diem = Math.Round(diem, 2);
            return true;
        }

        /// <summary>Bắt lỗi bất ngờ (ví dụ khi đổi sang repository dùng SQL Server) để View không bị crash.</summary>
        private void Run(Action action)
        {
            try { action(); }
            catch (Exception ex) { _view.ShowError("Đã xảy ra lỗi: " + ex.Message); }
        }
    }
}
