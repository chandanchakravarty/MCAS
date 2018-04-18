IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sheet1$]') AND type in (N'U'))
DROP TABLE [dbo].[Sheet1$]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sheet1$](
	[ID] [float] (53) NULL,
	[Model] [nvarchar] (255) NULL,
	[Type Designator] [nvarchar] (255) NULL,
	[Aircraft Category] [float] (53) NULL,
	[Engine Count] [float] (53) NULL,
	[Maker Id] [float] (53) NULL
) ON [PRIMARY]
GO

