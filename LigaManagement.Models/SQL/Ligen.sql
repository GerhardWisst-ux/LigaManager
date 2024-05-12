USE [LigaManager]
--IF OBJECT_ID(N'dbo.VereineSaison', N'U') IS NULL

CREATE TABLE [dbo].[Ligen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Liganame] [nvarchar](max) NOT NULL,
	[Verband] [nvarchar](max) NOT NULL,
	[Erstaustragung] [datetime2](7) NOT NULL,
	[Absteiger] [int] NOT NULL,
	[Aktiv] [nvarchar](max) NULL,
 CONSTRAINT [PK_Ligen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Ligen] ON;  
GO
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (1, N'Bundesliga', N'DFB', CAST(N'1963-08-14T00:00:00.0000000' AS DateTime2), 3, N'True')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (2, N'2. Bundesliga', N'DFB', CAST(N'1991-08-14T00:00:00.0000000' AS DateTime2), 3, N'True')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (3, N'3. Liga', N'DFB', CAST(N'2008-08-14T00:00:00.0000000' AS DateTime2), 3, N'False')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (4, N'Premier League', N'FA', CAST(N'1992-08-15T00:00:00.0000000' AS DateTime2), 3, N'True')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (6, N'Serie A', N'FA', CAST(N'1992-08-15T00:00:00.0000000' AS DateTime2), 3, N'True')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (7, N'Ligue 1', N'FA', CAST(N'1992-08-15T00:00:00.0000000' AS DateTime2), 3, N'True')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (8, N'La Liga', N'FA', CAST(N'1992-08-15T00:00:00.0000000' AS DateTime2), 3, N'True')
GO
INSERT [dbo].[Ligen] ([Id], [Liganame], [Verband], [Erstaustragung], [Absteiger], [Aktiv]) VALUES (9, N'Eredivisie', N'FA', CAST(N'1992-08-15T00:00:00.0000000' AS DateTime2), 3, N'True')
GO