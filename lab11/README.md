# COMP1019 – Lab 11: ADO.NET và CRUD với SQL Server (Quản lý lớp học)

## Yêu cầu
- Visual Studio 2022 hoặc .NET 8 SDK, Windows.
- Đã chạy script buổi 10 (`01`, `02`) để có `EduManagerDB` với bảng KHOA, LOP, SINHVIEN.
- Package `Microsoft.Data.SqlClient` (đã khai báo trong `.csproj`, tự khôi phục khi build).

## Cách chạy
1. Mở `ClassManagerApp.csproj` trong Visual Studio → `F5` (hoặc `dotnet run`).
2. Nếu không dùng LocalDB, sửa `Server=` trong `DbConfig.cs`
   (ví dụ `.\SQLEXPRESS`).

## Cấu trúc code
| File | Vai trò |
|---|---|
| `DbConfig.cs` | Chuỗi kết nối |
| `LopValidator.cs` | Kiểm tra dữ liệu nhập (tách riêng để dễ test) |
| `LopRepository.cs` | SqlConnection/SqlCommand, câu SQL có tham số |
| `FrmClassManager.cs` | Giao diện + xử lý sự kiện |

## Chức năng
- Hiển thị LOP (kèm tên khoa, số sinh viên) lên DataGridView.
- Thêm / Sửa / Xóa / Tìm kiếm (LIKE theo mã hoặc tên lớp) bằng SQL có tham số.
- Kiểm tra rỗng, độ dài, chưa chọn khoa; kiểm tra trùng mã lớp (kiểm tra trước + bắt lỗi 2627/2601).
- Xóa lớp còn sinh viên: báo lỗi thân thiện, không crash (kiểm tra trước + bắt lỗi khóa ngoại 547).
- Mã lớp (khóa chính) bị khóa khi sửa.

## Test case
| # | Thao tác | Kết quả mong đợi |
|---|---|---|
| 1 | Mở form | Lưới hiện 3 lớp mẫu, combobox có 2 khoa |
| 2 | Thêm lớp hợp lệ `DH23CNTT01` | Báo thành công, lưới có thêm dòng |
| 3 | Thêm khi bỏ trống Mã lớp | Báo "Mã lớp không được để trống" |
| 4 | Thêm khi bỏ trống Tên lớp | Báo "Tên lớp không được để trống" |
| 5 | Thêm khi chưa chọn khoa | Báo "Vui lòng chọn khoa" |
| 6 | Thêm mã trùng `DH21CNTT01` | Báo mã đã tồn tại, không thêm |
| 7 | Chọn dòng, đổi tên lớp, bấm Sửa | Lưới cập nhật tên mới |
| 8 | Bấm Sửa khi chưa chọn dòng | Báo chọn lớp để sửa |
| 9 | Xóa lớp `DH21CNTT01` (còn sinh viên) | Báo còn N sinh viên, không xóa, không crash |
| 10 | Xóa lớp `DH23CNTT01` vừa thêm (chưa có SV) | Xác nhận → xóa thành công |
| 11 | Tìm `CNTT` | Chỉ hiện các lớp có chữ CNTT |
| 12 | Tìm chuỗi không tồn tại | Lưới rỗng; bấm "Hiện tất cả" để khôi phục |
| 13 | Tắt SQL Server / sai Server rồi mở form | Hiện hộp thoại lỗi, không crash |
