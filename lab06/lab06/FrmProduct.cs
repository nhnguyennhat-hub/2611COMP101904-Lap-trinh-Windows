using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProductManager
{
    public partial class FrmProduct : Form
    {
        private BindingList<Product> products = new BindingList<Product>();
        private BindingSource bindingSource = new BindingSource();
        private bool isEditing = false;
        private string editingMaSP = "";

        public FrmProduct()
        {
            InitializeComponent();
            Load += FrmProduct_Load;
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.AddRange(new object[]
            {
                "Điện tử", "Văn phòng", "Gia dụng", "Thời trang"
            });

            products.Add(new Product
            {
                MaSP = "SP001", TenSP = "Laptop Dell", LoaiSP = "Điện tử",
                DonGia = 18500000, SoLuong = 5, NgayNhap = DateTime.Today.AddDays(-10),
                ConKinhDoanh = true, DuongDanAnh = ""
            });
            products.Add(new Product
            {
                MaSP = "SP002", TenSP = "Bàn phím cơ", LoaiSP = "Điện tử",
                DonGia = 850000, SoLuong = 12, NgayNhap = DateTime.Today.AddDays(-7),
                ConKinhDoanh = true, DuongDanAnh = ""
            });
            products.Add(new Product
            {
                MaSP = "SP003", TenSP = "Áo sơ mi", LoaiSP = "Thời trang",
                DonGia = 450000, SoLuong = 20, NgayNhap = DateTime.Today.AddDays(-3),
                ConKinhDoanh = true, DuongDanAnh = ""
            });

            bindingSource.DataSource = products;
            dgvProducts.DataSource = bindingSource;
            FormatGrid();
            ClearInput();
            UpdateStatus("Đã tải dữ liệu mẫu.");
        }

        private void FormatGrid()
        {
            if (dgvProducts.Columns["MaSP"] != null)
                dgvProducts.Columns["MaSP"].HeaderText = "Mã SP";
            if (dgvProducts.Columns["TenSP"] != null)
                dgvProducts.Columns["TenSP"].HeaderText = "Tên SP";
            if (dgvProducts.Columns["LoaiSP"] != null)
                dgvProducts.Columns["LoaiSP"].HeaderText = "Loại SP";
            if (dgvProducts.Columns["DonGia"] != null)
            {
                dgvProducts.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvProducts.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
            if (dgvProducts.Columns["SoLuong"] != null)
                dgvProducts.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvProducts.Columns["NgayNhap"] != null)
            {
                dgvProducts.Columns["NgayNhap"].HeaderText = "Ngày nhập";
                dgvProducts.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvProducts.Columns["ConKinhDoanh"] != null)
                dgvProducts.Columns["ConKinhDoanh"].HeaderText = "Còn kinh doanh";
            if (dgvProducts.Columns["DuongDanAnh"] != null)
                dgvProducts.Columns["DuongDanAnh"].HeaderText = "Đường dẫn ảnh";
        }

        private bool ValidateInput()
        {
            errorProvider1.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            {
                errorProvider1.SetError(txtMaSP, "Mã sản phẩm không được rỗng.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                errorProvider1.SetError(txtTenSP, "Tên sản phẩm không được rỗng.");
                valid = false;
            }

            if (cboLoaiSP.SelectedIndex < 0)
            {
                errorProvider1.SetError(cboLoaiSP, "Vui lòng chọn loại sản phẩm.");
                valid = false;
            }

            if (numDonGia.Value <= 0)
            {
                errorProvider1.SetError(numDonGia, "Đơn giá phải lớn hơn 0.");
                valid = false;
            }

            if (numSoLuong.Value < 0)
            {
                errorProvider1.SetError(numSoLuong, "Số lượng phải lớn hơn hoặc bằng 0.");
                valid = false;
            }

            if (dtpNgayNhap.Value.Date > DateTime.Today)
            {
                errorProvider1.SetError(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại.");
                valid = false;
            }

            string ma = txtMaSP.Text.Trim();
            bool duplicate = products.Any(p =>
                p.MaSP.Equals(ma, StringComparison.OrdinalIgnoreCase) &&
                (!isEditing || !p.MaSP.Equals(editingMaSP, StringComparison.OrdinalIgnoreCase)));

            if (duplicate)
            {
                errorProvider1.SetError(txtMaSP, "Mã sản phẩm đã tồn tại.");
                valid = false;
            }

            return valid;
        }

        private Product GetProductFromForm()
        {
            return new Product
            {
                MaSP = txtMaSP.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                LoaiSP = cboLoaiSP.Text,
                DonGia = numDonGia.Value,
                SoLuong = (int)numSoLuong.Value,
                NgayNhap = dtpNgayNhap.Value.Date,
                ConKinhDoanh = chkConKinhDoanh.Checked,
                DuongDanAnh = txtDuongDanAnh.Text
            };
        }

        private void AddProduct()
        {
            isEditing = false;
            if (!ValidateInput())
            {
                MessageBox.Show("Vui lòng kiểm tra lại dữ liệu nhập.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            products.Add(GetProductFromForm());
            bindingSource.ResetBindings(false);
            FormatGrid();
            ClearInput();
            UpdateStatus("Đã thêm sản phẩm.");
        }

        private void EditProduct()
        {
            if (isEditing && !string.IsNullOrWhiteSpace(editingMaSP))
            {
                SaveEdit();
                return;
            }

            if (dgvProducts.CurrentRow == null || dgvProducts.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product p = (Product)dgvProducts.CurrentRow.DataBoundItem;
            isEditing = true;
            editingMaSP = p.MaSP;
            LoadProductToForm(p);
            UpdateStatus("Đang chỉnh sửa " + p.MaSP);
        }

        private void SaveEdit()
        {
            if (!isEditing)
            {
                AddProduct();
                return;
            }

            if (!ValidateInput())
            {
                MessageBox.Show("Vui lòng kiểm tra lại dữ liệu nhập.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product old = products.FirstOrDefault(p =>
                p.MaSP.Equals(editingMaSP, StringComparison.OrdinalIgnoreCase));

            if (old != null)
            {
                Product updated = GetProductFromForm();
                old.MaSP = updated.MaSP;
                old.TenSP = updated.TenSP;
                old.LoaiSP = updated.LoaiSP;
                old.DonGia = updated.DonGia;
                old.SoLuong = updated.SoLuong;
                old.NgayNhap = updated.NgayNhap;
                old.ConKinhDoanh = updated.ConKinhDoanh;
                old.DuongDanAnh = updated.DuongDanAnh;
            }

            bindingSource.ResetBindings(false);
            FormatGrid();
            ClearInput();
            isEditing = false;
            editingMaSP = "";
            UpdateStatus("Đã cập nhật sản phẩm.");
        }

        private void DeleteProduct()
        {
            if (dgvProducts.CurrentRow == null || dgvProducts.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product p = (Product)dgvProducts.CurrentRow.DataBoundItem;
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm {p.MaSP} - {p.TenSP}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                products.Remove(p);
                bindingSource.ResetBindings(false);
                ClearInput();
                UpdateStatus("Đã xóa sản phẩm.");
            }
        }

        private void SearchProducts()
        {
            using Form inputForm = new Form();
            inputForm.Text = "Tìm kiếm sản phẩm";
            inputForm.Size = new Size(400, 150);
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MinimizeBox = false;
            inputForm.MaximizeBox = false;

            Label label = new Label { Text = "Nhập mã hoặc tên:", Location = new Point(15, 15), AutoSize = true };
            TextBox input = new TextBox { Location = new Point(15, 45), Width = 250 };
            Button ok = new Button { Text = "Tìm", Location = new Point(280, 43), Width = 80, DialogResult = DialogResult.OK };
            inputForm.Controls.Add(label);
            inputForm.Controls.Add(input);
            inputForm.Controls.Add(ok);
            inputForm.AcceptButton = ok;

            string keyword = inputForm.ShowDialog(this) == DialogResult.OK ? input.Text : "";

            if (string.IsNullOrWhiteSpace(keyword))
            {
                bindingSource.DataSource = products;
                UpdateStatus("Đã hiển thị toàn bộ sản phẩm.");
                return;
            }

            var result = products.Where(p =>
                p.MaSP.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase) ||
                p.TenSP.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();

            bindingSource.DataSource = result;
            UpdateStatus($"Tìm thấy {result.Count} sản phẩm.");
        }

        private void RefreshProducts()
        {
            bindingSource.DataSource = products;
            bindingSource.ResetBindings(false);
            ClearInput();
            UpdateStatus("Đã làm mới dữ liệu.");
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            dtpNgayNhap.Value = DateTime.Today;
            chkConKinhDoanh.Checked = true;
            txtDuongDanAnh.Clear();
            picProduct.Image = null;
            errorProvider1.Clear();
            isEditing = false;
            editingMaSP = "";
        }

        private void LoadProductToForm(Product p)
        {
            txtMaSP.Text = p.MaSP;
            txtTenSP.Text = p.TenSP;
            cboLoaiSP.SelectedItem = p.LoaiSP;
            numDonGia.Value = Math.Min(numDonGia.Maximum, p.DonGia);
            numSoLuong.Value = Math.Min(numSoLuong.Maximum, p.SoLuong);
            dtpNgayNhap.Value = p.NgayNhap <= DateTime.Today ? p.NgayNhap : DateTime.Today;
            chkConKinhDoanh.Checked = p.ConKinhDoanh;
            txtDuongDanAnh.Text = p.DuongDanAnh ?? "";

            if (!string.IsNullOrWhiteSpace(p.DuongDanAnh) && File.Exists(p.DuongDanAnh))
            {
                using var temp = Image.FromFile(p.DuongDanAnh);
                picProduct.Image = new Bitmap(temp);
            }
            else
            {
                picProduct.Image = null;
            }
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
            lblCount.Text = $"Số lượng: {products.Count}";
        }

        private void ChooseImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDuongDanAnh.Text = openFileDialog1.FileName;
                using var temp = Image.FromFile(openFileDialog1.FileName);
                picProduct.Image = new Bitmap(temp);
                UpdateStatus("Đã chọn ảnh sản phẩm.");
            }
        }

        private void ShowDetail()
        {
            if (dgvProducts.CurrentRow == null || dgvProducts.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product p = (Product)dgvProducts.CurrentRow.DataBoundItem;
            string detail =
                $"Mã SP: {p.MaSP}\n" +
                $"Tên SP: {p.TenSP}\n" +
                $"Loại SP: {p.LoaiSP}\n" +
                $"Đơn giá: {p.DonGia:N0}\n" +
                $"Số lượng: {p.SoLuong}\n" +
                $"Ngày nhập: {p.NgayNhap:dd/MM/yyyy}\n" +
                $"Trạng thái: {(p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh")}";

            MessageBox.Show(detail, "Chi tiết sản phẩm",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
            {
                if (!isEditing)
                    LoadProductToForm(p);
            }
        }

        private void FrmProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                SearchProducts();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteProduct();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                RefreshProducts();
                e.SuppressKeyPress = true;
            }
        }

        // Khi đang sửa, nút Sửa sẽ nạp dữ liệu lần đầu; lần bấm tiếp theo lưu.
        // Dùng double-click để nạp nhanh, sau đó bấm Sửa trên toolbar để lưu.
    }
}
