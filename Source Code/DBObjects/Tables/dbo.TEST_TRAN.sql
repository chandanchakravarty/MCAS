IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEST_TRAN]') AND type in (N'U'))
DROP TABLE [dbo].[TEST_TRAN]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEST_TRAN](
	[IDEN_COL] [int] NOT NULL IDENTITY (1,1),
	[TEST_COL] [nvarchar] (100) NULL
) ON [PRIMARY]
GO

