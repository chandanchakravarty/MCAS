IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IMPORT_COVRAGE]') AND type in (N'U'))
DROP TABLE [dbo].[IMPORT_COVRAGE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IMPORT_COVRAGE](
	[ID] [int] NOT NULL IDENTITY (1,1),
	[PolicyNo] [varchar] (MAX) NULL,
	[EndorsementNo] [varchar] (MAX) NULL,
	[COVERAGE_CODE] [varchar] (MAX) NULL,
	[COVERAGE_NAME] [varchar] (MAX) NULL,
	[SUM_INSURED] [varchar] (MAX) NULL,
	[RATE] [varchar] (MAX) NULL,
	[PREMIUM] [varchar] (MAX) NULL,
	[DEDUCTIBLE_TYPE] [varchar] (MAX) NULL,
	[DEDUTIBLE_VALUE] [varchar] (MAX) NULL,
	[DEDUCTIBLE_MIN_AMOUNT] [varchar] (MAX) NULL,
	[DEDUCTIBLE_DESCRIPITION] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

