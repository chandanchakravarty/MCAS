IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_IMPORTNEW_POLICY_INFO]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_IMPORTNEW_POLICY_INFO]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEMP_IMPORTNEW_POLICY_INFO](
	[SUSEPLoB] [nvarchar] (255) NULL,
	[SubLoB] [nvarchar] (255) NULL,
	[SUSEPCarrierCode] [nvarchar] (255) NULL,
	[Carrier Branch ID] [nvarchar] (255) NULL,
	[ProductCode] [nvarchar] (255) NULL,
	[CustomerType] [nvarchar] (255) NULL,
	[InsuredName] [nvarchar] (255) NULL,
	[Individual/Company Id] [nvarchar] (255) NULL,
	[Address] [nvarchar] (255) NULL,
	[Number] [nvarchar] (255) NULL,
	[Complement] [nvarchar] (255) NULL,
	[District] [nvarchar] (255) NULL,
	[City] [nvarchar] (255) NULL,
	[State] [nvarchar] (255) NULL,
	[PostalCode] [nvarchar] (255) NULL,
	[Country] [nvarchar] (255) NULL,
	[CoinsuranceType] [nvarchar] (255) NULL,
	[BranchCoinsurerId] [nvarchar] (255) NULL,
	[CoinsurerId] [nvarchar] (255) NULL,
	[CoinsurerShareRate] [nvarchar] (255) NULL,
	[CoinsuranceFee] [nvarchar] (255) NULL,
	[TransactionID] [nvarchar] (255) NULL,
	[LeaderPolicyNumber] [nvarchar] (255) NULL,
	[LeaderEndorsementNumber] [nvarchar] (255) NULL,
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[TransactionType] [nvarchar] (255) NULL,
	[Endorsement Type] [nvarchar] (255) NULL,
	[ApplicationNo] [nvarchar] (255) NULL,
	[ApplicationDate] [nvarchar] (255) NULL,
	[Issuance Date] [nvarchar] (255) NULL,
	[Effective Date] [nvarchar] (255) NULL,
	[Expire Date] [nvarchar] (255) NULL,
	[NumberInstallments] [nvarchar] (255) NULL,
	[Currency] [nvarchar] (255) NULL,
	[CurrencyReferenceDate] [nvarchar] (255) NULL,
	[ExchangeRate] [nvarchar] (255) NULL,
	[NetPremium] [nvarchar] (255) NULL,
	[IssuanceCost] [nvarchar] (255) NULL,
	[Tax] [nvarchar] (255) NULL,
	[Insterest] [nvarchar] (255) NULL,
	[GrossPremium] [nvarchar] (255) NULL,
	[PaymentMethod] [nvarchar] (255) NULL,
	[Policy Level Commission Percentage] [nvarchar] (255) NULL,
	[susep process number] [nvarchar] (255) NULL,
	[FIRST_NAME] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

