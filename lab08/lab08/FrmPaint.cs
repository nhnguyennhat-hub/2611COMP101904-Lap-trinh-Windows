using System.Drawing;
using System.Drawing.Imaging;
using COMP1019_Lab08_SimplePaint.Models;

namespace COMP1019_Lab08_SimplePaint
{
    public class FrmPaint : Form
    {
        private ComboBox cboMode = null!;
        private NumericUpDown nudWidth = null!;
        private Button btnColor = null!;
        private Button btnUndo = null!;
        private Button btnClear = null!;
        private Button btnSave = null!;
        private Label lblStatus = null!;
        private Panel canvas = null!;

        private Color currentColor = Color.Black;
        private Point startPoint;
        private Point currentPoint;
        private bool isDrawing;

        private readonly List<DrawItem> drawItems = new List<DrawItem>();

        public FrmPaint()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void InitializeComponent()
        {
            Text = "COMP1019 - Simple Paint";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(800, 550);
            Size = new Size(1100, 700);
            KeyPreview = true;

            Panel toolbar = new Panel();
            toolbar.Dock = DockStyle.Top;
            toolbar.Height = 58;
            toolbar.Padding = new Padding(10);
            toolbar.BackColor = Color.WhiteSmoke;

            Label lblMode = new Label();
            lblMode.Text = "Chế độ:";
            lblMode.AutoSize = true;
            lblMode.Location = new Point(10, 20);

            cboMode = new ComboBox();
            cboMode.Name = "cboMode";
            cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" });
            cboMode.SelectedIndex = 0;
            cboMode.Location = new Point(70, 16);
            cboMode.Width = 120;

            Label lblWidth = new Label();
            lblWidth.Text = "Độ dày:";
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(210, 20);

            nudWidth = new NumericUpDown();
            nudWidth.Name = "nudWidth";
            nudWidth.Minimum = 1;
            nudWidth.Maximum = 20;
            nudWidth.Value = 3;
            nudWidth.Location = new Point(265, 16);
            nudWidth.Width = 65;

            btnColor = new Button();
            btnColor.Name = "btnColor";
            btnColor.Text = "Màu";
            btnColor.Location = new Point(350, 13);
            btnColor.Size = new Size(85, 32);
            btnColor.BackColor = currentColor;
            btnColor.ForeColor = Color.White;

            btnUndo = new Button();
            btnUndo.Name = "btnUndo";
            btnUndo.Text = "Undo";
            btnUndo.Location = new Point(450, 13);
            btnUndo.Size = new Size(85, 32);

            btnClear = new Button();
            btnClear.Name = "btnClear";
            btnClear.Text = "Clear";
            btnClear.Location = new Point(550, 13);
            btnClear.Size = new Size(85, 32);

            btnSave = new Button();
            btnSave.Name = "btnSave";
            btnSave.Text = "Lưu PNG";
            btnSave.Location = new Point(650, 13);
            btnSave.Size = new Size(95, 32);

            lblStatus = new Label();
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "X: 0   Y: 0";
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(770, 20);

            toolbar.Controls.Add(lblMode);
            toolbar.Controls.Add(cboMode);
            toolbar.Controls.Add(lblWidth);
            toolbar.Controls.Add(nudWidth);
            toolbar.Controls.Add(btnColor);
            toolbar.Controls.Add(btnUndo);
            toolbar.Controls.Add(btnClear);
            toolbar.Controls.Add(btnSave);
            toolbar.Controls.Add(lblStatus);

            canvas = new Panel();
            canvas.Name = "canvas";
            canvas.Dock = DockStyle.Fill;
            canvas.BackColor = Color.White;
            canvas.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(canvas);
            Controls.Add(toolbar);
        }

        private void SetupEvents()
        {
            canvas.Paint += Canvas_Paint;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseLeave += Canvas_MouseLeave;

            btnColor.Click += BtnColor_Click;
            btnUndo.Click += BtnUndo_Click;
            btnClear.Click += BtnClear_Click;
            btnSave.Click += BtnSave_Click;

            KeyDown += FrmPaint_KeyDown;
        }

        private void Canvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            startPoint = e.Location;
            currentPoint = e.Location;
            isDrawing = true;

            DrawMode mode = GetCurrentMode();

            if (mode == DrawMode.Pen)
            {
                DrawItem item = new DrawItem
                {
                    Mode = DrawMode.Pen,
                    Color = currentColor,
                    Width = (float)nudWidth.Value
                };

                item.Points.Add(e.Location);
                drawItems.Add(item);
            }

            UpdateStatus(e.Location);
        }

