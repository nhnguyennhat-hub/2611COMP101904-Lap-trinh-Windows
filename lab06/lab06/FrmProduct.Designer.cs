using System.Drawing;
using System.Windows.Forms;

namespace ProductManager
{
    partial class FrmProduct
    {
        private System.ComponentModel.IContainer components = null;

        private MenuStrip menuStrip1;
        private ToolStrip toolStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCount;
        private GroupBox grpProduct;
        private Label lblMaSP, lblTenSP, lblLoaiSP, lblDonGia, lblSoLuong, lblNgayNhap;
        private TextBox txtMaSP, txtTenSP, txtDuongDanAnh;
        private ComboBox cboLoaiSP;
        private NumericUpDown numDonGia, numSoLuong;
        private DateTimePicker dtpNgayNhap;
        private CheckBox chkConKinhDoanh;
        private PictureBox picProduct;
        private Button btnChonAnh;
        private DataGridView dgvProducts;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ctxSua, ctxXoa, ctxChiTiet;
        private ErrorProvider errorProvider1;
        private OpenFileDialog openFileDialog1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            menuStrip1 = new MenuStrip();
            toolStrip1 = new ToolStrip();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCount = new ToolStripStatusLabel();
            grpProduct = new GroupBox();
            lblMaSP = new Label();
            lblTenSP = new Label();
            lblLoaiSP = new Label();
            lblDonGia = new Label();
            lblSoLuong = new Label();
            lblNgayNhap = new Label();
            txtMaSP = new TextBox();
            txtTenSP = new TextBox();
            txtDuongDanAnh = new TextBox();
            cboLoaiSP = new ComboBox();
            numDonGia = new NumericUpDown();
            numSoLuong = new NumericUpDown();
            dtpNgayNhap = new DateTimePicker();
            chkConKinhDoanh = new CheckBox();
            picProduct = new PictureBox();
            btnChonAnh = new Button();
            dgvProducts = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ctxSua = new ToolStripMenuItem("Sửa");
            ctxXoa = new ToolStripMenuItem("Xóa");
            ctxChiTiet = new ToolStripMenuItem("Xem chi tiết");
            errorProvider1 = new ErrorProvider(components);
            openFileDialog1 = new OpenFileDialog();

            SuspendLayout();

