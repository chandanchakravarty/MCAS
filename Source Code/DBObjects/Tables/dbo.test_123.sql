IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test_123]') AND type in (N'U'))
DROP TABLE [dbo].[test_123]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[test_123](
	[a] [nvarchar] (MAX) NULL,
	[b] [nvarchar] (MAX) NOT NULL,
	[c] [nvarchar] (MAX) NULL,
	[d] [nvarchar] (MAX) NULL,
	[e] [nvarchar] (MAX) NULL,
	[f] [nvarchar] (MAX) NULL,
	[g] [nvarchar] (MAX) NULL,
	[h] [nvarchar] (MAX) NULL,
	[i] [nvarchar] (MAX) NULL
) ON [PRIMARY]
GO

