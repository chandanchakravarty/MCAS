CREATE TABLE [dbo].[Claim_Log](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Claim_Log_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Trx_ID] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRX_Type] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Log_Desc] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Claim_Log] PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


