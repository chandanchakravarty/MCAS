IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEST_TRIGGER]') AND type in (N'U'))
DROP TABLE [dbo].[TEST_TRIGGER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEST_TRIGGER](
	[Client_name] [nvarchar] (100) NULL,
	[Claint_Zip] [nvarchar] (100) NULL
) ON [PRIMARY]
GO

