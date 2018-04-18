GO

/****** Object:  Table [dbo].[POL_RISK_DETAIL]    Script Date: 01/13/2012 16:51:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_RISK_DETAIL](
	[LOB_ID] [int] NOT NULL,
	[SUSEP_LOB_CODE] [nvarchar](100) NULL,
	[DISPLAY_COLUMN_NAME] [nvarchar](200) NULL,
	[TABLE_NAME] [nvarchar](max) NULL,
	[RISK_ID] [nvarchar](200) NULL
) ON [PRIMARY]

GO


