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

Khi khởi động, ComboBox được nạp sẵn 4 khóa học, mặc định chọn khóa đầu tiên (_C# WinForms cơ bản_), hình thức mặc định là _Online_, số tháng mặc định là _1_ và tổng học phí ban đầu hiển thị là **800.000 VND**.

![Giao diện khởi tạo](lab_05/images/Screenshot 2026-10-18 081531.png)

_Mô tả: Giao diện form khởi tạo chuẩn với các giá trị mặc định._

---

### 5.2. Chọn ngày sinh qua DateTimePicker

Hỗ trợ giao diện lịch trực quan để chọn chính xác ngày tháng năm sinh của học viên.

![Chọn ngày sinh](images/02_datetimepicker.png)

_Mô tả: Dropdown lịch hiển thị khi chọn ngày sinh._

---

### 5.3. Danh sách lựa chọn khóa học trong ComboBox

ComboBox hiển thị đầy đủ danh sách 4 khóa học kèm đơn giá từng tháng.

![Danh sách khóa học](images/03_combobox_khoahoc.png)

_Mô tả: Danh sách khóa học được nạp đầy đủ vào ComboBox._

---

### 5.4. Tự động tính học phí khi thay đổi thông tin

Khi thay đổi hình thức sang _Offline (Trực tiếp)_ và tăng số tháng đăng ký lên **2 tháng**, nhãn tổng học phí tự động cập nhật:
$$800.000 \times 2 = \mathbf{1.600.000\,\text{VND}}$$

![Tính học phí tự động](images/04_tinh_hoc_phi_tu_dong.png)

_Mô tả: Tổng học phí tự động nhân theo số tháng đăng ký._

---

### 5.5. Đăng ký thành công & Xuất phiếu đăng ký

Khi nhập đầy đủ dữ liệu và nhấn nút **Đăng ký**, một hộp thoại `MessageBox` xuất hiện tổng hợp toàn bộ thông tin đăng ký khóa học của học viên.

![Phiếu đăng ký thành công](images/05_phieu_dang_ky_thanh_cong.png)

_Mô tả: Hộp thoại xác nhận đăng ký thành công với đầy đủ các trường thông tin._

---

### 5.6. Kiểm tra hợp lệ: Bắt lỗi để trống số điện thoại

Nếu bỏ trống ô số điện thoại và bấm Đăng ký, hệ thống kích hoạt hộp thoại cảnh báo: `Vui lòng nhập số điện thoại!`.

![Lỗi trống số điện thoại](images/06_loi_trong_sdt.png)

_Mô tả: Cảnh báo khi người dùng không điền số điện thoại._

---

### 5.7. Kiểm tra hợp lệ: Bắt lỗi để trống họ tên học viên

Nếu bỏ trống ô họ tên học viên, hệ thống phát hiện lỗi và cảnh báo: `Vui lòng nhập họ tên học viên!`.

![Lỗi trống họ tên](images/07_loi_trong_ho_ten.png)

_Mô tả: Cảnh báo khi người dùng để trống họ tên._

---

### 5.8. Chức năng Thoát có xác nhận (Yes/No)

Nhấn nút **Thoát**, một hộp thoại hỏi `Bạn có chắc chắn muốn thoát chương trình không?` hiển thị giúp tránh việc vô tình tắt ứng dụng.

![Xác nhận thoát](images/08_xac_nhan_thoat.png)

_Mô tả: Hộp thoại xác nhận trước khi đóng Form._

---
