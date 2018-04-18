IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES]') AND type in (N'U'))
DROP TABLE [dbo].[POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[POLICY_NUMBER] [nvarchar] (75) NULL,
	[COVERAGE_DESC] [nvarchar] (150) NULL,
	[COVERAGE_AMOUNT] [nvarchar] (100) NULL,
	[POLICY_TEXT] [nvarchar] (75) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[IS_POLICY] [bit] NULL,
	[COV_CODE] [nvarchar] (10) NULL,
	[COVERAGE_TYPE] [nvarchar] (10) NULL,
	[POLICY_COMPANY] [nvarchar] (150) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES] ADD CONSTRAINT [PK_POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
		,FILLFACTOR = 90
	) ON [PRIMARY]
GO

