IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_CUSTOMER_BALANCE_INFORMATION]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_CUSTOMER_BALANCE_INFORMATION]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_CUSTOMER_BALANCE_INFORMATION](
	[ROW_ID] [int] NOT NULL IDENTITY (1,1),
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [int] NULL,
	[POLICY_VERSION_ID] [int] NULL,
	[OPEN_ITEM_ROW_ID] [int] NULL,
	[AMOUNT] [decimal] (18,2) NOT NULL CONSTRAINT [DF_ACT_CUSTOMER_BALANCE_INFORMATION_AMOUNT] DEFAULT ((0)),
	[AMOUNT_DUE] [decimal] (18,2) NULL,
	[TRAN_DESC] [nvarchar] (250) NULL,
	[UPDATED_FROM] [nvarchar] (10) NULL,
	[CREATED_DATE] [datetime] NULL,
	[IS_PRINTED] [bit] NULL,
	[PRINT_DATE] [datetime] NULL,
	[SOURCE_ROW_ID] [int] NULL,
	[DUE_DATE] [datetime] NULL,
	[PRINTED_ON_NOTICE] [char] (1) NULL,
	[NOTICE_URL] [nvarchar] (400) NULL,
	[NOTICE_TYPE] [nvarchar] (20) NULL,
	[MIN_DUE] [decimal] (18,2) NULL,
	[TOTAL_PREMIUM_DUE] [decimal] (18,2) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_CUSTOMER_BALANCE_INFORMATION] ADD CONSTRAINT [PK_ACT_CUSTOMER_BALANCE_INFORMATION_ROW_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[ROW_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

