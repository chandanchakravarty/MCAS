USE [EBIX-ADV-INSURER-DEV]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_SUP_FORM_CONTRACTOR]') AND type in (N'U'))
DROP TABLE [dbo].[POL_SUP_FORM_CONTRACTOR]
GO

/****** Object:  Table [dbo].[POL_SUP_FORM_CONTRACTOR]    Script Date: 11/24/2011 15:29:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_SUP_FORM_CONTRACTOR](
	[CUSTOMER_ID] [int]				NOT NULL,
	[POLICY_ID] [int]				NOT NULL,
	[POLICY_VERSION_ID] [smallint]  NOT NULL,
	[LOCATION_ID] [smallint]		NOT NULL,
	[PREMISES_ID] [int]				NOT NULL,
	[CONTRACTOR_ID] [int]			NOT NULL,
	[TYP_CONTRACTOR] [nvarchar](100) NULL,
	[YR_EXP] [nvarchar](100)		  NULL,
	[CONT_LICENSE] [nvarchar](100) NULL,
	[LICENSE_HOLDER] [nvarchar](10) NULL,
	[LMT_CONTRACTOR_OCC] [nvarchar](10) NULL,
	[LMT_CONTRACTOR_AGG] [nvarchar](10) NULL,
	[TOT_CST_PST_YR] [decimal](18, 2) NULL,
	[IS_EXPL_ENVRNT] [nvarchar](10) NULL,
	[IS_FIRE_ALARM] [nvarchar](10) NULL,
	[IS_HOSPITALS] [nvarchar](10) NULL,
	[IS_EXP_ENVRNT] [nvarchar](10) NULL,
	[IS_SWIMMING_POOL] [nvarchar](10) NULL,
	[IS_BRG_ALARM] [nvarchar](10) NULL,
	[IS_PWR_PLANTS] [nvarchar](10) NULL,
	[IS_BCK_EQUIPMNT] [nvarchar](10) NULL,
	[IS_LIVE_WIRES] [nvarchar](10) NULL,
	[IS_ARPT_CONSTRCT] [nvarchar](10) NULL,
	[IS_HIGH_VOLTAGE] [nvarchar](10) NULL,
	[IS_TRAFFIC_WRK] [nvarchar](10) NULL,
	[IS_LND_FILL] [nvarchar](10) NULL,
	[IS_DAM_CONSTRNT] [nvarchar](10) NULL,
	[IS_REFINERY] [nvarchar](10) NULL,
	[IS_HZD_MATERIAL] [nvarchar](10) NULL,
	[IS_PETRO_PLNT] [nvarchar](10) NULL,
	[IS_NUCL_PLNT] [nvarchar](10) NULL,
	[IS_PWR_LINES] [nvarchar](10) NULL,
	[DRW_PLANS] [nvarchar](10) NULL,
	[OPR_BLASTING] [nvarchar](10) NULL,
	[OPR_TRENCHING] [nvarchar](10) NULL,
	[OPR_EXCAVACATION] [nvarchar](10) NULL,
	[IS_SECTY_POLICY] [nvarchar](10) NULL,
	[ANY_DEMOLITION] [nvarchar](10) NULL,
	[ANY_CRANES] [nvarchar](10) NULL,
	[PERCENT_ROOFING] [nvarchar](150) NULL,
	[ANY_LESS_LMTS] [nvarchar](10) NULL,
	[ANY_SHOP_WRK] [nvarchar](10) NULL,
	[PERCENT_RENOVATION] [nvarchar](150) NULL,
	[ANY_GUTTING] [nvarchar](10) NULL,
	[PERCENT_SNOWPLOWING] [nvarchar](150) NULL,
	[ANY_WRK_LOAD] [nvarchar](10) NULL,
	[PERCENT_PNTG_OUTSIDE] [nvarchar](150) NULL,
	[PERCENT_PNTG_INSIDE] [nvarchar](150) NULL,
	[ANY_PNTG_TNKS] [nvarchar](10) NULL,
	[PERCENT_PNTG_SPRY] [nvarchar](150) NULL,
	[ANY_EPOXIES] [nvarchar](10) NULL,
	[ANY_ACID] [nvarchar](10) NULL,
	[ANY_LEASE_EQUIPMNT] [nvarchar](10) NULL,
	[ANY_BOATS_OWND] [nvarchar](10) NULL,
	[DRCT_SIGHT_WRK_IN_PRGRSS] [nvarchar](10) NULL,
	[PRDCT_SOLD_IN_APPL_NAME] [nvarchar](10) NULL,
	[UTILITY_CMPNY_CALLED] [nvarchar](10) NULL,
	[TYP_IN_DGGN_PRCSS] [nvarchar](150) NULL,
	[PERMIT_OBTAINED] [nvarchar](10) NULL,
	[PERCENT_SPRINKLE_WRK] [nvarchar](150) NULL,
	[ANY_EXCAVAION] [nvarchar](10) NULL,
	[ANY_PEST_SPRAY] [nvarchar](10) NULL,
	[PERCENT_TREE_TRIMNG] [nvarchar](150) NULL,
	[ANY_WRK_OFFSEASON] [nvarchar](10) NULL,
	[ANY_MIX_TRANSIT] [nvarchar](10) NULL,
	[ANY_CONTSRUCTION_WRK] [nvarchar](10) NULL,
	[ANY_WRK_ABVE_THREE_STORIES] [nvarchar](10) NULL,
	[ANY_SCAFHOLDING_ABVE_TWELVE_FEET] [nvarchar](10) NULL,
	[ANY_PNTG_TOWERS] [nvarchar](10) NULL,
	[ANY_SPRAY_GUNS] [nvarchar](10) NULL,
	[ANY_REMOVAL_DONE] [nvarchar](10) NULL,
	[ANY_WAXING_FLOORS] [nvarchar](10) NULL,
	[PER_RESIDENT] [nvarchar](10) NULL,
	[PER_COMMERICAL] [nvarchar](10) NULL,
	[PER_CONST] [nvarchar](10) NULL,
	[PER_REMODEL] [nvarchar](10) NULL,
	[MAJOR_ELECT] [nvarchar](10) NULL,
	[CARRY_LIMITS] [nvarchar](10) NULL

 CONSTRAINT [POL_SUP_FORM_CONTRACTOR_CUST_ID_POL_ID_VERSION_ID_LOC_ID_PREMISES_ID_CONTRACTOR_ID] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC,
	[PREMISES_ID] ASC,
	[CONTRACTOR_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CONTRACTOR_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CONTRACTOR_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CONTRACTOR_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TYP_CONTRACTOR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TYP_CONTRACTOR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TYP_CONTRACTOR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'YR_EXP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'YR_EXP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'YR_EXP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CONT_LICENSE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CONT_LICENSE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CONT_LICENSE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LICENSE_HOLDER'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LICENSE_HOLDER'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LICENSE_HOLDER'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LMT_CONTRACTOR_OCC'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LMT_CONTRACTOR_OCC'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LMT_CONTRACTOR_OCC'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LMT_CONTRACTOR_AGG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LMT_CONTRACTOR_AGG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'LMT_CONTRACTOR_AGG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TOT_CST_PST_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TOT_CST_PST_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TOT_CST_PST_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_EXPL_ENVRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_EXPL_ENVRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_EXPL_ENVRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_FIRE_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_FIRE_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_FIRE_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HOSPITALS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HOSPITALS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HOSPITALS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_EXP_ENVRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_EXP_ENVRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_EXP_ENVRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_SWIMMING_POOL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_SWIMMING_POOL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_SWIMMING_POOL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_BRG_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_BRG_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_BRG_ALARM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PWR_PLANTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PWR_PLANTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PWR_PLANTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_BCK_EQUIPMNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_BCK_EQUIPMNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_BCK_EQUIPMNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_LIVE_WIRES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_LIVE_WIRES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_LIVE_WIRES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_ARPT_CONSTRCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_ARPT_CONSTRCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_ARPT_CONSTRCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HIGH_VOLTAGE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HIGH_VOLTAGE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HIGH_VOLTAGE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_TRAFFIC_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_TRAFFIC_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_TRAFFIC_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_LND_FILL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_LND_FILL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_LND_FILL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_DAM_CONSTRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_DAM_CONSTRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_DAM_CONSTRNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_REFINERY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_REFINERY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_REFINERY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HZD_MATERIAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HZD_MATERIAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_HZD_MATERIAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PETRO_PLNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PETRO_PLNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PETRO_PLNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_NUCL_PLNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_NUCL_PLNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_NUCL_PLNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PWR_LINES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PWR_LINES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_PWR_LINES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'DRW_PLANS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'DRW_PLANS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'DRW_PLANS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_BLASTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_BLASTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_BLASTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_TRENCHING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_TRENCHING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_TRENCHING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_EXCAVACATION'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_EXCAVACATION'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'OPR_EXCAVACATION'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_SECTY_POLICY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_SECTY_POLICY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'IS_SECTY_POLICY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_DEMOLITION'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_DEMOLITION'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_DEMOLITION'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_CRANES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_CRANES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_CRANES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_ROOFING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_ROOFING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_ROOFING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_LESS_LMTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_LESS_LMTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_LESS_LMTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SHOP_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SHOP_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SHOP_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_RENOVATION'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_RENOVATION'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_RENOVATION'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_GUTTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_GUTTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_GUTTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_SNOWPLOWING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_SNOWPLOWING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_SNOWPLOWING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_LOAD'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_LOAD'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_LOAD'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_OUTSIDE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_OUTSIDE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_OUTSIDE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_INSIDE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_INSIDE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_INSIDE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PNTG_TNKS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PNTG_TNKS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PNTG_TNKS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_SPRY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_SPRY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_PNTG_SPRY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_EPOXIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_EPOXIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_EPOXIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_ACID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_ACID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_ACID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_LEASE_EQUIPMNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_LEASE_EQUIPMNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_LEASE_EQUIPMNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_BOATS_OWND'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_BOATS_OWND'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_BOATS_OWND'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'DRCT_SIGHT_WRK_IN_PRGRSS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'DRCT_SIGHT_WRK_IN_PRGRSS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'DRCT_SIGHT_WRK_IN_PRGRSS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PRDCT_SOLD_IN_APPL_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PRDCT_SOLD_IN_APPL_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PRDCT_SOLD_IN_APPL_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'UTILITY_CMPNY_CALLED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'UTILITY_CMPNY_CALLED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'UTILITY_CMPNY_CALLED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TYP_IN_DGGN_PRCSS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TYP_IN_DGGN_PRCSS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'TYP_IN_DGGN_PRCSS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERMIT_OBTAINED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERMIT_OBTAINED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERMIT_OBTAINED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_SPRINKLE_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_SPRINKLE_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_SPRINKLE_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_EXCAVAION'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_EXCAVAION'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_EXCAVAION'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PEST_SPRAY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PEST_SPRAY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PEST_SPRAY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_TREE_TRIMNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_TREE_TRIMNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PERCENT_TREE_TRIMNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_OFFSEASON'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_OFFSEASON'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_OFFSEASON'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_MIX_TRANSIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_MIX_TRANSIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_MIX_TRANSIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_CONTSRUCTION_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_CONTSRUCTION_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_CONTSRUCTION_WRK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_ABVE_THREE_STORIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_ABVE_THREE_STORIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WRK_ABVE_THREE_STORIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SCAFHOLDING_ABVE_TWELVE_FEET'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SCAFHOLDING_ABVE_TWELVE_FEET'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SCAFHOLDING_ABVE_TWELVE_FEET'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PNTG_TOWERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PNTG_TOWERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_PNTG_TOWERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SPRAY_GUNS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SPRAY_GUNS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_SPRAY_GUNS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_REMOVAL_DONE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_REMOVAL_DONE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_REMOVAL_DONE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WAXING_FLOORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WAXING_FLOORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'ANY_WAXING_FLOORS'
GO


EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_RESIDENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_RESIDENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_RESIDENT'
GO



EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_COMMERICAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_COMMERICAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_COMMERICAL'
GO



EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_CONST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_CONST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_CONST'
GO



EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_REMODEL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_REMODEL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'PER_REMODEL'
GO




EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'MAJOR_ELECT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'MAJOR_ELECT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'MAJOR_ELECT'
GO




EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CARRY_LIMITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CARRY_LIMITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_CONTRACTOR', @level2type=N'COLUMN',@level2name=N'CARRY_LIMITS'
GO

ALTER TABLE [dbo].[POL_SUP_FORM_CONTRACTOR]  WITH CHECK ADD  CONSTRAINT [FK_POL_SUP_FORM_CONTRACTOR_POL_BOP_PREMISES_INFO] FOREIGN KEY([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
REFERENCES [dbo].[POL_BOP_PREMISES_INFO] ([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
GO

ALTER TABLE [dbo].[POL_SUP_FORM_CONTRACTOR] CHECK CONSTRAINT [FK_POL_SUP_FORM_CONTRACTOR_POL_BOP_PREMISES_INFO]
GO

