IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_SUBLINE_CODE_MULTILINGUAL]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_SUBLINE_CODE_MULTILINGUAL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_SUBLINE_CODE_MULTILINGUAL](
	[SUBLINE_CODE_ID] [smallint] NOT NULL,
	[SUBLINE_CODE] [nvarchar] (8) NULL,
	[SUBLINE_CODE_DESC] [nvarchar] (75) NULL,
	[LANG_ID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_SUBLINE_CODE_MULTILINGUAL] ADD CONSTRAINT [PK_MNT_SUBLINE_CODE_MULTILINGUAL_SUBLINE_CODE_ID_LANG_ID] PRIMARY KEY
	CLUSTERED
	(
		[SUBLINE_CODE_ID] ASC
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

