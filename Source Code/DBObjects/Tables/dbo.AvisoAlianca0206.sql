IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AvisoAlianca0206]') AND type in (N'U'))
DROP TABLE [dbo].[AvisoAlianca0206]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AvisoAlianca0206](
	[SUSEP_Class_of_Business] [int] NULL,
	[LEADER_POLICY_NUMBER] [int] NULL,
	[LEADER_DOC_NUMBER] [int] NULL,
	[LEADER_CLAIM_NUMBER] [nvarchar] (7) NULL,
	[LEADER_INSURANCE_CARRIER] [int] NULL
) ON [PRIMARY]
GO

