-- Tạo cơ sở dữ liệu QuanlySV
CREATE DATABASE QuanlySV;
GO

-- Sử dụng cơ sở dữ liệu vừa tạo
USE QuanlySV;
GO

-- Tạo bảng Lop
CREATE TABLE Lop (
    MaLop CHAR(3) PRIMARY KEY,          -- Mã lớp, khóa chính
    TenLop NVARCHAR(30) NOT NULL       -- Tên lớp, không được để trống
);

-- Tạo bảng Sinhvien
CREATE TABLE Sinhvien (
    MaSV CHAR(6) PRIMARY KEY,          -- Mã sinh viên, khóa chính
    HotenSV NVARCHAR(40),              -- Họ tên sinh viên
    NgaySinh DATETIME,                 -- Ngày sinh
    MaLop CHAR(3),                     -- Mã lớp (khóa ngoại tham chiếu bảng Lop)
    FOREIGN KEY (MaLop) REFERENCES Lop(MaLop) ON DELETE CASCADE
);

-- Thêm dữ liệu vào bảng Lop
INSERT INTO Lop (MaLop, TenLop)
VALUES 
    ('C01', N'Công Nghệ Thông Tin'),
    ('C02', N'Ngôn ngữ Anh');

-- Thêm dữ liệu vào bảng Sinhvien
INSERT INTO Sinhvien (MaSV, HotenSV, NgaySinh, MaLop)
VALUES
    ('SV0001', N'Nguyễn Văn A', '2000-01-01', 'C01'),
    ('SV0002', N'Lê Thị B', '2001-02-15', 'C01'),
    ('SV0003', N'Trần Văn C', '1999-07-20', 'C02'),
    ('SV0004', N'Phạm Thị D', '2002-11-30', 'C02');
