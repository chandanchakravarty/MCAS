IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_BUDGET_PLAN]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_BUDGET_PLAN]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_BUDGET_PLAN](
	[IDEN_ROW_ID] [int] NOT NULL IDENTITY (1,1),
	[BUDGET_CATEGORY_ID] [int] NULL,
	[GL_ID] [int] NULL,
	[JAN_BUDGET] [decimal] (18,2) NULL,
	[FEB_BUDGET] [decimal] (18,2) NULL,
	[MARCH_BUDGET] [decimal] (18,2) NULL,
	[APRIL_BUDGET] [decimal] (18,2) NULL,
	[MAY_BUDGET] [decimal] (18,2) NULL,
	[JUNE_BUDGET] [decimal] (18,2) NULL,
	[JULY_BUDGET] [decimal] (18,2) NULL,
	[AUG_BUDGET] [decimal] (18,2) NULL,
	[SEPT_BUDGET] [decimal] (18,2) NULL,
	[OCT_BUDGET] [decimal] (18,2) NULL,
	[NOV_BUDGET] [decimal] (18,2) NULL,
	[DEC_BUDGET] [decimal] (18,2) NULL,
	[FISCAL_ID] [int] NULL,
	[ACCOUNT_ID] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_BUDGET_PLAN] ADD CONSTRAINT [PK_ACT_BUDGET_PLAN_IDEN_ROW_ID] PRIMARY KEY
	CLUSTERED
	(
		[IDEN_ROW_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

