CREATE TABLE [dbo].[VISION_TransRefNos1](
	[TranTypeCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TranTypeDesc] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Suffix] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Prefix] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaxLength] [int] NULL,
	[CurrentNo] [int] NULL,
 CONSTRAINT [PK_VISION_TransRefNos] PRIMARY KEY CLUSTERED 
(
	[TranTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


