IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BACKUP_CLT_APPLICANT_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[BACKUP_CLT_APPLICANT_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BACKUP_CLT_APPLICANT_LIST](
	[APPLICANT_ID] [int] NOT NULL,
	[CUSTOMER_ID] [int] NULL,
	[TITLE] [nvarchar] (10) NULL,
	[SUFFIX] [nvarchar] (5) NULL,
	[FIRST_NAME] [nvarchar] (100) NULL,
	[MIDDLE_NAME] [nvarchar] (50) NULL,
	[LAST_NAME] [nvarchar] (50) NULL,
	[ADDRESS1] [nvarchar] (150) NULL,
	[ADDRESS2] [nvarchar] (100) NULL,
	[CITY] [nvarchar] (70) NULL,
	[COUNTRY] [nvarchar] (10) NULL,
	[STATE] [nvarchar] (10) NULL,
	[ZIP_CODE] [nvarchar] (20) NULL,
	[PHONE] [nvarchar] (20) NULL,
	[EMAIL] [nvarchar] (50) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[MODIFIED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[LAST_UPDATED_TIME] [datetime] NULL,
	[CO_APPLI_OCCU] [int] NULL,
	[CO_APPLI_EMPL_NAME] [nvarchar] (75) NULL,
	[CO_APPLI_EMPL_ADDRESS] [nvarchar] (150) NULL,
	[CO_APPLI_YEARS_WITH_CURR_EMPL] [real] NULL,
	[CO_APPL_YEAR_CURR_OCCU] [real] NULL,
	[CO_APPL_MARITAL_STATUS] [nchar] (1) NULL,
	[CO_APPL_DOB] [datetime] NULL,
	[CO_APPL_SSN_NO] [nvarchar] (44) NULL,
	[IS_PRIMARY_APPLICANT] [int] NULL,
	[DESC_CO_APPLI_OCCU] [nvarchar] (200) NULL,
	[BUSINESS_PHONE] [nvarchar] (20) NULL,
	[MOBILE] [nvarchar] (20) NULL,
	[EXT] [nvarchar] (6) NULL,
	[CO_APPLI_EMPL_CITY] [nvarchar] (70) NULL,
	[CO_APPLI_EMPL_COUNTRY] [nvarchar] (10) NULL,
	[CO_APPLI_EMPL_STATE] [nvarchar] (10) NULL,
	[CO_APPLI_EMPL_ZIP_CODE] [nvarchar] (12) NULL,
	[CO_APPLI_EMPL_PHONE] [nvarchar] (20) NULL,
	[CO_APPLI_EMPL_EMAIL] [nvarchar] (50) NULL,
	[CO_APPLI_EMPL_ADDRESS1] [nvarchar] (150) NULL,
	[PER_CUST_MOBILE] [nvarchar] (15) NULL,
	[EMP_EXT] [nvarchar] (6) NULL,
	[CO_APPL_GENDER] [nvarchar] (20) NULL,
	[CO_APPL_RELATIONSHIP] [nvarchar] (25) NULL,
	[POSITION] [int] NULL,
	[CONTACT_CODE] [nvarchar] (20) NULL,
	[ID_TYPE] [int] NULL,
	[ID_TYPE_NUMBER] [nvarchar] (20) NULL,
	[NUMBER] [nvarchar] (20) NULL,
	[COMPLIMENT] [nvarchar] (20) NULL,
	[DISTRICT] [nvarchar] (20) NULL,
	[NOTE] [nvarchar] (250) NULL,
	[REGIONAL_IDENTIFICATION] [nvarchar] (20) NULL,
	[REG_ID_ISSUE] [datetime] NULL,
	[ORIGINAL_ISSUE] [nvarchar] (20) NULL,
	[FAX] [nvarchar] (20) NULL,
	[CPF_CNPJ] [nvarchar] (20) NULL,
	[APPLICANT_TYPE] [int] NULL,
	[BANK_NAME] [nvarchar] (25) NULL,
	[BANK_NUMBER] [nvarchar] (20) NULL,
	[BANK_BRANCH] [nvarchar] (20) NULL,
	[ACCOUNT_NUMBER] [nvarchar] (20) NULL,
	[ACCOUNT_TYPE] [int] NULL
) ON [PRIMARY]
GO

