IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Source2]') AND type in (N'U'))
DROP TABLE [dbo].[Source2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Source2](
	[Sou_ID] [int] NOT NULL IDENTITY (1,1),
	[Sou_Desc] [varchar] (10) NULL
) ON [PRIMARY]
GO

