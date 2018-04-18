IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MyData]') AND type in (N'U'))
DROP TABLE [dbo].[MyData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MyData](
	[LOOKUP_UNIQUE_ID] [float] (53) NULL,
	[LOOKUP_ID] [float] (53) NULL,
	[LOOKUP_VALUE_ID] [float] (53) NULL,
	[LOOKUP_VALUE_CODE] [nvarchar] (255) NULL,
	[LOOKUP_VALUE_DESC] [nvarchar] (255) NULL,
	[LOOKUP_SYS_DEF] [nvarchar] (255) NULL,
	[IS_ACTIVE] [nvarchar] (255) NULL,
	[LAST_UPDATED_DATETIME] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

