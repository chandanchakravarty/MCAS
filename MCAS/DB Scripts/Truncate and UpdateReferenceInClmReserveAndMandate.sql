
truncate table CLM_Mandate

truncate table clm_reserve

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Reserve_CLM_Claim]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Reserve]'))
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [FK_CLM_Reserve_CLM_Claim]
GO

ALTER TABLE [dbo].[CLM_Reserve]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Reserve_CLM_Claim] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Mandate_CLM_Claim]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]'))
ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [FK_CLM_Mandate_CLM_Claim]
GO


ALTER TABLE [dbo].[CLM_Mandate]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Mandate_CLM_Claim] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Claims_CaseStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Claims] DROP CONSTRAINT [DF_CLM_Claims_CaseStatus]
END

ALTER TABLE [dbo].[CLM_Claims] ADD  CONSTRAINT [DF_CLM_Claims_CaseStatus]  DEFAULT ((1)) FOR [CaseStatus]


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Claims_ClaimStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Claims] DROP CONSTRAINT [DF_CLM_Claims_ClaimStatus]
END

ALTER TABLE [dbo].[CLM_Claims] ADD  CONSTRAINT [DF_CLM_Claims_ClaimStatus]  DEFAULT ((1)) FOR [ClaimStatus]


update CLM_Claims set ClaimStatus=1 where ClaimStatus is null