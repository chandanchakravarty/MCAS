USE [EBIX-ADV-INSURER-DEV]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_PREMISES_PROPERTY_INFO]') AND type in (N'U'))
DROP TABLE [dbo].[POL_PREMISES_PROPERTY_INFO]
GO

/****** Object:  Table [dbo].[POL_PREMISES_PROPERTY_INFO]   Script Date: 11/22/2011 18:19:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_PREMISES_PROPERTY_INFO](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NOT NULL,
	[PREMISES_ID] [int] NOT NULL,
	[PROPERTY_ID] [int] NOT NULL,
	[PROP_DEDUCT] [nvarchar](10) NULL,
	[PROP_WNDSTORM] [nvarchar](150) NULL,
	[OPT_CVG] [nvarchar](10) NULL,
	[BLD_LMT] [nvarchar](150) NULL,
	[BLD_PERCENT_COINS] [nvarchar](10) NULL,
	[BLD_VALU] [nvarchar](10) NULL,
	[BLD_INF] [nvarchar](10) NULL,
	[BPP_LMT] [nvarchar](150) NULL,
	[BPP_PERCENT_COINS] [nvarchar](10) NULL,
	[BPP_VALU] [nvarchar](10) NULL,
	[BPP_STOCK] [nvarchar](150) NULL,
	[YEAR_BUILT] [nvarchar](150) NULL,
	[CONST_TYPE] [nvarchar](10) NULL,
	[NUM_STORIES] [nvarchar](10) NULL,
	[PERCENT_SPRINKLERS] [nvarchar](150) NULL,
	[BP_PRESENT] [nvarchar](10) NULL,
	[BP_FNSHD] [nvarchar](10) NULL,
	[BP_OPEN] [nvarchar](10) NULL,
	[BI_WIRNG_YR] [nvarchar](150) NULL,
	[BI_ROOFING_YR] [nvarchar](150) NULL,
	[BI_PLMG_YR] [nvarchar](150) NULL,
	[BI_ROOF_TYP] [nvarchar](10) NULL,
	[BI_HEATNG_YR] [nvarchar](150) NULL,
	[BI_WIND_CLASS] [nvarchar](10) NULL,
 CONSTRAINT [PK_POL_PREMISES_PROPERTY_INFO_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_PROPERTY_ID] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC,
	[PROPERTY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROPERTY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROPERTY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROPERTY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROP_DEDUCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROP_DEDUCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROP_DEDUCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROP_WNDSTORM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROP_WNDSTORM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PROP_WNDSTORM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'OPT_CVG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'OPT_CVG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'OPT_CVG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_PERCENT_COINS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_PERCENT_COINS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_PERCENT_COINS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_VALU'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_VALU'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_VALU'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_INF'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_INF'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BLD_INF'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_LMT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_PERCENT_COINS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_PERCENT_COINS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_PERCENT_COINS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_VALU'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_VALU'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_VALU'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_STOCK'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_STOCK'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BPP_STOCK'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'YEAR_BUILT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'YEAR_BUILT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'YEAR_BUILT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'CONST_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'CONST_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'CONST_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'NUM_STORIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'NUM_STORIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'NUM_STORIES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PERCENT_SPRINKLERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PERCENT_SPRINKLERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'PERCENT_SPRINKLERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_PRESENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_PRESENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_PRESENT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_FNSHD'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_FNSHD'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_FNSHD'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_OPEN'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_OPEN'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BP_OPEN'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_WIRNG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_WIRNG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_WIRNG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_ROOFING_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_ROOFING_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_ROOFING_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_PLMG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_PLMG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_PLMG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_ROOF_TYP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_ROOF_TYP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_ROOF_TYP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_HEATNG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_HEATNG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_HEATNG_YR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_WIND_CLASS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_WIND_CLASS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PREMISES_PROPERTY_INFO', @level2type=N'COLUMN',@level2name=N'BI_WIND_CLASS'
GO

ALTER TABLE [dbo].[POL_PREMISES_PROPERTY_INFO]  WITH CHECK ADD  CONSTRAINT [FK_POL_PREMISES_PROPERTY_INFO_POL_BOP_PREMISES_INFO] FOREIGN KEY([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
REFERENCES [dbo].[POL_BOP_PREMISES_INFO] ([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
GO

ALTER TABLE [dbo].[POL_PREMISES_PROPERTY_INFO] CHECK CONSTRAINT [FK_POL_PREMISES_PROPERTY_INFO_POL_BOP_PREMISES_INFO]
GO

