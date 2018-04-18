CREATE TABLE [dbo].[ClaimAccidentHistoryDetails](
	[ClaimAccStatusId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NULL,
	[ClaimId] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[StatusChangeDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[CreatedDate] [datetime] NULL,
	[Remarks] [nvarchar](500) NULL,
 CONSTRAINT [PK_ClaimAccidentHistoryDetails] PRIMARY KEY CLUSTERED 
(
	[ClaimAccStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
CONSTRAINT [FK_ClaimAccidentHistoryDetails_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
)
