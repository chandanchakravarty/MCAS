IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_ADJUSTER]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_ADJUSTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLM_ADJUSTER](
	[ADJUSTER_ID] [int] NOT NULL,
	[ADJUSTER_TYPE] [int] NOT NULL,
	[ADJUSTER_NAME] [nvarchar] (60) NULL,
	[ADJUSTER_CODE] [nvarchar] (10) NULL,
	[SUB_ADJUSTER] [nvarchar] (60) NULL,
	[SUB_ADJUSTER_LEGAL_NAME] [nvarchar] (60) NULL,
	[SUB_ADJUSTER_ADDRESS1] [nvarchar] (150) NULL,
	[SUB_ADJUSTER_ADDRESS2] [nvarchar] (150) NULL,
	[SUB_ADJUSTER_CITY] [nvarchar] (70) NULL,
	[SUB_ADJUSTER_STATE] [nvarchar] (10) NULL,
	[SUB_ADJUSTER_ZIP] [nvarchar] (20) NULL,
	[SUB_ADJUSTER_PHONE] [nvarchar] (15) NULL,
	[SUB_ADJUSTER_FAX] [nvarchar] (15) NULL,
	[SUB_ADJUSTER_EMAIL] [nvarchar] (50) NULL,
	[SUB_ADJUSTER_WEBSITE] [nvarchar] (150) NULL,
	[SUB_ADJUSTER_NOTES] [nvarchar] (1000) NULL,
	[IS_ACTIVE] [char] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[SUB_ADJUSTER_COUNTRY] [nvarchar] (10) NULL,
	[SUB_ADJUSTER_CONTACT_NAME] [nvarchar] (60) NULL,
	[SA_ADDRESS1] [nvarchar] (150) NULL,
	[SA_ADDRESS2] [nvarchar] (150) NULL,
	[SA_CITY] [nvarchar] (70) NULL,
	[SA_COUNTRY] [nvarchar] (2) NULL,
	[SA_STATE] [int] NULL,
	[SA_ZIPCODE] [nvarchar] (20) NULL,
	[SA_PHONE] [nvarchar] (15) NULL,
	[SA_FAX] [nvarchar] (15) NULL,
	[LOB_ID] [nvarchar] (120) NULL,
	[USER_ID] [int] NULL,
	[CLAIM_COUNTER] [int] NULL,
	[DISPLAY_ON_CLAIM] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_ADJUSTER] ADD CONSTRAINT [PK_CLM_ADJUSTER_ADJUSTER_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[ADJUSTER_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

