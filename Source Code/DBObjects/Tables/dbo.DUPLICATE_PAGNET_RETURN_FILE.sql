IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DUPLICATE_PAGNET_RETURN_FILE]') AND type in (N'U'))
DROP TABLE [dbo].[DUPLICATE_PAGNET_RETURN_FILE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DUPLICATE_PAGNET_RETURN_FILE](
	[ROW_ID] [int] NOT NULL IDENTITY (1,1),
	[EVENT_CODE] [nvarchar] (5) NULL,
	[PAYMENT_ID] [nvarchar] (25) NULL,
	[COMMISSION_PAYMENT_DATE] [datetime] NULL,
	[COMMISSION_PAID_AMOUNT_SIGN] [nvarchar] (1) NULL,
	[COMMISSION_PAID_AMOUNT] [decimal] (18,2) NULL,
	[PAYMENT_CURRENCY] [nvarchar] (5) NULL,
	[BANK_ACCOUNT_NUMBER] [nvarchar] (5) NULL,
	[BANK_BRANCH_CODE] [nvarchar] (10) NULL,
	[No_DA_CONTA_CORRENTE] [nvarchar] (12) NULL,
	[CHEQUE_NUMBER] [nvarchar] (15) NULL,
	[SINAL_DO_VALOR_IR_2] [nvarchar] (1) NULL,
	[VALOR_IR_2] [decimal] (18,2) NULL,
	[SINAL_DO_VALOR_ISS_2] [nvarchar] (1) NULL,
	[VALOR_ISS] [decimal] (18,2) NULL,
	[SINAL_DO_VALOR_INSS] [nvarchar] (1) NULL,
	[VALOR_INSS] [decimal] (18,2) NULL,
	[SINAL_DO_VALOR_CSLL] [nvarchar] (1) NULL,
	[VALOR_CSLL] [decimal] (18,2) NULL,
	[SINAL_DO_VALOR_COFINS] [nvarchar] (1) NULL,
	[VALOR_COFINS] [decimal] (18,2) NULL,
	[SINAL_DO_VALOR_PIS] [nvarchar] (1) NULL,
	[VALOR_PIS] [decimal] (18,2) NULL,
	[OCCURRENCE_CODE] [nvarchar] (3) NULL,
	[CHEQUE_CANCELLATION_REASON] [nvarchar] (60) NULL,
	[PAYMENT_METHOD_2] [nvarchar] (3) NULL,
	[CARRIER_BANK_NUMBER] [nvarchar] (5) NULL,
	[CARRIER_BANK_BRANCH_NUMBER] [nvarchar] (10) NULL,
	[CARRIER_BANK_ACCOUNT_NUMBER] [nvarchar] (12) NULL,
	[EXCHANGE_RATE_2] [decimal] (18,7) NULL,
	[CREATED_BY] [nvarchar] (50) NULL,
	[CREATED_DATE] [datetime] NULL,
	[FILE_NAME] [nvarchar] (50) NULL,
	[FULL_RECORD] [varchar] (MAX) NULL,
	[INCONSISTENCY_1] [varchar] (5) NULL,
	[INCONSISTENCY_2] [varchar] (5) NULL,
	[INCONSISTENCY_3] [varchar] (5) NULL,
	[INCONSISTENCY_4] [varchar] (5) NULL,
	[INCONSISTENCY_5] [varchar] (5) NULL
) ON [PRIMARY]
GO

