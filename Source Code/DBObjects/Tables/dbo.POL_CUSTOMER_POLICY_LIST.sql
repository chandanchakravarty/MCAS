IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_CUSTOMER_POLICY_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[POL_CUSTOMER_POLICY_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_CUSTOMER_POLICY_LIST](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[APP_ID] [int] NULL,
	[APP_VERSION_ID] [smallint] NULL,
	[POLICY_TYPE] [nvarchar] (12) NULL,
	[POLICY_NUMBER] [nvarchar] (75) NULL,
	[POLICY_DISP_VERSION] [nvarchar] (6) NULL,
	[POLICY_STATUS] [nvarchar] (20) NULL,
	[POLICY_LOB] [nvarchar] (5) NULL,
	[POLICY_SUBLOB] [nvarchar] (5) NULL,
	[POLICY_DESCRIPTION] [nvarchar] (MAX) NULL,
	[ACCOUNT_EXEC] [int] NULL,
	[CSR] [int] NULL,
	[UNDERWRITER] [int] NULL,
	[PROCESS_STATUS] [smallint] NULL,
	[IS_UNDER_CONFIRMATION] [nchar] (1) NULL,
	[LAST_PROCESS] [nvarchar] (10) NULL,
	[LAST_PROCESS_COMPLETED] [datetime] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[POLICY_ACCOUNT_STATUS] [int] NULL,
	[AGENCY_ID] [smallint] NULL,
	[PARENT_APP_VERSION_ID] [smallint] NULL,
	[APP_STATUS] [nvarchar] (25) NULL,
	[APP_NUMBER] [nvarchar] (75) NULL,
	[APP_VERSION] [nvarchar] (4) NULL,
	[APP_TERMS] [nvarchar] (5) NULL,
	[APP_INCEPTION_DATE] [datetime] NULL,
	[APP_EFFECTIVE_DATE] [datetime] NULL,
	[APP_EXPIRATION_DATE] [datetime] NULL,
	[IS_UNDER_REVIEW] [nchar] (1) NULL,
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
	[SHOW_QUOTE] [nchar] (1) NULL,
	[APP_VERIFICATION_XML] [text] NULL,
	[YEAR_AT_CURR_RESI] [real] NULL,
	[YEARS_AT_PREV_ADD] [nvarchar] (250) NULL,
	[POLICY_TERMS] [nvarchar] (5) NULL,
	[POLICY_EFFECTIVE_DATE] [datetime] NULL,
	[POLICY_EXPIRATION_DATE] [datetime] NULL,
	[POLICY_STATUS_CODE] [nvarchar] (6) NULL,
	[SEND_RENEWAL_DIARY_REM] [char] (1) NULL,
	[TO_BE_AUTO_RENEWED] [smallint] NULL,
	[POLICY_PREMIUM_XML] [text] NULL,
	[MVR_WIN_SERVICE] [char] (1) NULL,
	[ALL_DATA_VALID] [int] NULL,
	[PIC_OF_LOC] [int] NULL,
	[PROPRTY_INSP_CREDIT] [int] NULL,
	[BILL_TYPE_ID] [int] NULL,
	[IS_HOME_EMP] [bit] NULL,
	[RULE_INPUT_XML] [text] NULL,
	[POL_VER_EFFECTIVE_DATE] [datetime] NULL,
	[POL_VER_EXPIRATION_DATE] [datetime] NULL,
	[APPLY_INSURANCE_SCORE] [int] NULL,
	[DWELLING_ID] [smallint] NULL,
	[ADD_INT_ID] [int] NULL,
	[PRODUCER] [int] NULL,
	[DOWN_PAY_MODE] [int] NULL,
	[CURRENT_TERM] [smallint] NULL,
	[NOT_RENEW] [char] (1) NULL,
	[NOT_RENEW_REASON] [int] NULL,
	[REFER_UNDERWRITER] [char] (1) NULL,
	[REFERAL_INSTRUCTIONS] [nvarchar] (1000) NULL,
	[REINS_SPECIAL_ACPT] [int] NULL,
	[FROM_AS400] [char] (1) NULL,
	[CUSTOMER_REASON_CODE] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE2] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE3] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE4] [nvarchar] (10) NULL,
	[IS_REWRITE_POLICY] [char] (1) NULL,
	[IS_YEAR_WITH_WOL_UPDATED] [char] (1) NULL,
	[POLICY_CURRENCY] [int] NULL,
	[POLICY_LEVEL_COMISSION] [decimal] (18,2) NULL,
	[BILLTO] [nvarchar] (50) NULL,
	[PAYOR] [int] NULL,
	[CO_INSURANCE] [int] NULL,
	[CONTACT_PERSON] [int] NULL,
	[TRANSACTION_TYPE] [int] NULL,
	[PREFERENCE_DAY] [smallint] NULL,
	[BROKER_REQUEST_NO] [nvarchar] (50) NULL,
	[POLICY_LEVEL_COMM_APPLIES] [nchar] (1) NULL,
	[BROKER_COMM_FIRST_INSTM] [nchar] (1) NULL,
	[OLD_POLICY_NUMBER] [nvarchar] (75) NULL,
	[APP_SUBMITTED_DATE] [datetime] NULL,
	[POLICY_VERIFY_DIGIT] [smallint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains Customer ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_CUSTOMER_POLICY_LIST', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains  Policy Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_CUSTOMER_POLICY_LIST', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains  Policy Version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_CUSTOMER_POLICY_LIST', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains  application id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_CUSTOMER_POLICY_LIST', @level2type=N'COLUMN',@level2name=N'APP_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains  application version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_CUSTOMER_POLICY_LIST', @level2type=N'COLUMN',@level2name=N'APP_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains Policy Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_CUSTOMER_POLICY_LIST', @level2type=N'COLUMN',@level2name=N'POLICY_NUMBER'
GO

ALTER TABLE [dbo].[POL_CUSTOMER_POLICY_LIST] ADD CONSTRAINT [PK_POL_CUSTOMER_POLICY_LIST_CUSTOMER_ID_POLICY_ID_VERSION_ID] PRIMARY KEY
	NONCLUSTERED
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
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_CUSTOMER_POLICY_LIST] WITH NOCHECK ADD CONSTRAINT [FK_POL_CUSTOMER_POLICY_LIST_CLT_CUSTOMER_LIST_CUSTOMER_ID] FOREIGN KEY
	(
		[CUSTOMER_ID]
	)
	REFERENCES [dbo].[CLT_CUSTOMER_LIST]
	(
		[CUSTOMER_ID]
	) 
GO

