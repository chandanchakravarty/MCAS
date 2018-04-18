CREATE TABLE [dbo].[MNT_Branch](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[BranchType] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BranchCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BranchName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RegionCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MainBranchCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Branch_BranchId] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Branch_BranchCode] UNIQUE NONCLUSTERED 
(
	[BranchCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


