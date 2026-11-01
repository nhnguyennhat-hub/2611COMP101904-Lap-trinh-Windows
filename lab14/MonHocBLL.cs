using System;
using System.Collections.Generic;
using System.Linq;
using EduManager.DAL;
using EduManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EduManager.BLL
{
    /// <summary>Kết quả một thao tác nghiệp vụ: thành công hay thất bại + thông báo cho người dùng.</summary>
    public class OperationResult
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public static OperationResult Ok(string message) => new OperationResult { Success = true, Message = message };
        public static OperationResult Fail(string message) => new OperationResult { Success = false, Message = message };
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; private set; }

        public static OperationResult<T> Ok(T data) => new OperationResult<T> { Success = true, Data = data };
        public new static OperationResult<T> Fail(string message) => new OperationResult<T> { Success = false, Message = message };
    }

    /// <summary>
    /// Tầng nghiệp vụ: kiểm tra dữ liệu, kiểm tra quy tắc (trùng mã...), rồi mới gọi DAL.
    /// Dịch mọi lỗi kỹ thuật thành thông báo dễ hiểu; GUI chỉ việc hiển thị.
    /// </summary>
    public class MonHocBLL
    {
        public const int TinChiToiThieu = 1, TinChiToiDa = 6;
        private const int MaToiDa = 10, TenToiDa = 100;

        private readonly MonHocDAL _dal = new MonHocDAL();

        public OperationResult<List<MonHoc>> LayDanhSach(string tuKhoa = null)
        {
            try
            {
                tuKhoa = (tuKhoa ?? "").Trim();
                var ds = tuKhoa.Length == 0 ? _dal.GetAll() : _dal.Search(tuKhoa);
                return OperationResult<List<MonHoc>>.Ok(ds);
            }
            catch (Exception ex)
            {
                return OperationResult<List<MonHoc>>.Fail(LoiKetNoi(ex));
            }
        }

        public OperationResult Them(MonHoc monHoc)
        {
            string loi = KiemTra(monHoc);
            if (loi != null) return OperationResult.Fail(loi);
            ChuanHoa(monHoc);

            try
            {
                if (_dal.Exists(monHoc.MaMonHoc))
                    return OperationResult.Fail($"Mã môn học '{monHoc.MaMonHoc}' đã tồn tại. Vui lòng nhập mã khác.");

                _dal.Add(monHoc);
                return OperationResult.Ok("Thêm môn học thành công.");
            }
            catch (DbUpdateException ex) when (MonHocDAL.IsDuplicateKey(ex))
            {
                return OperationResult.Fail($"Mã môn học '{monHoc.MaMonHoc}' đã tồn tại.");
            }
            catch (Exception ex) { return OperationResult.Fail(LoiKetNoi(ex)); }
        }

        public OperationResult Sua(MonHoc monHoc)
        {
            string loi = KiemTra(monHoc);
            if (loi != null) return OperationResult.Fail(loi);
            ChuanHoa(monHoc);

            try
            {
                return _dal.Update(monHoc)
                    ? OperationResult.Ok("Cập nhật môn học thành công.")
                    : OperationResult.Fail("Không tìm thấy môn học cần sửa (có thể đã bị xóa). Hãy bấm Làm mới.");
            }
            catch (Exception ex) { return OperationResult.Fail(LoiKetNoi(ex)); }
        }

        public OperationResult Xoa(string maMonHoc)
        {
            if (string.IsNullOrWhiteSpace(maMonHoc))
                return OperationResult.Fail("Vui lòng chọn môn học cần xóa.");

            try
            {
                return _dal.Delete(maMonHoc.Trim())
                    ? OperationResult.Ok("Đã xóa môn học.")
                    : OperationResult.Fail("Không tìm thấy môn học cần xóa (có thể đã bị xóa trước đó).");
            }
            catch (DbUpdateException ex) when (MonHocDAL.IsReferenceConflict(ex))
            {
                return OperationResult.Fail("Không thể xóa vì môn học này đang được sử dụng ở dữ liệu khác (ví dụ kết quả học tập).");
            }
            catch (Exception ex) { return OperationResult.Fail(LoiKetNoi(ex)); }
        }

        /// <summary>Quy tắc kiểm tra dữ liệu. Trả về thông báo lỗi, hoặc null nếu hợp lệ.</summary>
        public string KiemTra(MonHoc monHoc)
        {
            if (monHoc == null) return "Dữ liệu môn học không hợp lệ.";

            string ma = (monHoc.MaMonHoc ?? "").Trim();
            string ten = (monHoc.TenMonHoc ?? "").Trim();

            if (ma.Length == 0) return "Mã môn học không được để trống.";
            if (ma.Length > MaToiDa) return $"Mã môn học tối đa {MaToiDa} ký tự.";
            if (ma.Any(char.IsWhiteSpace)) return "Mã môn học không được chứa khoảng trắng.";
            if (ten.Length == 0) return "Tên môn học không được để trống.";
            if (ten.Length > TenToiDa) return $"Tên môn học tối đa {TenToiDa} ký tự.";
            if (monHoc.SoTinChi < TinChiToiThieu || monHoc.SoTinChi > TinChiToiDa)
                return $"Số tín chỉ phải nằm trong khoảng từ {TinChiToiThieu} đến {TinChiToiDa}.";
            return null;
        }

        private static void ChuanHoa(MonHoc m)
        {
            m.MaMonHoc = m.MaMonHoc.Trim();
            m.TenMonHoc = m.TenMonHoc.Trim();
        }

        private static string LoiKetNoi(Exception ex) =>
            "Không thể thao tác với cơ sở dữ liệu. Hãy kiểm tra SQL Server đang chạy và đã chạy script tạo bảng.\nChi tiết: "
            + ex.GetBaseException().Message;
    }
}
