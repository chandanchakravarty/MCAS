IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Mandate_MovementType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [DF_CLM_Mandate_MovementType]
END


ALTER TABLE [dbo].[CLM_Mandate] ADD  CONSTRAINT [DF_CLM_Mandate_MovementType]  DEFAULT ('I') FOR [MovementType]