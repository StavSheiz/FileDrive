USE [master]
GO

/****** Object:  Database [FileDriveDB]    Script Date: 29/12/2020 16:00:24 ******/
CREATE DATABASE [FileDriveDB]
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FileDriveDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [FileDriveDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [FileDriveDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [FileDriveDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [FileDriveDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [FileDriveDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [FileDriveDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [FileDriveDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [FileDriveDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [FileDriveDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [FileDriveDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [FileDriveDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [FileDriveDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [FileDriveDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [FileDriveDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [FileDriveDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [FileDriveDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [FileDriveDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [FileDriveDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [FileDriveDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [FileDriveDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [FileDriveDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [FileDriveDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [FileDriveDB] SET RECOVERY FULL 
GO

ALTER DATABASE [FileDriveDB] SET  MULTI_USER 
GO

ALTER DATABASE [FileDriveDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [FileDriveDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [FileDriveDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [FileDriveDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [FileDriveDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [FileDriveDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [FileDriveDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [FileDriveDB] SET  READ_WRITE 
GO


insert [Tree_Entities] ([Id],[Name],[ParentId])
select 1,'Photos',NULL UNION ALL
select 2,'Documents',NULL UNION ALL
select 3,'Pdf Docs',2 UNION ALL
select 4,'Word Docs',2 UNION ALL
select 5,'Wedding',1 UNION ALL
select 6,'Birthday',1 UNION ALL
select 7,'Bar Mitzvah',1;
