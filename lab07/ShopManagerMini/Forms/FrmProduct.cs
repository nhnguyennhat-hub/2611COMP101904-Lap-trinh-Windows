using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public class FrmProduct : Form
    {
        private BindingList<Product> products = new BindingList<Product>();
        private BindingSource source = new BindingSource();

        private TextBox txtMaSP, txtTenSP;
        private ComboBox cboLoaiSP;
        private NumericUpDown numDonGia, numSoLuong;
        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTim;
        private DataGridView dgvProducts;

        public FrmProduct()
        {
            Text = "Quản lý sản phẩm";
            Size = new Size(950, 580);
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            Label title = new Label
            {
                Text = "QUẢN LÝ SẢN PHẨM",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            Label l1 = new Label { Text = "Mã sản phẩm:", Location = new Point(25, 65), AutoSize = true };
            Label l2 = new Label { Text = "Tên sản phẩm:", Location = new Point(25, 105), AutoSize = true };
            Label l3 = new Label { Text = "Loại sản phẩm:", Location = new Point(25, 145), AutoSize = true };
            Label l4 = new Label { Text = "Đơn giá:", Location = new Point(350, 65), AutoSize = true };
            Label l5 = new Label { Text = "Số lượng:", Location = new Point(350, 105), AutoSize = true };

            txtMaSP = new TextBox { Location = new Point(130, 62), Width = 180 };
            txtTenSP = new TextBox { Location = new Point(130, 102), Width = 180 };
            cboLoaiSP = new ComboBox { Location = new Point(130, 142), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });

            numDonGia = new NumericUpDown { Location = new Point(440, 62), Width = 180, Maximum = 1000000000, ThousandsSeparator = true };
            numSoLuong = new NumericUpDown { Location = new Point(440, 102), Width = 180, Maximum = 1000000 };

            btnThem = new Button { Text = "Thêm", Location = new Point(680, 60), Width = 90 };
            btnSua = new Button { Text = "Sửa", Location = new Point(775, 60), Width = 90 };
            btnXoa = new Button { Text = "Xóa", Location = new Point(680, 100), Width = 90 };
            btnLamMoi = new Button { Text = "Làm mới", Location = new Point(775, 100), Width = 90 };
            btnTim = new Button { Text = "Tìm kiếm", Location = new Point(680, 140), Width = 185 };

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (s, e) => { source.DataSource = products; ClearInput(); };
            btnTim.Click += BtnTim_Click;

            dgvProducts = new DataGridView
            {
                Name = "dgvProducts",
                Location = new Point(20, 205),
                Size = new Size(885, 300),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            Controls.AddRange(new Control[]
            {
                title, l1, l2, l3, l4, l5, txtMaSP, txtTenSP, cboLoaiSP,
                numDonGia, numSoLuong, btnThem, btnSua, btnXoa, btnLamMoi, btnTim, dgvProducts
            });
        }

        private void LoadData()
        {
            products.Add(new Product { MaSP = "SP001", TenSP = "Laptop Dell", LoaiSP = "Điện tử", DonGia = 18500000, SoLuong = 5 });
            products.Add(new Product { MaSP = "SP002", TenSP = "Bút bi", LoaiSP = "Văn phòng", DonGia = 5000, SoLuong = 100 });
            products.Add(new Product { MaSP = "SP003", TenSP = "Nồi cơm điện", LoaiSP = "Gia dụng", DonGia = 950000, SoLuong = 10 });

            source.DataSource = products;
            dgvProducts.DataSource = source;
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (dgvProducts.Columns["MaSP"] != null) dgvProducts.Columns["MaSP"].HeaderText = "Mã SP";
            if (dgvProducts.Columns["TenSP"] != null) dgvProducts.Columns["TenSP"].HeaderText = "Tên SP";
            if (dgvProducts.Columns["LoaiSP"] != null) dgvProducts.Columns["LoaiSP"].HeaderText = "Loại SP";
            if (dgvProducts.Columns["DonGia"] != null)
            {
                dgvProducts.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvProducts.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
            if (dgvProducts.Columns["SoLuong"] != null) dgvProducts.Columns["SoLuong"].HeaderText = "Số lượng";
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Mã và tên sản phẩm không được để trống.", "Cảnh báo");
                return false;
            }

            if (numDonGia.Value < 0 || numSoLuong.Value < 0)
            {
                MessageBox.Show("Đơn giá và số lượng không được âm.", "Cảnh báo");
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string ma = txtMaSP.Text.Trim();
            if (products.Any(p => p.MaSP.Equals(ma, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại.", "Cảnh báo");
                return;
            }

            products.Add(new Product
            {
                MaSP = ma,
                TenSP = txtTenSP.Text.Trim(),
                LoaiSP = cboLoaiSP.Text,
                DonGia = numDonGia.Value,
                SoLuong = (int)numSoLuong.Value
            });

            source.ResetBindings(false);
            ClearInput();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is not Product p)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.");
                return;
            }

            if (!ValidateInput()) return;

            p.MaSP = txtMaSP.Text.Trim();
            p.TenSP = txtTenSP.Text.Trim();
            p.LoaiSP = cboLoaiSP.Text;
            p.DonGia = numDonGia.Value;
            p.SoLuong = (int)numSoLuong.Value;

            source.ResetBindings(false);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is not Product p)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            if (MessageBox.Show($"Xóa sản phẩm {p.TenSP}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                products.Remove(p);
                source.ResetBindings(false);
                ClearInput();
            }
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            string key = Prompt("Nhập mã hoặc tên sản phẩm:");
            if (string.IsNullOrWhiteSpace(key))
            {
                source.DataSource = products;
                return;
            }

            source.DataSource = products.Where(p =>
                p.MaSP.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                p.TenSP.Contains(key, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private string Prompt(string message)
        {
            using Form f = new Form { Text = "Tìm kiếm", Size = new Size(380, 140), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog };
            TextBox box = new TextBox { Location = new Point(15, 20), Width = 240 };
            Button ok = new Button { Text = "OK", Location = new Point(270, 18), Width = 70, DialogResult = DialogResult.OK };
            f.Controls.AddRange(new Control[] { new Label { Text = message, Location = new Point(15, 0), AutoSize = true }, box, ok });
            f.AcceptButton = ok;
            return f.ShowDialog(this) == DialogResult.OK ? box.Text : "";
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            dgvProducts.SelectionChanged += (s, a) =>
            {
                if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
                {
                    txtMaSP.Text = p.MaSP;
                    txtTenSP.Text = p.TenSP;
                    cboLoaiSP.Text = p.LoaiSP;
                    numDonGia.Value = p.DonGia;
                    numSoLuong.Value = p.SoLuong;
                }
            };
        }
    }
}
