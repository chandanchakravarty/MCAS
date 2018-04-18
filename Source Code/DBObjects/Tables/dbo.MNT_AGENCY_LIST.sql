IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_AGENCY_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_AGENCY_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_AGENCY_LIST](
	[AGENCY_ID] [int] NOT NULL IDENTITY (1,1),
	[AGENCY_CODE] [nvarchar] (8) NULL,
	[AGENCY_DISPLAY_NAME] [nvarchar] (75) NULL,
	[AGENCY_LIC_NUM] [smallint] NULL,
	[AGENCY_ADD1] [nvarchar] (150) NULL,
	[AGENCY_ADD2] [nvarchar] (70) NULL,
	[AGENCY_CITY] [nvarchar] (40) NULL,
	[AGENCY_STATE] [nvarchar] (5) NULL,
	[AGENCY_ZIP] [nvarchar] (24) NULL,
	[AGENCY_COUNTRY] [nvarchar] (5) NULL,
	[AGENCY_PHONE] [nvarchar] (20) NULL,
	[AGENCY_EXT] [nvarchar] (200) NULL,
	[AGENCY_FAX] [nvarchar] (20) NULL,
	[AGENCY_EMAIL] [nvarchar] (50) NULL,
	[AGENCY_WEBSITE] [nvarchar] (100) NULL,
	[AGENCY_PAYMENT_METHOD] [nvarchar] (1) NULL,
	[AGENCY_COMMISSION] [decimal] (6,2) NULL,
	[AGENCY_BILL_TYPE] [nvarchar] (5) NULL,
	[AGENCY_SIGNATURES] [smallint] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[PRINCIPAL_CONTACT] [nvarchar] (50) NULL,
	[OTHER_CONTACT] [nvarchar] (50) NULL,
	[FEDERAL_ID] [nvarchar] (50) NULL,
	[ORIGINAL_CONTRACT_DATE] [datetime] NULL,
	[UNDERWRITER_ASSIGNED_AGENCY] [nvarchar] (250) NULL,
	[BANK_ACCOUNT_NUMBER] [nvarchar] (100) NULL,
	[ROUTING_NUMBER] [nvarchar] (100) NULL,
	[HOME_UNDERWRITER] [nvarchar] (100) NULL,
	[MOTOR_UNDERWRITER] [nvarchar] (100) NULL,
	[PRIVATE_UNDERWRITER] [nvarchar] (100) NULL,
	[UMBRELLA_UNDERWRITER] [nvarchar] (100) NULL,
	[WATER_UNDERWRITER] [nvarchar] (100) NULL,
	[GENERAL_UNDERWRITER] [nvarchar] (100) NULL,
	[RENTAL_UNDERWRITER] [nvarchar] (100) NULL,
	[M_AGENCY_ADD_1] [nvarchar] (150) NULL,
	[M_AGENCY_ADD_2] [nvarchar] (70) NULL,
	[M_AGENCY_CITY] [nvarchar] (40) NULL,
	[M_AGENCY_COUNTRY] [nvarchar] (5) NULL,
	[M_AGENCY_STATE] [nvarchar] (5) NULL,
	[M_AGENCY_ZIP] [nvarchar] (24) NULL,
	[M_AGENCY_PHONE] [nvarchar] (20) NULL,
	[M_AGENCY_FAX] [nvarchar] (20) NULL,
	[M_AGENCY_EXT] [nvarchar] (5) NULL,
	[TERMINATION_DATE] [datetime] NULL,
	[TERMINATION_REASON] [nvarchar] (75) NULL,
	[AGENCY_SPEED_DIAL] [int] NULL,
	[AGENCY_DBA] [nvarchar] (100) NULL,
	[AGENCY_COMBINED_CODE] [nvarchar] (10) NULL,
	[BANK_BRANCH] [nvarchar] (20) NULL,
	[BANK_NAME] [nvarchar] (20) NULL,
	[BANK_ACCOUNT_NUMBER1] [nvarchar] (100) NULL,
	[ROUTING_NUMBER1] [nvarchar] (100) NULL,
	[NOTES] [nvarchar] (100) NULL,
	[NUM_AGENCY_CODE] [int] NULL,
	[HOME_MARKETING] [nvarchar] (200) NULL,
	[MOTOR_MARKETING] [nvarchar] (200) NULL,
	[UMBRELLA_MARKETING] [nvarchar] (200) NULL,
	[WATER_MARKETING] [nvarchar] (200) NULL,
	[GENERAL_MARKETING] [nvarchar] (200) NULL,
	[RENTAL_MARKETING] [nvarchar] (200) NULL,
	[PRIVATE_MARKETING] [nvarchar] (200) NULL,
	[CURRENT_CONTRACT_DATE] [datetime] NULL,
	[AgencyName] [nvarchar] (200) NULL,
	[TERMINATION_DATE_RENEW] [datetime] NULL,
	[TERMINATION_NOTICE] [char] (1) NULL,
	[ACCOUNT_ISVERIFIED1] [int] NULL,
	[ACCOUNT_ISVERIFIED2] [int] NULL,
	[ACCOUNT_VERIFIED_DATE1] [datetime] NULL,
	[ACCOUNT_VERIFIED_DATE2] [datetime] NULL,
	[REASON1] [nvarchar] (200) NULL,
	[REASON2] [nvarchar] (200) NULL,
	[ALLOWS_EFT] [int] NULL,
	[ACCOUNT_TYPE] [nvarchar] (4) NULL,
	[ACCOUNT_TYPE_2] [nvarchar] (4) NULL,
	[BANK_NAME_2] [nvarchar] (20) NULL,
	[BANK_BRANCH_2] [nvarchar] (20) NULL,
	[INCORPORATED_LICENSE] [char] (1) NULL,
	[PROCESS_1099] [int] NULL,
	[EXEMPT_ON_CANCEL] [char] (1) NULL,
	[ALLOWS_CUSTOMER_SWEEP] [int] NULL,
	[REVERIFIED_AC1] [int] NULL,
	[REVERIFIED_AC2] [int] NULL,
	[FED_SSN_1099]  AS (case when [PROCESS_1099]='11735' OR [PROCESS_1099]='11734' OR [PROCESS_1099]='11733' then 'F' else case when [PROCESS_1099]='114246' OR [PROCESS_1099]='114245' OR [PROCESS_1099]='114244' then 'S' else '' end end),
	[REQ_SPECIAL_HANDLING] [int] NULL,
	[SUSEP_NUMBER] [nvarchar] (20) NULL,
	[IS_CARRIER] [char] (1) NULL,
	[BROKER_CURRENCY] [int] NOT NULL CONSTRAINT [DF__MNT_AGENC__BROKE__6BF8D902] DEFAULT ((0)),
	[AGENCY_TYPE_ID] [int] NULL,
	[DISTRICT] [nvarchar] (25) NULL,
	[NUMBER] [nvarchar] (20) NULL,
	[BROKER_TYPE] [int] NULL,
	[BROKER_CPF_CNPJ] [nvarchar] (20) NULL,
	[BROKER_DATE_OF_BIRTH] [datetime] NULL,
	[BROKER_REGIONAL_ID] [nvarchar] (20) NULL,
	[REGIONAL_ID_ISSUANCE] [nvarchar] (20) NULL,
	[REGIONAL_ID_ISSUE_DATE] [datetime] NULL,
	[MARITAL_STATUS] [int] NULL,
	[GENDER] [int] NULL,
	[BROKER_BANK_NUMBER] [nvarchar] (10) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_AGENCY_LIST] ADD CONSTRAINT [PK_MNT_AGENCY_LIST_AGENCY_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[AGENCY_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

