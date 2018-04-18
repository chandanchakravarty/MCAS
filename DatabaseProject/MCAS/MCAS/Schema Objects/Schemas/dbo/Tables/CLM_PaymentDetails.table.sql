CREATE TABLE [dbo].[CLM_PaymentDetails](
	[PaymentDetailID] [int] IDENTITY(1,1) NOT NULL,
	[CmpCode] [nchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalPaymentDue] [numeric](18, 2) NULL,
	[TotalAmountMandate] [numeric](18, 2) NULL,
	[Createdby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ReserveId] [int] NOT NULL,
	[MandateId] [int] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
 CONSTRAINT [PK_CLM_PaymentDetails] PRIMARY KEY CLUSTERED 
(
	[PaymentDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_PaymentDetails_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK_CLM_PaymentDetails_CLM_MandateSummary] FOREIGN KEY([MandateId])
REFERENCES [dbo].[CLM_MandateSummary] ([MandateId]),
 CONSTRAINT [FK_CLM_PaymentDetails_CLM_PaymentSummary] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_PaymentSummary] ([PaymentId]),
 CONSTRAINT [FK_CLM_PaymentDetails_CLM_ReserveSummary] FOREIGN KEY([ReserveId])
REFERENCES [dbo].[CLM_ReserveSummary] ([ReserveId])
)


