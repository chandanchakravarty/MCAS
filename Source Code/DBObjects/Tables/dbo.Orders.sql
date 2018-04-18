IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
DROP TABLE [dbo].[Orders]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Orders](
	[Ord_ID] [int] NOT NULL IDENTITY (1,1),
	[Ord_Priority] [varchar] (10) NULL
) ON [PRIMARY]
GO

