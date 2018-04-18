CREATE TABLE [dbo].[MNT_Province](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProvinceName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Province] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


