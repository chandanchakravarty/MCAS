IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_UMBRELLA_WATERCRAFT_ENGINE_INFO]') AND type in (N'U'))
DROP TABLE [dbo].[POL_UMBRELLA_WATERCRAFT_ENGINE_INFO]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_UMBRELLA_WATERCRAFT_ENGINE_INFO](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[ENGINE_ID] [smallint] NOT NULL,
	[ENGINE_NO] [nvarchar] (20) NULL,
	[YEAR] [int] NULL,
	[MAKE] [nvarchar] (75) NULL,
	[MODEL] [nvarchar] (75) NULL,
	[SERIAL_NO] [nvarchar] (75) NULL,
	[HORSEPOWER] [nvarchar] (5) NULL,
	[ASSOCIATED_BOAT] [smallint] NULL,
	[OTHER] [nvarchar] (100) NULL,
	[INSURING_VALUE] [decimal] (9,2) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_UMBRELLA_WATERCRAFT_ENGINE_INFO] ADD CONSTRAINT [PK_POL_UMBRELLA_WATERCRAFT_ENGINE_INFO_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_ENGINE_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[ENGINE_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

