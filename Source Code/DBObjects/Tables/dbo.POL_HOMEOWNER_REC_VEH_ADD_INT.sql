IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_HOMEOWNER_REC_VEH_ADD_INT]') AND type in (N'U'))
DROP TABLE [dbo].[POL_HOMEOWNER_REC_VEH_ADD_INT]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_HOMEOWNER_REC_VEH_ADD_INT](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[HOLDER_ID] [int] NULL,
	[REC_VEH_ID] [smallint] NOT NULL,
	[MEMO] [nvarchar] (250) NULL,
	[NATURE_OF_INTEREST] [nvarchar] (30) NULL,
	[RANK] [smallint] NULL,
	[LOAN_REF_NUMBER] [nvarchar] (75) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[ADD_INT_ID] [int] NOT NULL CONSTRAINT [DF__POL_HOME__ADD_I__3431ACB9] DEFAULT (0),
	[HOLDER_NAME] [nvarchar] (70) NULL,
	[HOLDER_ADD1] [nvarchar] (140) NULL,
	[HOLDER_ADD2] [nvarchar] (140) NULL,
	[HOLDER_CITY] [nvarchar] (80) NULL,
	[HOLDER_COUNTRY] [nvarchar] (10) NULL,
	[HOLDER_STATE] [nvarchar] (10) NULL,
	[HOLDER_ZIP] [nvarchar] (11) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_HOMEOWNER_REC_VEH_ADD_INT] ADD CONSTRAINT [PK_POL_HOMEOWNER_REC_VEH_ADD_INT_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_REC_VEH_ID_ADD_INT_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[REC_VEH_ID] ASC
		,[ADD_INT_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_HOMEOWNER_REC_VEH_ADD_INT] ADD CONSTRAINT [FK_POL_HOMEOWNER_REC_VEH_ADD_INT_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_REC_VEH_ID_POL_HOME_OWNER_RECREATIONAL_VEHICLES] FOREIGN KEY
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID],
		[REC_VEH_ID]
	)
	REFERENCES [dbo].[POL_HOME_OWNER_RECREATIONAL_VEHICLES]
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID],
		[REC_VEH_ID]
	) 
GO

