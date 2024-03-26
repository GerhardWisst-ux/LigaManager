USE [LigaManager]
--IF OBJECT_ID(N'dbo.SpielerSpieltag', N'U') IS NULL

CREATE TABLE [dbo].SpielerSpieltag(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KaderID] [int] NOT NULL,
	[SaisonID] [int] NOT NULL,
	[SpieltagNr] [int] NOT NULL,
	[Spielminuten] [int] NOT NULL,
	[Einsatz] [int] NOT NULL,
	[Tore] [int] NOT NULL,
	[Eingewechselt] [bit],
	[EingewechseltMin] [int],
	[Ausgewechselt] [bit],
	[AusgewechseltMin] [int] NOT NULL,
	[GelbeKarten] [bit],
	[RoteKarten] [bit],
	[Position] [nchar](50) NULL,
	[Sortierung] [nchar](50) NULL,
) 
GO