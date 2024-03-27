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
SET IDENTITY_INSERT [dbo].[VereineSaison] ON;
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (520, 1, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (521, 2, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (522, 6, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (523, 7, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (524, 10, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (525, 11, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (526, 13, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (527, 14, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (528, 16, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (529, 19, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (530, 24, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (531, 25, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (532, 26, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (533, 29, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (534, 32, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (535, 39, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (536, 40, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (537, 47, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (538, 2, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (539, 7, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (540, 11, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (541, 13, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (542, 16, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (543, 19, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (544, 20, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (545, 23, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (546, 24, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (547, 25, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (548, 26, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (549, 29, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (550, 30, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (551, 31, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (552, 44, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (553, 49, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (554, 1, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (555, 2, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (556, 7, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (557, 10, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (558, 11, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (559, 13, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (560, 16, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (561, 19, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (562, 23, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (563, 24, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (564, 25, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (565, 26, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (566, 27, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (567, 29, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (568, 30, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (569, 31, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (570, 35, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (571, 44, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (572, 1, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (573, 2, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (574, 7, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (575, 10, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (576, 11, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (577, 13, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (578, 16, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (579, 19, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (580, 23, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (581, 24, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (582, 25, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (583, 26, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (584, 29, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (585, 30, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (586, 31, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (587, 38, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (588, 44, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (589, 49, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (590, 1, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (591, 2, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (592, 7, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (593, 10, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (594, 11, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (595, 13, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (596, 16, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (597, 19, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (598, 20, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (599, 23, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (600, 24, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (601, 25, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (602, 26, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (603, 30, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (604, 31, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (605, 34, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (606, 38, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (607, 44, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (608, 1, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (609, 2, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (610, 7, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (611, 10, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (612, 11, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (613, 13, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (614, 16, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (615, 19, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (616, 20, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (617, 23, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (618, 24, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (619, 26, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (620, 30, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (621, 31, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (622, 35, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (623, 38, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (624, 41, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (625, 44, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (626, 1, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (627, 2, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (628, 7, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (629, 10, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (630, 11, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (631, 13, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (632, 14, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (633, 16, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (634, 19, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (635, 20, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (636, 23, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (637, 24, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (638, 26, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (639, 27, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (640, 30, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (641, 41, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (642, 42, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (643, 44, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (644, 1, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (645, 7, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (646, 10, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (647, 11, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (648, 13, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (649, 14, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (650, 16, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (651, 19, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (652, 20, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (653, 23, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (654, 24, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (655, 26, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (656, 27, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (657, 30, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (658, 34, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (659, 41, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (660, 44, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (661, 55, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (662, 1, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (663, 7, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (664, 10, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (665, 11, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (666, 13, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (667, 14, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (668, 16, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (669, 19, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (670, 20, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (671, 23, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (672, 24, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (673, 26, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (674, 27, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (675, 34, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (676, 35, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (677, 44, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (678, 54, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (679, 55, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (680, 1, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (681, 7, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (682, 10, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (683, 11, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (684, 13, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (685, 14, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (686, 16, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (687, 19, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (688, 20, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (689, 24, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (690, 26, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (691, 27, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (692, 30, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (693, 34, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (694, 35, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (695, 44, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (696, 53, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (697, 55, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (698, 1, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (699, 7, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (700, 10, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (701, 11, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (702, 13, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (703, 14, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (704, 19, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (705, 20, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (706, 23, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (707, 24, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (708, 26, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (709, 27, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (710, 29, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (711, 30, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (712, 32, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (713, 34, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (714, 35, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (715, 44, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (716, 1, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (717, 2, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (718, 7, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (719, 10, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (720, 11, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (721, 13, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (722, 14, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (723, 19, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (724, 20, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (725, 24, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (726, 26, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (727, 27, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (728, 29, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (729, 30, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (730, 35, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (731, 39, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (732, 44, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (733, 53, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (734, 1, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (735, 2, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (736, 7, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (737, 10, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (738, 11, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (739, 13, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (740, 14, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (741, 16, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (742, 19, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (743, 20, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (744, 24, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (745, 26, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (746, 27, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (747, 30, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (748, 31, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (749, 36, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (750, 39, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (751, 44, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (752, 1, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (753, 2, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (754, 7, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (755, 10, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (756, 11, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (757, 13, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (758, 14, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (759, 16, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (760, 17, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (761, 19, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (762, 20, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (763, 24, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (764, 25, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (765, 26, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (766, 27, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (767, 30, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (768, 42, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (769, 44, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (770, 1, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (771, 2, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (772, 6, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (773, 7, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (774, 10, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (775, 11, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (776, 13, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (777, 14, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (778, 16, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (779, 19, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (780, 20, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (781, 24, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (782, 26, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (783, 27, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (784, 30, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (785, 31, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (786, 32, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (787, 44, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (788, 1, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (789, 2, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (790, 6, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (791, 7, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (792, 10, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (793, 11, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (794, 14, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (795, 16, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (796, 19, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (797, 24, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (798, 25, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (799, 26, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (800, 27, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (801, 29, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (802, 31, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (803, 32, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (804, 42, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (805, 44, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (806, 1, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (807, 2, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (808, 6, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (809, 7, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (810, 10, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (811, 11, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (812, 13, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (813, 14, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (814, 16, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (815, 17, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (816, 24, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (817, 25, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (818, 26, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (819, 27, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (820, 29, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (821, 30, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (822, 42, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (823, 44, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (824, 1, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (825, 2, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (826, 6, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (827, 7, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (828, 10, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (829, 11, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (830, 13, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (831, 14, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (832, 16, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (833, 19, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (834, 20, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (835, 24, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (836, 25, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (837, 26, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (838, 27, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (839, 29, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (840, 30, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (841, 42, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (842, 1, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (843, 2, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (844, 6, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (845, 7, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (846, 10, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (847, 11, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (848, 13, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (849, 14, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (850, 16, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (851, 24, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (852, 25, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (853, 26, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (854, 27, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (855, 30, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (856, 32, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (857, 33, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (858, 34, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (859, 42, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (860, 1, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (861, 2, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (862, 6, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (863, 7, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (864, 10, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (865, 11, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (866, 13, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (867, 14, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (868, 16, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (869, 19, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (870, 24, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (871, 26, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (872, 27, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (873, 29, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (874, 30, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (875, 32, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (876, 33, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (877, 42, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (878, 1, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (879, 2, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (880, 6, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (881, 7, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (882, 10, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (883, 11, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (884, 13, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (885, 14, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (886, 16, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (887, 19, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (888, 24, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (889, 25, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (890, 26, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (891, 27, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (892, 32, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (893, 33, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (894, 52, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (895, 56, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (896, 1, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (897, 2, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (898, 6, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (899, 7, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (900, 10, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (901, 11, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (902, 13, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (903, 14, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (904, 16, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (905, 19, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (906, 23, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (907, 24, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (908, 25, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (909, 26, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (910, 29, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (911, 32, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (912, 33, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (913, 56, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (914, 1, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (915, 2, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (916, 6, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (917, 7, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (918, 10, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (919, 11, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (920, 13, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (921, 14, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (922, 16, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (923, 23, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (924, 24, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (925, 25, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (926, 26, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (927, 29, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (928, 32, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (929, 33, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (930, 36, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (931, 57, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (932, 1, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (933, 2, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (934, 6, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (935, 7, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (936, 10, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (937, 11, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (938, 13, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (939, 14, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (940, 16, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (941, 24, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (942, 25, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (943, 26, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (944, 27, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (945, 29, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (946, 32, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (947, 33, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (948, 36, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (949, 56, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (950, 1, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (951, 2, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (952, 6, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (953, 7, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (954, 10, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (955, 11, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (956, 13, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (957, 14, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (958, 16, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (959, 20, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (960, 24, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (961, 25, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (962, 26, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (963, 27, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (964, 29, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (965, 32, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (966, 36, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (967, 47, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (968, 1, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (969, 10, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (970, 11, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (971, 13, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (972, 16, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (973, 19, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (974, 2, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (975, 24, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (976, 25, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (977, 26, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (978, 29, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (979, 40, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (980, 44, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (981, 47, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (982, 48, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (983, 5, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (984, 6, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (985, 7, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (986, 1, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (987, 10, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (988, 11, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (989, 13, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (990, 14, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (991, 16, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (992, 19, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (993, 2, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (994, 24, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (995, 26, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (996, 29, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (997, 31, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (998, 32, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (999, 40, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1000, 44, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1001, 5, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1002, 6, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1003, 7, 94, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1004, 1, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1005, 10, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1006, 11, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1007, 13, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1008, 14, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1009, 16, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1010, 19, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1011, 2, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1012, 26, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1013, 27, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1014, 29, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1015, 31, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1016, 36, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1017, 42, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1018, 43, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1019, 44, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1020, 5, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1021, 6, 95, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1022, 1, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1023, 10, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1024, 11, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1025, 13, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1026, 14, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1027, 16, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1028, 19, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1029, 2, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1030, 26, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1031, 27, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1032, 29, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1033, 31, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1034, 36, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1035, 42, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1036, 43, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1037, 44, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1038, 5, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1039, 6, 96, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1040, 1, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1041, 10, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1042, 11, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1043, 13, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1044, 14, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1045, 16, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1046, 19, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1047, 2, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1048, 20, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1049, 24, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1050, 26, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1051, 29, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1052, 31, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1053, 42, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1054, 43, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1055, 44, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1056, 6, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1057, 8, 97, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1058, 1, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1059, 10, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1060, 13, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1061, 14, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1062, 16, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1063, 19, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1064, 2, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1065, 20, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1066, 24, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1067, 25, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1068, 26, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1069, 31, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1070, 43, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1071, 44, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1072, 5, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1073, 6, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1074, 7, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1075, 8, 98, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1076, 1, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1077, 10, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1078, 11, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1079, 12, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1080, 13, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1081, 14, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1082, 15, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1083, 16, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1084, 19, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1085, 2, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1086, 20, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1087, 3, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1088, 4, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1089, 5, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1090, 6, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1091, 7, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1092, 8, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1093, 9, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1094, 1, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1095, 13, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1096, 16, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1097, 19, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1098, 2, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1099, 20, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1100, 24, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1101, 26, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1102, 31, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1103, 42, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1104, 43, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1105, 44, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1106, 45, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1107, 46, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1108, 5, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1109, 6, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1110, 7, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1111, 8, 99, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1112, 1, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1113, 11, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1114, 13, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1115, 14, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1116, 16, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1117, 19, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1118, 2, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1119, 20, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1120, 24, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1121, 26, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1122, 31, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1123, 37, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1124, 43, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1125, 45, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1126, 5, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1127, 6, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1128, 7, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1129, 8, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1130, 1, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1131, 10, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1132, 11, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1133, 13, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1134, 16, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1135, 19, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1136, 2, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1137, 20, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1138, 24, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1139, 25, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1140, 26, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1141, 31, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1142, 36, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1143, 37, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1144, 43, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1145, 5, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1146, 6, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1147, 8, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1148, 1, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1149, 10, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1150, 13, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1151, 14, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1152, 16, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1153, 19, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1154, 2, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1155, 20, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1156, 23, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1157, 24, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1158, 25, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1159, 26, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1160, 31, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1161, 37, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1162, 42, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1163, 43, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1164, 6, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1165, 8, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1166, 1, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1167, 10, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1168, 11, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1169, 13, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1170, 14, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1171, 16, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1172, 19, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1173, 2, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1174, 20, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1175, 23, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1176, 24, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1177, 26, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1178, 31, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1179, 43, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1180, 5, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1181, 6, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1182, 7, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1183, 8, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1184, 1, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1185, 10, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1186, 13, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1187, 14, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1188, 16, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1189, 19, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1190, 2, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1191, 20, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1192, 23, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1193, 24, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1194, 25, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1195, 26, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1196, 42, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1197, 43, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1198, 5, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1199, 6, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1200, 8, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1201, 9, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1202, 1, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1203, 10, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1204, 11, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1205, 13, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1206, 16, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1207, 19, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1208, 2, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1209, 20, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1210, 23, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1211, 24, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1212, 25, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1213, 26, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1214, 42, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1215, 44, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1216, 6, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1217, 7, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1218, 8, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1219, 9, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1220, 1, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1221, 10, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1222, 13, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1223, 14, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1224, 16, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1225, 19, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1226, 2, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1227, 20, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1228, 23, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1229, 25, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1230, 26, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1231, 37, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1232, 38, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1233, 42, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1234, 6, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1235, 7, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1236, 8, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1237, 9, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1238, 1, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1239, 13, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1240, 14, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1241, 16, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1242, 19, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1243, 2, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1244, 20, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1245, 23, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1246, 25, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1247, 26, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1248, 29, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1249, 37, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1250, 42, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1251, 43, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1252, 44, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1253, 6, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1254, 7, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1255, 8, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1256, 1, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1257, 10, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1258, 11, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1259, 12, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1260, 13, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1261, 14, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1262, 16, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1263, 19, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1264, 2, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1265, 20, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1266, 23, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1267, 26, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1268, 29, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1269, 37, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1270, 42, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1271, 6, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1272, 7, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1273, 8, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1274, 1, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1275, 10, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1276, 11, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1277, 12, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1278, 13, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1279, 14, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1280, 16, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1281, 19, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1282, 2, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1283, 20, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1284, 23, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1285, 25, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1286, 26, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1287, 5, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1288, 6, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1289, 7, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1290, 8, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1291, 9, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1292, 1, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1293, 10, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1294, 11, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1295, 12, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1296, 13, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1297, 16, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1298, 19, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1299, 2, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1300, 23, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1301, 24, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1302, 25, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1303, 26, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1304, 36, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1305, 5, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1306, 6, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1307, 7, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1308, 8, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1309, 9, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1310, 1, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1311, 10, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1312, 11, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1313, 12, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1314, 13, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1315, 15, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1316, 16, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1317, 19, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1318, 2, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1319, 20, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1320, 23, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1321, 24, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1322, 25, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1323, 26, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1324, 5, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1325, 6, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1326, 8, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1327, 9, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1328, 1, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1329, 10, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1330, 12, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1331, 13, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1332, 15, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1333, 16, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1334, 19, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1335, 2, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1336, 21, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1337, 23, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1338, 25, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1339, 26, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1340, 27, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1341, 5, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1342, 6, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1343, 7, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1344, 8, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1345, 9, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1346, 1, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1347, 10, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1348, 12, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1349, 13, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1350, 15, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1351, 16, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1352, 19, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1353, 2, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1354, 20, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1355, 23, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1356, 25, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1357, 26, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1358, 30, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1359, 5, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1360, 6, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1361, 7, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1362, 8, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1363, 9, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1364, 1, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1365, 10, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1366, 11, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1367, 12, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1368, 13, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1369, 15, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1370, 16, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1371, 19, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1372, 2, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1373, 20, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1374, 22, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1375, 23, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1376, 26, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1377, 5, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1378, 6, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1379, 7, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1380, 8, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1381, 9, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1382, 1, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1383, 10, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1384, 11, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1385, 12, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1386, 13, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1387, 15, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1388, 16, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1389, 17, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1390, 19, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1391, 2, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1392, 20, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1393, 23, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1394, 26, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1395, 28, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1396, 6, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1397, 7, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1398, 8, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1399, 9, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1400, 1, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1401, 10, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1402, 11, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1403, 12, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1404, 13, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1405, 15, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1406, 17, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1407, 19, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1408, 2, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1409, 20, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1410, 26, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1411, 28, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1412, 3, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1413, 5, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1414, 6, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1415, 7, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1416, 8, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1417, 9, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1418, 1, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1419, 10, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1420, 11, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1421, 12, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1422, 13, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1423, 15, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1424, 16, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1425, 19, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1426, 2, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1427, 20, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1428, 23, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1429, 26, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1430, 3, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1431, 5, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1432, 6, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1433, 7, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1434, 8, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1435, 9, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1436, 1, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1437, 10, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1438, 12, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1439, 13, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1440, 15, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1441, 16, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1442, 19, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1443, 2, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1444, 20, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1445, 23, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1446, 25, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1447, 27, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1448, 3, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1449, 5, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1450, 6, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1451, 7, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1452, 8, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1453, 9, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1454, 1, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1455, 10, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1456, 11, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1457, 12, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1458, 13, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1459, 15, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1460, 19, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1461, 2, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1462, 20, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1463, 22, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1464, 27, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1465, 3, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1466, 4, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1467, 5, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1468, 6, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1469, 7, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1470, 8, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1471, 9, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1472, 1, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1473, 10, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1474, 11, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1475, 12, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1476, 13, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1477, 15, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1478, 16, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1479, 19, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1480, 2, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1481, 20, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1482, 3, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1483, 4, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1484, 42, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1485, 5, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1486, 6, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1487, 7, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1488, 8, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1489, 9, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1490, 1, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1491, 10, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1492, 11, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1493, 12, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1494, 14, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1495, 15, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1496, 16, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1497, 2, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1498, 20, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1499, 21, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1500, 3, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1501, 4, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1502, 42, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1503, 5, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1504, 6, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1505, 7, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1506, 8, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1507, 9, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1529, 1, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1530, 10, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1531, 11, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1532, 12, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1533, 13, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1534, 14, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1535, 15, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1536, 16, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1537, 17, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1538, 18, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1539, 2, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1540, 3, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1541, 4, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1542, 5, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1543, 6, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1544, 7, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1545, 8, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1546, 9, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[VereineSaison] OFF
GO
