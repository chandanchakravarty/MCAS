/****** Object:  Table [dbo].[MNT_BranchLogin]    Script Date: 06/03/2014 11:15:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_BranchLogin]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_BranchLogin]
GO

/****** Object:  Table [dbo].[MNT_BranchLogin]    Script Date: 06/03/2014 11:15:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_BranchLogin](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[BranchType] [varchar](1) NULL,
	[BranchCode] [nvarchar](3) NULL,
	[BranchName] [nvarchar](30) NULL,
	[RegionCode] [nvarchar](20) NULL,
	[MainBranchCode] [nvarchar](3) NULL,
	[AppURL] [nvarchar](500) NULL,
	[FundType] [nvarchar](10) NULL,
 CONSTRAINT [PK_MNT_BranchLogin] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/******  Insert Script ******/

INSERT INTO [CDGI].[dbo].[MNT_BranchLogin]
           ([BranchType],[BranchCode],[BranchName],[RegionCode],[MainBranchCode],[AppURL],[FundType])
     VALUES
           ('B','01','Singapore Branch','','','/BeazleyGenericTest/',Null)
GO

INSERT INTO [CDGI].[dbo].[MNT_BranchLogin]
           ([BranchType],[BranchCode],[BranchName],[RegionCode],[MainBranchCode],[AppURL],[FundType])
     VALUES
           ('B','02','Service Company','','','/BeazleyGenericTest/',Null)
GO


