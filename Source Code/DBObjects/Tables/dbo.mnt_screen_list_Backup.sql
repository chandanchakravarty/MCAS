IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mnt_screen_list_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[mnt_screen_list_Backup]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[mnt_screen_list_Backup](
	[SCREEN_ID] [nvarchar] (20) NULL,
	[SCREEN_DESC] [nvarchar] (255) NULL,
	[SCREEN_PATH] [nvarchar] (512) NULL,
	[SCREEN_READ] [bit] NULL,
	[SCREEN_WRITE] [bit] NULL,
	[SCREEN_DELETE] [bit] NULL,
	[SCREEN_EXECUTE] [bit] NULL,
	[IS_ACTIVE] [nchar] (1) NULL
) ON [PRIMARY]
GO

