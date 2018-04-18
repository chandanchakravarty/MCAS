IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEST]') AND type in (N'U'))
DROP TABLE [dbo].[TEST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEST](
	[col1] [int] NULL,
	[col2] [int] NULL
) ON [PRIMARY]
GO

