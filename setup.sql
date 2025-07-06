CREATE DATABASE LigaDB;
GO
USE [LigaDB]
GO
/****** Object:  Table [dbo].[champions-league-2024-UTC]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[champions-league-2024-UTC](
	[Match_Number] [tinyint] NOT NULL,
	[Runde] [nvarchar](50) NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](50) NOT NULL,
	[Verein1] [nvarchar](50) NOT NULL,
	[Verein2] [nvarchar](50) NOT NULL,
	[Result] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHARTDATA]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHARTDATA](
	[ChartDataId] [int] IDENTITY(1,1) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Spiele] [int] NOT NULL,
	[Punkte] [int] NOT NULL,
	[LigaID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChartDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Einstellungen]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Einstellungen](
	[ID] [int] NULL,
	[Sprache_LandKZ] [nchar](2) NULL,
	[ImportVisible] [bit] NULL,
	[TabellenAnlegenVisible] [bit] NULL,
	[Spielverlauf] [bit] NULL,
	[Aufstellungen] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InfoTexte]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoTexte](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[NewsContent] [nvarchar](max) NULL,
	[VereinID] [int] NULL,
	[SaisonID] [int] NULL,
	[LigaID] [int] NULL,
	[PublishedAt] [datetime] NULL,
	[ChangedAt] [datetime] NULL,
 CONSTRAINT [PK_InfoTexte2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kader]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SpielerName] [nvarchar](100) NOT NULL,
	[Vorname] [nvarchar](100) NOT NULL,
	[Geburtstag] [date] NOT NULL,
	[Groesse] [decimal](18, 0) NULL,
	[Gewicht] [decimal](18, 0) NULL,
	[Laenderspiele] [int] NULL,
	[LaenderspieleTore] [int] NULL,
	[VereinNr] [int] NOT NULL,
	[LandID] [int] NOT NULL,
	[SaisonID] [int] NULL,
	[LigaID] [int] NULL,
	[Rueckennummer] [int] NOT NULL,
	[Einsaetze] [int] NOT NULL,
	[Spielminuten] [int] NULL,
	[Tore] [int] NULL,
	[Abloesesumme] [decimal](18, 0) NULL,
	[Image] [image] NULL,
	[ImVereinSeit] [date] NULL,
	[Aktiv] [bit] NULL,
	[Position] [nchar](50) NULL,
	[PositionsNr] [int] NULL,
 CONSTRAINT [PK_Kader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KaderVerein]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KaderVerein](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Verein] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Alter] [float] NULL,
	[Nationalität] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NOT NULL,
	[Short_Pos] [nvarchar](max) NULL,
	[Dealing Club] [nvarchar](max) NULL,
	[Dealing_country] [nchar](10) NULL,
	[Ablöse] [float] NULL,
	[Movement] [nvarchar](max) NULL,
	[Transferfesnster] [nvarchar](max) NULL,
	[LigaID] [int] NULL,
	[SaisonID] [int] NULL,
	[Leihe] [bit] NULL,
	[Leih-Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_KaderVerein] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laender]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](10) NULL,
	[Laendername] [nchar](100) NULL,
	[Aktiv] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LetzteErgebnisse]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LetzteErgebnisse](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [nvarchar](max) NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [nvarchar](max) NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NULL,
	[Zuschauer] [int] NULL,
	[TeamIconUrl1] [nvarchar](max) NULL,
	[TeamIconUrl2] [nvarchar](max) NULL,
	[Anlagedatum] [datetime2](7) NULL,
	[Liga] [nvarchar](max) NULL,
 CONSTRAINT [PK_LetzteErgebnisse] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ligen]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ligen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Liganummer] [int] NULL,
	[Liganame] [nvarchar](max) NOT NULL,
	[Verband] [nvarchar](max) NOT NULL,
	[Erstaustragung] [datetime2](7) NOT NULL,
	[Aktiv] [nvarchar](max) NULL,
	[LandID] [int] NULL,
	[AusrichterLand] [nvarchar](max) NULL,
	[Saisonen] [int] NULL,
	[Rekordspieler] [nvarchar](max) NULL,
	[Spiele_Rekordspieler] [int] NULL,
	[EMWM] [bit] NULL,
 CONSTRAINT [PK_Ligen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MannschaftEMWM]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MannschaftEMWM](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MannschaftNr] [int] NOT NULL,
	[MannschaftName1] [nvarchar](max) NOT NULL,
	[MannschaftName2] [nvarchar](max) NOT NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[LandID] [int] NULL,
	[GroupID1954] [int] NULL,
	[GroupID1950] [int] NULL,
	[GroupID1938] [int] NULL,
	[GroupID1982] [int] NULL,
	[GroupID1974] [int] NULL,
	[GroupID1970] [int] NULL,
	[GroupID1966] [int] NULL,
	[GroupID1962] [int] NULL,
	[GroupID1958] [int] NULL,
	[GroupID2024] [int] NULL,
	[GroupID2022] [int] NULL,
	[GroupID2020] [int] NULL,
	[GroupID2018] [int] NULL,
	[GroupID2016] [int] NULL,
	[GroupID2014] [int] NULL,
	[GroupID2012] [int] NULL,
	[GroupID2010] [int] NULL,
	[GroupID2008] [int] NULL,
	[GroupID2006] [int] NULL,
	[GroupID2004] [int] NULL,
	[GroupID2002] [int] NULL,
	[GroupID2000] [int] NULL,
	[GroupID1998] [int] NULL,
	[GroupID1996] [int] NULL,
	[GroupID1994] [int] NULL,
	[GroupID1992] [int] NULL,
	[GroupID1990] [int] NULL,
	[GroupID1988] [int] NULL,
	[GroupID1986] [int] NULL,
	[GroupID1984] [int] NULL,
	[GroupID1980] [int] NULL,
	[GroupID1978] [int] NULL,
	[GroupID1934] [int] NULL,
	[GroupID1930] [int] NULL,
	[GroupID1978_2] [int] NULL,
	[GroupID1974_2] [int] NULL,
 CONSTRAINT [PK_EMWM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pokalergebnisse]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  Table [dbo].[Roles]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Saisonen]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Saisonen](
	[SaisonID] [int] IDENTITY(1,1) NOT NULL,
	[Saisonname] [nvarchar](max) NOT NULL,
	[LigaID] [int] NOT NULL,
	[Liganame] [nvarchar](max) NOT NULL,
	[Aktuell] [bit] NOT NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[LandID] [int] NULL,
	[Ligahoehe] [int] NULL,
	[AnzahlVereine] [int] NULL,
	[AnzahlAbsteiger] [int] NULL,
	[AnzahlAufsteiger] [int] NULL,
	[AnzahlCL_Plaetze] [int] NULL,
	[AnzahlEL_Plaetze] [int] NULL,
	[AnzahlCF_Plaetze] [int] NULL,
	[Anzahl_Relegation] [int] NULL,
	[SpielplanVorhanden] [bit] NULL,
 CONSTRAINT [PK_Saisonen] PRIMARY KEY CLUSTERED 
(
	[SaisonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaisonenCL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaisonenCL](
	[SaisonID] [int] IDENTITY(1,1) NOT NULL,
	[Saisonname] [nvarchar](max) NOT NULL,
	[LigaID] [int] NOT NULL,
	[Liganame] [nvarchar](max) NOT NULL,
	[Aktuell] [bit] NOT NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[EMWM] [bit] NULL,
 CONSTRAINT [PK_SaisonenCL] PRIMARY KEY CLUSTERED 
(
	[SaisonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spieler]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spieler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Rueckennummer] [int] NOT NULL,
	[Einsaetze] [int] NOT NULL,
	[Spielminuten] [int] NOT NULL,
	[Tore] [int] NOT NULL,
	[Abloesesumme] [decimal](18, 0) NOT NULL,
	[Image] [image] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpielerSpieltag]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpielerSpieltag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KaderID] [int] NOT NULL,
	[SaisonID] [int] NOT NULL,
	[SpieltagNr] [int] NOT NULL,
	[Spielminuten] [int] NOT NULL,
	[Einsatz] [int] NOT NULL,
	[Tore] [int] NOT NULL,
	[Eingewechselt] [bit] NULL,
	[EingewechseltMin] [int] NULL,
	[Ausgewechselt] [bit] NULL,
	[AusgewechseltMin] [int] NOT NULL,
	[GelbeKarten] [bit] NULL,
	[RoteKarten] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpielerVerein]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpielerVerein](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SpielerID] [int] NOT NULL,
	[VereinNr] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spielplaene]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spielplaene](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NULL,
	[SaisonID] [int] NULL,
	[LigaID] [int] NULL,
	[Verein1_Nr] [nvarchar](max) NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [nvarchar](max) NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[DatumString] [nvarchar](max) NULL,
	[Ort] [nvarchar](max) NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NULL,
	[Zuschauer] [int] NULL,
	[TeamIconUrl1] [nvarchar](max) NULL,
	[TeamIconUrl2] [nvarchar](max) NULL,
	[StadionID] [int] NULL,
	[Datum] [datetime] NULL,
 CONSTRAINT [PK_Spielplaene] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spielplan 2021]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spielplan 2021](
	[Datum] [nvarchar](50) NOT NULL,
	[ST] [tinyint] NOT NULL,
	[Verein1] [nvarchar](50) NOT NULL,
	[Tore1] [nvarchar](1) NULL,
	[Verein_2] [nvarchar](50) NOT NULL,
	[Tore_2] [nvarchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spielplan 2022]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spielplan 2022](
	[Datum] [nvarchar](50) NOT NULL,
	[ST] [tinyint] NOT NULL,
	[Verein1] [nvarchar](50) NOT NULL,
	[Tore1] [nvarchar](1) NULL,
	[Verein_2] [nvarchar](50) NOT NULL,
	[Tore_2] [nvarchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spielplan 2023]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spielplan 2023](
	[Datum] [nvarchar](50) NOT NULL,
	[ST] [tinyint] NOT NULL,
	[Verein1] [nvarchar](50) NOT NULL,
	[Tore1] [tinyint] NOT NULL,
	[Verein_2] [nvarchar](50) NOT NULL,
	[Tore_2] [nvarchar](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spielplan 2024]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spielplan 2024](
	[Datum] [nvarchar](50) NOT NULL,
	[ST] [tinyint] NOT NULL,
	[Verein1] [nvarchar](50) NOT NULL,
	[Tore1] [tinyint] NOT NULL,
	[Verein_2] [nvarchar](50) NOT NULL,
	[Tore_2] [tinyint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spielplan 2025]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spielplan 2025](
	[Datum] [nvarchar](255) NULL,
	[Spieltag] [int] NULL,
	[Verein1] [nvarchar](255) NULL,
	[Verein2] [nvarchar](255) NULL,
	[Verein1Nr] [int] NULL,
	[Verein2Nr] [int] NULL,
	[Saison] [nchar](10) NULL,
	[LigaID] [int] NULL,
	[Schiedrichter] [nchar](10) NULL,
	[Abgeschlossen] [bit] NULL,
	[Zuschauer] [int] NULL,
	[Tore1] [int] NULL,
	[Tore2] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spielplan 2025-2]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spielplan 2025-2](
	[Datum] [nvarchar](255) NULL,
	[Spieltag] [int] NULL,
	[Verein1] [nvarchar](255) NULL,
	[Verein2] [nvarchar](255) NULL,
	[Verein1Nr] [int] NULL,
	[Verein2Nr] [int] NULL,
	[Saison] [nchar](10) NULL,
	[LigaID] [int] NULL,
	[Schiedrichter] [nchar](10) NULL,
	[Abgeschlossen] [bit] NULL,
	[Zuschauer] [int] NULL,
	[Tore1] [int] NULL,
	[Tore2] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spieltage]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spieltage](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [nvarchar](max) NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [nvarchar](max) NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
	[TeamIconUrl1] [nvarchar](max) NULL,
	[TeamIconUrl2] [nvarchar](max) NULL,
	[StadionID] [int] NULL,
 CONSTRAINT [PK_Spieltage] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageBE]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageBE](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltageBE] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageCL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageCL](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[Verein1_Nr] [nvarchar](max) NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Land1_Nr] [int] NULL,
	[Verein2_Nr] [nvarchar](max) NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Land2_Nr] [int] NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Runde] [nvarchar](max) NULL,
	[RundeDetail] [nchar](100) NULL,
	[Zuschauer] [int] NULL,
	[Verlängerung] [bit] NULL,
	[Elfmeterschiessen] [bit] NULL,
	[Gruppe] [nvarchar](max) NULL,
	[LigaID] [int] NULL,
	[Abgeschlossen] [bit] NULL,
	[GroupID] [int] NULL,
	[TeamIconUrl1] [nvarchar](max) NULL,
	[TeamIconUrl2] [nvarchar](max) NULL,
 CONSTRAINT [ChampionsLegue2] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageEMWM]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageEMWM](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[Verein1_Nr] [nvarchar](max) NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Land1_Nr] [int] NULL,
	[Verein2_Nr] [nvarchar](max) NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Land2_Nr] [int] NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Runde] [nvarchar](max) NULL,
	[RundeDetail] [nchar](100) NULL,
	[Zuschauer] [int] NULL,
	[Verlängerung] [bit] NULL,
	[Elfmeterschiessen] [bit] NULL,
	[Gruppe] [nvarchar](max) NULL,
	[LigaID] [int] NULL,
	[Abgeschlossen] [bit] NULL,
	[GroupID] [int] NULL,
	[TeamIconUrl1] [nvarchar](max) NULL,
	[TeamIconUrl2] [nvarchar](max) NULL,
 CONSTRAINT [SpieltagId] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageES]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageES](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltageES] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageFR]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageFR](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltageFR] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageIT]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageIT](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltageIT] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageL3]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageL3](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [nvarchar](max) NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [nvarchar](max) NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
	[TeamIconUrl1] [nvarchar](max) NULL,
	[TeamIconUrl2] [nvarchar](max) NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[StadionID] [int] NULL,
 CONSTRAINT [PK_SpieltageL3] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageNL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageNL](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltageNL] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltagePL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltagePL](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltagePL] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltagePT]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltagePT](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltagePT] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpieltageTU]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpieltageTU](
	[SpieltagId] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [nvarchar](max) NOT NULL,
	[Saison] [nvarchar](max) NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,
	[Verein1_Nr] [int] NOT NULL,
	[Verein1] [nvarchar](max) NOT NULL,
	[Verein2_Nr] [int] NOT NULL,
	[Verein2] [nvarchar](max) NOT NULL,
	[Tore1_Nr] [int] NOT NULL,
	[Tore2_Nr] [int] NOT NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Ort] [nvarchar](max) NOT NULL,
	[Schiedrichter] [nvarchar](max) NULL,
	[Abgeschlossen] [bit] NOT NULL,
	[Zuschauer] [int] NULL,
 CONSTRAINT [PK_SpieltageTU] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stadion]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stadion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NULL,
	[Stadionname] [nvarchar](max) NULL,
	[Kapazitaet] [int] NULL,
	[Ort] [nvarchar](max) NULL,
	[JahrVon] [int] NULL,
	[JahrBis] [int] NULL,
	[JahrVonDate] [datetime] NULL,
	[JahrBisDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tabellen]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabellen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Verein] [nvarchar](max) NOT NULL,
	[Tab_Sai_Id] [int] NOT NULL,
	[Liga] [nvarchar](max) NOT NULL,
	[Tab_Lig_Id] [nvarchar](max) NULL,
	[Platz] [int] NOT NULL,
	[Spiele] [int] NOT NULL,
	[Punkte] [int] NOT NULL,
	[Gewonnen] [int] NOT NULL,
	[Untentschieden] [int] NOT NULL,
	[Verloren] [int] NOT NULL,
	[TorePlus] [int] NOT NULL,
	[ToreMinus] [int] NOT NULL,
 CONSTRAINT [PK_Tabellen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tore]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [int] NULL,
	[SaisonID] [int] NULL,
	[LigaID] [int] NULL,
	[Spielminute] [int] NULL,
	[Eigentor] [bit] NULL,
	[SpielerID] [int] NULL,
	[SpielerVorlageID] [int] NULL,
	[Spielstand] [nchar](10) NULL,
	[SpieltagId] [int] NOT NULL,
	[Torart] [nvarchar](50) NULL,
	[Elfmeter] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainer]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](100) NOT NULL,
	[Vorname] [nchar](100) NOT NULL,
	[VereinNr] [int] NULL,
	[ImVereinSeit] [date] NULL,
	[Geburtsdatum] [date] NULL,
	[VertragBis] [date] NULL,
 CONSTRAINT [PK_Trainer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Location] [nvarchar](256) NULL,
	[Mail] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vereine]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
	[Fax] [nvarchar](max) NULL,
	[Telefon] [nvarchar](max) NULL,
	[Ort] [nvarchar](max) NULL,
	[EMail] [nvarchar](max) NULL,
	[Strasse] [nvarchar](max) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
 CONSTRAINT [PK_Vereine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineBE]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineBE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereineBE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineCL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineCL](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[LandID] [nchar](10) NULL,
	[TN2023] [bit] NULL,
	[TN2024] [bit] NULL,
 CONSTRAINT [PK_VereineCL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineES]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineES](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereineES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineFR]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineFR](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereineFR] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineIT]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineIT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereineIT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineNL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineNL](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereineNL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereinePL]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereinePL](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereinePL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereinePT]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereinePT](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereinePT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineSaison]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineSaison](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineSaisonAUS]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineSaisonAUS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VereineTU]    Script Date: 04.07.2025 15:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VereineTU](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[Vereinsname1] [nvarchar](max) NOT NULL,
	[Vereinsname2] [nvarchar](max) NOT NULL,
	[Stadion] [nvarchar](max) NOT NULL,
	[Fassungsvermoegen] [nvarchar](max) NULL,
	[Erfolge] [nvarchar](max) NULL,
	[Gegruendet] [int] NOT NULL,
	[Pokal] [bit] NOT NULL,
	[Liga1] [bit] NULL,
	[Hyperlink] [nvarchar](max) NULL,
	[Liga2] [bit] NULL,
 CONSTRAINT [PK_VereineTU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Kader]  WITH CHECK ADD  CONSTRAINT [FK_Kader_Ligen] FOREIGN KEY([LigaID])
REFERENCES [dbo].[Ligen] ([Id])
GO
ALTER TABLE [dbo].[Kader] CHECK CONSTRAINT [FK_Kader_Ligen]
GO
ALTER TABLE [dbo].[Pokalergebnisse]  WITH CHECK ADD  CONSTRAINT [FK_SaisonID] FOREIGN KEY([SaisonID])
REFERENCES [dbo].[Saisonen] ([SaisonID])
GO
ALTER TABLE [dbo].[Pokalergebnisse] CHECK CONSTRAINT [FK_SaisonID]
GO
ALTER TABLE [dbo].[Spieler]  WITH CHECK ADD  CONSTRAINT [FK_Spieler_Ligen] FOREIGN KEY([LigaID])
REFERENCES [dbo].[Ligen] ([Id])
GO
ALTER TABLE [dbo].[Spieler] CHECK CONSTRAINT [FK_Spieler_Ligen]
GO
ALTER TABLE [dbo].[Spieler]  WITH CHECK ADD  CONSTRAINT [FK_Spieler_Saisonen] FOREIGN KEY([SaisonID])
REFERENCES [dbo].[Saisonen] ([SaisonID])
GO
ALTER TABLE [dbo].[Spieler] CHECK CONSTRAINT [FK_Spieler_Saisonen]
GO
ALTER TABLE [dbo].[Tore]  WITH CHECK ADD  CONSTRAINT [FK_SpielerID] FOREIGN KEY([SpielerID])
REFERENCES [dbo].[Kader] ([Id])
GO
ALTER TABLE [dbo].[Tore] CHECK CONSTRAINT [FK_SpielerID]
GO
ALTER TABLE [dbo].[Tore]  WITH CHECK ADD  CONSTRAINT [FK_Tore_Ligen] FOREIGN KEY([LigaID])
REFERENCES [dbo].[Ligen] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tore] CHECK CONSTRAINT [FK_Tore_Ligen]
GO
ALTER TABLE [dbo].[Tore]  WITH CHECK ADD  CONSTRAINT [FK_Tore_Saisonen] FOREIGN KEY([SaisonID])
REFERENCES [dbo].[Saisonen] ([SaisonID])
GO
ALTER TABLE [dbo].[Tore] CHECK CONSTRAINT [FK_Tore_Saisonen]
GO
ALTER TABLE [dbo].[Trainer]  WITH CHECK ADD  CONSTRAINT [FK_VereinNr] FOREIGN KEY([ID])
REFERENCES [dbo].[Trainer] ([ID])
GO
ALTER TABLE [dbo].[Trainer] CHECK CONSTRAINT [FK_VereinNr]
GO
ALTER TABLE [dbo].[Vereine]  WITH CHECK ADD  CONSTRAINT [FK_Vereine_Vereine] FOREIGN KEY([Id])
REFERENCES [dbo].[Vereine] ([Id])
GO
ALTER TABLE [dbo].[Vereine] CHECK CONSTRAINT [FK_Vereine_Vereine]
GO
ALTER TABLE [dbo].[Vereine]  WITH CHECK ADD  CONSTRAINT [VereinNr] FOREIGN KEY([Id])
REFERENCES [dbo].[Vereine] ([Id])
GO
ALTER TABLE [dbo].[Vereine] CHECK CONSTRAINT [VereinNr]
GO
ALTER TABLE [dbo].[Spielplaene]  WITH CHECK ADD  CONSTRAINT [CK_Spielplaene_Tore1] CHECK  (([Tore1_Nr]>(-1) AND [Tore1_Nr]<(50)))
GO
ALTER TABLE [dbo].[Spielplaene] CHECK CONSTRAINT [CK_Spielplaene_Tore1]
GO
ALTER TABLE [dbo].[Spielplaene]  WITH CHECK ADD  CONSTRAINT [CK_Spielplaene_Tore2] CHECK  (([Tore2_Nr]>(-1) AND [Tore2_Nr]<(50)))
GO
ALTER TABLE [dbo].[Spielplaene] CHECK CONSTRAINT [CK_Spielplaene_Tore2]
GO
ALTER TABLE [dbo].[Spieltage]  WITH CHECK ADD  CONSTRAINT [CK_Spieltage_Tore1] CHECK  (([Tore1_Nr]>(-1) AND [Tore1_Nr]<(50)))
GO
ALTER TABLE [dbo].[Spieltage] CHECK CONSTRAINT [CK_Spieltage_Tore1]
GO
ALTER TABLE [dbo].[Spieltage]  WITH CHECK ADD  CONSTRAINT [CK_Spieltage_Tore2] CHECK  (([Tore2_Nr]>(-1) AND [Tore2_Nr]<(50)))
GO
ALTER TABLE [dbo].[Spieltage] CHECK CONSTRAINT [CK_Spieltage_Tore2]
GO
