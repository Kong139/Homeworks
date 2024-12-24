-- Tạo Database
CREATE DATABASE StudentManagement;
GO

USE StudentManagement;
GO

-- Tạo bảng Faculty
CREATE TABLE Faculty (
    FacultyID INT PRIMARY KEY,
    FacultyName NVARCHAR(255) NOT NULL
);

-- Tạo bảng Major
CREATE TABLE Major (
    FacultyID INT NOT NULL,
    MajorID INT PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID)
);

-- Tạo bảng Student
CREATE TABLE Student (
    StudentID NVARCHAR(10) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    AverageScore FLOAT NOT NULL,
    MajorID INT NOT NULL,
    FacultyID INT NOT NULL,
    Avatar NVARCHAR(255),
    FOREIGN KEY (MajorID) REFERENCES Major(MajorID),
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID)
);

-- Thêm dữ liệu vào bảng Faculty
INSERT INTO Faculty (FacultyID, FacultyName)
VALUES 
(1, N'Công nghệ thông tin'),
(2, N'Ngôn Ngữ Anh'),
(3, N'Quản Trị Kinh Doanh');

-- Thêm dữ liệu vào bảng Major
INSERT INTO Major (FacultyID, MajorID, Name)
VALUES 
(1, 1, N'Công nghệ phần mềm'),
(2, 2, N'Tiếng Anh Thương Mại'),
(1, 3, N'Hệ thống thông tin'),
(2, 4, N'Tiếng Anh Truyền Thông'),
(1, 5, N'An toàn thông tin');

-- Thêm dữ liệu vào bảng Student
INSERT INTO Student (StudentID, FullName, AverageScore, MajorID, FacultyID, Avatar)
VALUES 
('1234567890', N'Nguyen Van B', 7.5, 1, 1, NULL),
('1234567891', N'Nguyen Van A', 4.5, 3, 1, NULL),
('2345643213', N'Nguyen Van C', 10, 2, 2, NULL);