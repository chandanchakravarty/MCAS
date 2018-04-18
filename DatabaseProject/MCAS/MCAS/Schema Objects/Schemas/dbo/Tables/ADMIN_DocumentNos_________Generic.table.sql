CREATE TABLE [dbo].[ADMIN_DocumentNos_________Generic](
	[TranTypeCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TranYear] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BranchCode] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TranTypeDesc] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaxLength] [int] NULL,
	[CurrentNo] [int] NULL,
 CONSTRAINT [PK_ADMIN_DocumentNos] PRIMARY KEY CLUSTERED 
(
	[TranTypeCode] ASC,
	[TranYear] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


