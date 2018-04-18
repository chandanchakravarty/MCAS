CREATE TYPE [dbo].[CLM_PaymentDetails] AS TABLE(
	[CmpCode] [nchar](15) NOT NULL,
	[TotalPaymentDue] [numeric](18, 2) NULL,
	[TotalAmountMandate] [numeric](18, 2) NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ReserveId] [int] NOT NULL,
	[MandateId] [int] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[ClaimID] [int] NOT NULL
)
GO


