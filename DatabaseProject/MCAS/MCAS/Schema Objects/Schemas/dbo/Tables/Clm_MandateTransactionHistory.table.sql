CREATE TABLE [dbo].[Clm_MandateTransactionHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MandateId] [int] NOT NULL,
	[ComponentID] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Initial] [decimal](18, 2) NULL,
	[ReserveMovement] [decimal](18, 2) NULL,
	[Outstanding] [decimal](18, 2) NULL,
	[PaymentId] [int] NULL,
	[AccidentClaimId] [int] NULL,
	[ClaimID] [int] NULL,
 CONSTRAINT [PK_Clm_MandateTransactionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_Clm_MandateTransactionHistory_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID]),
 CONSTRAINT [FK_Clm_MandateTransactionHistory_Clm_MandateTransactionHistory] FOREIGN KEY([Id])
REFERENCES [dbo].[Clm_MandateTransactionHistory] ([Id]),
 CONSTRAINT [FK_Clm_MandateTransactionHistory_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
)


