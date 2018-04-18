IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Query]') AND type in (N'U'))
DROP TABLE [dbo].[Query]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Query](
	[ZIP_CODE_ID] [int] NOT NULL,
	[ZIP_CODE] [date] NULL,
	[STREET_TYPE] [date] NULL,
	[STREET_NAME] [date] NULL,
	[DISTRICT] [date] NULL,
	[CITY] [date] NULL,
	[STATE] [date] NULL,
	[IS_ACTIVE] [nchar] (1) NULL
) ON [PRIMARY]
GO

