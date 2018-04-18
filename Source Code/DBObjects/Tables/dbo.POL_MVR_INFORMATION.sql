IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_MVR_INFORMATION]') AND type in (N'U'))
DROP TABLE [dbo].[POL_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_MVR_INFORMATION](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[DRIVER_ID] [smallint] NOT NULL,
	[POL_MVR_ID] [int] NOT NULL,
	[MVR_AMOUNT] [decimal] (20,2) NULL,
	[MVR_DEATH] [nvarchar] (1) NULL,
	[MVR_DATE] [datetime] NULL,
	[VIOLATION_ID] [int] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[VIOLATION_SOURCE] [nvarchar] (10) NULL,
	[VERIFIED] [smallint] NULL,
	[VIOLATION_TYPE] [int] NULL,
	[POINTS_ASSIGNED] [int] NULL,
	[ADJUST_VIOLATION_POINTS] [int] NULL,
	[OCCURENCE_DATE] [datetime] NULL,
	[DETAILS] [nvarchar] (500) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_MVR_INFORMATION] ADD CONSTRAINT [PK_POL_MVR_INFORMATION_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_DRIVER_ID_POL_MVR_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[DRIVER_ID] ASC
		,[POL_MVR_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_MVR_INFORMATION] ADD CONSTRAINT [FK_POL_MVR_INFORMATION_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID] FOREIGN KEY
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

