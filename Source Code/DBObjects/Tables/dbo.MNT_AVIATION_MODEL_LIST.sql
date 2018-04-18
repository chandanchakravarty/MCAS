IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_AVIATION_MODEL_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_AVIATION_MODEL_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_AVIATION_MODEL_LIST](
	[ID] [smallint] NOT NULL,
	[MODEL] [nvarchar] (100) NOT NULL,
	[TYPE_DESIGNATOR] [nvarchar] (100) NOT NULL,
	[AIRCRAFT_CATEGORY] [smallint] NOT NULL,
	[ENGINE_COUNT] [smallint] NOT NULL,
	[MAKER_ID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_AVIATION_MODEL_LIST] ADD CONSTRAINT [PK_MNT_AVIATION_MODEL_LIST_ID] PRIMARY KEY
	CLUSTERED
	(
		[ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

