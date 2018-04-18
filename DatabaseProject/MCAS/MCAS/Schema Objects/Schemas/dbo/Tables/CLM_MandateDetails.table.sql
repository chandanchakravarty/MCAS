CREATE TABLE [dbo].[CLM_MandateDetails](
	[MandateDetailId] [int] IDENTITY(1,1) NOT NULL,
	[MandateId] [int] NOT NULL,
	[CmpCode] [nchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PreMandate] [numeric](18, 2) NULL,
	[MovementMandate] [numeric](18, 2) NULL,
	[CurrentMandate] [numeric](18, 2) NULL,
	[PaymentId] [int] NULL,
	[Createdby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentClaimId] [int] NULL,
	[ClaimID] [int] NULL,
	[PreMandateSP] [numeric](18, 2) NULL,
	[MovementMandateSP] [numeric](18, 2) NULL,
	[CurrentMandateSP] [numeric](18, 2) NULL,
	[PreviousOffers] [numeric](18, 2) NULL,
	[TPCounterOffer] [numeric](18, 2) NULL,
	[ReserveId] [int] NULL,
	[MovementType] [char](1) NULL,
 CONSTRAINT [PK_CLM_MandateDetails] PRIMARY KEY CLUSTERED 
(
	[MandateDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_MandateDetails_CLM_MandateDetails] FOREIGN KEY([MandateId])
REFERENCES [dbo].[CLM_MandateSummary] ([MandateId]),
CONSTRAINT [FK_CLM_MandateDetails_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
CONSTRAINT [FK_CLM_MandateDetails_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
)


