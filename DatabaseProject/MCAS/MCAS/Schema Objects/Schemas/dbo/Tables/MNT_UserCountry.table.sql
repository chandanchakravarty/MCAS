CREATE TABLE [dbo].[MNT_UserCountry](
	[CountryCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryShortCode] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryName] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_UserCountry] PRIMARY KEY CLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


