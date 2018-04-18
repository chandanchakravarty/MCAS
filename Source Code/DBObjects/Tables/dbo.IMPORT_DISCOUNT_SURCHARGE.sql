IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IMPORT_DISCOUNT_SURCHARGE]') AND type in (N'U'))
DROP TABLE [dbo].[IMPORT_DISCOUNT_SURCHARGE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IMPORT_DISCOUNT_SURCHARGE](
	[ID] [int] NOT NULL IDENTITY (1,1),
	[PolicyNo] [varchar] (MAX) NULL,
	[EndorsementNo] [varchar] (MAX) NULL,
	[Item] [varchar] (MAX) NULL,
	[DiscountID] [varchar] (MAX) NULL,
	[DiscountRate] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

