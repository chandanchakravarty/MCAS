IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_EOD_PROCESS]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_EOD_PROCESS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_EOD_PROCESS](
	[PROCESS_ID] [int] NOT NULL IDENTITY (1,1),
	[STEP_NO] [smallint] NULL,
	[ACTIVITY_CODE] [nvarchar] (30) NULL,
	[ACTIVITY_DESC] [nvarchar] (50) NULL,
	[START_DATE_TIME] [datetime] NULL,
	[END_DATE_TIME] [datetime] NULL,
	[STATUS] [nvarchar] (2) NULL,
	[STARTED_BY] [smallint] NULL
) ON [PRIMARY]
GO

