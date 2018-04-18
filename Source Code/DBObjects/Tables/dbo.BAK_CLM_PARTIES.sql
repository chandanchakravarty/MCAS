IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BAK_CLM_PARTIES]') AND type in (N'U'))
DROP TABLE [dbo].[BAK_CLM_PARTIES]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BAK_CLM_PARTIES](
	[PARTY_ID] [int] NOT NULL,
	[CLAIM_ID] [int] NOT NULL,
	[NAME] [nvarchar] (100) NULL,
	[ADDRESS1] [nvarchar] (150) NULL,
	[ADDRESS2] [nvarchar] (150) NULL,
	[CITY] [nvarchar] (70) NULL,
	[STATE] [int] NOT NULL,
	[ZIP] [nvarchar] (20) NULL,
	[CONTACT_PHONE] [nvarchar] (15) NULL,
	[CONTACT_EMAIL] [nvarchar] (50) NULL,
	[OTHER_DETAILS] [nvarchar] (500) NULL,
	[IS_ACTIVE] [char] (1) NOT NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[PARTY_TYPE_ID] [int] NULL,
	[COUNTRY] [int] NULL,
	[PARTY_DETAIL] [int] NULL,
	[AGE] [smallint] NULL,
	[EXTENT_OF_INJURY] [nvarchar] (200) NULL,
	[PARTY_CODE] [nvarchar] (10) NULL,
	[REFERENCE] [nvarchar] (30) NULL,
	[BANK_NAME] [nvarchar] (100) NULL,
	[ACCOUNT_NUMBER] [nvarchar] (20) NULL,
	[ACCOUNT_NAME] [nvarchar] (100) NULL,
	[CONTACT_PHONE_EXT] [nvarchar] (5) NULL,
	[CONTACT_FAX] [nvarchar] (15) NULL,
	[PARTY_TYPE_DESC] [nvarchar] (50) NULL,
	[PARENT_ADJUSTER] [int] NULL,
	[FEDRERAL_ID] [nvarchar] (50) NULL,
	[PROCESSING_OPTION_1099] [int] NULL,
	[MASTER_VENDOR_CODE] [nvarchar] (50) NULL,
	[VENDOR_CODE] [nvarchar] (50) NULL,
	[CONTACT_NAME] [nvarchar] (50) NULL,
	[EXPERT_SERVICE_TYPE] [int] NULL,
	[EXPERT_SERVICE_TYPE_DESC] [nvarchar] (500) NULL,
	[SUB_ADJUSTER_CONTACT_NAME] [nvarchar] (50) NULL,
	[SA_ADDRESS1] [nvarchar] (150) NULL,
	[SA_ADDRESS2] [nvarchar] (150) NULL,
	[SA_CITY] [nvarchar] (150) NULL,
	[SA_COUNTRY] [nvarchar] (2) NULL,
	[SA_STATE] [nvarchar] (10) NULL,
	[SA_ZIPCODE] [nvarchar] (20) NULL,
	[SA_PHONE] [nvarchar] (15) NULL,
	[SA_FAX] [nvarchar] (15) NULL,
	[SUB_ADJUSTER] [nvarchar] (60) NULL,
	[ADJUSTER_ID] [int] NULL,
	[PROP_DAMAGED_ID] [int] NULL,
	[MAIL_1099_ADD1] [nvarchar] (140) NULL,
	[MAIL_1099_ADD2] [nvarchar] (140) NULL,
	[MAIL_1099_CITY] [nvarchar] (80) NULL,
	[MAIL_1099_STATE] [nvarchar] (10) NULL,
	[MAIL_1099_COUNTRY] [nvarchar] (10) NULL,
	[MAIL_1099_ZIP] [nvarchar] (11) NULL,
	[MAIL_1099_NAME] [nvarchar] (75) NULL,
	[W9_FORM] [nvarchar] (5) NULL,
	[AGENCY_BANK] [nvarchar] (100) NULL,
	[DISTRICT] [nvarchar] (25) NULL,
	[PARTY_CPF_CNPJ] [nvarchar] (20) NULL,
	[NUMBER] [nvarchar] (10) NULL,
	[PARTY_TYPE] [int] NULL,
	[BANK_NUMBER] [nvarchar] (10) NULL,
	[BANK_BRANCH] [nvarchar] (20) NULL,
	[ACCOUNT_TYPE] [int] NULL,
	[DATE_OF_BIRTH] [datetime] NULL,
	[SOURCE_PARTY_ID] [int] NULL,
	[PARTY_PERCENTAGE] [decimal] (5,2) NULL,
	[SOURCE_PARTY_TYPE_ID] [int] NULL
) ON [PRIMARY]
GO

