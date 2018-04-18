--test
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_MovementType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_MovementType]
END

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Payment_MovementType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [DF_CLM_Payment_MovementType]
END

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_MovementType]  DEFAULT ('I') FOR [MovementType]

ALTER TABLE [dbo].[CLM_Payment] ADD  CONSTRAINT [DF_CLM_Payment_MovementType]  DEFAULT ('I') FOR [MovementType]