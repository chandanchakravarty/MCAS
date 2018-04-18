IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RefundDataTemp]') AND type in (N'U'))
DROP TABLE [dbo].[RefundDataTemp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RefundDataTemp](
	[ROW_ID] [int] NULL,
	[TABLE_NAME] [varchar] (MAX) NULL,
	[IS_COMMISSION_PROCESS] [varchar] (2) NULL,
	[CLAIM_ID] [int] NULL,
	[ACTIVITY_ID] [int] NULL,
	[COV_ID] [int] NULL,
	[FLAG_RI_CLAIM] [int] NULL,
	[COMP_ID] [int] NULL
) ON [PRIMARY]
GO

