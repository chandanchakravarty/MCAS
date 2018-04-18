IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCKUP_MNT_DIV_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[BCKUP_MNT_DIV_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BCKUP_MNT_DIV_LIST](
	[DIV_ID] [smallint] NOT NULL IDENTITY (1,1),
	[DIV_CODE] [nvarchar] (6) NULL,
	[DIV_NAME] [nvarchar] (70) NULL,
	[DIV_ADD1] [nvarchar] (70) NULL,
	[DIV_ADD2] [nvarchar] (70) NULL,
	[DIV_CITY] [nvarchar] (40) NULL,
	[DIV_STATE] [nvarchar] (5) NULL,
	[DIV_ZIP] [nvarchar] (11) NULL,
	[DIV_COUNTRY] [nvarchar] (5) NULL,
	[DIV_PHONE] [nvarchar] (20) NULL,
	[DIV_EXT] [nvarchar] (5) NULL,
	[DIV_FAX] [nvarchar] (20) NULL,
	[DIV_EMAIL] [nvarchar] (50) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[NAIC_CODE] [nvarchar] (10) NULL,
	[BRANCH_CODE] [nvarchar] (10) NULL,
	[BRANCH_CNPJ] [nvarchar] (30) NULL,
	[DATE_OF_BIRTH] [datetime] NULL,
	[REGIONAL_IDENTIFICATION] [nvarchar] (40) NULL,
	[ACTIVITY] [int] NULL,
	[REG_ID_ISSUE_DATE] [datetime] NULL,
	[REG_ID_ISSUE] [nvarchar] (40) NULL
) ON [PRIMARY]
GO

