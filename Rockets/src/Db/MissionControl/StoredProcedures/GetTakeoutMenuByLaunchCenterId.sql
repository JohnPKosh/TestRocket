CREATE PROCEDURE [dbo].[GetTakeoutMenuByLaunchCenterId]
  @LaunchCenterId int
AS
SELECT [Id]
      ,[LaunchCenterId]
      ,[SKU]
      ,[Name]
FROM [dbo].[TakeoutMenu]
WHERE [LaunchCenterId] = @LaunchCenterId
