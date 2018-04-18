CREATE TABLE [dbo].[Claim_RegistrationHistory](
	[TrxRefNo] [int] IDENTITY(1,1) NOT NULL,
	[ClaimRefNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Claim_RegistrationHistory_CreatedDate]  DEFAULT (getdate()),
	[Trx_type] [smallint] NULL,
	[trx_Date] [datetime] NULL,
	[trx_Desc] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[isLatest] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_Claim_RegistrationHistory_isLatest]  DEFAULT ('Y')
)


