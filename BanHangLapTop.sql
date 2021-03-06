--
USE MASTER
GO
--
IF EXISTS (SELECT NAME FROM SYS.DATABASES WHERE NAME='BanHangLaptop')
	DROP DATABASE BanHangLaptop
GO
-- Có thể chỉnh lại đường dẫn
CREATE DATABASE BanHangLaptop
ON(NAME='BanHangLaptop_DATA',FILENAME='D:\Database\BanHangLaptop.MDF')
LOG ON(NAME='BanHangLaptop_LOG',FILENAME='D:\Database\BanHangLaptop.LDF')
GO 
--
USE BanHangLaptop
GO
--
create table KhachHang
(
	TaiKhoanKH varchar(30) PRIMARY KEY,
	MatKhau nvarchar(50)NOT NULL,
	HoTen nvarchar(50)NOT NULL,
	Email nvarchar(100) UNIQUE,
	DiaChi nvarchar(100)NOT NULL,
	DienThoai varchar(11)NOT NULL
)
go
CREATE TABLE NhanVien
(
	TaikhoanNV varchar (30) PRIMARY KEY,
	Matkhau char (30),
	HoTen nvarchar(50)NOT NULL,
	Email nvarchar(100) UNIQUE,
	DiaChi nvarchar(100)NOT NULL,
	DienThoai varchar(11)NOT NULL
)
go
create TABLE Khuyenmai
(
	MaKM varchar(10) PRIMARY KEY,
	Noidung nvarchar(50) NOT NULL,
	Ngaybatdau Date NOT NULL,
	Ngayketthuc Date NOT NULL,
	Tyle int,
	CONSTRAINt TyleKM CHECK (Tyle between 0 and 100)
)
go
create TABLE HangSX
(
	IDHang INT identity (1,1) PRIMARY KEY,
	TenHang nvarchar (50) NOT NULL unique
)
go
create TABLE NhaCC
(
	IDNhaCC int identity (1,1) primary key,
	TenNhaCC nvarchar(50),
	Diachi nvarchar(100) NOT NULL,
	Dienthoai varchar(11) NOT NULL,
	IDHang int
)
create TABLE [Model]
(
	MaModel INT identity(1,1) PRIMARY KEY,
	TenModel nvarchar (50) NOT NULL unique,
	IDHang int,
	Soluongton int NOT NULL,
	AnhMH1 nvarchar(50) NOT NULL,
	AnhMH2 nvarchar(50),
	AnhMH3 nvarchar(50),
	AnhMH4 nvarchar(50),
	CPU nvarchar(50),
	Ram nvarchar(50),
	[Card] nvarchar(50),
	ManHinh nvarchar(50),
	OCung nvarchar(50),
	Ghichu ntext

)
go

create TABLE CTKM
(
	MaKM varchar(10),
	MaModel int,
	CONSTRAINT PK_CTKM PRIMARY KEY(MaKM,MaModel)
)
go

CREATE TABLE DonDatHang
(
	MaDDH INT primary key,
	IDNhaCC int,
	Ngaydat date,
	TaikhoanNV varchar(30),
)
go
create TABLE ChitietDDH
(
	MaDDH int,
	MaModel int,
	Soluong int NOT NULL,
	Dongia bigint NOT NULL,	
	CONSTRAINT PK_ChitietPN PRIMARY KEY(MaDDH,MaModel),
	CONSTRAINT soluongdat CHECK (Soluong >0)
)
go
--
create TABLE PhieuNhapHang
(
	Maphieunhap int PRIMARY KEY,
	MaDDH int,
	TaikhoanNV varchar(30),
	Ngaynhaphang date,
	Ghichu nvarchar(20),
)
go
--
create TABLE PhieuMuaHang
(
	MaPhieuMH int identity(1,1) primary key,
	TaikhoanKH varchar(30),
	Ttthanhtoan bit,
	ttgiaohang nvarchar(15),
	HoTen nvarchar(50)NOT NULL,
	Email nvarchar(100) ,
	DiaChi nvarchar(100)NOT NULL,
	DienThoai varchar(11)NOT NULL,
	ngaydat date,
	ngaygiao date,
	TaikhoanNVduyet varchar(30),
	TaikhoanNVgiao varchar(30),
)
go
create TABLE SanPham
(
	Serial nvarchar(10) PRIMARY KEY,
	MaModel int,
	Giaban bigint,
	Maphieunhap int,
	MaphieuMH int,
	MaBH int
)
go

go

create TABLE Giaban
(
	MaModel INT,
	Ngay Date,
	Gia bigint,
	CONSTRAINT pk_Thaydoigia PRIMARY KEY(MaModel,Ngay)
)
go
--


create TABLE HoaDon
(
	MaHD int primary key,
	MaPhieuMH int,
	TaikhoanKH varchar(30),
	TaikhoanNVlap varchar(30),
	Hoten nvarchar(50),
	Email nvarchar(100) UNIQUE,
	DiaChi nvarchar(100),
	DienThoai varchar(11)
)
go
create TABLE BaoHanh
(
	MaBH int identity(1,1) primary key,
	SoDT varchar(11),
	TaikhoanNVlap varchar(30)
)
go
create TABLE ChitietBH
(	
	MaBH int,
	TaikhoanNV varchar(30),
	NgayBH Date,
	ttmaynhanBH nvarchar(50),
	ttmaytraBH nvarchar(50),
	CONSTRAINT pk_ChitietBH primary key(MaBH,TaikhoanNV,NgayBH)
)
go

