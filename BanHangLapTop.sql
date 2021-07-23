--
USE MASTER
GO
--
IF EXISTS (SELECT NAME FROM SYS.DATABASES WHERE NAME='BanHangLaptop')
	DROP DATABASE BanHangLaptop
GO
-- Nhớ chỉnh lại đường dẫn
CREATE DATABASE BanHangLaptop
ON(NAME='BanHangLaptop_DATA',FILENAME='D:\Study\BanHangLaptop.MDF')
LOG ON(NAME='BanHangLaptop_LOG',FILENAME='D:\Study\BanHangLaptop.LDF')
GO 
--
USE BanHangLaptop
GO
--
CREATE TABLE QuanTri
(
	tentk CHAR (30) PRIMARY KEY,
	matkhautk char (30),
	tenadmin nvarchar(50)
)
go
--
create TABLE SanPham
(
	MaSP INT IDENTITY(1,1) PRIMARY KEY,
	TenSP nvarchar(50) unique (TenSP),
	MaHang int,
	AnhMH nvarchar(50),
	AnhMH1 nvarchar(50),
	AnhMH2 nvarchar(50),
	AnhMH3 nvarchar(50),
	NgayCapNhat Datetime,
	Soluongton INT,
	GiaBan bigint,
	CPU nvarchar(50),
	LoaiCpu nvarchar(50),
	Ram nvarchar(50),
	Crad nvarchar(50),
	ManHinh nvarchar(50),
	OCung nvarchar(50),
	NoiDung ntext
)
go
--
create TABLE Lienhe
(
	MaLH INT IDENTITY(1,1) PRIMARY KEY,
	TenKH nvarchar (50),
	Emai nvarchar (50),
	NoiDung ntext

)
go
--
create TABLE Tintuc
(
	MaTinTuc INT IDENTITY(1,1) PRIMARY KEY,
	AnhTinTuc nvarchar (100),
	TieuDe nvarchar (100),
	Link ntext,
	Noidung2 ntext,
	Noidung3 ntext,
	NoiDung ntext

)
go
--
create TABLE Hang
(
	MaHang INT IDENTITY(1,1) PRIMARY KEY,
	TenHang nvarchar (50) unique

)
go
--
create table KhachHang
(
IDKH INT IDENTITY(1,1) primary key,
HoTen nvarchar(50)NOT NULL,
TaiKhoan nvarchar(50) UNIQUE,
MatKhau nvarchar(50)NOT NULL,
Email nvarchar(100) UNIQUE,
DiaChi nvarchar(100),
DienThoai varchar(11),
SoTienCo bigint
)
go
--
CREATE TABLE DonDatHang
(
MaDH INT IDENTITY(1,1) PRIMARY KEY,
DaThanhToan int,
TinhTrangGiaoHang varchar(20),
NgayDat Datetime,
NgayGiao Datetime,
IDKH INT
)
go
--
CREATE TABLE AnhBia
(
MaAnhBia INT IDENTITY(1,1) primary key,
TenAnhBia nvarchar(50),
Link ntext,
)
go
--
CREATE TABLE ChiTietDDH
(
MaDH INT,
MaSP INT,
Soluong Int Check(Soluong>0),
Dongia Decimal(18,0) Check(Dongia>=0),
PRIMARY KEY (MaDH,MaSP)
)
go
--
ALTER TABLE DonDatHang
	ADD CONSTRAINT FK_DonDatHang_KhachHang FOREIGN KEY (IDKH) REFERENCES KhachHang(IDKH)	
go
--
ALTER TABLE ChiTietDDH
	ADD CONSTRAINT FK_ChiTietDDH_DonDatHang FOREIGN KEY (MaDH) REFERENCES DonDatHang(MaDH)	
go
--
ALTER TABLE ChiTietDDH
	ADD CONSTRAINT FK_ChiTietDDH_SanPham FOREIGN KEY (MaSP) REFERENCES	SanPham(MaSP)	
go
--
ALTER TABLE SanPham
	ADD CONSTRAINT FK_SanPham_Hang FOREIGN KEY (MaHang) REFERENCES	Hang(MaHang)	
go