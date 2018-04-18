CREATE TABLE [dbo].[LoginAuditLog](
	[SNo] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoggedInUserId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LoggedInUserName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LoggedInBranch] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LoggedInCountryId] [int] NULL,
	[LoggedInCountryName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserGroupId] [int] NULL,
	[LogInTime] [datetime] NULL,
	[LogOutTime] [datetime] NULL,
 CONSTRAINT [PK_LoginAuditLog] PRIMARY KEY CLUSTERED 
(
	[SNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


