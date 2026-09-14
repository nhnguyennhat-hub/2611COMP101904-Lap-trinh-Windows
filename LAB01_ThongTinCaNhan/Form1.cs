namespace LAB01_ThongTinCaNhan;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        cboKhoa.SelectedIndex = 0;
    }

    private void btnHienThi_Click(object sender, EventArgs e)
    {
        if (txtHoTen.Text.Trim() == "")
        {
            MessageBox.Show("Vui lòng nhập họ tên!");
            txtHoTen.Focus();
            return;
        }

        int namSinh;

        if (txtNamSinh.Text.Trim() == "")
        {
            MessageBox.Show("Vui lòng nhập năm sinh!");
            txtNamSinh.Focus();
            return;
        }

        if (!int.TryParse(txtNamSinh.Text, out namSinh))
        {
            MessageBox.Show("Năm sinh phải là số nguyên!");
            txtNamSinh.Focus();
            return;
        }

        if (namSinh < 1900 || namSinh > DateTime.Now.Year)
        {
            MessageBox.Show("Năm sinh phải từ 1900 đến năm hiện tại!");
            txtNamSinh.Focus();
            return;
        }

        if (txtEmail.Text.Trim() == "")
        {
            MessageBox.Show("Vui lòng nhập email!");
            txtEmail.Focus();
            return;
        }

        if (!radNam.Checked && !radNu.Checked)
        {
            MessageBox.Show("Vui lòng chọn giới tính!");
            return;
        }

        if (cboKhoa.SelectedIndex == -1)
        {
            MessageBox.Show("Vui lòng chọn khoa/lớp!");
            return;
        }

        string hoTen = txtHoTen.Text.Trim();
        string email = txtEmail.Text.Trim();
        string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
        int tuoi = DateTime.Now.Year - namSinh;
        string khoa = cboKhoa.SelectedItem?.ToString() ?? "";

        lblKetQua.Text =
            "THÔNG TIN SINH VIÊN\n" +
            "Họ tên: " + hoTen + "\n" +
            "Tuổi: " + tuoi + "\n" +
            "Email: " + email + "\n" +
            "Giới tính: " + gioiTinh + "\n" +
            "Khoa/Lớp: " + khoa;
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        txtHoTen.Clear();
        txtNamSinh.Clear();
        txtEmail.Clear();
        radNam.Checked = false;
        radNu.Checked = false;
        cboKhoa.SelectedIndex = 0;
        lblKetQua.Text = "";
        txtHoTen.Focus();
    }

    private void btnThoat_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show(
            "Bạn có chắc muốn thoát không?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
            Application.Exit();
    }
}
