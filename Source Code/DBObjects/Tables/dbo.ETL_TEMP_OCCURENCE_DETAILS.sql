IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ETL_TEMP_OCCURENCE_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[ETL_TEMP_OCCURENCE_DETAILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ETL_TEMP_OCCURENCE_DETAILS](
	[LOSS_TYPE] [nvarchar] (255) NULL,
	[LOSS_LOCATION] [nvarchar] (255) NULL,
	[LOSS_DESCRIPTION] [nvarchar] (255) NULL,
	[AUTHORITY_CONTACTED] [nvarchar] (255) NULL,
	[REPORT_NUMBER] [nvarchar] (255) NULL,
	[VIOLATIONS] [nvarchar] (255) NULL,
	[Loss Location Zip] [nvarchar] (255) NULL,
	[Loss Location State] [nvarchar] (255) NULL,
	[Loss Location City] [nvarchar] (255) NULL,
	[POLICY NO#] [nvarchar] (255) NULL,
	[ENDORSEMENT NO#] [nvarchar] (255) NULL,
	[CLAIM NO#] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

