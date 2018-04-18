CREATE TABLE [dbo].[MNT_Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[TranId] [int] NULL,
	[CountryCode] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GroupCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryShortCode] [nvarchar](8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Country_CountryId] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Country_CountryCode] UNIQUE NONCLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


