IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Copy of View_India_Fim]') AND type in (N'U'))
DROP TABLE [dbo].[Copy of View_India_Fim]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Copy of View_India_Fim](
	[ID] [int] NOT NULL,
	[ZIP_CODE_ID] [int] NULL,
	[ZIP_CODES] [int] NULL,
	[STREET_TYPE] [nvarchar] (255) NULL,
	[STREET_NAME] [nvarchar] (255) NULL,
	[DISTRICT_NAME] [nvarchar] (255) NULL,
	[CITY_NAME] [nvarchar] (255) NULL,
	[STATE] [nvarchar] (255) NULL,
	[IS_ACTIVE] [nvarchar] (255) NULL,
	[REGION] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

