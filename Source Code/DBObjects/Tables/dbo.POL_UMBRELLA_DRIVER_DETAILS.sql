IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_UMBRELLA_DRIVER_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_UMBRELLA_DRIVER_DETAILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_UMBRELLA_DRIVER_DETAILS](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[DRIVER_ID] [smallint] NOT NULL,
	[DRIVER_FNAME] [nvarchar] (75) NULL,
	[DRIVER_MNAME] [nvarchar] (25) NULL,
	[DRIVER_LNAME] [nvarchar] (75) NULL,
	[DRIVER_CODE] [nvarchar] (20) NULL,
	[DRIVER_SUFFIX] [nvarchar] (10) NULL,
	[DRIVER_ADD1] [nvarchar] (70) NULL,
	[DRIVER_ADD2] [nvarchar] (70) NULL,
	[DRIVER_CITY] [nvarchar] (40) NULL,
	[DRIVER_STATE] [nvarchar] (5) NULL,
	[DRIVER_ZIP] [nvarchar] (11) NULL,
	[DRIVER_COUNTRY] [nchar] (5) NULL,
	[DRIVER_HOME_PHONE] [nvarchar] (20) NULL,
	[DRIVER_BUSINESS_PHONE] [nvarchar] (20) NULL,
	[DRIVER_EXT] [nvarchar] (5) NULL,
	[DRIVER_MOBILE] [nvarchar] (20) NULL,
	[DRIVER_DOB] [datetime] NULL,
	[DRIVER_SSN] [nvarchar] (44) NULL,
	[DRIVER_MART_STAT] [nchar] (1) NULL,
	[DRIVER_SEX] [nchar] (1) NULL,
	[DRIVER_DRIV_LIC] [nvarchar] (30) NULL,
	[DRIVER_LIC_STATE] [nvarchar] (5) NULL,
	[DRIVER_LIC_CLASS] [nvarchar] (5) NULL,
	[DATE_EXP_START] [datetime] NULL,
	[DATE_LICENSED] [datetime] NULL,
	[DRIVER_REL] [nvarchar] (5) NULL,
	[DRIVER_DRIV_TYPE] [nvarchar] (5) NULL,
	[DRIVER_OCC_CODE] [nvarchar] (6) NULL,
	[DRIVER_OCC_CLASS] [nvarchar] (5) NULL,
	[DRIVER_DRIVERLOYER_NAME] [nvarchar] (75) NULL,
	[DRIVER_DRIVERLOYER_ADD] [nvarchar] (255) NULL,
	[DRIVER_INCOME] [decimal] (18,2) NULL,
	[DRIVER_BROADEND_NOFAULT] [smallint] NULL,
	[DRIVER_PHYS_MED_IMPAIRE] [nchar] (1) NULL,
	[DRIVER_DRINK_VIOLATION] [nchar] (1) NULL,
	[DRIVER_PREF_RISK] [nchar] (1) NULL,
	[DRIVER_GOOD_STUDENT] [nchar] (1) NULL,
	[DRIVER_STUD_DIST_OVER_HUNDRED] [nchar] (1) NULL,
	[DRIVER_LIC_SUSPENDED] [nchar] (1) NULL,
	[DRIVER_VOLUNTEER_POLICE_FIRE] [nchar] (1) NULL,
	[DRIVER_US_CITIZEN] [nchar] (1) NULL,
	[DRIVER_FAX] [varchar] (15) NULL,
	[RELATIONSHIP] [int] NULL,
	[DRIVER_TITLE] [int] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[SAFE_DRIVER_RENEWAL_DISCOUNT] [nchar] (2) NULL,
	[APP_VEHICLE_PRIN_OCC_ID] [int] NULL,
	[VEHICLE_ID] [int] NULL,
	[OP_VEHICLE_ID] [smallint] NULL,
	[OP_APP_VEHICLE_PRIN_OCC_ID] [int] NULL,
	[OP_DRIVER_COST_GAURAD_AUX] [int] NULL,
	[FORM_F95] [int] NULL,
	[MOT_VEHICLE_ID] [smallint] NULL,
	[MOT_APP_VEHICLE_PRIN_OCC_ID] [smallint] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_UMBRELLA_DRIVER_DETAILS] ADD CONSTRAINT [PK_POL_UMBRELLA_DRIVER_DETAILS_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_DRIVER_ID] PRIMARY KEY
	CLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[DRIVER_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

