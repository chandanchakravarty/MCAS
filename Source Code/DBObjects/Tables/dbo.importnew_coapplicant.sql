IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_coapplicant]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_coapplicant]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_coapplicant](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[APPLICANT_TYPE] [nvarchar] (255) NULL,
	[CONTACT_CODE] [nvarchar] (255) NULL,
	[FIRST_NAME] [nvarchar] (255) NULL,
	[MIDDLE_NAME] [nvarchar] (255) NULL,
	[LAST_NAME] [nvarchar] (255) NULL,
	[ZIP_CODE] [nvarchar] (255) NULL,
	[ADDRESS] [nvarchar] (255) NULL,
	[NUMBER] [nvarchar] (255) NULL,
	[COMPLEMENT] [nvarchar] (255) NULL,
	[DISTRICT] [nvarchar] (255) NULL,
	[CITY] [nvarchar] (255) NULL,
	[COUNTRY] [nvarchar] (255) NULL,
	[State] [nvarchar] (255) NULL,
	[CPF_CNPJ] [nvarchar] (255) NULL,
	[HOME PHONE] [nvarchar] (255) NULL,
	[BUSINESS_PHONE] [nvarchar] (255) NULL,
	[EXT] [nvarchar] (255) NULL,
	[MOBILE NUMBER ] [nvarchar] (255) NULL,
	[FAX] [nvarchar] (255) NULL,
	[EMAIL] [nvarchar] (255) NULL,
	[REGIONAL_IDENTIFICATION] [nvarchar] (255) NULL,
	[REG_ID_ISSUE] [nvarchar] (255) NULL,
	[ORIGINAL_ISSUE] [nvarchar] (255) NULL,
	[POSITION] [nvarchar] (255) NULL,
	[DOB] [nvarchar] (255) NULL,
	[Marital Status] [nvarchar] (255) NULL,
	[Gender] [nvarchar] (255) NULL,
	[Remarks] [nvarchar] (255) NULL,
	[TOTAL_COMMISSION_PERCENT] [nvarchar] (255) NULL,
	[TOTAL_FEE_PERCENT] [nvarchar] (255) NULL,
	[TOTAL PRO LABORE PERCENT] [nvarchar] (255) NULL,
	[Insured Effective] [varchar] (MAX) NULL,
	[Insured Expire] [varchar] (MAX) NULL,
	[Number of Passengers] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

