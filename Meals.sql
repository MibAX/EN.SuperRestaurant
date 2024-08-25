USE [EN.SuperRestaurant]
GO
SET IDENTITY_INSERT [dbo].[Meals] ON 
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (3, N'Pizza', N'Large Pizza', CAST(14.28 AS Decimal(4, 2)), N'2.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (4, N'Shawerma Meal', N'Extra Large Shawerma', CAST(13.16 AS Decimal(4, 2)), N'1.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (6, N'Zinger', N'Mighty Zinger', CAST(10.36 AS Decimal(4, 2)), N'4.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (7, N'Chicken Burger', N'The 2nd best burger in the World', CAST(8.54 AS Decimal(4, 2)), N'3.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (9, N'Chicken Tenders', N'Chicken Tinder ', CAST(11.20 AS Decimal(4, 2)), N'5.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (10, N'Wedges', N'Wedges are good', CAST(1.96 AS Decimal(4, 2)), N'6.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (11, N'Fries', N'Jordanian French Fries ', CAST(1.47 AS Decimal(4, 2)), N'7.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (12, N'Chicken Nuggets', N'Chicken Nuggets for our brothers', CAST(8.61 AS Decimal(4, 2)), N'8.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (13, N'Beef Burger', N'Beef Burger the master of all meals', CAST(2.80 AS Decimal(4, 2)), N'9.png')
GO
INSERT [dbo].[Meals] ([Id], [Name], [Description], [Price], [ImageName]) VALUES (14, N'Shawerma Sandwitch', N'Shawerma Sandwitch is very good', CAST(19.32 AS Decimal(4, 2)), N'10.png')
GO
SET IDENTITY_INSERT [dbo].[Meals] OFF
GO
