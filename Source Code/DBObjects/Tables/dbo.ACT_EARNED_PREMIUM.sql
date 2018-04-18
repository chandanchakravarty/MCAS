IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_EARNED_PREMIUM]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_EARNED_PREMIUM]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_EARNED_PREMIUM](
	[IDEN_ROW_ID] [int] NOT NULL IDENTITY (1,1),
	[MONTH_NUMBER] [int] NULL,
	[YEAR_NUMBER] [int] NULL,
	[POLICY_NUMBER] [nvarchar] (20) NULL,
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [int] NULL,
	[CURRENT_TERM] [int] NULL,
	[AGENCY_ID] [int] NULL,
	[STATE_ID] [int] NULL,
	[POLICY_EFFECTIVE_DATE] [datetime] NULL,
	[POLICY_EXPIRATION_DATE] [datetime] NULL,
	[TRAN_EFFECTIVE_DATE] [datetime] NULL,
	[PROCESS_ID] [int] NULL,
	[POLICY_TERM] [int] NULL,
	[MONTH_ELAPSED] [int] NULL,
	[UNEARNED_FACTOR_END] [decimal] (18,6) NULL,
	[UNEARNED_FACTOR_BEG] [decimal] (18,6) NULL,
	[INFORCE_PREMIUM] [decimal] (18,6) NULL,
	[BEGINNING_UNEARNED] [decimal] (18,6) NULL,
	[WRITTEN_PREMIUM] [decimal] (18,6) NULL,
	[ENDING_UNEARNED] [decimal] (18,2) NULL,
	[EARNED_PREMIUM] [decimal] (18,2) NULL,
	[COVERAGEID] [nvarchar] (20) NULL,
	[RISK_ID] [int] NULL,
	[RISK_TYPE] [nvarchar] (20) NULL,
	[VERSION_FOR_RISK] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_EARNED_PREMIUM] ADD CONSTRAINT [PK_ACT_EARNED_PREMIUM_IDEN_ROW_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[IDEN_ROW_ID] ASC
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

