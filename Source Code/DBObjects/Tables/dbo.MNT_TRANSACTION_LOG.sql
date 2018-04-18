IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_TRANSACTION_LOG]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_TRANSACTION_LOG]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_TRANSACTION_LOG](
	[TRANS_ID] [int] NOT NULL,
	[TRANS_TYPE_ID] [smallint] NULL,
	[CLIENT_ID] [int] NULL,
	[POLICY_ID] [smallint] NULL,
	[POLICY_VER_TRACKING_ID] [smallint] NULL,
	[RECORDED_BY] [smallint] NULL,
	[RECORDED_BY_NAME] [nvarchar] (75) NULL,
	[RECORD_DATE_TIME] [datetime] NULL,
	[TRANS_DESC] [text] NULL,
	[ENTITY_ID] [int] NULL,
	[ENTITY_TYPE] [nvarchar] (5) NULL,
	[IS_COMPLETED] [nchar] (1) NULL,
	[APP_ID] [int] NULL,
	[APP_VERSION_ID] [smallint] NULL,
	[QUOTE_ID] [int] NULL,
	[QUOTE_VERSION_ID] [smallint] NULL,
	[ADDITIONAL_INFO] [varchar] (MAX) NULL,
	[LANG_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_TRANSACTION_LOG] ADD CONSTRAINT [PK_MNT_TRANSACTION_LOG_TRANS_ID] PRIMARY KEY
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

