IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Payment_CLM_Mandate]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Payment]'))
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [FK_CLM_Payment_CLM_Mandate]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Payment_CLM_Reserve]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Payment]'))
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [FK_CLM_Payment_CLM_Reserve]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Reserve_CLM_Payment]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Reserve]'))
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [FK_CLM_Reserve_CLM_Payment]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Mandate_CLM_Payment]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]'))
ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [FK_CLM_Mandate_CLM_Payment]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Clm_MandateTransactionHistory_CLM_Payment]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clm_MandateTransactionHistory]'))
ALTER TABLE [dbo].[Clm_MandateTransactionHistory] DROP CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Payment]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_Finalize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_Finalize]
END


ALTER TABLE [dbo].[CLM_Payment]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Payment_CLM_Mandate] FOREIGN KEY([MandateId])
REFERENCES [dbo].[CLM_Mandate] ([MandateId])
GO

ALTER TABLE [dbo].[CLM_Payment]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Payment_CLM_Reserve] FOREIGN KEY([ReserveId])
REFERENCES [dbo].[CLM_Reserve] ([ReserveId])
GO

ALTER TABLE [dbo].[CLM_Reserve]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Reserve_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
GO

ALTER TABLE [dbo].[CLM_Mandate]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Mandate_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_Finalize]  DEFAULT ('N') FOR [Finalize]
Go