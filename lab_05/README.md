# LAB 05 - WINDOWS FORMS CƠ BẢN

## COMP1019 - Lập trình trên Windows

### Thông tin
- Buổi: Buổi 5
- Chủ đề: Windows Forms cơ bản
- Công cụ: Visual Studio, C# Windows Forms
- Hình thức: Cá nhân

## 1. Mục tiêu
- Tạo project Windows Forms App bằng C#.
- Thiết kế giao diện bằng Form Designer, Toolbox và Properties.
- Sử dụng Label, TextBox, Button, ComboBox, RadioButton, CheckBox, DateTimePicker, NumericUpDown và GroupBox.
- Xử lý sự kiện Load, Click, SelectedIndexChanged và ValueChanged.
- Kiểm tra dữ liệu và hiển thị kết quả bằng MessageBox.
## 2. Khóa học
| Khóa học | Học phí/tháng |
|---|---:|
| C# WinForms cơ bản | 800.000 VNĐ |
| SQL Server cơ bản | 700.000 VNĐ |
| Web Frontend cơ bản | 750.000 VNĐ |
| Lập trình Python cơ bản | 650.000 VNĐ |

Công thức: `Tổng học phí = Học phí/tháng × Số tháng`.

## 3. Control
- `txtHoTen`: Họ tên
- `txtSoDienThoai`: Số điện thoại
- `dtpNgaySinh`: Ngày sinh
- `chkNhanEmail`: Nhận email
- `cboKhoaHoc`: Khóa học
- `radOnline`: Online
- `radOffline`: Trực tiếp
- `numSoThang`: Số tháng
- `lblTongTien`: Tổng học phí
- `btnDangKy`: Đăng ký
- `btnLamMoi`: Làm mới
- `btnThoat`: Thoát

## 5. Chức năng
### 5.1. Khởi tạo ứng dụng khi Form Load

![Giao diện khởi tạo](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/0bff9a8ccbd044497ecb7040e205c8bc5493ab8e/lab_05/images/Screenshot%202026-10-18%20081531.png)

_Mô tả: Giao diện form khởi tạo chuẩn với các giá trị mặc định._

---

### 5.2. Chọn ngày sinh qua DateTimePicker

![Chọn ngày sinh](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/ba4da225d4b429957cefcf2f7a84644dca31d83f/lab_05/images/Screenshot%202026-10-18%20081633.png)

_Mô tả: Dropdown lịch hiển thị khi chọn ngày sinh._

---

### 5.3. Danh sách lựa chọn khóa học trong ComboBox

![Danh sách khóa học](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/ba4da225d4b429957cefcf2f7a84644dca31d83f/lab_05/images/Screenshot%202026-10-18%20081712.png)

_Mô tả: Danh sách khóa học được nạp đầy đủ vào ComboBox._

---

### 5.4. Tự động tính học phí khi thay đổi thông tin

![Tính học phí tự động](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/lab_05/images/Screenshot%202026-10-18%20082052.png)

_Mô tả: Tổng học phí tự động nhân theo số tháng đăng ký._

---

### 5.5. Đăng ký thành công & Xuất phiếu đăng ký

![Phiếu đăng ký thành công](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/lab_05/images/Screenshot%202026-10-18%20091216.png)

_Mô tả: Hộp thoại xác nhận đăng ký thành công với đầy đủ các trường thông tin._

---

### 5.6. Kiểm tra hợp lệ: Bắt lỗi để trống số điện thoại

![Lỗi trống số điện thoại](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/lab_05/images/Screenshot%202026-10-18%20110947.png)

_Mô tả: Cảnh báo khi người dùng không điền số điện thoại._

---

### 5.7. Kiểm tra hợp lệ: Bắt lỗi để trống họ tên học viên

![Lỗi trống họ tên](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/lab_05/images/Screenshot%202026-10-18%20111023.png)

_Mô tả: Cảnh báo khi người dùng để trống họ tên._

---

### 5.8. Chức năng Thoát có xác nhận (Yes/No)

![Xác nhận thoát](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/lab_05/images/Screenshot%202026-10-18%20111051.png)

_Mô tả: Hộp thoại xác nhận trước khi đóng Form._

---
