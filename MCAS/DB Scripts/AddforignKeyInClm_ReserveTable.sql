IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Reserve_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Reserve]'))
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [FK_CLM_Reserve_ClaimAccidentDetails]
GO



ALTER TABLE [dbo].[CLM_Reserve]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Reserve_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO




IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Reserve_CLM_Claim]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Reserve]'))
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [FK_CLM_Reserve_CLM_Claim]
GO

ALTER TABLE [dbo].[CLM_Reserve]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Reserve_CLM_Claim] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claim] ([ClaimID])
GO



IF EXISTS (SELECT
    *
  FROM sys.foreign_keys
  WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Mandate_ClaimAccidentDetails]')
  AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]'))
BEGIN
  ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [FK_CLM_Mandate_ClaimAccidentDetails]
END


ALTER TABLE [dbo].[CLM_Mandate] WITH CHECK ADD CONSTRAINT [FK_CLM_Mandate_ClaimAccidentDetails] FOREIGN KEY ([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])



IF EXISTS (SELECT
    *
  FROM sys.foreign_keys
  WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Mandate_CLM_Claim]')
  AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]'))
BEGIN
  ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [FK_CLM_Mandate_CLM_Claim]
END

ALTER TABLE [dbo].[CLM_Mandate] WITH CHECK ADD CONSTRAINT [FK_CLM_Mandate_CLM_Claim] FOREIGN KEY ([ClaimID])
REFERENCES [dbo].[CLM_Claim] ([ClaimID])