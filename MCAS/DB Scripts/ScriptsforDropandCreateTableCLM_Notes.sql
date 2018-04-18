IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Notes_CLM_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Notes]'))
ALTER TABLE [dbo].[CLM_Notes] DROP CONSTRAINT [FK_CLM_Notes_CLM_Notes]
GO

/****** Object:  Table [dbo].[CLM_Notes]    Script Date: 07/03/2014 20:18:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_Notes]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_Notes]
GO

USE [CDGI]
GO

/****** Object:  Table [dbo].[CLM_Notes]    Script Date: 07/03/2014 20:18:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CLM_Notes](
	[NoteId] [int] IDENTITY(1,1) NOT NULL,
	[PolicyId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	[NoteCode] [nvarchar](100) NOT NULL,
	[NoteDate] [datetime] NOT NULL,
	[NoteTime] [nvarchar](20) NOT NULL,
	[ImageCode] [nvarchar](50) NOT NULL,
	[ImageId] [nvarchar](200) NULL,
	[NotesDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_CLM_Notes] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CLM_Notes]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Notes_CLM_Notes] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[CLM_Notes] CHECK CONSTRAINT [FK_CLM_Notes_CLM_Notes]
GO


