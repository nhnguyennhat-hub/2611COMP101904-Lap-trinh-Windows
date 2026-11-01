using System.Drawing.Drawing2D;

namespace COMP1019_Lab08_SimplePaint;

public sealed class FrmPaint : Form
{
    private readonly ComboBox cboMode = new();
    private readonly NumericUpDown nudWidth = new();
    private readonly Button btnColor = new();
    private readonly Button btnUndo = new();
    private readonly Button btnClear = new();
    private readonly Button btnSave = new();
    private readonly Panel canvas = new();
    private readonly ToolStripStatusLabel lblStatus = new();
    private readonly List<DrawItem> drawItems = new();
    private Color drawColor = Color.Black;
    private DrawItem? currentItem;
    private bool isDrawing;

    public FrmPaint()
    {
        Text = "COMP1019 - Simple Paint";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(800, 550);
        Size = new Size(1100, 720);
        KeyPreview = true;
        BuildInterface();
        WireEvents();
    }

    private void BuildInterface()
    {
        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top, Height = 52, Padding = new Padding(8, 8, 8, 4),
            WrapContents = false, AutoScroll = true, BackColor = SystemColors.Control
        };
        toolbar.Controls.Add(new Label { Text = "Chế độ vẽ:", AutoSize = true, Margin = new Padding(3, 8, 3, 0) });
        cboMode.Name = "cboMode";
        cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMode.Width = 125;
        cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" });
        cboMode.SelectedIndex = 0;
        toolbar.Controls.Add(cboMode);
        toolbar.Controls.Add(new Label { Text = "Độ dày:", AutoSize = true, Margin = new Padding(10, 8, 3, 0) });
        nudWidth.Name = "nudWidth";
        nudWidth.Minimum = 1; nudWidth.Maximum = 20; nudWidth.Value = 3; nudWidth.Width = 60;
        toolbar.Controls.Add(nudWidth);

        btnColor.Name = "btnColor"; btnColor.Text = "Chọn màu"; btnColor.AutoSize = true;
        btnColor.BackColor = drawColor; btnColor.ForeColor = Color.White; toolbar.Controls.Add(btnColor);
        btnUndo.Name = "btnUndo"; btnUndo.Text = "Undo"; btnUndo.AutoSize = true; toolbar.Controls.Add(btnUndo);
        btnClear.Name = "btnClear"; btnClear.Text = "Clear"; btnClear.AutoSize = true; toolbar.Controls.Add(btnClear);
        btnSave.Name = "btnSave"; btnSave.Text = "Lưu PNG"; btnSave.AutoSize = true; toolbar.Controls.Add(btnSave);

        canvas.Name = "canvas"; canvas.Dock = DockStyle.Fill; canvas.BackColor = Color.White;
        canvas.BorderStyle = BorderStyle.FixedSingle; canvas.Cursor = Cursors.Cross; canvas.Margin = new Padding(10);

        var statusStrip = new StatusStrip();
        lblStatus.Text = "Tọa độ: (0, 0) | Sẵn sàng";
        statusStrip.Items.Add(lblStatus);

