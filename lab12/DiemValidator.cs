using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace QLSinhVienLab12.Helpers
{
    /// <summary>Kiểm tra điểm / ghi chú. Không phụ thuộc giao diện nên dễ kiểm thử.</summary>
    public static class DiemValidator
    {
        public const int GhiChuMaxLength = 200;

        /// <summary>Chấp nhận cả "7.5" và "7,5" (không phụ thuộc culture của Windows).</summary>
        public static bool TryParseDiem(string text, out decimal diem, out string error)
        {
            diem = 0; error = null;
            text = (text ?? "").Trim();

            if (text.Length == 0) { error = "Điểm không được để trống."; return false; }

            text = text.Replace(',', '.');
            if (!decimal.TryParse(text, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                                  CultureInfo.InvariantCulture, out diem))
            { error = "Điểm phải là số."; return false; }

            if (diem < 0 || diem > 10) { error = "Điểm phải nằm trong khoảng từ 0 đến 10."; return false; }

            diem = Math.Round(diem, 2);
            return true;
        }

        /// <summary>Kiểm tra toàn bộ bảng trước khi lưu. Trả về danh sách lỗi (rỗng = hợp lệ).</summary>
        public static List<string> ValidateRows(DataTable table)
        {
            var errors = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                row.RowError = "";

                string who = $"{row["MaSV"]} - {row["HoTen"]}";
                string err = null;

                if (row["Diem"] == DBNull.Value) err = "Điểm không được để trống.";
                else
                {
                    decimal d = Convert.ToDecimal(row["Diem"]);
                    if (d < 0 || d > 10) err = "Điểm phải nằm trong khoảng từ 0 đến 10.";
                }
                if (err == null && Convert.ToString(row["GhiChu"]).Length > GhiChuMaxLength)
                    err = $"Ghi chú tối đa {GhiChuMaxLength} ký tự.";

                if (err != null) { row.RowError = err; errors.Add($"{who}: {err}"); }
            }
            return errors;
        }
    }
}
