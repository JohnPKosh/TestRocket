﻿CREATE TABLE [dbo].[LaunchCenter]
(
  [Id] INT IDENTITY(100,1) NOT NULL,
  [CommandName] NVARCHAR(30) NOT NULL,
  [FullName] NVARCHAR(100) NOT NULL,
  [Elevation] DECIMAL(8,1) NOT NULL,
  [Lat] DECIMAL(18,7) NOT NULL,
  [Long] DECIMAL(18,7) NOT NULL,
  CONSTRAINT [PK_ProcessControl] PRIMARY KEY CLUSTERED
  (
    [Id] ASC
  )
)
