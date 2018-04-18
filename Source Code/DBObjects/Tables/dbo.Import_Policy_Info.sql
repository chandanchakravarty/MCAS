IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Import_Policy_Info]') AND type in (N'U'))
DROP TABLE [dbo].[Import_Policy_Info]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Import_Policy_Info](
	[SUSEPLoB] [nvarchar] (2) NULL,
	[SubLoB] [nvarchar] (2) NULL,
	[SUSEPCarrierCode] [nvarchar] (3) NULL,
	[ProductCode] [nvarchar] (4) NULL,
	[CustomerType] [nvarchar] (4) NULL,
	[InsuredName] [nvarchar] (40) NULL,
	[Individual/Company Id] [nvarchar] (40) NULL,
	[Address] [nvarchar] (40) NULL,
	[Number] [nvarchar] (40) NULL,
	[Complement] [nvarchar] (40) NULL,
	[District] [nvarchar] (40) NULL,
	[City] [nvarchar] (40) NULL,
	[State] [nvarchar] (2) NULL,
	[PostalCode] [nvarchar] (10) NULL,
	[Country] [nvarchar] (20) NULL,
	[CoinsuranceType] [nvarchar] (4) NULL,
	[IsCoinsuranceLeader] [nvarchar] (2) NULL,
	[BranchCoinsurerId] [nvarchar] (3) NULL,
	[CoinsurerId] [nvarchar] (10) NULL,
	[CoinsurerShareRate] [nvarchar] (4) NULL,
	[CoinsuranceFee] [nvarchar] (4) NULL,
	[CoinsuranceNo] [nvarchar] (15) NULL,
	[CommissionRate] [nvarchar] (4) NULL,
	[DiscountRate] [nvarchar] (4) NULL,
	[TotalSumInsured] [nvarchar] (15) NULL,
	[PolicyNo] [nvarchar] (20) NULL,
	[EndorsementNo] [nvarchar] (4) NULL,
	[TransactionType] [nvarchar] (4) NULL,
	[ApplicationNo] [nvarchar] (10) NULL,
	[ApplicationDate] [nvarchar] (8) NULL,
	[Issuance Date] [nvarchar] (8) NULL,
	[Effective Date] [nvarchar] (8) NULL,
	[Expire Date] [nvarchar] (8) NULL,
	[NumberInstallments] [nvarchar] (2) NULL,
	[Currency] [nvarchar] (2) NULL,
	[CurrencyReferenceDate] [nvarchar] (8) NULL,
	[ExchangeRate ] [nvarchar] (6) NULL,
	[NetPremium] [nvarchar] (13) NULL,
	[IssuanceCost] [nvarchar] (13) NULL,
	[Tax] [nvarchar] (13) NULL,
	[Insterest] [nvarchar] (13) NULL,
	[GrossPremium] [nvarchar] (13) NULL,
	[PaymentMethod] [nvarchar] (2) NULL,
	[ReinsuranceType] [nvarchar] (4) NULL,
	[ReinsuranceId] [nvarchar] (10) NULL,
	[ReinsuranceContractCode] [nvarchar] (10) NULL,
	[ReinsurerId] [nvarchar] (10) NULL,
	[ReinsuranceShareRate] [nvarchar] (4) NULL,
	[ReinsuranceFee] [nvarchar] (6) NULL,
	[ID] [int] NOT NULL IDENTITY (1,1)
) ON [PRIMARY]
GO

