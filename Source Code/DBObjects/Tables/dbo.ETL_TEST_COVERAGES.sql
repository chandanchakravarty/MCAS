IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ETL_TEST_COVERAGES]') AND type in (N'U'))
DROP TABLE [dbo].[ETL_TEST_COVERAGES]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ETL_TEST_COVERAGES](
	[POLICY NO#] [nvarchar] (255) NULL,
	[ENDORSEMENT NO#] [float] (53) NULL,
	[CLAIM NO#] [nvarchar] (255) NULL,
	[Official Claim Number] [nvarchar] (255) NULL,
	[COVERAGE ID] [float] (53) NULL,
	[Limit Override] [nvarchar] (255) NULL,
	[Limit 1] [float] (53) NULL,
	[Deductible Amount] [nvarchar] (255) NULL,
	[Is Risk Coverage] [nvarchar] (255) NULL,
	[Policy Limit] [float] (53) NULL,
	[Deductible Amount text] [nvarchar] (255) NULL,
	[Victim Serial Number] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

