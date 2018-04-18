CREATE TABLE [dbo].[MNT_BranchLogin](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[BranchType] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BranchCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BranchName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RegionCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MainBranchCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AppURL] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FundType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_BranchLogin] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


