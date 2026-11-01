/* ============================================================
   File 02: Nhập dữ liệu mẫu (chạy sau 01_CreateDatabase.sql)
   Thứ tự: bảng cha trước, bảng con sau để không vi phạm khóa ngoại
   ============================================================ */
USE EduManagerDB;
GO

-- 2 khoa
INSERT INTO KHOA (MaKhoa, TenKhoa) VALUES
('CNTT', N'Công nghệ thông tin'),
('KT',   N'Kinh tế');

-- 3 lớp
INSERT INTO LOP (MaLop, TenLop, KhoaHoc, MaKhoa) VALUES
('DH21CNTT01', N'Đại học CNTT 21-01',      'K49', 'CNTT'),
('DH22CNTT01', N'Đại học CNTT 22-01',      'K50', 'CNTT'),
('DH22KT01',   N'Đại học Kinh tế 22-01',   'K50', 'KT');

-- 7 sinh viên (SV007 chưa có điểm -> dùng cho bài xóa)
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, Email, MaLop) VALUES
('SV001', N'Nguyễn Văn An',   '2003-03-15', N'Nam', 'an.nv@edu.vn',    'DH21CNTT01'),
('SV002', N'Trần Thị Bình',   '2003-07-22', N'Nữ',  'binh.tt@edu.vn',  'DH21CNTT01'),
('SV003', N'Lê Hoàng Cường',  '2004-01-10', N'Nam', 'cuong.lh@edu.vn', 'DH22CNTT01'),
('SV004', N'Phạm Thu Dung',   '2004-05-30', N'Nữ',  'dung.pt@edu.vn',  'DH22CNTT01'),
('SV005', N'Hoàng Minh Đức',  '2004-09-05', N'Nam', 'duc.hm@edu.vn',   'DH22KT01'),
('SV006', N'Vũ Ngọc Hà',      '2004-11-18', N'Nữ',  'ha.vn@edu.vn',    'DH22KT01'),
('SV007', N'Đỗ Quang Huy',    '2004-02-02', N'Nam', 'huy.dq@edu.vn',   'DH22KT01');

-- 4 môn học
INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES
('MH01', N'Lập trình C#',          3),
('MH02', N'Cơ sở dữ liệu',         3),
('MH03', N'Toán rời rạc',          2),
('MH04', N'Kinh tế vi mô',         3);

-- 10 kết quả học tập (SV007 không có dòng nào)
INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES
('SV001', 'MH01', 8.5),
('SV001', 'MH02', 9.0),
('SV002', 'MH01', 7.0),
('SV002', 'MH03', 6.5),
('SV003', 'MH02', 8.0),
('SV004', 'MH01', 9.5),
('SV004', 'MH02', 7.5),
('SV005', 'MH04', 6.0),
('SV005', 'MH03', 5.5),
('SV006', 'MH04', 8.5);

-- 2 tài khoản demo (mật khẩu: admin123 / user123, lưu dạng hash SHA2_256)
INSERT INTO TAIKHOAN (TenDangNhap, MatKhauHash, HoTen, VaiTro) VALUES
('admin', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'admin123'), 2), N'Quản trị viên', 'Admin'),
('user1', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'user123'),  2), N'Người dùng 1',  'User');
GO

-- Kiểm tra nhanh số dòng từng bảng
SELECT 'KHOA' AS Bang, COUNT(*) AS SoDong FROM KHOA UNION ALL
SELECT 'LOP',      COUNT(*) FROM LOP       UNION ALL
SELECT 'SINHVIEN', COUNT(*) FROM SINHVIEN  UNION ALL
SELECT 'MONHOC',   COUNT(*) FROM MONHOC    UNION ALL
SELECT 'KETQUA',   COUNT(*) FROM KETQUA    UNION ALL
SELECT 'TAIKHOAN', COUNT(*) FROM TAIKHOAN;
GO
