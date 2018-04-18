IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_POL_SUP_FORM_SHOP_POL_BOP_PREMISES_INFO]') AND parent_object_id = OBJECT_ID(N'[dbo].[POL_SUP_FORM_SHOP]'))
ALTER TABLE [dbo].[POL_SUP_FORM_SHOP] DROP CONSTRAINT [FK_POL_SUP_FORM_SHOP_POL_BOP_PREMISES_INFO]
GO


GO

/****** Object:  Table [dbo].[POL_SUP_FORM_SHOP]    Script Date: 11/24/2011 20:12:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_SUP_FORM_SHOP]') AND type in (N'U'))
DROP TABLE [dbo].[POL_SUP_FORM_SHOP]
GO


GO

/****** Object:  Table [dbo].[POL_SUP_FORM_SHOP]    Script Date: 11/24/2011 20:12:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_SUP_FORM_SHOP](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NOT NULL,
	[PREMISES_ID] [int] NOT NULL,
	[SHOP_ID] [int] NOT NULL,
	[UNITS] [int] NULL,
	[PERCENT_OCUP] [real] NULL,
	[RESTURANT_OCUP] [nvarchar](10) NULL,
	[FLAME_COOKING] [nvarchar](10) NULL,
	[NUM_FRYERS] [nvarchar](10) NULL,
	[NUM_GRILLS] [nvarchar](10) NULL,
	[DUCT_SYS] [nvarchar](10) NULL,
	[SUPPR_SYS] [nvarchar](10) NULL,
	[DUCT_CLND_PST_SIX_MONTHS] [nvarchar](10) NULL,
	[IS_INSURED] [nvarchar](10) NULL,
	[TENANT_LIABILITY] [nvarchar](10) NULL,
	[PERCENT_SALES] [decimal](18, 2) NULL,
	[SEPARATE_BAR] [nvarchar](10) NULL,
	[BBQ_PIT] [nvarchar](10) NULL,
	[BBQ_PIT_DIST] [real] NULL,
	[BLDG_TYPE_COOKNG] [nvarchar](10) NULL,
	[IS_ENTERTNMT] [nvarchar](10) NULL,
 CONSTRAINT [PK_POL_SUP_FORM_SHOP_CUST_ID_POL_ID_VERSION_ID_LOC_ID_PREMISES_ID_SHOP_ID] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC,
	[PREMISES_ID] ASC,
	[SHOP_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SHOP_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SHOP_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SHOP_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'UNITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'UNITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'UNITS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PERCENT_OCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PERCENT_OCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PERCENT_OCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'RESTURANT_OCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'RESTURANT_OCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'RESTURANT_OCUP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'FLAME_COOKING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'FLAME_COOKING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'FLAME_COOKING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'NUM_FRYERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'NUM_FRYERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'NUM_FRYERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'NUM_GRILLS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'NUM_GRILLS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'NUM_GRILLS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'DUCT_SYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'DUCT_SYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'DUCT_SYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SUPPR_SYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SUPPR_SYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SUPPR_SYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'DUCT_CLND_PST_SIX_MONTHS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'DUCT_CLND_PST_SIX_MONTHS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'DUCT_CLND_PST_SIX_MONTHS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'IS_INSURED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'IS_INSURED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'IS_INSURED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'TENANT_LIABILITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'TENANT_LIABILITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'TENANT_LIABILITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PERCENT_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PERCENT_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'PERCENT_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SEPARATE_BAR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SEPARATE_BAR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'SEPARATE_BAR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BBQ_PIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BBQ_PIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BBQ_PIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BBQ_PIT_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BBQ_PIT_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BBQ_PIT_DIST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BLDG_TYPE_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BLDG_TYPE_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_SHOP', @level2type=N'COLUMN',@level2name=N'BLDG_TYPE_COOKNG'
GO

ALTER TABLE [dbo].[POL_SUP_FORM_SHOP]  WITH CHECK ADD  CONSTRAINT [FK_POL_SUP_FORM_SHOP_POL_BOP_PREMISES_INFO] FOREIGN KEY([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
REFERENCES [dbo].[POL_BOP_PREMISES_INFO] ([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
GO

ALTER TABLE [dbo].[POL_SUP_FORM_SHOP] CHECK CONSTRAINT [FK_POL_SUP_FORM_SHOP_POL_BOP_PREMISES_INFO]
GO


