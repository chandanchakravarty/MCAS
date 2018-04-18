﻿CREATE TABLE [dbo].[PolicyAmountTrx](
	[TranRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TotalSumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_TotalSumIns_1]  DEFAULT ((0)),
	[TotalPremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_TotalPremium_1]  DEFAULT ((0)),
	[InsuredPremium] [decimal](18, 2) NULL,
	[InsuredSumIns] [decimal](18, 2) NULL,
	[ObligatoryPremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatoryPremium_1]  DEFAULT ((0)),
	[ObligatoryRate] [decimal](18, 3) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatoryRate]  DEFAULT ((0)),
	[ObligatorySumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatorySumIns_1]  DEFAULT ((0)),
	[FacPremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacPremium_1]  DEFAULT ((0)),
	[FacRate] [decimal](18, 9) NULL CONSTRAINT [DF_PolicyAmountTrx_FacRate]  DEFAULT ((0)),
	[FacSumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacSumIns_1]  DEFAULT ((0)),
	[QuotaSharePremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaSharePremium_1]  DEFAULT ((0)),
	[QuotaShareRate] [decimal](18, 3) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaShareRate]  DEFAULT ((0)),
	[QuotaShareSumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaShareSumIns_1]  DEFAULT ((0)),
	[FacObligSumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligSumIns_1]  DEFAULT ((0)),
	[FacObligRate] [decimal](18, 3) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligRate]  DEFAULT ((0)),
	[FacObligPremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligPremium_1]  DEFAULT ((0)),
	[SurplusPremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusPremium_1]  DEFAULT ((0)),
	[SurplusRate] [decimal](18, 3) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusRate]  DEFAULT ((0)),
	[SurplusSumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusSumIns_1]  DEFAULT ((0)),
	[Surplus2SumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2SumIns_1]  DEFAULT ((0)),
	[Surplus2Rate] [decimal](18, 3) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2Rate]  DEFAULT ((0)),
	[Surplus2Premium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2Premium_1]  DEFAULT ((0)),
	[XOLSumIns] [decimal](18, 0) NULL,
	[XOLRate] [decimal](18, 2) NULL,
	[XOLPremium] [decimal](18, 0) NULL,
	[GrossRetPremium] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_GrossRetPremium_1]  DEFAULT ((0)),
	[GrossRetRate] [decimal](18, 3) NULL,
	[GrossRetSumIns] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_GrossRetSumIns_1]  DEFAULT ((0)),
	[TotalSumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_TotalSumIns_Init_1]  DEFAULT ((0)),
	[TotalPremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_TotalPremium_Init_1]  DEFAULT ((0)),
	[InsuredPremium_Init] [decimal](18, 2) NULL,
	[InsuredSumIns_Init] [decimal](18, 2) NULL,
	[ObligatoryPremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatoryPremium_Init_1]  DEFAULT ((0)),
	[ObligatorySumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatorySumIns_Init_1]  DEFAULT ((0)),
	[FacPremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacPremium_Init_1]  DEFAULT ((0)),
	[FacSumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacSumIns_Init_1]  DEFAULT ((0)),
	[QuotaSharePremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaSharePremium_Init_1]  DEFAULT ((0)),
	[QuotaShareSumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaShareSumIns_Init_1]  DEFAULT ((0)),
	[FacObligSumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligSumIns_Init_1]  DEFAULT ((0)),
	[FacObligPremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligPremium_Init_1]  DEFAULT ((0)),
	[SurplusPremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusPremium_Init_1]  DEFAULT ((0)),
	[SurplusSumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusSumIns_Init_1]  DEFAULT ((0)),
	[Surplus2SumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2SumIns_Init_1]  DEFAULT ((0)),
	[Surplus2Premium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2Premium_Init_1]  DEFAULT ((0)),
	[XOLSumIns_Init] [decimal](18, 0) NULL,
	[XOLPremium_Init] [decimal](18, 0) NULL,
	[GrossRetPremium_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_GrossRetPremium_Init_1]  DEFAULT ((0)),
	[GrossRetSumIns_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_GrossRetSumIns_Init_1]  DEFAULT ((0)),
	[TotalSumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_TotalSumIns_Final_1]  DEFAULT ((0)),
	[TotalPremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_TotalPremium_Final_1]  DEFAULT ((0)),
	[InsuredPremium_Final] [decimal](18, 2) NULL,
	[InsuredSumIns_Final] [decimal](18, 2) NULL,
	[ObligatoryPremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatoryPremium_Final_1]  DEFAULT ((0)),
	[ObligatorySumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_ObligatorySumIns_Final_1]  DEFAULT ((0)),
	[FacPremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacPremium_Final_1]  DEFAULT ((0)),
	[FacSumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacSumIns_Final_1]  DEFAULT ((0)),
	[QuotaSharePremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaSharePremium_Final_1]  DEFAULT ((0)),
	[QuotaShareSumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_QuotaShareSumIns_Final_1]  DEFAULT ((0)),
	[FacObligSumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligSumIns_Final_1]  DEFAULT ((0)),
	[FacObligPremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacObligPremium_Final_1]  DEFAULT ((0)),
	[SurplusPremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusPremium_Final_1]  DEFAULT ((0)),
	[SurplusSumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_SurplusSumIns_Final_1]  DEFAULT ((0)),
	[Surplus2SumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2SumIns_Final_1]  DEFAULT ((0)),
	[Surplus2Premium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Surplus2Premium_Final_1]  DEFAULT ((0)),
	[XOLSumIns_Final] [decimal](18, 0) NULL,
	[XOLPremium_Final] [decimal](18, 0) NULL,
	[GrossRetPremium_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_GrossRetPremium_Final_1]  DEFAULT ((0)),
	[GrossRetSumIns_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_GrossRetSumIns_Final_1]  DEFAULT ((0)),
	[CommAmt] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_CommAmt]  DEFAULT ((0)),
	[CommAmt_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_CommAmt_Init]  DEFAULT ((0)),
	[CommAmt_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_CommAmt_Final]  DEFAULT ((0)),
	[LessDiscRate] [decimal](18, 2) NULL,
	[LessDiscAmt] [decimal](18, 2) NULL,
	[LessDiscAmt_Init] [decimal](18, 2) NULL,
	[LessDiscAmt_Final] [decimal](18, 2) NULL,
	[Surplus3Premium] [decimal](18, 2) NULL,
	[Surplus3Rate] [decimal](18, 3) NULL,
	[Surplus3SumIns] [decimal](18, 2) NULL,
	[Surplus3Premium_Init] [decimal](18, 2) NULL,
	[Surplus3Premium_Final] [decimal](18, 2) NULL,
	[Surplus3SumIns_Init] [decimal](18, 2) NULL,
	[Surplus3SumIns_Final] [decimal](18, 2) NULL,
	[Surplus4Premium] [decimal](18, 2) NULL,
	[Surplus4Rate] [decimal](18, 3) NULL,
	[Surplus4SumIns] [decimal](18, 2) NULL,
	[Surplus4Premium_Init] [decimal](18, 2) NULL,
	[Surplus4Premium_Final] [decimal](18, 2) NULL,
	[Surplus4SumIns_Init] [decimal](18, 2) NULL,
	[Surplus4SumIns_Final] [decimal](18, 2) NULL,
	[Surplus5Premium] [decimal](18, 2) NULL,
	[Surplus5Rate] [decimal](18, 3) NULL,
	[Surplus5SumIns] [decimal](18, 2) NULL,
	[Surplus5Premium_Init] [decimal](18, 2) NULL,
	[Surplus5Premium_Final] [decimal](18, 2) NULL,
	[Surplus5SumIns_Init] [decimal](18, 2) NULL,
	[Surplus5SumIns_Final] [decimal](18, 2) NULL,
	[Comm2Amt] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Comm2Amt]  DEFAULT ((0)),
	[Comm2Amt_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Comm2Amt_Init]  DEFAULT ((0)),
	[Comm2Amt_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_Comm2Amt_Final]  DEFAULT ((0)),
	[StampDuty] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_StampDuty]  DEFAULT ((0)),
	[StampDuty_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_StampDuty_Init]  DEFAULT ((0)),
	[StampDuty_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_StampDuty_Final]  DEFAULT ((0)),
	[PolicyCost] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_PolicyCost]  DEFAULT ((0)),
	[PolicyCost_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_PolicyCost_Init]  DEFAULT ((0)),
	[PolicyCost_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_PolicyCost_Final]  DEFAULT ((0)),
	[ExtraSumIns] [decimal](18, 2) NULL,
	[ExtraPremium] [decimal](18, 2) NULL,
	[ExtraSumIns_Init] [decimal](18, 2) NULL,
	[ExtraPremium_Init] [decimal](18, 2) NULL,
	[ExtraSumIns_Final] [decimal](18, 2) NULL,
	[ExtraPremium_Final] [decimal](18, 2) NULL,
	[ExtraDiscAmt] [decimal](18, 2) NULL,
	[ExtraCommAmt] [decimal](18, 2) NULL,
	[ExtraComm2Amt] [decimal](18, 2) NULL,
	[ExtraDiscAmt_Init] [decimal](18, 2) NULL,
	[ExtraCommAmt_Init] [decimal](18, 2) NULL,
	[ExtraComm2Amt_Init] [decimal](18, 2) NULL,
	[ExtraDiscAmt_Final] [decimal](18, 2) NULL,
	[ExtraCommAmt_Final] [decimal](18, 2) NULL,
	[ExtraComm2Amt_Final] [decimal](18, 2) NULL,
	[CreditNoteAmt] [decimal](18, 2) NULL,
	[CreditNoteAmt_Init] [decimal](18, 2) NULL,
	[CreditNoteAmt_Final] [decimal](18, 2) NULL,
	[EQPoolPremium] [decimal](18, 2) NULL,
	[EQPoolRate] [decimal](18, 3) NULL,
	[EQPoolRIRate] [decimal](18, 4) NULL,
	[EQPoolSumIns] [decimal](18, 2) NULL,
	[EQPoolCode] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EQPoolPremium_Init] [decimal](18, 2) NULL,
	[EQPoolPremium_Final] [decimal](18, 2) NULL,
	[EQPoolSumIns_Init] [decimal](18, 2) NULL,
	[EQPoolSumIns_Final] [decimal](18, 2) NULL,
	[QuotaShareRIComm] [decimal](18, 4) NULL,
	[Surplus1RIComm] [decimal](18, 4) NULL,
	[Surplus2RIComm] [decimal](18, 4) NULL,
	[Surplus3RIComm] [decimal](18, 4) NULL,
	[QuotaShareRICommAmt] [decimal](18, 2) NULL,
	[Surplus1RICommAmt] [decimal](18, 2) NULL,
	[Surplus2RICommAmt] [decimal](18, 2) NULL,
	[Surplus3RICommAmt] [decimal](18, 2) NULL,
	[QuotaShareRIORComm] [decimal](18, 4) NULL,
	[Surplus1RIORComm] [decimal](18, 4) NULL,
	[Surplus2RIORComm] [decimal](18, 4) NULL,
	[Surplus3RIORComm] [decimal](18, 4) NULL,
	[QuotaShareRIORCommAmt] [decimal](18, 2) NULL,
	[Surplus1RIORCommAmt] [decimal](18, 2) NULL,
	[Surplus2RIORCommAmt] [decimal](18, 2) NULL,
	[Surplus3RIORCommAmt] [decimal](18, 2) NULL,
	[QuotaShareRICommAmt_Init] [decimal](18, 2) NULL,
	[Surplus1RICommAmt_Init] [decimal](18, 2) NULL,
	[Surplus2RICommAmt_Init] [decimal](18, 2) NULL,
	[Surplus3RICommAmt_Init] [decimal](18, 2) NULL,
	[QuotaShareRIORCommAmt_Init] [decimal](18, 2) NULL,
	[Surplus1RIORCommAmt_Init] [decimal](18, 2) NULL,
	[Surplus2RIORCommAmt_Init] [decimal](18, 2) NULL,
	[Surplus3RIORCommAmt_Init] [decimal](18, 2) NULL,
	[QuotaShareRICommAmt_Final] [decimal](18, 2) NULL,
	[Surplus1RICommAmt_Final] [decimal](18, 2) NULL,
	[Surplus2RICommAmt_Final] [decimal](18, 2) NULL,
	[Surplus3RICommAmt_Final] [decimal](18, 2) NULL,
	[QuotaShareRIORCommAmt_Final] [decimal](18, 2) NULL,
	[Surplus1RIORCommAmt_Final] [decimal](18, 2) NULL,
	[Surplus2RIORCommAmt_Final] [decimal](18, 2) NULL,
	[Surplus3RIORCommAmt_Final] [decimal](18, 2) NULL,
	[TaxRate] [decimal](18, 3) NULL,
	[Tax2Rate] [decimal](18, 2) NULL,
	[VAT2Rate] [decimal](18, 3) NULL,
	[CreditType] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FacPremiumXOL] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacPremiumXOL]  DEFAULT ((0)),
	[FacSumInsXOL] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacSumInsXOL]  DEFAULT ((0)),
	[FacRateXOL] [decimal](18, 2) NULL,
	[FacPremiumXOL_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacPremiumXOL_Init]  DEFAULT ((0)),
	[FacSumInsXOL_Init] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacSumInsXOL_Init]  DEFAULT ((0)),
	[FacPremiumXOL_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacPremiumXOL_Final]  DEFAULT ((0)),
	[FacSumInsXOL_Final] [decimal](18, 2) NULL CONSTRAINT [DF_PolicyAmountTrx_FacSumInsXOL_Final]  DEFAULT ((0)),
	[TaxType] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GST] [decimal](19, 3) NULL,
	[Broker_TaxType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiscountRate] [decimal](18, 3) NULL,
	[DiscountAmt] [decimal](18, 2) NULL,
	[DiscountAmt_Init] [decimal](18, 2) NULL,
	[DiscountAmt_Final] [decimal](18, 2) NULL,
	[OtherCharges] [decimal](18, 2) NULL,
	[OtherCharges_Init] [decimal](18, 2) NULL,
	[OtherCharges_Final] [decimal](18, 2) NULL,
	[Comm2Rate] [decimal](18, 5) NULL,
	[GSTAmount] [decimal](18, 2) NULL,
	[GSTAmount_Init] [decimal](18, 2) NULL,
	[GSTAmount_Final] [decimal](18, 2) NULL,
	[BrokerGST] [decimal](19, 3) NULL,
	[BrokerGSTAmount] [decimal](18, 2) NULL,
	[BrokerGSTAmount_Init] [decimal](18, 2) NULL,
	[BrokerGSTAmount_Final] [decimal](18, 2) NULL,
	[BrokerTaxType] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Currency] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrExRate] [decimal](18, 9) NULL,
	[LossDiscountPct] [decimal](18, 2) NULL,
	[LessLeadFeePct] [decimal](18, 5) NULL,
	[LessLeadFeeAmt] [decimal](18, 2) NULL,
	[LessCommision] [decimal](18, 5) NULL,
	[LessTaxPct] [decimal](18, 5) NULL,
	[BasicPremiuFor] [decimal](18, 2) NULL,
	[LossDiscountFor] [decimal](18, 2) NULL,
	[NetPremiumFor] [decimal](18, 2) NULL,
	[TotalDue] [decimal](18, 2) NULL,
	[BGST] [decimal](18, 2) NULL,
	[TotalDueAGST] [decimal](18, 2) NULL,
	[LessLeadFeeFo] [decimal](18, 2) NULL,
	[LessCommisionFor] [decimal](18, 2) NULL,
	[VAT2Amt] [decimal](18, 2) NULL,
	[LessTaxFor] [decimal](18, 2) NULL,
	[NettDue] [decimal](18, 2) NULL,
	[LessLeadFeeFor] [decimal](18, 2) NULL,
	[BasicPremiumFor] [decimal](18, 2) NULL,
	[LessLeadFee] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FacObligRIComm_Final] [decimal](18, 2) NULL,
	[FacObligRIComm_Init] [decimal](18, 2) NULL,
	[FacObligRIComm] [decimal](18, 2) NULL,
	[EstPrem] [decimal](18, 2) NULL,
	[EstBeazley] [decimal](18, 2) NULL,
	[QuotaShareCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SurplusCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FacObligcode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TSI] [decimal](18, 2) NULL,
	[TSIBeazley] [decimal](18, 2) NULL,
	[EstPrem_Init] [decimal](18, 2) NULL,
	[EstBeazley_Init] [decimal](18, 2) NULL,
	[TSIBeazley_Init] [decimal](18, 2) NULL,
	[CommitmentFee] [decimal](18, 2) NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[InstCtr] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


