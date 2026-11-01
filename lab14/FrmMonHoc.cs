using System;
using System.Drawing;
using System.Windows.Forms;
using EduManager.BLL;
using EduManager.Models;

namespace EduManager.GUI
{
    /// <summary>
    /// Giao diện quản lý môn học. Chỉ nhận dữ liệu, gọi BLL và hiển thị kết quả.
    /// Không chứa SQL, không chứa quy tắc nghiệp vụ.
    /// </summary>
    public class FrmMonHoc : Form
    {
        private readonly MonHocBLL _bll = new MonHocBLL();

        private TextBox txtMaMonHoc, txtTenMonHoc, txtTimKiem;
        private NumericUpDown nudSoTinChi;
        private CheckBox chkTrangThai;
        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem;
        private DataGridView dgvMonHoc;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tslTongSo;

        private bool _dangNapDuLieu;   // chặn SelectionChanged khi đang gán DataSource

        public FrmMonHoc()
        {
            BuildUi();
            Load += (s, e) => LoadDanhSach();
        }

        #region Giao diện
        private void BuildUi()
        {
            Text = "Quản lý môn học - EduManager (COMP1019 Lab 14)";
            ClientSize = new Size(900, 620);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10f);

            var grpNhap = new GroupBox { Text = "Thông tin môn học", Dock = DockStyle.Top, Height = 130 };
            txtMaMonHoc = new TextBox { Name = "txtMaMonHoc", MaxLength = 10 };
            txtTenMonHoc = new TextBox { Name = "txtTenMonHoc", MaxLength = 100 };
            nudSoTinChi = new NumericUpDown { Name = "nudSoTinChi", Minimum = MonHocBLL.TinChiToiThieu, Maximum = MonHocBLL.TinChiToiDa, Value = 3 };
            chkTrangThai = new CheckBox { Name = "chkTrangThai", Text = "Còn sử dụng", Checked = true };
            Dat(grpNhap, "Mã môn học (*)", txtMaMonHoc, 20, 30, 120, 200);
            Dat(grpNhap, "Tên môn học (*)", txtTenMonHoc, 20, 70, 120, 330);
            Dat(grpNhap, "Số tín chỉ", nudSoTinChi, 500, 30, 90, 80);
            chkTrangThai.Left = 590; chkTrangThai.Top = 72; chkTrangThai.Width = 160;
            grpNhap.Controls.Add(chkTrangThai);

