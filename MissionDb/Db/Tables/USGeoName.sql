CREATE TABLE [dbo].[USGeoName](
	[PostalCode] [int] NOT NULL,
	[PlaceName] [varchar](180) NULL,
	[AdminName1] [varchar](100) NULL,
	[AdminCode1] [varchar](20) NULL,
	[AdminName2] [varchar](100) NULL,
	[AdminCode2] [varchar](20) NULL,
	[Latitude] [decimal](9, 4) NOT NULL,
	[Longitude] [decimal](9, 4) NOT NULL,
	[Accuracy] [tinyint] NULL
) 