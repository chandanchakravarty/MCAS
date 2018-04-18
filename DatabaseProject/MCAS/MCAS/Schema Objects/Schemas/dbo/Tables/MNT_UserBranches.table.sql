CREATE TABLE [dbo].[MNT_UserBranches](
	[UserId] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BranchCode] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[TranId] [int] IDENTITY(1,1) NOT NULL
)


