USE [LigaManager]
IF OBJECT_ID(N'dbo.[Tore]', N'U') IS NULL

CREATE TABLE [dbo].[Tore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [int] NULL,
	[SaisonID] [int] NULL,
	[LigaID] [int] NULL,
	[Spielminute] [int] NULL,
	[SpielerID] [int] NULL,
	[Spielstand] [nchar](10) NULL,
	[SpieltagsID] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tore] ON 
GO
INSERT [dbo].[Tore] ([ID], [SpieltagNr], [SaisonID], [LigaID], [Spielminute], [SpielerID], [Spielstand], [SpieltagsID]) VALUES (1, 26, 1, 1, 16, 12, N'0:1       ', 14476)
GO
INSERT [dbo].[Tore] ([ID], [SpieltagNr], [SaisonID], [LigaID], [Spielminute], [SpielerID], [Spielstand], [SpieltagsID]) VALUES (2, 26, 1, 1, 45, 9, N'0:2       ', 14476)
GO
INSERT [dbo].[Tore] ([ID], [SpieltagNr], [SaisonID], [LigaID], [Spielminute], [SpielerID], [Spielstand], [SpieltagsID]) VALUES (3, 26, 1, 1, 68, 14, N'0:3       ', 14476)
GO
INSERT [dbo].[Tore] ([ID], [SpieltagNr], [SaisonID], [LigaID], [Spielminute], [SpielerID], [Spielstand], [SpieltagsID]) VALUES (4, 26, 1, 1, 16, 15, N'1:1       ', 14476)
GO
SET IDENTITY_INSERT [dbo].[Tore] OFF
GO

