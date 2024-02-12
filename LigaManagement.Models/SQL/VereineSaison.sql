USE [LigaDB]
--IF OBJECT_ID(N'dbo.VereineSaison', N'U') IS NULL

CREATE TABLE [dbo].[VereineSaison](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,	
	[SaisonID] [int] NOT NULL,	
	[LigaID] [int] NOT NULL,	

) 
GO
