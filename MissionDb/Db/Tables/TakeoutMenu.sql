CREATE TABLE [dbo].[TakeoutMenu]
(
  [Id] INT IDENTITY(10000,1) NOT NULL,
  [LaunchCenterId] [int] NOT NULL,
  [SKU] VARCHAR(36) NOT NULL,
  [Name] NVARCHAR(30)
  CONSTRAINT [PK_TakeoutMenu] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
)
