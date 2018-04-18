IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[View_India_Fim]') AND type in (N'U'))
DROP TABLE [dbo].[View_India_Fim]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[View_India_Fim](
	[ZIP_CODE_ID] [varchar] (50) NULL,
	[ZIP_CODE] [varchar] (50) NULL,
	[STREET_TYPE] [varchar] (50) NULL,
	[STREET_NAME] [varchar] (50) NULL,
	[DISTRICT_NAME] [varchar] (50) NULL,
	[CITY_NAME] [varchar] (50) NULL,
	[STATE] [varchar] (50) NULL,
	[IS_ACTIVE] [varchar] (50) NULL,
	[REGION] [varchar] (50) NULL
) ON [PRIMARY]
GO

