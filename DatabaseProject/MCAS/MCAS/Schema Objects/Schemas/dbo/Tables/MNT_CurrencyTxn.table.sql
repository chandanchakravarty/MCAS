CREATE TABLE [dbo].[MNT_CurrencyTxn](
	[Id_CurrencyTrans] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EffDate] [datetime] NOT NULL,
	[ExpDate] [datetime] NULL,
	[ExchangeRate] [decimal](18, 9) NULL,
	[UserId] [int] NULL,
	[TranDate] [datetime] NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_CurrencyTxn] PRIMARY KEY CLUSTERED 
(
	[Id_CurrencyTrans] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_MNT_CurrencyTxn_MNT_CurrencyM] FOREIGN KEY([CurrencyCode])
REFERENCES [dbo].[MNT_CurrencyM] ([CurrencyCode])
)


