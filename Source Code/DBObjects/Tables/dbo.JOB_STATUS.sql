IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JOB_STATUS]') AND type in (N'U'))
DROP TABLE [dbo].[JOB_STATUS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[JOB_STATUS](
	[ITRACK_ID] [nvarchar] (1000) NULL,
	[PROJECT] [nvarchar] (1000) NULL,
	[FILE_NAME] [nvarchar] (1000) NULL,
	[OWNER] [nvarchar] (1000) NULL,
	[REMARKS] [nvarchar] (4000) NULL,
	[CRITICAL] [nvarchar] (1000) NULL,
	[REVIEWD_BY] [nvarchar] (4000) NULL,
	[STATUS] [nvarchar] (1000) NULL,
	[SHEAT_DATE] [nvarchar] (1000) NULL
) ON [PRIMARY]
GO

