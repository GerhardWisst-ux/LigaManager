USE [LigaDB]

IF OBJECT_ID(N'dbo.Spieler', N'U') IS NOT NULL
DROP TABLE [dbo].[Spieler]
IF OBJECT_ID(N'dbo.VereineSaison', N'U') IS NOT NULL
DROP TABLE [dbo].[VereineSaison]
GO