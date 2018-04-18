IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RULE_TABLE]') AND type in (N'U'))
DROP TABLE [dbo].[RULE_TABLE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RULE_TABLE](
	[RULE_ID] [int] NOT NULL,
	[LOB_ID] [int] NULL,
	[SCREEN_ID] [nvarchar] (10) NULL,
	[SCREEN_NAME] [nvarchar] (100) NULL,
	[TABLE_NAME] [nvarchar] (50) NULL,
	[FIELD_NAME] [nvarchar] (100) NULL,
	[RULE_CATEGORY] [nvarchar] (10) NULL,
	[FIELD_VALUE] [nvarchar] (20) NULL,
	[CONDITION_OPERATOR] [varchar] (5) NULL,
	[CONDITION_VALUE] [nvarchar] (20) NULL,
	[RULE_MESSAGE] [nvarchar] (1000) NULL,
	[EXTRA_DEPENDENCY] [char] (1) NULL
) ON [PRIMARY]
GO

