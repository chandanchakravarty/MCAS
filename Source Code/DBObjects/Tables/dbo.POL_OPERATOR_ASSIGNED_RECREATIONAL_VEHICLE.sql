IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE]') AND type in (N'U'))
DROP TABLE [dbo].[POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[DRIVER_ID] [smallint] NOT NULL,
	[RECREATIONAL_VEH_ID] [smallint] NOT NULL,
	[POL_REC_VEHICLE_PRIN_OCC_ID] [int] NOT NULL
) ON [PRIMARY]
GO

