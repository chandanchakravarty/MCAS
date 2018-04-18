IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_LOB_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_LOB_MASTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_LOB_MASTER](
	[LOB_ID] [smallint] NOT NULL,
	[LOB_CODE] [nvarchar] (20) NULL,
	[LOB_DESC] [nvarchar] (70) NULL,
	[LOB_CATEGORY] [nvarchar] (20) NULL,
	[LOB_TYPE] [char] (1) NULL,
	[LOB_PKG] [char] (1) NULL,
	[LOB_ACORD_STD] [char] (1) NULL,
	[DEF_CLAIMS_TYPE] [nvarchar] (2) NULL,
	[LOB_PREFIX] [nvarchar] (10) NULL,
	[LOB_SUFFIX] [nvarchar] (10) NULL,
	[LOB_SEED] [int] NULL,
	[MAPPING_LOOKUP_ID] [int] NULL,
	[OVERRIDE_LOB_PREFIX] [char] (1) NULL,
	[REWRITE_SEED] [nvarchar] (7) NULL,
	[SUSEP_LOB_ID] [int] NULL,
	[SUSEP_LOB_CODE] [nvarchar] (10) NULL,
	[COMMISSION_LEVEL] [char] (2) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[APPLICABLE_COMMISSION] [nvarchar] (100) NULL,
	[PRODUCT_TYPE] [int] NULL,
	[MIN_CANCELLATION_DAYS] [smallint] NULL,
	[ADMINISTRATIVE_EXPENSE] [decimal] (10,2) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_LOB_MASTER] ADD CONSTRAINT [PK_MNT_LOB_MASTER_LOB_ID] PRIMARY KEY
	CLUSTERED
	(
		[LOB_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_LOB_MASTER] ADD CONSTRAINT [FK_MNT_LOB_MASTER_SUSEP_LOB_ID] FOREIGN KEY
	(
		[SUSEP_LOB_ID]
	)
	REFERENCES [dbo].[MNT_SUSEP_LOB_MASTER]
	(
		[SUSEP_LOB_ID]
	) 
GO

