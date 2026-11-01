/* ============================================================
   COMP1019 - Lab 12: QLSinhVienLab12
   Tạo database, bảng, dữ liệu mẫu và 4 stored procedure.
   Chạy cả file trong SSMS (F5). Chạy lại được nhiều lần.
   ============================================================ */
USE master;
GO
IF DB_ID(N'QLSinhVienLab12') IS NOT NULL
BEGIN
    ALTER DATABASE QLSinhVienLab12 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLSinhVienLab12;
END
GO
CREATE DATABASE QLSinhVienLab12;
GO
USE QLSinhVienLab12;
GO

/* ---------------- BẢNG ---------------- */
CREATE TABLE LOP (
    MaLop  VARCHAR(15)   NOT NULL,
    TenLop NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_LOP PRIMARY KEY (MaLop)
);
CREATE TABLE SINHVIEN (
    MaSV     VARCHAR(15)   NOT NULL,
    HoTen    NVARCHAR(100) NOT NULL,
    NgaySinh DATE          NULL,
    MaLop    VARCHAR(15)   NOT NULL,
    CONSTRAINT PK_SINHVIEN PRIMARY KEY (MaSV),
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);
CREATE TABLE MONHOC (
    MaMon    VARCHAR(10)   NOT NULL,
    TenMon   NVARCHAR(100) NOT NULL,
    SoTinChi INT           NOT NULL,
    CONSTRAINT PK_MONHOC PRIMARY KEY (MaMon),
    CONSTRAINT CK_MONHOC_SoTinChi CHECK (SoTinChi > 0)
);
CREATE TABLE KETQUA (
    MaSV   VARCHAR(15)  NOT NULL,
    MaMon  VARCHAR(10)  NOT NULL,
    Diem   DECIMAL(4,2) NOT NULL,
    GhiChu NVARCHAR(200) NULL,
    CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMon),
    CONSTRAINT FK_KETQUA_SINHVIEN FOREIGN KEY (MaSV)  REFERENCES SINHVIEN(MaSV),
    CONSTRAINT FK_KETQUA_MONHOC   FOREIGN KEY (MaMon) REFERENCES MONHOC(MaMon),
    CONSTRAINT CK_KETQUA_Diem CHECK (Diem BETWEEN 0 AND 10)
);
GO

/* ---------------- DỮ LIỆU MẪU ---------------- */
INSERT INTO LOP (MaLop, TenLop) VALUES
('DH21CNTT01', N'Đại học CNTT 21-01'),
('DH22CNTT01', N'Đại học CNTT 22-01'),
('DH22KT01',   N'Đại học Kinh tế 22-01');

INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES
('SV001', N'Nguyễn Văn An',   '2003-03-15', 'DH21CNTT01'),
('SV002', N'Trần Thị Bình',   '2003-07-22', 'DH21CNTT01'),
('SV003', N'Lê Hoàng Cường',  '2003-01-10', 'DH21CNTT01'),
('SV004', N'Phạm Thu Dung',   '2004-05-30', 'DH22CNTT01'),
('SV005', N'Hoàng Minh Đức',  '2004-09-05', 'DH22CNTT01'),
('SV006', N'Vũ Ngọc Hà',      '2004-11-18', 'DH22CNTT01'),
('SV007', N'Đỗ Quang Huy',    '2004-02-02', 'DH22KT01'),
('SV008', N'Bùi Thanh Lan',   '2004-06-12', 'DH22KT01'),
('SV009', N'Ngô Gia Khánh',   '2004-08-25', 'DH22KT01');

INSERT INTO MONHOC (MaMon, TenMon, SoTinChi) VALUES
('MH01', N'Lập trình C#',   3),
('MH02', N'Cơ sở dữ liệu',  3),
('MH03', N'Toán rời rạc',   2),
('MH04', N'Kinh tế vi mô',  3);

-- Một số sinh viên đã có điểm (để thấy cả trường hợp UPDATE lẫn INSERT)
INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu) VALUES
('SV001', 'MH01', 8.50, N'Khá giỏi'),
('SV002', 'MH01', 7.00, NULL),
('SV004', 'MH02', 9.00, N'Xuất sắc'),
('SV007', 'MH04', 6.50, NULL);
GO

/* ---------------- STORED PROCEDURE ---------------- */
CREATE PROCEDURE sp_Lop_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaLop, TenLop FROM LOP ORDER BY TenLop;
END
GO

CREATE PROCEDURE sp_MonHoc_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaMon, TenMon, SoTinChi FROM MONHOC ORDER BY TenMon;
END
GO

-- Danh sách sinh viên của lớp, kèm điểm môn đã chọn (NULL nếu chưa có)
CREATE PROCEDURE sp_KetQua_GetByLopMon
    @MaLop VARCHAR(15),
    @MaMon VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT sv.MaSV, sv.HoTen, kq.Diem, kq.GhiChu
    FROM SINHVIEN sv
    LEFT JOIN KETQUA kq ON kq.MaSV = sv.MaSV AND kq.MaMon = @MaMon
    WHERE sv.MaLop = @MaLop
    ORDER BY sv.MaSV;
END
GO

-- Đã có điểm thì UPDATE, chưa có thì INSERT (upsert)
CREATE PROCEDURE sp_KetQua_Save
    @MaSV   VARCHAR(15),
    @MaMon  VARCHAR(10),
    @Diem   DECIMAL(4,2),
    @GhiChu NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Diem IS NULL OR @Diem < 0 OR @Diem > 10
        THROW 50001, N'Điểm phải nằm trong khoảng từ 0 đến 10.', 1;

    UPDATE KETQUA WITH (UPDLOCK, HOLDLOCK)
    SET Diem = @Diem, GhiChu = @GhiChu
    WHERE MaSV = @MaSV AND MaMon = @MaMon;

    IF @@ROWCOUNT = 0
        INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu)
        VALUES (@MaSV, @MaMon, @Diem, @GhiChu);
END
GO

/* ---------------- KIỂM TRA NHANH (tùy chọn) ----------------
EXEC sp_Lop_GetAll;
EXEC sp_MonHoc_GetAll;
EXEC sp_KetQua_GetByLopMon @MaLop = 'DH21CNTT01', @MaMon = 'MH01';
EXEC sp_KetQua_Save 'SV003', 'MH01', 9.0, N'Thêm mới';   -- INSERT
EXEC sp_KetQua_Save 'SV003', 'MH01', 9.5, N'Sửa lại';    -- UPDATE
EXEC sp_KetQua_Save 'SV003', 'MH01', 11,  NULL;          -- lỗi 50001
---------------------------------------------------------- */
PRINT N'Đã tạo QLSinhVienLab12 với 4 bảng, dữ liệu mẫu và 4 stored procedure.';
GO
