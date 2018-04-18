USE [EBIX-ADV-INSURER-DEV]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_SUP_FORM_WAREHOUSE]') AND type in (N'U'))
DROP TABLE [dbo].[POL_SUP_FORM_WAREHOUSE]
GO

/****** Object:  Table [dbo].[POL_SUP_FORM_WAREHOUSE]    Script Date: 11/23/2011 18:02:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_SUP_FORM_WAREHOUSE](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NOT NULL,
	[PREMISES_ID] [int] NOT NULL,
	[WAREHOUSE_ID] [int] NOT NULL,
	[BUILDINGS] [int] NULL,
	[OWN_MGMR] [nvarchar](10) NULL,
	[RES_MGMR] [nvarchar](10) NULL,
	[DAYTIME_ATTNDT] [nvarchar](10) NULL,
	[ANY_BUSS_ACTY] [nvarchar](10) NULL,
	[VLT_STYLE] [nvarchar](10) NULL,
	[TRUCK_RENTAL] [nvarchar](10) NULL,
	[MGMR_KYS_CUST_UNIT] [nvarchar](10) NULL,
	[NOTICE_SENT] [nvarchar](10) NULL,
	[SALES_TENANT_LST_TWELVE_MNTHS] [int]  NULL,
	[ANY_COLD_STORAGE] [nvarchar](10) NULL,
	[MGMR_TYPE] [nvarchar](10) NULL,
	[STORAGEUNITS] [int] NULL,
	[IS_FENCED] [nvarchar](10) NULL,
	[IS_PRKNG_AVL] [nvarchar](10) NULL,
	[IS_BOAT_PRKNG_AVL] [nvarchar](10) NULL,
	[ANY_FIREARMS] [nvarchar](10) NULL,
	[TENANT_LCKS_CHK] [nvarchar](10) NULL,
	[ANY_BUSN_GUIDELINES] [nvarchar](10) NULL,
	[NO_DYS_TENANT_PROP_SOLD] [int] NULL,
	[DISP_PUBL] [nvarchar](10) NULL,
	[ANY_CLIMATE_CNTL] [nvarchar](10) NULL,
	[GUARD_DOG] [nvarchar](10) NULL
 CONSTRAINT [PK_POL_SUP_FORM_WAREHOUSE_CUST_ID_POL_ID_VERSION_ID_LOC_ID_PREMISES_ID_WAREHOUSE_ID] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC,
	[PREMISES_ID] ASC,
	[WAREHOUSE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'WAREHOUSE_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'WAREHOUSE_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'WAREHOUSE_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'BUILDINGS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'BUILDINGS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'BUILDINGS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'OWN_MGMR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'OWN_MGMR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'OWN_MGMR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'RES_MGMR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'RES_MGMR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'RES_MGMR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'DAYTIME_ATTNDT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'DAYTIME_ATTNDT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'DAYTIME_ATTNDT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_BUSS_ACTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_BUSS_ACTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_BUSS_ACTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'VLT_STYLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'VLT_STYLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'VLT_STYLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'TRUCK_RENTAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'TRUCK_RENTAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'TRUCK_RENTAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'MGMR_KYS_CUST_UNIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'MGMR_KYS_CUST_UNIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'MGMR_KYS_CUST_UNIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'NOTICE_SENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'NOTICE_SENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'NOTICE_SENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'SALES_TENANT_LST_TWELVE_MNTHS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'SALES_TENANT_LST_TWELVE_MNTHS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'SALES_TENANT_LST_TWELVE_MNTHS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_COLD_STORAGE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_COLD_STORAGE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_COLD_STORAGE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'MGMR_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'MGMR_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'MGMR_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'STORAGEUNITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'STORAGEUNITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'STORAGEUNITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_FENCED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_FENCED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_FENCED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_PRKNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_PRKNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_PRKNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_BOAT_PRKNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_BOAT_PRKNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'IS_BOAT_PRKNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_FIREARMS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_FIREARMS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_FIREARMS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'TENANT_LCKS_CHK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'TENANT_LCKS_CHK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'TENANT_LCKS_CHK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_BUSN_GUIDELINES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_BUSN_GUIDELINES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_BUSN_GUIDELINES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'NO_DYS_TENANT_PROP_SOLD'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'NO_DYS_TENANT_PROP_SOLD'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'NO_DYS_TENANT_PROP_SOLD'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'DISP_PUBL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'DISP_PUBL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'DISP_PUBL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_CLIMATE_CNTL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_CLIMATE_CNTL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'ANY_CLIMATE_CNTL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'GUARD_DOG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_WAREHOUSE', @level2type=N'COLUMN',@level2name=N'GUARD_DOG'
GO

ALTER TABLE [dbo].[POL_SUP_FORM_WAREHOUSE]  WITH CHECK ADD  CONSTRAINT [FK_POL_SUP_FORM_WAREHOUSE_POL_BOP_PREMISES_INFO] FOREIGN KEY([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
REFERENCES [dbo].[POL_BOP_PREMISES_INFO] ([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
GO

ALTER TABLE [dbo].[POL_SUP_FORM_WAREHOUSE] CHECK CONSTRAINT [FK_POL_SUP_FORM_WAREHOUSE_POL_BOP_PREMISES_INFO]
GO

