IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ETL_TEST_FNOL]') AND type in (N'U'))
DROP TABLE [dbo].[ETL_TEST_FNOL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ETL_TEST_FNOL](
	[LOB_ID] [nvarchar] (255) NULL,
	[POLICY NO#] [nvarchar] (255) NULL,
	[ENDORSEMENT NO#] [nvarchar] (255) NULL,
	[CLAIM_CURRENCY_ID] [nvarchar] (255) NULL,
	[CLAIM NO#] [nvarchar] (255) NULL,
	[OFFICIAL CLAIM NO#] [nvarchar] (255) NULL,
	[Linked to Claim] [nvarchar] (255) NULL,
	[Date of Loss] [nvarchar] (255) NULL,
	[Time of Loss] [nvarchar] (255) NULL,
	[ADJUSTER_ID] [nvarchar] (255) NULL,
	[File in Litigation] [nvarchar] (255) NULL,
	[CO_INSURANCE_TYPE] [nvarchar] (255) NULL,
	[Reported To] [nvarchar] (255) NULL,
	[Reported By] [nvarchar] (255) NULL,
	[Catastrophe Event Code] [nvarchar] (255) NULL,
	[At Fault Indicator] [nvarchar] (255) NULL,
	[Claimant Party] [nvarchar] (255) NULL,
	[Notify Reinsurer] [nvarchar] (255) NULL,
	[First Notice of Loss] [nvarchar] (255) NULL,
	[Claim Status] [nvarchar] (255) NULL,
	[Claim SubStatus] [nvarchar] (255) NULL,
	[Claim Description (Max# 1000 Characters)] [nvarchar] (255) NULL,
	[LAST_DOC_RECEIVE_DATE] [datetime] NULL,
	[Reinsurance Type] [nvarchar] (255) NULL,
	[Reinsurance Claim Number] [nvarchar] (255) NULL,
	[Controlling Number of Notice of Loss for Reinsurer] [nvarchar] (255) NULL,
	[Is Victim Claim] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

