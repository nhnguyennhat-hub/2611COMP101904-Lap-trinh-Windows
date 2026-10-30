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

### 5.1. Thêm sinh viên

![Thêm sinh viên](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/them%20sinh%20vien.png)

### 5.2. Xuất toàn bộ danh sách sinh viên

![Xuất danh sách](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/Xuat%20danh%20sach.png)


### 5.3. Tìm sinh viên theo mã

![Tìm theo mã](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/tim%20sinh%20vien%20theo%20ma.png)


### 5.4. Tìm sinh viên theo tên

![Tìm theo tên](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/tim%20sinh%20vien%20theo%20ten.png)


### 5.5. Cập nhật sửa điểm trung bình

![Sửa điểm trung bình](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/sua%20diem%20trung%20binh.png)

### 5.6. Xóa sinh viên khỏi danh sách

![Xóa sinh viên](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/xoa%20sinh%20vien.png)


### 5.7. Sắp xếp danh sách theo điểm giảm dần bằng LINQ (Chức năng 7)


![Sắp xếp theo điểm](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/sap%20sep%20diem%20giam%20dan.png)


### 5.8. Lọc danh sách sinh viên đạt (Điểm >= 5.0) bằng LINQ (Chức năng 8)

![Lọc sinh viên đạt](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/raw/main/Lab03/image/loc%20sinh%20vien%20dat.png)

