CREATE TABLE [dbo].[MNT_Lookups](
	[LookupID] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[Lookupvalue] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Lookupdesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Category] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lookupCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreateDate] [datetime] NULL,
	[CreateBy] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [pk_MNT_Lookups_LookupID] PRIMARY KEY CLUSTERED 
(
	[LookupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


