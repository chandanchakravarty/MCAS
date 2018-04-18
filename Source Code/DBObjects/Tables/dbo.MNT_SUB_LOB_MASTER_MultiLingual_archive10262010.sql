IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_SUB_LOB_MASTER_MultiLingual_archive10262010]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_SUB_LOB_MASTER_MultiLingual_archive10262010]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_SUB_LOB_MASTER_MultiLingual_archive10262010](
	[LOB_ID] [smallint] NULL,
	[SUB_LOB_ID] [smallint] NULL,
	[SUB_LOB_DESC] [nvarchar] (70) NULL,
	[SUB_LOB_CODE] [nvarchar] (20) NULL,
	[LANG_ID] [smallint] NULL
) ON [PRIMARY]
GO

