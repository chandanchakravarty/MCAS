IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_PPC_STATE_ZIP]') AND type in (N'U'))
DROP TABLE [dbo].[APP_PPC_STATE_ZIP]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[APP_PPC_STATE_ZIP](
	[STATE_CODE] [nvarchar] (2) NOT NULL,
	[ZIP_CODE] [nvarchar] (5) NOT NULL,
	[RECCNT] [nvarchar] (20) NULL,
	[PPC] [nvarchar] (20) NULL,
	[PCT] [nvarchar] (20) NULL
) ON [PRIMARY]
GO

