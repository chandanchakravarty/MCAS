IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UTL_RECEIVED_XML_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[UTL_RECEIVED_XML_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UTL_RECEIVED_XML_LIST](
	[RECEIVING_ID] [int] NOT NULL,
	[UPLOADED_FILE_NAME] [nvarchar] (255) NULL,
	[FULL_FILE_NAME] [nvarchar] (512) NULL,
	[IS_VALID] [bit] NULL,
	[RECORD_DATE] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UTL_RECEIVED_XML_LIST] ADD CONSTRAINT [PK_UTL_RECEIVED_XML_LIST_RECEIVING_ID] PRIMARY KEY
	CLUSTERED
	(
		[RECEIVING_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

