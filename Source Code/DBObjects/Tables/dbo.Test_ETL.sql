IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test_ETL]') AND type in (N'U'))
DROP TABLE [dbo].[Test_ETL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Test_ETL](
	[Column 0] [nvarchar] (MAX) NULL,
	[Policy_ID] [nvarchar] (100) NULL
) ON [PRIMARY]
GO

