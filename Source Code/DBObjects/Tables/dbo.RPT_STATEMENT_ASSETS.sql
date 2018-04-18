IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RPT_STATEMENT_ASSETS]') AND type in (N'U'))
DROP TABLE [dbo].[RPT_STATEMENT_ASSETS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RPT_STATEMENT_ASSETS](
	[GL_ID] [int] NULL,
	[ACCOUNT_ID] [int] NULL,
	[ACC_NUMBER] [decimal] (18,2) NULL,
	[ACC_TYPE] [int] NULL,
	[ACC_TYPE_DESC] [nvarchar] (100) NULL,
	[YEAR_MTD] [decimal] (18,2) NULL,
	[PRIOR_YEAR_MTD] [decimal] (18,2) NULL,
	[VARIANCE_MTD] [decimal] (18,2) NULL,
	[CHNG_MTD] [decimal] (18,2) NULL,
	[YEAR_YTD] [decimal] (18,2) NULL,
	[PRIOR_YEAR_YTD] [decimal] (18,2) NULL,
	[VARIANCE_YTD] [decimal] (18,2) NULL,
	[CHNG_YTD] [decimal] (18,2) NULL,
	[LEDGER_NAME] [nvarchar] (100) NULL,
	[ACC_DESC] [nvarchar] (100) NULL,
	[RPT_MONTH] [int] NULL,
	[RPT_YEAR] [int] NULL,
	[RPT_DAY] [int] NULL
) ON [PRIMARY]
GO

