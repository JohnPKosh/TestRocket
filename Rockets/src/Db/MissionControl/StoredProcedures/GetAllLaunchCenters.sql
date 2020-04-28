CREATE PROCEDURE [dbo].[GetAllLaunchCenters]
AS
SELECT [Id]
      ,[CommandName]
      ,[FullName]
      ,[Elevation]
      ,[Lat]
      ,[Long]
FROM [dbo].[LaunchCenter]
