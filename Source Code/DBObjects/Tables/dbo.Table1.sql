IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table1]') AND type in (N'U'))
DROP TABLE [dbo].[Table1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Table1](
	[ID] [numeric] (18,0) NOT NULL,
	[stvalue] [numeric] (18,0) NULL,
	[endvalue] [numeric] (18,0) NULL,
	[month] [int] NULL,
	[year] [int] NULL,
	[stdate] [datetime] NULL,
	[Enddate] [datetime] NULL
) ON [PRIMARY]
GO

