IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_MOTORCYCLE_CC]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_MOTORCYCLE_CC]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_MOTORCYCLE_CC](
	[CC_ID] [int] NOT NULL,
	[CC_RANGE1] [int] NULL,
	[CC_RANGE2] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_MOTORCYCLE_CC] ADD CONSTRAINT [PK_CC_ID] PRIMARY KEY
	CLUSTERED
	(
		[CC_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

