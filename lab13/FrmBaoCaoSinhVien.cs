using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StudentReportDemo.Models;

namespace StudentReportDemo.Forms
{
    public class FrmBaoCaoSinhVien : Form
    {
        private const string TatCaLop = "";
        private const string KqTatCa = "Tất cả", KqDat = "Đạt", KqKhongDat = "Không đạt";

        private List<SinhVien> _danhSach;                 // dữ liệu gốc
        private string _tieuDeBaoCao = "Báo cáo sinh viên"; // mô tả điều kiện của báo cáo đang hiển thị
        private string _tomTat = "";                      // dòng tổng kết, dùng khi xuất file

        private ComboBox cboLop, cboKetQua;
        private TextBox txtTuKhoa;
        private Button btnXemBaoCao, btnThongKe, btnXuatFile;
        private DataGridView dgvBaoCao;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tslThongKe;

        private class LopItem { public string MaLop { get; set; } public string TenLop { get; set; } }

        private static readonly Dictionary<string, string> TieuDeCot = new Dictionary<string, string>
        {
            ["MaSV"] = "Mã SV", ["HoTen"] = "Họ tên", ["MaLop"] = "Mã lớp", ["TenLop"] = "Tên lớp",
            ["Diem"] = "Điểm TB", ["KetQua"] = "Kết quả", ["SoSV"] = "Số SV", ["SoDat"] = "Đạt",
            ["SoKhongDat"] = "Không đạt", ["DiemTB"] = "Điểm TB", ["DiemCaoNhat"] = "Cao nhất", ["DiemThapNhat"] = "Thấp nhất"
        };

        public FrmBaoCaoSinhVien()
        {
            BuildUi();
            Load += (s, e) =>
            {
                _danhSach = TaoDuLieuMau();
                LoadComboBox();
                XemBaoCao(false);          // hiển thị toàn bộ khi mở form
            };
        }

        #region Giao diện
        private void BuildUi()
        {
            Text = "Báo cáo sinh viên bằng LINQ - COMP1019 Lab 13";
            ClientSize = new Size(960, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10f);

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 100 };
            cboLop = new ComboBox { Name = "cboLop", DropDownStyle = ComboBoxStyle.DropDownList, Left = 60, Top = 14, Width = 250 };
            cboKetQua = new ComboBox { Name = "cboKetQua", DropDownStyle = ComboBoxStyle.DropDownList, Left = 430, Top = 14, Width = 150 };
            txtTuKhoa = new TextBox { Name = "txtTuKhoa", Left = 140, Top = 58, Width = 240 };
            btnXemBaoCao = new Button { Name = "btnXemBaoCao", Text = "Xem báo cáo", Left = 600, Top = 10, Width = 140, Height = 34 };
            btnThongKe = new Button { Name = "btnThongKe", Text = "Thống kê", Left = 750, Top = 10, Width = 120, Height = 34 };
            btnXuatFile = new Button { Name = "btnXuatFile", Text = "Xuất file", Left = 600, Top = 52, Width = 140, Height = 34 };
            pnlTop.Controls.AddRange(new Control[]
            {
                new Label { Text = "Lớp:", Left = 15, Top = 18, Width = 45 }, cboLop,
                new Label { Text = "Kết quả:", Left = 350, Top = 18, Width = 75 }, cboKetQua,
                new Label { Text = "Từ khóa (mã/tên):", Left = 15, Top = 62, Width = 125 }, txtTuKhoa,
                btnXemBaoCao, btnThongKe, btnXuatFile
            });

            dgvBaoCao = new DataGridView
            {
                Name = "dgvBaoCao", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
                AllowUserToDeleteRows = false, RowHeadersVisible = false, BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AutoGenerateColumns = true
            };

            statusStrip = new StatusStrip();
            tslThongKe = new ToolStripStatusLabel();
            statusStrip.Items.Add(tslThongKe);

            Controls.Add(dgvBaoCao);
            Controls.Add(statusStrip);
            Controls.Add(pnlTop);