        private void Canvas_MouseMove(object? sender, MouseEventArgs e)
        {
            currentPoint = e.Location;
            UpdateStatus(e.Location);

            if (!isDrawing)
                return;

            if (GetCurrentMode() == DrawMode.Pen)
            {
                if (drawItems.Count > 0)
                {
                    DrawItem item = drawItems[^1];
                    item.Points.Add(e.Location);
                }

                canvas.Invalidate();
            }
            else
            {
                canvas.Invalidate();
            }
        }

        private void Canvas_MouseUp(object? sender, MouseEventArgs e)
        {
            if (!isDrawing || e.Button != MouseButtons.Left)
                return;

            currentPoint = e.Location;

            if (GetCurrentMode() != DrawMode.Pen)
            {
                DrawItem item = new DrawItem
                {
                    Mode = GetCurrentMode(),
                    Start = startPoint,
                    End = currentPoint,
                    Color = currentColor,
                    Width = (float)nudWidth.Value
                };

                drawItems.Add(item);
            }

            isDrawing = false;
            canvas.Invalidate();
        }

        private void Canvas_MouseLeave(object? sender, EventArgs e)
        {
            if (!isDrawing)
                lblStatus.Text = "X: -   Y: -";
        }

        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            foreach (DrawItem item in drawItems)
            {
                DrawItemOnCanvas(e.Graphics, item);
            }

            if (isDrawing && GetCurrentMode() != DrawMode.Pen)
            {
                DrawPreview(e.Graphics);
            }
        }

        private void DrawItemOnCanvas(Graphics g, DrawItem item)
        {
            using Pen pen = new Pen(item.Color, item.Width)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            if (item.Mode == DrawMode.Pen)
            {
                if (item.Points.Count == 1)
                {
                    Point p = item.Points[0];
                    float r = item.Width / 2;
                    using SolidBrush brush = new SolidBrush(item.Color);
                    g.FillEllipse(brush, p.X - r, p.Y - r, item.Width, item.Width);
                }
                else if (item.Points.Count > 1)
                {
                    g.DrawLines(pen, item.Points.ToArray());
                }
            }
            else if (item.Mode == DrawMode.Line)
            {
                g.DrawLine(pen, item.Start, item.End);
            }
            else if (item.Mode == DrawMode.Rectangle)
            {
                Rectangle rect = GetNormalizedRectangle(item.Start, item.End);
                g.DrawRectangle(pen, rect);
            }
            else if (item.Mode == DrawMode.Ellipse)
            {
                Rectangle rect = GetNormalizedRectangle(item.Start, item.End);
                g.DrawEllipse(pen, rect);
            }
        }

        private void DrawPreview(Graphics g)
        {
            using Pen pen = new Pen(currentColor, (float)nudWidth.Value)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };

            if (GetCurrentMode() == DrawMode.Line)
            {
                g.DrawLine(pen, startPoint, currentPoint);
            }
            else if (GetCurrentMode() == DrawMode.Rectangle)
            {
                g.DrawRectangle(pen, GetNormalizedRectangle(startPoint, currentPoint));
            }
            else if (GetCurrentMode() == DrawMode.Ellipse)
            {
                g.DrawEllipse(pen, GetNormalizedRectangle(startPoint, currentPoint));
            }
        }

        private Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);

            return new Rectangle(x, y, width, height);
        }

        private DrawMode GetCurrentMode()
        {
            return cboMode.SelectedIndex switch
            {
                0 => DrawMode.Pen,
                1 => DrawMode.Line,
                2 => DrawMode.Rectangle,
                3 => DrawMode.Ellipse,
                _ => DrawMode.Pen
            };
        }

        private void BtnColor_Click(object? sender, EventArgs e)
        {
            using ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = currentColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
                btnColor.BackColor = currentColor;

                btnColor.ForeColor =
                    currentColor.GetBrightness() < 0.5f ? Color.White : Color.Black;
            }
        }

        private void BtnUndo_Click(object? sender, EventArgs e)
        {
            if (drawItems.Count == 0)
            {
                MessageBox.Show(
                    "Không có nét vẽ để Undo.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            drawItems.RemoveAt(drawItems.Count - 1);
            canvas.Invalidate();
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            if (drawItems.Count == 0)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa toàn bộ nét vẽ?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                drawItems.Clear();
                canvas.Invalidate();
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG Image|*.png";
            dialog.FileName = "simple-paint.png";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using Bitmap bitmap = new Bitmap(
                Math.Max(canvas.ClientSize.Width, 1),
                Math.Max(canvas.ClientSize.Height, 1));

            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            foreach (DrawItem item in drawItems)
            {
                DrawItemOnCanvas(g, item);
            }

            bitmap.Save(dialog.FileName, ImageFormat.Png);

            MessageBox.Show(
                "Đã lưu ảnh PNG thành công.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FrmPaint_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                BtnUndo_Click(sender, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        private void UpdateStatus(Point point)
        {
            lblStatus.Text = $"X: {point.X}   Y: {point.Y}";
        }
    }
}
