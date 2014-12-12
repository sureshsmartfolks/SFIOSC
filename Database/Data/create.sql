SET IDENTITY_INSERT [dbo].[Client] ON 
GO
INSERT [dbo].[Client] ([clientID], [name], [email], [phoneNumber]) VALUES (1, N'TIMEX', NULL, NULL)
GO
INSERT [dbo].[Client] ([clientID], [name], [email], [phoneNumber]) VALUES (2, N'Atlantis Rising', NULL, NULL)
GO
INSERT [dbo].[Client] ([clientID], [name], [email], [phoneNumber]) VALUES (3, N'Bagel', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Document] ON 

GO
INSERT [dbo].[Document] ([filePath], [status], [version], [typeID], [sourceLanguageID], [documentID], [projectID]) VALUES (N'x:\', N'not done  ', 1, 12, 1, 1, 1)
GO
INSERT [dbo].[Document] ([filePath], [status], [version], [typeID], [sourceLanguageID], [documentID], [projectID]) VALUES (N'x:\mentalCase\goo.doc', N'done      ', 13, 1, 2, 2, 2)
GO
INSERT [dbo].[Document] ([filePath], [status], [version], [typeID], [sourceLanguageID], [documentID], [projectID]) VALUES (N'x:\mentalCase\goo.png', N'LIMBO     ', 2, 2, 2, 3, 2)
GO
SET IDENTITY_INSERT [dbo].[Document] OFF
GO
INSERT [dbo].[DocumentLanguage] ([documentID], [targetLanguageID]) VALUES (1, NULL)
GO
INSERT [dbo].[DocumentLanguage] ([documentID], [targetLanguageID]) VALUES (1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Language] ON 

GO
INSERT [dbo].[Language] ([LanguageID], [Name], [shortName]) VALUES (1, N'English', N'en        ')
GO
INSERT [dbo].[Language] ([LanguageID], [Name], [shortName]) VALUES (2, N'French', N'fr        ')
GO
INSERT [dbo].[Language] ([LanguageID], [Name], [shortName]) VALUES (3, N'Spanish', N'es        ')
GO
INSERT [dbo].[Language] ([LanguageID], [Name], [shortName]) VALUES (4, N'Japanese', N'ja        ')
GO
INSERT [dbo].[Language] ([LanguageID], [Name], [shortName]) VALUES (5, N'Portugese', N'pt        ')
GO
INSERT [dbo].[Language] ([LanguageID], [Name], [shortName]) VALUES (6, N'Russian', N'ru        ')
GO
SET IDENTITY_INSERT [dbo].[Language] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

GO
INSERT [dbo].[Project] ([ProjectID], [Title], [SourceLanguage], [RootFolder], [WordCount], [client], [trackStatus], [invoice], [trackPath]) VALUES (1, N'Nike Test Project', 1, 1, 250, 1, N'1         ', N'0         ', N'1         ')
GO
INSERT [dbo].[Project] ([ProjectID], [Title], [SourceLanguage], [RootFolder], [WordCount], [client], [trackStatus], [invoice], [trackPath]) VALUES (2, N'Timex Test Project', 3, 2, 300, 1, N'2         ', N'0         ', N'1         ')
GO
INSERT [dbo].[Project] ([ProjectID], [Title], [SourceLanguage], [RootFolder], [WordCount], [client], [trackStatus], [invoice], [trackPath]) VALUES (3, N'Frank Test Project', 1, 3, 500, 2, N'1         ', N'0         ', N'2         ')
GO
INSERT [dbo].[Project] ([ProjectID], [Title], [SourceLanguage], [RootFolder], [WordCount], [client], [trackStatus], [invoice], [trackPath]) VALUES (4, N'Neil Test Project', 3, 1, 430, 3, N'1         ', N'0         ', N'1         ')
GO
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (1, 4)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (1, 6)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (2, 2)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (2, 4)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (3, 2)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (3, 6)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (4, 1)
GO
INSERT [dbo].[ProjectLanguage] ([ProjectID], [LanguageID]) VALUES (4, 5)
GO
SET IDENTITY_INSERT [dbo].[Resource] ON 

GO
INSERT [dbo].[Resource] ([ProjectID], [ResourceID], [name], [isCompany], [type], [languageAssociations]) VALUES (1, 1, N'Janet     ', NULL, N'1         ', 1)
GO
INSERT [dbo].[Resource] ([ProjectID], [ResourceID], [name], [isCompany], [type], [languageAssociations]) VALUES (2, 2, N'Ben       ', NULL, N'1         ', 2)
GO
SET IDENTITY_INSERT [dbo].[Resource] OFF
GO
INSERT [dbo].[ResourceProject] ([resourceID], [projectID]) VALUES (N'1         ', N'1         ')
GO
INSERT [dbo].[ResourceProject] ([resourceID], [projectID]) VALUES (N'1         ', N'2         ')
GO
INSERT [dbo].[ResourceProject] ([resourceID], [projectID]) VALUES (N'2         ', N'2         ')
GO
INSERT [dbo].[ResourceProject] ([resourceID], [projectID]) VALUES (N'2         ', N'3         ')
GO
