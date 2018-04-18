IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_WATERCRAFT_INFO]') AND type in (N'U'))
DROP TABLE [dbo].[POL_WATERCRAFT_INFO]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_WATERCRAFT_INFO](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[BOAT_ID] [smallint] NOT NULL,
	[BOAT_NO] [int] NULL,
	[BOAT_NAME] [nvarchar] (25) NULL,
	[YEAR] [int] NULL,
	[MAKE] [nvarchar] (75) NULL,
	[MODEL] [nvarchar] (75) NULL,
	[HULL_ID_NO] [nvarchar] (75) NULL,
	[STATE_REG] [nvarchar] (5) NULL,
	[HULL_MATERIAL] [int] NULL,
	[FUEL_TYPE] [int] NULL,
	[DATE_PURCHASED] [datetime] NULL,
	[LENGTH] [nvarchar] (10) NULL,
	[MAX_SPEED] [decimal] (10,0) NULL,
	[BERTH_LOC] [nvarchar] (100) NULL,
	[WATERS_NAVIGATED] [nvarchar] (250) NULL,
	[TERRITORY] [nvarchar] (25) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[TYPE_OF_WATERCRAFT] [char] (10) NULL,
	[INSURING_VALUE] [decimal] (10,2) NULL,
	[WATERCRAFT_HORSE_POWER] [int] NULL,
	[TWIN_SINGLE] [int] NULL,
	[DESC_OTHER_WATERCRAFT] [nvarchar] (150) NULL,
	[INCHES] [nvarchar] (10) NULL,
	[LORAN_NAV_SYSTEM] [smallint] NULL,
	[DIESEL_ENGINE] [smallint] NULL,
	[SHORE_STATION] [smallint] NULL,
	[HALON_FIRE_EXT_SYSTEM] [smallint] NULL,
	[DUAL_OWNERSHIP] [smallint] NULL,
	[REMOVE_SAILBOAT] [smallint] NULL,
	[COV_TYPE_BASIS] [int] NULL,
	[PHOTO_ATTACHED] [int] NULL,
	[MARINE_SURVEY] [int] NULL,
	[DATE_MARINE_SURVEY] [datetime] NULL,
	[LOCATION_ADDRESS] [nvarchar] (200) NULL,
	[LOCATION_CITY] [nvarchar] (50) NULL,
	[LOCATION_STATE] [nvarchar] (50) NULL,
	[LOCATION_ZIP] [nvarchar] (20) NULL,
	[LAY_UP_PERIOD_FROM_DAY] [int] NULL,
	[LAY_UP_PERIOD_FROM_MONTH] [int] NULL,
	[LAY_UP_PERIOD_TO_DAY] [int] NULL,
	[LAY_UP_PERIOD_TO_MONTH] [int] NULL,
	[LOSSREPORT_ORDER] [int] NULL,
	[LOSSREPORT_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_WATERCRAFT_INFO] ADD CONSTRAINT [PK_POL_WATERCRAFT_INFO_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_BOAT_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[BOAT_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_WATERCRAFT_INFO] WITH NOCHECK ADD CONSTRAINT [FK_POL_WATERCRAFT_INFO_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] FOREIGN KEY
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID]
	)
	REFERENCES [dbo].[POL_CUSTOMER_POLICY_LIST]
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID]
	) 
GO

