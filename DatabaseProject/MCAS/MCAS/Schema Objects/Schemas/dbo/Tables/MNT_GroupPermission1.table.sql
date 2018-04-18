CREATE TABLE [dbo].[MNT_GroupPermission1](
	[GroupId] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MenuId] [int] NOT NULL,
	[Status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RowId] [int] NULL,
	[Read] [bit] NULL,
	[Write] [bit] NULL,
	[Delete] [bit] NULL,
	[SplPermission] [bit] NULL
)


