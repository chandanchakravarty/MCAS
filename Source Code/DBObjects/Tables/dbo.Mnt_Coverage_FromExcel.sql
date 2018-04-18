IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mnt_Coverage_FromExcel]') AND type in (N'U'))
DROP TABLE [dbo].[Mnt_Coverage_FromExcel]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Mnt_Coverage_FromExcel](
	[SUSEPLOB] [float] (53) NULL,
	[SUSEPSUBLOB] [float] (53) NULL,
	[COVERAGEID] [float] (53) NULL,
	[COVERAGEDESCRIPTION] [nvarchar] (255) NULL,
	[MAINCOVERAGE] [nvarchar] (255) NULL,
	[USEDFORRI] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

