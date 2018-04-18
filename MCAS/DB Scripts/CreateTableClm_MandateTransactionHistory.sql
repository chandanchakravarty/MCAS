IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Clm_MandateTransactionHistory_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clm_MandateTransactionHistory]'))
ALTER TABLE [dbo].[Clm_MandateTransactionHistory] DROP CONSTRAINT [FK_Clm_MandateTransactionHistory_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Clm_MandateTransactionHistory_CLM_Claims]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clm_MandateTransactionHistory]'))
ALTER TABLE [dbo].[Clm_MandateTransactionHistory] DROP CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Claims]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Clm_MandateTransactionHistory_Clm_MandateTransactionHistory]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clm_MandateTransactionHistory]'))
ALTER TABLE [dbo].[Clm_MandateTransactionHistory] DROP CONSTRAINT [FK_Clm_MandateTransactionHistory_Clm_MandateTransactionHistory]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Clm_MandateTransactionHistory_CLM_Payment]') AND parent_object_id = OBJECT_ID(N'[dbo].[Clm_MandateTransactionHistory]'))
ALTER TABLE [dbo].[Clm_MandateTransactionHistory] DROP CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Payment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clm_MandateTransactionHistory]') AND type in (N'U'))
DROP TABLE [dbo].[Clm_MandateTransactionHistory]
GO

CREATE TABLE [dbo].[Clm_MandateTransactionHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MandateId] [int] NOT NULL,
	[ComponentID] [nchar](10) NOT NULL,
	[Initial] [decimal](18, 2) NULL,
	[ReserveMovement] [decimal](18, 2) NULL,
	[Outstanding] [decimal](18, 2) NULL,
	[PaymentId] [int] NULL,
	[AccidentClaimId] [int] NULL,
	[ClaimID] [int] NULL,
 CONSTRAINT [PK_Clm_MandateTransactionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_Clm_MandateTransactionHistory_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory] CHECK CONSTRAINT [FK_Clm_MandateTransactionHistory_ClaimAccidentDetails]
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory] CHECK CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Claims]
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_Clm_MandateTransactionHistory_Clm_MandateTransactionHistory] FOREIGN KEY([Id])
REFERENCES [dbo].[Clm_MandateTransactionHistory] ([Id])
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory] CHECK CONSTRAINT [FK_Clm_MandateTransactionHistory_Clm_MandateTransactionHistory]
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
GO

ALTER TABLE [dbo].[Clm_MandateTransactionHistory] CHECK CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Payment]
GO


