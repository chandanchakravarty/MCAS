IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OLE DB_Destination_test]') AND type in (N'U'))
DROP TABLE [dbo].[OLE DB_Destination_test]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[OLE DB_Destination_test](
	[Column 0] [varchar] (202) NULL
) ON [PRIMARY]
GO

