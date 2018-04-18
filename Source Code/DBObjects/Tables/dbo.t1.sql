IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[t1]') AND type in (N'U'))
DROP TABLE [dbo].[t1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t1](
	[STRR] [nvarchar] (1604) NULL,
	[ID] [int] NOT NULL IDENTITY (1,1)
) ON [PRIMARY]
GO

