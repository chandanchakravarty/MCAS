CREATE TABLE [dbo].[Vision_TransRefNos](
	[TranTypeCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TranTypeDesc] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Suffix] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Prefix] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaxLength] [int] NULL,
	[CurrentNo] [int] NULL,
	[Year] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


