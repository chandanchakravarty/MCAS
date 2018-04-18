IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Payment_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Payment]'))
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [FK_CLM_Payment_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Payment_CLM_Claims]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Payment]'))
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [FK_CLM_Payment_CLM_Claims]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Payment_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [DF_CLM_Payment_IsActive]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Payment_ApprovePayment]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [DF_CLM_Payment_ApprovePayment]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Payment_MovementType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Payment] DROP CONSTRAINT [DF_CLM_Payment_MovementType]
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_Payment]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_Payment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLM_Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[Payee] [int] NULL,
	[AssignedToSupervisor] [nvarchar](max) NULL,
	[LossofEarning_D] [decimal](18, 2) NULL,
	[LossofEarning_S] [decimal](18, 2) NULL,
	[CostofRepairs_D] [decimal](18, 2) NULL,
	[CostofRepairs_S] [decimal](18, 2) NULL,
	[LossofUse_D] [decimal](18, 2) NULL,
	[LossofUse_S] [decimal](18, 2) NULL,
	[LossofUseUn_D] [decimal](18, 2) NULL,
	[LossofUseUn_S] [decimal](18, 2) NULL,
	[LossofRental_D] [decimal](18, 2) NULL,
	[LossofRental_S] [decimal](18, 2) NULL,
	[Excess_D] [decimal](18, 2) NULL,
	[Excess_S] [decimal](18, 2) NULL,
	[OtherExpenses_D] [decimal](18, 2) NULL,
	[OtherExpenses_S] [decimal](18, 2) NULL,
	[ReportFees_D] [decimal](18, 2) NULL,
	[ReportFees_S] [decimal](18, 2) NULL,
	[SurveyFee_D] [decimal](18, 2) NULL,
	[SurveyFee_S] [decimal](18, 2) NULL,
	[ReSurveyFee_D] [decimal](18, 2) NULL,
	[ReSurveyFee_S] [decimal](18, 2) NULL,
	[LGPolRepFee_D] [decimal](18, 2) NULL,
	[LGPolRepFee_S] [decimal](18, 2) NULL,
	[ParLawCost3rd_D] [decimal](18, 2) NULL,
	[ParLawCost3rd_S] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_D] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_S] [decimal](18, 2) NULL,
	[OurLawyerCost_D] [decimal](18, 2) NULL,
	[OurLawyerCost_S] [decimal](18, 2) NULL,
	[OurLawDisbursements_D] [decimal](18, 2) NULL,
	[OurLawDisbursements_S] [decimal](18, 2) NULL,
	[GeneralDamages_D] [decimal](18, 2) NULL,
	[GeneralDamages_S] [decimal](18, 2) NULL,
	[MedicalExpenses_D] [decimal](18, 2) NULL,
	[MedicalExpenses_S] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_D] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_S] [decimal](18, 2) NULL,
	[LOGMedicalExpenses_D] [decimal](18, 2) NULL,
	[LOGMedicalExpenses_S] [decimal](18, 2) NULL,
	[LossofEarningsCapacity_D] [decimal](18, 2) NULL,
	[LossofEarningsCapacity_S] [decimal](18, 2) NULL,
	[LossofEarnings_D] [decimal](18, 2) NULL,
	[LossofEarnings_S] [decimal](18, 2) NULL,
	[LossofFutureEarnings_D] [decimal](18, 2) NULL,
	[LossofFutureEarnings_S] [decimal](18, 2) NULL,
	[Transport_D] [decimal](18, 2) NULL,
	[Transport_S] [decimal](18, 2) NULL,
	[MedicalRecord_D] [decimal](18, 2) NULL,
	[MedicalRecord_S] [decimal](18, 2) NULL,
	[PublicTrusteeFee_D] [decimal](18, 2) NULL,
	[PublicTrusteeFee_S] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_D] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_S] [decimal](18, 2) NULL,
	[Rateperday_D] [decimal](18, 2) NULL,
	[Rateperday_S] [decimal](18, 2) NULL,
	[Total_D] [decimal](18, 2) NULL,
	[Total_S] [decimal](18, 2) NULL,
	[Createddate] [datetime] NULL,
	[Modifieddate] [datetime] NULL,
	[PaymentRequestDate] [datetime] NULL,
	[PaymentDueDate] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[Modifiedby] [nvarchar](100) NULL,
	[Noofdays_D] [nvarchar](max) NULL,
	[Noofdays_S] [nvarchar](max) NULL,
	[AssignedTo] [nvarchar](max) NULL,
	[ClaimantName] [nvarchar](max) NULL,
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
 CONSTRAINT [PK_CLM_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CLM_Payment]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Payment_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_Payment] CHECK CONSTRAINT [FK_CLM_Payment_ClaimAccidentDetails]
GO

ALTER TABLE [dbo].[CLM_Payment]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Payment_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[CLM_Payment] CHECK CONSTRAINT [FK_CLM_Payment_CLM_Claims]
GO

ALTER TABLE [dbo].[CLM_Payment] ADD  CONSTRAINT [DF_CLM_Payment_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO

ALTER TABLE [dbo].[CLM_Payment] ADD  CONSTRAINT [DF_CLM_Payment_ApprovePayment]  DEFAULT ('N') FOR [ApprovePayment]
GO

ALTER TABLE [dbo].[CLM_Payment] ADD  CONSTRAINT [DF_CLM_Payment_MovementType]  DEFAULT ('I') FOR [MovementType]
GO


