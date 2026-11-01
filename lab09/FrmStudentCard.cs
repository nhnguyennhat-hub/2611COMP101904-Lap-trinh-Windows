using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace StudentCardApp
{
    /// <summary>Panel chống nhấp nháy khi vẽ lại preview.</summary>
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }

    /// <summary>Dữ liệu thẻ sinh viên tại thời điểm vẽ/in.</summary>
    public class StudentInfo
    {
        public string StudentId, FullName, ClassName, Faculty, Course, Gender;
        public DateTime BirthDate;
        public Image Photo;
    }

    public class FrmStudentCard : Form
    {
        // Kích thước logic của thẻ (tỉ lệ 85.6 x 54 mm). Mọi thứ vẽ theo đơn vị này rồi scale.
        private const float CardW = 856f, CardH = 540f;

        // ===== Controls =====
        private TextBox txtStudentId, txtFullName;
        private ComboBox cboClass, cboFaculty, cboCourse, cboGender;
        private DateTimePicker dtpBirthDate;
        private PictureBox picAvatar;
        private Button btnChooseImage, btnReset, btnPrintPreview, btnPrint, btnExit;
        private DoubleBufferedPanel pnlPreview;
        private OpenFileDialog ofdAvatar;
        private PrintDocument printDocCard;
        private PrintPreviewDialog ppdCard;
        private PrintDialog pdlCard;
        private ErrorProvider errProvider;

        private Image _avatar;   // bản sao trong bộ nhớ, không giữ khóa file

        public FrmStudentCard()
        {
            BuildUi();
            ResetForm();
        }

        #region Dựng giao diện
        private void BuildUi()
        {
            Text = "Thiết kế và in thẻ sinh viên - COMP1019 Lab 09";
            ClientSize = new Size(1040, 640);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10f);

            errProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

            // --- Vùng trái: nhập liệu ---
            var pnlLeft = new Panel { Dock = DockStyle.Left, Width = 430, Padding = new Padding(10) };

            var grpInfo = new GroupBox { Text = "Thông tin sinh viên", Left = 10, Top = 10, Width = 410, Height = 300 };
            txtStudentId = new TextBox { Name = "txtStudentId" };
            txtFullName = new TextBox { Name = "txtFullName" };
            cboClass = MakeCombo("cboClass", "DH21CNTT01", "DH21CNTT02", "DH22KTPM01", "DH22KTPM02", "DH23ATTT01");
            cboFaculty = MakeCombo("cboFaculty", "Công nghệ thông tin", "Kinh tế", "Ngoại ngữ", "Cơ khí", "Điện - Điện tử");
            cboCourse = MakeCombo("cboCourse", "K49", "K50", "K51", "K52");
            dtpBirthDate = new DateTimePicker { Name = "dtpBirthDate", Format = DateTimePickerFormat.Short, MaxDate = DateTime.Today };
            cboGender = MakeCombo("cboGender", "Nam", "Nữ", "Khác");

            AddRow(grpInfo, "Mã sinh viên (*)", txtStudentId, 30);
            AddRow(grpInfo, "Họ tên (*)", txtFullName, 63);
            AddRow(grpInfo, "Lớp", cboClass, 96);
            AddRow(grpInfo, "Khoa", cboFaculty, 129);
            AddRow(grpInfo, "Khóa học", cboCourse, 162);
            AddRow(grpInfo, "Ngày sinh", dtpBirthDate, 195);
            AddRow(grpInfo, "Giới tính", cboGender, 228);

            var grpPhoto = new GroupBox { Text = "Ảnh đại diện (*)", Left = 10, Top = 320, Width = 410, Height = 220 };
            picAvatar = new PictureBox
            {
                Name = "picAvatar", Left = 15, Top = 25, Width = 150, Height = 185,
                SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.WhiteSmoke
            };
            btnChooseImage = new Button { Name = "btnChooseImage", Text = "Chọn ảnh...", Left = 185, Top = 25, Width = 200, Height = 36 };
            var lblHint = new Label
            {
                Left = 185, Top = 70, Width = 205, Height = 60,
                Text = "Hỗ trợ JPG, JPEG, PNG, BMP. Ảnh giữ nguyên tỉ lệ (không méo).",
                ForeColor = Color.DimGray
            };
            grpPhoto.Controls.AddRange(new Control[] { picAvatar, btnChooseImage, lblHint });

            pnlLeft.Controls.AddRange(new Control[] { grpInfo, grpPhoto });

            // --- Vùng dưới: nút điều khiển ---
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60 };
            btnReset = MakeButton("btnReset", "Làm mới");
            btnPrintPreview = MakeButton("btnPrintPreview", "Xem trước in");
            btnPrint = MakeButton("btnPrint", "In thẻ");
            btnExit = MakeButton("btnExit", "Thoát");
            var flow = new FlowLayoutPanel { Dock = DockStyle.Right, Width = 540, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(5, 10, 10, 5) };
            flow.Controls.AddRange(new Control[] { btnExit, btnPrint, btnPrintPreview, btnReset });
            pnlBottom.Controls.Add(flow);

            // --- Vùng phải: preview ---
            var grpPreview = new GroupBox { Text = "Xem trước thẻ", Dock = DockStyle.Fill, Padding = new Padding(10) };
            pnlPreview = new DoubleBufferedPanel { Name = "pnlPreview", Dock = DockStyle.Fill, BackColor = Color.FromArgb(235, 235, 235) };
            grpPreview.Controls.Add(pnlPreview);

            Controls.Add(grpPreview);   // Fill phải add trước để docking đúng thứ tự
            Controls.Add(pnlBottom);
            Controls.Add(pnlLeft);

            // --- Component in ấn / ảnh ---
            ofdAvatar = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = false
            };
            printDocCard = new PrintDocument { DocumentName = "The sinh vien" };
            printDocCard.DefaultPageSettings.Landscape = true;
            ppdCard = new PrintPreviewDialog { Document = printDocCard, Width = 1000, Height = 700, UseAntiAlias = true };
            pdlCard = new PrintDialog { Document = printDocCard, UseEXDialog = true };

            // --- Sự kiện ---
            btnChooseImage.Click += btnChooseImage_Click;
            btnReset.Click += (s, e) => ResetForm();
            btnPrintPreview.Click += btnPrintPreview_Click;
            btnPrint.Click += btnPrint_Click;
            btnExit.Click += (s, e) => Close();
            pnlPreview.Paint += pnlPreview_Paint;
            printDocCard.PrintPage += printDocCard_PrintPage;

            txtStudentId.TextChanged += InputChanged;
            txtFullName.TextChanged += InputChanged;
            cboClass.SelectedIndexChanged += InputChanged;
            cboFaculty.SelectedIndexChanged += InputChanged;
            cboCourse.SelectedIndexChanged += InputChanged;
            cboGender.SelectedIndexChanged += InputChanged;
            dtpBirthDate.ValueChanged += InputChanged;

            FormClosed += (s, e) => { _avatar?.Dispose(); };
        }

        private static ComboBox MakeCombo(string name, params string[] items)
        {
            var c = new ComboBox { Name = name, DropDownStyle = ComboBoxStyle.DropDownList };
            c.Items.AddRange(items);
            return c;
        }

        private static Button MakeButton(string name, string text) =>
            new Button { Name = name, Text = text, Width = 120, Height = 38 };

        private static void AddRow(GroupBox grp, string label, Control input, int top)
        {
            grp.Controls.Add(new Label { Text = label, Left = 15, Top = top + 4, Width = 130, AutoSize = false });
            input.Left = 150; input.Top = top; input.Width = 240;
            grp.Controls.Add(input);
        }
        #endregion

        #region F1 - Chọn ảnh
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            if (ofdAvatar.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                // Đọc vào bộ nhớ rồi đóng stream ngay => không khóa file, đổi ảnh nhiều lần không lỗi.
                Image loaded;
                using (var fs = new FileStream(ofdAvatar.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var img = Image.FromStream(fs))
                    loaded = new Bitmap(img);

                SetAvatar(loaded);
                errProvider.SetError(btnChooseImage, "");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is IOException || ex is OutOfMemoryException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Không thể mở ảnh đã chọn.\n" + ex.Message, "Lỗi ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetAvatar(Image img)
        {
            picAvatar.Image = null;        // gỡ tham chiếu trước khi dispose
            _avatar?.Dispose();
            _avatar = img;
            picAvatar.Image = _avatar;     // SizeMode = Zoom => giữ tỉ lệ
            pnlPreview.Invalidate();
        }
        #endregion

        #region F2 - Preview
        private void InputChanged(object sender, EventArgs e) => pnlPreview.Invalidate();

        private StudentInfo GetStudent() => new StudentInfo
        {
            StudentId = txtStudentId.Text.Trim(),
            FullName = txtFullName.Text.Trim(),
            ClassName = cboClass.Text,
            Faculty = cboFaculty.Text,
            Course = cboCourse.Text,
            Gender = cboGender.Text,
            BirthDate = dtpBirthDate.Value.Date,
            Photo = _avatar
        };

        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(pnlPreview.BackColor);
            var cardRect = FitRect(pnlPreview.ClientRectangle, CardW / CardH, 20);

            if (!IsValid(false))
            {
                using (var f = new Font("Segoe UI", 12f, FontStyle.Italic))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    g.DrawString("Nhập Mã sinh viên, Họ tên và chọn ảnh\nđể xem trước thẻ.", f, Brushes.DimGray, pnlPreview.ClientRectangle, sf);
                return;
            }
            DrawCard(g, cardRect, GetStudent());
        }

        /// <summary>Rect có tỉ lệ cho trước, căn giữa trong vùng container.</summary>
        private static RectangleF FitRect(Rectangle container, float ratio, float margin)
        {
            float w = container.Width - 2 * margin, h = container.Height - 2 * margin;
            if (w <= 0 || h <= 0) return RectangleF.Empty;
            if (w / h > ratio) w = h * ratio; else h = w / ratio;
            return new RectangleF(container.X + (container.Width - w) / 2, container.Y + (container.Height - h) / 2, w, h);
        }
        #endregion

        #region Vẽ thẻ bằng GDI+ (dùng chung cho Panel và PrintDocument)
        private static void DrawCard(Graphics g, RectangleF target, StudentInfo s)
        {
            if (target.Width <= 0 || target.Height <= 0) return;
            var state = g.Save();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TranslateTransform(target.X, target.Y);
            g.ScaleTransform(target.Width / CardW, target.Height / CardH);

            var navy = Color.FromArgb(0, 51, 102);
            var card = new RectangleF(0, 0, CardW, CardH);

            // Nền gradient nhẹ
            using (var bg = new LinearGradientBrush(card, Color.FromArgb(232, 242, 255), Color.White, 60f))
                g.FillRectangle(bg, card);

            // Header: tên khoa
            using (var hb = new SolidBrush(navy))
                g.FillRectangle(hb, 0, 0, CardW, 110);
            using (var f1 = new Font("Segoe UI", 18f, FontStyle.Regular))
            using (var f2 = new Font("Segoe UI", 24f, FontStyle.Bold))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter })
            {
                g.DrawString("TRƯỜNG ĐẠI HỌC", f1, Brushes.White, new RectangleF(10, 8, CardW - 20, 40), sf);
                g.DrawString("KHOA " + s.Faculty.ToUpper(), f2, Brushes.White, new RectangleF(10, 50, CardW - 20, 50), sf);
            }

            // Tiêu đề
            using (var ft = new Font("Segoe UI", 28f, FontStyle.Bold))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var tb = new SolidBrush(Color.FromArgb(180, 30, 30)))
                g.DrawString("THẺ SINH VIÊN", ft, tb, new RectangleF(0, 115, CardW, 55), sf);

            // Ảnh 3x4 bên trái, giữ tỉ lệ ảnh gốc (aspect-fit)
            var photoBox = new RectangleF(40, 185, 240, 320);
            g.FillRectangle(Brushes.White, photoBox);
            if (s.Photo != null)
            {
                float k = Math.Min(photoBox.Width / s.Photo.Width, photoBox.Height / s.Photo.Height);
                float w = s.Photo.Width * k, h = s.Photo.Height * k;
                g.DrawImage(s.Photo, new RectangleF(photoBox.X + (photoBox.Width - w) / 2, photoBox.Y + (photoBox.Height - h) / 2, w, h));
            }
            using (var pp = new Pen(navy, 3f))
                g.DrawRectangle(pp, photoBox.X, photoBox.Y, photoBox.Width, photoBox.Height);

            // Thông tin bên phải
            string[] labels = { "Mã SV:", "Họ tên:", "Ngày sinh:", "Giới tính:", "Lớp:", "Khóa:" };
            string[] values = { s.StudentId, s.FullName.ToUpper(), s.BirthDate.ToString("dd/MM/yyyy"), s.Gender, s.ClassName, s.Course };
            using (var fl = new Font("Segoe UI", 20f, FontStyle.Bold))
            using (var fv = new Font("Segoe UI", 20f, FontStyle.Regular))
            using (var lb = new SolidBrush(navy))
            using (var sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                float y = 190;
                for (int i = 0; i < labels.Length; i++, y += 52)
                {
                    g.DrawString(labels[i], fl, lb, new RectangleF(310, y, 170, 44), sf);
                    g.DrawString(values[i], fv, Brushes.Black, new RectangleF(480, y, 360, 44), sf);
                }
            }

            // Khung ngoài
            using (var border = new Pen(navy, 8f))
                g.DrawRectangle(border, 4, 4, CardW - 8, CardH - 8);

            g.Restore(state);
        }
        #endregion

        #region F3 - Validation
        /// <summary>Kiểm tra dữ liệu bắt buộc. showError = true thì hiện thông báo và focus control lỗi.</summary>
        private bool IsValid(bool showError)
        {
            string msg = null; Control focus = null;

            if (string.IsNullOrWhiteSpace(txtStudentId.Text)) { msg = "Vui lòng nhập Mã sinh viên."; focus = txtStudentId; }
            else if (string.IsNullOrWhiteSpace(txtFullName.Text)) { msg = "Vui lòng nhập Họ tên."; focus = txtFullName; }
            else if (_avatar == null) { msg = "Vui lòng chọn ảnh đại diện."; focus = btnChooseImage; }

            if (showError)
            {
                errProvider.Clear();
                if (msg != null)
                {
                    errProvider.SetError(focus, msg);
                    MessageBox.Show(msg, "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    focus.Focus();
                }
            }
            return msg == null;
        }
        #endregion

        #region F4, F5 - In ấn
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (!IsValid(true)) return;
            ppdCard.ShowDialog(this);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!IsValid(true)) return;
            if (pdlCard.ShowDialog(this) == DialogResult.OK)
            {
                try { printDocCard.Print(); }
                catch (InvalidPrinterException ex)
                {
                    MessageBox.Show("Máy in không hợp lệ.\n" + ex.Message, "Lỗi in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void printDocCard_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Đơn vị in = 1/100 inch. Thẻ 85.6 x 54 mm ≈ 337 x 213; phóng 1.5 lần cho dễ nhìn.
            float w = 337f * 1.5f, h = w * CardH / CardW;
            var rect = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, w, h);
            DrawCard(e.Graphics, rect, GetStudent());
            e.HasMorePages = false;
        }
        #endregion

        #region F6 - Làm mới
        private void ResetForm()
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            cboClass.SelectedIndex = 0;
            cboFaculty.SelectedIndex = 0;
            cboCourse.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            dtpBirthDate.Value = new DateTime(2003, 1, 1);
            picAvatar.Image = null;
            _avatar?.Dispose();
            _avatar = null;
            errProvider.Clear();
            pnlPreview.Invalidate();
            txtStudentId.Focus();
        }
        #endregion
    }
}
