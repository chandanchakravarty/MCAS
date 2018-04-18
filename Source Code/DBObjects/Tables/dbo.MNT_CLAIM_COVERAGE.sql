IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_CLAIM_COVERAGE]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_CLAIM_COVERAGE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_CLAIM_COVERAGE](
	[COV_ID] [int] NOT NULL IDENTITY (1,1),
	[CLAIM_ID] [int] NULL,
	[COV_ID_CLAIM] [int] NULL,
	[COV_DES] [nvarchar] (500) NULL,
	[STATE_ID] [nvarchar] (10) NULL,
	[LOB_ID] [nvarchar] (10) NULL,
	[LIMIT_1] [decimal] (18,2) NULL,
	[DEDUCTIBLE_1] [decimal] (18,2) NULL,
	[IS_ACTIVE] [char] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_CLAIM_COVERAGE] ADD CONSTRAINT [PK_MNT_CLAIM_COVERAGE_COV_ID] PRIMARY KEY
	CLUSTERED
	(
		[COV_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

