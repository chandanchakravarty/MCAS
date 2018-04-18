IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_LIST_tmp]') AND type in (N'U'))
DROP TABLE [dbo].[APP_LIST_tmp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[APP_LIST_tmp](
	[CUSTOMER_ID] [int] NOT NULL,
	[APP_ID] [int] NOT NULL,
	[APP_VERSION_ID] [smallint] NOT NULL,
	[PARENT_APP_VERSION_ID] [smallint] NULL,
	[APP_STATUS] [nvarchar] (25) NULL,
	[APP_NUMBER] [nvarchar] (75) NULL,
	[APP_VERSION] [nvarchar] (4) NULL,
	[APP_TERMS] [nvarchar] (5) NULL,
	[APP_INCEPTION_DATE] [datetime] NULL,
	[APP_EFFECTIVE_DATE] [datetime] NULL,
	[APP_EXPIRATION_DATE] [datetime] NULL,
	[APP_LOB] [nvarchar] (5) NULL,
	[APP_SUBLOB] [nvarchar] (5) NULL,
	[CSR] [int] NULL,
	[UNDERWRITER] [int] NULL,
	[IS_UNDER_REVIEW] [nchar] (1) NULL,
	[APP_AGENCY_ID] [smallint] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[COUNTRY_ID] [int] NULL,
	[STATE_ID] [smallint] NULL,
	[DIV_ID] [smallint] NULL,
	[DEPT_ID] [smallint] NULL,
	[PC_ID] [smallint] NULL,
	[BILL_TYPE] [char] (2) NULL,
	[COMPLETE_APP] [char] (1) NULL,
	[INSTALL_PLAN_ID] [int] NULL,
	[CHARGE_OFF_PRMIUM] [nvarchar] (5) NULL,
	[RECEIVED_PRMIUM] [decimal] (18,2) NULL,
	[PROXY_SIGN_OBTAINED] [int] NULL,
	[POLICY_TYPE] [int] NULL,
	[APP_VERIFICATION_XML] [text] NULL,
	[SHOW_QUOTE] [nchar] (1) NULL,
	[YEAR_AT_CURR_RESI] [int] NULL,
	[YEARS_AT_PREV_ADD] [nvarchar] (250) NULL,
	[RULE_INPUT_XML] [text] NULL,
	[PROPRTY_INSP_CREDIT] [int] NULL,
	[PIC_OF_LOC] [int] NULL,
	[BILL_TYPE_ID] [int] NULL,
	[IS_HOME_EMP] [bit] NULL,
	[APPLY_INSURANCE_SCORE] [numeric] (18,0) NULL,
	[DWELLING_ID] [smallint] NULL,
	[ADD_INT_ID] [int] NULL,
	[PRODUCER] [int] NULL,
	[DOWN_PAY_MODE] [int] NULL,
	[DELETE_FLAG] [char] (1) NULL,
	[RULE_VERIFICATION] [int] NULL,
	[CUSTOMER_REASON_CODE] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE2] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE3] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE4] [nvarchar] (10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

