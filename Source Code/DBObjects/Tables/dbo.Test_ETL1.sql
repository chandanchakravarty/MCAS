IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test_ETL1]') AND type in (N'U'))
DROP TABLE [dbo].[Test_ETL1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Test_ETL1](
	[Column 0] [nvarchar] (MAX) NULL,
	[Derived Column 1] [nvarchar] (40) NULL
) ON [PRIMARY]
GO

