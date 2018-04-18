CREATE TABLE [dbo].[FIN_ClaimLedger](
	[TranId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[TranType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReserveRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProdCode] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClassCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GLCode] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TraRefCode1] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TraRefCode2] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TraRefCode3] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DebitAmount] [decimal](18, 2) NULL,
	[CreditAmount] [decimal](18, 2) NULL,
	[TranDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DebitAmountFor] [decimal](18, 2) NULL,
	[CreditAmountFor] [decimal](18, 2) NULL,
	[Currency] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExRate] [decimal](18, 9) NULL
)


