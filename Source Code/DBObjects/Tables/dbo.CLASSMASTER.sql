IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLASSMASTER]') AND type in (N'U'))
DROP TABLE [dbo].[CLASSMASTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLASSMASTER](
	[CLASSID] [int] NOT NULL,
	[CARRIERID] [numeric] (8,0) NULL,
	[CLASSNAME] [nvarchar] (100) NULL,
	[CLASSCODE] [nvarchar] (20) NULL,
	[CLASSDESC] [nvarchar] (512) NULL,
	[MINPREMIUM] [decimal] (15,2) NULL,
	[MAXPREMIUM] [decimal] (15,2) NULL,
	[PARENTCLASSID] [int] NULL,
	[CREATIONDATE] [datetime] NULL,
	[LASTMODIFIEDDATE] [datetime] NULL,
	[MODIFIEDBY] [int] NULL,
	[ISACTIVE] [nchar] (2) NULL,
	[SAMEQUOTING] [nchar] (1) NULL CONSTRAINT [DF__CLASSMAST__SAMEQ__6F0C20AD] DEFAULT ('Y'),
	[DISPLAYORDER] [numeric] (18,0) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLASSMASTER] ADD CONSTRAINT [PK_CLASSMASTER_CLASSID] PRIMARY KEY
	NONCLUSTERED
	(
		[CLASSID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

