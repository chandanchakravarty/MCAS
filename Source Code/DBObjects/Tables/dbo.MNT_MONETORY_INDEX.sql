IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_MONETORY_INDEX]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_MONETORY_INDEX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_MONETORY_INDEX](
	[ROW_ID] [int] NOT NULL,
	[DATE] [datetime] NULL,
	[INFLATION_RATE] [decimal] (15,2) NOT NULL CONSTRAINT [DF_MNT_MONETORY_INDEX_INFLATION_RATE] DEFAULT ((0)),
	[INTEREST_RATE] [decimal] (15,2) NOT NULL CONSTRAINT [DF_MNT_MONETORY_INDEX_INTEREST_RATE] DEFAULT ((0)),
	[IS_ACTIVE] [char] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_MONETORY_INDEX] ADD CONSTRAINT [PK_MNT_MONETORY_INDEX ROW_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[ROW_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

