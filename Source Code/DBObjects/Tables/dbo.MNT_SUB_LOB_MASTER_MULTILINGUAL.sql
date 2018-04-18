IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_SUB_LOB_MASTER_MULTILINGUAL]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_SUB_LOB_MASTER_MULTILINGUAL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_SUB_LOB_MASTER_MULTILINGUAL](
	[LOB_ID] [smallint] NOT NULL,
	[SUB_LOB_ID] [smallint] NOT NULL,
	[SUB_LOB_DESC] [nvarchar] (70) NULL,
	[SUB_LOB_CODE] [nvarchar] (20) NULL,
	[LANG_ID] [smallint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_SUB_LOB_MASTER_MULTILINGUAL] ADD CONSTRAINT [PK_MNT_SUB_LOB_MASTER_MULTILINGUAL_LOB_ID_SUB_LOB_ID_LANG_ID] PRIMARY KEY
	CLUSTERED
	(
		[LOB_ID] ASC
		,[SUB_LOB_ID] ASC
		,[LANG_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

