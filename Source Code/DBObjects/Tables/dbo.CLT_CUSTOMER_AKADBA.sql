IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLT_CUSTOMER_AKADBA]') AND type in (N'U'))
DROP TABLE [dbo].[CLT_CUSTOMER_AKADBA]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLT_CUSTOMER_AKADBA](
	[AKADBA_ID] [smallint] NOT NULL,
	[CUSTOMER_ID] [int] NOT NULL,
	[AKADBA_TYPE] [int] NOT NULL,
	[AKADBA_NAME] [nvarchar] (255) NULL,
	[AKADBA_ADD] [nvarchar] (70) NULL,
	[AKADBA_CITY] [nvarchar] (40) NULL,
	[AKADBA_STATE] [int] NULL,
	[AKADBA_ZIP] [nvarchar] (11) NULL,
	[AKADBA_COUNTRY] [int] NULL,
	[AKADBA_WEBSITE] [nvarchar] (150) NULL,
	[AKADBA_EMAIL] [nvarchar] (50) NULL,
	[AKADBA_LEGAL_ENTITY_CODE] [int] NULL,
	[AKADBA_NAME_ON_FORM] [nchar] (1) NULL,
	[AKADBA_MEMO] [nvarchar] (35) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[AKADBA_ADD2] [nvarchar] (140) NULL,
	[AKADBA_DISP_ORDER] [smallint] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLT_CUSTOMER_AKADBA] ADD CONSTRAINT [PK_CLT_CUSTOMER_AKADBA_AKADBA_ID] PRIMARY KEY
	CLUSTERED
	(
		[AKADBA_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLT_CUSTOMER_AKADBA] ADD CONSTRAINT [FK_CLT_CUSTOMER_AKADBA_CLT_CUSTOMER_LIST_CUSTOMER_ID] FOREIGN KEY
	(
		[CUSTOMER_ID]
	)
	REFERENCES [dbo].[CLT_CUSTOMER_LIST]
	(
		[CUSTOMER_ID]
	) 
GO

