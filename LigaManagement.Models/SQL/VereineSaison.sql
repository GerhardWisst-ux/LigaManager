USE [LigaManager]
--IF OBJECT_ID(N'dbo.VereineSaison', N'U') IS NULL

CREATE TABLE [dbo].[VereineSaison](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,	
	[SaisonID] [int] NOT NULL,	
	[LigaID] [int] NOT NULL,	

) 
GO

CREATE TABLE [dbo].[StadionVerein](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VereinNr] [int] NOT NULL,		
	[STADION] [nchar](200) NULL,
	[DatumVon] Datetime,
	[DatumBis] Datetime,
) 
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1, 30, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2, 44, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (3, 24, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (4, 11, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (5, 16, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (6, 2, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (7, 31, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (8, 26, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (9, 25, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (10, 13, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (12, 29, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (16, 7, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (17, 39, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (18, 19, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (19, 20, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (21, 51, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (22, 30, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (23, 44, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (24, 24, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (25, 11, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (26, 16, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (27, 2, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (28, 31, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (29, 26, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (30, 25, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (31, 13, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (32, 29, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (33, 7, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (34, 23, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (35, 19, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (36, 20, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (37, 49, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (38, 1, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (39, 2, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (40, 6, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (41, 7, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (42, 10, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (43, 11, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (44, 13, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (45, 14, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (46, 16, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (47, 20, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (48, 24, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (49, 25, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (50, 26, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (51, 27, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (52, 29, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (53, 32, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (54, 36, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (55, 47, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (56, 1, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (57, 2, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (58, 6, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (59, 7, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (60, 10, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (61, 11, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (62, 13, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (63, 14, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (64, 16, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (65, 24, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (66, 25, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (67, 26, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (68, 27, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (69, 29, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (70, 32, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (71, 33, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (72, 36, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (73, 56, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (74, 64, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (75, 64, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (76, 64, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (77, 64, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (78, 64, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (79, 64, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (80, 64, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (81, 64, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (82, 64, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (83, 64, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (84, 64, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (85, 64, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (86, 64, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (87, 64, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (88, 64, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (89, 64, 1, 33)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (90, 64, 1, 36)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (91, 64, 1, 57)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (92, 59, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (93, 59, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (94, 59, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (95, 59, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (96, 59, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (97, 59, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (98, 59, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (99, 59, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (100, 59, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (101, 59, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (102, 59, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (103, 59, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (104, 59, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (105, 59, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (106, 59, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (107, 59, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (108, 59, 1, 33)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (109, 59, 1, 39)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (110, 60, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (111, 60, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (112, 60, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (113, 60, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (114, 60, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (115, 60, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (116, 60, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (117, 60, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (118, 60, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (119, 60, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (120, 60, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (121, 60, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (122, 60, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (123, 60, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (124, 60, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (125, 60, 1, 33)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (126, 60, 1, 52)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (127, 60, 1, 56)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (128, 58, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (129, 58, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (130, 58, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (131, 58, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (132, 58, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (133, 58, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (134, 58, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (135, 58, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (136, 58, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (137, 58, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (138, 58, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (139, 58, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (140, 58, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (141, 58, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (142, 58, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (143, 58, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (144, 58, 1, 33)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (145, 58, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (146, 56, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (147, 56, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (148, 56, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (149, 56, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (150, 56, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (151, 56, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (152, 56, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (153, 56, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (154, 56, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (155, 56, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (156, 56, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (157, 56, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (158, 56, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (159, 56, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (160, 56, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (161, 56, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (162, 56, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (163, 56, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (164, 57, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (165, 57, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (166, 57, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (167, 57, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (168, 57, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (169, 57, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (170, 57, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (171, 57, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (172, 57, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (173, 57, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (174, 57, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (175, 57, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (176, 57, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (177, 57, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (178, 57, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (179, 57, 1, 33)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (180, 57, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (181, 57, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (182, 53, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (183, 53, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (184, 53, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (185, 53, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (186, 53, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (187, 53, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (188, 53, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (189, 53, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (190, 53, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (191, 53, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (192, 53, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (193, 53, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (194, 53, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (195, 53, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (196, 53, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (197, 53, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (198, 53, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (199, 53, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (200, 54, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (201, 54, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (202, 54, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (203, 54, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (204, 54, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (205, 54, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (206, 54, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (207, 54, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (208, 54, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (209, 54, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (210, 54, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (211, 54, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (212, 54, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (213, 54, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (214, 54, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (215, 54, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (216, 54, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (217, 54, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (218, 55, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (219, 55, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (220, 55, 1, 6)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (221, 55, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (222, 55, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (223, 55, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (224, 55, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (225, 55, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (226, 55, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (227, 55, 1, 17)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (228, 55, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (229, 55, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (230, 55, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (231, 55, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (232, 55, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (233, 55, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (234, 55, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (235, 55, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (236, 48, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (237, 48, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (238, 48, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (239, 48, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (240, 48, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (241, 48, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (242, 48, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (243, 48, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (244, 48, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (245, 48, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (246, 48, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (247, 48, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (248, 48, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (249, 48, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (250, 48, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (251, 48, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (252, 48, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (253, 48, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (254, 50, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (255, 50, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (256, 50, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (257, 50, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (258, 50, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (259, 50, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (260, 50, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (261, 50, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (262, 50, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (263, 50, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (264, 50, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (265, 50, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (266, 50, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (267, 50, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (268, 50, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (269, 50, 1, 39)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (270, 50, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (271, 50, 1, 53)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (272, 51, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (273, 51, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (274, 51, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (275, 51, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (276, 51, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (277, 51, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (278, 51, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (279, 51, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (280, 51, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (281, 51, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (282, 51, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (283, 51, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (284, 51, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (285, 51, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (286, 51, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (287, 51, 1, 36)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (288, 51, 1, 39)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (289, 51, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (290, 52, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (291, 52, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (292, 52, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (293, 52, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (294, 52, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (295, 52, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (296, 52, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (297, 52, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (298, 52, 1, 17)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (299, 52, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (300, 52, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (301, 52, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (302, 52, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (303, 52, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (304, 52, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (305, 52, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (306, 52, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (307, 52, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (308, 44, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (309, 44, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (310, 44, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (311, 44, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (312, 44, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (313, 44, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (314, 44, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (315, 44, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (316, 44, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (317, 44, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (318, 44, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (319, 44, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (320, 44, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (321, 44, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (322, 44, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (323, 44, 1, 41)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (324, 44, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (325, 44, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (326, 45, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (327, 45, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (328, 45, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (329, 45, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (330, 45, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (331, 45, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (332, 45, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (333, 45, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (334, 45, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (335, 45, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (336, 45, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (337, 45, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (338, 45, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (339, 45, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (340, 45, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (341, 45, 1, 41)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (342, 45, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (343, 45, 1, 55)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (344, 46, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (345, 46, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (346, 46, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (347, 46, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (348, 46, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (349, 46, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (350, 46, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (351, 46, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (352, 46, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (353, 46, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (354, 46, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (355, 46, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (356, 46, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (357, 46, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (358, 46, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (359, 46, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (360, 46, 1, 54)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (361, 46, 1, 55)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (362, 48, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (363, 48, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (364, 48, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (365, 48, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (366, 48, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (367, 48, 1, 14)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (368, 48, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (369, 48, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (370, 48, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (371, 48, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (372, 48, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (373, 48, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (374, 48, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (375, 48, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (376, 48, 1, 32)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (377, 48, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (378, 48, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (379, 48, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (380, 40, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (381, 40, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (382, 40, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (383, 40, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (384, 40, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (385, 40, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (386, 40, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (387, 40, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (388, 40, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (389, 40, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (390, 40, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (391, 40, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (392, 40, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (393, 40, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (394, 40, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (395, 40, 1, 38)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (396, 40, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (397, 40, 1, 49)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (398, 41, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (399, 41, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (400, 41, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (401, 41, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (402, 41, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (403, 41, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (404, 41, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (405, 41, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (406, 41, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (407, 41, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (408, 41, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (409, 41, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (410, 41, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (411, 41, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (412, 41, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (413, 41, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (414, 41, 1, 38)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (415, 41, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (416, 42, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (417, 42, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (418, 42, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (419, 42, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (420, 42, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (421, 42, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (422, 42, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (423, 42, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (424, 42, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (425, 42, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (426, 42, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (427, 42, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (428, 42, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (429, 42, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (430, 42, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (431, 42, 1, 38)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (432, 42, 1, 41)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (433, 42, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (434, 43, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (435, 43, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (436, 43, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (437, 43, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (438, 43, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (439, 43, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (440, 43, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (441, 43, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (442, 43, 1, 20)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (443, 43, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (444, 43, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (445, 43, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (446, 43, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (447, 43, 1, 34)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (448, 43, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (449, 43, 1, 41)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (450, 43, 1, 42)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (451, 43, 1, 44)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (452, 39, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (453, 39, 1, 2)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (454, 39, 1, 7)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (455, 39, 1, 10)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (456, 39, 1, 11)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (457, 39, 1, 13)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (458, 39, 1, 16)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (459, 39, 1, 19)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (460, 39, 1, 23)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (461, 39, 1, 24)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (462, 39, 1, 25)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (463, 39, 1, 26)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (464, 39, 1, 27)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (465, 39, 1, 29)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (466, 39, 1, 30)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (467, 39, 1, 31)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (468, 39, 1, 35)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (469, 39, 1, 44)
GO