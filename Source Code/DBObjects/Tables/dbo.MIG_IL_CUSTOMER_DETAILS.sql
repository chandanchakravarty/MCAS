IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MIG_IL_CUSTOMER_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[MIG_IL_CUSTOMER_DETAILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MIG_IL_CUSTOMER_DETAILS](
	[IMPORT_REQUEST_ID] [int] NOT NULL,
	[IMPORT_SERIAL_NO] [bigint] NOT NULL,
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [int] NULL,
	[LOB_ID] [int] NULL,
	[POLICY_VERSION_ID] [smallint] NULL,
	[IS_ACTIVE] [nchar] (1) NULL CONSTRAINT [MIG_IL_CUSTOMER_DETAILS_IS_ACTIVE] DEFAULT ('N'),
	[IS_PROCESSED] [char] (1) NULL CONSTRAINT [MIG_IL_CUSTOMER_DETAILS_IS_DELETED] DEFAULT ('N'),
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[HAS_ERRORS] [bit] NULL CONSTRAINT [MIG_IL_CUSTOMER_DETAILS_HAS_ERRORS] DEFAULT ('N'),
	[ERROR_TYPES] [nvarchar] (4000) NULL,
	[ERROR_COLUMNS] [nvarchar] (4000) NULL,
	[ERRORCOLUMNVALUES] [nvarchar] (4000) NULL,
	[CUSTOMER_CODE] [nvarchar] (50) NULL,
	[STATUS] [nvarchar] (2) NULL,
	[CUST_TYPE] [int] NULL,
	[PARENT] [nvarchar] (50) NULL,
	[CUSTOMER_NAME] [nvarchar] (200) NULL,
	[MIDDLE_NAME] [nvarchar] (200) NULL,
	[LAST_NAME] [nvarchar] (200) NULL,
	[POLITICALLY_EXPOSED] [nchar] (1) NULL,
	[BROKER] [int] NULL,
	[TITLE] [int] NULL,
	[ZIP CODE] [nvarchar] (50) NULL,
	[ADDRESS] [nvarchar] (300) NULL,
	[NUMBER] [nvarchar] (50) NULL,
	[COMPLIMENT] [nvarchar] (50) NULL,
	[DISTRICT] [nvarchar] (50) NULL,
	[CITY] [nvarchar] (150) NULL,
	[COUNTRY] [int] NULL,
	[STATE] [int] NULL,
	[CPF/CNPJ] [nvarchar] (50) NULL,
	[ID_TYPE] [nvarchar] (200) NULL,
	[WEBSITE] [nvarchar] (300) NULL,
	[BUSINESS_TYPE] [nvarchar] (50) NULL,
	[REG_ID_ISSUE] [nvarchar] (10) NULL,
	[ORIGINAL_ISSUE] [nvarchar] (50) NULL,
	[REG_IDENTIFICATION] [nvarchar] (50) NULL,
	[EMAIL_ADDRESS] [nvarchar] (100) NULL,
	[MARITAL_STATUS] [int] NULL,
	[CREATION DATE] [nvarchar] (10) NULL,
	[DATE OF BIRTH] [nvarchar] (10) NULL,
	[BUSINESS_DESC] [nvarchar] (2000) NULL,
	[GENDER] [nvarchar] (2) NULL,
	[HOME_PHONE] [nvarchar] (50) NULL,
	[CADEMP_NUMBER] [nvarchar] (200) NULL,
	[NET_ASSETS_AMOUNT] [decimal] (28,4) NULL,
	[TOTAL_ASSETS_AMOUNT] [decimal] (28,4) NULL,
	[AMOUNT_TYPE] [int] NULL,
	[ACCOUNT_TYPE] [int] NULL,
	[ACCOUNT_NUMBER] [nvarchar] (50) NULL,
	[BANK_NAME] [nvarchar] (100) NULL,
	[BANK_BRANCH] [nvarchar] (50) NULL,
	[BANK_NUMBER] [nvarchar] (50) NULL,
	[CONTACT_CODE] [nvarchar] (50) NULL,
	[ADDRESS1] [nvarchar] (300) NULL,
	[COMPLIMENT1] [nvarchar] (50) NULL,
	[CPF] [nvarchar] (50) NULL,
	[MOBILE] [nvarchar] (50) NULL,
	[BUSINESS_PHONE] [nvarchar] (50) NULL,
	[EXT] [nvarchar] (50) NULL,
	[FAX] [nvarchar] (50) NULL,
	[REMARKS] [nvarchar] (500) NULL,
	[REG_ID_TYPE] [nvarchar] (50) NULL,
	[NATIONALITY] [nvarchar] (200) NULL,
	[EMAIL_ADDRESS1] [nvarchar] (200) NULL,
	[TITLE_1] [int] NULL,
	[FIRST_NAME1] [nvarchar] (200) NULL,
	[MIDDLE_NAME 1] [nvarchar] (200) NULL,
	[LAST_NAME1] [nvarchar] (200) NULL,
	[ZIP_CODE1] [nvarchar] (50) NULL,
	[NUMBER1] [nvarchar] (50) NULL,
	[DISTRICT1] [nvarchar] (50) NULL,
	[CITY1] [nvarchar] (150) NULL,
	[STATE1] [int] NULL,
	[COUNTRY1] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MIG_IL_CUSTOMER_DETAILS] ADD CONSTRAINT [PK_MIG_IL_CUSTOMER_DETAILS_IMPORT_REQUEST_ID_SERIAL_NO] PRIMARY KEY
	NONCLUSTERED
	(
		[IMPORT_REQUEST_ID] ASC
		,[IMPORT_SERIAL_NO] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

