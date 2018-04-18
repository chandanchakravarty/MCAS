IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Coverage_Test]') AND type in (N'U'))
DROP TABLE [dbo].[Coverage_Test]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Coverage_Test](
	[SUSEP ACCOUNTING LOB] [nvarchar] (MAX) NULL,
	[SUBLOB] [nvarchar] (MAX) NULL,
	[COVERAGE ID] [nvarchar] (MAX) NULL,
	[BASIC COVERAGE TYPE] [nvarchar] (MAX) NULL,
	[COVERAGE DESCRIPTION] [nvarchar] (MAX) NULL,
	[Effective Date
(expire date is the prior day of the Effective Date of a coverage with the same coverage code).] [nvarchar] (MAX) NULL,
	[MAIN COVERAGE
S = YES
N = NO] [nvarchar] (MAX) NULL,
	[COVERAGE USED FOR RI (HAS TO BE SUMMED UP IN CASE OF RI TREATY)
S = YES / N = NO] [nvarchar] (MAX) NULL,
	[PRODUCT FILTER] [nvarchar] (MAX) NULL
) ON [PRIMARY]
GO

