CREATE TABLE [dbo].[CLM_Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionType] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreditorName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ExpenseCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AmountPaid] [numeric](18, 0) NOT NULL,
	[Authorizedby] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuthorizedDate] [datetime] NULL,
	[AuthorizedTime] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProcessedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CLM_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_Transactions_CLM_Claims] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
)


