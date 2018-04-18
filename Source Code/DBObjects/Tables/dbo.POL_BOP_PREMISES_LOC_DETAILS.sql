/****** Object:  Table [dbo].[POL_BOP_PREMISES_LOC_DETAILS]    Script Date: 11/24/2011 17:52:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_BOP_PREMISES_LOC_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_BOP_PREMISES_LOC_DETAILS]
GO

GO

/****** Object:  Table [dbo].[POL_BOP_PREMISES_LOC_DETAILS]    Script Date: 11/24/2011 17:52:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_BOP_PREMISES_LOC_DETAILS](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NOT NULL,
	[PREMISES_ID] [int] NOT NULL,
	[LOC_DETAILS_ID] [int] NOT NULL,
	[DESC_BLDNG] [nvarchar](150) NULL,
	[DESC_OPERTN] [nvarchar](150) NULL,
	[LST_ALL_OCCUP] [nvarchar](150) NULL,
	[ANN_SALES] [decimal](18, 2) NULL,
	[TOT_PAYROLL] [decimal](18, 2) NULL,
	[RATE_NUM] [int] NULL,
	[RATE_GRP] [int] NULL,
	[RATE_TER_NUM] [int] NULL,
	[PROT_CLS] [nvarchar](150) NULL,
	[IS_ALM_USED] [nvarchar](10) NULL,
	[IS_RES_SPACE] [nvarchar](10) NULL,
	[RES_SPACE_SMK_DET] [nvarchar](10) NULL,
	[RES_OCC] [nvarchar](10) NULL,
	[FIRE_HYDRANT_DIST] [real] NULL,
	[FIRE_STATION_DIST] [real] NULL,
	[FIRE_DIST_NAME] [nvarchar](150) NULL,
	[FIRE_DIST_CODE] [nvarchar](150) NULL,
	[BCEGS] [nvarchar](150) NULL,
	[CITY_LMT] [nvarchar](10) NULL,
	[SWIMMING_POOL] [nvarchar](10) NULL,
	[PLAY_GROUND] [nvarchar](10) NULL,
	[BUILD_UNDER_CON] [nvarchar](10) NULL,
	[BUILD_SHPNG_CENT] [nvarchar](10) NULL,
	[BOILER] [nvarchar](10) NULL,
	[MED_EQUIP] [nvarchar](10) NULL,
	[ALARM_TYPE] [nvarchar](10) NULL,
	[ALARM_DESC] [nvarchar](10) NULL,
	[SAFE_VAULT] [nvarchar](10) NULL,
	[PREMISE_ALARM] [nvarchar](10) NULL,
	[CYL_DOOR_LOCK] [nvarchar](10) NULL,
	[SAFE_VAULT_LBL] [nvarchar](10) NULL,
	[SAFE_VAULT_CLASS] [nvarchar](150) NULL,
	[SAFE_VAULT_MANUFAC] [nvarchar](150) NULL,
	[MAX_CASH_PREM] [decimal](18, 2) NULL,
	[MAX_CASH_MSG] [decimal](18, 2) NULL,
	[MONEY_OVER_NIGHT] [decimal](18, 2) NULL,
	[FREQUENCY_DEPOSIT] [int] NULL,
	[SAFE_DOOR_CONST] [nvarchar](50) NULL,
	[GRADE] [nvarchar](10) NULL,
	[OTH_PROTECTION] [nvarchar](100) NULL,
	[RIGHT_EXP_DESC] [nvarchar](100) NULL,
	[RIGHT_EXP_DIST] [nvarchar](100) NULL,
	[LEFT_EXP_DESC] [nvarchar](100) NULL,
	[LEFT_EXP_DIST] [nvarchar](100) NULL,
	[FRONT_EXP_DESC] [nvarchar](100) NULL,
	[FRONT_EXP_DIST] [nvarchar](100) NULL,
	[REAR_EXP_DESC] [nvarchar](100) NULL,
	[REAR_EXP_DIST] [nvarchar](100) NULL,
	[COUNTY] [nvarchar](150) NULL,
 CONSTRAINT [PK_POL_BOP_PREMISES_LOC_DETAILS] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC,
	[PREMISES_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LOC_DETAILS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LOC_DETAILS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LOC_DETAILS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'DESC_BLDNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'DESC_BLDNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'DESC_BLDNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'DESC_OPERTN'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'DESC_OPERTN'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'DESC_OPERTN'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LST_ALL_OCCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LST_ALL_OCCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LST_ALL_OCCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ANN_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ANN_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ANN_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'TOT_PAYROLL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'TOT_PAYROLL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'TOT_PAYROLL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_NUM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_NUM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_NUM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_GRP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_GRP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_GRP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_TER_NUM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_TER_NUM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RATE_TER_NUM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PROT_CLS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PROT_CLS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PROT_CLS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_ALM_USED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_ALM_USED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_ALM_USED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_RES_SPACE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_RES_SPACE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_RES_SPACE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RES_SPACE_SMK_DET'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RES_SPACE_SMK_DET'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RES_SPACE_SMK_DET'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RES_OCC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RES_OCC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RES_OCC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_HYDRANT_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_HYDRANT_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_HYDRANT_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_STATION_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_STATION_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_STATION_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_DIST_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_DIST_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_DIST_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_DIST_CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_DIST_CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FIRE_DIST_CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BCEGS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BCEGS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BCEGS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CITY_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CITY_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CITY_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SWIMMING_POOL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SWIMMING_POOL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SWIMMING_POOL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PLAY_GROUND'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PLAY_GROUND'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PLAY_GROUND'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BUILD_UNDER_CON'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BUILD_UNDER_CON'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BUILD_UNDER_CON'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BUILD_SHPNG_CENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BUILD_SHPNG_CENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BUILD_SHPNG_CENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BOILER'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BOILER'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'BOILER'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MED_EQUIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MED_EQUIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MED_EQUIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ALARM_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ALARM_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ALARM_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ALARM_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ALARM_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'ALARM_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PREMISE_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PREMISE_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'PREMISE_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CYL_DOOR_LOCK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CYL_DOOR_LOCK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'CYL_DOOR_LOCK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_LBL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_LBL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_LBL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_CLASS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_CLASS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_CLASS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_MANUFAC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_MANUFAC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_VAULT_MANUFAC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MAX_CASH_PREM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MAX_CASH_PREM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MAX_CASH_PREM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MAX_CASH_MSG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MAX_CASH_MSG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MAX_CASH_MSG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MONEY_OVER_NIGHT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MONEY_OVER_NIGHT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'MONEY_OVER_NIGHT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FREQUENCY_DEPOSIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FREQUENCY_DEPOSIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FREQUENCY_DEPOSIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_DOOR_CONST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_DOOR_CONST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'SAFE_DOOR_CONST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'GRADE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'GRADE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'GRADE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'OTH_PROTECTION'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'OTH_PROTECTION'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'OTH_PROTECTION'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RIGHT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RIGHT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RIGHT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RIGHT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RIGHT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'RIGHT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LEFT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LEFT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LEFT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LEFT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LEFT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'LEFT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FRONT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FRONT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FRONT_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FRONT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FRONT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'FRONT_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'REAR_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'REAR_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'REAR_EXP_DESC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'REAR_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'REAR_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'REAR_EXP_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'COUNTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'COUNTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISES_LOC_DETAILS', @level2type=N'COLUMN',@level2name=N'COUNTY'
GO


