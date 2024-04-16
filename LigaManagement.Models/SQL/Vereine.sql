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
	[Pokal] [bit] NOT NULL,
	[Bundesliga] [bit] NULL,
 CONSTRAINT [PK_Vereine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Vereine] ON 
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (2, 1, N'Bayern München', N'Bayern München', N'ALLIANZ-AERNA', N'66000', N'3X CHAMPIONS LEAGUE-SIEGER(19/20 ,  12/13 ,  00/01), 3X EUROPAPOKAL-DER-LANDESMEISTER-SIEGER(75/76 ,  74/75 ,  73/74), 20X DEUTSCHER POKALSIEGER(19/20 ,  18/19 ,  15/16 ,  13/14 ,  12/13 ,  09/10 ,  07/08 ,  05/06 ,  04/05 ,  02/03 ,  99/00 ,  97/98 ,  85/86 ,  83/84 ,  81/82 ,  70/71 ,  68/69 ,  66/67 ,  65/66 ,  56/57), 33X DEUTSCHER MEISTER(22/23 ,  21/22 ,  20/21 ,  19/20 ,  18/19 ,  17/18 ,  16/17 ,  15/16 ,  14/15 ,  13/14 ,  12/13 ,  09/10 ,  07/08 ,  05/06 ,  04/05 ,  02/03 ,  00/01 ,  99/00 ,  98/99 ,  96/97 ,  93/94 ,  89/90 ,  88/89 ,  86/87 ,  85/86 ,  84/85 ,  80/81 ,  79/80 ,  73/74 ,  72/73 ,  71/72 ,  68/69 ,  31/32)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (59, 2, N'Borussia Dortmund', N'Borussia Dortmund', N'Signal Iduna Park', N'80000', N'1X CHAMPIONS LEAGUE-SIEGER(96/97), 8X DEUTSCHER MEISTER(11/12 ,  10/11 ,  01/02 ,  95/96 ,  94/95 ,  62/63 ,  56/57 ,  55/56), 1X EUROPAPOKAL-DER-POKALSIEGER-SIEGER(65/66), 5X DEUTSCHER POKALSIEGER(20/21 ,  16/17 ,  11/12 ,  88/89 ,  64/65), 6X DEUTSCHER SUPERPOKALSIEGER(19/20 ,  14/15 ,  13/14 ,  96/97 ,  95/96 ,  89/90), 1X WELTPOKALSIEGER(97/98)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (60, 3, N'RB Leipzig', N'RB Leipzig', N'Red-Bull-Arena', N'46000', N'2X DEUTSCHER POKALSIEGER(22/23 , 21/22),1X DEUTSCHER SUPERPOKALSIEGER(23/24)', 2009, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (62, 4, N'Union Berlin', N'Union Berlin', N'STADION AN DER ALTEN FÖRSTEREI', N'22500', N'1X INTERTOTO-CUP-SIEGER(86/87), 1X DEUTSCHER DRITTLIGAMEISTER(08/09)', 1924, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (64, 5, N'SC Freiburg', N'SC Freiburg', N'EUROPAPARK-Stadion', N'34500', N'4X DEUTSCHER ZWEITLIGAMEISTER(15/16 ,  08/09 ,  02/03 ,  92/93)', 1931, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (65, 6, N'Bayer Leverkusen', N'Bayer Leverkusen', N'BAY-ARENA', N'29100', N'1X DEUTSCHER MEISTER (23/24), 1X UEFA-CUP-SIEGER(87/88), 1X DEUTSCHER POKALSIEGER(92/93), 1X DEUTSCHER ZWEITLIGAMEISTER(78/79)', 1948, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (66, 7, N'Eintracht Frankfurt', N'Eintracht Frankfurt', N'DEUTSCHE-BANK-PARK', N'55000', N'1X DEUTSCHER MEISTER(58/59), 1X EUROPA-LEAGUE-SIEGER(21/22),1X UEFA-CUP-SIEGER(79/80), 5X DEUTSCHER POKALSIEGER(17/18 ,  87/88 ,  80/81 ,  74/75 ,  73/74)', 1921, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (67, 8, N'VFL Wolfsburg', N'VFL Wolfsburg', N'VOLKSWAGEN-ARENA', N'29000', N'1X DEUTSCHER MEISTER (08/09), 1X DEUTSCHER POKALSIEGER(14/15),1X DEUTSCHER SUPERPOKALSIEGER(15/16)', 1944, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (68, 9, N'FSV Mainz 05', N'FSV Mainz 05', N'MEWA-ARENA', N'34000', N'1X DEUTSCHER AMATEURMEISTER(81/82),1X MEISTER REGIONALLIGA SÜDWEST (GER)(72/73)', 1905, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (69, 10, N'Bor. Mönchengladbach', N'Bor. Mönchengladbach', N'BORUSSIA-PARK', N'50000', N'5X DEUTSCHER MEISTER(76/77 ,  75/76 ,  74/75 ,  70/71 ,  69/70), 2X UEFA-CUP-SIEGER(78/79, 74/75), 3X DEUTSCHER POKALSIEGER(94/95 ,  72/73 ,  59/60), 1X DEUTSCHER ZWEITLIGAMEISTER(07/08)', 1910, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (70, 11, N'FC Köln', N'FC Köln', N'Rhein-Energie Station', N'50000', N'3X DEUTSCHER MEISTER(77/78 ,  63/64 ,  61/62),4X DEUTSCHER POKALSIEGER(82/83 ,  77/78 ,  76/77 ,  67/68), 4X DEUTSCHER ZWEITLIGAMEISTER(18/19 ,  13/14 ,  04/05 ,  99/00)', 1948, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (71, 12, N'TSG Hoffenheim', N'TSG Hoffenheim', N'PREZERO-ARENA', N'30000', N'4X LANDESPOKAL-BADEN-SIEGER(04/05 ,  03/04 ,  02/03 ,  01/02)', 2001, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (72, 13, N'Werder Bremen', N'Werder Bremen', N'Wohninvest Weserstadion', N'44000', N'4X DEUTSCHER MEISTER(03/04 ,  92/93 ,  87/88 ,  64/65), 1X EUROPAPOKAL-DER-POKALSIEGER-SIEGER(91/92), 6X DEUTSCHER POKALSIEGER(08/09 ,  03/04 ,  98/99 ,  93/94 ,  90/91 ,  60/61), 3X DEUTSCHER SUPERPOKALSIEGER(94/95 ,  93/94 ,  88/89), 1X DEUTSCHER ZWEITLIGAMEISTER(80/81)', 1912, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (73, 14, N'VFL Bochum', N'VFL Bochum', N'VONOVIA-RUHRSTADION', N'29000', N'12/13 ,  10/11 ,  89/90, 1X WESTDEUTSCHER POKALSIEGER', 1948, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (74, 15, N'FC Augsburg', N'FC Augsburg', N'WWK-ARENA', N'30000', N'1X LANDESPOKAL-BAYERN-SIEGER(50/51)', 1958, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (75, 16, N'VfB Stuttgart', N'VfB Stuttgart', N'MHP-ARENA', N'60000', N'5X DEUTSCHER MEISTER(06/07 ,  91/92 ,  83/84 ,  51/52 ,  49/50), 3X DEUTSCHER POKALSIEGER(96/97 ,  57/58 ,  53/54),  1X DEUTSCHER SUPERPOKALSIEGER(92/93), 2X DEUTSCHER ZWEITLIGAMEISTER(16/17 ,  76/77)', 1893, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (76, 17, N'SV Darmstadt 98', N'SV Darmstadt 98', N'MERCK-STADION AM BÖLLENFALLTOR', N'17500', N'2X DEUTSCHER ZWEITLIGAMEISTER(80/81 ,  77/78), 1X MEISTER REGIONALLIGA SÜDWEST (GER)(10/11)', 1898, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (77, 18, N'FC Heidenheim', N'FC Heidenheim', N'VOITH-ARENA', N'15000', N'1X DEUTSCHER ZWEITLIGAMEISTER(22/23), 1X DEUTSCHER DRITTLIGAMEISTER(13/14), 6X LANDESPOKAL-WÜRTTEMBERG-SIEGER(13/14 ,  12/13 ,  11/12 ,  10/11 ,  07/08 ,  64/65)', 1924, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (78, 19, N'FC Schalke 04', N'FC Schalke 04', N'VELTINS-ARENA', N'62000', N'7X DEUTSCHER MEISTER(57/58 ,  41/42 ,  39/40 ,  38/39 ,  36/37 ,  34/35 ,  33/34), 1X UEFA-CUP-SIEGER(96/97), 5X DEUTSCHER POKALSIEGER(10/11 ,  01/02 ,  00/01 ,  71/72 ,  1937), 1X DEUTSCHER SUPERPOKALSIEGER(11/12), 3X DEUTSCHER ZWEITLIGAMEISTER(21/22 ,  90/91 ,  81/82)', 1904, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (79, 20, N'Hertha BSC', N'Hertha BSC', N'OLYMPIASTADION(Berlin)', N'77000', N'2X DEUTSCHER MEISTER (30/31 , 1929/30), 2X DEUTSCHER POKALSIEGER(02/03, 01/02), 3X DEUTSCHER ZWEITLIGAMEISTER(12/13 ,  10/11 ,  89/90), 1X EUROPAPOKAL-DER-POKALSIEGER-SIEGER(76/77), 3X DEUTSCHER POKALSIEGER(86/87 ,  75/76 ,  62/63)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (82, 21, N'Greuther Fürth', N'Greuther Fürth', N'SPORTPARK RONHOF I THOMAS SOMMER', N'18000', N'3X DEUTSCHER MEISTER(1928/29 ,  1925/26 ,  1913/14), 1X DEUTSCHER ZWEITLIGAMEISTER(11/12)', 1905, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (83, 42, N'Arminia Bielefeld', N'Arminia Bielefeld', N'SCHÜCO-ARENA', N'27332', N'4X DEUTSCHER ZWEITLIGAMEISTER(19/20 ,  98/99 ,  79/80 ,  77/78), 1X DEUTSCHER DRITTLIGAMEISTER(14/15), 2X WESTDEUTSCHER POKALSIEGER(73/74 ,  65/66)', 1905, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (84, 22, N'SC Paderborn 07', N'SC Paderborn 07', N'HOME-DELUXE-ARENA', N'15000', N'9X LANDESPOKAL-WESTFALEN-SIEGER(17/18 ,  16/17 ,  03/04 ,  01/02 ,  00/01 ,  99/00 ,  95/96 ,  93/94 ,  84/85)', 1907, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (86, 23, N'Hannover 96', N'Hannover 96', N'Niedersachsenstadion', N'49000', N'DEUTSCHER MEISTER 1953', 1897, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (88, 24, N'FC Kaiserslautern', N'FC Kaiserslautern', N'FRITZ-WALTER-STADION', N'50000', N'DEUTSCHER MEISTER 1951, 1991, 1998', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (89, 25, N'FC Nürnberg', N'FC Nürnberg', N'Max-Morlock Stadion', N'50000', N'9X DEUTSCHER MEISTER(67/68 ,  60/61 ,  47/48 ,  35/36 ,  1926/27 ,  1924/25 ,  1923/24 ,  1920/21 ,  1919/20), 4X DEUTSCHER POKALSIEGER(06/07 ,  61/62 ,  1939 ,  1935), 1X INTERTOTO-CUP-SIEGER(68/69), 4X DEUTSCHER ZWEITLIGAMEISTER(03/04 ,  00/01 ,  84/85 ,  79/80)', 1895, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (93, 26, N'Hamburger SV', N'Hamburger SV', N'VOLKSPARKSTADION', N'60000', N'1X EUROPAPOKAL-DER-LANDESMEISTER(82/83), 6X DEUTSCHER MEISTER(82/83 ,  81/82 ,  78/79 ,  59/60 ,  1927/28 ,  1922/23), 2X DEUTSCHER ZWEITLIGAMEISTER()', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (94, 27, N'Fortuna Düsseldorf', N'Fortuna Düsseldorf', N'MERKUR SPIEL-ARENA', N'45000', N'1X DEUTSCHER MEISTER(32/33), 2X DEUTSCHER POKALSIEGER(79/80 ,  78/79), 3X INTERTOTO-CUP-SIEGER(85/86 ,  83/84 ,  66/67), 17/18 ,  88/89', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (96, 28, N'FC Ingolstadt 04', N'FC Ingolstadt 04', N'AUDI-SPORTPARK', N'18000', N'1X DEUTSCHER ZWEITLIGAMEISTER(14/15)', 1904, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (125, 29, N'Karlsruher SC', N'Karlsruher SC', N'WILDPARKSTADION', N'26000', N'1X DEUTSCHER MEISTER(1908/09), 2X DEUTSCHER POKALSIEGER(55/56 ,  54/55), 3X DEUTSCHER ZWEITLIGAMEISTER(06/07 ,  83/84 ,  74/75), 1X DEUTSCHER DRITTLIGAMEISTER(12/13)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (126, 30, N'Eintracht Braunschweig', N'Eintracht Braunschweig', N'EINTRACHT-STADION', N'22000', N'DEUTSCHER MEISTER 1967', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (127, 31, N'1860 München', N'1860 München', N'OLYMPIASTADION(München)', N'77000', N'1X DEUTSCHER MEISTER(65/66), 2X DEUTSCHER POKALSIEGER(63/64 ,  1942), 1X DEUTSCHER ZWEITLIGAMEISTER(78/79)', 1860, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (129, 32, N'KFC Uerdingen 05', N'Bayer 05 Uerdingen', N'GROTENBURG-Kampfbahn', N'24000', N'1X DEUTSCHER POKALSIEGER(84/85), 4X INTERTOTO-CUP-SIEGER(92/93 ,  91/92 ,  90/91 ,  88/89), 1X DEUTSCHER ZWEITLIGAMEISTER(91/92)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (130, 33, N'SV Waldhof Mannheim', N'SV Waldhof Mannheim', N'CARL-BENZ STADION', N'27000', N'1X DEUTSCHER ZWEITLIGAMEISTER(82/83), 2X MEISTER REGIONALLIGA SÜDWEST (GER)(18/19 , 15/16)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (131, 34, N'Kickers Offenbach', N'Kickers Offenbach', N'BIEBERER BERG', N'20000', N'1X DEUTSCHER POKALSIEGER(69/70), 1X MEISTER REGIONALLIGA SÜDWEST (GER)(14/15)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (132, 35, N'Rot-Weiss Essen	', N'Rot-Weiss Essen	', N'GEORG-MELCHES-STADION', N'18500', N'1X DEUTSCHER MEISTER(54/55),1X DEUTSCHER POKALSIEGER(52/53), 1X DEUTSCHER AMATEURMEISTER(91/92), 1X MEISTER REGIONALLIGA WEST (GER)(21/22)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (133, 36, N'FC St. Pauli', N'FC St. Pauli', N'MILLERNTOR', N'29000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (139, 37, N'Energie Cottbus', N'Energie Cottbus', N'STADION DER FREUNDSCHAFT', N'18000', N'2X MEISTER REGIONALLIGA NORDOST(22/23 ,  17/18)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (140, 38, N'Alemannia Aachen', N'Alemannia Aachen', N'NEUER TIVOLI', N'32000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (142, 39, N'1. FC Saarbrücken', N'1. FC Saarbrücken', N'LUDWIGSPARKSTADION', N'20000', N'1X INTERTOTO-CUP-SIEGER(78/79), 2X DEUTSCHER ZWEITLIGAMEISTER(91/92, 1975/76), 2X MEISTER REGIONALLIGA SÜDWEST(19/20, 17/18)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (143, 40, N'Dynamo Dresden', N'Dynamo Dresden', N'RUDOLF-HARBIG-STADION', N'29000', N'2X DEUTSCHER DRITTLIGAMEISTER(20/21 ,  15/16), 8X DDR-MEISTER(89/90 ,  88/89 ,  77/78 ,  76/77 ,  75/76 ,  72/73 ,  70/71 ,  52/53), 7X DDR-POKALSIEGER(89/90 ,  84/85 ,  83/84 ,  81/82 ,  76/77 ,  70/71 ,  51/52)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (144, 41, N'RW Oberhausen', N'RW Oberhausen', N'NIEDERRHEIN-STADION', N'15000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (147, 43, N'FC Hansa Rostock', N'FC Hansa Rostock', N'OSTSEESTADION', N'26000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (149, 44, N'MSV Duisburg', N'MSV Duisburg', N'WEDAUSTADION', N'30000', N'3X INTERTOTO-CUP-SIEGER(78/79 ,  77/78 ,  74/75), 1X DEUTSCHER DRITTLIGAMEISTER(16/17), 1X DEUTSCHER AMATEURMEISTER(86/87)', 1907, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (151, 45, N'SpVgg Unterhaching', N'SpVgg Unterhaching', N'SPORTPARK', N'15000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (153, 46, N'SSV Ulm 1846', N'SSV Ulm 1846', N'DONAUSTADION', N'20000', N'1X DEUTSCHER AMATEURMEISTER(95/96), 1X MEISTER REGIONALLIGA SÜDWEST (GER)(22/23), 11X LANDESPOKAL-WÜRTTEMBERG-SIEGER(20/21 ,  19/20 ,  18/19 ,  17/18 ,  96/97 ,  94/95 ,  93/94 ,  91/92 ,  82/83 ,  81/82 ,  56/57)', 1846, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (154, 47, N'SG Wattenscheid 09', N'SG Wattenscheid 09', N'LOHRHEIDESTADION', N'20000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (156, 48, N'VfB Leipzig', N'VfB Leipzig', N'ZENTRALSTADION', N'44500', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (157, 49, N'Borussia Neunkirchen', N'Borussia Neunkirchen', N'ELLENFELD', N'20000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (159, 50, N'SC Tasmania 1900 Berlin', N'SC Tasmania 1900 Berlin', N'POSTSTADION BERLIN', N'20000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (161, 51, N'Preußen Münster', N'Preußen Münster', N'PREUSSSENSTADION', N'25000', N'1X DEUTSCHER AMATEURMEISTER(93/94), 2X MEISTER REGIONALLIGA WEST (GER)(22/23 ,  10/11)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (163, 52, N'SpVgg Blau-Weiß 90 Berlin', N'SpVgg Blau-Weiß 90 Berlin', N'POSTSTADION BERLIN', N'25000', N'', 1890, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (166, 53, N'Tennis Borussia Berlin', N'Tennis Borussia Berlin', N'MOMMSENSTADION', N'11500', N'1X DEUTSCHER ZWEITLIGAMEISTER(75/76), 1X DEUTSCHER AMATEURMEISTER(97/98)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (167, 54, N'Fortuna Köln', N'Fortuna Köln', N'SÜDSTADION', N'15000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (168, 55, N'Wuppertaler SV', N'Wuppertaler SV', N'STADION AM ZOO', N'28000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (169, 56, N'FC 08 Homburg', N'FC 08 Homburg', N'WALDSTADION', N'20000', N'', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (172, 57, N'Stuttgarter Kickers', N'Stuttgarter Kickers', N'Stadion auf der Waldau', N'15000', N'1X DEUTSCHER ZWEITLIGAMEISTER(87/88),3X LANDESPOKAL-WÜRTTEMBERG-SIEGER(21/22 ,  05/06 ,  04/05)', 1900, 1, 1)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (173, 58, N'1. FC Magdeburg', N'1. FC Magdeburg', N'MDCC-Arena', N'20000', N'', 1900, 1, 0)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (175, 59, N'Holstein Kiel', N'Holstein Kiel', N'Holstein-Stadion', N'15000', N'1X DEUTSCHER MEISTER (1911/12)', 1900, 1, 0)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (176, 60, N'FC Viktoria Köln', N'FC Viktoria Köln', N'Sportpark Höhenberg', N'10000', N'', 1900, 1, 0)
GO
INSERT [dbo].[Vereine] ([Id], [VereinNr], [Vereinsname1], [Vereinsname2], [Stadion], [Fassungsvermoegen], [Erfolge], [Gegruendet], [Pokal], [Bundesliga]) VALUES (177, 61, N'SV Sandhausen', N'SV Sandhausen', N'Hardtwaldstadion', N'10000', N'', 1900, 1, 0)
GO