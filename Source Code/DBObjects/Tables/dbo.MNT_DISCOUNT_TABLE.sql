IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_DISCOUNT_TABLE]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_DISCOUNT_TABLE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_DISCOUNT_TABLE](
	[LOB_ID] [int] NOT NULL,
	[DISC_COMP_CODE] [nvarchar] (40) NOT NULL,
	[DISC_COMP_DESC] [nvarchar] (80) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_DISCOUNT_TABLE] ADD CONSTRAINT [PK_MNT_DISCOUNT_TABLE_LOB_ID_DISC_COMP_CODE] PRIMARY KEY
	CLUSTERED
	(
		[LOB_ID] ASC
		,[DISC_COMP_CODE] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

