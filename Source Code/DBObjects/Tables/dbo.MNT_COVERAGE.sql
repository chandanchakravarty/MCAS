IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_COVERAGE]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_COVERAGE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_COVERAGE](
	[COV_ID] [int] NOT NULL,
	[COV_REF_CODE] [nvarchar] (10) NULL,
	[COV_CODE] [nvarchar] (10) NULL,
	[COV_DES] [nvarchar] (1000) NULL,
	[STATE_ID] [smallint] NULL,
	[LOB_ID] [smallint] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[IS_DEFAULT] [bit] NULL,
	[TYPE] [nchar] (1) NULL,
	[PURPOSE] [nchar] (1) NULL,
	[LIMIT_TYPE] [nchar] (1) NULL,
	[DEDUCTIBLE_TYPE] [nchar] (1) NULL,
	[IsLimitApplicable] [nchar] (1) NULL,
	[IsDeductApplicable] [nchar] (1) NULL,
	[IS_MANDATORY] [nchar] (1) NULL,
	[INCLUDED] [decimal] (18,2) NULL,
	[COVERAGE_TYPE] [nchar] (10) NULL,
	[RANK] [decimal] (7,2) NULL,
	[EFFECTIVE_FROM_DATE] [datetime] NULL,
	[EFFECTIVE_TO_DATE] [datetime] NULL,
	[DISABLED_DATE] [datetime] NULL,
	[ISADDDEDUCTIBLE_APP] [nchar] (1) NULL,
	[ADDDEDUCTIBLE_TYPE] [nchar] (1) NULL,
	[COMPONENT_CODE] [nvarchar] (100) NULL,
	[FORM_NUMBER] [nvarchar] (20) NULL,
	[EDITION_DATE] [datetime] NULL,
	[NSS_VALUE_CODE] [nvarchar] (40) NULL,
	[NSS_SUBLINE_CODE] [nvarchar] (40) NULL,
	[NSS_LOSSTYPE_CODE] [nvarchar] (10) NULL,
	[REINSURANCE_LOB] [int] NULL,
	[REINSURANCE_COV] [int] NULL,
	[ASLOB] [int] NULL,
	[REINSURANCE_CALC] [int] NULL,
	[COMM_VEHICLE] [int] NULL,
	[COMM_REIN_COV_CAT] [int] NULL,
	[REIN_ASLOB] [int] NULL,
	[COMM_CALC] [int] NULL,
	[REIN_REPORT_BUCK] [int] NULL,
	[REIN_REPORT_BUCK_COMM] [int] NULL,
	[SHOW_ACT_PREMIUM] [nvarchar] (20) NULL,
	[IS_SYSTEM_GENERAED] [char] (1) NULL,
	[MANDATORY_DATE] [datetime] NULL,
	[NON_MANDATORY_DATE] [datetime] NULL,
	[DEFAULT_DATE] [datetime] NULL,
	[NON_DEFAULT_DATE] [datetime] NULL,
	[DISPLAY_ON_CLAIM] [int] NULL,
	[CLAIM_RESERVE_APPLY] [int] NULL,
	[IS_MAIN] [nchar] (1) NULL,
	[SUB_LOB_ID] [int] NULL,
	[CARRIER_COV_CODE] [nvarchar] (5) NULL,
	[CLM_PAYMENT_TYPE] [nvarchar] (50) NULL,
	[COV_TYPE_ABBR] [nvarchar] (10) NULL,
	[SUSEP_COV_CODE] [numeric] (4,0) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_COVERAGE] ADD CONSTRAINT [PK_MNT_COVERAGE_COV_ID] PRIMARY KEY
	CLUSTERED
	(
		[COV_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

