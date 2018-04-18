IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Mandate_MandateType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [DF_CLM_Mandate_MandateType]
END
ALTER TABLE [dbo].[CLM_Mandate] ADD  CONSTRAINT [DF_CLM_Mandate_MandateType]  DEFAULT ((0)) FOR [MandateType]
