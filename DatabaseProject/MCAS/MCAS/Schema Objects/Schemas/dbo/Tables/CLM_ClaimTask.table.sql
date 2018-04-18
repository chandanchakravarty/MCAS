CREATE TABLE [dbo].[CLM_ClaimTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskNo] [int] NULL,
	[ClaimID] [int] NULL,
	[ActionDue] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[PromtDetails] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[isApprove] [int] NOT NULL,
	[ApproveDate] [datetime] NULL,
	[ApproveBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Remarks] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentId] [int] NULL,
	[AccidentClaimId] [int] NULL,
	ClaimantNames [nvarchar](255) null,
	ClaimsOfficer [int] null,
	[TaskModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CLM_ClaimTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


