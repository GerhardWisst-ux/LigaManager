USE [LigaDB]
GO
/****** Object:  Table [dbo].[Laender]    Script Date: 12.02.2024 17:33:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](10) NULL,
	[Laendername] [nchar](100) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Laender] ON 
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (1, N'AD        ', N'Andorra                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (2, N'AE        ', N'Vereinigte Arabische Emirate                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (3, N'AF        ', N'Afghanistan                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (4, N'AG        ', N'Antigua und Barbuda                                                                                 ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (5, N'AI        ', N'Anguilla                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (6, N'AL        ', N'Albanien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (7, N'AM        ', N'Armenien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (8, N'AO        ', N'Angola                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (9, N'AQ        ', N'Antarktis                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (10, N'AR        ', N'Argentinien                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (11, N'AS        ', N'Amerikanisch-Samoa                                                                                  ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (12, N'AT        ', N'Österreich                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (13, N'AU        ', N'Australien                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (14, N'AW        ', N'Aruba                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (15, N'AX        ', N'Ålandinseln                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (16, N'AZ        ', N'Aserbaidschan                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (17, N'BA        ', N'Bosnien und Herzegowina                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (18, N'BB        ', N'Barbados                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (19, N'BD        ', N'Bangladesch                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (20, N'BE        ', N'Belgien                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (21, N'BF        ', N'Burkina Faso                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (22, N'BG        ', N'Bulgarien                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (23, N'BH        ', N'Bahrain                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (24, N'BI        ', N'Burundi                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (25, N'BJ        ', N'Benin                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (26, N'BL        ', N'St. Barthélemy                                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (27, N'BM        ', N'Bermuda                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (28, N'BN        ', N'Brunei Darussalam                                                                                   ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (29, N'BO        ', N'Bolivien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (30, N'BQ        ', N'Bonaire, Sint Eustatius und Saba                                                                    ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (31, N'BR        ', N'Brasilien                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (32, N'BS        ', N'Bahamas                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (33, N'BT        ', N'Bhutan                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (34, N'BV        ', N'Bouvetinsel                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (35, N'BW        ', N'Botsuana                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (36, N'BY        ', N'Belarus                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (37, N'BZ        ', N'Belize                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (38, N'CA        ', N'Kanada                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (39, N'CC        ', N'Kokosinseln                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (40, N'CD        ', N'Kongo-Kinshasa                                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (41, N'CF        ', N'Zentralafrikanische Republik                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (42, N'CG        ', N'Kongo-Brazzaville                                                                                   ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (43, N'CH        ', N'Schweiz                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (44, N'CI        ', N'Côte d’Ivoire                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (45, N'CK        ', N'Cookinseln                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (46, N'CL        ', N'Chile                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (47, N'CM        ', N'Kamerun                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (48, N'CN        ', N'China                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (49, N'CO        ', N'Kolumbien                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (50, N'CR        ', N'Costa Rica                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (51, N'CU        ', N'Kuba                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (52, N'CV        ', N'Cabo Verde                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (53, N'CW        ', N'Curaçao                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (54, N'CX        ', N'Weihnachtsinsel                                                                                     ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (55, N'CY        ', N'Zypern                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (56, N'CZ        ', N'Tschechien                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (57, N'DE        ', N'Deutschland                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (58, N'DJ        ', N'Dschibuti                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (59, N'DK        ', N'Dänemark                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (60, N'DM        ', N'Dominica                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (61, N'DO        ', N'Dominikanische Republik                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (62, N'DZ        ', N'Algerien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (63, N'EC        ', N'Ecuador                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (64, N'EE        ', N'Estland                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (65, N'EG        ', N'Ägypten                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (66, N'EH        ', N'Westsahara                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (67, N'ER        ', N'Eritrea                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (68, N'ES        ', N'Spanien                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (69, N'ET        ', N'Äthiopien                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (70, N'FI        ', N'Finnland                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (71, N'FJ        ', N'Fidschi                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (72, N'FK        ', N'Falklandinseln                                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (73, N'FM        ', N'Mikronesien                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (74, N'FO        ', N'Färöer                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (75, N'FR        ', N'Frankreich                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (76, N'GA        ', N'Gabun                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (77, N'GB        ', N'Vereinigtes Königreich                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (78, N'GD        ', N'Grenada                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (79, N'GE        ', N'Georgien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (80, N'GF        ', N'Französisch-Guayana                                                                                 ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (81, N'GG        ', N'Guernsey                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (82, N'GH        ', N'Ghana                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (83, N'GI        ', N'Gibraltar                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (84, N'GL        ', N'Grönland                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (85, N'GM        ', N'Gambia                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (86, N'GN        ', N'Guinea                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (87, N'GP        ', N'Guadeloupe                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (88, N'GQ        ', N'Äquatorialguinea                                                                                    ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (89, N'GR        ', N'Griechenland                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (90, N'GS        ', N'Südgeorgien und die Südlichen Sandwichinseln                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (91, N'GT        ', N'Guatemala                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (92, N'GU        ', N'Guam                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (93, N'GW        ', N'Guinea-Bissau                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (94, N'GY        ', N'Guyana                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (95, N'HK        ', N'Sonderverwaltungsregion Hongkong                                                                    ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (96, N'HM        ', N'Heard und McDonaldinseln                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (97, N'HN        ', N'Honduras                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (98, N'HR        ', N'Kroatien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (99, N'HT        ', N'Haiti                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (100, N'HU        ', N'Ungarn                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (101, N'ID        ', N'Indonesien                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (102, N'IE        ', N'Irland                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (103, N'IL        ', N'Israel                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (104, N'IM        ', N'Isle of Man                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (105, N'IN        ', N'Indien                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (106, N'IO        ', N'Britisches Territorium im Indischen Ozean                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (107, N'IQ        ', N'Irak                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (108, N'IR        ', N'Iran                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (109, N'IS        ', N'Island                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (110, N'IT        ', N'Italien                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (111, N'JE        ', N'Jersey                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (112, N'JM        ', N'Jamaika                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (113, N'JO        ', N'Jordanien                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (114, N'JP        ', N'Japan                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (115, N'KE        ', N'Kenia                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (116, N'KG        ', N'Kirgisistan                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (117, N'KH        ', N'Kambodscha                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (118, N'KI        ', N'Kiribati                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (119, N'KM        ', N'Komoren                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (120, N'KN        ', N'St. Kitts und Nevis                                                                                 ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (121, N'KP        ', N'Nordkorea                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (122, N'KR        ', N'Südkorea                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (123, N'KW        ', N'Kuwait                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (124, N'KY        ', N'Kaimaninseln                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (125, N'KZ        ', N'Kasachstan                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (126, N'LA        ', N'Laos                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (127, N'LB        ', N'Libanon                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (128, N'LC        ', N'St. Lucia                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (129, N'LI        ', N'Liechtenstein                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (130, N'LK        ', N'Sri Lanka                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (131, N'LR        ', N'Liberia                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (132, N'LS        ', N'Lesotho                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (133, N'LT        ', N'Litauen                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (134, N'LU        ', N'Luxemburg                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (135, N'LV        ', N'Lettland                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (136, N'LY        ', N'Libyen                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (137, N'MA        ', N'Marokko                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (138, N'MC        ', N'Monaco                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (139, N'MD        ', N'Republik Moldau                                                                                     ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (140, N'ME        ', N'Montenegro                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (141, N'MF        ', N'St. Martin                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (142, N'MG        ', N'Madagaskar                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (143, N'MH        ', N'Marshallinseln                                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (144, N'MK        ', N'Nordmazedonien                                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (145, N'ML        ', N'Mali                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (146, N'MM        ', N'Myanmar                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (147, N'MN        ', N'Mongolei                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (148, N'MO        ', N'Sonderverwaltungsregion Macau                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (149, N'MP        ', N'Nördliche Marianen                                                                                  ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (150, N'MQ        ', N'Martinique                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (151, N'MR        ', N'Mauretanien                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (152, N'MS        ', N'Montserrat                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (153, N'MT        ', N'Malta                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (154, N'MU        ', N'Mauritius                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (155, N'MV        ', N'Malediven                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (156, N'MW        ', N'Malawi                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (157, N'MX        ', N'Mexiko                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (158, N'MY        ', N'Malaysia                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (159, N'MZ        ', N'Mosambik                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (160, N'NA        ', N'Namibia                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (161, N'NC        ', N'Neukaledonien                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (162, N'NE        ', N'Niger                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (163, N'NF        ', N'Norfolkinsel                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (164, N'NG        ', N'Nigeria                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (165, N'NI        ', N'Nicaragua                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (166, N'NL        ', N'Niederlande                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (167, N'NO        ', N'Norwegen                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (168, N'NP        ', N'Nepal                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (169, N'NR        ', N'Nauru                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (170, N'NU        ', N'Niue                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (171, N'NZ        ', N'Neuseeland                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (172, N'OM        ', N'Oman                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (173, N'PA        ', N'Panama                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (174, N'PE        ', N'Peru                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (175, N'PF        ', N'Französisch-Polynesien                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (176, N'PG        ', N'Papua-Neuguinea                                                                                     ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (177, N'PH        ', N'Philippinen                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (178, N'PK        ', N'Pakistan                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (179, N'PL        ', N'Polen                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (180, N'PM        ', N'St. Pierre und Miquelon                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (181, N'PN        ', N'Pitcairninseln                                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (182, N'PR        ', N'Puerto Rico                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (183, N'PS        ', N'Palästinensische Autonomiegebiete                                                                   ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (184, N'PT        ', N'Portugal                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (185, N'PW        ', N'Palau                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (186, N'PY        ', N'Paraguay                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (187, N'QA        ', N'Katar                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (188, N'RE        ', N'Réunion                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (189, N'RO        ', N'Rumänien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (190, N'RS        ', N'Serbien                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (191, N'RU        ', N'Russland                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (192, N'RW        ', N'Ruanda                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (193, N'SA        ', N'Saudi-Arabien                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (194, N'SB        ', N'Salomonen                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (195, N'SC        ', N'Seychellen                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (196, N'SD        ', N'Sudan                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (197, N'SE        ', N'Schweden                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (198, N'SG        ', N'Singapur                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (199, N'SH        ', N'St. Helena                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (200, N'SI        ', N'Slowenien                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (201, N'SJ        ', N'Spitzbergen und Jan Mayen                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (202, N'SK        ', N'Slowakei                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (203, N'SL        ', N'Sierra Leone                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (204, N'SM        ', N'San Marino                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (205, N'SN        ', N'Senegal                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (206, N'SO        ', N'Somalia                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (207, N'SR        ', N'Suriname                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (208, N'SS        ', N'Südsudan                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (209, N'ST        ', N'São Tomé und Príncipe                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (210, N'SV        ', N'El Salvador                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (211, N'SX        ', N'Sint Maarten                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (212, N'SY        ', N'Syrien                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (213, N'SZ        ', N'Eswatini                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (214, N'TC        ', N'Turks- und Caicosinseln                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (215, N'TD        ', N'Tschad                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (216, N'TF        ', N'Französische Süd- und Antarktisgebiete                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (217, N'TG        ', N'Togo                                                                                                ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (218, N'TH        ', N'Thailand                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (219, N'TJ        ', N'Tadschikistan                                                                                       ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (220, N'TK        ', N'Tokelau                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (221, N'TL        ', N'Timor-Leste                                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (222, N'TM        ', N'Turkmenistan                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (223, N'TN        ', N'Tunesien                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (224, N'TO        ', N'Tonga                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (225, N'TR        ', N'Türkei                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (226, N'TT        ', N'Trinidad und Tobago                                                                                 ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (227, N'TV        ', N'Tuvalu                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (228, N'TW        ', N'Taiwan                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (229, N'TZ        ', N'Tansania                                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (230, N'UA        ', N'Ukraine                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (231, N'UG        ', N'Uganda                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (232, N'UM        ', N'Amerikanische Überseeinseln                                                                         ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (233, N'US        ', N'Vereinigte Staaten                                                                                  ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (234, N'UY        ', N'Uruguay                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (235, N'UZ        ', N'Usbekistan                                                                                          ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (236, N'VA        ', N'Vatikanstadt                                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (237, N'VC        ', N'St. Vincent und die Grenadinen                                                                      ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (238, N'VE        ', N'Venezuela                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (239, N'VG        ', N'Britische Jungferninseln                                                                            ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (240, N'VI        ', N'Amerikanische Jungferninseln                                                                        ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (241, N'VN        ', N'Vietnam                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (242, N'VU        ', N'Vanuatu                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (243, N'WF        ', N'Wallis und Futuna                                                                                   ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (244, N'WS        ', N'Samoa                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (245, N'YE        ', N'Jemen                                                                                               ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (246, N'YT        ', N'Mayotte                                                                                             ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (247, N'ZA        ', N'Südafrika                                                                                           ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (248, N'ZM        ', N'Sambia                                                                                              ')
GO
INSERT [dbo].[Laender] ([ID], [Code], [Laendername]) VALUES (249, N'ZW        ', N'Simbabwe                                                                                            ')
GO
SET IDENTITY_INSERT [dbo].[Laender] OFF
GO
