IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_PROCESS_LOG]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_PROCESS_LOG]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLM_PROCESS_LOG](
	[PROCESS_ID] [int] NOT NULL,
	[CLAIM_ID] [int] NULL,
	[ACTIVITY_ID] [int] NULL,
	[PAYEE_ID] [int] NULL,
	[PROCESS_TYPE] [nvarchar] (20) NULL,
	[DOC_TEXT] [text] NULL,
	[LETTER_SEQUENCE_NO] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_PROCESS_LOG] ADD CONSTRAINT [PK_CLM_PROCESS_LOG_PROCESS_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[PROCESS_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_PROCESS_LOG] WITH NOCHECK ADD CONSTRAINT [FK_CLM_PROCESS_LOG_CLAIM_ID] FOREIGN KEY
	(
		[CLAIM_ID]
	)
	REFERENCES [dbo].[CLM_CLAIM_INFO]
	(
		[CLAIM_ID]
	) 
GO

