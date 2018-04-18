IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_discountsurcharge]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_discountsurcharge]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_discountsurcharge](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[Item] [nvarchar] (255) NULL,
	[DiscountID] [nvarchar] (255) NULL,
	[DiscountRate] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

