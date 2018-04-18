IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLT_CUSTOMER_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[CLT_CUSTOMER_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLT_CUSTOMER_LIST](
	[CUSTOMER_ID] [int] NOT NULL IDENTITY (1,1),
	[CUSTOMER_CODE] [nvarchar] (10) NULL,
	[CUSTOMER_TYPE] [int] NULL,
	[CUSTOMER_PARENT] [int] NULL,
	[CUSTOMER_SUFFIX] [nvarchar] (5) NULL,
	[CUSTOMER_FIRST_NAME] [nvarchar] (100) NULL,
	[CUSTOMER_MIDDLE_NAME] [nvarchar] (100) NULL,
	[CUSTOMER_LAST_NAME] [nvarchar] (100) NULL,
	[CUSTOMER_ADDRESS1] [nvarchar] (150) NULL,
	[CUSTOMER_ADDRESS2] [nvarchar] (200) NULL,
	[CUSTOMER_CITY] [nvarchar] (70) NULL,
	[CUSTOMER_COUNTRY] [nvarchar] (10) NULL,
	[CUSTOMER_STATE] [nvarchar] (20) NULL,
	[CUSTOMER_ZIP] [nvarchar] (20) NULL,
	[CUSTOMER_BUSINESS_TYPE] [nvarchar] (20) NULL,
	[CUSTOMER_BUSINESS_DESC] [nvarchar] (1000) NULL,
	[CUSTOMER_CONTACT_NAME] [nvarchar] (35) NULL,
	[CUSTOMER_BUSINESS_PHONE] [nvarchar] (15) NULL,
	[CUSTOMER_EXT] [nvarchar] (6) NULL,
	[CUSTOMER_HOME_PHONE] [nvarchar] (15) NULL,
	[CUSTOMER_MOBILE] [nvarchar] (15) NULL,
	[CUSTOMER_FAX] [nvarchar] (15) NULL,
	[CUSTOMER_PAGER_NO] [nvarchar] (15) NULL,
	[CUSTOMER_Email] [nvarchar] (50) NULL,
	[CUSTOMER_WEBSITE] [nvarchar] (150) NULL,
	[CUSTOMER_INSURANCE_SCORE] [int] NULL,
	[CUSTOMER_INSURANCE_RECEIVED_DATE] [datetime] NULL,
	[CUSTOMER_REASON_CODE] [nvarchar] (10) NULL,
	[CUSTOMER_LATE_CHARGES] [nchar] (1) NULL,
	[CUSTOMER_LATE_NOTICES] [nchar] (1) NULL,
	[CUSTOMER_SEND_STATEMENT] [nchar] (1) NULL,
	[CUSTOMER_RECEIVABLE_DUE_DAYS] [int] NULL,
	[CUSTOMER_AGENCY_ID] [smallint] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[CUSTOMER_ATTENTION_NOTE] [nvarchar] (500) NULL,
	[PREFIX] [int] NULL,
	[CUSTOMER_REASON_CODE2] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE3] [nvarchar] (10) NULL,
	[CUSTOMER_REASON_CODE4] [nvarchar] (10) NULL,
	[ATTENTION_NOTE_UPDATED] [datetime] NULL,
	[IS_HOME_EMPLOYEE] [nvarchar] (2000) NULL,
	[LAST_INSURANCE_SCORE_FETCHED] [datetime] NULL,
	[APPLICANT_OCCU] [int] NULL,
	[EMPLOYER_NAME] [nvarchar] (75) NULL,
	[EMPLOYER_ADDRESS] [nvarchar] (75) NULL,
	[YEARS_WITH_CURR_EMPL] [real] NULL,
	[SSN_NO] [nvarchar] (44) NULL,
	[MARITAL_STATUS] [nchar] (1) NULL,
	[DATE_OF_BIRTH] [datetime] NULL,
	[DESC_APPLICANT_OCCU] [nvarchar] (200) NULL,
	[LAST_MVR_SCORE_FETCHED] [datetime] NULL,
	[EMPLOYER_ADD1] [nvarchar] (150) NULL,
	[EMPLOYER_ADD2] [nvarchar] (150) NULL,
	[EMPLOYER_CITY] [nvarchar] (70) NULL,
	[EMPLOYER_COUNTRY] [nvarchar] (10) NULL,
	[EMPLOYER_STATE] [nvarchar] (10) NULL,
	[EMPLOYER_ZIPCODE] [nvarchar] (11) NULL,
	[EMPLOYER_HOMEPHONE] [nvarchar] (15) NULL,
	[EMPLOYER_EMAIL] [nvarchar] (50) NULL,
	[YEARS_WITH_CURR_OCCU] [real] NULL,
	[GENDER] [nchar] (1) NULL,
	[PER_CUST_MOBILE] [nvarchar] (15) NULL,
	[EMP_EXT] [nvarchar] (6) NULL,
	[PRIORINFO_ORDERED] [datetime] NULL,
	[CPF_CNPJ] [nvarchar] (20) NULL,
	[NUMBER] [nvarchar] (20) NULL,
	[COMPLIMENT] [nvarchar] (20) NULL,
	[DISTRICT] [nvarchar] (20) NULL,
	[BROKER] [int] NULL,
	[MAIN_TITLE] [int] NULL,
	[MAIN_POSITION] [int] NULL,
	[MAIN_CPF_CNPJ] [nvarchar] (20) NULL,
	[MAIN_ADDRESS] [nvarchar] (20) NULL,
	[MAIN_NUMBER] [nvarchar] (20) NULL,
	[MAIN_COMPLIMENT] [nvarchar] (20) NULL,
	[MAIN_DISTRICT] [nvarchar] (20) NULL,
	[MAIN_NOTE] [nvarchar] (250) NULL,
	[MAIN_CONTACT_CODE] [nvarchar] (20) NULL,
	[REGIONAL_IDENTIFICATION] [nvarchar] (20) NULL,
	[REG_ID_ISSUE] [datetime] NULL,
	[ORIGINAL_ISSUE] [nvarchar] (20) NULL,
	[MAIN_CITY] [nvarchar] (70) NULL,
	[MAIN_STATE] [int] NULL,
	[MAIN_COUNTRY] [int] NULL,
	[MAIN_ZIPCODE] [nvarchar] (20) NULL,
	[MAIN_FIRST_NAME] [nvarchar] (100) NULL,
	[MAIN_MIDDLE_NAME] [nvarchar] (100) NULL,
	[MAIN_LAST_NAME] [nvarchar] (100) NULL,
	[ID_TYPE] [nvarchar] (100) NULL,
	[MONTHLY_INCOME] [decimal] (10,2) NULL,
	[AMOUNT_TYPE] [int] NULL,
	[CADEMP] [nvarchar] (100) NULL,
	[NET_ASSETS_AMOUNT] [decimal] (10,2) NULL,
	[NATIONALITY] [nvarchar] (100) NULL,
	[EMAIL_ADDRESS] [nvarchar] (100) NULL,
	[REGIONAL_IDENTIFICATION_TYPE] [nvarchar] (40) NULL,
	[IS_POLITICALLY_EXPOSED] [char] (2) NULL,
	[ACC_COI_FLAG] [char] (1) NULL
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'caption', @value='CUSTOMER ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CLT_CUSTOMER_LIST', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

ALTER TABLE [dbo].[CLT_CUSTOMER_LIST] ADD CONSTRAINT [PK_CLT_CUSTOMER_LIST_CUSTOMER_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

