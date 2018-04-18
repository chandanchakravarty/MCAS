CREATE TABLE [dbo].[MNT_AttachmentList](
	[AttachId] [int] IDENTITY(1,1) NOT NULL,
	[AttachLoc] [nchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachEntId] [int] NULL,
	[AttachFileName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachDateTime] [datetime] NULL,
	[AttachFileDesc] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachPolicyId] [int] NULL,
	[ClaimantName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachFileType] [nvarchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachEntityType] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AttachType] [nvarchar](50) NULL,
	[FilePath] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[ClaimID] [int] NULL,
	[AccidentId] [int] NULL,
 CONSTRAINT [PK_MNT_AttachmentList_AttachId] PRIMARY KEY CLUSTERED 
(
	[AttachId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_MNT_AttachmentList_MNT_InsruanceM] FOREIGN KEY([AttachPolicyId])
REFERENCES [dbo].[MNT_InsruanceM] ([PolicyId])
)


