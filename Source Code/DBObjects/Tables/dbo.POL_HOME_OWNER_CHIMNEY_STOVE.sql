IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_HOME_OWNER_CHIMNEY_STOVE]') AND type in (N'U'))
DROP TABLE [dbo].[POL_HOME_OWNER_CHIMNEY_STOVE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_HOME_OWNER_CHIMNEY_STOVE](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[FUEL_ID] [smallint] NOT NULL,
	[IS_STOVE_VENTED] [nchar] (1) NULL,
	[OTHER_DEVICES_ATTACHED] [nvarchar] (100) NULL,
	[CONSTRUCT_OTHER_DESC] [nvarchar] (100) NULL,
	[IS_TILE_FLUE_LINING] [nchar] (1) NULL,
	[IS_CHIMNEY_GROUND_UP] [nchar] (1) NULL,
	[CHIMNEY_INST_AFTER_HOUSE_BLT] [nchar] (1) NULL,
	[IS_CHIMNEY_COVERED] [nchar] (1) NULL,
	[DIST_FROM_SMOKE_PIPE] [smallint] NULL,
	[THIMBLE_OR_MATERIAL] [nvarchar] (100) NULL,
	[STOVE_PIPE_IS] [nvarchar] (5) NULL,
	[DOES_SMOKE_PIPE_FIT] [nchar] (1) NULL,
	[SMOKE_PIPE_WASTE_HEAT] [nchar] (1) NULL,
	[STOVE_CONN_SECURE] [nchar] (1) NULL,
	[SMOKE_PIPE_PASS] [nchar] (1) NULL,
	[SELECT_PASS] [nvarchar] (5) NULL,
	[PASS_INCHES] [decimal] (25,2) NULL,
	[CHIMNEY_CONSTRUCTION] [nchar] (5) NULL,
	[IS_ACTIVE] [nchar] (2) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_HOME_OWNER_CHIMNEY_STOVE] ADD CONSTRAINT [PK_POL_HOME_OWNER_CHIMNEY_STOVE_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_FUEL_ID] PRIMARY KEY
	NONCLUSTERED
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

ALTER TABLE [dbo].[POL_HOME_OWNER_CHIMNEY_STOVE] ADD CONSTRAINT [FK_POL_HOME_OWNER_CHIMNEY_STOVE_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_FUEL_ID] FOREIGN KEY
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID],
		[FUEL_ID]
	)
	REFERENCES [dbo].[POL_HOME_OWNER_SOLID_FUEL]
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID],
		[FUEL_ID]
	) 
GO

