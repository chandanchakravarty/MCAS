IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OLE DB Destination1]') AND type in (N'U'))
DROP TABLE [dbo].[OLE DB Destination1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[OLE DB Destination1](
	[PolicyID] [nvarchar] (50) NULL
) ON [PRIMARY]
GO

