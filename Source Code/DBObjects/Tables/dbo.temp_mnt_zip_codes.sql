IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_mnt_zip_codes]') AND type in (N'U'))
DROP TABLE [dbo].[temp_mnt_zip_codes]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp_mnt_zip_codes](
	[ZIP_CODE] [nvarchar] (10) NULL,
	[STREET_TYPE] [nvarchar] (5) NULL,
	[STREET_NAME] [nvarchar] (40) NULL,
	[DISTRICT] [nvarchar] (20) NULL,
	[CITY] [nvarchar] (40) NULL,
	[STATE] [nvarchar] (5) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[REGION] [nvarchar] (4) NULL,
	[ZIP_CODE_ID] [nvarchar] (4000) NULL
) ON [PRIMARY]
GO

