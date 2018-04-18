CREATE TABLE [dbo].[CLM_Notes](
	[NoteId] [int] IDENTITY(1,1) NOT NULL,
	[PolicyId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	[NoteCode] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoteDate] [datetime] NOT NULL,
	[NoteTime] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ImageCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ImageId] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NotesDescription] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[URL_PATH] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentId] [int] NULL,
	[ClaimantNames] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CLM_Notes] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_Notes_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
)


