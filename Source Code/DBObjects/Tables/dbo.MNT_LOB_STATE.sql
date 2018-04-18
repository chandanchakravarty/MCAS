IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_LOB_STATE]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_LOB_STATE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_LOB_STATE](
	[LOB_ID] [smallint] NOT NULL,
	[STATE_ID] [smallint] NOT NULL,
	[PARENT_LOB] [smallint] NULL
) ON [PRIMARY]
GO

