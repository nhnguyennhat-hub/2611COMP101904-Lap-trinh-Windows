using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CourseRegistrationApp;

public partial class Form1 : Form
{
    private readonly Dictionary<string, decimal> hocPhi = new()
    {
        { "C# WinForms cơ bản", 800000m },
        { "SQL Server cơ bản", 700000m },
        { "Web Frontend cơ bản", 750000m },
        { "Lập trình Python cơ bản", 650000m }
    };

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        cboKhoaHoc.Items.Clear();
        foreach (string khoaHoc in hocPhi.Keys)
            cboKhoaHoc.Items.Add(khoaHoc);

        cboKhoaHoc.SelectedIndex = 0;
        radOnline.Checked = true;
        numSoThang.Minimum = 1;
        numSoThang.Maximum = 12;
        numSoThang.Value = 1;
        CapNhatTongTien();
    }

    private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        CapNhatTongTien();
    }

    private void numSoThang_ValueChanged(object sender, EventArgs e)
    {
        CapNhatTongTien();
    }

    private void CapNhatTongTien()
    {
        if (cboKhoaHoc.SelectedItem == null)
        {
            lblTongTien.Text = "Tổng học phí: 0 VNĐ";
            return;
        }

        string khoaHoc = cboKhoaHoc.SelectedItem.ToString();
        decimal tongTien = hocPhi[khoaHoc] * numSoThang.Value;
        lblTongTien.Text = "Tổng học phí: " + tongTien.ToString("N0") + " VNĐ";
    }

    private void btnDangKy_Click(object sender, EventArgs e)
    {
        string hoTen = txtHoTen.Text.Trim();
        string soDienThoai = txtSoDienThoai.Text.Trim();

        if (hoTen == "")
        {
            MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }

        if (soDienThoai == "")
        {
            MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSoDienThoai.Focus();
            return;
        }

        if (cboKhoaHoc.SelectedIndex == -1)
        {
            MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboKhoaHoc.Focus();
            return;
        }

        string khoaHoc = cboKhoaHoc.SelectedItem.ToString();
        string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp";
        string nhanEmail = chkNhanEmail.Checked ? "Có" : "Không";
        decimal tongTien = hocPhi[khoaHoc] * numSoThang.Value;

        string phieuDangKy =
            "===== PHIẾU ĐĂNG KÝ KHÓA HỌC =====\n\n" +
            "Họ tên: " + hoTen + "\n" +
            "Số điện thoại: " + soDienThoai + "\n" +
            "Ngày sinh: " + dtpNgaySinh.Value.ToString("dd/MM/yyyy") + "\n" +
            "Khóa học: " + khoaHoc + "\n" +
            "Hình thức học: " + hinhThuc + "\n" +
            "Số tháng đăng ký: " + numSoThang.Value + "\n" +
            "Tổng tiền: " + tongTien.ToString("N0") + " VNĐ\n" +
            "Nhận email thông báo: " + nhanEmail + "\n" +
            "Trạng thái: Đăng ký thành công";

        MessageBox.Show(phieuDangKy, "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        txtHoTen.Clear();
        txtSoDienThoai.Clear();
        dtpNgaySinh.Value = DateTime.Now;
        chkNhanEmail.Checked = false;
        cboKhoaHoc.SelectedIndex = 0;
        radOnline.Checked = true;
        radOffline.Checked = false;
        numSoThang.Value = 1;
        CapNhatTongTien();
        txtHoTen.Focus();
    }

    private void btnThoat_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show(
            "Bạn có chắc muốn thoát chương trình không?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
            Close();
    }
}
