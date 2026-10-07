# LAB 05 - WINDOWS FORMS CƠ BẢN

## COMP1019 - Lập trình trên Windows

### Thông tin
- Buổi: Buổi 5
- Chủ đề: Windows Forms cơ bản
- Tên ứng dụng: `CourseRegistrationApp`
- Công cụ: Visual Studio, C# Windows Forms
- Hình thức: Cá nhân

## 1. Mục tiêu
- Tạo project Windows Forms App bằng C#.
- Thiết kế giao diện bằng Form Designer, Toolbox và Properties.
- Sử dụng Label, TextBox, Button, ComboBox, RadioButton, CheckBox, DateTimePicker, NumericUpDown và GroupBox.
- Xử lý sự kiện Load, Click, SelectedIndexChanged và ValueChanged.
- Kiểm tra dữ liệu và hiển thị kết quả bằng MessageBox.

## 2. Mô tả
`CourseRegistrationApp` là ứng dụng đăng ký khóa học. Người dùng nhập thông tin học viên, chọn khóa học, hình thức học, số tháng và nhận phiếu đăng ký.

## 3. Khóa học
| Khóa học | Học phí/tháng |
|---|---:|
| C# WinForms cơ bản | 800.000 VNĐ |
| SQL Server cơ bản | 700.000 VNĐ |
| Web Frontend cơ bản | 750.000 VNĐ |
| Lập trình Python cơ bản | 650.000 VNĐ |

Công thức: `Tổng học phí = Học phí/tháng × Số tháng`.

## 4. Control
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
### Form Load
Nạp 4 khóa học, chọn khóa đầu tiên, Online mặc định, số tháng 1-12 và tính tổng ban đầu.

### Đăng ký
Kiểm tra họ tên, số điện thoại và khóa học. Sau đó hiển thị phiếu gồm họ tên, số điện thoại, ngày sinh, khóa học, hình thức, số tháng, tổng tiền và trạng thái email.

### Làm mới
Xóa họ tên/số điện thoại; ngày sinh về hôm nay; bỏ email; chọn khóa đầu tiên; chọn Online; số tháng = 1; focus vào họ tên.

### Thoát
Có hộp thoại xác nhận, chỉ đóng khi chọn Yes.

## 6. Sự kiện
- `Form1_Load`
- `cboKhoaHoc_SelectedIndexChanged`
- `numSoThang_ValueChanged`
- `btnDangKy_Click`
- `btnLamMoi_Click`
- `btnThoat_Click`

## 7. Cấu trúc project
```text
CourseRegistrationApp
├── CourseRegistrationApp.sln
├── README.md
├── images
└── CourseRegistrationApp
    ├── CourseRegistrationApp.csproj
    ├── Form1.cs
    ├── Form1.Designer.cs
    └── Program.cs
```

## 8. Cách chạy
1. Mở `CourseRegistrationApp.sln` bằng Visual Studio.
2. Đặt `CourseRegistrationApp` làm Startup Project nếu cần.
3. Nhấn `Ctrl + F5` hoặc `F5`.

## 9. Test mẫu
**Dữ liệu:** Nguyễn Văn A, 0901234567, 01/01/2005, C# WinForms cơ bản, Online, 2 tháng, nhận email.

**Kết quả:** Tổng tiền `1.600.000 VNĐ`, trạng thái đăng ký thành công.

Test thêm: để trống họ tên, để trống số điện thoại, đổi khóa học, đổi số tháng, Làm mới và Thoát.

## 10. Hình ảnh báo cáo
Sau khi chạy chương trình, chụp màn hình và đặt vào thư mục `images`, ví dụ:
- `images/giao-dien.png`
- `images/dang-ky.png`
- `images/ket-qua.png`
- `images/lam-moi.png`

## 11. Thông tin sinh viên
- MSSV: ........................................
- Họ và tên: ........................................
- Lớp: ........................................
- Nhóm: ........................................

## 12. Kết luận
Bài Lab 05 xây dựng ứng dụng Windows Forms đăng ký khóa học, đáp ứng yêu cầu về giao diện, đặt tên control, xử lý sự kiện, kiểm tra dữ liệu, tính học phí, đăng ký, làm mới và thoát chương trình.
