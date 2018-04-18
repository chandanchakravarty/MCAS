CREATE TABLE [dbo].[Claim_ReAssignmentDairy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DairyId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TypeOfAssignment] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReAssignTo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DairyFromUser] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReAssignDateFrom] [datetime] NULL,
	[ReAssignDateTo] [datetime] NULL,
	[Remark] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailId] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[ClaimId] [int] NULL,
	[AccidentClaimId] [int] NULL,
 CONSTRAINT [PK_Claim_ReAssignmentDairy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


