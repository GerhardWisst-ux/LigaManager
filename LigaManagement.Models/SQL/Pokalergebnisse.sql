USE [LigaManager]
IF OBJECT_ID(N'dbo.[Tore]', N'U') IS NULL

CREATE TABLE [dbo].[Pokalergebnisse](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[Verein1_Nr] [nvarchar](max) NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [nvarchar](max) NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Runde] [nvarchar](max) NULL,
	[Zuschauer] [int] NULL,
	[Verlängerung] [bit] NULL,
	[Elfmeterschiessen] [bit] NULL,
 CONSTRAINT [Pokalergebnisse2] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Pokalergebnisse] ON 
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (3, N'2023/24', 1, N'6', N'Bayer Leverkusen', N'27', N'Fortuna Düsseldorf', 4, 0, CAST(N'2024-04-03T20:45:00.0000000' AS DateTime2), N'BAY-ARENA', N'SR', N'HF', 29500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (4, N'2023/24', 1, N'39', N'1. FC Saarbrücken', N'24', N'FC Kaiserslautern', 0, 2, CAST(N'2024-04-02T20:45:00.0000000' AS DateTime2), N'LUDWIGSPARKSTADION', N'Marco Fritz', N'HF', 15903, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (5, N'2023/24', 1, N'36', N'FC St. Pauli', N'24', N'Fortuna Düsseldorf', 3, 4, CAST(N'2024-01-30T20:45:00.0000000' AS DateTime2), N'MILLERNTOR', N'SR', N'VF', 29500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (6, N'2023/24', 1, N'6', N'Bayer Leverkusen', N'16', N'VfB Stuttgart', 3, 2, CAST(N'2024-02-06T20:45:00.0000000' AS DateTime2), N'BAY-ARENA', N'Daniel Schlager', N'VF', 30210, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (8, N'2023/24', 1, N'39', N'1. FC Saarbrücken', N'10', N'Bor. Mönchengladbach', 2, 1, CAST(N'2024-03-12T20:00:00.0000000' AS DateTime2), N'LUDWIGSPARKSTADION', N'SR', N'VF', 15903, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (9, N'2023/24', 1, N'24', N'FC Kaiserslautern', N'24', N'FC Nürnberg', 2, 0, CAST(N'2023-12-05T20:00:00.0000000' AS DateTime2), N'FRITZ-WALTER-STADION', N'SR', N'AF', 16000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (10, N'2023/24', 1, N'10', N'Bor. Mönchengladbach', N'8', N'VFL Wolfsburg', 1, 0, CAST(N'2023-12-05T20:00:00.0000000' AS DateTime2), N'BORUSSIA-PARK', N'SR', N'AF', 16000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (11, N'2023/24', 1, N'56', N'FC 08 Homburg', N'36', N'FC St. Pauli', 1, 4, CAST(N'2023-12-05T20:00:00.0000000' AS DateTime2), N'WALDSTADION', N'SR', N'AF', 10000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (12, N'2023/24', 1, N'16', N'VfB Stuttgart', N'2', N'Borussia Dortmund', 2, 0, CAST(N'2023-12-06T20:30:00.0000000' AS DateTime2), N'MHP-ARENA', N'SR', N'AF', 29500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (13, N'2023/24', 1, N'20', N'Hertha BSC', N'26', N'Hamburger SV', 5, 3, CAST(N'2023-12-06T20:30:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'SR', N'AF', 60000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (14, N'2023/24', 1, N'39', N'1. FC Saarbrücken', N'7', N'Eintracht Frankfurt', 2, 0, CAST(N'2023-12-06T20:30:00.0000000' AS DateTime2), N'LUDWIGSPARKSTADION', N'SR', N'AF', 16000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (15, N'2023/24', 1, N'6', N'Bayer Leverkusen', N'22', N'SC Paderborn 07', 3, 1, CAST(N'2023-12-06T20:30:00.0000000' AS DateTime2), N'BAY-ARENA', N'SR', N'AF', 29500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (16, N'2023/24', 1, N'16', N'VfB Stuttgart', N'4', N'Union Berlin', 1, 0, CAST(N'2023-10-31T18:00:00.0000000' AS DateTime2), N'MHP-ARENA', N'Sascha Stegemann', N'2', 52000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (17, N'2023/24', 1, N'24', N'FC Kaiserslautern', N'11', N'FC Köln', 3, 2, CAST(N'2023-10-31T20:30:00.0000000' AS DateTime2), N'FRITZ-WALTER-STADION', N'SR', N'2', 16000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (18, N'2023/24', 1, N'6', N'Bayer Leverkusen', N'24', N'FC Kaiserslautern', 0, 0, CAST(N'2024-05-25T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'SR', N'F', 77500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (19, N'2023/24', 1, N'10', N'Bor. Mönchengladbach', N'18', N'FC Heidenheim', 3, 1, CAST(N'2023-10-31T20:00:00.0000000' AS DateTime2), N'BORUSSIA-PARK', N'SR', N'2', 50000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (20, N'2023/24', 1, N'8', N'VFL Wolfsburg', N'3', N'RB Leipzig', 1, 0, CAST(N'2023-10-31T20:00:00.0000000' AS DateTime2), N'VOLKSWAGEN-ARENA', N'SR', N'2', 16000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (21, N'2023/24', 1, N'45', N'SpVgg Unterhaching', N'24', N'Fortuna Düsseldorf', 3, 6, CAST(N'2023-10-31T18:30:00.0000000' AS DateTime2), N'SPORTPARK', N'SR', N'2', 15000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (22, N'2023/24', 1, N'56', N'FC 08 Homburg', N'21', N'Greuther Fürth', 2, 1, CAST(N'2023-10-31T20:45:00.0000000' AS DateTime2), N'WALDSTADION', N'SR', N'2', 10000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (23, N'2023/24', 1, N'20', N'Hertha BSC', N'24', N'FC Kaiserslautern', 1, 3, CAST(N'2024-01-31T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'SR', N'VF', 60000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (24, N'2023/24', 1, N'58', N'1.FC Magdeburg', N'36', N'FC St. Pauli', 1, 3, CAST(N'2023-12-05T20:00:00.0000000' AS DateTime2), N'MDCC-Arena', N'Robert Hartmann', N'AF', 20090, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (25, N'2023/24', 1, N'2', N'Borussia Dortmund', N'12', N'TSG Hoffenheim', 1, 0, CAST(N'2023-11-01T18:00:00.0000000' AS DateTime2), N'Signal Iduna Park', N'Harm Osmers', N'2', 81365, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (26, N'2023/24', 1, N'39', N'1. FC Saarbrücken', N'1', N'Bayern München', 2, 1, CAST(N'2023-11-01T20:45:00.0000000' AS DateTime2), N'LUDWIGSPARKSTADION', N'Frank Willenborg', N'2', 15903, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (27, N'2023/24', 1, N'25', N'FC Nürnberg', N'43', N'FC Hansa Rostock', 3, 2, CAST(N'2023-11-01T20:45:00.0000000' AS DateTime2), N'Max-Morlock Stadion', N'Arne Aarnick', N'2', 28489, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (28, N'2023/24', 1, N'42', N'Arminia Bielefeld', N'26', N'Hamburger SV', 3, 4, CAST(N'2023-10-31T20:45:00.0000000' AS DateTime2), N'Schüco-Arena', N'Robert Kampka', N'2', 26561, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (29, N'2023/24', 1, N'36', N'FC St. Pauli', N'19', N'FC Schalke 04', 2, 1, CAST(N'2023-10-31T18:00:00.0000000' AS DateTime2), N'MILLERNTOR', N'Bastian Dankert', N'2', 29500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (30, N'2023/24', 1, N'5', N'SC Freiburg', N'22', N'SC Paderborn 07', 1, 3, CAST(N'2023-11-01T18:00:00.0000000' AS DateTime2), N'Badenova-Stadion', N'Christian Dingert', N'2', 31500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (31, N'2023/24', 1, N'20', N'Hertha BSC', N'9', N'FSV Mainz 05', 3, 0, CAST(N'2023-11-01T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Matthias Jöllenbeck', N'2', 29421, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (32, N'2023/24', 1, N'59', N'Holstein Kiel', N'58', N'1.FC Magdeburg', 3, 4, CAST(N'2023-11-01T18:00:00.0000000' AS DateTime2), N'Holstein-Stadion', N'Tom Bauer', N'2', 11112, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (33, N'2023/24', 1, N'60', N'FC Viktoria Köln', N'7', N'Eintracht Frankfurt', 3, 4, CAST(N'2023-11-01T18:00:00.0000000' AS DateTime2), N'Sportpark Höhenberg', N'Max Burda', N'2', 8343, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (34, N'2023/24', 1, N'61', N'
SV Sandhausen', N'6', N'Bayer Leverkusen', 2, 5, CAST(N'2023-11-01T18:00:00.0000000' AS DateTime2), N'Hardtwaldstadion', N'Richard Hempel', N'2', 10222, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (43, N'2022/23', 2, N'3', N'RB Leipzig', N'7', N'Eintracht Frankfurt', 2, 0, CAST(N'2023-05-25T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Daniel Siebert', N'F', 74322, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (44, N'2022/23', 2, N'16', N'VfB Stuttgart', N'7', N'Eintracht Frankfurt', 0, 2, CAST(N'2023-05-03T20:45:00.0000000' AS DateTime2), N'MHP-ARENA', N'Daniel Schlager', N'HF', 47500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (45, N'2022/23', 2, N'6', N'SC Freiburg', N'3', N'RB Leipzig', 1, 5, CAST(N'2023-05-02T20:45:00.0000000' AS DateTime2), N'Europa-Parkstadion', N'Sven Jablonski', N'HF', 34500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (46, N'2022/23', 2, N'25', N'FC Nürnberg', N'16', N'VfB Stuttgart', 0, 1, CAST(N'2023-04-05T20:45:00.0000000' AS DateTime2), N'Max-Morlock Stadion', N'Daniel Siebert', N'VF', 50000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (47, N'2022/23', 2, N'7', N'Eintracht Frankfurt', N'4', N'Union Berlin', 2, 0, CAST(N'2023-04-04T20:45:00.0000000' AS DateTime2), N'DEUTSCHE-BANK-PARK', N'Bastian Dankert', N'VF', 49500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (48, N'2022/23', 2, N'3', N'RB Leipzig', N'2', N'Borussia Dortmund', 2, 0, CAST(N'2023-04-05T20:45:00.0000000' AS DateTime2), N'Red-Bull-Arena', N'Felix Brych', N'VF', 47069, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (49, N'2022/23', 2, N'1', N'Bayern München', N'5', N'SC Freiburg', 1, 2, CAST(N'2023-04-04T20:45:00.0000000' AS DateTime2), N'ALLIANZ-AERNA', N'Harm Osmers', N'VF', 75000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (50, N'2021/22', 3, N'5', N'SC Freiburg', N'3', N'RB Leipzig', 2, 4, CAST(N'2022-05-21T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Sascha Stegemann', N'F', 74322, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (51, N'2020/21', 4, N'3', N'RB Leipzig', N'2', N'Borussia Dortmund', 1, 4, CAST(N'2021-05-13T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Felix Brych', N'F', 74322, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (52, N'2019/20', 5, N'6', N'Bayer Leverkusen', N'1', N'Bayern München', 2, 4, CAST(N'2020-07-04T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Tobias Welz', N'F', 74322, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (53, N'2018/19', 6, N'3', N'RB Leipzig', N'1', N'Bayern München', 0, 3, CAST(N'2019-05-25T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Tobias Stieler', N'F', 74322, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (54, N'2017/18', 7, N'1', N'Bayern München', N'7', N'Eintracht Frankfurt', 1, 3, CAST(N'2019-05-19T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Felix Zwayer', N'F', 74322, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (55, N'2012/13', 12, N'1', N'Bayern München', N'16', N'VfB Stuttgart', 3, 2, CAST(N'2013-06-01T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Manuel Gräfe', N'F', 75420, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (56, N'2006/07', 18, N'25', N'FC Nürnberg', N'16', N'VfB Stuttgart', 3, 2, CAST(N'2007-05-26T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Michael Weiner', N'F', 74220, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (57, N'2016/17', 8, N'2', N'Borussia Dortmund', N'7', N'Eintracht Frankfurt', 2, 1, CAST(N'2017-05-27T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Deniz Aytekin', N'F', 74220, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (58, N'1971/72', 44, N'19', N'FC Schalke 04', N'24', N'FC Kaiserslautern', 5, 0, CAST(N'1972-07-01T20:00:00.0000000' AS DateTime2), N'Niedersachsenstadion', N'Heinz Aldinger', N'F', 61000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (59, N'2015/16', 9, N'1', N'Bayern München', N'2', N'Borussia Dortmund', 4, 3, CAST(N'2016-05-21T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Marco Fritz', N'F', 74220, 1, 1)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (60, N'2014/15', 10, N'8', N'VFL Wolfsburg', N'2', N'Borussia Dortmund', 3, 1, CAST(N'2015-05-30T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Felix Brych', N'F', 74220, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (61, N'2013/14', 11, N'8', N'Bayern München', N'2', N'Borussia Dortmund', 2, 0, CAST(N'2014-05-17T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Florian Meyer', N'F', 76197, 1, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (62, N'2011/12', 13, N'2', N'Borussia Dortmund', N'1', N'Bayern München', 5, 2, CAST(N'2012-05-12T20:45:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Peter Gagelmann', N'F', 75708, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (63, N'2010/11', 14, N'19', N'FC Schalke 04', N'44', N'MSV Duisburg', 5, 0, CAST(N'2011-05-21T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Peter Gagelmann', N'F', 75708, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (64, N'2009/10', 15, N'19', N'FC Schalke 04', N'1', N'Bayern München', 0, 4, CAST(N'2010-05-15T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Peter Gagelmann', N'F', 75708, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (65, N'2008/09', 16, N'6', N'Bayer Leverkusen', N'13', N'Werder Bremen', 0, 1, CAST(N'2009-05-30T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Helmut Fleischer', N'F', 74244, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (66, N'2007/08', 17, N'1', N'Bayern München', N'2', N'Borussia Dortmund', 2, 1, CAST(N'2008-04-19T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Knut Kircher', N'F', 74244, 1, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (67, N'2005/06', 18, N'1', N'Bayern München', N'7', N'Eintracht Frankfurt', 1, 0, CAST(N'2006-04-29T20:00:00.0000000' AS DateTime2), N'OLYMPIASTADION(Berlin)', N'Herbert Fandel', N'F', 74369, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (70, N'2022/23', 2, N'22', N'SC Paderborn 07', N'16', N'VfB Stuttgart', 1, 2, CAST(N'2023-01-31T18:00:00.0000000' AS DateTime2), N'HOME-DELUXE-ARENA', N'SR', N'AF', 15000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1069, N'2022/23', 2, N'4', N'Union Berlin', N'8', N'VFL Wolfsburg', 2, 1, CAST(N'2023-01-31T20:45:00.0000000' AS DateTime2), N'STADION AN DER ALTEN FÖRSTEREI', N'SR', N'AF', 22500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1070, N'2022/23', 2, N'3', N'RB Leipzig', N'12', N'TSG Hoffenheim', 3, 1, CAST(N'2023-02-01T18:00:00.0000000' AS DateTime2), N'Red-Bull-Arena', N'SR', N'AF', 44000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1071, N'2022/23', 2, N'9', N'FSV Mainz 05', N'1', N'Bayern München', 0, 4, CAST(N'2023-02-01T20:45:00.0000000' AS DateTime2), N'MEWA-ARENA', N'SR', N'AF', 30000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1072, N'2022/23', 2, N'61', N'SV Sandhausen', N'5', N'SC Freiburg', 0, 2, CAST(N'2023-02-07T18:00:00.0000000' AS DateTime2), N'Hardtwaldstadion', N'SR', N'AF', 10000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1073, N'2022/23', 2, N'7', N'Eintracht Frankfurt', N'17', N'SV Darmstadt 98', 4, 2, CAST(N'2023-02-07T20:45:00.0000000' AS DateTime2), N'DEUTSCHE-BANK-PARK', N'SR', N'AF', 50000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1074, N'2022/23', 2, N'14', N'VFL Bochum', N'2', N'Borussia Dortmund', 1, 2, CAST(N'2023-02-08T20:45:00.0000000' AS DateTime2), N'VONOVIA-RUHRSTADION', N'SR', N'AF', 26000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1075, N'2022/23', 2, N'16', N'VfB Stuttgart', N'42', N'Arminia Bielefeld', 6, 0, CAST(N'2022-10-19T18:00:00.0000000' AS DateTime2), N'MHP-ARENA', N'SR', N'2', 40000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1076, N'2022/23', 2, N'25', N'FC Nürnberg', N'27', N'Fortuna Düsseldorf', 5, 4, CAST(N'2023-02-07T18:00:00.0000000' AS DateTime2), N'Max-Morlock Stadion', N'SR', N'AF', 50000, 1, 1)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1077, N'2022/23', 2, N'57', N'Stuttgarter Kickers', N'7', N'Eintracht Frankfurt', 0, 2, CAST(N'2022-10-18T18:00:00.0000000' AS DateTime2), N'Stadion auf der Waldau', N'SR', N'2', 10000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1078, N'2022/23', 2, N'33', N'SV Waldhof Mannheim', N'25', N'FC Nürnberg', 0, 1, CAST(N'2022-10-18T18:00:00.0000000' AS DateTime2), N'CARL-BENZ STADION', N'SR', N'2', 26000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1079, N'2022/23', 2, N'61', N'SV Sandhausen', N'29', N'Karlsruher SC', 8, 7, CAST(N'2022-10-19T18:00:00.0000000' AS DateTime2), N'Hardtwaldstadion', N'SR', N'2', 8343, 1, 1)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1080, N'2022/23', 2, N'4', N'Union Berlin', N'18', N'FC Heidenheim', 2, 0, CAST(N'2022-10-19T20:45:00.0000000' AS DateTime2), N'STADION AN DER ALTEN FÖRSTEREI', N'SR', N'2', 22500, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1081, N'2022/23', 2, N'15', N'FC Augsburg', N'1', N'Bayern München', 2, 5, CAST(N'2022-10-19T20:45:00.0000000' AS DateTime2), N'WWK-ARENA', N'SR', N'2', 30000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1082, N'2022/23', 2, N'5', N'SC Freiburg', N'36', N'FC St. Pauli', 2, 1, CAST(N'2022-10-19T20:45:00.0000000' AS DateTime2), N'Badenova-Stadion', N'SR', N'2', 22000, 1, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1083, N'2022/23', 2, N'30', N'Eintracht Braunschweig', N'8', N'VFL Wolfsburg', 1, 2, CAST(N'2022-10-18T20:45:00.0000000' AS DateTime2), N'EINTRACHT-STADION', N'SR', N'2', 22000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1084, N'2022/23', 2, N'12', N'TSG Hoffenheim', N'19', N'FC Schalke 04', 5, 1, CAST(N'2022-10-18T20:45:00.0000000' AS DateTime2), N'PREZERO-ARENA', N'SR', N'2', 18000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1085, N'2022/23', 2, N'3', N'RB Leipzig', N'26', N'Hamburger SV', 4, 0, CAST(N'2022-10-18T18:00:00.0000000' AS DateTime2), N'Red-Bull-Arena', N'Benjamin Cortus', N'2', 44787, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1086, N'2022/23', 2, N'23', N'Hannover 96', N'2', N'Borussia Dortmund', 0, 2, CAST(N'2022-10-19T18:00:00.0000000' AS DateTime2), N'Niedersachsenstadion', N'Sven Jablonski', N'2', 49000, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1087, N'2022/23', 2, N'62', N'VfB Lübeck', N'9', N'FSV Mainz 05', 0, 3, CAST(N'2022-10-18T18:00:00.0000000' AS DateTime2), N'Lohmühle', N'Dr. Robin Braun', N'2', 9974, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1088, N'2022/23', 2, N'63', N'Jahn Regensburg', N'27', N'Fortuna Düsseldorf', 0, 2, CAST(N'2022-10-18T18:00:00.0000000' AS DateTime2), N'Jahn-Stadion', N'Timo Gerach', N'2', 7892, 0, 0)
GO
INSERT [dbo].[Pokalergebnisse] ([SpieltagId], [Saison], [SaisonID], [Verein1_Nr], [Verein1], [Verein2_Nr], [Verein2], [Tore1_Nr], [Tore2_Nr], [Datum], [Ort], [Schiedrichter], [Runde], [Zuschauer], [Verlängerung], [Elfmeterschiessen]) VALUES (1089, N'2022/23', 2, N'22', N'SC Paderborn 07', N'13', N'Werder Bremen', 5, 4, CAST(N'2022-10-19T18:00:00.0000000' AS DateTime2), N'HOME-DELUXE-ARENA', N'Frank Willenborg', N'2', 15000, 1, 1)
GO