IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Claim_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Claim]'))
ALTER TABLE [dbo].[CLM_Claim] DROP CONSTRAINT [FK_CLM_Claim_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_Claim]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_Claim]
GO

CREATE TABLE [dbo].[CLM_Claim](
	[ClaimID] [int] IDENTITY(1,1) NOT NULL,
	[ClaimantName] [nvarchar](250) NOT NULL,
	[ClaimantNRICPPNO] [nvarchar](250) NOT NULL,
	[ClaimantDOB] [datetime] NULL,
	[ClaimantGender] [nvarchar](10) NULL,
	[ClaimantType] [int] NOT NULL,
	[ClaimantAddress1] [nvarchar](250) NOT NULL,
	[ClaimantAddress2] [nvarchar](250) NOT NULL,
	[ClaimantAddress3] [nvarchar](250) NOT NULL,
	[PostalCode] [nvarchar](250) NOT NULL,
	[ClaimantContactNo] [nvarchar](20) NOT NULL,
	[ClaimantEmail] [nvarchar](100) NULL,
	[VehicleRegnNo] [nvarchar](8) NOT NULL,
	[VehicleMake] [nvarchar](150) NOT NULL,
	[VehicleModel] [nvarchar](150) NOT NULL,
	[Isclaimantaninfant] [nvarchar](5) NULL,
	[InfantName] [nvarchar](250) NULL,
	[InfantDOB] [datetime] NULL,
	[InfantGender] [nvarchar](10) NULL,
	[ClaimType] [int] NOT NULL,
	[ClaimRecordNo] [nvarchar](50) NOT NULL,
	[TimeBarDate] [datetime] NOT NULL,
	[ClaimDate] [datetime] NOT NULL,
	[ClaimsOfficer] [int] NOT NULL,
	[DriverLiablity] [nvarchar](250) NULL,
	[AccidentCause] [nvarchar](100) NOT NULL,
	[CaseCategory] [nvarchar](100) NOT NULL,
	[CaseStatus] [nvarchar](100) NOT NULL,
	[ClaimantStatus] [nvarchar](10) NOT NULL,
	[FinalSettleDate] [datetime] NOT NULL,
	[WritIssuedDate] [datetime] NULL,
	[WritNo] [nvarchar](50) NULL,
	[SensitiveCase] [datetime] NULL,
	[ReopenedDate] [datetime] NULL,
	[RecordReopenedReason] [nvarchar](150) NULL,
	[RecordCancellationDate] [datetime] NULL,
	[RecordCancellationReason] [nvarchar](150) NULL,
	[Createdby] [nvarchar](250) NOT NULL,
	[Createddate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](250) NULL,
	[Modifieddate] [datetime] NULL,
	[AccidentId] [int] NULL,
	[PolicyId] [int] NULL,
	[IsActive] [nvarchar](1) NOT NULL,
	[MP] [nvarchar](250) NULL,
	[Constituency] [nvarchar](250) NULL,
	[DateOfMpLetter] [datetime] NULL,
	[SeverityReferenceNo] [nvarchar](50) NULL,
	[ReportSentToInsurer] [datetime] NULL,
	[ReferredToInsurers] [nvarchar](250) NULL,
	[InformedInsurerOfSettlement] [nvarchar](250) NULL,
	[ExcessRecoveredDate] [datetime] NULL,
	[ReferredToInsurersB] [bit] NOT NULL,
	[DateReferredToInsurersB] [datetime] NULL,
 CONSTRAINT [PK_CLM_Claim] PRIMARY KEY CLUSTERED 
(
	[ClaimID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CLM_Claim]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Claim_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_Claim] CHECK CONSTRAINT [FK_CLM_Claim_ClaimAccidentDetails]
GO


