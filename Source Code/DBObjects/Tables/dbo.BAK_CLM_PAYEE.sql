IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BAK_CLM_PAYEE]') AND type in (N'U'))
DROP TABLE [dbo].[BAK_CLM_PAYEE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BAK_CLM_PAYEE](
	[CLAIM_ID] [int] NOT NULL,
	[ACTIVITY_ID] [int] NOT NULL,
	[EXPENSE_ID] [int] NULL,
	[PAYEE_ID] [int] NOT NULL,
	[PARTY_ID] [nvarchar] (250) NULL,
	[PAYMENT_METHOD] [int] NULL,
	[ADDRESS1] [nvarchar] (75) NULL,
	[ADDRESS2] [nvarchar] (75) NULL,
	[CITY] [nvarchar] (25) NULL,
	[STATE] [int] NOT NULL,
	[ZIP] [nvarchar] (11) NULL,
	[COUNTRY] [int] NOT NULL,
	[NARRATIVE] [nvarchar] (300) NULL,
	[IS_ACTIVE] [char] (1) NOT NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[AMOUNT] [decimal] (18,2) NULL,
	[INVOICE_NUMBER] [nvarchar] (50) NULL,
	[INVOICE_DATE] [datetime] NULL,
	[SERVICE_TYPE] [int] NULL,
	[SERVICE_DESCRIPTION] [nvarchar] (300) NULL,
	[SECONDARY_PARTY_ID] [nvarchar] (250) NULL,
	[FIRST_NAME] [nvarchar] (30) NULL,
	[LAST_NAME] [nvarchar] (30) NULL,
	[TO_ORDER_DESC] [nvarchar] (250) NULL,
	[PAYEE_PARTY_ID] [nvarchar] (250) NULL,
	[INVOICE_DUE_DATE] [datetime] NULL,
	[INVOICE_SERIAL_NUMBER] [nvarchar] (50) NULL,
	[PAYEE_BANK_ID] [int] NULL
) ON [PRIMARY]
GO

