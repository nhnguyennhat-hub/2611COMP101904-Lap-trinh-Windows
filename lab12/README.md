# COMP1019 – Lab 12: Data Binding, Stored Procedure và Transaction

## Chạy chương trình
1. SSMS: mở `Database/QLSinhVienLab12.sql` → Execute (F5). Script tạo DB, 4 bảng, dữ liệu mẫu, 4 stored procedure (chạy lại được nhiều lần).
2. Visual Studio: mở `QLSinhVienLab12.csproj` → F5 (hoặc `dotnet run`).
3. Dùng SQL Express thay vì LocalDB: sửa `Server=` trong `DAL/Database.cs` (ví dụ `.\SQLEXPRESS`).

## Cấu trúc
```
Forms/FrmNhapDiemTheoLop.cs   Giao diện, binding, sự kiện
DAL/Database.cs               Connection string
DAL/KetQuaDAL.cs              Gọi stored procedure, SaveAll bằng SqlTransaction
Helpers/DiemValidator.cs      Kiểm tra điểm/ghi chú (không phụ thuộc UI)
Database/QLSinhVienLab12.sql  Script DB + dữ liệu mẫu + stored procedure
```

## Cách hoạt động
- ComboBox: `DataSource = DataTable` + `DisplayMember/ValueMember`.
- DataGridView: `DataTable → BindingSource → dgvKetQua`; chỉ cột Điểm, Ghi chú được sửa.
- **Lưu tất cả:** (1) validate toàn bộ bảng trước (rỗng / không phải số / ngoài 0–10 / ghi chú > 200 ký tự), dòng lỗi hiện biểu tượng lỗi; (2) `SaveAll` mở một `SqlTransaction`, gọi `sp_KetQua_Save` cho từng dòng; lỗi ở bất kỳ dòng nào → `Rollback`, ngược lại `Commit` và báo số dòng đã lưu.
- `sp_KetQua_Save`: UPDATE trước, nếu `@@ROWCOUNT = 0` thì INSERT.
- Mở rộng: lọc theo họ tên (escape ký tự đặc biệt), thống kê số SV / đã có điểm / điểm TB trên thanh trạng thái, hỏi xác nhận khi tải lại lúc còn thay đổi chưa lưu.

## Test case
| # | Thao tác | Kết quả mong đợi |
|---|---|---|
| 1 | Mở form | Hai ComboBox có đủ lớp và môn học |
| 2 | Bấm Tải danh sách khi chưa chọn | Báo chọn lớp và môn học |
| 3 | Chọn `Đại học CNTT 21-01` + `Lập trình C#`, tải | 3 SV; SV001 = 8.5, SV002 = 7, SV003 để trống |
| 4 | Nhập `abc` vào Điểm | Báo "Điểm phải là số", giữ tại ô (Esc để hủy) |
| 5 | Nhập `11` hoặc `-1` | Báo "Điểm phải nằm trong khoảng từ 0 đến 10" |
| 6 | Nhập `7.5` và `7,5` | Cả hai đều nhận là 7,5 |
| 7 | Để trống điểm của SV003, bấm Lưu tất cả | Báo dòng lỗi, không lưu dòng nào |
| 8 | Nhập đủ điểm hợp lệ, bấm Lưu tất cả | "Đã lưu thành công 3 dòng"; kiểm tra bảng KETQUA trong SSMS (SV003 được INSERT, SV001–002 được UPDATE) |
| 9 | Sửa điểm SV001 rồi lưu lại | Chỉ cập nhật, không sinh dòng trùng |
| 10 | Chọn môn khác của cùng lớp, tải | Điểm trống (mỗi môn có điểm riêng) |
| 11 | Gõ `An` vào ô lọc | Chỉ còn SV có chữ "An" trong tên |
| 12 | **Rollback:** lớp `Đại học Kinh tế 22-01` + `Kinh tế vi mô`, tải; trong SSMS chạy `DELETE FROM SINHVIEN WHERE MaSV='SV009'`; nhập đủ điểm, bấm Lưu | Lỗi khóa ngoại tại SV009, báo "đã ROLLBACK"; kiểm tra SV007, SV008 **không** bị đổi điểm |
| 13 | Tắt SQL Server rồi mở form | Hộp thoại lỗi kết nối, không crash |

(Sau test 12, chạy lại file SQL để khôi phục SV009.)
