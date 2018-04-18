IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IMPORT_CO_APPLICANTS]') AND type in (N'U'))
DROP TABLE [dbo].[IMPORT_CO_APPLICANTS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IMPORT_CO_APPLICANTS](
	[ID] [int] NOT NULL IDENTITY (1,1),
	[PolicyNo] [varchar] (MAX) NULL,
	[EndorsementMo] [varchar] (MAX) NULL,
	[APPLICANT_TYPE] [varchar] (MAX) NULL,
	[CONTACT_CODE] [varchar] (MAX) NULL,
	[FIRST_NAME] [varchar] (MAX) NULL,
	[MIDDLE_NAME] [varchar] (MAX) NULL,
	[LAST_NAME] [varchar] (MAX) NULL,
	[ZIP_CODE] [varchar] (MAX) NULL,
	[ADDRESS] [varchar] (MAX) NULL,
	[NUMBER] [varchar] (MAX) NULL,
	[COMPLEMENT] [varchar] (MAX) NULL,
	[DISTRICT] [varchar] (MAX) NULL,
	[CITY] [varchar] (MAX) NULL,
	[COUNTRY] [varchar] (MAX) NULL,
	[State] [varchar] (MAX) NULL,
	[CPF_CNPJ] [varchar] (MAX) NULL,
	[HOME PHONE] [varchar] (MAX) NULL,
	[BUSINESS_PHONE] [varchar] (MAX) NULL,
	[EXT] [varchar] (MAX) NULL,
	[MOBILE NUMBER] [varchar] (MAX) NULL,
	[FAX] [varchar] (MAX) NULL,
	[EMAIL] [varchar] (MAX) NULL,
	[REGIONAL_IDENTIFICATION] [varchar] (MAX) NULL,
	[REG_ID_ISSUE] [varchar] (MAX) NULL,
	[ORIGINAL_ISSUE] [varchar] (MAX) NULL,
	[POSITION] [varchar] (MAX) NULL,
	[DOB] [varchar] (MAX) NULL,
	[Marital Status] [varchar] (MAX) NULL,
	[Gender] [varchar] (MAX) NULL,
	[Remarks] [varchar] (MAX) NULL,
	[TOTAL_COMMISSION_PERCENT] [varchar] (MAX) NULL,
	[TOTAL_FEE_PERCENT] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

