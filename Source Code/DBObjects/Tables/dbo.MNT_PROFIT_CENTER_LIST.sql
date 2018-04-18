IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_PROFIT_CENTER_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_PROFIT_CENTER_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_PROFIT_CENTER_LIST](
	[PC_ID] [smallint] NOT NULL IDENTITY (1,1),
	[PC_CODE] [nvarchar] (6) NULL,
	[PC_NAME] [nvarchar] (70) NULL,
	[PC_ADD1] [nvarchar] (70) NULL,
	[PC_ADD2] [nvarchar] (70) NULL,
	[PC_CITY] [nvarchar] (40) NULL,
	[PC_STATE] [nvarchar] (5) NULL,
	[PC_ZIP] [nvarchar] (11) NULL,
	[PC_COUNTRY] [nvarchar] (5) NULL,
	[PC_PHONE] [nvarchar] (20) NULL,
	[PC_EXT] [nvarchar] (5) NULL,
	[PC_FAX] [nvarchar] (20) NULL,
	[PC_EMAIL] [nvarchar] (50) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_PROFIT_CENTER_LIST] ADD CONSTRAINT [PK_MNT_PROFIT_CENTER_LIST_PC_ID] PRIMARY KEY
	CLUSTERED
	(
		[PC_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

