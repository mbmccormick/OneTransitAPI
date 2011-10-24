USE [master]
GO

/****** Object:  Database [OneTransitAPI]    Script Date: 10/24/2011 16:26:39 ******/
CREATE DATABASE [OneTransitAPI]
GO

ALTER DATABASE [OneTransitAPI] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OneTransitAPI].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO

ALTER DATABASE [OneTransitAPI] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [OneTransitAPI] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [OneTransitAPI] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [OneTransitAPI] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [OneTransitAPI] SET ARITHABORT OFF 
GO

ALTER DATABASE [OneTransitAPI] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [OneTransitAPI] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [OneTransitAPI] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [OneTransitAPI] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [OneTransitAPI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [OneTransitAPI] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [OneTransitAPI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [OneTransitAPI] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [OneTransitAPI] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [OneTransitAPI] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [OneTransitAPI] SET  DISABLE_BROKER 
GO

ALTER DATABASE [OneTransitAPI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [OneTransitAPI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [OneTransitAPI] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [OneTransitAPI] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO

ALTER DATABASE [OneTransitAPI] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [OneTransitAPI] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [OneTransitAPI] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [OneTransitAPI] SET  READ_WRITE 
GO

ALTER DATABASE [OneTransitAPI] SET RECOVERY FULL 
GO

ALTER DATABASE [OneTransitAPI] SET  MULTI_USER 
GO

ALTER DATABASE [OneTransitAPI] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [OneTransitAPI] SET DB_CHAINING OFF 
GO


