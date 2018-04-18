IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_DEPT_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_DEPT_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_DEPT_LIST](
	[DEPT_ID] [smallint] NOT NULL IDENTITY (1,1),
	[DEPT_CODE] [varchar] (6) NULL,
	[DEPT_NAME] [nvarchar] (70) NULL,
	[DEPT_ADD1] [nvarchar] (70) NULL,
	[DEPT_ADD2] [nvarchar] (70) NULL,
	[DEPT_CITY] [nvarchar] (40) NULL,
	[DEPT_STATE] [nvarchar] (5) NULL,
	[DEPT_ZIP] [varchar] (11) NULL,
	[DEPT_COUNTRY] [nvarchar] (5) NULL,
	[DEPT_PHONE] [nvarchar] (20) NULL,
	[DEPT_EXT] [nvarchar] (5) NULL,
	[DEPT_FAX] [nvarchar] (20) NULL,
	[DEPT_EMAIL] [nvarchar] (50) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_DEPT_LIST] ADD CONSTRAINT [PK_MNT_DEPT_LIST_DEPT_ID] PRIMARY KEY
	CLUSTERED
	(
		[DEPT_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

