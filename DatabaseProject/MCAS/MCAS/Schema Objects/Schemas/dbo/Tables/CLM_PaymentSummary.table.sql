CREATE TABLE [dbo].[CLM_PaymentSummary](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[Payee] [nvarchar](max) NULL,
	[AssignedToSupervisor] [nvarchar](max) NULL,
	[TotalPaymentDue] [numeric](18, 2) NULL,
	[TotalAmountMandate] [numeric](18, 2) NULL,
	[Createddate] [datetime] NULL,
	[Modifieddate] [datetime] NULL,
	[PaymentRequestDate] [datetime] NULL,
	[PaymentDueDate] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[Modifiedby] [nvarchar](100) NULL,
	[AssignedTo] [nvarchar](max) NULL,
	[ClaimantName] [nvarchar](max) NULL,
	[PaymentRecordNo] [nvarchar](max) NULL,
	[ClaimType] [int] NULL,
	[IsActive] [char](1) NOT NULL,
	[ClaimID] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[PostalCodes] [nvarchar](max) NULL,
	[CoRemarks] [nvarchar](max) NULL,
	[ApprovePayment] [nvarchar](max) NULL,
	[SupervisorRemarks] [nvarchar](max) NULL,
	[ApprovedDate] [datetime] NULL,
	[MovementType] [nvarchar](50) NULL,
	[MandateId] [int] NULL,
	[ReserveId] [int] NULL,
	[MandateRecord] [nvarchar](max) NULL,
	[DateofNoticetoSafety] [datetime] NULL,
	[InformSafetytoreviewfindings] [nvarchar](10) NULL,
	[EZLinkCardNo] [varchar](1) NULL,
	[ODStatus] [varchar](1) NULL,
	[RecoverableFromInsurerBI] [varchar](1) NULL,
  CONSTRAINT [PK_CLM_PaymentSummary] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_PaymentSummary_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK_CLM_PaymentSummary_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID]),
 CONSTRAINT [FK_CLM_PaymentSummary_CLM_MandateSummary] FOREIGN KEY([MandateId])
REFERENCES [dbo].[CLM_MandateSummary] ([MandateId]),
 CONSTRAINT [FK_CLM_PaymentSummary_CLM_ReserveSummary] FOREIGN KEY([ReserveId])
REFERENCES [dbo].[CLM_ReserveSummary] ([ReserveId])
)