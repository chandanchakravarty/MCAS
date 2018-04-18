CREATE TABLE [dbo].[MNT_CountryGrouping](
	[GroupCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GroupName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GroupDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TM_CountryGrouping] PRIMARY KEY CLUSTERED 
(
	[GroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


