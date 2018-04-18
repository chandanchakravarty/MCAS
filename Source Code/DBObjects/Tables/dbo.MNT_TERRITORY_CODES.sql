IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_TERRITORY_CODES]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_TERRITORY_CODES]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_TERRITORY_CODES](
	[LOBID] [smallint] NOT NULL,
	[CITY] [nvarchar] (50) NULL,
	[COUNTY] [nvarchar] (50) NULL,
	[ZIP] [nvarchar] (10) NOT NULL,
	[TERR] [smallint] NOT NULL,
	[STATE] [int] NULL,
	[ZONE] [int] NULL,
	[EARTHQUAKE_ZONE] [smallint] NULL,
	[EFFECTIVE_FROM_DATE] [datetime] NULL,
	[EFFECTIVE_TO_DATE] [datetime] NULL,
	[AUTO_VEHICLE_TYPE] [nvarchar] (20) NULL
) ON [PRIMARY]
GO

