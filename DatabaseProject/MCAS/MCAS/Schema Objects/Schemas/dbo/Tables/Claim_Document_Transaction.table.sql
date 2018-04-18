CREATE TABLE [dbo].[Claim_Document_Transaction](
	[DocumentID] [int] NOT NULL,
	[DocRefNo] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DocType] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DocPath] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CretedDate] [datetime] NULL CONSTRAINT [DF_Claim_Document_Transaction_CretedDate]  DEFAULT (getdate()),
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Claim_Document_Transaction] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


