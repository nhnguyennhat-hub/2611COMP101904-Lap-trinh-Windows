# ProductManager - COMP1019 Lab 06

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


### Nhập thông tin


![Thêm sản phẩm mới](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/lab06/ProductManager/image/them%20san%20pham.png)


## Thêm sản phẩm mới

![Thêm thành công](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/lab06/ProductManager/image/sau%20khi%20them%20san%20pham.png)


### Xem chi tiết sản phẩm

![Chọn sản phẩm](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/lab06/ProductManager/image/xem%20chi%20tiet%20san%20pham.png)


### 4.4. Xóa sản phẩm có hộp thoại xác nhận

![Xác nhận xóa](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/lab06/ProductManager/image/hop%20thoai%20xoa%20san%20pham.png)


### 4.5. Chọn hình ảnh

![Chọn ảnh sản phẩm](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/lab06/ProductManager/image/them%20anh.png)
