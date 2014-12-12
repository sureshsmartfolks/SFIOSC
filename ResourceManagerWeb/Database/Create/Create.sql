USE [Dev_ResourceManager]
GO

/****** Object:  Table [dbo].[Translator]    Script Date: 3/27/2014 9:21:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Translator](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[note] [nvarchar](2000) NULL
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[TranslatorLanguage]    Script Date: 3/27/2014 9:21:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TranslatorLanguage](
	[language] [nvarchar](100) NULL,
	[translatorID] [int] NULL
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Contact]    Script Date: 3/27/2014 9:22:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[translatorID] [int] NOT NULL,
	[contactType] [nvarchar](100) NULL,
	[contactValue] [nvarchar](2000) NULL
) ON [PRIMARY]

GO