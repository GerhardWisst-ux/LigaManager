USE [LigaManager]
--IF OBJECT_ID(N'dbo.VereineSaison', N'U') IS NULL

CREATE TABLE [dbo].[Vereine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
 CONSTRAINT [PK_Vereine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (2, 1, N'Bayern München', N'Bayern München', N'ALLIANZ-AERNA', N'66000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (59, 2, N'Borussia Dortmund', N'Borussia Dortmund', N'Signal Iduna Park', N'80000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (60, 3, N'RB Leipzig', N'RB Leipzig', N'Red-Bull-Arena', N'46000', N'2022,2023', 2009)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (62, 4, N'Union Berlin', N'Union Berlin', N'STADION AN DER ALTEN FÖRSTEREI', N'22500', N'', 1924)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (64, 5, N'SC Freiburg', N'SC Freiburg', N'Badenova-Stadion', N'34500', N'', 1931)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (65, 6, N'Bayer Leverkusen', N'Bayer Leverkusen', N'BAY-ARENA', N'29100', N'1988', 1948)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (66, 7, N'Eintracht Frankfurt', N'Eintracht Frankfurt', N'DEUTSCHE-BANK-PARK', N'55000', N'1960', 1921)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (67, 8, N'VFL Wolfsburg', N'VFL Wolfsburg', N'VOLKSWAGEN-ARENA', N'29000', N'DEUTSCHER MEISTER 2009, POKALSIEGER 2015', 1944)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (68, 9, N'FSV Mainz 05', N'FSV Mainz 05', N'MEWA-ARENA', N'34000', N'', 1905)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (69, 10, N'Bor. Mönchengladbach', N'Bor. Mönchengladbach', N'BORUSSIA-PARK', N'50000', N'1977, 1976', 1910)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (70, 11, N'FC Köln', N'FC Köln', N'Rhein-Energie Station', N'50000', N'1978', 1948)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (71, 12, N'TSG Hoffenheim', N'TSG Hoffenheim', N'PREZERO-ARENA', N'30000', N'', 2001)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (72, 13, N'Werder Bremen', N'Werder Bremen', N'Wohninvest Weserstadion', N'44000', N'2004', 1912)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (73, 14, N'VFL Bochum', N'VFL Bochum', N'VONOVIA-RUHRSTADION', N'29000', N'4X DEUTSCHER ZWEITLIGAMEISTER, 1X WESTDEUTSCHER POKALSIEGER', 1948)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (74, 15, N'FC Augsburg', N'FC Augsburg', N'WWK-ARENA', N'30000', N'', 1958)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (75, 16, N'VfB Stuttgart', N'VfB Stuttgart', N'MHP-ARENA', N'60000', N'5X DEUTSCHER MEISTER, 3X DEUTSCHER POKALSIEGER, 2X ZWEITLIGAMEISTER, 1X DEUTSCHER SUPERPOKALSIEGER 1992', 1893)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (76, 17, N'SV Darmstadt 98', N'SV Darmstadt 98', N'MERCK-STADION AM BÖLLENFALLTOR', N'17500', N'', 1898)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (77, 18, N'FC Heidenheim', N'FC Heidenheim', N'VOITH-ARENA', N'15000', N'', 1924)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (78, 19, N'FC Schalke 04', N'FC Schalke 04', N'VELTINS-ARENA', N'62000', N'7X DEUTSCHER MEISTER', 1904)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (79, 20, N'Hertha BSC', N'Hertha BSC', N'OLYMPIASTADION(Berlin)', N'77000', N'2X DEUTSCHER MEISTER 1930,1931', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (82, 21, N'Greuther Fürth', N'Greuther Fürth', N'SPORTPARK RONHOF I THOMAS SOMMER', N'18000', N'', 1905)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (83, 42, N'Arminia Bielefeld', N'Arminia Bielefeld', N'SCHÜCO-ARENA', N'27332', N'', 1905)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (84, 22, N'SC Paderborn 07', N'SC Paderborn 07', N'HOME-DELUXE-ARENA', N'15000', N'', 1907)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (86, 23, N'Hannover 96', N'Hannover 96', N'Niedersachsenstadion', N'49000', N'DEUTSCHER MEISTER 1953', 1897)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (88, 24, N'FC Kaiserslautern', N'FC Kaiserslautern', N'FRITZ-WALTER-STADION', N'50000', N'DEUTSCHER MEISTER 1951, 1991, 1998', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (89, 25, N'FC Nürnberg', N'FC Nürnberg', N'Max-Morlock Stadion', N'50000', N'', 1895)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (93, 26, N'Hamburger SV', N'Hamburger SV', N'VOLKSPARKSTADION', N'60000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (94, 27, N'Fortuna Düsseldorf', N'Fortuna Düsseldorf', N'MERKUR SPIEL-ARENA', N'45000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (96, 28, N'FC Ingolstadt 04', N'FC Ingolstadt 04', N'AUDI-SPORTPARK', N'18000', N'', 1904)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (125, 29, N'Karlsruher SC', N'Karlsruher SC', N'WILDPARKSTADION', N'26000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (126, 30, N'Eintracht Braunschweig', N'Eintracht Braunschweig', N'EINTRACHT-STADION', N'22000', N'DEUTSCHER MEISTER 1967', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (127, 31, N'1860 München', N'1860 München', N'OLYMPIASTADION(München)', N'77000', N'DEUTSCHER MEISTER 1966', 1860)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (129, 32, N'KFC Uerdingen 05', N'KFC Uerdingen 05', N'GROTENBURG-Kampfbahn', N'24000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (130, 33, N'SV Waldhof Mannheim', N'SV Waldhof Mannheim', N'CARL-BENZ STADION', N'27000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (131, 34, N'Kickers Offenbach', N'Kickers Offenbach', N'BIEBERER BERG', N'20000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (132, 35, N'Rot-Weiss Essen	', N'Rot-Weiss Essen	', N'GEORG-MELCHES-STADION', N'18500', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (133, 36, N'FC St. Pauli', N'FC St. Pauli', N'MILLERNTOR', N'29000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (139, 37, N'Energie Cottbus', N'Energie Cottbus', N'STADION DER FREUNDSCHAFT', N'18000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (140, 38, N'Alemannia Aachen', N'Alemannia Aachen', N'NEUER TIVOLI', N'32000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (142, 39, N'1. FC Saarbrücken', N'1. FC Saarbrücken', N'LUDWIGSPARKSTADION', N'20000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (143, 40, N'Dynamo Dresden', N'Dynamo Dresden', N'RUDOLF-HARBIG-STADION', N'29000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (144, 41, N'RW Oberhausen', N'RW Oberhausen', N'NIEDERRHEIN-STADION', N'15000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (147, 43, N'FC Hansa Rostock', N'FC Hansa Rostock', N'OSTSEESTADION', N'26000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (149, 44, N'MSV Duisburg', N'MSV Duisburg', N'WEDAUSTADION', N'30000', N'', 1907)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (151, 45, N'SpVgg Unterhaching', N'SpVgg Unterhaching', N'SPORTPARK', N'15000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (153, 46, N'SSV Ulm 1846', N'SSV Ulm 1846', N'DONAUSTADION', N'20000', N'', 1846)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (154, 47, N'SG Wattenscheid 09', N'SG Wattenscheid 09', N'LOHRHEIDESTADION', N'20000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (156, 48, N'VfB Leipzig', N'VfB Leipzig', N'ZENTRALSTADION', N'44500', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (157, 49, N'Borussia Neunkirchen', N'Borussia Neunkirchen', N'ELLENFELD', N'20000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (159, 50, N'SC Tasmania 1900 Berlin', N'SC Tasmania 1900 Berlin', N'POSTSTADION BERLIN', N'20000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (161, 51, N'Preußen Münster', N'Preußen Münster', N'PREUSSSENSTADION', N'25000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (163, 52, N'SpVgg Blau-Weiß 90 Berlin', N'SpVgg Blau-Weiß 90 Berlin', N'POSTSTADION BERLIN', N'25000', N'', 1890)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (166, 53, N'Tennis Borussia Berlin', N'Tennis Borussia Berlin', N'MOMMSENSTADION', N'11500', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (167, 54, N'Fortuna Köln', N'Fortuna Köln', N'SÜDSTADION', N'15000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (168, 55, N'Wuppertaler SV', N'Wuppertaler SV', N'STADION AM ZOO', N'28000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (169, 56, N'FC 08 Homburg', N'FC 08 Homburg', N'WALDSTADION', N'20000', N'', 1900)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet]) VALUES (172, 57, N'Stuttgarter Kickers', N'Stuttgarter Kickers', N'Stadion auf der Waldau', N'20000', N'', 1900)