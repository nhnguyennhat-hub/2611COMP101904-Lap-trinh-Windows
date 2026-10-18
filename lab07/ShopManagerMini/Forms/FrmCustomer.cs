using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public class FrmCustomer : Form
    {
        private BindingList<Customer> customers = new BindingList<Customer>();
        private BindingSource source = new BindingSource();
        private TextBox txtMaKH, txtHoTen, txtSDT;
        private ComboBox cboLoaiKH;
        private Button btnThem;
        private DataGridView dgvCustomers;

        public FrmCustomer()
        {
            Text = "Quản lý khách hàng";
            Size = new Size(850, 500);
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            Label title = new Label { Text = "QUẢN LÝ KHÁCH HÀNG", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };

            Label l1 = new Label { Text = "Mã khách hàng:", Location = new Point(20, 70), AutoSize = true };
            Label l2 = new Label { Text = "Họ tên:", Location = new Point(20, 110), AutoSize = true };
            Label l3 = new Label { Text = "Số điện thoại:", Location = new Point(350, 70), AutoSize = true };
            Label l4 = new Label { Text = "Loại khách:", Location = new Point(350, 110), AutoSize = true };

            txtMaKH = new TextBox { Location = new Point(125, 67), Width = 180 };
            txtHoTen = new TextBox { Location = new Point(125, 107), Width = 180 };
            txtSDT = new TextBox { Location = new Point(450, 67), Width = 180 };
            cboLoaiKH = new ComboBox { Location = new Point(450, 107), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiKH.Items.AddRange(new object[] { "Thường", "VIP" });

            btnThem = new Button { Text = "Thêm khách hàng", Location = new Point(665, 67), Width = 130 };
            btnThem.Click += BtnThem_Click;

            dgvCustomers = new DataGridView
            {
                Location = new Point(20, 170),
                Size = new Size(775, 260),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            Controls.AddRange(new Control[] { title, l1, l2, l3, l4, txtMaKH, txtHoTen, txtSDT, cboLoaiKH, btnThem, dgvCustomers });
        }

        private void LoadData()
        {
            customers.Add(new Customer { MaKH = "KH001", HoTen = "Nguyễn Văn An", SoDienThoai = "0900000001", LoaiKH = "Thường" });
            customers.Add(new Customer { MaKH = "KH002", HoTen = "Trần Thị Bình", SoDienThoai = "0900000002", LoaiKH = "VIP" });
            source.DataSource = customers;
            dgvCustomers.DataSource = source;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Mã khách hàng và họ tên không được để trống.");
                return;
            }

            if (customers.Any(c => c.MaKH.Equals(txtMaKH.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại.");
                return;
            }

            customers.Add(new Customer
            {
                MaKH = txtMaKH.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                LoaiKH = cboLoaiKH.Text
            });

            source.ResetBindings(false);
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            cboLoaiKH.SelectedIndex = -1;
        }
    }
}
