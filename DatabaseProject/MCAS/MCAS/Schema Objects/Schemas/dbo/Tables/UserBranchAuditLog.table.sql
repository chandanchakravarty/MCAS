CREATE TABLE [dbo].[UserBranchAuditLog](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PrevBranchCode] [varchar](8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrentBranchCode] [varchar](8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PrevBranchName] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrentBranchName] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


