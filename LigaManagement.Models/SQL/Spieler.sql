USE [LigaManager]
--IF OBJECT_ID(N'dbo.Spieler', N'U') IS NULL

CREATE TABLE [dbo].Spieler(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,
	[SaisonID] [int] NOT NULL,
	[LigaID] [int] NOT NULL,	
	[Rueckennummer] [int] NOT NULL,
	[Einsaetze] [int] NOT NULL,
	[Spielminuten] [int] NOT NULL,
	[Tore] [int] NOT NULL,
	[Abloesesumme] [decimal] NOT NULL,
	[Image] [image] NOT NULL,
) 
GO