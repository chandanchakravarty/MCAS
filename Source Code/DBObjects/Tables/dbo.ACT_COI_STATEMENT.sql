IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_COI_STATEMENT]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_COI_STATEMENT]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_COI_STATEMENT](
	[MONTH_NUMBER] [smallint] NULL,
	[COMPANY_ID] [smallint] NULL,
	[POLICY_ID] [int] NULL,
	[POLICY_VERSION_ID] [int] NULL,
	[CUSTOMER_ID] [smallint] NULL,
	[SOURCE_EFF_DATE] [datetime] NULL,
	[PREMIUM_AMOUNT] [decimal] (20,2) NULL,
	[COMMISSION_RATE] [decimal] (15,4) NULL,
	[TOTAL_PAID] [decimal] (20,2) NULL,
	[SOURCE_ROW_ID] [int] NULL,
	[MONTH_YEAR] [int] NULL,
	[TRAN_TYPE] [nvarchar] (5) NULL,
	[AMOUNT_FOR_CALCULATION] [decimal] (18,2) NULL,
	[COMMISSION_AMOUNT] [decimal] (18,2) NULL,
	[DUE_AMOUNT] [decimal] (18,2) NULL,
	[DATE_FULL_PAID] [datetime] NULL,
	[PAYMENT_DATE] [datetime] NULL,
	[PAYMENT_STATUS] [int] NULL,
	[IS_TEMP_ENTRY] [bit] NULL,
	[IN_RECON] [decimal] (18,2) NULL,
	[AMT_IN_RECON] [decimal] (18,2) NULL,
	[COI_OPEN_ITEM_ID] [int] NULL,
	[CUSTOMER_OPEN_ITEM_ID] [int] NULL,
	[ROW_ID] [int] NOT NULL IDENTITY (0,1),
	[GROSS_PREMIUM] [decimal] (20,2) NULL,
	[FEES] [decimal] (20,2) NULL,
	[TRAN_CODE] [nvarchar] (10) NULL,
	[COMM_TYPE] [nvarchar] (10) NULL,
	[BILL_TYPE] [nvarchar] (2) NULL,
	[ITEM_STATUS] [nvarchar] (20) NULL,
	[UPDATED_FROM] [nvarchar] (20) NULL,
	[IS_CHECK_CREATED] [char] (1) NULL,
	[STAT_FEES_FOR_CALCULATION] [decimal] (18,2) NULL,
	[CSR_ID] [int] NULL
) ON [PRIMARY]
GO

