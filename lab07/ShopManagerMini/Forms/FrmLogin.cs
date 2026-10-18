using System;
using System.Drawing;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public class FrmLogin : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        public User LoggedInUser { get; private set; }

        public FrmLogin()
        {
            Text = "Đăng nhập - ShopManagerMini";
            Size = new Size(420, 280);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label title = new Label
            {
                Text = "SHOP MANAGER MINI",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(105, 25)
            };

            Label lblUser = new Label { Text = "Username:", Location = new Point(45, 85), AutoSize = true };
            Label lblPass = new Label { Text = "Password:", Location = new Point(45, 125), AutoSize = true };

            txtUsername = new TextBox { Name = "txtUsername", Location = new Point(130, 82), Width = 220 };
            txtPassword = new TextBox { Name = "txtPassword", Location = new Point(130, 122), Width = 220, UseSystemPasswordChar = true };

            btnLogin = new Button { Text = "Đăng nhập", Location = new Point(130, 170), Width = 105 };
            btnExit = new Button { Text = "Thoát", Location = new Point(245, 170), Width = 105 };

            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            AcceptButton = btnLogin;
            CancelButton = btnExit;

            Controls.AddRange(new Control[] { title, lblUser, lblPass, txtUsername, txtPassword, btnLogin, btnExit });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ username và password.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "123")
            {
                LoggedInUser = new User { Username = "admin", FullName = "Quản trị viên", Role = "Admin" };
            }
            else if (username.Equals("user", StringComparison.OrdinalIgnoreCase) && password == "123")
            {
                LoggedInUser = new User { Username = "user", FullName = "Nhân viên bán hàng", Role = "User" };
            }
            else
            {
                MessageBox.Show("Username hoặc password không đúng.", "Đăng nhập thất bại",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
