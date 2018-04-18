CREATE TABLE [dbo].[Claim_Document](
	[DocRefNo] [char](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CretedDate] [datetime] NULL CONSTRAINT [DF_Claim_Document_CretedDate]  DEFAULT (getdate()),
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] [char](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RefNo] [char](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AmountFor] [numeric](18, 2) NULL,
	[AmountLoc] [numeric](18, 2) NULL,
	[DocType] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TreatyType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrxType] [int] NULL,
 CONSTRAINT [PK_Claim_Document] PRIMARY KEY CLUSTERED 
(
	[DocRefNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


