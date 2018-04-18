IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_VENDOR_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_VENDOR_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_VENDOR_LIST](
	[VENDOR_ID] [int] NOT NULL,
	[VENDOR_CODE] [nvarchar] (6) NULL,
	[VENDOR_FNAME] [nvarchar] (65) NULL,
	[VENDOR_LNAME] [nvarchar] (15) NULL,
	[VENDOR_ADD1] [nvarchar] (70) NULL,
	[VENDOR_ADD2] [nvarchar] (70) NULL,
	[VENDOR_CITY] [nvarchar] (40) NULL,
	[VENDOR_COUNTRY] [nvarchar] (5) NULL,
	[VENDOR_STATE] [nvarchar] (5) NULL,
	[VENDOR_ZIP] [nvarchar] (11) NULL,
	[VENDOR_PHONE] [nvarchar] (20) NULL,
	[VENDOR_EXT] [nvarchar] (10) NULL,
	[VENDOR_FAX] [nvarchar] (20) NULL,
	[VENDOR_MOBILE] [nvarchar] (20) NULL,
	[VENDOR_EMAIL] [nvarchar] (50) NULL,
	[VENDOR_SALUTATION] [nvarchar] (25) NULL,
	[VENDOR_FEDERAL_NUM] [nvarchar] (200) NULL,
	[VENDOR_NOTE] [nvarchar] (250) NULL,
	[VENDOR_ACC_NUMBER] [nvarchar] (20) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[BUSI_OWNERNAME] [nvarchar] (35) NULL,
	[COMPANY_NAME] [nvarchar] (35) NULL,
	[CHK_MAIL_ADD1] [nvarchar] (140) NULL,
	[CHK_MAIL_ADD2] [nvarchar] (140) NULL,
	[CHK_MAIL_CITY] [nvarchar] (80) NULL,
	[CHK_MAIL_STATE] [nvarchar] (10) NULL,
	[CHKCOUNTRY] [nvarchar] (10) NULL,
	[CHK_MAIL_ZIP] [nvarchar] (11) NULL,
	[MAIL_1099_ADD1] [nvarchar] (140) NULL,
	[MAIL_1099_ADD2] [nvarchar] (140) NULL,
	[MAIL_1099_CITY] [nvarchar] (80) NULL,
	[MAIL_1099_STATE] [nvarchar] (10) NULL,
	[MAIL_1099_COUNTRY] [nvarchar] (10) NULL,
	[MAIL_1099_ZIP] [nvarchar] (11) NULL,
	[PROCESS_1099_OPT] [nvarchar] (10) NULL,
	[W9_FORM] [nvarchar] (10) NULL,
	[DFI_ACCOUNT_NUMBER] [nvarchar] (100) NULL,
	[ROUTING_NUMBER] [nvarchar] (100) NULL,
	[ACCOUNT_VERIFIED_DATE] [datetime] NULL,
	[ACCOUNT_ISVERIFIED] [int] NULL,
	[REASON] [nvarchar] (100) NULL,
	[ALLOWS_EFT] [int] NULL,
	[BANK_NAME] [nvarchar] (20) NULL,
	[BANK_BRANCH] [nvarchar] (20) NULL,
	[ACCOUNT_TYPE] [nvarchar] (4) NULL,
	[MAIL_1099_NAME] [nvarchar] (75) NULL,
	[REVERIFIED_AC] [int] NULL,
	[FED_SSN_1099]  AS (case when [PROCESS_1099_OPT]='11735' OR [PROCESS_1099_OPT]='11734' OR [PROCESS_1099_OPT]='11733' then 'F' else case when [PROCESS_1099_OPT]='114248' OR [PROCESS_1099_OPT]='114246' OR [PROCESS_1099_OPT]='114245' OR [PROCESS_1099_OPT]='114244' then 'S' else '' end end),
	[REQ_SPECIAL_HANDLING] [int] NULL,
	[SUSEP_NUM] [nvarchar] (20) NULL,
	[DATE_OF_BIRTH] [datetime] NULL,
	[CPF] [nvarchar] (40) NULL,
	[REGIONAL_IDENTIFICATION] [nvarchar] (40) NULL,
	[REG_ID_ISSUE_DATE] [datetime] NULL,
	[ACTIVITY] [int] NULL,
	[REG_ID_ISSUE] [nvarchar] (40) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_VENDOR_LIST] ADD CONSTRAINT [PK_MNT_VENDOR_LIST_VENDOR_ID] PRIMARY KEY
	CLUSTERED
	(
		[VENDOR_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

