IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test2]') AND type in (N'U'))
DROP TABLE [dbo].[test2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[test2](
	[name] [nvarchar] (20) NULL
) ON [PRIMARY]
GO

