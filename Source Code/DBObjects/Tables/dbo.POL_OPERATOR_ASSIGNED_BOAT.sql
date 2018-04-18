IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_OPERATOR_ASSIGNED_BOAT]') AND type in (N'U'))
DROP TABLE [dbo].[POL_OPERATOR_ASSIGNED_BOAT]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_OPERATOR_ASSIGNED_BOAT](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[DRIVER_ID] [smallint] NOT NULL,
	[BOAT_ID] [smallint] NOT NULL,
	[APP_VEHICLE_PRIN_OCC_ID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_OPERATOR_ASSIGNED_BOAT] ADD CONSTRAINT [PK_POL_OPERATOR_ASSIGNED_BOAT] PRIMARY KEY
	NONCLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[DRIVER_ID] ASC
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

ALTER TABLE [dbo].[POL_OPERATOR_ASSIGNED_BOAT] WITH NOCHECK ADD CONSTRAINT [FK_POL_OPERATOR_ASSIGNED_BOAT_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] FOREIGN KEY
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

