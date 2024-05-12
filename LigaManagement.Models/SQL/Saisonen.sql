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
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (2, N'2022/23', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (3, N'2021/22', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (4, N'2020/21', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (5, N'2019/20', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (6, N'2018/19', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (7, N'2017/18', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (8, N'2016/17', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (9, N'2015/16', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (10, N'2014/15', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (11, N'2013/14', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (12, N'2012/13', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (13, N'2011/12', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (14, N'2010/11', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (15, N'2009/10', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (16, N'2008/09', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (17, N'2007/08', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (18, N'2006/07', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (19, N'2005/06', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (20, N'2004/05', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (21, N'2003/04', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (22, N'2002/03', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (23, N'2001/02', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (24, N'2000/01', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (25, N'1997/98', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (26, N'1998/99', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (27, N'1999/00', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (28, N'1996/97', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (29, N'1995/96', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (30, N'1994/95', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (31, N'1993/94', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (35, N'1963/64', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (36, N'1964/65', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (37, N'1965/66', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (39, N'1966/67', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (40, N'1967/68', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (41, N'1968/69', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (42, N'1969/70', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (43, N'1970/71', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (44, N'1971/72', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (45, N'1972/73', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (46, N'1973/74', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (47, N'1974/75', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (48, N'1975/76', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (50, N'1976/77', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (51, N'1977/78', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (52, N'1978/79', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (53, N'1979/80', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (54, N'1980/81', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (55, N'1981/82', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (56, N'1982/83', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (57, N'1983/84', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (58, N'1984/85', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (59, N'1985/86', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (60, N'1986/87', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (63, N'1987/88', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (64, N'1988/89', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (65, N'1989/90', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (66, N'1990/91', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (67, N'1991/92', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (68, N'1992/93', 1, N'Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (76, N'2023/24', 2, N'2. Bundesliga', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (77, N'2022/23', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (78, N'2021/22', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (79, N'2020/21', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (80, N'2019/20', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (81, N'2018/19', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (82, N'2017/18', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (83, N'2016/17', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (84, N'2015/16', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (85, N'2014/15', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (86, N'2013/14', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (87, N'2012/13', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (88, N'2011/12', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (89, N'2010/11', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (90, N'2009/10', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (91, N'2008/09', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (92, N'2007/08', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (93, N'2006/07', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (94, N'2005/06', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (95, N'2004/05', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (96, N'2003/04', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (97, N'2002/03', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (98, N'2001/02', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (99, N'2000/01', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (100, N'1999/00', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (101, N'1998/99', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (102, N'1997/98', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (103, N'1996/97', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (104, N'1995/96', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (105, N'1994/95', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (106, N'1993/94', 2, N'2. Bundesliga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (107, N'2023/24', 4, N'Premier League', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (108, N'2022/23', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (109, N'2021/22', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (110, N'2020/21', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (111, N'2019/20', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (112, N'2018/19', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (113, N'2017/18', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (114, N'2016/17', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (115, N'2015/16', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (116, N'2014/15', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (117, N'2013/14', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (118, N'2012/13', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (119, N'2011/12', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (120, N'2010/11', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (121, N'2009/10', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (122, N'2008/09', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (123, N'2007/08', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (124, N'2006/07', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (125, N'2005/06', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (126, N'2004/05', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (127, N'2003/04', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (128, N'2002/03', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (129, N'2001/02', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (130, N'2000/01', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (131, N'1999/00', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (132, N'1998/99', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (133, N'1997/98', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (134, N'1996/97', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (135, N'1995/96', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (136, N'1994/95', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (137, N'1993/94', 4, N'Premier League', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (138, N'2023/24', 6, N'Serie A', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (139, N'2022/23', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (140, N'2021/22', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (141, N'2020/21', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (142, N'2019/20', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (143, N'2018/19', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (144, N'2017/18', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (145, N'2016/17', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (146, N'2015/16', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (147, N'2014/15', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (148, N'2013/14', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (149, N'2012/13', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (151, N'2011/12', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (152, N'2010/11', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (153, N'2009/10', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (154, N'2008/09', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (155, N'2007/08', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (156, N'2006/07', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (157, N'2005/06', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (158, N'2004/05', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (159, N'2003/04', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (160, N'2002/03', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (161, N'2001/02', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (162, N'2000/01', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (163, N'1999/00', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (164, N'1998/99', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (165, N'1997/98', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (166, N'1996/97', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (167, N'1995/96', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (168, N'1994/95', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (169, N'1993/94', 6, N'Serie A', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (170, N'2023/24', 7, N'Ligue 1', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (171, N'2022/23', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (172, N'2021/22', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (173, N'2020/21', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (174, N'2019/00', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (175, N'2018/19', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (176, N'2017/18', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (177, N'2016/17', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (178, N'2015/16', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (179, N'2014/15', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (180, N'2013/14', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (181, N'2012/13', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (182, N'2011/12', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (183, N'2010/11', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (184, N'2009/10', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (185, N'2008/09', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (186, N'2007/08', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (187, N'2006/07', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (188, N'2005/06', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (189, N'2004/05', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (190, N'2003/04', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (191, N'2002/03', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (192, N'2001/02', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (193, N'2000/01', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (194, N'1999/00', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (195, N'1998/99', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (196, N'1997/98', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (197, N'1996/97', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (198, N'1995/96', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (199, N'1994/95', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (200, N'1993/94', 7, N'Ligue 1', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (201, N'2023/24', 8, N'La Liga', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (202, N'2022/23', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (203, N'2021/22', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (204, N'2020/21', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (205, N'2019/20', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (206, N'2018/19', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (207, N'2017/18', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (208, N'2016/17', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (209, N'2015/16', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (210, N'2014/15', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (211, N'2013/14', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (212, N'2012/13', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (213, N'2011/12', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (214, N'2010/11', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (215, N'2009/10', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (216, N'2008/09', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (217, N'2007/08', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (218, N'2006/07', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (219, N'2005/06', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (220, N'2004/05', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (221, N'2003/04', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (222, N'2002/03', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (223, N'2001/02', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (224, N'2000/01', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (225, N'1999/00', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (226, N'1998/99', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (227, N'1997/98', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (228, N'1996/97', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (229, N'1995/96', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (230, N'1994/95', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (231, N'1993/94', 8, N'La Liga', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (232, N'2023/24', 9, N'Eredivisie', 1, 0)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (233, N'2022/23', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (234, N'2021/22', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (235, N'2020/21', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (236, N'2019/20', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (237, N'2018/19', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (238, N'2017/18', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (239, N'2016/17', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (240, N'2015/16', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (241, N'2014/15', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (242, N'2013/14', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (243, N'2012/13', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (244, N'2011/12', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (245, N'2010/11', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (246, N'2009/10', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (247, N'2008/09', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (248, N'2007/08', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (249, N'2006/07', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (250, N'2005/06', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (251, N'2004/05', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (252, N'2003/04', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (253, N'2002/03', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (254, N'2001/02', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (255, N'2000/01', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (256, N'1999/00', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (257, N'1998/99', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (258, N'1997/98', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (259, N'1996/97', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (260, N'1995/96', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (261, N'1994/95', 9, N'Eredivisie', 0, 1)
GO
INSERT [dbo].[Saisonen] ([SaisonID], [Saisonname], [LigaID], [Liganame], [Aktuell], [Abgeschlossen]) VALUES (262, N'1993/94', 9, N'Eredivisie', 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Saisonen] OFF
GO
