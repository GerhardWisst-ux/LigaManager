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
SET IDENTITY_INSERT [dbo].[SpielerSpieltag] ON 
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (21, 15, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (22, 16, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (23, 37, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (24, 32, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (25, 31, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (26, 34, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (27, 12, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (28, 10, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (29, 9, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (20, 27, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[SpielerSpieltag] ([Id], [KaderID], [SaisonID], [SpieltagNr], [Spielminuten], [Einsatz], [Tore], [Eingewechselt], [EingewechseltMin], [Ausgewechselt], [AusgewechseltMin], [GelbeKarten], [RoteKarten]) VALUES (19, 75, 1, 23, 90, 1, 0, 0, 0, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[SpielerSpieltag] OFF