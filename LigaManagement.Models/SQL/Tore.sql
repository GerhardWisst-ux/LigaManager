USE [LigaManager]
IF OBJECT_ID(N'dbo.[Tore]', N'U') IS NULL

CREATE TABLE [dbo].[Tore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SpieltagNr] [int] NULL,
	[SaisonID] [int] NULL,
	[LigaID] [int] NULL,
	[Spielminute] [int] NULL,
	[SpielerID] [int] NULL
) ON [PRIMARY]
GO

