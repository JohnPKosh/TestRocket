CREATE VIEW [dbo].[LaunchCenterTakeoutMenu]
  AS
SELECT a.[CommandName]
      ,a.[FullName]
      ,a.[Elevation]
      ,a.[Lat]
      ,a.[Long]
	    ,b.[Id]
      ,b.[LaunchCenterId]
      ,b.[SKU]
      ,b.[Name]
FROM [dbo].[LaunchCenter] as a
INNER JOIN [dbo].[TakeoutMenu] as b
ON a.[Id] = b.[LaunchCenterId]
