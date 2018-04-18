IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_ClaimRecovery_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_ClaimRecovery]'))
ALTER TABLE [dbo].[CLM_ClaimRecovery] DROP CONSTRAINT [FK_CLM_ClaimRecovery_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_ClaimRecovery_CLM_Claims]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_ClaimRecovery]'))
ALTER TABLE [dbo].[CLM_ClaimRecovery] DROP CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Claims]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_ClaimRecovery_CLM_Payment]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_ClaimRecovery]'))
ALTER TABLE [dbo].[CLM_ClaimRecovery] DROP CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Payment]
GO


/****** Object:  Table [dbo].[CLM_ClaimRecovery]    Script Date: 02/27/2015 10:19:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_ClaimRecovery]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_ClaimRecovery]
GO


/****** Object:  Table [dbo].[CLM_ClaimRecovery]    Script Date: 02/27/2015 10:19:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLM_ClaimRecovery](
	[RecoveryId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[ClaimentId] [int] NOT NULL,
	[RecoverFrom] [varchar](100) NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NULL,
	[Address3] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[RecoveryReason] [nvarchar](max) NULL,
	[CostofRepairs] [decimal](18, 2) NULL,
	[LossofUse] [decimal](18, 2) NULL,
	[OtherExpences] [decimal](18, 2) NULL,
	[ReportServeyFee] [decimal](18, 2) NULL,
	[ReportReserveyFee] [decimal](18, 2) NULL,
	[ReportLTA_GIA_PolicyFee] [decimal](18, 2) NULL,
	[TPLawyerCost] [decimal](18, 2) NULL,
	[TPLawyerDisbursment] [decimal](18, 2) NULL,
	[LeagalLawyerCost] [decimal](18, 2) NULL,
	[legalLawyerDisbursement] [decimal](18, 2) NULL,
	[TotalAmt] [decimal](18, 2) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[Modifiedby] [nvarchar](100) NULL,
	[ClaimantName] [nvarchar](max) NULL,
	[ClaimType] [int] NULL,
	[PaymentId] [int] NULL,
	[TPBIPayout] [decimal](18, 2) NULL,
	[Deductible] [decimal](18, 2) NULL,
	[RecoverAmt] [decimal](18, 2) NULL,
 CONSTRAINT [PK_CLM_ClaimRecovery] PRIMARY KEY CLUSTERED 
(
	[RecoveryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery]  WITH CHECK ADD  CONSTRAINT [FK_CLM_ClaimRecovery_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery] CHECK CONSTRAINT [FK_CLM_ClaimRecovery_ClaimAccidentDetails]
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery]  WITH CHECK ADD  CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Claims] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery] CHECK CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Claims]
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery]  WITH CHECK ADD  CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery] CHECK CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Payment]
GO


