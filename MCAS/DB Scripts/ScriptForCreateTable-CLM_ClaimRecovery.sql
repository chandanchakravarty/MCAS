
CREATE TABLE [dbo].[CLM_ClaimRecovery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[RecoveryReason] [nvarchar](500) NULL,
	[CurrCode] [nchar](10) NULL,
	[ClaimID] [int] NULL,
	[ExchangeRate] [numeric](18, 9) NULL,
	[LocalCurrAmt] [numeric](18, 2) NULL,
	[Status] [int] NULL,
	[PaymentDetails] [nvarchar](200) NULL,
	[IsActive] [char](1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CLM_ClaimRecovery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery] ADD  DEFAULT ('Y') FOR [IsActive]
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[CLM_ClaimRecovery] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO


