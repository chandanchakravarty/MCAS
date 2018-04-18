IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ebxdv25_image]') AND type in (N'U'))
DROP TABLE [dbo].[ebxdv25_image]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ebxdv25_image](
	[id] [int] NOT NULL IDENTITY (1,1),
	[photoImage] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

