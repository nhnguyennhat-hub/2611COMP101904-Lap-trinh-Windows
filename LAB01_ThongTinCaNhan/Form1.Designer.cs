namespace LAB01_ThongTinCaNhan;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblHoTen;
    private Label lblNamSinh;
    private Label lblEmail;
    private Label lblKhoa;
    private Label lblGioiTinh;
    private TextBox txtHoTen;
    private TextBox txtNamSinh;
    private TextBox txtEmail;
    private GroupBox grpGioiTinh;
    private RadioButton radNam;
    private RadioButton radNu;
    private ComboBox cboKhoa;
    private Button btnHienThi;
    private Button btnXoa;
    private Button btnThoat;
    private Label lblKetQua;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        lblTitle = new Label();
        lblHoTen = new Label();
        lblNamSinh = new Label();
        lblEmail = new Label();
        lblKhoa = new Label();
        lblGioiTinh = new Label();
        txtHoTen = new TextBox();
        txtNamSinh = new TextBox();
        txtEmail = new TextBox();
        grpGioiTinh = new GroupBox();
        radNam = new RadioButton();
        radNu = new RadioButton();
        cboKhoa = new ComboBox();
        btnHienThi = new Button();
        btnXoa = new Button();
        btnThoat = new Button();
        lblKetQua = new Label();

        grpGioiTinh.SuspendLayout();
        SuspendLayout();

        // lblTitle
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(120, 25);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(320, 30);
        lblTitle.Text = "THÔNG TIN CÁ NHÂN SINH VIÊN";

        // labels
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(45, 85);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Text = "Họ tên:";

        lblNamSinh.AutoSize = true;
        lblNamSinh.Location = new Point(45, 125);
        lblNamSinh.Name = "lblNamSinh";
        lblNamSinh.Text = "Năm sinh:";

        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(45, 165);
        lblEmail.Name = "lblEmail";
        lblEmail.Text = "Email:";

        lblGioiTinh.AutoSize = true;
        lblGioiTinh.Location = new Point(45, 195);
        lblGioiTinh.Name = "lblGioiTinh";
        lblGioiTinh.Text = "Giới tính:";

        lblKhoa.AutoSize = true;
        lblKhoa.Location = new Point(45, 235);
        lblKhoa.Name = "lblKhoa";
        lblKhoa.Text = "Khoa/Lớp:";

        // textboxes
        txtHoTen.Location = new Point(135, 82);
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(350, 23);

        txtNamSinh.Location = new Point(135, 122);
        txtNamSinh.Name = "txtNamSinh";
        txtNamSinh.Size = new Size(350, 23);

        txtEmail.Location = new Point(135, 162);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(350, 23);

        // group box
        grpGioiTinh.Controls.Add(radNam);
        grpGioiTinh.Controls.Add(radNu);
        grpGioiTinh.Location = new Point(135, 188);
        grpGioiTinh.Name = "grpGioiTinh";
        grpGioiTinh.Size = new Size(350, 35);
        

        radNam.AutoSize = true;
        radNam.Location = new Point(15, 10);
        radNam.Name = "radNam";
        radNam.Text = "Nam";

        radNu.AutoSize = true;
        radNu.Location = new Point(90, 10);
        radNu.Name = "radNu";
        radNu.Text = "Nữ";

        // combobox
        cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhoa.FormattingEnabled = true;
        cboKhoa.Items.AddRange(new object[] {
            "Công nghệ thông tin",
            "Sư phạm tin học",
            "Công nghệ Giáo dục"
        });
        cboKhoa.Location = new Point(135, 232);
        cboKhoa.Name = "cboKhoa";
        cboKhoa.Size = new Size(350, 23);

        // buttons
        btnHienThi.Location = new Point(135, 275);
        btnHienThi.Name = "btnHienThi";
        btnHienThi.Size = new Size(105, 35);
        btnHienThi.Text = "Hiển thị";
        btnHienThi.Click += btnHienThi_Click;

        btnXoa.Location = new Point(255, 275);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(105, 35);
        btnXoa.Text = "Xóa";
        btnXoa.Click += btnXoa_Click;

        btnThoat.Location = new Point(375, 275);
        btnThoat.Name = "btnThoat";
        btnThoat.Size = new Size(110, 35);
        btnThoat.Text = "Thoát";
        btnThoat.Click += btnThoat_Click;

        // result
        lblKetQua.BorderStyle = BorderStyle.FixedSingle;
        lblKetQua.Location = new Point(45, 330);
        lblKetQua.Name = "lblKetQua";
        lblKetQua.Size = new Size(440, 145);
        lblKetQua.Text = "";
        lblKetQua.Padding = new Padding(10);

        // Form1
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(540, 510);
        Controls.Add(lblKetQua);
        Controls.Add(btnThoat);
        Controls.Add(btnXoa);
        Controls.Add(btnHienThi);
        Controls.Add(cboKhoa);
        Controls.Add(lblKhoa);
        Controls.Add(grpGioiTinh);
        Controls.Add(lblGioiTinh);
        Controls.Add(txtEmail);
        Controls.Add(txtNamSinh);
        Controls.Add(txtHoTen);
        Controls.Add(lblEmail);
        Controls.Add(lblNamSinh);
        Controls.Add(lblHoTen);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Ứng dụng thông tin cá nhân";

        grpGioiTinh.ResumeLayout(false);
        grpGioiTinh.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