create TABLE Phanhoi
(
	IDPH INT IDENTITY(1,1) PRIMARY KEY,
	TaikhoanKH varchar (30),
	TaikhoanNV varchar (30),
	NoiDung ntext,
	PhanhoicuaNV ntext
)
go
--
create TABLE Tintuc
(
	MaTinTuc INT IDENTITY(1,1) PRIMARY KEY,
	AnhTinTuc nvarchar (100),
	TieuDe nvarchar (100),
	Link ntext,
	Noidung ntext,
)
go


CREATE TABLE AnhBia
(
IDAnhBia INT IDENTITY(1,1) primary key,
TenAnhBia nvarchar(50),
Link ntext,
)
go
--

ALTER TABLE NhaCC
	ADD CONSTRAINT FK_NhaCC_HangSX FOREIGN KEY (IDHang) REFERENCES HangSX(IDHang);
go
ALTER TABLE CTKM
	ADD CONSTRAINT FK_CTKM_Khuyenmai FOREIGN KEY (MaKM) REFERENCES Khuyenmai(MaKM),
		CONSTRAINT FK_CTKM_Model FOREIGN KEY (MaModel) REFERENCES [Model](MaModel);
go
ALTER TABLE Model	
	ADD	CONSTRAINT FK_Model_HangSX FOREIGN KEY (IDHang) REFERENCES HangSX(IDHang);
go
ALTER TABLE Giaban
	ADD CONSTRAINT FK_Thaydoigia_Model FOREIGN KEY (MaModel) REFERENCES [Model](MaModel);
go
ALTER TABLE DonDatHang
	ADD CONSTRAINT FK_DonDatHang_NhaCC FOREIGN KEY (IDNhaCC) REFERENCES NhaCC(IDNhaCC),
		CONSTRAINT FK_DonDatHang_Nhanvien FOREIGN KEY (TaikhoanNV) REFERENCES Nhanvien(TaikhoanNV);
go
ALTER TABLE ChitietDDH
	ADD CONSTRAINT FK_ChitietDDH_DonDatHang FOREIGN KEY (MaDDH) REFERENCES DonDatHang(MaDDH),
		CONSTRAINT FK_ChitietDDH_Model FOREIGN KEY (MaModel) REFERENCES [Model](MaModel);
go
ALTER TABLE PhieuNhapHang
	ADD CONSTRAINT FK_PhieuNhapHang_DonDatHang FOREIGN KEY (MaDDH) REFERENCES DonDatHang(MaDDH),
		CONSTRAINT FK_PhieuNhapHang_Nhanvien FOREIGN KEY (TaikhoanNV) REFERENCES Nhanvien(TaikhoanNV);	
go
ALTER TABLE PhieuMuaHang
	ADD CONSTRAINT FK_PhieuMuaHang_Nhanvienduyet FOREIGN KEY (TaikhoanNVduyet) REFERENCES Nhanvien(TaikhoanNV),
		CONSTRAINT FK_PhieuMuaHang_KhachHang FOREIGN KEY (TaikhoanKH) REFERENCES KhachHang(TaikhoanKH),
		CONSTRAINT FK_PhieuMuaHang_Nhanviengiao FOREIGN KEY (TaikhoanNVgiao) REFERENCES Nhanvien(TaikhoanNV);
go
ALTER TABLE SanPham
	ADD CONSTRAINT FK_SanPham_Model FOREIGN KEY (MaModel) REFERENCES [Model](MaModel),
		CONSTRAINT FK_SanPham_PhieuNhapHang FOREIGN KEY (Maphieunhap) REFERENCES PhieuNhapHang(Maphieunhap),
		CONSTRAINT FK_SanPham_PhieuMuaHang FOREIGN KEY (MaphieuMH) REFERENCES PhieuMuaHang(MaphieuMH),
		CONSTRAINT FK_SanPham_BaoHanh FOREIGN KEY (MaBH) REFERENCES BaoHanh(MaBH);
go

ALTER TABLE HoaDon
	ADD CONSTRAINT FK_HoaDon_PhieuMuaHang FOREIGN KEY (MaPhieuMH) REFERENCES PhieuMuaHang(MaPhieuMH),	
		CONSTRAINT FK_HoaDon_Nhanvien FOREIGN KEY (TaikhoanNVlap) REFERENCES Nhanvien(TaikhoanNV);	
go

ALTER TABLE BaoHanh
	ADD CONSTRAINT FK_BaoHanh_Nhanvien FOREIGN KEY (TaikhoanNVlap) REFERENCES Nhanvien(TaikhoanNV);	
go

ALTER TABLE ChitietBH
	ADD CONSTRAINT FK_ChitietBH_Baohanh FOREIGN KEY (MaBH) REFERENCES BaoHanh(MaBH),
		CONSTRAINT FK_ChitietBH_Nhanvien FOREIGN KEY (TaikhoanNV) REFERENCES NhanVien(TaikhoanNV);
go
ALTER TABLE Phanhoi
	ADD CONSTRAINT FK_Phanhoi_KhachHang FOREIGN KEY (TaikhoanKH) REFERENCES KhachHang(TaikhoanKH),
		CONSTRAINT FK_Phanhoi_Nhanvien FOREIGN KEY (TaikhoanNV) REFERENCES NhanVien(TaikhoanNV);
go