IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NSS_TERRITORIES_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[NSS_TERRITORIES_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NSS_TERRITORIES_LIST](
	[ZIP] [nvarchar] (10) NULL,
	[TERR] [smallint] NULL,
	[STATE] [nvarchar] (5) NULL,
	[COUNTY] [nvarchar] (50) NULL,
	[CITY] [nvarchar] (50) NULL
) ON [PRIMARY]
GO

