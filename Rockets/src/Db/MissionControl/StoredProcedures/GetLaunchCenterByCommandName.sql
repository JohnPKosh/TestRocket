CREATE PROCEDURE [dbo].[GetLaunchCenterByCommandName]
  @CommandName NVARCHAR(30)
AS
SELECT [Id]
      ,[CommandName]
      ,[FullName]
      ,[Elevation]
      ,[Lat]
      ,[Long]
FROM [dbo].[LaunchCenter]
WHERE [CommandName] LIKE @CommandName
