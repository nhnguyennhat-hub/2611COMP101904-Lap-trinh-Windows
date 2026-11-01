using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ClassManagerApp
{
    public class FrmClassManager : Form
    {
        private readonly LopRepository _repo = new LopRepository();

        private TextBox txtMaLop, txtTenLop, txtKhoaHoc, txtSearch;
        private ComboBox cboKhoa;
        private Button btnAdd, btnUpdate, btnDelete, btnClear, btnSearch, btnShowAll;
        private DataGridView dgvLop;

        public FrmClassManager()
        {
            BuildUi();
            Load += (s, e) => { LoadKhoa(); LoadGrid(); };
        }

        #region Giao diện
        private void BuildUi()
        {
            Text = "Quản lý lớp học - COMP1019 Lab 11";
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10f);

            // Nhóm nhập liệu
            var grp = new GroupBox { Text = "Thông tin lớp", Dock = DockStyle.Top, Height = 150, Padding = new Padding(10) };
            txtMaLop = new TextBox { Name = "txtMaLop", MaxLength = 15 };
            txtTenLop = new TextBox { Name = "txtTenLop", MaxLength = 100 };
            txtKhoaHoc = new TextBox { Name = "txtKhoaHoc", MaxLength = 10 };
            cboKhoa = new ComboBox { Name = "cboKhoa", DropDownStyle = ComboBoxStyle.DropDownList };
            Place(grp, "Mã lớp (*)", txtMaLop, 20, 30, 200);
            Place(grp, "Tên lớp (*)", txtTenLop, 20, 70, 200, 300);
            Place(grp, "Khóa học", txtKhoaHoc, 520, 30, 90, 150);
            Place(grp, "Khoa (*)", cboKhoa, 520, 70, 90, 250);

            // Thanh nút CRUD
            var pnlBtn = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(10, 6, 0, 0) };
            btnAdd = Btn("btnAdd", "Thêm");
            btnUpdate = Btn("btnUpdate", "Sửa");
            btnDelete = Btn("btnDelete", "Xóa");
            btnClear = Btn("btnClear", "Làm mới");
            pnlBtn.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            // Thanh tìm kiếm
            var pnlSearch = new Panel { Dock = DockStyle.Top, Height = 48 };
            pnlSearch.Controls.Add(new Label { Text = "Tìm (mã/tên lớp):", Left = 12, Top = 13, Width = 130 });
            txtSearch = new TextBox { Name = "txtSearch", Left = 145, Top = 10, Width = 280 };
            btnSearch = new Button { Name = "btnSearch", Text = "Tìm kiếm", Left = 435, Top = 8, Width = 110, Height = 32 };
            btnShowAll = new Button { Name = "btnShowAll", Text = "Hiện tất cả", Left = 555, Top = 8, Width = 110, Height = 32 };
            pnlSearch.Controls.AddRange(new Control[] { txtSearch, btnSearch, btnShowAll });

            // Lưới dữ liệu
            dgvLop = new DataGridView
            {
                Name = "dgvLop", Dock = DockStyle.Fill, ReadOnly = true, MultiSelect = false,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoGenerateColumns = true, BackgroundColor = Color.White, RowHeadersVisible = false
            };

            // Thứ tự add: Fill trước, rồi các Top theo thứ tự ngược lại
            Controls.Add(dgvLop);
            Controls.Add(pnlSearch);
            Controls.Add(pnlBtn);
            Controls.Add(grp);

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += (s, e) => ClearInputs();
            btnSearch.Click += (s, e) => LoadGrid(txtSearch.Text);
            btnShowAll.Click += (s, e) => { txtSearch.Clear(); LoadGrid(); };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { LoadGrid(txtSearch.Text); e.SuppressKeyPress = true; } };
            dgvLop.SelectionChanged += dgvLop_SelectionChanged;
            dgvLop.DataBindingComplete += (s, e) => FormatGrid();
        }

        private static void Place(GroupBox g, string label, Control c, int x, int y, int labelW, int w = 220)
        {
            g.Controls.Add(new Label { Text = label, Left = x, Top = y + 4, Width = labelW - 10 });
            c.Left = x + labelW; c.Top = y; c.Width = w;
            g.Controls.Add(c);
        }

        private static Button Btn(string name, string text) => new Button { Name = name, Text = text, Width = 110, Height = 34 };

        private void FormatGrid()
        {
            string[] heads = { "Mã lớp", "Tên lớp", "Khóa học", "Mã khoa", "Tên khoa", "Số SV" };
            for (int i = 0; i < heads.Length && i < dgvLop.Columns.Count; i++)
                dgvLop.Columns[i].HeaderText = heads[i];
            if (dgvLop.Columns.Count > 5) dgvLop.Columns[5].FillWeight = 40;
        }
        #endregion

        #region Tải dữ liệu
        private void LoadKhoa()
        {
            try
            {
                cboKhoa.DataSource = _repo.GetKhoaList();
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.SelectedIndex = -1;
            }
            catch (SqlException ex) { ShowDbError(ex); }
        }

        private void LoadGrid(string keyword = null)
        {
            try
            {
                dgvLop.DataSource = string.IsNullOrWhiteSpace(keyword) ? _repo.GetAll() : _repo.Search(keyword.Trim());
                dgvLop.ClearSelection();
            }
            catch (SqlException ex) { ShowDbError(ex); }
        }

        private void dgvLop_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLop.SelectedRows.Count == 0) return;
            var row = dgvLop.SelectedRows[0];
            txtMaLop.Text = Convert.ToString(row.Cells["MaLop"].Value);
            txtTenLop.Text = Convert.ToString(row.Cells["TenLop"].Value);
            txtKhoaHoc.Text = Convert.ToString(row.Cells["KhoaHoc"].Value);
            cboKhoa.SelectedValue = Convert.ToString(row.Cells["MaKhoa"].Value);
            txtMaLop.ReadOnly = true;   // MaLop là khóa chính, không cho sửa
        }
        #endregion

        #region CRUD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            string ma = txtMaLop.Text.Trim();
            try
            {
                if (_repo.Exists(ma))
                {
                    Warn($"Mã lớp '{ma}' đã tồn tại. Vui lòng nhập mã khác.");
                    txtMaLop.Focus();
                    return;
                }
                _repo.Insert(ma, txtTenLop.Text.Trim(), txtKhoaHoc.Text.Trim(), (string)cboKhoa.SelectedValue);
                Info("Thêm lớp thành công.");
                LoadGrid();
                ClearInputs();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                Warn($"Mã lớp '{ma}' đã tồn tại.");
            }
            catch (SqlException ex) { ShowDbError(ex); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!txtMaLop.ReadOnly) { Warn("Hãy chọn một lớp trong danh sách để sửa."); return; }
            if (!ValidateInput()) return;
            try
            {
                int n = _repo.Update(txtMaLop.Text.Trim(), txtTenLop.Text.Trim(), txtKhoaHoc.Text.Trim(), (string)cboKhoa.SelectedValue);
                if (n == 0) { Warn("Không tìm thấy lớp để cập nhật (có thể đã bị xóa)."); LoadGrid(); return; }
                Info("Cập nhật thành công.");
                LoadGrid();
                ClearInputs();
            }
            catch (SqlException ex) { ShowDbError(ex); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!txtMaLop.ReadOnly) { Warn("Hãy chọn một lớp trong danh sách để xóa."); return; }
            string ma = txtMaLop.Text.Trim();
            try
            {
                int soSv = _repo.CountStudents(ma);
                if (soSv > 0)
                {
                    Warn($"Không thể xóa lớp '{ma}' vì còn {soSv} sinh viên thuộc lớp này.\nHãy chuyển hoặc xóa các sinh viên trước.");
                    return;
                }
                if (MessageBox.Show($"Xóa lớp '{ma}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                _repo.Delete(ma);
                Info("Đã xóa lớp.");
                LoadGrid();
                ClearInputs();
            }
            catch (SqlException ex) when (ex.Number == 547)   // vi phạm khóa ngoại (phòng khi có người thêm SV vào lúc đó)
            {
                Warn($"Không thể xóa lớp '{ma}' vì đang được tham chiếu bởi dữ liệu khác.");
            }
            catch (SqlException ex) { ShowDbError(ex); }
        }

        private bool ValidateInput()
        {
            string err = LopValidator.Validate(txtMaLop.Text, txtTenLop.Text, txtKhoaHoc.Text, cboKhoa.SelectedValue);
            if (err != null) Warn(err);
            return err == null;
        }

        private void ClearInputs()
        {
            txtMaLop.Clear(); txtTenLop.Clear(); txtKhoaHoc.Clear();
            cboKhoa.SelectedIndex = -1;
            txtMaLop.ReadOnly = false;
            dgvLop.ClearSelection();
            txtMaLop.Focus();
        }
        #endregion

        #region Thông báo
        private static void Info(string m) => MessageBox.Show(m, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        private static void Warn(string m) => MessageBox.Show(m, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private static void ShowDbError(SqlException ex) =>
            MessageBox.Show("Lỗi cơ sở dữ liệu (" + ex.Number + "):\n" + ex.Message +
                            "\n\nKiểm tra SQL Server đang chạy, đã chạy script buổi 10 và chuỗi kết nối trong DbConfig.cs.",
                            "Lỗi SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
        #endregion
    }
}
