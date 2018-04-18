IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MIG_POLICY_INSTALLMENT_CANCEL]') AND type in (N'U'))
DROP TABLE [dbo].[MIG_POLICY_INSTALLMENT_CANCEL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MIG_POLICY_INSTALLMENT_CANCEL](
	[IMPORT_REQUEST_ID] [int] NOT NULL,
	[IMPORT_SERIAL_NO] [int] NOT NULL,
	[ALBA_POLICY_NUMBER] [nvarchar] (80) NULL,
	[ALBA_ENDORSEMENT_NO] [int] NULL,
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [int] NULL,
	[POLICY_VERSION_ID] [smallint] NULL,
	[IS_ACTIVE] [nchar] (1) NULL CONSTRAINT [DF_MIG_POLICY_INSTALLMENT_CANCEL_IS_ACTIVE] DEFAULT ('N'),
	[IS_DELETED] [nchar] (1) NULL CONSTRAINT [DF_MIG_POLICY_INSTALLMENT_CANCEL_IS_DELETED] DEFAULT ('N'),
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[HAS_ERRORS] [nchar] (1) NULL CONSTRAINT [DF_MIG_POLICY_INSTALLMENT_CANCEL_HAS_ERRORS] DEFAULT ('N'),
	[ALBA_SUSEP_CODE] [nvarchar] (4) NULL,
	[PRODUCT_LOB] [int] NULL,
	[LEADER_POLICY_NUMBER] [nvarchar] (7) NULL,
	[LEADER_ENDORSEMENT_NUMBER] [int] NULL,
	[INSTALLMENT_NUMBER] [int] NULL,
	[LEADER_ENDORSEMENT_TRANSACTION_ID1] [nvarchar] (3) NULL,
	[LEADER_ENDORSEMENT_TRANSACTION_ID2] [nvarchar] (10) NULL,
	[LEADER_POLICY_TRANSACTION_ID1] [nvarchar] (3) NULL,
	[LEADER_POLICY_TRANSACTION_ID2] [nvarchar] (10) NULL,
	[ERROR_TYPES] [varchar] (MAX) NULL,
	[ERROR_COLUMNS] [nvarchar] (MAX) NULL,
	[ERROR_COLUMN_VALUES] [nvarchar] (MAX) NULL,
	[ERROR_SOURCE_FILE] [nvarchar] (1000) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MIG_POLICY_INSTALLMENT_CANCEL] ADD CONSTRAINT [PK_MIG_POLICY_INSTALLMENT_CANCEL_IMPORT_REQUEST_ID_SERIAL_NO] PRIMARY KEY
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

