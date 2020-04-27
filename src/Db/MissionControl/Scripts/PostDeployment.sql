/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[LaunchCenter] ON
INSERT INTO [dbo].[LaunchCenter] ([Id], [CommandName], [FullName], [Elevation], [Lat], [Long]) VALUES (100, N'Cape', N'Cape Canaveral', CAST(3.2 AS Decimal(8, 1)), CAST(28.3882506 AS Decimal(18, 7)), CAST(-80.6155172 AS Decimal(18, 7)))
INSERT INTO [dbo].[LaunchCenter] ([Id], [CommandName], [FullName], [Elevation], [Lat], [Long]) VALUES (101, N'Houston', N'Johnson Space Center', CAST(105.4 AS Decimal(8, 1)), CAST(29.5593497 AS Decimal(18, 7)), CAST(-95.0921867 AS Decimal(18, 7)))
INSERT INTO [dbo].[LaunchCenter] ([Id], [CommandName], [FullName], [Elevation], [Lat], [Long]) VALUES (102, N'Backyard', N'Impomptu Flight Center', CAST(1125.2 AS Decimal(8, 1)), CAST(41.4657103 AS Decimal(18, 7)), CAST(-81.0933740 AS Decimal(18, 7)))
INSERT INTO [dbo].[LaunchCenter] ([Id], [CommandName], [FullName], [Elevation], [Lat], [Long]) VALUES (103, N'Zvyozdny gorodok', N'Closed Military Townlet No. 1', CAST(16380.0 AS Decimal(8, 1)), CAST(55.8785287 AS Decimal(18, 7)), CAST(38.1016783 AS Decimal(18, 7)))
SET IDENTITY_INSERT [dbo].[LaunchCenter] OFF


SET IDENTITY_INSERT [dbo].[TakeoutMenu] ON
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10000, 100, N'1DD48DEE-173F-4791-B460-DF0E10729B8F', N'Shrimp Salad')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10001, 100, N'D74BFC19-F113-4E2B-BC62-42CC241FCB7F', N'Corn on the Cobb')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10002, 100, N'C8BBDBB3-7DB0-4B48-BD2D-431FEF0B427F', N'Key Lime Pie')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10003, 101, N'C95AD0B4-00BE-46CB-96F1-5EC31F1FB61C', N'Southwest Burrito')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10004, 101, N'44C7E0CC-8E75-496B-AA46-35DE99832C7A', N'Barbecue Ribs')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10006, 101, N'86A75D13-1DF2-4A87-AB9E-770B02D26DDA', N'Jelly Donut')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10007, 102, N'B424B762-F339-42EF-9D80-9556B631C6CC', N'Bacon Cheesburger')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10008, 102, N'C4C8FBA3-F11A-4AA6-AD6E-2CC8C28570D6', N'Potato Salad')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10009, 102, N'B9A5BC0C-BB36-451E-B640-A6BC2C9E3FEB', N'Apple Pie')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10010, 103, N'DB035CDC-2DB8-4A5F-8F78-0C9B74A93536', N'Pizza')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10011, 103, N'7BFF6D37-437A-4258-B04C-E34A778B7F49', N'Cheese Fries')
INSERT INTO [dbo].[TakeoutMenu] ([Id], [LaunchCenterId], [SKU], [Name]) VALUES (10013, 103, N'20D2FF19-3FB6-45F0-8E61-0A18619D8D7D', N'Caviar')
SET IDENTITY_INSERT [dbo].[TakeoutMenu] OFF