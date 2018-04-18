IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_coverage]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_coverage]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_coverage](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[Item] [nvarchar] (255) NULL,
	[Coverage Code] [nvarchar] (255) NULL,
	[Coverage Name] [nvarchar] (255) NULL,
	[Sum Insured] [nvarchar] (255) NULL,
	[Rate] [nvarchar] (255) NULL,
	[Premium] [nvarchar] (255) NULL,
	[DeductibleType] [nvarchar] (255) NULL,
	[DeductibleValue] [nvarchar] (255) NULL,
	[DeductibleMinAmount] [nvarchar] (255) NULL,
	[DeducbtibleDescripition] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

