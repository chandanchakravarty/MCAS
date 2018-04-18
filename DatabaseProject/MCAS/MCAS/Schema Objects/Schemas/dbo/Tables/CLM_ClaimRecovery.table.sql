CREATE TABLE [dbo].[CLM_ClaimRecovery](
	[RecoveryId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[ClaimentId] [int] NOT NULL,
	[RecoverFrom] [varchar](100) NOT NULL,
	[RecoveryReason] [nvarchar](max) NULL,
	[LeagalLawyerCost_R] [decimal](18, 2) NULL,
	[TotalAmt_R] [decimal](18, 2) NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[Modifiedby] [nvarchar](100) NULL,
	[ClaimantName] [nvarchar](max) NULL,
	[ClaimType] [int] NULL,
	[PaymentId] [int] NULL,
	[TPBIPayout] [decimal](18, 2) NULL,
	[Deductible] [decimal](18, 2) NULL,
	[RecoverAmt] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[NetAmtRecovered] [decimal](18, 2) NULL,
	[MandateId] [int] NULL,
 CONSTRAINT [PK_CLM_ClaimRecovery] PRIMARY KEY CLUSTERED 
(
	[RecoveryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_ClaimRecovery_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK_CLM_ClaimRecovery_CLM_Claims] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[CLM_Claims] ([ClaimID]),
CONSTRAINT [FK_CLM_ClaimRecovery_CLM_MandateSummary] FOREIGN KEY([MandateId])
REFERENCES [dbo].[CLM_MandateSummary] ([MandateId]),
 CONSTRAINT [FK_CLM_ClaimRecovery_CLM_PaymentSummary] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_PaymentSummary] ([PaymentId])
)


