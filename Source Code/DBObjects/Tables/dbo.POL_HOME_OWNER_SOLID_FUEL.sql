IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_HOME_OWNER_SOLID_FUEL]') AND type in (N'U'))
DROP TABLE [dbo].[POL_HOME_OWNER_SOLID_FUEL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_HOME_OWNER_SOLID_FUEL](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[FUEL_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NULL,
	[SUB_LOC_ID] [smallint] NULL,
	[MANUFACTURER] [nvarchar] (100) NULL,
	[BRAND_NAME] [nvarchar] (75) NULL,
	[MODEL_NUMBER] [nvarchar] (35) NULL,
	[FUEL] [nvarchar] (35) NULL,
	[STOVE_TYPE] [nvarchar] (5) NULL,
	[HAVE_LABORATORY_LABEL] [nchar] (1) NULL,
	[IS_UNIT] [nvarchar] (5) NULL,
	[UNIT_OTHER_DESC] [nvarchar] (35) NULL,
	[CONSTRUCTION] [nvarchar] (5) NULL,
	[LOCATION] [nvarchar] (5) NULL,
	[LOC_OTHER_DESC] [nvarchar] (35) NULL,
	[YEAR_DEVICE_INSTALLED] [smallint] NULL,
	[WAS_PROF_INSTALL_DONE] [nchar] (1) NULL,
	[INSTALL_INSPECTED_BY] [nvarchar] (5) NULL,
	[INSTALL_OTHER_DESC] [nvarchar] (35) NULL,
	[HEATING_USE] [nvarchar] (5) NULL,
	[HEATING_SOURCE] [nvarchar] (5) NULL,
	[OTHER_DESC] [nvarchar] (65) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[STOVE_INSTALLATION_CONFORM_SPECIFICATIONS] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_HOME_OWNER_SOLID_FUEL] ADD CONSTRAINT [PK_POL_HOME_OWNER_SOLID_FUEL_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_FUEL_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[FUEL_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_HOME_OWNER_SOLID_FUEL] WITH NOCHECK ADD CONSTRAINT [FK_POL_HOME_OWNER_SOLID_FUEL_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] FOREIGN KEY
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

