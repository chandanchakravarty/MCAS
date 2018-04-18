IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Temp_who]') AND type in (N'U'))
DROP TABLE [dbo].[Temp_who]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Temp_who](
	[WORKFLOW_ID] [int] NOT NULL,
	[GROUP_ID] [varchar] (20) NULL,
	[SCREEN_ID] [varchar] (20) NULL,
	[WORKFLOW_SCREENS] [varchar] (255) NULL,
	[TABLE_NAME] [varchar] (255) NULL,
	[PRIMARY_KEYS] [varchar] (255) NULL,
	[WORKFLOW_ORDER] [int] NULL,
	[WORKFLOW_TEXT] [varchar] (255) NULL,
	[IS_ACTIVE] [bit] NULL,
	[TAB_NUMBER] [smallint] NULL,
	[MENU_ID] [int] NULL
) ON [PRIMARY]
GO

