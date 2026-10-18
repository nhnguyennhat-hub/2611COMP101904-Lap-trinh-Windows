using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public class FrmMain : Form
    {
        private User currentUser;
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblUser;
        private ToolStripStatusLabel lblRole;
        private ToolStripStatusLabel lblTime;
        private Timer timer;

        public FrmMain(User user)
        {
            currentUser = user;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "ShopManagerMini - Quản lý cửa hàng";
            WindowState = FormWindowState.Maximized;
            IsMdiContainer = true;
            StartPosition = FormStartPosition.CenterScreen;

            menuStrip = new MenuStrip();
            ToolStripMenuItem mnuSystem = new ToolStripMenuItem("Hệ thống");
            ToolStripMenuItem mnuCategory = new ToolStripMenuItem("Danh mục");
            ToolStripMenuItem mnuHelp = new ToolStripMenuItem("Trợ giúp");

            mnuSystem.DropDownItems.Add("Đăng xuất", null, (s, e) => Logout());
            mnuSystem.DropDownItems.Add("Thoát", null, (s, e) => Close());

            mnuCategory.DropDownItems.Add("Quản lý sản phẩm", null, (s, e) => OpenChild<FrmProduct>());
            mnuCategory.DropDownItems.Add("Quản lý khách hàng", null, (s, e) => OpenChild<FrmCustomer>());

            mnuHelp.DropDownItems.Add("Giới thiệu", null, (s, e) => OpenChild<FrmAbout>());

            menuStrip.Items.AddRange(new ToolStripItem[] { mnuSystem, mnuCategory, mnuHelp });
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            statusStrip = new StatusStrip();
            lblUser = new ToolStripStatusLabel("Người dùng: " + currentUser.FullName);
            lblRole = new ToolStripStatusLabel("Vai trò: " + currentUser.Role);
            lblTime = new ToolStripStatusLabel();
            ToolStripStatusLabel spacer = new ToolStripStatusLabel { Spring = true };
            statusStrip.Items.AddRange(new ToolStripItem[] { lblUser, lblRole, spacer, lblTime });
            Controls.Add(statusStrip);

            timer = new Timer { Interval = 1000 };
            timer.Tick += (s, e) => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timer.Start();

            FormClosing += FrmMain_FormClosing;
        }

        private void OpenChild<T>() where T : Form
        {
            Form existing = MdiChildren.FirstOrDefault(f => f is T);
            if (existing != null)
            {
                existing.Activate();
                return;
            }

            Form child;
            if (typeof(T) == typeof(FrmProduct))
                child = new FrmProduct();
            else if (typeof(T) == typeof(FrmCustomer))
                child = new FrmCustomer();
            else
                child = new FrmAbout();

            child.MdiParent = this;
            child.Show();
        }

        private void Logout()
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?",
                "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Hide();
                using (FrmLogin login = new FrmLogin())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        currentUser = login.LoggedInUser;
                        lblUser.Text = "Người dùng: " + currentUser.FullName;
                        lblRole.Text = "Vai trò: " + currentUser.Role;
                        Show();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
}
