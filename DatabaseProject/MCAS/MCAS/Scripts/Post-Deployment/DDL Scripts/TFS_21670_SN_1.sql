-- =============================================
-- Script Template
-- =============================================
CREATE TABLE [dbo].[Clm_ConfirmAmtBreakdown](
	[ConfirmAmtId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	[ConfirmInvoiceAmt] [numeric](18, 2) NULL,
	[LossOfIncome] [numeric](18, 2) NULL,
	[LossofRental] [numeric](18, 2) NULL,
	[Others1] [numeric](18, 2) NULL,
	[LegalFee] [numeric](18, 2) NULL,
	[TPGIAFee] [numeric](18, 2) NULL,
	[SurveyFee] [numeric](18, 2) NULL,
	[Others2] [numeric](18, 2) NULL,
	[LTAFee] [numeric](18, 2) NULL,
	[LossOfUse] [numeric](18, 2) NULL,
	[MedicalExp] [numeric](18, 2) NULL,
	[CarRental] [numeric](18, 2) NULL,
	[CourtesyCar] [numeric](18, 2) NULL,
	[Total] [numeric](18, 2) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Clm_ConfirmAmtData] PRIMARY KEY CLUSTERED 
(
	[ConfirmAmtId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Clm_ConfirmAmtBreakdown]  WITH CHECK ADD  CONSTRAINT [FK_Clm_ConfirmAmtBreakdown_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[Clm_ConfirmAmtBreakdown] CHECK CONSTRAINT [FK_Clm_ConfirmAmtBreakdown_ClaimAccidentDetails]
GO

ALTER TABLE [dbo].[Clm_ConfirmAmtBreakdown]  WITH CHECK ADD  CONSTRAINT [FK_Clm_ConfirmAmtBreakdown_CLM_Claims] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[Clm_ConfirmAmtBreakdown] CHECK CONSTRAINT [FK_Clm_ConfirmAmtBreakdown_CLM_Claims]
GO
