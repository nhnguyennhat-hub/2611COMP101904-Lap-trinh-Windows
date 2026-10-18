using System.Drawing;
using System.Windows.Forms;

namespace ShopManagerMini.Forms
{
    public class FrmAbout : Form
    {
        public FrmAbout()
        {
            Text = "Giới thiệu";
            Size = new Size(500, 300);
            StartPosition = FormStartPosition.CenterScreen;

            Label info = new Label
            {
                Text = "SHOP MANAGER MINI\n\n" +
                       "Ứng dụng quản lý cửa hàng mini\n" +
                       "Môn học: COMP1019 - Lập trình trên Windows\n" +
                       "Buổi 7: SDI, MDI và ứng dụng nhiều Form\n\n" +
                       "Sinh viên: ____________________\n" +
                       "Lớp: __________________________",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(35, 35)
            };

            Controls.Add(info);
        }
    }
}
