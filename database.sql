CREATE TABLE library.dbo.TheLoai (
	id int IDENTITY(1,1) NOT NULL,
	TenLoai nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__TheLoai__3213E83FE134E4B2 PRIMARY KEY (id)
);EATE TABLE library.dbo.Sach (


CREATE TABLE library.dbo.TheLoai (
	id int IDENTITY(1,1) NOT NULL,
	TenLoai nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__TheLoai__3213E83FE134E4B2 PRIMARY KEY (id)
	id int IDENTITY(1,1) NOT NULL,
	TenSach nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TacGia nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	NamXuatBan int NULL,
	TheLoaiId int NULL,
	SoLuong int NOT NULL,
	CONSTRAINT PK__Sach__3213E83FEE027662 PRIMARY KEY (id),
	CONSTRAINT FK__Sach__TheLoaiId__44FF419A FOREIGN KEY (TheLoaiId) REFERENCES library.dbo.TheLoai(id)
);

CREATE TABLE library.dbo.KhachHang (
	id int IDENTITY(1,1) NOT NULL,
	TenKhachHang nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	SoDienThoai nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__KhachHan__3213E83F1DD5849A PRIMARY KEY (id)
);

CREATE TABLE library.dbo.PhieuThue (
	id int IDENTITY(1,1) NOT NULL,
	KhachHangId int NOT NULL,
	NgayThue datetime DEFAULT getdate() NULL,
	NgayTra datetime NULL,
	TrangThai nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Ðang mu?n' NULL,
	CONSTRAINT PK__PhieuThu__3213E83FE1C8BD19 PRIMARY KEY (id),
	CONSTRAINT FK__PhieuThue__Khach__4BAC3F29 FOREIGN KEY (KhachHangId) REFERENCES library.dbo.KhachHang(id)
);

CREATE TABLE library.dbo.ChiTietPhieuThue (
	id int IDENTITY(1,1) NOT NULL,
	PhieuThueId int NOT NULL,
	SachId int NOT NULL,
	SoLuong int NOT NULL,
	CONSTRAINT PK__ChiTietP__3213E83FF5C0D0F1 PRIMARY KEY (id),
	CONSTRAINT FK__ChiTietPh__Phieu__4E88ABD4 FOREIGN KEY (PhieuThueId) REFERENCES library.dbo.PhieuThue(id),
	CONSTRAINT FK__ChiTietPh__SachI__4F7CD00D FOREIGN KEY (SachId) REFERENCES library.dbo.Sach(id)
);