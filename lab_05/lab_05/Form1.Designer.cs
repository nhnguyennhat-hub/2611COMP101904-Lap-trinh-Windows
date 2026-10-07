namespace CourseRegistrationApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.GroupBox grpHocVien;
    private System.Windows.Forms.GroupBox grpKhoaHoc;
    private System.Windows.Forms.Label lblHoTen, lblSoDienThoai, lblNgaySinh, lblKhoaHoc, lblHinhThuc, lblSoThang;
    private System.Windows.Forms.TextBox txtHoTen, txtSoDienThoai;
    private System.Windows.Forms.DateTimePicker dtpNgaySinh;
    private System.Windows.Forms.CheckBox chkNhanEmail;
    private System.Windows.Forms.ComboBox cboKhoaHoc;
    private System.Windows.Forms.RadioButton radOnline, radOffline;
    private System.Windows.Forms.NumericUpDown numSoThang;
    private System.Windows.Forms.Label lblTongTien;
    private System.Windows.Forms.Button btnDangKy, btnLamMoi, btnThoat;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label(); grpHocVien = new GroupBox(); grpKhoaHoc = new GroupBox();
        lblHoTen = new Label(); lblSoDienThoai = new Label(); lblNgaySinh = new Label(); lblKhoaHoc = new Label();
        lblHinhThuc = new Label(); lblSoThang = new Label(); txtHoTen = new TextBox(); txtSoDienThoai = new TextBox();
        dtpNgaySinh = new DateTimePicker(); chkNhanEmail = new CheckBox(); cboKhoaHoc = new ComboBox();
        radOnline = new RadioButton(); radOffline = new RadioButton(); numSoThang = new NumericUpDown();
        lblTongTien = new Label(); btnDangKy = new Button(); btnLamMoi = new Button(); btnThoat = new Button();
        grpHocVien.SuspendLayout(); grpKhoaHoc.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)numSoThang).BeginInit(); SuspendLayout();

        lblTitle.AutoSize = true; lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold); lblTitle.Location = new System.Drawing.Point(220, 20); lblTitle.Text = "ĐĂNG KÝ KHÓA HỌC";
        grpHocVien.Location = new System.Drawing.Point(35, 85); grpHocVien.Size = new System.Drawing.Size(710, 205); grpHocVien.Text = "Thông tin học viên";
        lblHoTen.AutoSize = true; lblHoTen.Location = new System.Drawing.Point(25, 35); lblHoTen.Text = "Họ tên:";
        txtHoTen.Location = new System.Drawing.Point(180, 31); txtHoTen.Name = "txtHoTen"; txtHoTen.Size = new System.Drawing.Size(480, 27); txtHoTen.TabIndex = 0;
        lblSoDienThoai.AutoSize = true; lblSoDienThoai.Location = new System.Drawing.Point(25, 78); lblSoDienThoai.Text = "Số điện thoại:";
        txtSoDienThoai.Location = new System.Drawing.Point(180, 74); txtSoDienThoai.Name = "txtSoDienThoai"; txtSoDienThoai.Size = new System.Drawing.Size(480, 27); txtSoDienThoai.TabIndex = 1;
        lblNgaySinh.AutoSize = true; lblNgaySinh.Location = new System.Drawing.Point(25, 121); lblNgaySinh.Text = "Ngày sinh:";
        dtpNgaySinh.Location = new System.Drawing.Point(180, 117); dtpNgaySinh.Name = "dtpNgaySinh"; dtpNgaySinh.Size = new System.Drawing.Size(220, 27); dtpNgaySinh.TabIndex = 2; dtpNgaySinh.Format = DateTimePickerFormat.Short;
        chkNhanEmail.AutoSize = true; chkNhanEmail.Location = new System.Drawing.Point(180, 158); chkNhanEmail.Name = "chkNhanEmail"; chkNhanEmail.Text = "Nhận email thông báo"; chkNhanEmail.TabIndex = 3;
        grpHocVien.Controls.AddRange(new Control[] { lblHoTen, txtHoTen, lblSoDienThoai, txtSoDienThoai, lblNgaySinh, dtpNgaySinh, chkNhanEmail });

        grpKhoaHoc.Location = new System.Drawing.Point(35, 305); grpKhoaHoc.Size = new System.Drawing.Size(710, 175); grpKhoaHoc.Text = "Thông tin khóa học";
        lblKhoaHoc.AutoSize = true; lblKhoaHoc.Location = new System.Drawing.Point(25, 35); lblKhoaHoc.Text = "Khóa học:";
        cboKhoaHoc.DropDownStyle = ComboBoxStyle.DropDownList; cboKhoaHoc.Location = new System.Drawing.Point(180, 31); cboKhoaHoc.Name = "cboKhoaHoc"; cboKhoaHoc.Size = new System.Drawing.Size(480, 28); cboKhoaHoc.TabIndex = 4;
        lblHinhThuc.AutoSize = true; lblHinhThuc.Location = new System.Drawing.Point(25, 78); lblHinhThuc.Text = "Hình thức:";
        radOnline.AutoSize = true; radOnline.Location = new System.Drawing.Point(180, 75); radOnline.Name = "radOnline"; radOnline.Text = "Online"; radOnline.TabIndex = 5;
        radOffline.AutoSize = true; radOffline.Location = new System.Drawing.Point(280, 75); radOffline.Name = "radOffline"; radOffline.Text = "Trực tiếp"; radOffline.TabIndex = 6;
        lblSoThang.AutoSize = true; lblSoThang.Location = new System.Drawing.Point(25, 120); lblSoThang.Text = "Số tháng:";
        numSoThang.Location = new System.Drawing.Point(180, 117); numSoThang.Name = "numSoThang"; numSoThang.Size = new System.Drawing.Size(100, 27); numSoThang.TabIndex = 7; numSoThang.Minimum = 1; numSoThang.Maximum = 12; numSoThang.Value = 1;
        lblTongTien.AutoSize = true; lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); lblTongTien.Location = new System.Drawing.Point(380, 118); lblTongTien.Name = "lblTongTien"; lblTongTien.Text = "Tổng học phí: 0 VNĐ";
        grpKhoaHoc.Controls.AddRange(new Control[] { lblKhoaHoc, cboKhoaHoc, lblHinhThuc, radOnline, radOffline, lblSoThang, numSoThang, lblTongTien });

        btnDangKy.Location = new System.Drawing.Point(180, 510); btnDangKy.Name = "btnDangKy"; btnDangKy.Size = new System.Drawing.Size(120, 42); btnDangKy.Text = "Đăng ký"; btnDangKy.TabIndex = 8;
        btnLamMoi.Location = new System.Drawing.Point(330, 510); btnLamMoi.Name = "btnLamMoi"; btnLamMoi.Size = new System.Drawing.Size(120, 42); btnLamMoi.Text = "Làm mới"; btnLamMoi.TabIndex = 9;
        btnThoat.Location = new System.Drawing.Point(480, 510); btnThoat.Name = "btnThoat"; btnThoat.Size = new System.Drawing.Size(120, 42); btnThoat.Text = "Thoát"; btnThoat.TabIndex = 10;
        Controls.AddRange(new Control[] { lblTitle, grpHocVien, grpKhoaHoc, btnDangKy, btnLamMoi, btnThoat });
        ClientSize = new System.Drawing.Size(780, 600); StartPosition = FormStartPosition.CenterScreen; Text = "ĐĂNG KÝ KHÓA HỌC"; FormBorderStyle = FormBorderStyle.FixedSingle; MaximizeBox = false;
        Load += Form1_Load; cboKhoaHoc.SelectedIndexChanged += cboKhoaHoc_SelectedIndexChanged; numSoThang.ValueChanged += numSoThang_ValueChanged; btnDangKy.Click += btnDangKy_Click; btnLamMoi.Click += btnLamMoi_Click; btnThoat.Click += btnThoat_Click;
        grpHocVien.ResumeLayout(false); grpHocVien.PerformLayout(); grpKhoaHoc.ResumeLayout(false); grpKhoaHoc.PerformLayout(); ((System.ComponentModel.ISupportInitialize)numSoThang).EndInit(); ResumeLayout(false); PerformLayout();
    }
}
