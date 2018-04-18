IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Import_remuneration]') AND type in (N'U'))
DROP TABLE [dbo].[Import_remuneration]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Import_remuneration](
	[COMMISSION_TYPE] [nvarchar] (100) NULL,
	[BROKER_ID] [nvarchar] (100) NULL,
	[BRANCH NUMBER] [varchar] (10) NULL,
	[AMOUNT] [nvarchar] (14) NULL,
	[LEADER] [nvarchar] (4) NULL,
	[BROKER NAME] [varchar] (100) NULL,
	[COMMISSION_PERCENT] [nvarchar] (12) NULL,
	[CO -APPLICANT ID] [varchar] (12) NULL,
	[POLICYNO] [nvarchar] (MAX) NULL,
	[EndorsementNO] [nvarchar] (MAX) NULL,
	[ID] [int] NOT NULL IDENTITY (1,1)
) ON [PRIMARY]
GO

