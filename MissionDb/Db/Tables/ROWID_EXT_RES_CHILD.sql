﻿CREATE TABLE [dbo].[ROWID_EXT_RES_CHILD]
(
  [GDID] [binary](12) NOT NULL,
  [ROWID] [bigint] NOT NULL,
  [OWNER] SMALLINT NOT NULL,
  [EXTERNALID] NVARCHAR(36) NOT NULL,
  CONSTRAINT [PK_ROWID_EXT_RES_CHILD] PRIMARY KEY NONCLUSTERED
  (
    [OWNER] ASC, [EXTERNALID] ASC
  )
)