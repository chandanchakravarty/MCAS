IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_agent]') AND type in (N'U'))
DROP TABLE [dbo].[temp_agent]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp_agent](
	[STRR] [nvarchar] (1600) NULL
) ON [PRIMARY]
GO

