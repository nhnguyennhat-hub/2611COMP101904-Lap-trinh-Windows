# ProductManager - COMP1019 Lab 05

## Yêu cầu
- Windows
- Visual Studio 2022 (hoặc mới hơn)
- Workload .NET desktop development
- .NET 8 SDK (hoặc đổi TargetFramework trong file `.csproj` sang phiên bản Windows .NET đang cài)

## Cách chạy
1. Giải nén thư mục `ProductManager`.
2. Mở `ProductManager.csproj` bằng Visual Studio.
3. Chờ Visual Studio tải project.
4. Nhấn `F5` để chạy.

## Chức năng
- Dữ liệu mẫu 3 sản phẩm, lưu trong bộ nhớ.
- Thêm, sửa, xóa, tìm kiếm, hiển thị tất cả/làm mới.
- Kiểm tra dữ liệu bằng ErrorProvider.
- MenuStrip, ToolStrip, StatusStrip, ContextMenuStrip.
- DataGridView, chọn ảnh bằng OpenFileDialog.
- Phím tắt: Ctrl+N thêm (mở form để nhập), Delete xóa, F5 hiển thị tất cả.

Lưu ý: dữ liệu chỉ tồn tại trong lúc chương trình đang chạy; chưa kết nối SQL Server và chưa lưu dữ liệu vào file.
## Thực nghiệm

### Khởi tạo ứng dụng

![Giao diện khởi tạo](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/lab06/ProductManager/image/khoi%20tao.png)


### 4.2. Nhập thông tin và Thêm sản phẩm mới

Nhập thông tin sản phẩm mới vào GroupBox nhập liệu và bấm nút **Thêm**.

![Thêm sản phẩm mới](images/1792339130812_472893807751410465_4785306904529670105_f313de57a71b7e6b0e6e08a99fdf8400.jpg)

_Mô tả: Nhập dữ liệu sản phẩm mới trên form._

Sau khi thêm thành công, dữ liệu lập tức cập nhật lên DataGridView, thanh trạng thái báo thông báo thành công và cập nhật lại số lượng/tổng giá trị kho.

![Thêm thành công](images/1792339150931_472893807751410465_4785306904529670105_9c3849ab440ecaeb331cdaf755f9309f.jpg)

_Mô tả: Sản phẩm mới được cập nhật lên DataGridView và StatusStrip._

---

### 4.3. Chọn sản phẩm từ DataGridView để xem chi tiết

Click chọn một dòng bất kỳ trên DataGridView, dữ liệu tương ứng tự động đổ lên các control ở phần nhập liệu.

![Chọn sản phẩm](images/1792339167000_472893807751410465_4785306904529670105_13a9dcb7e5397ff59d69bb3d80fd3692.jpg)

_Mô tả: Chọn dòng dữ liệu trên bảng để đưa thông tin lên form._

---

### 4.4. Xóa sản phẩm có hộp thoại xác nhận

Chọn sản phẩm cần xóa và nhấn nút **Xóa** (hoặc dùng ContextMenuStrip/phím Delete), hệ thống kích hoạt hộp thoại `MessageBox` yêu cầu người dùng xác nhận trước khi xóa hẳn khỏi bộ nhớ.

![Xác nhận xóa](images/1792339181953_472893807751410465_4785306904529670105_a3faa6ac02a3bb3308404cc7864f8965.jpg)

_Mô tả: Hộp thoại xác nhận trước khi thực hiện xóa sản phẩm._

---

### 4.5. Chọn hình ảnh minh họa qua OpenFileDialog

Nhấn nút **Chọn ảnh...**, cửa sổ `OpenFileDialog` mở ra cho phép người dùng duyệt file ảnh từ máy tính để gán cho sản phẩm.

![Chọn ảnh sản phẩm](images/1792339201192_472893807751410465_4785306904529670105_694be04e1e0546e7f2692f736111f986.jpg)

_Mô tả: Hộp thoại OpenFileDialog nạp ảnh minh họa._

---
