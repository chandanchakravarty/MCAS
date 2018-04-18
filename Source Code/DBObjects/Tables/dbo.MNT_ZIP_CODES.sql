IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_ZIP_CODES]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_ZIP_CODES]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_ZIP_CODES](
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
EXEC sp_addextendedproperty @name=N'caption', @value='Zip Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_ZIP_CODES', @level2type=N'COLUMN',@level2name=N'ZIP_CODE'
GO

ALTER TABLE [dbo].[MNT_ZIP_CODES] ADD CONSTRAINT [MNT_ZIP_CODES_ZIP_CODE_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[ZIP_CODE_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

