CREATE TABLE [dbo].[MNT_City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[TranId] [int] NULL,
	[CityCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CityName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ZoneCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_City_CityId] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_City_CityCode] UNIQUE NONCLUSTERED 
(
	[CityCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


