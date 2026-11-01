using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLSinhVienLab12.DAL;
using QLSinhVienLab12.Helpers;

namespace QLSinhVienLab12.Forms
{
    public class FrmNhapDiemTheoLop : Form
    {
        private readonly KetQuaDAL _dal = new KetQuaDAL();
        private readonly BindingSource _bsKetQua = new BindingSource();

        private ComboBox cboLop, cboMonHoc;
        private Button btnTaiDanhSach, btnLuuTatCa;
        private TextBox txtLoc;
        private DataGridView dgvKetQua;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tslTrangThai;

        // Lớp/môn của danh sách đang hiển thị (người dùng có thể đổi ComboBox sau khi tải)
        private string _maLopDangTai, _maMonDangTai, _tenLopMon;

        public FrmNhapDiemTheoLop()
        {
            BuildUi();
            Load += FrmNhapDiemTheoLop_Load;
        }

        #region Giao diện
        private void BuildUi()
        {
            Text = "Nhập điểm theo lớp - COMP1019 Lab 12";
            ClientSize = new Size(880, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10f);

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 100 };
            cboLop = new ComboBox { Name = "cboLop", DropDownStyle = ComboBoxStyle.DropDownList, Left = 70, Top = 14, Width = 260 };
            cboMonHoc = new ComboBox { Name = "cboMonHoc", DropDownStyle = ComboBoxStyle.DropDownList, Left = 410, Top = 14, Width = 260 };
            btnTaiDanhSach = new Button { Name = "btnTaiDanhSach", Text = "Tải danh sách", Left = 700, Top = 10, Width = 150, Height = 34 };
            txtLoc = new TextBox { Name = "txtLoc", Left = 150, Top = 62, Width = 260 };
            pnlTop.Controls.AddRange(new Control[]
            {
                new Label { Text = "Lớp:", Left = 15, Top = 18, Width = 50 }, cboLop,
                new Label { Text = "Môn học:", Left = 340, Top = 18, Width = 70 }, cboMonHoc,
                btnTaiDanhSach,
                new Label { Text = "Lọc theo họ tên:", Left = 15, Top = 66, Width = 130 }, txtLoc
            });

            dgvKetQua = new DataGridView
            {
                Name = "dgvKetQua", Dock = DockStyle.Fill, AutoGenerateColumns = false,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect, BackgroundColor = Color.White,
                RowHeadersVisible = true, EditMode = DataGridViewEditMode.EditOnEnter
            };
            dgvKetQua.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSV", HeaderText = "Mã SV", Name = "colMaSV", ReadOnly = true, Width = 110 });
            dgvKetQua.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ tên", Name = "colHoTen", ReadOnly = true, Width = 260 });
            var colDiem = new DataGridViewTextBoxColumn { DataPropertyName = "Diem", HeaderText = "Điểm (0-10)", Name = "colDiem", Width = 110 };
            colDiem.DefaultCellStyle.Format = "0.##";
            colDiem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvKetQua.Columns.Add(colDiem);
            dgvKetQua.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GhiChu", HeaderText = "Ghi chú", Name = "colGhiChu", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MaxInputLength = DiemValidator.GhiChuMaxLength });
            dgvKetQua.DataSource = _bsKetQua;

            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 56 };
            btnLuuTatCa = new Button { Name = "btnLuuTatCa", Text = "Lưu tất cả", Width = 150, Height = 36, Top = 10, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnLuuTatCa.Left = pnlBottom.Width - 170;
            pnlBottom.Controls.Add(btnLuuTatCa);

            statusStrip = new StatusStrip();
            tslTrangThai = new ToolStripStatusLabel("Sẵn sàng. Chọn lớp, môn học rồi bấm Tải danh sách.");
            statusStrip.Items.Add(tslTrangThai);

            Controls.Add(dgvKetQua);
            Controls.Add(pnlBottom);
            Controls.Add(statusStrip);
            Controls.Add(pnlTop);

            btnTaiDanhSach.Click += btnTaiDanhSach_Click;
            btnLuuTatCa.Click += btnLuuTatCa_Click;
            txtLoc.TextChanged += txtLoc_TextChanged;
            dgvKetQua.CellValidating += dgvKetQua_CellValidating;
            dgvKetQua.CellParsing += dgvKetQua_CellParsing;
            dgvKetQua.CellEndEdit += (s, e) => dgvKetQua.Rows[e.RowIndex].ErrorText = "";
            dgvKetQua.DataError += (s, e) => { e.ThrowException = false; SetStatus("Giá trị nhập không hợp lệ.", true); };
        }
        #endregion

        #region Tải dữ liệu / Binding
        private void FrmNhapDiemTheoLop_Load(object sender, EventArgs e)
        {
            try
            {
                cboLop.DataSource = _dal.GetLopList();
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";
                cboLop.SelectedIndex = -1;

                cboMonHoc.DataSource = _dal.GetMonHocList();
                cboMonHoc.DisplayMember = "TenMon";
                cboMonHoc.ValueMember = "MaMon";
                cboMonHoc.SelectedIndex = -1;

                SetStatus("Đã tải danh sách lớp và môn học.");
            }
            catch (Exception ex) { ShowDbError(ex); }
        }

