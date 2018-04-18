IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_DISCOUNT_SURCHARGE]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_DISCOUNT_SURCHARGE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_DISCOUNT_SURCHARGE](
	[DISCOUNT_ID] [int] NOT NULL,
	[TYPE_ID] [int] NULL,
	[LOB_ID] [int] NULL,
	[SUBLOB_ID] [int] NULL,
	[DISCOUNT_TYPE] [int] NULL,
	[DISCOUNT_DESCRIPTION] [nvarchar] (100) NULL,
	[PERCENTAGE] [decimal] (12,4) NULL CONSTRAINT [DF_MNT_DISCOUNT_SURCHARGE_PERCENTAGE] DEFAULT ((0)),
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[EFFECTIVE_DATE] [datetime] NULL,
	[FINAL_DATE] [datetime] NULL,
	[LEVEL] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_DISCOUNT_SURCHARGE] ADD CONSTRAINT [PK_MNT_DISCOUNT_SURCHARGE_DISCOUNT_ID] PRIMARY KEY
	CLUSTERED
	(
		[DISCOUNT_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

