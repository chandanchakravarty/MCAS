

/****** Object:  Table [dbo].[MNT_UserOrgAccess]    Script Date: 12/08/2014 16:27:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_UserOrgAccess]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_UserOrgAccess]
GO



/****** Object:  Table [dbo].[MNT_UserOrgAccess]    Script Date: 12/08/2014 16:27:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_UserOrgAccess](
	[UserOrgId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[OrgCode] [nvarchar](50) NULL,
	[OrgName] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_MNT_UserOrgAccess] PRIMARY KEY CLUSTERED 
(
	[UserOrgId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


