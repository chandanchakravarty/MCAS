IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZipCode]') AND type in (N'U'))
DROP TABLE [dbo].[ZipCode]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ZipCode](
	[ZipCode] [int] NULL,
	[StreetType] [nvarchar] (3) NULL,
	[StreetName] [nvarchar] (40) NULL,
	[District] [nvarchar] (20) NULL,
	[City] [nvarchar] (40) NULL,
	[State] [nvarchar] (2) NULL
) ON [PRIMARY]
GO

