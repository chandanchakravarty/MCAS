IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_COMMISION_CLASS_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_COMMISION_CLASS_MASTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_COMMISION_CLASS_MASTER](
	[COMMISSION_CLASS_ID] [int] NOT NULL,
	[STATE_ID] [smallint] NOT NULL,
	[LOB_ID] [smallint] NOT NULL,
	[SUB_LOB_ID] [smallint] NULL,
	[CLASS_CODE] [nvarchar] (10) NULL,
	[CLASS_DESCRIPTION] [nvarchar] (200) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_COMMISION_CLASS_MASTER] ADD CONSTRAINT [PK_ACT_COMMISION_CLASS_MASTER_COMMISSION_CLASS_ID_LOB_ID_STATE_ID] PRIMARY KEY
	CLUSTERED
	(
		[COMMISSION_CLASS_ID] ASC
		,[STATE_ID] ASC
		,[LOB_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_COMMISION_CLASS_MASTER] ADD CONSTRAINT [FK_ACT_COMMISION_CLASS_MASTER_LOB_ID_MNT_LOB_MASTER_LOB_ID] FOREIGN KEY
	(
		[LOB_ID]
	)
	REFERENCES [dbo].[MNT_LOB_MASTER]
	(
		[LOB_ID]
	) 
GO

