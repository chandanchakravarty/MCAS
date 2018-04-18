CREATE TYPE [dbo].[TVP_PaymentDetails] AS TABLE(
	[PaymentDetailID] [int] NOT NULL,
	[CmpCode] [nchar](15) NOT NULL,
	[TotalPaymentDue] [numeric](18, 2) NULL,
	[TotalAmountMandate] [numeric](18, 2) NULL,
	[Createdby] [nvarchar](max) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ReserveId] [int] NOT NULL,
	[MandateId] [int] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL
)