IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Transactions_CLM_Claims]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Transactions]'))
ALTER TABLE [dbo].[CLM_Transactions] DROP CONSTRAINT [FK_CLM_Transactions_CLM_Claims]
GO


/****** Object:  Table [dbo].[CLM_Transactions]    Script Date: 07/03/2014 20:19:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_Transactions]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_Transactions]
GO

USE [CDGI]
GO

/****** Object:  Table [dbo].[CLM_Transactions]    Script Date: 07/03/2014 20:19:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CLM_Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionType] [nvarchar](20) NOT NULL,
	[CreditorName] [nvarchar](200) NOT NULL,
	[ExpenseCode] [nvarchar](50) NOT NULL,
	[AmountPaid] [numeric](18, 0) NOT NULL,
	[Authorizedby] [nvarchar](200) NOT NULL,
	[AuthorizedDate] [datetime] NULL,
	[AuthorizedTime] [nvarchar](20) NULL,
	[ProcessedDate] [datetime] NULL,
 CONSTRAINT [PK_CLM_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CLM_Transactions]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Transactions_CLM_Claims] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[CLM_Transactions] CHECK CONSTRAINT [FK_CLM_Transactions_CLM_Claims]
GO


