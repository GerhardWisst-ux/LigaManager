USE [LigaManager]
--IF OBJECT_ID(N'dbo.[Saisonen]', N'U') IS NULL

CREATE TABLE [dbo].[Saisonen](
	[SaisonID] [int] IDENTITY(1,1) NOT NULL,
	[Saisonname] [nvarchar](max) NOT NULL,
	[LigaID] [int] NOT NULL,
	[Liganame] [nvarchar](max) NOT NULL,
	[Aktuell] [bit] NOT NULL,
	[Abgeschlossen] [bit] NOT NULL,
 CONSTRAINT [PK_Saisonen] PRIMARY KEY CLUSTERED 
(
	[SaisonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Saisonen] ON 
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (1, N'2023/24', 1, N'Bundesliga', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (2, N'2022/23', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (3, N'2021/22', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (4, N'2020/21', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (5, N'2019/20', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (6, N'2018/19', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (7, N'2017/18', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (8, N'2016/17', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (9, N'2015/16', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (10, N'2014/15', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (11, N'2013/14', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (12, N'2012/13', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (13, N'2011/12', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (14, N'2010/11', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (15, N'2009/10', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (16, N'2008/09', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (17, N'2007/08', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (18, N'2006/07', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (19, N'2005/06', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (20, N'2004/05', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (21, N'2003/04', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (22, N'2002/03', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (23, N'2001/02', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (24, N'2000/01', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (25, N'1997/98', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (26, N'1998/99', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (27, N'1999/00', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (28, N'1996/97', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (29, N'1995/96', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (30, N'1994/95', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (31, N'1993/94', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (35, N'1963/64', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (36, N'1964/65', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (37, N'1965/66', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (38, N'1998/99', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (39, N'1966/67', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (40, N'1967/68', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (41, N'1968/69', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (42, N'1969/70', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (43, N'1970/71', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (44, N'1971/72', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (45, N'1972/73', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (46, N'1973/74', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (47, N'1974/75', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (48, N'1975/76', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (50, N'1976/77', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (51, N'1977/78', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (52, N'1978/79', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (53, N'1979/80', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (54, N'1980/81', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (55, N'1981/82', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (56, N'1982/83', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (57, N'1983/84', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (58, N'1984/85', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (59, N'1985/86', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (60, N'1986/87', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (63, N'1987/88', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (64, N'1988/89', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (65, N'1989/90', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (66, N'1990/91', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (67, N'1991/92', 1, N'Bundesliga', 1, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (68, N'1992/93', 1, N'Bundesliga', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Saisonen] OFF
GO
