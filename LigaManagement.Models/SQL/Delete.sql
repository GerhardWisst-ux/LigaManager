USE [LigaManager]

IF OBJECT_ID(N'dbo.Spieler', N'U') IS NOT NULL
DROP TABLE [dbo].[Spieler]
IF OBJECT_ID(N'dbo.SpielerSpieltag', N'U') IS NOT NULL
DROP TABLE [dbo].[SpielerSpieltag]
IF OBJECT_ID(N'dbo.VereineSaison', N'U') IS NOT NULL
DROP TABLE [dbo].[VereineSaison]
--IF OBJECT_ID(N'dbo.Kader', N'U') IS NOT NULL
--DROP TABLE [dbo].[Kader]
--IF OBJECT_ID(N'dbo.KaderVerein', N'U') IS NOT NULL
--DROP TABLE [dbo].[KaderVerein]
GO