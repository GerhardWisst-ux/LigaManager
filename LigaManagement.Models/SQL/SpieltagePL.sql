
USE [LigaManager]

CREATE TABLE [dbo].[SpieltageFR](
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
 CONSTRAINT [PK_SpieltageFR] PRIMARY KEY CLUSTERED 
(
	[SpieltagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Spieltage] ON;