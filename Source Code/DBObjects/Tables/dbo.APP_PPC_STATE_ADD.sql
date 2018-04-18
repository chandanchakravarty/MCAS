IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APP_PPC_STATE_ADD]') AND type in (N'U'))
DROP TABLE [dbo].[APP_PPC_STATE_ADD]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[APP_PPC_STATE_ADD](
	[STATE_CODE] [nvarchar] (2) NOT NULL,
	[ZIP_CODE] [nvarchar] (5) NOT NULL,
	[CITY] [nvarchar] (20) NOT NULL,
	[PRE_CODE] [nvarchar] (20) NULL,
	[STREET] [nvarchar] (30) NULL,
	[POST_CODE] [nvarchar] (20) NULL,
	[TYPE] [nvarchar] (20) NULL,
	[LOW] [nvarchar] (20) NULL,
	[HIGH] [nvarchar] (20) NULL,
	[O_E_B] [nvarchar] (5) NULL,
	[RECCNT] [nvarchar] (20) NULL,
	[FIPS] [nvarchar] (20) NULL,
	[PPC] [nvarchar] (20) NULL,
	[DC] [nvarchar] (20) NULL,
	[FD] [nvarchar] (20) NULL,
	[SS] [nvarchar] (20) NULL,
	[SUB] [nvarchar] (20) NULL,
	[IS_NUMERIC] [varchar] (10) NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ppc_index] ON [dbo].[APP_PPC_STATE_ADD]
(
	[STATE_CODE] ASC,
	[ZIP_CODE] ASC,
	[IS_NUMERIC] ASC,
	[LOW] ASC,
	[HIGH] ASC
)ON [PRIMARY]
GO