        private void btnTaiDanhSach_Click(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue == null || cboMonHoc.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp và môn học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (HasUnsavedChanges() &&
                MessageBox.Show("Có điểm đã nhập nhưng chưa lưu. Tải lại sẽ mất các thay đổi này. Tiếp tục?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            LoadDanhSach((string)cboLop.SelectedValue, (string)cboMonHoc.SelectedValue,
                         $"{cboLop.Text} - {cboMonHoc.Text}");
        }

        private void LoadDanhSach(string maLop, string maMon, string tenLopMon)
        {
            try
            {
                var dt = _dal.GetByLopMon(maLop, maMon);
                _maLopDangTai = maLop; _maMonDangTai = maMon; _tenLopMon = tenLopMon;
                txtLoc.Clear();
                _bsKetQua.Filter = null;
                _bsKetQua.DataSource = dt;        // DataTable -> BindingSource -> DataGridView
                CapNhatThongKe();
            }
            catch (Exception ex) { ShowDbError(ex); }
        }

        private bool HasUnsavedChanges()
        {
            dgvKetQua.EndEdit();
            return _bsKetQua.DataSource is DataTable dt && dt.GetChanges() != null;
        }

        private void CapNhatThongKe()
        {
            if (!(_bsKetQua.DataSource is DataTable dt)) return;
            int total = dt.Rows.Count;
            int daCoDiem = dt.AsEnumerable().Count(r => r["Diem"] != DBNull.Value);
            object avg = dt.Compute("AVG(Diem)", "");
            string tb = avg == DBNull.Value ? "chưa có" : Convert.ToDecimal(avg).ToString("0.00");
            SetStatus($"{_tenLopMon}: {total} sinh viên, {daCoDiem} đã có điểm, điểm trung bình {tb}.");
        }
        #endregion

        #region Lọc (mở rộng)
        private void txtLoc_TextChanged(object sender, EventArgs e)
        {
            if (!(_bsKetQua.DataSource is DataTable)) return;
            string kw = txtLoc.Text.Trim();
            _bsKetQua.Filter = kw.Length == 0 ? null : $"HoTen LIKE '%{EscapeFilter(kw)}%'";
        }

        /// <summary>Escape ký tự đặc biệt của biểu thức DataView.RowFilter.</summary>
        private static string EscapeFilter(string s) =>
            s.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]");
        #endregion

        #region Validation khi nhập
        private void dgvKetQua_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvKetQua.Columns[e.ColumnIndex].Name != "colDiem") return;
            string text = Convert.ToString(e.FormattedValue).Trim();
            if (text.Length == 0) return;        // cho phép tạm để trống, kiểm tra lại khi Lưu

            if (!DiemValidator.TryParseDiem(text, out _, out string err))
            {
                dgvKetQua.Rows[e.RowIndex].ErrorText = err;
                SetStatus(err, true);
                e.Cancel = true;                 // giữ con trỏ tại ô; nhấn Esc để hủy nhập
            }
        }

        private void dgvKetQua_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (dgvKetQua.Columns[e.ColumnIndex].Name != "colDiem") return;
            string text = Convert.ToString(e.Value).Trim();
            if (text.Length == 0) { e.Value = DBNull.Value; e.ParsingApplied = true; return; }
            if (DiemValidator.TryParseDiem(text, out decimal d, out _)) { e.Value = d; e.ParsingApplied = true; }
        }
        #endregion

        #region Lưu tất cả (transaction)
        private void btnLuuTatCa_Click(object sender, EventArgs e)
        {
            if (!(_bsKetQua.DataSource is DataTable dt) || dt.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có danh sách để lưu. Hãy tải danh sách trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvKetQua.EndEdit();
            _bsKetQua.EndEdit();

            var errors = DiemValidator.ValidateRows(dt);
            if (errors.Count > 0)
            {
                string detail = string.Join("\n", errors.Take(10)) + (errors.Count > 10 ? $"\n... và {errors.Count - 10} lỗi khác." : "");
                MessageBox.Show("Dữ liệu chưa hợp lệ, không lưu dòng nào:\n\n" + detail, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetStatus($"Không lưu: {errors.Count} dòng lỗi.", true);
                return;
            }

            try
            {
                int n = _dal.SaveAll(dt, _maMonDangTai);
                MessageBox.Show($"Đã lưu thành công {n} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach(_maLopDangTai, _maMonDangTai, _tenLopMon);   // nạp lại để thấy dữ liệu thật từ DB
                SetStatus($"Đã lưu {n} dòng. " + tslTrangThai.Text);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + "\n\nĐã ROLLBACK: không có dòng nào được lưu.", "Lưu thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Lưu thất bại - đã rollback toàn bộ.", true);
            }
        }
        #endregion

        #region Tiện ích
        private void SetStatus(string msg, bool isError = false)
        {
            tslTrangThai.Text = msg;
            tslTrangThai.ForeColor = isError ? Color.Firebrick : SystemColors.ControlText;
        }

        private void ShowDbError(Exception ex)
        {
            SetStatus("Lỗi kết nối cơ sở dữ liệu.", true);
            MessageBox.Show("Lỗi cơ sở dữ liệu:\n" + ex.Message +
                            "\n\nKiểm tra: SQL Server đang chạy, đã chạy QLSinhVienLab12.sql, chuỗi kết nối trong DAL/Database.cs.",
                            "Lỗi SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}
