/****** Object:  Table [dbo].[ResourceProject]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[ResourceProject]
GO
/****** Object:  Table [dbo].[ResourceLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[ResourceLanguage]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[Resource]
GO
/****** Object:  Table [dbo].[ProjectResource]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[ProjectResource]
GO
/****** Object:  Table [dbo].[ProjectLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[ProjectLanguage]
GO
/****** Object:  Table [dbo].[ProjectDocument]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[ProjectDocument]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[Project]
GO
/****** Object:  Table [dbo].[LanguageLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[LanguageLanguage]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[Language]
GO
/****** Object:  Table [dbo].[DocumentLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[DocumentLanguage]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[Document]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP TABLE [dbo].[Client]
GO
/****** Object:  StoredProcedure [dbo].[UpdateProject]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[UpdateProject]
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[InsertProject]
GO
/****** Object:  StoredProcedure [dbo].[GetResourceAssociationsWithInformation]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[GetResourceAssociationsWithInformation]
GO
/****** Object:  StoredProcedure [dbo].[GetProject]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[GetProject]
GO
/****** Object:  StoredProcedure [dbo].[GetLanguageAssociationsWithInformation]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[GetLanguageAssociationsWithInformation]
GO
/****** Object:  StoredProcedure [dbo].[GetLanguageAssociations]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[GetLanguageAssociations]
GO
/****** Object:  StoredProcedure [dbo].[GetLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[GetLanguage]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[GetAllProjects]
GO
/****** Object:  StoredProcedure [dbo].[DeleteResourceAssociations]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[DeleteResourceAssociations]
GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[DeleteProject]
GO
/****** Object:  StoredProcedure [dbo].[DeleteLanguageAssociations]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[DeleteLanguageAssociations]
GO
/****** Object:  StoredProcedure [dbo].[AddResourceAssociation]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[AddResourceAssociation]
GO
/****** Object:  StoredProcedure [dbo].[AddLanguageAssociation]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP PROCEDURE [dbo].[AddLanguageAssociation]
GO
/****** Object:  Database [Dev_Storm]    Script Date: 11/18/2014 4:28:17 PM ******/
DROP DATABASE [Dev_Storm]
GO
/****** Object:  Database [Dev_Storm]    Script Date: 11/18/2014 4:28:17 PM ******/
CREATE DATABASE [Dev_Storm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Dev_Storm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Dev_Storm.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Dev_Storm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Dev_Storm_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Dev_Storm] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Dev_Storm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Dev_Storm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Dev_Storm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Dev_Storm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Dev_Storm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Dev_Storm] SET ARITHABORT OFF 
GO
ALTER DATABASE [Dev_Storm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Dev_Storm] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Dev_Storm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Dev_Storm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Dev_Storm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Dev_Storm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Dev_Storm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Dev_Storm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Dev_Storm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Dev_Storm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Dev_Storm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Dev_Storm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Dev_Storm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Dev_Storm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Dev_Storm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Dev_Storm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Dev_Storm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Dev_Storm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Dev_Storm] SET RECOVERY FULL 
GO
ALTER DATABASE [Dev_Storm] SET  MULTI_USER 
GO
ALTER DATABASE [Dev_Storm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Dev_Storm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Dev_Storm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Dev_Storm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
/****** Object:  StoredProcedure [dbo].[AddLanguageAssociation]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddLanguageAssociation]
	@ProjectID int,
	@LanguageID int
AS
BEGIN

	SET NOCOUNT ON;

	INSERT into [ProjectLanguage] VALUES (@ProjectID,@LanguageID);
END

GO
/****** Object:  StoredProcedure [dbo].[AddResourceAssociation]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddResourceAssociation]
	@ProjectID int,
	@ResourceID int
AS
BEGIN

	SET NOCOUNT ON;

	INSERT into [ProjectLanguage] VALUES (@ProjectID,@ResourceID);
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteLanguageAssociations]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteLanguageAssociations]
	@ProjectID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE from [ProjectLanguage]
		WHERE ProjectID = @ProjectID;
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteProject]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteProject]
	@ProjectID int
AS
BEGIN

	SET NOCOUNT ON;

	DELETE from [Project] WHERE [ProjectID] = @ProjectID;
	DELETE from [ProjectLanguage] WHERE [ProjectID] = @ProjectID;
	DELETE from [ProjectResource] WHERE [ProjectID] = @ProjectID;
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteResourceAssociations]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteResourceAssociations]
	@ProjectID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE from [ProjectResource]
		WHERE ProjectID = @ProjectID;
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProjects]
AS
BEGIN
	SELECT * from [Project]
END

GO
/****** Object:  StoredProcedure [dbo].[GetLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLanguage]
	@LanguageID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From Language WHERE LanguageID = @LanguageID;
END

GO
/****** Object:  StoredProcedure [dbo].[GetLanguageAssociations]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetLanguageAssociations]
	@ProjectID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from ProjectLanguage WHERE ProjectID = @ProjectID
END

GO
/****** Object:  StoredProcedure [dbo].[GetLanguageAssociationsWithInformation]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetLanguageAssociationsWithInformation]
	@ProjectID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from ProjectLanguage INNER JOIN Language on ProjectLanguage.LanguageID = Language.LanguageID WHERE ProjectLanguage.ProjectID = @ProjectID;
END

GO
/****** Object:  StoredProcedure [dbo].[GetProject]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProject]
	@ProjectID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * from [Project] WHERE ProjectID = @ProjectID;
END

GO
/****** Object:  StoredProcedure [dbo].[GetResourceAssociationsWithInformation]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GetResourceAssociationsWithInformation]
	@ProjectID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from ProjectResource INNER JOIN Dev_ResourceManager.dbo.Translator on ProjectResource.ResourceID = Dev_ResourceManager.dbo.Translator.id WHERE ProjectResource.ProjectID = @ProjectID;
END

GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertProject]
	@RootFolderID int,
	@SourceLanguageID int
AS
BEGIN

	SET NOCOUNT ON;

	Insert into [Project] ([RootFolder],[SourceLanguage]) VALUES (@RootFolderID,@SourceLanguageID);
	SELECT SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateProject]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateProject]
	@ProjectID int,
	@RootFolderID int,
	@SourceLanguage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update [Project] Set 
		RootFolder = @RootFolderID,
		SourceLanguage = @SourceLanguage
		WHERE ProjectID = @ProjectID;
END

GO
/****** Object:  Table [dbo].[Client]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client](
	[clientID] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[phoneNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[clientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Document]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Document](
	[filePath] [varchar](max) NULL,
	[status] [nchar](10) NULL,
	[version] [int] NULL,
	[typeID] [int] NULL,
	[sourceLanguageID] [int] NULL,
	[documentID] [int] IDENTITY(1,1) NOT NULL,
	[projectID] [int] NULL,
 CONSTRAINT [PK_ProjectDocument] PRIMARY KEY CLUSTERED 
(
	[documentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DocumentLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentLanguage](
	[documentID] [int] NULL,
	[targetLanguageID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Language]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[shortName] [nchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LanguageLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LanguageLanguage](
	[sourceLanguageID] [int] NULL,
	[targetLanguageID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](99) NULL,
	[SourceLanguage] [int] NOT NULL,
	[RootFolder] [int] NULL,
	[WordCount] [int] NULL,
	[client] [int] NULL,
	[trackStatus] [nchar](10) NULL,
	[invoice] [nchar](10) NULL,
	[trackPath] [nchar](10) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectDocument]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDocument](
	[projectID] [int] NULL,
	[documentID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectLanguage](
	[ProjectID] [int] NOT NULL,
	[LanguageID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectResource]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectResource](
	[projectID] [int] NULL,
	[resourceID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resource]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[ProjectID] [int] NOT NULL,
	[ResourceID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](10) NULL,
	[isCompany] [bit] NULL,
	[type] [nchar](10) NULL,
	[languageAssociations] [int] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResourceLanguage]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceLanguage](
	[resourceID] [int] NULL,
	[sourceLanguageID] [int] NULL,
	[targetLanguageID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ResourceProject]    Script Date: 11/18/2014 4:28:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceProject](
	[resourceID] [nchar](10) NULL,
	[projectID] [nchar](10) NULL
) ON [PRIMARY]

GO
ALTER DATABASE [Dev_Storm] SET  READ_WRITE 
GO
