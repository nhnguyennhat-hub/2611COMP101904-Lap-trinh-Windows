# ProductManager - COMP1019 LAB 06

## 1. Thông tin
- Môn: COMP1019 - Lập trình trên Windows
- Lab: 05 - Windows Forms nâng cao và DataGridView
- Ứng dụng: ProductManager
- Form chính: FrmProduct
- Tiêu đề: Quản lý sản phẩm

## 2. Chức năng
- Hiển thị dữ liệu sản phẩm bằng DataGridView.
- Thêm, sửa, xóa sản phẩm.
- Tìm kiếm theo mã hoặc tên, không phân biệt hoa thường.
- Làm mới dữ liệu.
- Kiểm tra dữ liệu bằng ErrorProvider.
- Chọn và hiển thị ảnh bằng OpenFileDialog + PictureBox.
- MenuStrip, ToolStrip, StatusStrip, ContextMenuStrip.
- BindingList + BindingSource.
- Phím tắt: Ctrl+F tìm kiếm, Delete xóa, F5 làm mới.

## 3. Dữ liệu
Dữ liệu chỉ lưu trong bộ nhớ, không kết nối SQL Server.

Có sẵn 3 sản phẩm mẫu:
- SP001 - Laptop Dell
- SP002 - Bàn phím cơ
- SP003 - Áo sơ mi

## 4. Lưu ý sửa sản phẩm
- Chọn một dòng trong DataGridView.
- Bấm `Sửa` để đưa dữ liệu lên form.
- Chỉnh sửa thông tin.
- Bấm `Sửa` lần nữa để lưu thay đổi.

## 5. Cấu trúc
- `ProductManager.sln`
- `ProductManager/ProductManager.csproj`
- `ProductManager/Program.cs`
- `ProductManager/Product.cs`
- `ProductManager/FrmProduct.cs`
- `ProductManager/FrmProduct.Designer.cs`
- `README.md`
