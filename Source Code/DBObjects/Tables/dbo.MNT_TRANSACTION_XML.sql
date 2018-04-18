IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_TRANSACTION_XML]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_TRANSACTION_XML]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_TRANSACTION_XML](
	[TRANS_ID] [int] NOT NULL,
	[TRANS_DETAILS] [text] NULL,
	[IS_VALIDXML] [nchar] (1) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_TRANSACTION_XML] ADD CONSTRAINT [PK_MNT_TRANSACTION_XML_TRANS_ID] PRIMARY KEY
	CLUSTERED
	(
		[TRANS_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_TRANSACTION_XML] ADD CONSTRAINT [FK__MNT_TRANSACTION_XML__TRANS_ID] FOREIGN KEY
	(
		[TRANS_ID]
	)
	REFERENCES [dbo].[MNT_TRANSACTION_LOG]
	(
		[TRANS_ID]
	) 
GO

