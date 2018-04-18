IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_LOOKUP_VALUES_MULTILINGUAL]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_LOOKUP_VALUES_MULTILINGUAL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_LOOKUP_VALUES_MULTILINGUAL](
	[LOOKUP_UNIQUE_ID] [int] NOT NULL,
	[LOOKUP_VALUE_DESC] [nvarchar] (100) NULL,
	[LANG_ID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_LOOKUP_VALUES_MULTILINGUAL] ADD CONSTRAINT [PK_MNT_LOOKUP_VALUES_PT_LOOKUP_UNIQUE_ID] PRIMARY KEY
	CLUSTERED
	(
		[LOOKUP_UNIQUE_ID] ASC
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

