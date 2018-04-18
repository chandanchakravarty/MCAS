IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_WATERCRAFT_DRIVER_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_WATERCRAFT_DRIVER_DETAILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_WATERCRAFT_DRIVER_DETAILS](
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
	[DRIVER_DOB] [datetime] NULL,
	[DRIVER_SSN] [nvarchar] (44) NULL,
	[DRIVER_SEX] [nchar] (1) NULL,
	[DRIVER_DRIV_LIC] [nvarchar] (30) NULL,
	[DRIVER_LIC_STATE] [nvarchar] (5) NULL,
	[DRIVER_COST_GAURAD_AUX] [int] NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[EXPERIENCE_CREDIT] [decimal] (9,2) NULL,
	[VEHICLE_ID] [smallint] NULL,
	[PERCENT_DRIVEN] [decimal] (9,2) NULL,
	[APP_VEHICLE_PRIN_OCC_ID] [int] NULL,
	[YEARS_LICENSED] [smallint] NULL,
	[WAT_SAFETY_COURSE] [int] NULL,
	[CERT_COAST_GUARD] [int] NULL,
	[REC_VEH_ID] [smallint] NULL,
	[APP_REC_VEHICLE_PRIN_OCC_ID] [int] NULL,
	[DATE_ORDERED] [datetime] NULL,
	[MVR_ORDERED] [int] NULL,
	[VIOLATIONS] [int] NULL,
	[MARITAL_STATUS] [nchar] (1) NULL,
	[MVR_CLASS] [nvarchar] (50) NULL,
	[MVR_LIC_CLASS] [nvarchar] (50) NULL,
	[MVR_LIC_RESTR] [nvarchar] (50) NULL,
	[MVR_DRIV_LIC_APPL] [nvarchar] (50) NULL,
	[MVR_REMARKS] [nvarchar] (250) NULL,
	[MVR_STATUS] [nvarchar] (10) NULL,
	[DRIVER_DRIV_TYPE] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_WATERCRAFT_DRIVER_DETAILS] ADD CONSTRAINT [PK_POL_WATERCRAFT_DRIVER_DETAILS_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_DRIVER_ID] PRIMARY KEY
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

ALTER TABLE [dbo].[POL_WATERCRAFT_DRIVER_DETAILS] WITH NOCHECK ADD CONSTRAINT [FK_POL_WATERCRAFT_DRIVER_DETAILS_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] FOREIGN KEY
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID]
	)
	REFERENCES [dbo].[POL_CUSTOMER_POLICY_LIST]
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID]
	) 
GO

