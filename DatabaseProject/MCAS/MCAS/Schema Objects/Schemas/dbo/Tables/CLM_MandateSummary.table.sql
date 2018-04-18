CREATE TABLE [dbo].[CLM_MandateSummary](
	[MandateId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ReserveId] [int] NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimType] [int] NOT NULL,
	[MovementType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AssignedTo] [int] NULL,
	[InvestigationResult] [int] NULL,
	[Scenario] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Evidence] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RelatedFacts] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[COAssessment] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SupervisorAssignto] [int] NULL,
	[ApproveRecommedations] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SupervisorRemarks] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PreMandate] [numeric](18, 2) NULL,
	[MovementMandate] [numeric](18, 2) NULL,
	[CurrentMandate] [numeric](18, 2) NULL,
	[PaymentId] [int] NULL,
	[InformSafetytoreviewfindings] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MandateRecordNo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createdby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PreMandateSP] [numeric](18, 2) NULL,
	[MovementMandateSP] [numeric](18, 2) NULL,
	[CurrentMandateSP] [numeric](18, 2) NULL,
	[PreviousOffers] [numeric](18, 2) NULL,
	[TPCounterOffer] [numeric](18, 2) NULL,
	[DateofNoticetoSafety] [datetime] NULL,
	[InformedInsurer] [datetime] NULL,
	[EZLinkCardNo] [varchar](1) NULL,
	[ODStatus] [varchar](1) NULL,
	[RecoverableFromInsurerBI] [varchar](1) NULL,
 CONSTRAINT [PK_CLM_MandateSummary] PRIMARY KEY CLUSTERED 
(
	[MandateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_MandateSummary_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK_CLM_MandateSummary_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID]),
 CONSTRAINT [FK_CLM_MandateSummary_CLM_ReserveSummary] FOREIGN KEY([ReserveId])
REFERENCES [dbo].[CLM_ReserveSummary] ([ReserveId])
)


