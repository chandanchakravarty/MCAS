IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_remuneration]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_remuneration]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_remuneration](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[CoApplicantID] [nvarchar] (255) NULL,
	[COMMISSION_TYPE] [nvarchar] (255) NULL,
	[BROKER_ID] [nvarchar] (255) NULL,
	[BRANCH NUMBER] [nvarchar] (255) NULL,
	[LEADER] [nvarchar] (255) NULL,
	[BROKER NAME] [nvarchar] (255) NULL,
	[COMMISSION_PERCENT] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

