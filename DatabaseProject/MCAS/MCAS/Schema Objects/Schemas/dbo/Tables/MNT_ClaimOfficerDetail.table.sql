CREATE TABLE [dbo].[MNT_ClaimOfficerDetail](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[UserGroup] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimentOfficerName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Department] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastAssignmentDate] [datetime] NOT NULL,
	[Type] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimNo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MNT_ClaimOfficerDetail] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


