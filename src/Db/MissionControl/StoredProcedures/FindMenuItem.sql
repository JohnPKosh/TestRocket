CREATE PROCEDURE [dbo].[FindMenuItem]
  @CommandName NVARCHAR(30),
  @Name NVARCHAR(30)
AS
SELECT [Id]
  ,[LaunchCenterId]
  ,[FullName]
  ,[SKU]
  ,[Name]
FROM [dbo].[LaunchCenterTakeoutMenu]
WHERE [CommandName] LIKE @CommandName
AND [Name] = @Name
