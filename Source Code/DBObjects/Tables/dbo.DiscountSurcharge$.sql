IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiscountSurcharge$]') AND type in (N'U'))
DROP TABLE [dbo].[DiscountSurcharge$]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DiscountSurcharge$](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[Item] [nvarchar] (255) NULL,
	[DiscountID] [nvarchar] (255) NULL,
	[DiscountRate] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

