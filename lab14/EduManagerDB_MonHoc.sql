/* ============================================================
   COMP1019 - Lab 14: bảng MONHOC cho EduManager (Entity Framework Core)
   Chạy trong SSMS. An toàn khi chạy nhiều lần, KHÔNG xóa dữ liệu.
   ============================================================ */
USE master;
GO
IF DB_ID(N'EduManagerDB') IS NULL
    CREATE DATABASE EduManagerDB;
GO
USE EduManagerDB;
GO

/* Lab 10 cũng có bảng MONHOC nhưng cấu trúc khác (MaMH, TenMH...).
   Nếu phát hiện bảng cũ, đổi tên thành MONHOC_Lab10 (dữ liệu và khóa ngoại KETQUA được giữ nguyên). */
IF OBJECT_ID(N'dbo.MONHOC', N'U') IS NOT NULL AND COL_LENGTH(N'dbo.MONHOC', N'MaMonHoc') IS NULL
BEGIN
    EXEC sp_rename N'dbo.MONHOC', N'MONHOC_Lab10';
    PRINT N'Đã đổi tên bảng MONHOC của Lab 10 thành MONHOC_Lab10.';
END
GO

IF OBJECT_ID(N'dbo.MONHOC', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.MONHOC (
        MaMonHoc  NVARCHAR(10)  NOT NULL,
        TenMonHoc NVARCHAR(100) NOT NULL,
        SoTinChi  INT           NOT NULL,
        TrangThai BIT           NOT NULL CONSTRAINT DF_MonHoc_TrangThai DEFAULT 1,
        CONSTRAINT PK_MonHoc_Lab14 PRIMARY KEY (MaMonHoc),
        CONSTRAINT CK_MonHoc_SoTinChi_1_6 CHECK (SoTinChi BETWEEN 1 AND 6)
    );
    PRINT N'Đã tạo bảng MONHOC.';
END
GO

-- Dữ liệu mẫu (chỉ thêm khi bảng đang trống)
IF NOT EXISTS (SELECT 1 FROM dbo.MONHOC)
    INSERT INTO dbo.MONHOC (MaMonHoc, TenMonHoc, SoTinChi, TrangThai) VALUES
    (N'COMP1019', N'Lập trình trên Windows', 3, 1),
    (N'COMP1010', N'Cơ sở dữ liệu',           3, 1),
    (N'MATH1001', N'Toán rời rạc',            2, 1),
    (N'ENGL1001', N'Tiếng Anh chuyên ngành',  3, 1),
    (N'COMP0001', N'Tin học đại cương (cũ)',  2, 0);
GO

SELECT * FROM dbo.MONHOC ORDER BY MaMonHoc;
GO
