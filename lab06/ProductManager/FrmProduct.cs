using System.ComponentModel;
using System.Drawing;

namespace ProductManager;

public class FrmProduct : Form
{
    private readonly BindingList<Product> products = new();
    private readonly BindingSource source = new();
    private readonly ErrorProvider errors = new();

    private TextBox txtMaSP = new();
    private TextBox txtTenSP = new();
    private ComboBox cboLoaiSP = new();
    private NumericUpDown numDonGia = new();
    private NumericUpDown numSoLuong = new();
    private DateTimePicker dtpNgayNhap = new();
    private CheckBox chkConKinhDoanh = new();
    private DataGridView dgvProducts = new();
    private ToolStripStatusLabel lblStatus = new();
    private ToolStripStatusLabel lblCount = new();
    private PictureBox picAnh = new();
    private string duongDanAnh = "";
    private int selectedIndex = -1;

    public FrmProduct()
    {
        Text = "Quản lý sản phẩm";
        Name = "FrmProduct";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1050, 680);
        Size = new Size(1250, 780);
        BuildInterface();
        LoadSampleData();
        source.DataSource = products;
        dgvProducts.DataSource = source;
        UpdateStatus("Sẵn sàng");
        dgvProducts.SelectionChanged += (_, _) => {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
                selectedIndex = products.IndexOf(p);
        };
        FormClosing += (_, _) => errors.Dispose();
    }

    private void BuildInterface()
    {
        var menu = new MenuStrip();
        var fileMenu = new ToolStripMenuItem("File");
        fileMenu.DropDownItems.Add("Làm mới", null, (_, _) => ClearInput());
        fileMenu.DropDownItems.Add("Thoát", null, (_, _) => Close());
        var manageMenu = new ToolStripMenuItem("Quản lý");
        manageMenu.DropDownItems.Add("Thêm", null, (_, _) => AddProduct());
        manageMenu.DropDownItems.Add("Sửa", null, (_, _) => EditProduct());
        manageMenu.DropDownItems.Add("Xóa", null, (_, _) => DeleteProduct());
        manageMenu.DropDownItems.Add("Tìm kiếm", null, (_, _) => SearchProduct());
        manageMenu.DropDownItems.Add("Hiển thị tất cả", null, (_, _) => ShowAll());
        var helpMenu = new ToolStripMenuItem("Trợ giúp");
        helpMenu.DropDownItems.Add("Thông tin", null, (_, _) =>
            MessageBox.Show("ProductManager - COMP1019 Lab 05\nQuản lý sản phẩm trong bộ nhớ.",
                "Trợ giúp", MessageBoxButtons.OK, MessageBoxIcon.Information));
        menu.Items.AddRange(new ToolStripItem[] { fileMenu, manageMenu, helpMenu });
        MainMenuStrip = menu;
        Controls.Add(menu);

        var tool = new ToolStrip { Dock = DockStyle.Top };
        AddToolButton(tool, "Thêm", AddProduct);
        AddToolButton(tool, "Sửa", EditProduct);
        AddToolButton(tool, "Xóa", DeleteProduct);
        AddToolButton(tool, "Làm mới", ClearInput);
        AddToolButton(tool, "Tìm kiếm", SearchProduct);
        Controls.Add(tool);

        var status = new StatusStrip();

        lblStatus.Text = "Sẵn sàng";
        lblStatus.Spring = true;
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;

        lblCount.Text = "Số sản phẩm: 0";

        status.Items.Add(lblStatus);
        status.Items.Add(lblCount);

        Controls.Add(status);

        var layout = new TableLayoutPanel {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(10)
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 255));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        Controls.Add(layout);
        layout.BringToFront();

        var group = new GroupBox { Text = "Thông tin sản phẩm", Dock = DockStyle.Fill, Padding = new Padding(10) };
        layout.Controls.Add(group, 0, 0);

        var grid = new TableLayoutPanel {
            Dock = DockStyle.Fill, ColumnCount = 6, RowCount = 4, Padding = new Padding(4)
        };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        for (int i = 0; i < 4; i++) grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        group.Controls.Add(grid);

        txtMaSP.Name = "txtMaSP";
        txtTenSP.Name = "txtTenSP";
        cboLoaiSP.Name = "cboLoaiSP";
        cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });
        cboLoaiSP.SelectedIndex = 0;
        numDonGia.Name = "numDonGia";
        numDonGia.Maximum = 1000000000;
        numDonGia.DecimalPlaces = 2;
        numDonGia.ThousandsSeparator = true;
        numSoLuong.Name = "numSoLuong";
        numSoLuong.Maximum = 1000000;
        dtpNgayNhap.Name = "dtpNgayNhap";
        dtpNgayNhap.MaxDate = DateTime.Today;
        chkConKinhDoanh.Name = "chkConKinhDoanh";
        chkConKinhDoanh.Text = "Còn kinh doanh";
        chkConKinhDoanh.Checked = true;
        picAnh.Name = "picAnh";
        picAnh.Dock = DockStyle.Fill;
        picAnh.SizeMode = PictureBoxSizeMode.Zoom;
        picAnh.BorderStyle = BorderStyle.FixedSingle;

        AddField(grid, "Mã sản phẩm", txtMaSP, 0, 0);
        AddField(grid, "Tên sản phẩm", txtTenSP, 2, 0);
        AddField(grid, "Loại sản phẩm", cboLoaiSP, 0, 1);
        AddField(grid, "Đơn giá", numDonGia, 2, 1);
        AddField(grid, "Số lượng", numSoLuong, 0, 2);
        AddField(grid, "Ngày nhập", dtpNgayNhap, 2, 2);
        grid.Controls.Add(chkConKinhDoanh, 1, 3);
        grid.SetColumnSpan(chkConKinhDoanh, 2);

        var imagePanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };
        imagePanel.Controls.Add(picAnh);
        picAnh.Size = new Size(130, 110);
        var btnImage = new Button { Text = "Chọn ảnh", AutoSize = true };
        btnImage.Click += (_, _) => ChooseImage();
        imagePanel.Controls.Add(btnImage);
        grid.Controls.Add(new Label { Text = "Ảnh", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 4, 0);
        grid.Controls.Add(imagePanel, 5, 0);
        grid.SetRowSpan(imagePanel, 4);

        dgvProducts.Name = "dgvProducts";
        dgvProducts.Dock = DockStyle.Fill;
        dgvProducts.ReadOnly = true;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.MultiSelect = false;
        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.RowHeadersVisible = false;
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSP", HeaderText = "Mã SP" });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSP", HeaderText = "Tên sản phẩm" });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoaiSP", HeaderText = "Loại" });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "Số lượng" });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayNhap", HeaderText = "Ngày nhập", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
        dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "ConKinhDoanh", HeaderText = "Đang bán" });
        layout.Controls.Add(dgvProducts, 0, 1);

        var context = new ContextMenuStrip();
        context.Items.Add("Sửa", null, (_, _) => EditProduct());
        context.Items.Add("Xóa", null, (_, _) => DeleteProduct());
        context.Items.Add("Xem chi tiết", null, (_, _) => ViewDetails());
        dgvProducts.ContextMenuStrip = context;
        dgvProducts.CellMouseDown += (_, e) => {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0) {
                dgvProducts.ClearSelection();
                dgvProducts.Rows[e.RowIndex].Selected = true;
                dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[0];
            }
        };

        errors.ContainerControl = this;
        KeyPreview = true;
        KeyDown += (_, e) => {
            if (e.Control && e.KeyCode == Keys.N) { AddProduct(); e.SuppressKeyPress = true; }
            if (e.KeyCode == Keys.Delete) DeleteProduct();
            if (e.KeyCode == Keys.F5) ShowAll();
        };
    }

    private static void AddField(TableLayoutPanel grid, string label, Control input, int labelColumn, int row)
    {
        grid.Controls.Add(new Label { Text = label, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, labelColumn, row);
        input.Dock = DockStyle.Fill;
        grid.Controls.Add(input, labelColumn + 1, row);
    }

    private static void AddToolButton(ToolStrip tool, string text, Action action)
    {
        var button = new ToolStripButton(text);
        button.Click += (_, _) => action();
        tool.Items.Add(button);
    }

    private void LoadSampleData()
    {
        products.Add(new Product { MaSP = "SP001", TenSP = "Chuột không dây", LoaiSP = "Điện tử", DonGia = 250000, SoLuong = 20, NgayNhap = DateTime.Today.AddDays(-3), ConKinhDoanh = true });
        products.Add(new Product { MaSP = "SP002", TenSP = "Sổ tay", LoaiSP = "Văn phòng", DonGia = 35000, SoLuong = 100, NgayNhap = DateTime.Today.AddDays(-2), ConKinhDoanh = true });
        products.Add(new Product { MaSP = "SP003", TenSP = "Bình nước", LoaiSP = "Gia dụng", DonGia = 89000, SoLuong = 0, NgayNhap = DateTime.Today.AddDays(-1), ConKinhDoanh = false });
    }

    private bool ValidateInput()
    {
        errors.Clear();
        bool valid = true;
        if (string.IsNullOrWhiteSpace(txtMaSP.Text)) { errors.SetError(txtMaSP, "Mã sản phẩm không được rỗng."); valid = false; }
        if (string.IsNullOrWhiteSpace(txtTenSP.Text)) { errors.SetError(txtTenSP, "Tên sản phẩm không được rỗng."); valid = false; }
        if (numDonGia.Value <= 0) { errors.SetError(numDonGia, "Đơn giá phải lớn hơn 0."); valid = false; }
        if (dtpNgayNhap.Value.Date > DateTime.Today) { errors.SetError(dtpNgayNhap, "Ngày nhập không được lớn hơn hôm nay."); valid = false; }

        bool duplicate = products.Any(p =>
            p.MaSP.Equals(txtMaSP.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
            (selectedIndex < 0 || !ReferenceEquals(p, products[selectedIndex])));
        if (duplicate) { errors.SetError(txtMaSP, "Mã sản phẩm đã tồn tại."); valid = false; }

        if (!valid)
            MessageBox.Show("Vui lòng kiểm tra các trường được đánh dấu.", "Dữ liệu chưa hợp lệ",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return valid;
    }

    private Product ReadForm() => new()
    {
        MaSP = txtMaSP.Text.Trim(),
        TenSP = txtTenSP.Text.Trim(),
        LoaiSP = cboLoaiSP.Text,
        DonGia = numDonGia.Value,
        SoLuong = (int)numSoLuong.Value,
        NgayNhap = dtpNgayNhap.Value.Date,
        ConKinhDoanh = chkConKinhDoanh.Checked,
        DuongDanAnh = duongDanAnh
    };

    private void AddProduct()
    {
        selectedIndex = -1;
        if (!ValidateInput()) return;
        products.Add(ReadForm());
        source.ResetBindings(false);
        ClearInput();
        UpdateStatus("Đã thêm sản phẩm");
    }

    private void EditProduct()
    {
        if (!EnsureSelected()) return;
        if (!ValidateInput()) return;
        var updated = ReadForm();
        products[selectedIndex] = updated;
        source.ResetBindings(false);
        ClearInput();
        UpdateStatus("Đã cập nhật sản phẩm");
    }

    private bool EnsureSelected()
    {
        if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
        {
            selectedIndex = products.IndexOf(p);
            if (selectedIndex >= 0) return true;
        }
        MessageBox.Show("Vui lòng chọn một sản phẩm trong bảng.", "Chưa chọn sản phẩm",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    private void DeleteProduct()
    {
        if (!EnsureSelected()) return;
        var p = products[selectedIndex];
        if (MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm '{p.TenSP}' không?",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        products.RemoveAt(selectedIndex);
        source.ResetBindings(false);
        ClearInput();
        UpdateStatus("Đã xóa sản phẩm");
    }

    private void SearchProduct()
    {
        string key = txtMaSP.Text.Trim();
        if (key.Length == 0) key = txtTenSP.Text.Trim();
        if (key.Length == 0)
        {
            MessageBox.Show("Nhập mã hoặc tên sản phẩm vào ô tương ứng để tìm.", "Tìm kiếm",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var found = products.FirstOrDefault(p =>
            p.MaSP.Contains(key, StringComparison.OrdinalIgnoreCase) ||
            p.TenSP.Contains(key, StringComparison.OrdinalIgnoreCase));
        if (found == null)
        {
            MessageBox.Show("Không tìm thấy sản phẩm phù hợp.", "Tìm kiếm",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateStatus("Không tìm thấy sản phẩm");
            return;
        }
        source.Position = products.IndexOf(found);
        dgvProducts.CurrentCell = dgvProducts.Rows[products.IndexOf(found)].Cells[0];
        dgvProducts.Rows[products.IndexOf(found)].Selected = true;
        LoadProductToForm(found);
        UpdateStatus("Đã tìm thấy sản phẩm");
    }

    private void ShowAll()
    {
        source.DataSource = null;
        source.DataSource = products;
        dgvProducts.DataSource = source;
        UpdateStatus("Đã hiển thị toàn bộ sản phẩm");
    }

    private void LoadProductToForm(Product p)
    {
        selectedIndex = products.IndexOf(p);
        txtMaSP.Text = p.MaSP;
        txtTenSP.Text = p.TenSP;
        cboLoaiSP.SelectedItem = p.LoaiSP;
        numDonGia.Value = Math.Min(numDonGia.Maximum, Math.Max(numDonGia.Minimum, p.DonGia));
        numSoLuong.Value = p.SoLuong;
        dtpNgayNhap.Value = p.NgayNhap > DateTime.Today ? DateTime.Today : p.NgayNhap;
        chkConKinhDoanh.Checked = p.ConKinhDoanh;
        duongDanAnh = p.DuongDanAnh;
        ShowImage(duongDanAnh);
        errors.Clear();
    }

    private void ClearInput()
    {
        errors.Clear();
        txtMaSP.Clear();
        txtTenSP.Clear();
        cboLoaiSP.SelectedIndex = 0;
        numDonGia.Value = 0;
        numSoLuong.Value = 0;
        dtpNgayNhap.Value = DateTime.Today;
        chkConKinhDoanh.Checked = true;
        duongDanAnh = "";
        ShowImage("");
        selectedIndex = -1;
        dgvProducts.ClearSelection();
        UpdateStatus("Đã làm mới dữ liệu nhập");
    }

    private void ChooseImage()
    {
        using var dialog = new OpenFileDialog {
            Title = "Chọn ảnh sản phẩm",
            Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            duongDanAnh = dialog.FileName;
            ShowImage(duongDanAnh);
            UpdateStatus("Đã chọn ảnh sản phẩm");
        }
    }

    private void ShowImage(string path)
    {
        var old = picAnh.Image;
        picAnh.Image = null;
        old?.Dispose();
        if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var temp = Image.FromStream(stream);
            picAnh.Image = new Bitmap(temp);
        }
    }

    private void ViewDetails()
    {
        if (!EnsureSelected()) return;
        var p = products[selectedIndex];
        MessageBox.Show(
            $"Mã: {p.MaSP}\nTên: {p.TenSP}\nLoại: {p.LoaiSP}\nĐơn giá: {p.DonGia:N0}\n" +
            $"Số lượng: {p.SoLuong}\nNgày nhập: {p.NgayNhap:dd/MM/yyyy}\n" +
            $"Trạng thái: {(p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh")}\nẢnh: {p.DuongDanAnh}",
            "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void UpdateStatus(string message)
    {
        lblStatus.Text = "Trạng thái: " + message;
        lblCount.Text = "Số sản phẩm: " + products.Count;
    }
}
