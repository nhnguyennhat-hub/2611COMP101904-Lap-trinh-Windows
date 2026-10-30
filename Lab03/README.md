# COMP1019 - LẬP TRÌNH TRÊN WINDOWS

## BÀI LAB 03: C# VÀ LẬP TRÌNH HƯỚNG ĐỐI TƯỢNG – QUẢN LÝ SINH VIÊN BẰNG CONSOLE

## 1. Giới thiệu & Mục tiêu

Bài thực hành nhằm xây dựng ứng dụng **Console Application** quản lý sinh viên bằng ngôn ngữ **C#** trên môi trường **Visual Studio**, áp dụng toàn diện các nguyên lý lập trình hướng đối tượng (OOP) và kỹ thuật xử lý dữ liệu hiện đại với các mục tiêu:

- Vận dụng kiến thức về Class, Object, Property, Constructor và tính Đóng gói (Encapsulation).
- Mô hình hóa quan hệ kế thừa (Inheritance): lớp `SinhVien` kế thừa từ lớp `Nguoi`.
- Quản lý tập hợp đối tượng bằng cấu trúc dữ liệu `List<SinhVien>`.
- Tổ chức kiến trúc phân tầng: tách biệt tầng giao diện (`Program.cs`) và tầng dịch vụ nghiệp vụ (`QuanLySinhVien.cs`).
- Ứng dụng các phương thức LINQ cơ bản (`Any`, `FirstOrDefault`, `Where`, `OrderByDescending`) để tìm kiếm, lọc và sắp xếp dữ liệu.
- Xử lý ngoại lệ và kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation), đảm bảo chương trình không bị dừng đột ngột khi nhập sai.

---

## 2. Yêu cầu hệ thống & Công cụ phát triển

- **IDE:** Microsoft Visual Studio 2026
- **Framework:** .NET 10.0 Console Application
- **Ngôn ngữ:** C#

---

## 3. Cấu trúc chương trình & Danh sách các lớp

| STT | Tên Class        | Thuộc tính / Phương thức chính                                                                        | Vai trò / Yêu cầu kỹ thuật                                                                                                               |
| :-: | :--------------- | :---------------------------------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------- |
|  1  | `Nguoi`          | `HoTen`, `NgaySinh`<br>`LayThongTin()` (virtual)                                                      | Là lớp cha (Base Class), chứa constructor khởi tạo và phương thức hiển thị có thể override.                                              |
|  2  | `SinhVien`       | `MaSinhVien`, `MaLop`, `DiemTrungBinh`<br>`XepLoai()`, `LayThongTin()` (override)                     | Kế thừa từ `Nguoi`, kiểm tra ràng buộc `DiemTrungBinh` từ 0 đến 10, phân loại học lực và ghi đè thông tin.                               |
|  3  | `QuanLySinhVien` | `Them()`, `SuaDiem()`, `Xoa()`, `TimTheoMa()`, `TimTheoTen()`, `SapXepTheoDiem()`, `LocSinhVienDat()` | Tầng Service đóng gói `List<SinhVien>`, thực hiện toàn bộ thao tác CRUD và xử lý LINQ, không để `Program` can thiệp trực tiếp danh sách. |
|  4  | `Program`        | `Main()`, `HienThiMenu()`, các hàm nhập liệu từ bàn phím                                              | Điều khiển luồng chương trình, điều hướng menu Console và bắt lỗi nhập liệu.                                                             |

---


## 5. Kết quả thực nghiệm

### 5.1. Thêm sinh viên mới từ bàn phím (Chức năng 1)

Nhập đầy đủ thông tin sinh viên hợp lệ: Mã SV `456`, Họ tên `Lê Hoàng Quân`, Ngày sinh `02/11/2007`, Lớp `12C3`, Điểm `10`.

![Thêm sinh viên]

### 5.2. Xuất toàn bộ danh sách sinh viên (Chức năng 2)

Danh sách được hiển thị dưới dạng bảng ngay ngắn, tự động tính cột xếp loại theo thang điểm.

![Xuất danh sách]


### 5.3. Tìm kiếm sinh viên theo mã (Chức năng 3)

Nhập mã sinh viên `A123` để tra cứu thông tin chính xác.

![Tìm theo mã]


### 5.4. Tìm kiếm sinh viên theo tên (Chức năng 4)

Nhập từ khóa tìm kiếm `"1"`, chương trình lọc ra tất cả sinh viên có họ tên chứa ký tự này.

![Tìm theo tên]


### 5.5. Cập nhật sửa điểm trung bình (Chức năng 5)

Nhập mã sinh viên `456`, chương trình hiển thị tên cùng điểm hiện tại (`10,0`), sau đó cho phép cập nhật điểm mới thành `9`.

![Sửa điểm trung bình]

### 5.6. Xóa sinh viên khỏi danh sách (Chức năng 6)

Nhập mã sinh viên cần xóa `1`. Hệ thống tìm kiếm theo mã và thực hiện xóa sinh viên ra khỏi bộ nhớ danh sách.

![Xóa sinh viên]


### 5.7. Sắp xếp danh sách theo điểm giảm dần bằng LINQ (Chức năng 7)

Áp dụng LINQ để sắp xếp lại thứ tự sinh viên theo điểm từ cao nhất xuống thấp nhất.

![Sắp xếp theo điểm]


### 5.8. Lọc danh sách sinh viên đạt (Điểm >= 5.0) bằng LINQ (Chức năng 8)

Lọc và xuất các sinh viên có điểm trung bình từ `5.0` trở lên thông qua phương thức `Where()` của LINQ.

![Lọc sinh viên đạt]

