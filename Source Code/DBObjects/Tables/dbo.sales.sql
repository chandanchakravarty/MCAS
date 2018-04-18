IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sales]') AND type in (N'U'))
DROP TABLE [dbo].[sales]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sales](
	[year] [int] NOT NULL,
	[country] [nvarchar] (20) NULL,
	[product] [nvarchar] (32) NULL,
	[profit] [int] NULL
) ON [PRIMARY]
GO