        Controls.Add(canvas);
        Controls.Add(toolbar);
        Controls.Add(statusStrip);
    }

    private void WireEvents()
    {
        canvas.Paint += Canvas_Paint;
        canvas.MouseDown += Canvas_MouseDown;
        canvas.MouseMove += Canvas_MouseMove;
        canvas.MouseUp += Canvas_MouseUp;
        btnColor.Click += (_, _) => ChooseColor();
        btnUndo.Click += (_, _) => Undo();
        btnClear.Click += (_, _) => ClearCanvas();
        btnSave.Click += (_, _) => SavePng();
        KeyDown += FrmPaint_KeyDown;
    }

    private void Canvas_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        foreach (DrawItem item in drawItems) DrawOneItem(e.Graphics, item);
        if (currentItem != null) DrawOneItem(e.Graphics, currentItem);
    }

    private static void DrawOneItem(Graphics graphics, DrawItem item)
    {
        using var pen = new Pen(item.Color, item.Width)
        {
            StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round
        };
        switch (item.Mode)
        {
            case DrawMode.Pen:
                if (item.Points.Count >= 2) graphics.DrawLines(pen, item.Points.ToArray());
                else if (item.Points.Count == 1)
                {
                    Point p = item.Points[0];
                    float r = Math.Max(1, item.Width / 2);
                    using var brush = new SolidBrush(item.Color);
                    graphics.FillEllipse(brush, p.X - r, p.Y - r, r * 2, r * 2);
                }
                break;
            case DrawMode.Line:
                graphics.DrawLine(pen, item.Start, item.End);
                break;
            case DrawMode.Rectangle:
            {
                Rectangle rect = MakeRectangle(item.Start, item.End);
                if (rect.Width > 0 && rect.Height > 0) graphics.DrawRectangle(pen, rect);
                break;
            }
            case DrawMode.Ellipse:
            {
                Rectangle rect = MakeRectangle(item.Start, item.End);
                if (rect.Width > 0 && rect.Height > 0) graphics.DrawEllipse(pen, rect);
                break;
            }
        }
    }

    private static Rectangle MakeRectangle(Point a, Point b) =>
        new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));

    private void Canvas_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;
        DrawMode mode = cboMode.SelectedIndex switch
        {
            1 => DrawMode.Line, 2 => DrawMode.Rectangle, 3 => DrawMode.Ellipse, _ => DrawMode.Pen
        };
        currentItem = new DrawItem(mode, e.Location, drawColor, (float)nudWidth.Value);
        isDrawing = true;
        canvas.Capture = true;
        canvas.Invalidate();
    }

    private void Canvas_MouseMove(object? sender, MouseEventArgs e)
    {
        lblStatus.Text = $"Tọa độ: ({e.X}, {e.Y}) | {(isDrawing ? "Đang vẽ..." : "Sẵn sàng")}";
        if (!isDrawing || currentItem == null) return;
        currentItem.End = e.Location;
        if (currentItem.Mode == DrawMode.Pen &&
            (currentItem.Points.Count == 0 || currentItem.Points[^1] != e.Location))
            currentItem.Points.Add(e.Location);
        canvas.Invalidate();
    }

    private void Canvas_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || !isDrawing || currentItem == null) return;
        currentItem.End = e.Location;
        if (currentItem.Mode == DrawMode.Pen &&
            (currentItem.Points.Count == 0 || currentItem.Points[^1] != e.Location))
            currentItem.Points.Add(e.Location);
        drawItems.Add(currentItem);
        currentItem = null;
        isDrawing = false;
        canvas.Capture = false;
        lblStatus.Text = $"Tọa độ: ({e.X}, {e.Y}) | Đã vẽ {drawItems.Count} đối tượng";
        canvas.Invalidate();
    }

    private void ChooseColor()
    {
        using var dialog = new ColorDialog { Color = drawColor, FullOpen = true };
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            drawColor = dialog.Color;
            btnColor.BackColor = drawColor;
            btnColor.ForeColor = drawColor.GetBrightness() < 0.5 ? Color.White : Color.Black;
            lblStatus.Text = $"Đã chọn màu: {drawColor.Name}";
        }
    }

    private void Undo()
    {
        if (drawItems.Count == 0) { lblStatus.Text = "Không có nét vẽ để Undo"; return; }
        drawItems.RemoveAt(drawItems.Count - 1);
        currentItem = null; isDrawing = false;
        canvas.Invalidate();
        lblStatus.Text = $"Đã Undo | Còn {drawItems.Count} đối tượng";
    }

    private void ClearCanvas()
    {
        if (drawItems.Count == 0 && currentItem == null) { lblStatus.Text = "Canvas đã trống"; return; }
        if (MessageBox.Show("Bạn có muốn xóa toàn bộ vùng vẽ không?", "Xác nhận Clear",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        drawItems.Clear(); currentItem = null; isDrawing = false;
        canvas.Invalidate();
        lblStatus.Text = "Đã xóa toàn bộ vùng vẽ";
    }

    private void SavePng()
    {
        using var dialog = new SaveFileDialog
        {
            Title = "Lưu vùng vẽ", Filter = "PNG Image|*.png",
            FileName = "SimplePaint.png", DefaultExt = "png", AddExtension = true
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            using var bitmap = new Bitmap(Math.Max(1, canvas.Width), Math.Max(1, canvas.Height));
            canvas.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bitmap.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            lblStatus.Text = "Đã lưu ảnh PNG";
            MessageBox.Show("Lưu ảnh thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không thể lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FrmPaint_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.Z) { Undo(); e.SuppressKeyPress = true; }
    }
}
