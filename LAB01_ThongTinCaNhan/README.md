# LAB 01 - ỨNG DỤNG THÔNG TIN CÁ NHÂN

## Công nghệ
- C#
- Windows Forms App
- Visual Studio
- .NET 8

## Chức năng
- Nhập họ tên, năm sinh, email.
- Chọn giới tính.
- Chọn khoa/lớp.
- Kiểm tra dữ liệu đầu vào.
- Hiển thị thông tin và tuổi.
- Xóa dữ liệu.
- Thoát có xác nhận.

## Thiết kế giao diện & Danh sách Controls

| STT | Loại Control  | Tên Control (`Name`) | Chức năng / Mô tả                         |
| :-: | :------------ | :------------------- | :---------------------------------------- |
|  1  | `Label`       | `lblTitle`           | Hiển thị tiêu đề "THÔNG TIN SINH VIÊN"    |
|  2  | `TextBox`     | `txtHoTen`           | Nhập họ và tên sinh viên                  |
|  3  | `TextBox`     | `txtNamSinh`         | Nhập năm sinh                             |
|  4  | `TextBox`     | `txtEmail`           | Nhập địa chỉ email                        |
|  5  | `RadioButton` | `radNam`, `radNu`    | Lựa chọn giới tính                        |
|  6  | `ComboBox`    | `cboKhoa`            | Danh sách khoa/ngành học                  |
|  7  | `Button`      | `btnHienThi`         | Kiểm tra dữ liệu và xuất thông tin        |
|  8  | `Button`      | `btnXoa`             | Xóa dữ liệu đã nhập về trạng thái ban đầu |
|  9  | `Button`      | `btnThoat`           | Mở hộp thoại xác nhận thoát chương trình  |
| 10  | `TextBox`     | `txtKetQua`          | Hiển thị kết quả tổng hợp sau khi xử lý   |
## Hình ảnh minh họa

### 1. Ảnh khởi tạo
![a](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/LAB01_ThongTinCaNhan/image/start.png))

### 2. Hiện thị thông tin
![b](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/LAB01_ThongTinCaNhan/image/end.png)

### 3. Danh sách nghành
![b](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows//main/LAB01_ThongTinCaNhan/image/chon%20khoa.png)

### 4. Lỗi họ tên
![Lỗi họ tên](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/LAB01_ThongTinCaNhan/image/end.png)

### 5. Lỗi năm sinh
![Lỗi năm sinh](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/LAB01_ThongTinCaNhan/image/loi%20email.png)

### 6. Lỗi email
![Lỗi e](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/LAB01_ThongTinCaNhan/image/loi%20nam%20sinh.png)

### 7. Lỗi giới tính

![Lỗi gt](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/LAB01_ThongTinCaNhan/image/loi%20gioi%20tinh.png)
