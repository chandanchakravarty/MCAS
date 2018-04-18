IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_CHECK_INFORMATION]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_CHECK_INFORMATION](
	[CHECK_ID] [int] NOT NULL,
	[CHECK_TYPE] [nvarchar] (5) NULL,
	[SELECT_FROM] [nvarchar] (5) NULL,
	[ACCOUNT_ID] [int] NULL,
	[MANUAL_CHECK] [nvarchar] (1) NULL,
	[CHECK_NUMBER] [nvarchar] (20) NULL,
	[CHECK_DATE] [datetime] NULL,
	[CHECK_AMOUNT] [decimal] (18,2) NULL,
	[CHECK_NOTE] [nvarchar] (600) NULL,
	[PAYEE_ENTITY_ID] [int] NULL,
	[PAYEE_ENTITY_TYPE] [nvarchar] (5) NULL,
	[PAYEE_ENTITY_NAME] [nvarchar] (255) NULL,
	[PAYEE_ADD1] [nvarchar] (70) NULL,
	[PAYEE_ADD2] [nvarchar] (70) NULL,
	[PAYEE_CITY] [nvarchar] (40) NULL,
	[PAYEE_STATE] [nvarchar] (30) NULL,
	[PAYEE_ZIP] [nvarchar] (12) NULL,
	[PAYEE_NOTE] [nvarchar] (100) NULL,
	[CREATED_IN] [nvarchar] (1) NULL,
	[DIV_ID] [smallint] NULL,
	[DEPT_ID] [smallint] NULL,
	[PC_ID] [smallint] NULL,
	[IS_COMMITED] [nchar] (1) NULL,
	[DATE_COMMITTED] [datetime] NULL,
	[COMMITED_BY] [int] NULL,
	[IN_RECON] [nchar] (1) NULL,
	[AVAILABLE_BALANCE] [decimal] (18,2) NULL,
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [smallint] NULL,
	[POLICY_VER_TRACKING_ID] [smallint] NULL,
	[GL_UPDATE] [nchar] (1) NULL,
	[IS_BNK_RECONCILED] [nchar] (1) NULL,
	[CHECKSIGN_1] [nvarchar] (200) NULL,
	[CHECKSIGN_2] [nvarchar] (200) NULL,
	[CHECK_MEMO] [nvarchar] (70) NULL,
	[IS_BNK_RECONCILED_VOID] [nvarchar] (2) NULL,
	[IN_BNK_RECON] [int] NULL,
	[SPOOL_STATUS] [int] NULL,
	[TRAN_TYPE] [int] NULL,
	[IS_DISPLAY_ON_STUB] [char] (1) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[OFFSET_ACC_ID] [int] NULL,
	[IS_AUTO_GENERATED] [char] (1) NULL,
	[IS_IN_CURRENT_SEQUENCE] [char] (1) NULL,
	[OPEN_ITEM_ROW_ID] [int] NULL,
	[MONTH] [int] NULL,
	[YEAR] [int] NULL,
	[AGENCY_ID] [int] NULL,
	[COMM_TYPE] [nvarchar] (10) NULL,
	[PAYMENT_MODE] [int] NULL,
	[CLM_SUSPENCE_ACCT] [int] NULL,
	[IS_PRINTED] [char] (1) NULL,
	[CLAIM_TO_ORDER_DESC] [nvarchar] (250) NULL,
	[PRINT_DATE] [datetime] NULL,
	[IS_1099_PROCESSED] [nchar] (1) NULL,
	[BANK_RECONCILED_DATE] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_CHECK_INFORMATION] ADD CONSTRAINT [PK_ACT_CHECK_INFORMATION_CHECK_ID] PRIMARY KEY
	CLUSTERED
	(
		[CHECK_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

