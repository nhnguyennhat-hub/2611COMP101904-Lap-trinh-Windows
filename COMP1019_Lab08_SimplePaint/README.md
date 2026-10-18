# COMP1019 - LAB 08 - Simple Paint

## 1. Thông tin

- Học phần: COMP1019 - Lập trình trên Windows
- Buổi: 8
- Chủ đề: Xây dựng ứng dụng Paint đơn giản bằng WinForms và GDI+
- Tên project: COMP1019_Lab08_SimplePaint
- Công nghệ: C# WinForms, .NET 8

## 2. Chức năng

Ứng dụng có các chức năng:

- Vẽ tự do bằng Pen.
- Vẽ đường thẳng bằng Line.
- Vẽ hình chữ nhật bằng Rectangle.
- Vẽ hình ellipse bằng Ellipse.
- Chọn màu bằng ColorDialog.
- Chọn độ dày nét từ 1 đến 20.
- Undo nét vẽ gần nhất.
- Clear toàn bộ vùng vẽ.
- Hiển thị tọa độ chuột.
- Vẽ lại trong Paint event.
- Lưu danh sách đối tượng bằng `List<DrawItem>`.
- Hỗ trợ Ctrl + Z để Undo.
- Lưu vùng vẽ thành file PNG.
## 3. Cách sử dụng

### Pen
Chọn `Pen`, giữ chuột trái và kéo để vẽ tự do.

### Line
Chọn `Line`, nhấn chuột tại điểm bắt đầu, kéo đến điểm kết thúc rồi thả chuột.

### Rectangle
Chọn `Rectangle`, kéo từ một góc đến góc đối diện.

### Ellipse
Chọn `Ellipse`, kéo để tạo hình ellipse.

Các hình chữ nhật và ellipse có thể được kéo theo mọi hướng.

### Chọn màu
Nhấn nút `Màu`, chọn màu trong ColorDialog rồi nhấn OK.

### Độ dày
Chọn giá trị từ 1 đến 20 ở ô `Độ dày`.

### Undo
Nhấn `Undo` hoặc dùng phím tắt `Ctrl + Z`.

### Clear
Nhấn `Clear` để xóa toàn bộ hình sau khi xác nhận.

### Lưu PNG
Nhấn `Lưu PNG`, chọn vị trí lưu và đặt tên file.

## 4. Kỹ thuật chính

Ứng dụng sử dụng:

- `Graphics`
- `Pen`
- `ColorDialog`
- `Paint`
- `MouseDown`
- `MouseMove`
- `MouseUp`
- `Invalidate()`
- `List<DrawItem>`

Các đối tượng cần vẽ được lưu trong `drawItems`.

Khi Panel cần vẽ lại, sự kiện `Paint` đọc danh sách `drawItems` và vẽ lại toàn bộ đối tượng. Vì vậy hình không bị mất khi resize cửa sổ hoặc cửa sổ bị che rồi hiện lại.

Các đối tượng `Pen`, `Brush`, `Graphics`, `Bitmap`, `ColorDialog` và `SaveFileDialog` được quản lý bằng `using` để giải phóng tài nguyên.

## 5. Cấu trúc project

```text
COMP1019_Lab08_SimplePaint
│
├── COMP1019_Lab08_SimplePaint.sln
├── README.md
│
└── COMP1019_Lab08_SimplePaint
    ├── COMP1019_Lab08_SimplePaint.csproj
    ├── Program.cs
    ├── FrmPaint.cs
    │
    └── Models
        └── DrawItem.cs
```
