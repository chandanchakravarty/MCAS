IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MIG_ERROR_TYPE_DETAIL_MULTILINGUAL]') AND type in (N'U'))
DROP TABLE [dbo].[MIG_ERROR_TYPE_DETAIL_MULTILINGUAL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MIG_ERROR_TYPE_DETAIL_MULTILINGUAL](
	[ERROR_TYPE_ID] [int] NOT NULL,
	[TYPE_DESC] [nvarchar] (150) NOT NULL,
	[LANG_ID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MIG_ERROR_TYPE_DETAIL_MULTILINGUAL] ADD CONSTRAINT [PK_CLM_TYPE_DETAIL_MULTILINGUAL_ERROR_TYPE_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[ERROR_TYPE_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MIG_ERROR_TYPE_DETAIL_MULTILINGUAL] WITH NOCHECK ADD CONSTRAINT [FK_MIG_ERROR_TYPE_DETAIL_MULTILINGUAL_ERROR_TYPE_ID] FOREIGN KEY
	(
		[ERROR_TYPE_ID]
	)
	REFERENCES [dbo].[MIG_ERROR_TYPE_DETAIL]
	(
		[ERROR_TYPE_ID]
	) 
GO

