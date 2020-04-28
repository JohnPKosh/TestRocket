ALTER TABLE [dbo].[TakeoutMenu]
  ADD CONSTRAINT [FK_LaunchCenter_TakeoutMenu]
  FOREIGN KEY ([LaunchCenterId])
  REFERENCES [dbo].[LaunchCenter] ([Id])
  ON UPDATE CASCADE
  ON DELETE CASCADE