IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mnt_vin_master11202009]') AND type in (N'U'))
DROP TABLE [dbo].[mnt_vin_master11202009]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[mnt_vin_master11202009](
	[VIN] [nvarchar] (510) NULL,
	[MAKE_CODE] [nvarchar] (510) NULL,
	[MODEL_YEAR] [nvarchar] (4) NULL,
	[MAKE_NAME] [nvarchar] (510) NULL,
	[SERIES_NAME] [nvarchar] (510) NULL,
	[BODY_TYPE] [nvarchar] (510) NULL,
	[ANTI_LCK_BRAKES] [nvarchar] (510) NULL,
	[AIRBAG] [nvarchar] (2) NULL,
	[SYMBOL] [nvarchar] (2) NULL
) ON [PRIMARY]
GO

