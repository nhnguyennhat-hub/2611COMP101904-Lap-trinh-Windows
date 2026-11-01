/* ============================================================
   COMP1019 - Lab 10: EduManagerDB
   File 01: Tạo database, bảng, khóa chính, khóa ngoại, ràng buộc
   ============================================================ */
USE master;
GO

-- Xóa database cũ (nếu có) và ngắt mọi kết nối đang mở để chạy lại từ đầu
IF DB_ID(N'EduManagerDB') IS NOT NULL
BEGIN
    ALTER DATABASE EduManagerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EduManagerDB;
END
GO

CREATE DATABASE EduManagerDB;
GO
USE EduManagerDB;
GO

/* ---------- 1. KHOA ---------- */
CREATE TABLE KHOA (
    MaKhoa   VARCHAR(10)   NOT NULL,
    TenKhoa  NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_KHOA PRIMARY KEY (MaKhoa),
    CONSTRAINT UQ_KHOA_TenKhoa UNIQUE (TenKhoa)
);
GO

/* ---------- 2. LOP (mỗi lớp thuộc một khoa) ---------- */
CREATE TABLE LOP (
    MaLop    VARCHAR(15)   NOT NULL,
    TenLop   NVARCHAR(100) NOT NULL,
    KhoaHoc  VARCHAR(10)   NULL,            -- ví dụ K49, K50
    MaKhoa   VARCHAR(10)   NOT NULL,
    CONSTRAINT PK_LOP PRIMARY KEY (MaLop),
    CONSTRAINT FK_LOP_KHOA FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa)
);
GO

/* ---------- 3. SINHVIEN (mỗi sinh viên thuộc một lớp) ---------- */
CREATE TABLE SINHVIEN (
    MaSV      VARCHAR(15)   NOT NULL,
    HoTen     NVARCHAR(100) NOT NULL,
    NgaySinh  DATE          NULL,
    GioiTinh  NVARCHAR(3)   NOT NULL,
    Email     VARCHAR(100)  NULL,
    MaLop     VARCHAR(15)   NOT NULL,
    CONSTRAINT PK_SINHVIEN PRIMARY KEY (MaSV),
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop),
    CONSTRAINT CK_SINHVIEN_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ'))
);
GO

/* ---------- 4. MONHOC ---------- */
CREATE TABLE MONHOC (
    MaMH     VARCHAR(10)   NOT NULL,
    TenMH    NVARCHAR(100) NOT NULL,
    SoTinChi INT           NOT NULL,
    CONSTRAINT PK_MONHOC PRIMARY KEY (MaMH),
    CONSTRAINT CK_MONHOC_SoTinChi CHECK (SoTinChi > 0)
);
GO

/* ---------- 5. KETQUA (khóa chính kép MaSV + MaMH) ---------- */
CREATE TABLE KETQUA (
    MaSV   VARCHAR(15)  NOT NULL,
    MaMH   VARCHAR(10)  NOT NULL,
    Diem   DECIMAL(4,2) NOT NULL,
    CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT FK_KETQUA_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV),
    CONSTRAINT FK_KETQUA_MONHOC   FOREIGN KEY (MaMH) REFERENCES MONHOC(MaMH),
    CONSTRAINT CK_KETQUA_Diem CHECK (Diem BETWEEN 0 AND 10)
);
GO

/* ---------- 6. TAIKHOAN (tài khoản demo; lưu hash, không lưu mật khẩu thô) ---------- */
CREATE TABLE TAIKHOAN (
    TenDangNhap  VARCHAR(50)   NOT NULL,
    MatKhauHash  VARCHAR(64)   NOT NULL,    -- SHA2_256 dạng hex
    HoTen        NVARCHAR(100) NOT NULL,
    VaiTro       VARCHAR(20)   NOT NULL DEFAULT 'User',
    CONSTRAINT PK_TAIKHOAN PRIMARY KEY (TenDangNhap),
    CONSTRAINT CK_TAIKHOAN_VaiTro CHECK (VaiTro IN ('Admin', 'User'))
);
GO

PRINT N'Đã tạo xong EduManagerDB và 6 bảng.';
GO
