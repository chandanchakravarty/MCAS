IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_VEH_MODEL_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[TEMP_VEH_MODEL_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TEMP_VEH_MODEL_LIST](
	[SL No#] [float] (53) NULL,
	[Effective From] [datetime] NULL,
	[Effective To] [datetime] NULL,
	[Vehicle Make] [nvarchar] (255) NULL,
	[Vehicle Model] [nvarchar] (255) NULL,
	[Body Type ] [nvarchar] (255) NULL,
	[Rate] [float] (53) NULL
) ON [PRIMARY]
GO

