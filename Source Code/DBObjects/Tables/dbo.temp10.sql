IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp10]') AND type in (N'U'))
DROP TABLE [dbo].[temp10]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp10](
	[strr] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

