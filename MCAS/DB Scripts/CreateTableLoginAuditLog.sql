IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoginAuditLog]') AND type in (N'U'))
DROP TABLE [dbo].[LoginAuditLog]
GO

CREATE TABLE [dbo].[LoginAuditLog](
	[SNo] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoggedInUserId] [nvarchar](100) NULL,
	[LoggedInUserName] [nvarchar](100) NULL,
	[LoggedInBranch] [nvarchar](100) NULL,
	[LoggedInCountryId] [int] NULL,
	[LoggedInCountryName] [nvarchar](100) NULL,
	[UserGroupId] [int] NULL,
	[LogInTime] [datetime] NULL,
	[LogOutTime] [datetime] NULL,
 CONSTRAINT [PK_LoginAuditLog] PRIMARY KEY CLUSTERED 
(
	[SNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


