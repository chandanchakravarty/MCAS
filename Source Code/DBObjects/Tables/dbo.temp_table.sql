IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_table]') AND type in (N'U'))
DROP TABLE [dbo].[temp_table]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[temp_table](
	[ID] [int] NOT NULL IDENTITY (1,1),
	[NAME] [varchar] (50) NULL,
	[AGE] [int] NULL,
	[SALARY] [int] NULL
) ON [PRIMARY]
GO

