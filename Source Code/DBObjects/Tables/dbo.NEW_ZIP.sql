IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NEW_ZIP]') AND type in (N'U'))
DROP TABLE [dbo].[NEW_ZIP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NEW_ZIP](
	[ZIP_CODE_ID] [float] (53) NULL,
	[ZIP_CODE] [nvarchar] (255) NULL,
	[STREET_TYPE] [nvarchar] (255) NULL,
	[STREET_NAME] [nvarchar] (255) NULL,
	[DISTRICT_NAME] [nvarchar] (255) NULL,
	[CITY_NAME] [nvarchar] (255) NULL,
	[STATE] [nvarchar] (255) NULL,
	[IS_ACTIVE] [nvarchar] (255) NULL,
	[REGION] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

