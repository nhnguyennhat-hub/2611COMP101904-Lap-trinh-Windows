/* ============================================================
   File 03: 12 truy vấn theo yêu cầu (chạy sau 02_InsertSampleData.sql)
   Chạy từng câu bằng cách bôi đen rồi nhấn F5, hoặc chạy cả file.
   ============================================================ */
USE EduManagerDB;
GO

-- Câu 1: Liệt kê toàn bộ sinh viên
SELECT * FROM SINHVIEN;
GO

-- Câu 2: Sinh viên kèm tên lớp và tên khoa (JOIN 3 bảng)
SELECT sv.MaSV, sv.HoTen, l.TenLop, k.TenKhoa
FROM SINHVIEN sv
JOIN LOP  l ON sv.MaLop  = l.MaLop
JOIN KHOA k ON l.MaKhoa  = k.MaKhoa
ORDER BY sv.MaSV;
GO

-- Câu 3: Tìm sinh viên theo một phần họ tên (LIKE)
SELECT MaSV, HoTen, MaLop
FROM SINHVIEN
WHERE HoTen LIKE N'%Văn%';
GO

-- Câu 4: Sinh viên theo mã lớp
SELECT MaSV, HoTen, GioiTinh, NgaySinh
FROM SINHVIEN
WHERE MaLop = 'DH22CNTT01';
GO

-- Câu 5: Cập nhật họ tên / lớp của một sinh viên
SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV = 'SV006';   -- trước khi sửa

UPDATE SINHVIEN
SET HoTen = N'Vũ Ngọc Hà My',
    MaLop = 'DH22CNTT01'
WHERE MaSV = 'SV006';

SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV = 'SV006';   -- sau khi sửa
GO

-- Câu 6: Xóa một sinh viên chưa có dữ liệu điểm (SV007)
-- Điều kiện NOT EXISTS đảm bảo chỉ xóa khi không có dòng trong KETQUA
DELETE FROM SINHVIEN
WHERE MaSV = 'SV007'
  AND NOT EXISTS (SELECT 1 FROM KETQUA kq WHERE kq.MaSV = SINHVIEN.MaSV);

SELECT COUNT(*) AS SoSV_SauKhiXoa FROM SINHVIEN;   -- còn 6 sinh viên
GO

-- Câu 7: Điểm của từng sinh viên theo từng môn
SELECT sv.MaSV, sv.HoTen, mh.TenMH, kq.Diem
FROM KETQUA kq
JOIN SINHVIEN sv ON kq.MaSV = sv.MaSV
JOIN MONHOC   mh ON kq.MaMH = mh.MaMH
ORDER BY sv.MaSV, mh.TenMH;
GO

-- Câu 8: Điểm trung bình của từng sinh viên
-- (LEFT JOIN để sinh viên chưa có điểm vẫn hiện, DTB = NULL)
SELECT sv.MaSV, sv.HoTen, CAST(AVG(kq.Diem) AS DECIMAL(4,2)) AS DiemTrungBinh
FROM SINHVIEN sv
LEFT JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen
ORDER BY sv.MaSV;
GO

-- Câu 9: Số sinh viên theo từng lớp
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
ORDER BY l.MaLop;
GO

-- Câu 10: Các lớp có từ 2 sinh viên trở lên (HAVING)
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
HAVING COUNT(sv.MaSV) >= 2;
GO

-- Câu 11: Test lỗi - thêm sinh viên vào lớp không tồn tại (vi phạm FK_SINHVIEN_LOP)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, Email, MaLop)
    VALUES ('SV999', N'Sinh Viên Lỗi', '2004-01-01', N'Nam', 'loi@edu.vn', 'LOP_KHONG_TON_TAI');
    PRINT N'KHÔNG MONG ĐỢI: thêm thành công!';
END TRY
BEGIN CATCH
    PRINT N'Câu 11 - Lỗi như mong đợi (' + CAST(ERROR_NUMBER() AS NVARCHAR(10)) + N'): ' + ERROR_MESSAGE();
END CATCH
GO

-- Câu 12: Test lỗi - nhập điểm ngoài khoảng 0..10 (vi phạm CK_KETQUA_Diem)
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV003', 'MH01', 11);
    PRINT N'KHÔNG MONG ĐỢI: thêm thành công!';
END TRY
BEGIN CATCH
    PRINT N'Câu 12 - Lỗi như mong đợi (' + CAST(ERROR_NUMBER() AS NVARCHAR(10)) + N'): ' + ERROR_MESSAGE();
END CATCH
GO

/* ---------- Test bổ sung (không bắt buộc) ---------- */
-- Giới tính ngoài Nam/Nữ
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, MaLop) VALUES ('SV998', N'Test', N'Khác', 'DH21CNTT01');
END TRY
BEGIN CATCH PRINT N'Test giới tính - ' + ERROR_MESSAGE(); END CATCH
GO
-- Số tín chỉ <= 0
BEGIN TRY
    INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES ('MH99', N'Môn lỗi', 0);
END TRY
BEGIN CATCH PRINT N'Test tín chỉ - ' + ERROR_MESSAGE(); END CATCH
GO
-- Xóa sinh viên đã có điểm (bị FK chặn)
BEGIN TRY
    DELETE FROM SINHVIEN WHERE MaSV = 'SV001';
END TRY
BEGIN CATCH PRINT N'Test xóa SV có điểm - ' + ERROR_MESSAGE(); END CATCH
GO
