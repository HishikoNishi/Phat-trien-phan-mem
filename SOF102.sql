USE [master]
GO
/****** Object:  Database [SOF102]    Script Date: 1/24/2025 5:49:16 PM ******/
CREATE DATABASE [SOF102]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SOF102', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOF102.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SOF102_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOF102_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SOF102] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SOF102].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SOF102] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SOF102] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SOF102] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SOF102] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SOF102] SET ARITHABORT OFF 
GO
ALTER DATABASE [SOF102] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SOF102] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SOF102] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SOF102] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SOF102] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SOF102] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SOF102] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SOF102] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SOF102] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SOF102] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SOF102] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SOF102] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SOF102] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SOF102] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SOF102] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SOF102] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SOF102] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SOF102] SET RECOVERY FULL 
GO
ALTER DATABASE [SOF102] SET  MULTI_USER 
GO
ALTER DATABASE [SOF102] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SOF102] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SOF102] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SOF102] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SOF102] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SOF102] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SOF102', N'ON'
GO
ALTER DATABASE [SOF102] SET QUERY_STORE = ON
GO
ALTER DATABASE [SOF102] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SOF102]
GO
/****** Object:  Table [dbo].[BaoCao]    Script Date: 1/24/2025 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCao](
	[MaBaoCao] [int] NOT NULL,
	[NgayBaoCao] [datetime] NOT NULL,
	[DoanhThu] [decimal](18, 2) NULL,
	[LoaiBaoCao] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaBaoCao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 1/24/2025 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaChiTietHoaDon] [int] NOT NULL,
	[MaHoaDon] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [decimal](18, 2) NOT NULL,
	[ThanhTien]  AS ([DonGia]*[SoLuong]) PERSISTED,
PRIMARY KEY CLUSTERED 
(
	[MaChiTietHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 1/24/2025 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [int] NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[SoBan] [int] NULL,
	[TongTien] [decimal](18, 2) NULL,
	[TrangThai] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 1/24/2025 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [int] NOT NULL,
	[TenSanPham] [nvarchar](100) NOT NULL,
	[Gia] [decimal](18, 2) NOT NULL,
	[LoaiSanPham] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoanNguoiDung]    Script Date: 1/24/2025 5:49:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoanNguoiDung](
	[MaNguoiDung] [int] NOT NULL,
	[TenDangNhap] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](256) NOT NULL,
	[VaiTro] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
USE [master]
GO
ALTER DATABASE [SOF102] SET  READ_WRITE 
GO
