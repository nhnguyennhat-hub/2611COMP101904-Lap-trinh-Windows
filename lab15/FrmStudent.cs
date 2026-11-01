using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StudentMvp.Models;
using StudentMvp.Presenters;
using StudentMvp.Repositories;

namespace StudentMvp.Views
{
    /// <summary>
    /// View: implements IStudentView. Mỗi sự kiện chỉ gọi một phương thức của Presenter.
    /// Không có logic kiểm tra, không có truy cập dữ liệu trong file này.
    /// </summary>
    public class FrmStudent : Form, IStudentView
    {
        private readonly StudentPresenter _presenter;

        private TextBox txtMaSV, txtHoTen, txtDiem, txtTuKhoa;
        private Button btnLoad, btnAdd, btnDelete, btnSearch, btnClear;
        private DataGridView dgvStudents;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tslStatus;
        private bool _dangNap;

        public FrmStudent()
        {
            BuildUi();
            _presenter = new StudentPresenter(this, new StudentRepository());

            Load += (s, e) => _presenter.LoadStudents();
            btnLoad.Click += (s, e) => _presenter.LoadStudents();
            btnAdd.Click += (s, e) => _presenter.AddStudent();
            btnDelete.Click += (s, e) => _presenter.DeleteStudent();
            btnSearch.Click += (s, e) => _presenter.SearchStudents();
            btnClear.Click += (s, e) => _presenter.Reset();
            txtTuKhoa.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { _presenter.SearchStudents(); e.SuppressKeyPress = true; } };
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (_dangNap || dgvStudents.SelectedRows.Count == 0) return;
            _presenter.SelectStudent(Convert.ToString(dgvStudents.SelectedRows[0].Cells["colMaSV"].Value));
        }

        #region IStudentView
        public string MaSV { get => txtMaSV.Text; set => txtMaSV.Text = value; }
        public string HoTen { get => txtHoTen.Text; set => txtHoTen.Text = value; }
        public string Diem { get => txtDiem.Text; set => txtDiem.Text = value; }
        public string TuKhoa => txtTuKhoa.Text;

        public void ShowStudents(IReadOnlyList<Student> students)
        {
            _dangNap = true;
            dgvStudents.DataSource = new List<Student>(students);
            _dangNap = false;
            dgvStudents.ClearSelection();
        }

        public void SetStatus(string text) => tslStatus.Text = text;

        public void ClearInputs()
        {
            txtMaSV.Clear(); txtHoTen.Clear(); txtDiem.Clear(); txtTuKhoa.Clear();
            txtMaSV.Focus();
        }

        public void ShowInfo(string message) =>
            MessageBox.Show(this, message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message) =>
            MessageBox.Show(this, message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public bool Confirm(string message) =>
            MessageBox.Show(this, message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        #endregion

        #region Dựng giao diện
        private void BuildUi()
        {
            Text = "Quản lý sinh viên (MVP) - COMP1019 Lab 15";
            ClientSize = new Size(820, 560);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10f);

            var grp = new GroupBox { Text = "Thông tin sinh viên", Dock = DockStyle.Top, Height = 110 };
            txtMaSV = new TextBox { Name = "txtMaSV", MaxLength = 15 };
            txtHoTen = new TextBox { Name = "txtHoTen", MaxLength = 100 };
            txtDiem = new TextBox { Name = "txtDiem", MaxLength = 6 };
            Dat(grp, "Mã SV (*)", txtMaSV, 20, 28, 90, 160);
            Dat(grp, "Họ tên (*)", txtHoTen, 20, 66, 90, 300);
            Dat(grp, "Điểm (0-10) (*)", txtDiem, 430, 28, 130, 100);

            var pnlNut = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(10, 6, 0, 0) };
            btnLoad = Nut("btnLoad", "Load danh sách", 140);
            btnAdd = Nut("btnAdd", "Thêm", 100);
            btnDelete = Nut("btnDelete", "Xóa theo mã", 120);
            btnClear = Nut("btnClear", "Làm mới", 100);
            pnlNut.Controls.AddRange(new Control[] { btnLoad, btnAdd, btnDelete, btnClear });

            var pnlTim = new Panel { Dock = DockStyle.Top, Height = 46 };
            txtTuKhoa = new TextBox { Name = "txtTuKhoa", Left = 150, Top = 9, Width = 280 };
            btnSearch = new Button { Name = "btnSearch", Text = "Tìm kiếm", Left = 445, Top = 7, Width = 110, Height = 32 };
            pnlTim.Controls.AddRange(new Control[] { new Label { Text = "Tìm theo mã/tên:", Left = 12, Top = 13, Width = 135 }, txtTuKhoa, btnSearch });

            dgvStudents = new DataGridView
            {
                Name = "dgvStudents", Dock = DockStyle.Fill, ReadOnly = true, MultiSelect = false,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false, RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AutoGenerateColumns = false
            };
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaSV", DataPropertyName = "MaSV", HeaderText = "Mã SV", FillWeight = 20 });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHoTen", DataPropertyName = "HoTen", HeaderText = "Họ tên", FillWeight = 40 });
            var colDiem = new DataGridViewTextBoxColumn { Name = "colDiem", DataPropertyName = "Diem", HeaderText = "Điểm", FillWeight = 15 };
            colDiem.DefaultCellStyle.Format = "0.0#";
            colDiem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStudents.Columns.Add(colDiem);
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { Name = "colXepLoai", DataPropertyName = "XepLoai", HeaderText = "Xếp loại", FillWeight = 25 });

            statusStrip = new StatusStrip();
            tslStatus = new ToolStripStatusLabel("Sẵn sàng");
            statusStrip.Items.Add(tslStatus);

            Controls.Add(dgvStudents);
            Controls.Add(statusStrip);
            Controls.Add(pnlTim);
            Controls.Add(pnlNut);
            Controls.Add(grp);
        }

        private static void Dat(GroupBox g, string nhan, Control c, int x, int y, int rongNhan, int rong)
        {
            g.Controls.Add(new Label { Text = nhan, Left = x, Top = y + 4, Width = rongNhan - 5 });
            c.Left = x + rongNhan; c.Top = y; c.Width = rong;
            g.Controls.Add(c);
        }

        private static Button Nut(string ten, string chu, int rong) => new Button { Name = ten, Text = chu, Width = rong, Height = 34 };
        #endregion
    }
}
