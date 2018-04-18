CREATE TABLE [dbo].[MNT_InsruanceM](
	[PolicyId] [int] IDENTITY(1,1) NOT NULL,
	[CedantId] [int] NOT NULL,
	[PolicyNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductId] [int] NOT NULL,
	[PolicyEffectiveFrom] [datetime] NULL,
	[PolicyEffectiveTo] [datetime] NULL,
	[Deductible] [decimal](15, 2) NULL,
	[PremiumAmount] [money] NULL,
	[SubClassId] [int] NULL,
	[CurrencyCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExchangeRate] [decimal](18, 9) NULL,
	[PremiumLocalCurrency] [decimal](18, 9) NULL,
	[CreatedBy] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_InsruanceM] PRIMARY KEY CLUSTERED 
(
	[PolicyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_MNT_InsruanceM_MNT_InsruanceM] FOREIGN KEY([PolicyId])
REFERENCES [dbo].[MNT_InsruanceM] ([PolicyId])
)


