update CLM_Payment set ApprovePayment='N' where ApprovePayment is null

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Payment_ApprovePayment]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [DF_CLM_Payment_ApprovePayment]
END

ALTER TABLE [dbo].[CLM_Payment] ADD  CONSTRAINT [DF_CLM_Payment_ApprovePayment]  DEFAULT ('N') FOR [ApprovePayment]