            // Menu
            menuStrip1.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("File", null, null, "mnuFile"),
                new ToolStripMenuItem("Quản lý", null, null, "mnuQuanLy"),
                new ToolStripMenuItem("Trợ giúp", null, null, "mnuTroGiup")
            });
            var fileMenu = (ToolStripMenuItem)menuStrip1.Items[0];
            fileMenu.DropDownItems.Add("Thoát", null, (s,e) => Close());
            var manageMenu = (ToolStripMenuItem)menuStrip1.Items[1];
            manageMenu.DropDownItems.Add("Thêm", null, (s,e) => AddProduct());
            manageMenu.DropDownItems.Add("Sửa", null, (s,e) => EditProduct());
            manageMenu.DropDownItems.Add("Xóa", null, (s,e) => DeleteProduct());
            manageMenu.DropDownItems.Add("Làm mới", null, (s,e) => RefreshProducts());
            manageMenu.DropDownItems.Add("Tìm kiếm", null, (s,e) => SearchProducts());
            var helpMenu = (ToolStripMenuItem)menuStrip1.Items[2];
            helpMenu.DropDownItems.Add("Giới thiệu", null, (s,e) =>
                MessageBox.Show("ProductManager - COMP1019 Lab 05", "Trợ giúp"));

            // ToolStrip
            toolStrip1.Items.Add(new ToolStripButton("Thêm", null, (s,e) => AddProduct()));
            toolStrip1.Items.Add(new ToolStripButton("Sửa", null, (s,e) => EditProduct()));
            toolStrip1.Items.Add(new ToolStripButton("Xóa", null, (s,e) => DeleteProduct()));
            toolStrip1.Items.Add(new ToolStripButton("Làm mới", null, (s,e) => RefreshProducts()));
            toolStrip1.Items.Add(new ToolStripButton("Tìm kiếm", null, (s,e) => SearchProducts()));
            toolStrip1.Dock = DockStyle.Top;

            // StatusStrip
            statusStrip1.Items.Add(lblStatus);
            statusStrip1.Items.Add(new ToolStripStatusLabel { Spring = true });
            statusStrip1.Items.Add(lblCount);
            lblStatus.Text = "Sẵn sàng";
            lblCount.Text = "Số lượng: 0";

            // GroupBox
            grpProduct.Text = "Thông tin sản phẩm";
            grpProduct.Location = new Point(10, 65);
            grpProduct.Size = new Size(1050, 220);

            lblMaSP.Text = "Mã sản phẩm:";
            lblMaSP.Location = new Point(20, 35);
            txtMaSP.Location = new Point(130, 32);
            txtMaSP.Size = new Size(220, 27);

            lblTenSP.Text = "Tên sản phẩm:";
            lblTenSP.Location = new Point(20, 75);
            txtTenSP.Location = new Point(130, 72);
            txtTenSP.Size = new Size(220, 27);

            lblLoaiSP.Text = "Loại sản phẩm:";
            lblLoaiSP.Location = new Point(20, 115);
            cboLoaiSP.Location = new Point(130, 112);
            cboLoaiSP.Size = new Size(220, 28);
            cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;

            lblDonGia.Text = "Đơn giá:";
            lblDonGia.Location = new Point(400, 35);
            numDonGia.Location = new Point(490, 32);
            numDonGia.Size = new Size(200, 27);
            numDonGia.Maximum = 1000000000;
            numDonGia.DecimalPlaces = 2;
            numDonGia.ThousandsSeparator = true;

            lblSoLuong.Text = "Số lượng:";
            lblSoLuong.Location = new Point(400, 75);
            numSoLuong.Location = new Point(490, 72);
            numSoLuong.Size = new Size(200, 27);
            numSoLuong.Maximum = 1000000;

            lblNgayNhap.Text = "Ngày nhập:";
            lblNgayNhap.Location = new Point(400, 115);
            dtpNgayNhap.Location = new Point(490, 112);
            dtpNgayNhap.Size = new Size(200, 27);
            dtpNgayNhap.Format = DateTimePickerFormat.Short;

            chkConKinhDoanh.Text = "Còn kinh doanh";
            chkConKinhDoanh.Checked = true;
            chkConKinhDoanh.Location = new Point(400, 155);
            chkConKinhDoanh.AutoSize = true;

            picProduct.Location = new Point(760, 30);
            picProduct.Size = new Size(130, 120);
            picProduct.BorderStyle = BorderStyle.FixedSingle;
            picProduct.SizeMode = PictureBoxSizeMode.Zoom;

            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.Location = new Point(760, 160);
            btnChonAnh.Size = new Size(130, 32);
            btnChonAnh.Click += (s,e) => ChooseImage();

            txtDuongDanAnh.Visible = false;

            grpProduct.Controls.AddRange(new Control[]
            {
                lblMaSP, txtMaSP, lblTenSP, txtTenSP, lblLoaiSP, cboLoaiSP,
                lblDonGia, numDonGia, lblSoLuong, numSoLuong, lblNgayNhap,
                dtpNgayNhap, chkConKinhDoanh, picProduct, btnChonAnh, txtDuongDanAnh
            });

            // DataGridView
            dgvProducts.Location = new Point(10, 295);
            dgvProducts.Size = new Size(1050, 300);
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ContextMenuStrip = contextMenuStrip1;
            dgvProducts.CellDoubleClick += (s,e) => EditProduct();
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ctxSua, ctxXoa, ctxChiTiet });
            ctxSua.Click += (s,e) => EditProduct();
            ctxXoa.Click += (s,e) => DeleteProduct();
            ctxChiTiet.Click += (s,e) => ShowDetail();

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.Title = "Chọn ảnh sản phẩm";

            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1075, 635);
            Controls.Add(dgvProducts);
            Controls.Add(grpProduct);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmProduct";
            Text = "Quản lý sản phẩm";
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
            KeyDown += FrmProduct_KeyDown;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
