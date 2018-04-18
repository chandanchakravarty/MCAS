IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_DEFAULT_HIERARCHY]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_DEFAULT_HIERARCHY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_DEFAULT_HIERARCHY](
	[REC_ID] [int] NOT NULL,
	[AGENCY_ID] [int] NULL,
	[DIV_ID] [int] NULL,
	[DEPT_ID] [int] NULL,
	[PC_ID] [int] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_DEFAULT_HIERARCHY] ADD CONSTRAINT [PK_MNT_DEFAULT_HIERARCHY_REC_ID] PRIMARY KEY
	CLUSTERED
	(
		[REC_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

