CREATE TABLE [dbo].[MNT_GroupsMaster](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GroupName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeptCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccessLevel] [smallint] NULL,
	[IsActive] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[RoleCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_GroupsMaster_GroupId] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


