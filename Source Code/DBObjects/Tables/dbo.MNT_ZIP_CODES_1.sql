IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_ZIP_CODES_1]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_ZIP_CODES_1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_ZIP_CODES_1](
	[ZIP_CODE_ID] [int] NOT NULL IDENTITY (1,1),
	[ZIP_CODE] [nvarchar] (10) NULL,
	[STREET_TYPE] [nvarchar] (5) NULL,
	[STREET_NAME] [nvarchar] (40) NULL,
	[DISTRICT] [nvarchar] (20) NULL,
	[CITY] [nvarchar] (40) NULL,
	[STATE] [nvarchar] (5) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[REGION] [nvarchar] (4) NULL
) ON [PRIMARY]
GO

