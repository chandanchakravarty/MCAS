IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_PERILS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_PERILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_PERILS](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[PERIL_ID] [smallint] NOT NULL,
	[CALCULATION_NUMBER] [int] NULL,
	[LOCATION] [int] NULL,
	[ADDRESS] [nvarchar] (80) NULL,
	[NUMBER] [nvarchar] (6) NULL,
	[COMPLEMENT] [nvarchar] (20) NULL,
	[CITY] [nvarchar] (30) NULL,
	[COUNTRY] [nvarchar] (5) NULL,
	[STATE] [nvarchar] (5) NULL,
	[ZIP] [nvarchar] (11) NULL,
	[TELEPHONE] [nvarchar] (12) NULL,
	[EXTENTION] [nvarchar] (6) NULL,
	[FAX] [nvarchar] (12) NULL,
	[CATEGORY] [nvarchar] (6) NULL,
	[ATIV_CONTROL] [int] NULL,
	[LOC] [nvarchar] (4) NULL,
	[LOCALIZATION] [nvarchar] (1) NULL,
	[OCCUPANCY] [nvarchar] (8) NULL,
	[CONSTRUCTION] [nvarchar] (8) NULL,
	[LOC_CITY] [nvarchar] (40) NULL,
	[CONSTRUCTION_TYPE] [nvarchar] (11) NULL,
	[ACTIVITY_TYPE] [nvarchar] (50) NULL,
	[RISK_TYPE] [nvarchar] (50) NULL,
	[VR] [numeric] (18,2) NULL,
	[LMI] [numeric] (18,2) NULL,
	[BUILDING] [numeric] (18,2) NULL,
	[MMU] [numeric] (12,0) NULL,
	[MMP] [numeric] (12,0) NULL,
	[MRI] [numeric] (18,2) NULL,
	[TYPE] [int] NULL,
	[LOSS] [numeric] (18,2) NULL,
	[LOYALTY] [numeric] (12,0) NULL,
	[PERC_LOYALTY] [numeric] (12,0) NULL,
	[DEDUCTIBLE_OPTION] [int] NULL,
	[MULTIPLE_DEDUCTIBLE] [nvarchar] (8) NULL,
	[E_FIRE] [int] NULL,
	[S_FIXED_FOAM] [int] NULL,
	[S_FIXED_INSERT_GAS] [int] NULL,
	[CAR_COMBAT] [int] NULL,
	[S_DETECT_ALARM] [int] NULL,
	[S_FIRE_UNIT] [int] NULL,
	[S_FOAM_PER_MANUAL] [int] NULL,
	[S_MANUAL_INERT_GAS] [int] NULL,
	[S_SEMI_HOSES] [int] NULL,
	[HYDRANTS] [int] NULL,
	[SHOWERS] [int] NULL,
	[SHOWER_CLASSIFICATION] [nvarchar] (100) NULL,
	[FIRE_CORPS] [nvarchar] (100) NULL,
	[PUNCTUATION_QUEST] [int] NULL,
	[DMP] [numeric] (12,0) NULL,
	[EXPLOSION_DEGREE] [nvarchar] (1) NULL,
	[PR_LIQUID] [int] NULL,
	[COD_ATIV_DRAFTS] [nvarchar] (87) NULL,
	[OCCUPATION_TEXT] [nvarchar] (50) NULL,
	[ASSIST24] [int] NULL,
	[LMRA] [numeric] (12,0) NULL,
	[AGGRAVATION_RCG_AIR] [numeric] (12,0) NULL,
	[EXPLOSION_DESC] [numeric] (12,0) NULL,
	[PROTECTIVE_DESC] [numeric] (12,0) NULL,
	[LMI_DESC] [numeric] (12,0) NULL,
	[LOSS_DESC] [numeric] (12,0) NULL,
	[QUESTIONNAIRE_DESC] [numeric] (12,0) NULL,
	[DEDUCTIBLE_DESC] [numeric] (12,0) NULL,
	[GROUPING_DESC] [numeric] (12,0) NULL,
	[LOC_FLOATING] [nvarchar] (4) NULL,
	[ADJUSTABLE] [numeric] (12,0) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[CORRAL_SYSTEM] [nvarchar] (20) NULL,
	[RAWVALUES] [nvarchar] (20) NULL,
	[REMARKS] [nvarchar] (4000) NULL,
	[PARKING_SPACES] [nvarchar] (20) NULL,
	[CLAIM_RATIO] [decimal] (12,2) NULL,
	[RAW_MATERIAL_VALUE] [nvarchar] (20) NULL,
	[CONTENT_VALUE] [nvarchar] (20) NULL,
	[BONUS] [decimal] (12,2) NULL,
	[CO_APPLICANT_ID] [int] NULL CONSTRAINT [DF_POL_PERILS_CO_APPLICANT_ID] DEFAULT ((0)),
	[LOCATION_NUMBER] [int] NULL,
	[ITEM_NUMBER] [int] NULL,
	[ACTUAL_INSURED_OBJECT] [nvarchar] (250) NULL,
	[ORIGINAL_VERSION_ID] [int] NULL,
	[CO_RISK_ID] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_PERILS] ADD CONSTRAINT [PK_POL_PERILS_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID_PERIL_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[PERIL_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_PERILS] ADD CONSTRAINT [FK_POL_PERILS_POLICY_ID_POLICY_VERSION_ID_CUSTOMER_ID] FOREIGN KEY
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

