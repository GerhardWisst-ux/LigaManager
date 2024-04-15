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
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1675, 1, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1676, 10, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1677, 11, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1678, 12, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1679, 13, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1680, 14, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1681, 15, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1682, 16, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1683, 17, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1684, 18, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1685, 2, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1686, 3, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1687, 4, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1688, 5, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1689, 6, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1690, 7, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1691, 8, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1692, 9, 1, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1693, 1, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1694, 10, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1695, 11, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1696, 12, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1697, 13, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1698, 14, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1699, 15, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1700, 16, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1701, 19, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1702, 2, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1703, 20, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1704, 3, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1705, 4, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1706, 5, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1707, 6, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1708, 7, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1709, 8, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1710, 9, 2, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1711, 1, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1712, 10, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1713, 11, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1714, 12, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1715, 14, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1716, 15, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1717, 16, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1718, 2, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1719, 20, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1720, 21, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1721, 3, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1722, 4, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1723, 42, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1724, 5, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1725, 6, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1726, 7, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1727, 8, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1728, 9, 3, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1729, 1, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1730, 10, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1731, 11, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1732, 12, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1733, 13, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1734, 15, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1735, 16, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1736, 19, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1737, 2, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1738, 20, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1739, 3, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1740, 4, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1741, 42, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1742, 5, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1743, 6, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1744, 7, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1745, 8, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1746, 9, 4, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1747, 1, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1748, 10, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1749, 11, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1750, 12, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1751, 13, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1752, 15, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1753, 19, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1754, 2, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1755, 20, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1756, 22, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1757, 27, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1758, 3, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1759, 4, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1760, 5, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1761, 6, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1762, 7, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1763, 8, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1764, 9, 5, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1765, 1, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1766, 10, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1767, 12, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1768, 13, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1769, 15, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1770, 16, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1771, 19, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1772, 2, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1773, 20, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1774, 23, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1775, 25, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1776, 27, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1777, 3, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1778, 5, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1779, 6, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1780, 7, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1781, 8, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1782, 9, 6, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1783, 1, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1784, 10, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1785, 11, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1786, 12, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1787, 13, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1788, 15, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1789, 16, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1790, 19, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1791, 2, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1792, 20, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1793, 23, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1794, 26, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1795, 3, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1796, 5, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1797, 6, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1798, 7, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1799, 8, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1800, 9, 7, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1801, 1, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1802, 10, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1803, 11, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1804, 12, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1805, 13, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1806, 15, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1807, 17, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1808, 19, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1809, 2, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1810, 20, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1811, 26, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1812, 28, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1813, 3, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1814, 5, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1815, 6, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1816, 7, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1817, 8, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1818, 9, 8, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1819, 1, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1820, 10, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1821, 11, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1822, 12, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1823, 13, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1824, 15, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1825, 16, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1826, 17, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1827, 19, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1828, 2, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1829, 20, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1830, 23, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1831, 26, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1832, 28, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1833, 6, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1834, 7, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1835, 8, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1836, 9, 9, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1837, 1, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1838, 10, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1839, 11, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1840, 12, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1841, 13, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1842, 15, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1843, 16, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1844, 19, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1845, 2, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1846, 20, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1847, 22, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1848, 23, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1849, 26, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1850, 5, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1851, 6, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1852, 7, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1853, 8, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1854, 9, 10, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1855, 1, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1856, 10, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1857, 12, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1858, 13, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1859, 15, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1860, 16, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1861, 19, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1862, 2, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1863, 20, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1864, 23, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1865, 25, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1866, 26, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1867, 30, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1868, 5, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1869, 6, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1870, 7, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1871, 8, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1872, 9, 11, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1873, 1, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1874, 10, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1875, 12, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1876, 13, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1877, 15, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1878, 16, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1879, 19, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1880, 2, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1881, 21, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1882, 23, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1883, 25, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1884, 26, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1885, 27, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1886, 5, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1887, 6, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1888, 7, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1889, 8, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1890, 9, 12, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1891, 1, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1892, 10, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1893, 11, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1894, 12, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1895, 13, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1896, 15, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1897, 16, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1898, 19, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1899, 2, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1900, 20, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1901, 23, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1902, 24, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1903, 25, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1904, 26, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1905, 5, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1906, 6, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1907, 8, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1908, 9, 13, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1909, 1, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1910, 10, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1911, 11, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1912, 12, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1913, 13, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1914, 16, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1915, 19, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1916, 2, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1917, 23, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1918, 24, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1919, 25, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1920, 26, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1921, 36, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1922, 5, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1923, 6, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1924, 7, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1925, 8, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1926, 9, 14, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1927, 1, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1928, 10, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1929, 11, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1930, 12, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1931, 13, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1932, 14, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1933, 16, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1934, 19, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1935, 2, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1936, 20, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1937, 23, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1938, 25, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1939, 26, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1940, 5, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1941, 6, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1942, 7, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1943, 8, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1944, 9, 15, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1945, 1, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1946, 10, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1947, 11, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1948, 12, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1949, 13, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1950, 14, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1951, 16, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1952, 19, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1953, 2, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1954, 20, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1955, 23, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1956, 26, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1957, 29, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1958, 37, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1959, 42, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1960, 6, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1961, 7, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1962, 8, 16, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1963, 1, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1964, 13, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1965, 14, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1966, 16, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1967, 19, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1968, 2, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1969, 20, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1970, 23, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1971, 25, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1972, 26, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1973, 29, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1974, 37, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1975, 42, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1976, 43, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1977, 44, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1978, 6, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1979, 7, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1980, 8, 17, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1981, 1, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1982, 10, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1983, 13, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1984, 14, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1985, 16, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1986, 19, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1987, 2, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1988, 20, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1989, 23, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1990, 25, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1991, 26, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1992, 37, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1993, 38, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1994, 42, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1995, 6, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1996, 7, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1997, 8, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1998, 9, 18, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (1999, 1, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2000, 10, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2001, 11, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2002, 13, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2003, 16, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2004, 19, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2005, 2, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2006, 20, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2007, 23, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2008, 24, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2009, 25, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2010, 26, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2011, 42, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2012, 44, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2013, 6, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2014, 7, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2015, 8, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2016, 9, 19, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2017, 1, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2018, 10, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2019, 13, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2020, 14, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2021, 16, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2022, 19, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2023, 2, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2024, 20, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2025, 23, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2026, 24, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2027, 25, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2028, 26, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2029, 42, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2030, 43, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2031, 5, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2032, 6, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2033, 8, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2034, 9, 20, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2035, 1, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2036, 10, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2037, 11, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2038, 13, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2039, 14, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2040, 16, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2041, 19, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2042, 2, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2043, 20, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2044, 23, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2045, 24, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2046, 26, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2047, 31, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2048, 43, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2049, 5, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2050, 6, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2051, 7, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2052, 8, 21, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2053, 1, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2054, 10, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2055, 13, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2056, 14, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2057, 16, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2058, 19, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2059, 2, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2060, 20, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2061, 23, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2062, 24, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2063, 25, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2064, 26, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2065, 31, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2066, 37, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2067, 42, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2068, 43, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2069, 6, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2070, 8, 22, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2071, 1, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2072, 10, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2073, 11, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2074, 13, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2075, 16, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2076, 19, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2077, 2, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2078, 20, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2079, 24, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2080, 25, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2081, 26, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2082, 31, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2083, 36, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2084, 37, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2085, 43, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2086, 5, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2087, 6, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2088, 8, 23, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2089, 1, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2090, 11, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2091, 13, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2092, 14, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2093, 16, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2094, 19, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2095, 2, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2096, 20, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2097, 24, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2098, 26, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2099, 31, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2100, 37, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2101, 43, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2102, 45, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2103, 5, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2104, 6, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2105, 7, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2106, 8, 24, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2107, 1, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2108, 10, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2109, 11, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2110, 13, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2111, 14, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2112, 16, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2113, 19, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2114, 2, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2115, 20, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2116, 24, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2117, 26, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2118, 29, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2119, 31, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2120, 42, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2121, 43, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2122, 44, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2123, 6, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2124, 8, 25, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2125, 1, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2126, 10, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2127, 13, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2128, 14, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2129, 16, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2130, 19, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2131, 2, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2132, 20, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2133, 24, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2134, 25, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2135, 26, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2136, 31, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2137, 43, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2138, 44, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2139, 5, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2140, 6, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2141, 7, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2142, 8, 26, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2143, 1, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2144, 13, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2145, 16, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2146, 19, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2147, 2, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2148, 20, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2149, 24, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2150, 26, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2151, 31, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2152, 42, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2153, 43, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2154, 44, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2155, 45, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2156, 46, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2157, 5, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2158, 6, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2159, 7, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2160, 8, 27, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2161, 1, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2162, 10, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2163, 11, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2164, 13, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2165, 14, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2166, 16, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2167, 19, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2168, 2, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2169, 26, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2170, 27, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2171, 29, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2172, 31, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2173, 36, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2174, 42, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2175, 43, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2176, 44, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2177, 5, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2178, 6, 28, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2179, 1, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2180, 10, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2181, 11, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2182, 13, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2183, 16, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2184, 19, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2185, 2, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2186, 24, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2187, 26, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2188, 27, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2189, 29, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2190, 31, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2191, 32, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2192, 36, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2193, 43, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2194, 5, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2195, 6, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2196, 7, 29, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2197, 1, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2198, 10, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2199, 11, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2200, 13, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2201, 16, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2202, 19, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2203, 2, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2204, 24, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2205, 25, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2206, 26, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2207, 29, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2208, 40, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2209, 44, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2210, 47, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2211, 48, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2212, 5, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2213, 6, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2214, 7, 93, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2215, 11, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2216, 13, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2217, 16, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2218, 19, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2219, 2, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2220, 20, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2221, 24, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2222, 25, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2223, 26, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2224, 29, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2225, 30, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2226, 31, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2227, 39, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2228, 44, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2229, 51, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2230, 7, 35, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2231, 11, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2232, 13, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2233, 16, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2234, 19, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2235, 2, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2236, 20, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2237, 23, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2238, 24, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2239, 25, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2240, 26, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2241, 29, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2242, 30, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2243, 31, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2244, 44, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2245, 49, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2246, 7, 36, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2247, 1, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2248, 10, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2249, 11, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2250, 13, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2251, 16, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2252, 19, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2253, 2, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2254, 23, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2255, 24, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2256, 25, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2257, 26, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2258, 29, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2259, 30, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2260, 31, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2261, 44, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2262, 49, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2263, 50, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2264, 7, 37, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2265, 1, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2266, 10, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2267, 11, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2268, 13, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2269, 16, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2270, 19, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2271, 2, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2272, 23, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2273, 24, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2274, 25, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2275, 26, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2276, 27, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2277, 29, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2278, 30, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2279, 31, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2280, 35, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2281, 44, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2282, 7, 39, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2283, 1, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2284, 10, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2285, 11, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2286, 13, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2287, 16, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2288, 19, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2289, 2, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2290, 23, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2291, 24, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2292, 25, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2293, 26, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2294, 29, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2295, 30, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2296, 31, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2297, 38, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2298, 44, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2299, 49, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2300, 7, 40, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2301, 1, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2302, 10, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2303, 11, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2304, 13, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2305, 16, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2306, 19, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2307, 2, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2308, 20, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2309, 23, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2310, 24, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2311, 25, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2312, 26, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2313, 30, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2314, 31, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2315, 34, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2316, 38, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2317, 44, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2318, 7, 41, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2319, 1, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2320, 10, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2321, 11, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2322, 13, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2323, 16, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2324, 19, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2325, 2, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2326, 20, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2327, 23, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2328, 24, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2329, 26, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2330, 30, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2331, 31, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2332, 35, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2333, 38, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2334, 41, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2335, 44, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2336, 7, 42, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2337, 1, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2338, 10, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2339, 11, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2340, 13, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2341, 16, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2342, 19, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2343, 2, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2344, 20, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2345, 23, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2346, 24, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2347, 26, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2348, 30, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2349, 34, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2350, 35, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2351, 41, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2352, 42, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2353, 44, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2354, 7, 43, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2355, 1, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2356, 10, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2357, 11, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2358, 13, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2359, 14, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2360, 16, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2361, 19, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2362, 2, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2363, 20, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2364, 23, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2365, 24, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2366, 26, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2367, 27, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2368, 30, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2369, 41, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2370, 42, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2371, 44, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2372, 7, 44, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2373, 1, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2374, 10, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2375, 11, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2376, 13, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2377, 14, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2378, 16, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2379, 19, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2380, 20, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2381, 23, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2382, 24, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2383, 26, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2384, 27, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2385, 30, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2386, 34, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2387, 41, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2388, 44, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2389, 55, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2390, 7, 45, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2391, 1, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2392, 10, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2393, 11, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2394, 13, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2395, 14, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2396, 16, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2397, 19, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2398, 20, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2399, 23, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2400, 24, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2401, 26, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2402, 27, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2403, 34, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2404, 35, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2405, 44, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2406, 54, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2407, 55, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2408, 7, 46, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2409, 1, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2410, 10, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2411, 11, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2412, 13, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2413, 14, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2414, 16, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2415, 19, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2416, 20, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2417, 24, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2418, 26, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2419, 27, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2420, 30, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2421, 34, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2422, 35, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2423, 44, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2424, 53, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2425, 55, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2426, 7, 47, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2427, 1, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2428, 10, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2429, 11, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2430, 13, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2431, 14, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2432, 19, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2433, 20, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2434, 23, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2435, 24, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2436, 26, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2437, 27, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2438, 29, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2439, 30, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2440, 32, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2441, 34, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2442, 35, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2443, 44, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2444, 7, 48, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2445, 1, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2446, 10, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2447, 11, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2448, 13, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2449, 14, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2450, 19, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2451, 2, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2452, 20, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2453, 24, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2454, 26, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2455, 27, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2456, 29, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2457, 30, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2458, 35, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2459, 39, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2460, 44, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2461, 53, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2462, 7, 50, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2463, 1, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2464, 10, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2465, 11, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2466, 13, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2467, 14, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2468, 16, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2469, 19, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2470, 2, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2471, 20, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2472, 24, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2473, 26, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2474, 27, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2475, 30, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2476, 31, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2477, 36, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2478, 39, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2479, 44, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2480, 7, 51, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2481, 1, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2482, 10, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2483, 11, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2484, 13, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2485, 14, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2486, 16, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2487, 17, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2488, 19, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2489, 2, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2490, 20, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2491, 24, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2492, 25, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2493, 26, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2494, 27, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2495, 30, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2496, 42, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2497, 44, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2498, 7, 52, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2499, 1, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2500, 10, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2501, 11, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2502, 13, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2503, 14, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2504, 16, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2505, 19, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2506, 2, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2507, 20, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2508, 24, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2509, 26, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2510, 27, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2511, 30, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2512, 31, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2513, 32, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2514, 44, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2515, 6, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2516, 7, 53, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2517, 1, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2518, 10, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2519, 11, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2520, 14, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2521, 16, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2522, 19, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2523, 2, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2524, 24, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2525, 25, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2526, 26, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2527, 27, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2528, 29, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2529, 31, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2530, 32, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2531, 42, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2532, 44, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2533, 6, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2534, 7, 54, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2535, 1, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2536, 10, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2537, 11, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2538, 13, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2539, 14, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2540, 16, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2541, 17, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2542, 2, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2543, 24, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2544, 25, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2545, 26, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2546, 27, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2547, 29, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2548, 30, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2549, 42, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2550, 44, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2551, 6, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2552, 7, 55, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2553, 1, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2554, 10, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2555, 11, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2556, 13, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2557, 14, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2558, 16, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2559, 19, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2560, 2, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2561, 20, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2562, 24, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2563, 25, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2564, 26, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2565, 27, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2566, 29, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2567, 30, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2568, 42, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2569, 6, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2570, 7, 56, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2571, 1, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2572, 10, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2573, 11, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2574, 13, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2575, 14, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2576, 16, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2577, 2, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2578, 24, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2579, 25, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2580, 26, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2581, 27, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2582, 30, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2583, 32, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2584, 33, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2585, 34, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2586, 42, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2587, 6, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2588, 7, 57, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2589, 1, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2590, 10, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2591, 11, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2592, 13, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2593, 14, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2594, 16, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2595, 19, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2596, 2, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2597, 24, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2598, 26, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2599, 27, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2600, 29, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2601, 30, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2602, 32, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2603, 33, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2604, 42, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2605, 6, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2606, 7, 58, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2607, 1, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2608, 10, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2609, 11, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2610, 13, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2611, 14, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2612, 16, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2613, 19, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2614, 2, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2615, 23, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2616, 24, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2617, 25, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2618, 26, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2619, 27, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2620, 32, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2621, 33, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2622, 39, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2623, 6, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2624, 7, 59, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2625, 1, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2626, 10, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2627, 11, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2628, 13, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2629, 14, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2630, 16, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2631, 19, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2632, 2, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2633, 24, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2634, 25, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2635, 26, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2636, 27, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2637, 32, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2638, 33, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2639, 52, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2640, 56, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2641, 6, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2642, 7, 60, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2643, 1, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2644, 10, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2645, 11, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2646, 13, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2647, 14, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2648, 16, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2649, 19, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2650, 2, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2651, 23, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2652, 24, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2653, 25, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2654, 26, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2655, 29, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2656, 32, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2657, 33, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2658, 56, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2659, 6, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2660, 7, 63, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2661, 1, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2662, 10, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2663, 11, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2664, 13, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2665, 14, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2666, 16, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2667, 2, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2668, 23, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2669, 24, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2670, 25, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2671, 26, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2672, 29, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2673, 32, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2674, 33, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2675, 36, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2676, 57, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2677, 6, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2678, 7, 64, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2679, 1, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2680, 10, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2681, 11, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2682, 13, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2683, 14, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2684, 16, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2685, 2, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2686, 24, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2687, 25, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2688, 26, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2689, 27, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2690, 29, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2691, 32, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2692, 33, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2693, 36, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2694, 56, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2695, 6, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2696, 7, 65, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2697, 1, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2698, 10, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2699, 11, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2700, 13, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2701, 14, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2702, 16, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2703, 2, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2704, 20, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2705, 24, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2706, 25, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2707, 26, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2708, 27, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2709, 29, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2710, 32, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2711, 36, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2712, 47, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2713, 6, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2714, 7, 66, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2715, 1, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2716, 10, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2717, 11, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2718, 13, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2719, 14, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2720, 16, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2721, 19, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2722, 2, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2723, 24, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2724, 25, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2725, 26, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2726, 27, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2727, 29, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2728, 40, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2729, 43, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2730, 44, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2731, 47, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2732, 57, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2733, 6, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2734, 7, 67, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2735, 1, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2736, 10, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2737, 11, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2738, 13, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2739, 14, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2740, 16, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2741, 19, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2742, 2, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2743, 24, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2744, 25, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2745, 26, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2746, 29, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2747, 32, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2748, 39, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2749, 40, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2750, 47, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2751, 6, 68, 1)
GO
INSERT [dbo].[VereineSaison] ([Id], [VereinNr], [SaisonID], [LigaID]) VALUES (2752, 7, 68, 1)
GO
SET IDENTITY_INSERT [dbo].[VereineSaison] OFF
GO
