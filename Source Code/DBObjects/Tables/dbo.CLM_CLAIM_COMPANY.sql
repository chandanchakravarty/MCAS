IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_CLAIM_COMPANY]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_CLAIM_COMPANY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLM_CLAIM_COMPANY](
	[CLAIM_ID] [int] NOT NULL,
	[COMPANY_ID] [int] NOT NULL,
	[AGENCY_ID] [int] NOT NULL,
	[NAIC_CODE] [nvarchar] (15) NULL,
	[REFERENCE_NUMBER] [nvarchar] (20) NULL,
	[CAT_NUMBER] [nvarchar] (20) NULL,
	[EFFECTIVE_DATE] [datetime] NULL,
	[EXPIRATION_DATE] [datetime] NULL,
	[ACCIDENT_DATE_TIME] [datetime] NULL,
	[PREVIOUSLY_REPORTED] [char] (1) NULL,
	[INSURED_CONTACT_ID] [int] NULL,
	[CONTACT_NAME] [nvarchar] (75) NULL,
	[CONTACT_ADDRESS1] [nvarchar] (75) NOT NULL,
	[CONTACT_ADDRESS2] [nvarchar] (75) NOT NULL,
	[CONTACT_CITY] [nvarchar] (20) NOT NULL,
	[CONTACT_STATE] [int] NULL,
	[CONTACT_COUNTRY] [int] NULL,
	[CONTACT_ZIP] [nvarchar] (11) NULL,
	[CONTACT_HOMEPHONE] [nvarchar] (20) NULL,
	[CONTACT_WORKPHONE] [nvarchar] (20) NULL,
	[IS_ACTIVE] [char] (1) NOT NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[LOSS_TIME_AM_PM] [smallint] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_CLAIM_COMPANY] ADD CONSTRAINT [PK_CLM_CLAIM_COMPANY_CLAIM_ID_COMPANY_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CLAIM_ID] ASC
		,[COMPANY_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_CLAIM_COMPANY] ADD CONSTRAINT [FK_CLM_CLAIM_COMPANY_CLAIM_ID] FOREIGN KEY
	(
		[CLAIM_ID]
	)
	REFERENCES [dbo].[CLM_CLAIM_INFO]
	(
		[CLAIM_ID]
	) 
GO

