IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_USER_TYPE_PERMISSION]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_USER_TYPE_PERMISSION]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_USER_TYPE_PERMISSION](
	[USER_TYPE_ID] [smallint] NOT NULL,
	[SCREEN_ID] [varchar] (20) NOT NULL,
	[PERMISSION_XML] [varchar] (100) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_USER_TYPE_PERMISSION] ADD CONSTRAINT [MNT_USER_TYPE_PERMISSION_USER_TYPE_ID_SCREEN_ID] PRIMARY KEY
	CLUSTERED
	(
		[USER_TYPE_ID] ASC
		,[SCREEN_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_USER_TYPE_PERMISSION] ADD CONSTRAINT [MNT_USER_TYPE_PERMISSION_SCREEN_ID] FOREIGN KEY
	(
		[SCREEN_ID]
	)
	REFERENCES [dbo].[MNT_SCREEN_LIST]
	(
		[SCREEN_ID]
	) 
GO

ALTER TABLE [dbo].[MNT_USER_TYPE_PERMISSION] ADD CONSTRAINT [MNT_USER_TYPE_PERMISSION_USER_TYPE_ID] FOREIGN KEY
	(
		[USER_TYPE_ID]
	)
	REFERENCES [dbo].[MNT_USER_TYPES]
	(
		[USER_TYPE_ID]
	) 
GO

