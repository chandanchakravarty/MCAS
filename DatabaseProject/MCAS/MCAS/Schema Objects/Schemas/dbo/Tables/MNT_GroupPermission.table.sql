CREATE TABLE [dbo].[MNT_GroupPermission](
	[GroupPermissionId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MenuId] [int] NOT NULL,
	[Status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RowId] [int] NULL,
	[Read] [bit] NULL,
	[Write] [bit] NULL,
	[Delete] [bit] NULL,
	[SplPermission] [bit] NULL,
 CONSTRAINT [PK_MNT_GroupPermission] PRIMARY KEY CLUSTERED 
(
	[GroupPermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


