IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_PRINT_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_PRINT_JOBS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEMP_PRINT_JOBS](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[DOCUMENT_CODE] [nvarchar] (50) NULL,
	[PRINT_DATETIME] [datetime] NULL,
	[URL_PATH] [nvarchar] (200) NULL,
	[ONDEMAND_FLAG] [nchar] (1) NULL,
	[PRINT_SUCCESSFUL] [nchar] (1) NULL,
	[PRINTED_DATETIME] [datetime] NULL,
	[DUPLEX] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[ENTITY_TYPE] [varchar] (20) NULL,
	[AGENCY_ID] [int] NULL,
	[PRINT_JOBS_ID] [int] NOT NULL IDENTITY (1,1),
	[PICKFROM] [varchar] (2) NULL,
	[FILE_NAME] [varchar] (400) NULL,
	[PROCESS_ID] [int] NULL,
	[IS_PROCESSED] [bit] NULL,
	[ENTITY_ID] [int] NULL,
	[ATTEMPTS] [smallint] NULL,
	[CLAIM_ID] [int] NULL,
	[ACTIVITY_ID] [int] NULL,
	[IS_PRINTED] [varchar] (2) NULL,
	[IS_DOWNLOADED] [varchar] (2) NULL
) ON [PRIMARY]
GO