            btnXemBaoCao.Click += (s, e) => XemBaoCao(true);
            btnThongKe.Click += (s, e) => ThongKeTheoLop();
            btnXuatFile.Click += (s, e) => XuatFile();
            txtTuKhoa.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { XemBaoCao(true); e.SuppressKeyPress = true; } };
            dgvBaoCao.DataBindingComplete += (s, e) => DinhDangLuoi();
        }

        private void DinhDangLuoi()
        {
            foreach (DataGridViewColumn col in dgvBaoCao.Columns)
            {
                if (TieuDeCot.TryGetValue(col.DataPropertyName, out string title)) col.HeaderText = title;
                if (col.ValueType == typeof(double))
                {
                    col.DefaultCellStyle.Format = "0.00";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }
        #endregion

        #region Dữ liệu mẫu và ComboBox
        private static List<SinhVien> TaoDuLieuMau()
        {
            return new List<SinhVien>
            {
                new SinhVien { MaSV = "SV001", HoTen = "Nguyễn Văn An",   MaLop = "DH21CNTT01", TenLop = "CNTT 21-01",    Diem = 8.5 },
                new SinhVien { MaSV = "SV002", HoTen = "Trần Thị Bình",   MaLop = "DH21CNTT01", TenLop = "CNTT 21-01",    Diem = 7.0 },
                new SinhVien { MaSV = "SV003", HoTen = "Lê Hoàng Cường",  MaLop = "DH21CNTT01", TenLop = "CNTT 21-01",    Diem = 4.5 },
                new SinhVien { MaSV = "SV004", HoTen = "Phạm Thu Dung",   MaLop = "DH21CNTT01", TenLop = "CNTT 21-01",    Diem = 9.2 },
                new SinhVien { MaSV = "SV005", HoTen = "Hoàng Minh Đức",  MaLop = "DH21CNTT01", TenLop = "CNTT 21-01",    Diem = 3.8 },
                new SinhVien { MaSV = "SV006", HoTen = "Vũ Ngọc Hà",      MaLop = "DH22CNTT01", TenLop = "CNTT 22-01",    Diem = 6.5 },
                new SinhVien { MaSV = "SV007", HoTen = "Đỗ Quang Huy",    MaLop = "DH22CNTT01", TenLop = "CNTT 22-01",    Diem = 5.0 },
                new SinhVien { MaSV = "SV008", HoTen = "Bùi Thanh Lan",   MaLop = "DH22CNTT01", TenLop = "CNTT 22-01",    Diem = 4.9 },
                new SinhVien { MaSV = "SV009", HoTen = "Ngô Gia Khánh",   MaLop = "DH22CNTT01", TenLop = "CNTT 22-01",    Diem = 7.8 },
                new SinhVien { MaSV = "SV010", HoTen = "Đặng Mỹ Linh",    MaLop = "DH22KT01",   TenLop = "Kinh tế 22-01", Diem = 8.0 },
                new SinhVien { MaSV = "SV011", HoTen = "Phan Văn Minh",   MaLop = "DH22KT01",   TenLop = "Kinh tế 22-01", Diem = 2.5 },
                new SinhVien { MaSV = "SV012", HoTen = "Lý Thị Ngọc",     MaLop = "DH22KT01",   TenLop = "Kinh tế 22-01", Diem = 6.0 },
                new SinhVien { MaSV = "SV013", HoTen = "Trương Quốc Phát",MaLop = "DH22KT01",   TenLop = "Kinh tế 22-01", Diem = 5.5 },
                new SinhVien { MaSV = "SV014", HoTen = "Huỳnh Anh Quân",  MaLop = "DH22KT01",   TenLop = "Kinh tế 22-01", Diem = 9.5 },
                new SinhVien { MaSV = "SV015", HoTen = "Võ Thị Sương",    MaLop = "DH22KT01",   TenLop = "Kinh tế 22-01", Diem = 4.0 }
            };
        }

        private void LoadComboBox()
        {
            var dsLop = _danhSach
                .GroupBy(s => s.MaLop)
                .Select(g => new LopItem { MaLop = g.Key, TenLop = g.First().TenLop })
                .OrderBy(l => l.TenLop)
                .ToList();
            dsLop.Insert(0, new LopItem { MaLop = TatCaLop, TenLop = "Tất cả lớp" });

            cboLop.DataSource = dsLop;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";

            cboKetQua.Items.AddRange(new object[] { KqTatCa, KqDat, KqKhongDat });
            cboKetQua.SelectedIndex = 0;
        }
        #endregion

        #region Xem báo cáo (LINQ: Where, OrderByDescending)
        private void XemBaoCao(bool baoKhiRong)
        {
            string maLop = Convert.ToString(cboLop.SelectedValue) ?? TatCaLop;
            string ketQua = cboKetQua.SelectedItem as string ?? KqTatCa;
            string tuKhoa = txtTuKhoa.Text.Trim();

            var ketQuaLoc = LocSinhVien(_danhSach, maLop, ketQua, tuKhoa)
                .OrderByDescending(s => s.Diem)
                .ThenBy(s => s.HoTen)
                .ToList();

            dgvBaoCao.DataSource = ketQuaLoc;
            _tieuDeBaoCao = $"Danh sách sinh viên | Lớp: {cboLop.Text} | Kết quả: {ketQua}" +
                            (tuKhoa.Length > 0 ? $" | Từ khóa: \"{tuKhoa}\"" : "");
            CapNhatThongKe(ketQuaLoc);

            if (ketQuaLoc.Count == 0 && baoKhiRong)
                MessageBox.Show("Không có sinh viên nào phù hợp với điều kiện đã chọn.\nHãy thử đổi lớp, kết quả hoặc từ khóa.",
                                "Không có dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>Lọc theo lớp, kết quả và từ khóa (bỏ qua hoa/thường và dấu tiếng Việt).</summary>
        private static IEnumerable<SinhVien> LocSinhVien(IEnumerable<SinhVien> nguon, string maLop, string ketQua, string tuKhoa)
        {
            IEnumerable<SinhVien> q = nguon;

            if (maLop != TatCaLop) q = q.Where(s => s.MaLop == maLop);
            if (ketQua != KqTatCa) q = q.Where(s => s.KetQua == ketQua);
            if (tuKhoa.Length > 0)
            {
                string kw = ChuanHoa(tuKhoa);
                q = q.Where(s => ChuanHoa(s.MaSV).Contains(kw) || ChuanHoa(s.HoTen).Contains(kw));
            }
            return q;
        }

        private static string ChuanHoa(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark) sb.Append(c);
            return sb.ToString().Replace('đ', 'd').Replace('Đ', 'D').ToLowerInvariant();
        }
        #endregion

        #region Thống kê (LINQ: Count, Average, GroupBy)
        private void CapNhatThongKe(IReadOnlyCollection<SinhVien> ds)
        {
            int tong = ds.Count;
            int dat = ds.Count(s => s.KetQua == KqDat);
            int khongDat = ds.Count(s => s.KetQua == KqKhongDat);
            double tb = ds.Any() ? ds.Average(s => s.Diem) : 0;

            _tomTat = $"Tổng: {tong} | Đạt: {dat} | Không đạt: {khongDat} | Điểm TB: {tb:0.00}";
            tslThongKe.Text = _tomTat;
        }

        private void ThongKeTheoLop()
        {
            var thongKe = _danhSach
                .GroupBy(s => new { s.MaLop, s.TenLop })
                .Select(g => new ThongKeLop
                {
                    MaLop = g.Key.MaLop,
                    TenLop = g.Key.TenLop,
                    SoSV = g.Count(),
                    SoDat = g.Count(s => s.KetQua == KqDat),
                    SoKhongDat = g.Count(s => s.KetQua == KqKhongDat),
                    DiemTB = Math.Round(g.Average(s => s.Diem), 2),
                    DiemCaoNhat = g.Max(s => s.Diem),
                    DiemThapNhat = g.Min(s => s.Diem)
                })
                .OrderBy(t => t.MaLop)
                .ToList();

            dgvBaoCao.DataSource = thongKe;
            _tieuDeBaoCao = "Thống kê theo lớp (toàn bộ sinh viên)";
            CapNhatThongKe(_danhSach);
            _tomTat = "Toàn trường - " + _tomTat;
            tslThongKe.Text = _tomTat;
        }
        #endregion

        #region Xuất file (CSV / TXT)
        private void XuatFile()
        {
            if (dgvBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất. Hãy bấm Xem báo cáo hoặc Thống kê trước.",
                                "Chưa có dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Title = "Xuất báo cáo",
                Filter = "CSV (*.csv)|*.csv|Văn bản (*.txt)|*.txt",
                DefaultExt = "csv",
                AddExtension = true,
                FileName = "BaoCaoSinhVien_" + DateTime.Now.ToString("yyyyMMdd_HHmm")
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    LayDuLieuDangHienThi(out string[] tieuDe, out List<string[]> dong);
                    bool laTxt = string.Equals(Path.GetExtension(sfd.FileName), ".txt", StringComparison.OrdinalIgnoreCase);
                    string noiDung = laTxt ? TaoNoiDungTxt(tieuDe, dong) : TaoNoiDungCsv(tieuDe, dong);

                    // UTF-8 có BOM để Excel/Notepad đọc đúng tiếng Việt
                    File.WriteAllText(sfd.FileName, noiDung, new UTF8Encoding(true));
                    MessageBox.Show($"Đã xuất {dong.Count} dòng ra file:\n{sfd.FileName}", "Xuất file thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Không có quyền ghi vào vị trí này. Hãy chọn thư mục khác.", "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Không ghi được file (có thể file đang mở trong Excel hoặc ổ đĩa đầy).\n" + ex.Message,
                                    "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>Lấy đúng các cột/dòng đang hiển thị trên lưới (báo cáo hoặc thống kê).</summary>
        private void LayDuLieuDangHienThi(out string[] tieuDe, out List<string[]> dong)
        {
            var cot = dgvBaoCao.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToList();
            tieuDe = cot.Select(c => c.HeaderText).ToArray();
            dong = dgvBaoCao.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Select(r => cot.Select(c => DinhDangO(r.Cells[c.Index].Value)).ToArray())
                .ToList();
        }

        private static string DinhDangO(object v)
        {
            if (v is double d) return d.ToString("0.00", CultureInfo.InvariantCulture);   // luôn dùng dấu chấm
            return Convert.ToString(v, CultureInfo.InvariantCulture) ?? "";
        }

        private static string TaoNoiDungCsv(string[] tieuDe, List<string[]> dong)
        {
            return string.Join(Environment.NewLine,
                       new[] { tieuDe }.Concat(dong).Select(r => string.Join(",", r.Select(CsvEscape))))
                   + Environment.NewLine;
        }

        private static string CsvEscape(string s) =>
            s.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0 ? "\"" + s.Replace("\"", "\"\"") + "\"" : s;

        private string TaoNoiDungTxt(string[] tieuDe, List<string[]> dong)
        {
            int[] rong = tieuDe.Select((h, i) => Math.Max(h.Length, dong.Max(r => r[i].Length))).ToArray();
            Func<string[], string> inDong = r => string.Join(" | ", r.Select((c, i) => c.PadRight(rong[i])));

            var sb = new StringBuilder();
            sb.AppendLine("BÁO CÁO SINH VIÊN");
            sb.AppendLine("Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("Điều kiện: " + _tieuDeBaoCao);
            sb.AppendLine(_tomTat);
            sb.AppendLine(new string('-', rong.Sum() + 3 * (rong.Length - 1)));
            sb.AppendLine(inDong(tieuDe));
            sb.AppendLine(new string('-', rong.Sum() + 3 * (rong.Length - 1)));
            foreach (var r in dong) sb.AppendLine(inDong(r));
            return sb.ToString();
        }
        #endregion
    }
}
