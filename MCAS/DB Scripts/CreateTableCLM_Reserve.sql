IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_Total_I]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_Total_I]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_Total_R]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_Total_R]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_Total_O]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_Total_O]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_LOGAmount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_LOGAmount]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_LOGDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_LOGDate]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_HospitalName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_HospitalName]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_AssignedTo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_AssignedTo]
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_Reserve]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_Reserve]
GO

CREATE TABLE [dbo].[CLM_Reserve](
	[ReserveId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimantName] [nvarchar](max) NOT NULL,
	[ClaimType] [int] NOT NULL,
	[CostofRepairs_I] [decimal](18, 2) NULL,
	[CostofRepairs_R] [decimal](18, 2) NULL,
	[CostofRepairs_O] [decimal](18, 2) NULL,
	[LossofUse_I] [decimal](18, 2) NULL,
	[LossofUse_R] [decimal](18, 2) NULL,
	[LossofUse_O] [decimal](18, 2) NULL,
	[LossofUseUn_I] [decimal](18, 2) NULL,
	[LossofUseUn_R] [decimal](18, 2) NULL,
	[LossofUseUn_O] [decimal](18, 2) NULL,
	[LossofRental_I] [decimal](18, 2) NULL,
	[LossofRental_R] [decimal](18, 2) NULL,
	[LossofRental_O] [decimal](18, 2) NULL,
	[Excess_I] [decimal](18, 2) NULL,
	[Excess_R] [decimal](18, 2) NULL,
	[Excess_O] [decimal](18, 2) NULL,
	[OtherExpenses_I] [decimal](18, 2) NULL,
	[OtherExpenses_R] [decimal](18, 2) NULL,
	[OtherExpenses_O] [decimal](18, 2) NULL,
	[ReportFees_I] [decimal](18, 2) NULL,
	[ReportFees_R] [decimal](18, 2) NULL,
	[ReportFees_O] [decimal](18, 2) NULL,
	[SurveyFee_I] [decimal](18, 2) NULL,
	[SurveyFee_R] [decimal](18, 2) NULL,
	[SurveyFee_O] [decimal](18, 2) NULL,
	[ReSurveyFee_I] [decimal](18, 2) NULL,
	[ReSurveyFee_R] [decimal](18, 2) NULL,
	[ReSurveyFee_O] [decimal](18, 2) NULL,
	[LGPolRepFee_I] [decimal](18, 2) NULL,
	[LGPolRepFee_R] [decimal](18, 2) NULL,
	[LGPolRepFee_O] [decimal](18, 2) NULL,
	[ParLawCost3rd_I] [decimal](18, 2) NULL,
	[ParLawCost3rd_R] [decimal](18, 2) NULL,
	[ParLawCost3rd_O] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_I] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_R] [decimal](18, 2) NULL,
	[ParLawDisbursements3rd_O] [decimal](18, 2) NULL,
	[OurLawyerCost_I] [decimal](18, 2) NULL,
	[OurLawyerCost_R] [decimal](18, 2) NULL,
	[OurLawyerCost_O] [decimal](18, 2) NULL,
	[OurLawDisbursements_I] [decimal](18, 2) NULL,
	[OurLawDisbursements_R] [decimal](18, 2) NULL,
	[OurLawDisbursements_O] [decimal](18, 2) NULL,
	[GeneralDamages_I] [decimal](18, 2) NULL,
	[GeneralDamages_R] [decimal](18, 2) NULL,
	[GeneralDamages_O] [decimal](18, 2) NULL,
	[MedicalExpenses_I] [decimal](18, 2) NULL,
	[MedicalExpenses_R] [decimal](18, 2) NULL,
	[MedicalExpenses_O] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_I] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_R] [decimal](18, 2) NULL,
	[FutureMedicalExpenses_O] [decimal](18, 2) NULL,
	[LOGMedicalExpenses_I] [decimal](18, 2) NULL,
	[LOGMedicalExpenses_R] [decimal](18, 2) NULL,
	[LOGMedicalExpenses_O] [decimal](18, 2) NULL,
	[LossofEarningsCapacity_I] [decimal](18, 2) NULL,
	[LossofEarningsCapacity_R] [decimal](18, 2) NULL,
	[LossofEarningsCapacity_O] [decimal](18, 2) NULL,
	[LossofEarnings_I] [decimal](18, 2) NULL,
	[LossofEarnings_R] [decimal](18, 2) NULL,
	[LossofEarnings_O] [decimal](18, 2) NULL,
	[LossofFutureEarnings_I] [decimal](18, 2) NULL,
	[LossofFutureEarnings_R] [decimal](18, 2) NULL,
	[LossofFutureEarnings_O] [decimal](18, 2) NULL,
	[Transport_I] [decimal](18, 2) NULL,
	[Transport_R] [decimal](18, 2) NULL,
	[Transport_O] [decimal](18, 2) NULL,
	[Noofdays_I] [nvarchar](max) NULL,
	[Noofdays_R] [nvarchar](max) NULL,
	[Noofdays_O] [nvarchar](max) NULL,
	[MedicalRecord_I] [decimal](18, 2) NULL,
	[MedicalRecord_R] [decimal](18, 2) NULL,
	[MedicalRecord_O] [decimal](18, 2) NULL,
	[PublicTrusteeFee_I] [decimal](18, 2) NULL,
	[PublicTrusteeFee_R] [decimal](18, 2) NULL,
	[PublicTrusteeFee_O] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_I] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_R] [decimal](18, 2) NULL,
	[OurProfessionalExpertFee_O] [decimal](18, 2) NULL,
	[Rateperday_I] [nvarchar](max) NULL,
	[Rateperday_R] [nvarchar](max) NULL,
	[Rateperday_O] [nvarchar](max) NULL,
	[Total_I] [decimal](18, 2) NOT NULL,
	[Total_R] [decimal](18, 2) NOT NULL,
	[Total_O] [decimal](18, 2) NOT NULL,
	[LOGAmount] [decimal](18, 2) NOT NULL,
	[LOGDate] [datetime] NOT NULL,
	[CORemarks] [nvarchar](800) NULL,
	[Createdby] [nvarchar](max) NULL,
	[HospitalName] [int] NOT NULL,
	[AssignedTo] [int] NOT NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[Modifieddate] [datetime] NULL,
	[AccidentId] [int] NULL,
	[PolicyId] [int] NULL,
	[IsActive] [nvarchar](1) NULL,
	[ClaimID] [int] NULL,
 CONSTRAINT [PK_CLM_Reserve] PRIMARY KEY CLUSTERED 
(
	[ReserveId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_Total_I]  DEFAULT ((0)) FOR [Total_I]
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_Total_R]  DEFAULT ((0)) FOR [Total_R]
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_Total_O]  DEFAULT ((0)) FOR [Total_O]
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_LOGAmount]  DEFAULT ((0)) FOR [LOGAmount]
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_LOGDate]  DEFAULT (getdate()) FOR [LOGDate]
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_HospitalName]  DEFAULT ((0)) FOR [HospitalName]
GO

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_AssignedTo]  DEFAULT ((0)) FOR [AssignedTo]
GO


