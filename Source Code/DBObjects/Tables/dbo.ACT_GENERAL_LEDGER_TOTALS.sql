IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_GENERAL_LEDGER_TOTALS]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_GENERAL_LEDGER_TOTALS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_GENERAL_LEDGER_TOTALS](
	[GL_TOTALS_ID] [int] NOT NULL IDENTITY (1,1),
	[GL_ID] [int] NULL,
	[ACCOUNT_ID] [int] NULL,
	[BROUGHT_DOWN_AMOUNT] [decimal] (18,2) NULL,
	[CURRENT_MTD_BALANCE] [decimal] (18,2) NULL,
	[CURRENT_YTD_BALANCE] [decimal] (18,2) NULL,
	[FISCAL_START_MONTH] [smallint] NULL,
	[FISCAL_START_YEAR] [smallint] NULL,
	[FISCAL_END_MONTH] [smallint] NULL,
	[FISCAL_END_YEAR] [smallint] NULL,
	[FISCAL_START_DATE] [datetime] NULL,
	[FISCAL_END_DATE] [datetime] NULL,
	[YEAR_JAN_MTD] [decimal] (18,2) NULL,
	[YEAR_JAN_YTD] [decimal] (18,2) NULL,
	[YEAR_FEB_MTD] [decimal] (18,2) NULL,
	[YEAR_FEB_YTD] [decimal] (18,2) NULL,
	[YEAR_MAR_MTD] [decimal] (18,2) NULL,
	[YEAR_MAR_YTD] [decimal] (18,2) NULL,
	[YEAR_APR_MTD] [decimal] (18,2) NULL,
	[YEAR_APR_YTD] [decimal] (18,2) NULL,
	[YEAR_MAY_MTD] [decimal] (18,2) NULL,
	[YEAR_MAY_YTD] [decimal] (18,2) NULL,
	[YEAR_JUN_MTD] [decimal] (18,2) NULL,
	[YEAR_JUN_YTD] [decimal] (18,2) NULL,
	[YEAR_JUL_MTD] [decimal] (18,2) NULL,
	[YEAR_JUL_YTD] [decimal] (18,2) NULL,
	[YEAR_AUG_MTD] [decimal] (18,2) NULL,
	[YEAR_AUG_YTD] [decimal] (18,2) NULL,
	[YEAR_SEP_MTD] [decimal] (18,2) NULL,
	[YEAR_SEP_YTD] [decimal] (18,2) NULL,
	[YEAR_OCT_MTD] [decimal] (18,2) NULL,
	[YEAR_OCT_YTD] [decimal] (18,2) NULL,
	[YEAR_NOV_MTD] [decimal] (18,2) NULL,
	[YEAR_NOV_YTD] [decimal] (18,2) NULL,
	[YEAR_DEC_MTD] [decimal] (18,2) NULL,
	[YEAR_DEC_YTD] [decimal] (18,2) NULL,
	[CARRY_FWDED_AMOUNT] [decimal] (18,2) NULL,
	[YEAR_EOY_ENTRY] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_GENERAL_LEDGER_TOTALS] ADD CONSTRAINT [PK_ACT_GENERAL_LEDGER_TOTALS_GL_TOTALS_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[GL_TOTALS_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
		,FILLFACTOR = 90
	) ON [PRIMARY]
GO

