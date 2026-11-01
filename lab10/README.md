# COMP1019 – Lab 10: EduManagerDB (SQL Server)

## Cách chạy
Mở SQL Server Management Studio (SSMS), kết nối vào SQL Server, rồi chạy lần lượt:
1. `01_CreateDatabase.sql` – xóa DB cũ (nếu có), tạo `EduManagerDB` và 6 bảng.
2. `02_InsertSampleData.sql` – nhập dữ liệu mẫu.
3. `03_Queries.sql` – 12 truy vấn theo đề (bôi đen từng câu, nhấn F5).

Chạy lại từ đầu: chạy lại file 01 (đã có `SINGLE_USER ... ROLLBACK IMMEDIATE` để ngắt kết nối cũ).

## Thiết kế
```
KHOA (MaKhoa PK)
 └─< LOP (MaLop PK, MaKhoa FK)
      └─< SINHVIEN (MaSV PK, MaLop FK)
            └─< KETQUA (MaSV, MaMH  PK kép) >─┐
MONHOC (MaMH PK) ───────────────────────────────┘
TAIKHOAN (TenDangNhap PK)
```
| Ràng buộc | Tên |
|---|---|
| Điểm 0..10 | `CK_KETQUA_Diem` |
| Giới tính Nam/Nữ | `CK_SINHVIEN_GioiTinh` |
| Số tín chỉ > 0 | `CK_MONHOC_SoTinChi` |
| FK LOP→KHOA, SINHVIEN→LOP, KETQUA→SINHVIEN/MONHOC | `FK_*` |

Dữ liệu mẫu: 2 khoa, 3 lớp, 7 sinh viên, 4 môn, 10 kết quả, 2 tài khoản (`admin/admin123`, `user1/user123`; mật khẩu lưu dạng hash SHA2_256).

## Kết quả mong đợi
- Câu 6: xóa SV007 thành công (chưa có điểm) → còn 6 sinh viên.
- Câu 9/10: sau câu 5 (chuyển SV006 sang `DH22CNTT01`), `DH21CNTT01` có 2 SV, `DH22CNTT01` có 3 SV, `DH22KT01` có 1 SV → câu 10 trả về 2 lớp đầu.
- Câu 11: lỗi 547 (vi phạm FOREIGN KEY `FK_SINHVIEN_LOP`).
- Câu 12: lỗi 547 (vi phạm CHECK `CK_KETQUA_Diem`).
