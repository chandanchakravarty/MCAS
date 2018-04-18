IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLT_CUSTOMER_EMAIL]') AND type in (N'U'))
DROP TABLE [dbo].[CLT_CUSTOMER_EMAIL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLT_CUSTOMER_EMAIL](
	[EMAIL_ROW_ID] [smallint] NOT NULL IDENTITY (1,1),
	[CUSTOMER_ID] [int] NOT NULL,
	[EMAIL_FROM_NAME] [nvarchar] (100) NULL,
	[EMAIL_FROM] [nvarchar] (100) NULL,
	[EMAIL_TO] [nvarchar] (100) NULL,
	[EMAIL_RECIPIENTS] [nvarchar] (2000) NULL,
	[EMAIL_SUBJECT] [nvarchar] (255) NULL,
	[EMAIL_MESSAGE] [nvarchar] (4000) NULL,
	[EMAIL_ATTACH_PATH] [nvarchar] (255) NULL,
	[EMAIL_SEND_DATE] [datetime] NULL,
	[DIARY_ITEM_REQ] [char] (1) NULL,
	[FOLLOW_UP_DATE] [datetime] NULL,
	[POLICY_NUMBER] [nvarchar] (100) NULL,
	[APP_NUMBER] [nvarchar] (100) NULL,
	[QUOTE] [nvarchar] (100) NULL,
	[CLAIM_NUMBER] [nvarchar] (10) NULL,
	[DIARY_ITEM_TO] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLT_CUSTOMER_EMAIL] ADD CONSTRAINT [PK_CLT_CUSTOMER_EMAIL_EMAIL_ROW_ID] PRIMARY KEY
	CLUSTERED
	(
		[EMAIL_ROW_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLT_CUSTOMER_EMAIL] ADD CONSTRAINT [FK_CLT_CUSTOMER_EMAIL_CLT_CUSTOMER_LIST_CUSTOMER_ID] FOREIGN KEY
	(
		[CUSTOMER_ID]
	)
	REFERENCES [dbo].[CLT_CUSTOMER_LIST]
	(
		[CUSTOMER_ID]
	) 
GO

