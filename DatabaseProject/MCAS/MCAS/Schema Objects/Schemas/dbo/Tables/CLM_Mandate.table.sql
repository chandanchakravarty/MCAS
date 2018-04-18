﻿CREATE TABLE [dbo].[CLM_Mandate](
	[MandateId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [int] NOT NULL,
	[AccidentId] [int] NULL,
	[PolicyId] [int] NULL,
	[ReserveId] [int] NULL,
	[IsActive] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimID] [int] NULL,
	[ClaimantName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AssignedTo] [int] NULL,
	[InvestigationResult] [int] NULL,
	[Scenario] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Evidence] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RelatedFacts] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[COAssessment] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SupervisorAssignto] [int] NULL,
	[ApproveRecommedations] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SupervisorRemarks] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CostofRepairs_Q] [decimal](18, 2) NULL,
	[CostofRepairs_P] [decimal](18, 2) NULL,
	[CostofRepairs_C] [decimal](18, 2) NULL,
	[CostofRepairs_S] [decimal](18, 2) NULL,
	[Liability_P] [decimal](18, 2) NULL,
	[Liability_C] [decimal](18, 2) NULL,
	[Liability_S] [decimal](18, 2) NULL,
	[LossofUseUn_Q] [decimal](18, 2) NULL,
	[LossofUseUn_P] [decimal](18, 2) NULL,
	[LossofUseUn_C] [decimal](18, 2) NULL,
	[LossofUseUn_S] [decimal](18, 2) NULL,
	[LossofEarning_P] [decimal](18, 2) NULL,
	[LossofEarning_C] [decimal](18, 2) NULL,
	[LossofEarning_S] [decimal](18, 2) NULL,
	[LossofRental_P] [decimal](18, 2) NULL,
	[LossofRental_C] [decimal](18, 2) NULL,
	[LossofRental_S] [decimal](18, 2) NULL,
	[Excess_P] [decimal](18, 2) NULL,
	[Excess_C] [decimal](18, 2) NULL,
	[Excess_S] [decimal](18, 2) NULL,
	[SubTotal_P] [decimal](18, 2) NULL,
	[SubTotal_C] [decimal](18, 2) NULL,
	[SubTotal_S] [decimal](18, 2) NULL,
	[OtherExpenses_Q] [decimal](18, 2) NULL,
	[OtherExpenses_P] [decimal](18, 2) NULL,
	[OtherExpenses_C] [decimal](18, 2) NULL,
	[OtherExpenses_S] [decimal](18, 2) NULL,
	[SurveyFee_Q] [decimal](18, 2) NULL,
	[SurveyFee_P] [decimal](18, 2) NULL,
	[SurveyFee_C] [decimal](18, 2) NULL,
	[SurveyFee_S] [decimal](18, 2) NULL,
	[ReSurveyFee_Q] [decimal](18, 2) NULL,
	[ReSurveyFee_P] [decimal](18, 2) NULL,
	[ReSurveyFee_C] [decimal](18, 2) NULL,
	[ReSurveyFee_S] [decimal](18, 2) NULL,
	[LGPolRepFee_Q] [decimal](18, 2) NULL,
	[LGPolRepFee_P] [decimal](18, 2) NULL,
	[LGPolRepFee_C] [decimal](18, 2) NULL,
	[LGPolRepFee_S] [decimal](18, 2) NULL,
	[ParLawCost3rd_Q] [decimal](18, 2) NULL,
	[ParLawCost3rd_P] [decimal](18, 2) NULL,
	[ParLawCost3rd_C] [decimal](18, 2) NULL,
	[ParLawCost3rd_S] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_Q] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_P] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_C] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_S] [decimal](18, 2) NULL,
	[OurLawyerCost_Q] [decimal](18, 2) NULL,
	[OurLawyerCost_P] [decimal](18, 2) NULL,
	[OurLawyerCost_C] [decimal](18, 2) NULL,
	[OurLawyerCost_S] [decimal](18, 2) NULL,
	[OurLawDisbursements_Q] [decimal](18, 2) NULL,
	[OurLawDisbursements_P] [decimal](18, 2) NULL,
	[OurLawDisbursements_C] [decimal](18, 2) NULL,
	[OurLawDisbursements_S] [decimal](18, 2) NULL,
	[Total_Q] [decimal](18, 2) NULL,
	[Total_P] [decimal](18, 2) NULL,
	[Total_C] [decimal](18, 2) NULL,
	[Total_S] [decimal](18, 2) NULL,
	[GeneralDamages_P] [decimal](18, 2) NULL,
	[GeneralDamages_C] [decimal](18, 2) NULL,
	[GeneralDamages_S] [decimal](18, 2) NULL,
	[MedicalExpenses_P] [decimal](18, 2) NULL,
	[MedicalExpenses_C] [decimal](18, 2) NULL,
	[MedicalExpenses_S] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_P] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_C] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_S] [decimal](18, 2) NULL,
	[LogMedicalExpenses_P] [decimal](18, 2) NULL,
	[LogMedicalExpenses_C] [decimal](18, 2) NULL,
	[LogMedicalExpenses_S] [decimal](18, 2) NULL,
	[LossofEarningCapacity_P] [decimal](18, 2) NULL,
	[LossofEarningCapacity_C] [decimal](18, 2) NULL,
	[LossofEarningCapacity_S] [decimal](18, 2) NULL,
	[LossofEarnings_P] [decimal](18, 2) NULL,
	[LossofEarnings_C] [decimal](18, 2) NULL,
	[LossofEarnings_S] [decimal](18, 2) NULL,
	[LossofFutureEarnings_P] [decimal](18, 2) NULL,
	[LossofFutureEarnings_C] [decimal](18, 2) NULL,
	[LossofFutureEarnings_S] [decimal](18, 2) NULL,
	[Transport_P] [decimal](18, 2) NULL,
	[Transport_C] [decimal](18, 2) NULL,
	[Transport_S] [decimal](18, 2) NULL,
	[MedicalRecord_P] [decimal](18, 2) NULL,
	[MedicalRecord_C] [decimal](18, 2) NULL,
	[MedicalRecord_S] [decimal](18, 2) NULL,
	[PublicTrusteeFee_P] [decimal](18, 2) NULL,
	[PublicTrusteeFee_C] [decimal](18, 2) NULL,
	[PublicTrusteeFee_S] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_P] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_C] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_S] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createdby] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Modifieddate] [datetime] NULL,
	[PaymentId] [int] NULL,
	[MovementType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_CLM_Mandate_MovementType]  DEFAULT ('I'),
	[InformSafetytoreviewfindings] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MandateRecordNo] [nvarchar](100) NULL,
 CONSTRAINT [PK__CLM_Mand__A5A5EC654D755761] PRIMARY KEY CLUSTERED 
(
	[MandateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_Mandate_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK_CLM_Mandate_CLM_Claim] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID]),
 CONSTRAINT [FK_CLM_Mandate_CLM_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[CLM_Payment] ([PaymentId])
)


