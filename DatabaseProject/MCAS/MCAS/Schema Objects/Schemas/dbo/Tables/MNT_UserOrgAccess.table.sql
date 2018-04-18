CREATE TABLE [dbo].[MNT_UserOrgAccess](
	[UserOrgId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrgCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrgName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_UserOrgAccess] PRIMARY KEY CLUSTERED 
(
	[UserOrgId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