            var pnlNut = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(10, 6, 0, 0) };
            btnThem = Nut("btnThem", "Thêm");
            btnSua = Nut("btnSua", "Sửa");
            btnXoa = Nut("btnXoa", "Xóa");
            btnLamMoi = Nut("btnLamMoi", "Làm mới");
            pnlNut.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnLamMoi });

            var pnlTim = new Panel { Dock = DockStyle.Top, Height = 46 };
            txtTimKiem = new TextBox { Name = "txtTimKiem", Left = 150, Top = 9, Width = 300 };
            btnTimKiem = new Button { Name = "btnTimKiem", Text = "Tìm kiếm", Left = 465, Top = 7, Width = 110, Height = 32 };
            pnlTim.Controls.AddRange(new Control[] { new Label { Text = "Tìm theo mã/tên:", Left = 12, Top = 13, Width = 135 }, txtTimKiem, btnTimKiem });

            dgvMonHoc = new DataGridView
            {
                Name = "dgvMonHoc", Dock = DockStyle.Fill, ReadOnly = true, MultiSelect = false,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false, RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AutoGenerateColumns = false
            };
            dgvMonHoc.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaMonHoc", HeaderText = "Mã môn học", FillWeight = 20 });
            dgvMonHoc.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenMonHoc", HeaderText = "Tên môn học", FillWeight = 50 });
            dgvMonHoc.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoTinChi", HeaderText = "Số tín chỉ", FillWeight = 15 });
            dgvMonHoc.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Còn sử dụng", FillWeight = 15 });

            statusStrip = new StatusStrip();
            tslTongSo = new ToolStripStatusLabel();
            statusStrip.Items.Add(tslTongSo);

            Controls.Add(dgvMonHoc);
            Controls.Add(statusStrip);
            Controls.Add(pnlTim);
            Controls.Add(pnlNut);
            Controls.Add(grpNhap);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += (s, e) => LamMoi();
            btnTimKiem.Click += (s, e) => LoadDanhSach(txtTimKiem.Text);
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { LoadDanhSach(txtTimKiem.Text); e.SuppressKeyPress = true; } };
            dgvMonHoc.SelectionChanged += dgvMonHoc_SelectionChanged;
        }

        private static void Dat(GroupBox g, string nhan, Control c, int x, int y, int rongNhan, int rong)
        {
            g.Controls.Add(new Label { Text = nhan, Left = x, Top = y + 4, Width = rongNhan - 5 });
            c.Left = x + rongNhan; c.Top = y; c.Width = rong;
            g.Controls.Add(c);
        }

        private static Button Nut(string ten, string chu) => new Button { Name = ten, Text = chu, Width = 110, Height = 34 };
        #endregion

        #region Tải dữ liệu và chọn dòng
        private void LoadDanhSach(string tuKhoa = null)
        {
            var ketQua = _bll.LayDanhSach(tuKhoa);
            if (!ketQua.Success) { HienLoi(ketQua.Message); return; }

            _dangNapDuLieu = true;
            dgvMonHoc.DataSource = ketQua.Data;
            _dangNapDuLieu = false;
            dgvMonHoc.ClearSelection();

            tslTongSo.Text = $"Tổng số: {ketQua.Data.Count} môn học";
            if (ketQua.Data.Count == 0 && !string.IsNullOrWhiteSpace(tuKhoa))
                MessageBox.Show("Không tìm thấy môn học nào phù hợp.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvMonHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangNapDuLieu || dgvMonHoc.SelectedRows.Count == 0) return;
            if (!(dgvMonHoc.SelectedRows[0].DataBoundItem is MonHoc mh)) return;

            txtMaMonHoc.Text = mh.MaMonHoc;
            txtTenMonHoc.Text = mh.TenMonHoc;
            nudSoTinChi.Value = Math.Min(Math.Max(mh.SoTinChi, nudSoTinChi.Minimum), nudSoTinChi.Maximum);
            chkTrangThai.Checked = mh.TrangThai;
            txtMaMonHoc.ReadOnly = true;     // khóa chính không cho sửa
        }
        #endregion

        #region Thêm / Sửa / Xóa / Làm mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaMonHoc.ReadOnly)
            {
                HienCanhBao("Đang chọn một môn học có sẵn. Bấm Làm mới để nhập môn học mới.");
                return;
            }
            var kq = _bll.Them(LayTuForm());
            if (!kq.Success) { HienCanhBao(kq.Message); return; }

            HienThongTin(kq.Message);
            LamMoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtMaMonHoc.ReadOnly) { HienCanhBao("Hãy chọn một môn học trong danh sách để sửa."); return; }

            var kq = _bll.Sua(LayTuForm());
            if (!kq.Success) { HienCanhBao(kq.Message); return; }

            HienThongTin(kq.Message);
            LamMoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!txtMaMonHoc.ReadOnly) { HienCanhBao("Hãy chọn một môn học trong danh sách để xóa."); return; }

            string ma = txtMaMonHoc.Text;
            var xacNhan = MessageBox.Show($"Bạn có chắc muốn xóa môn học '{ma} - {txtTenMonHoc.Text}'?",
                                          "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (xacNhan != DialogResult.Yes) return;

            var kq = _bll.Xoa(ma);
            if (!kq.Success) { HienCanhBao(kq.Message); return; }

            HienThongTin(kq.Message);
            LamMoi();
        }

        private void LamMoi()
        {
            txtMaMonHoc.Clear();
            txtTenMonHoc.Clear();
            txtTimKiem.Clear();
            nudSoTinChi.Value = 3;
            chkTrangThai.Checked = true;
            txtMaMonHoc.ReadOnly = false;
            LoadDanhSach();
            txtMaMonHoc.Focus();
        }

        /// <summary>Gom dữ liệu từ control thành đối tượng; mọi kiểm tra để BLL lo.</summary>
        private MonHoc LayTuForm() => new MonHoc
        {
            MaMonHoc = txtMaMonHoc.Text,
            TenMonHoc = txtTenMonHoc.Text,
            SoTinChi = (int)nudSoTinChi.Value,
            TrangThai = chkTrangThai.Checked
        };
        #endregion

        #region Thông báo
        private static void HienThongTin(string m) => MessageBox.Show(m, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        private static void HienCanhBao(string m) => MessageBox.Show(m, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private static void HienLoi(string m) => MessageBox.Show(m, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        #endregion
    }
}
