IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Mandate_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]'))
ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [FK_CLM_Mandate_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Mandate_CLM_Claim]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]'))
ALTER TABLE [dbo].[CLM_Mandate] DROP CONSTRAINT [FK_CLM_Mandate_CLM_Claim]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_Mandate]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_Mandate]
GO

CREATE TABLE [dbo].[CLM_Mandate](
	[MandateId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [int] NOT NULL,
	[AccidentId] [int] NULL,
	[PolicyId] [int] NULL,
	[ReserveId] [int] NULL,
	[IsActive] [nvarchar](2) NULL,
	[ClaimID] [int] NULL,
	[ClaimantName] [nvarchar](100) NULL,
	[AssignedTo] [int] NULL,
	[InvestigationResult] [int] NULL,
	[Scenario] [nvarchar](max) NULL,
	[Evidence] [nvarchar](max) NULL,
	[RelatedFacts] [nvarchar](max) NULL,
	[COAssessment] [nvarchar](max) NULL,
	[SupervisorAssignto] [int] NULL,
	[ApproveRecommedations] [nvarchar](2) NULL,
	[SupervisorRemarks] [nvarchar](max) NULL,
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
	[Remarks] [nvarchar](max) NULL,
	[Createdby] [nvarchar](50) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](50) NULL,
	[Modifieddate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[MandateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CLM_Mandate]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Mandate_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_Mandate] CHECK CONSTRAINT [FK_CLM_Mandate_ClaimAccidentDetails]
GO

ALTER TABLE [dbo].[CLM_Mandate]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Mandate_CLM_Claim] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claim] ([ClaimID])
GO

ALTER TABLE [dbo].[CLM_Mandate] CHECK CONSTRAINT [FK_CLM_Mandate_CLM_Claim]
GO


