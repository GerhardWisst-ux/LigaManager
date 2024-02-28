USE [LigaDB]
--IF OBJECT_ID(N'dbo.Spieler', N'U') IS NULL

CREATE TABLE [dbo].Kader(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SpielerName] [nvarchar](100) NOT NULL,
	[Vorname] [nvarchar](100) NOT NULL,
	[Geburtstag] [date] NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[PositionsNr] [int]  NOT NULL,
	[Groesse] [decimal],
	[Gewicht] [decimal],
	[Laenderspiele] [int],	
	[LaenderspieleTore] [int],	
	[VereinNr] [int] NOT NULL,	
	[LandID] [nvarchar](100),
	[SaisonID] [int],	
	[LigaID] [int],	
	[Rueckennummer] [int],
	[Position] [nvarchar](50) NOT NULL,
	[Einsaetze] [int]  NOT NULL,
	[Spielminuten] [int],
	[Tore] [int],
	[Abloesesumme] [decimal],
	[Image] [image],
	[ImVereinSeit] [date],
	[Aktiv] [bit],
) 
GO

--IF OBJECT_ID(N'dbo.SpielerVerein', N'U') IS NULL
CREATE TABLE [dbo].KaderVerein(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SpielerID] [int] NOT NULL,	
	[VereinNr] [int] NOT NULL,	
	[DateVon] [date],
	[DateBis] [date],
) 
GO

SET IDENTITY_INSERT [dbo].Kader ON 
GO
INSERT [dbo].[Kader] ([Id], [SpielerName], [Vorname], [Geburtstag], [Groesse], [Gewicht], [Laenderspiele], [LaenderspieleTore], [VereinNr], [LandID], [SaisonID], [LigaID], [Rueckennummer], [Einsaetze], [Spielminuten], [Tore], [Abloesesumme], [Image], [ImVereinSeit], [Aktiv]) VALUES (6, N'Undav', N'Deniz', CAST(N'1995-04-14' AS Date), 0 ,0 , 0, 0, 16, 57, 23, 1, 26, 18, 487, 14, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Kader] ([Id], [SpielerName], [Vorname], [Geburtstag], [Groesse], [Gewicht], [Laenderspiele], [LaenderspieleTore], [VereinNr], [LandID], [SaisonID], [LigaID], [Rueckennummer], [Einsaetze], [Spielminuten], [Tore], [Abloesesumme], [Image], [ImVereinSeit], [Aktiv]) VALUES (7, N'Guirassy', N'Serhou', CAST(N'1997-04-14' AS Date), 0 ,0 , 0, 0, 16, 57, 23, 1, 9, 18, 400, 17, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Spieler] OFF
GO
