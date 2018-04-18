IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_POL_SUP_FORM_RESTAURANT_POL_BOP_PREMISES_INFO]') AND parent_object_id = OBJECT_ID(N'[dbo].[POL_SUP_FORM_RESTAURANT]'))
ALTER TABLE [dbo].[POL_SUP_FORM_RESTAURANT] DROP CONSTRAINT [FK_POL_SUP_FORM_RESTAURANT_POL_BOP_PREMISES_INFO]
GO

/****** Object:  Table [dbo].[POL_SUP_FORM_RESTAURANT]    Script Date: 11/24/2011 20:06:16 ******/

/****** Object:  Table [dbo].[POL_SUP_FORM_RESTAURANT]    Script Date: 11/24/2011 20:06:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_SUP_FORM_RESTAURANT]') AND type in (N'U'))
DROP TABLE [dbo].[POL_SUP_FORM_RESTAURANT]
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_SUP_FORM_RESTAURANT](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NOT NULL,
	[PREMISES_ID] [int] NOT NULL,
	[RESTAURANT_ID] [int] NOT NULL,
	[SEATINGCAPACITY] [int] NULL,
	[BUS_TYP_RESTURANT] [bit] NULL,
	[BUS_TYP_FM_STYLE] [bit] NULL,
	[BUS_TYP_NGHT_CLUB] [bit] NULL,
	[BUS_TYP_FRNCHSED] [bit] NULL,
	[BUS_TYP_NT_FRNCHSED] [bit] NULL,
	[BUS_TYP_SEASONAL] [bit] NULL,
	[BUS_TYP_YR_ROUND] [bit] NULL,
	[BUS_TYP_DINNER] [bit] NULL,
	[BUS_TYP_BNQT_HALL] [bit] NULL,
	[BUS_TYP_BREKFAST] [bit] NULL,
	[BUS_TYP_FST_FOOD] [bit] NULL,
	[BUS_TYP_TAVERN] [bit] NULL,
	[BUS_TYP_OTHER] [bit] NULL,
	[STAIRWAYS] [bit] NULL,
	[ELEVATORS] [bit] NULL,
	[ESCALATORS] [bit] NULL,
	[GRILLING] [bit] NULL,
	[FRYING] [bit] NULL,
	[BROILING] [bit] NULL,
	[ROASTING] [bit] NULL,
	[COOKING] [bit] NULL,
	[PRK_TYP_VALET] [bit] NULL,
	[PRK_TYP_PREMISES] [bit] NULL,
	[OPR_ON_PREMISES] [bit] NULL,
	[OPR_OFF_PREMISES] [bit] NULL,
	[EMRG_LIGHTS] [bit] NULL,
	[WOOD_STOVE] [bit] NULL,
	[HIST_MARKER] [bit] NULL,
	[EXTNG_SYS_COV_COOKNG] [bit] NULL,
	[EXTNG_SYS_MNT_CNTRCT] [bit] NULL,
	[GAS_OFF_COOKNG] [bit] NULL,
	[HOOD_FILTER_CLND] [bit] NULL,
	[HOOD_DUCTS_EQUIP] [bit] NULL,
	[HOOD_DUCTS_MNT_SCH] [bit] NULL,
	[BC_EXTNG_AVL] [bit] NULL,
	[ADQT_CLEARANCE] [bit] NULL,
	[BEER_SALES] [bit] NULL,
	[WINE_SALES] [bit] NULL,
	[FULL_BAR] [bit] NULL,
	[TOT_EXPNS_FOOD_LIQUOR] [decimal](18, 2) NULL,
	[TOT_EXPNS_OTHERS] [decimal](18, 2) NULL,
	[NET_PROFIT] [decimal](18, 2) NULL,
	[ACCNT_PAYABLE] [decimal](18, 2) NULL,
	[NOTES_PAYABLE] [decimal](18, 2) NULL,
	[BNK_LOANS_PAYABLE] [decimal](18, 2) NULL,
 CONSTRAINT [PK_POL_SUP_FORM_RESTAURANT_CUST_ID_POL_ID_VERSION_ID_LOC_ID_PREMISES_ID_RESTAURANT_ID] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC,
	[PREMISES_ID] ASC,
	[RESTAURANT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PREMISES_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'RESTAURANT_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'RESTAURANT_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'RESTAURANT_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'SEATINGCAPACITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'SEATINGCAPACITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'SEATINGCAPACITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_RESTURANT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_RESTURANT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_RESTURANT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FM_STYLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FM_STYLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FM_STYLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_NGHT_CLUB'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_NGHT_CLUB'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_NGHT_CLUB'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FRNCHSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FRNCHSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FRNCHSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_NT_FRNCHSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_NT_FRNCHSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_NT_FRNCHSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_SEASONAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_SEASONAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_SEASONAL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_YR_ROUND'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_YR_ROUND'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_YR_ROUND'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_DINNER'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_DINNER'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_DINNER'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_BNQT_HALL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_BNQT_HALL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_BNQT_HALL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_BREKFAST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_BREKFAST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_BREKFAST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FST_FOOD'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FST_FOOD'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_FST_FOOD'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_TAVERN'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_TAVERN'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_TAVERN'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_OTHER'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_OTHER'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BUS_TYP_OTHER'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'STAIRWAYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'STAIRWAYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'STAIRWAYS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ELEVATORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ELEVATORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ELEVATORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ESCALATORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ESCALATORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ESCALATORS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'GRILLING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'GRILLING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'GRILLING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'FRYING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'FRYING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'FRYING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BROILING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BROILING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BROILING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ROASTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ROASTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ROASTING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'COOKING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'COOKING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'COOKING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PRK_TYP_VALET'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PRK_TYP_VALET'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PRK_TYP_VALET'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PRK_TYP_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PRK_TYP_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'PRK_TYP_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'OPR_ON_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'OPR_ON_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'OPR_ON_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'OPR_OFF_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'OPR_OFF_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'OPR_OFF_PREMISES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EMRG_LIGHTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EMRG_LIGHTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EMRG_LIGHTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'WOOD_STOVE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'WOOD_STOVE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'WOOD_STOVE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HIST_MARKER'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HIST_MARKER'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HIST_MARKER'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EXTNG_SYS_COV_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EXTNG_SYS_COV_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EXTNG_SYS_COV_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EXTNG_SYS_MNT_CNTRCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EXTNG_SYS_MNT_CNTRCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'EXTNG_SYS_MNT_CNTRCT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'GAS_OFF_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'GAS_OFF_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'GAS_OFF_COOKNG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_FILTER_CLND'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_FILTER_CLND'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_FILTER_CLND'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_DUCTS_EQUIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_DUCTS_EQUIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_DUCTS_EQUIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_DUCTS_MNT_SCH'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_DUCTS_MNT_SCH'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'HOOD_DUCTS_MNT_SCH'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BC_EXTNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BC_EXTNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BC_EXTNG_AVL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ADQT_CLEARANCE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ADQT_CLEARANCE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ADQT_CLEARANCE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BEER_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BEER_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BEER_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'WINE_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'WINE_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'WINE_SALES'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'FULL_BAR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'FULL_BAR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'FULL_BAR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'TOT_EXPNS_FOOD_LIQUOR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'TOT_EXPNS_FOOD_LIQUOR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'TOT_EXPNS_FOOD_LIQUOR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'TOT_EXPNS_OTHERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'TOT_EXPNS_OTHERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'TOT_EXPNS_OTHERS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'NET_PROFIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'NET_PROFIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'NET_PROFIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ACCNT_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ACCNT_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'ACCNT_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'NOTES_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'NOTES_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'NOTES_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BNK_LOANS_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BNK_LOANS_PAYABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_SUP_FORM_RESTAURANT', @level2type=N'COLUMN',@level2name=N'BNK_LOANS_PAYABLE'
GO

ALTER TABLE [dbo].[POL_SUP_FORM_RESTAURANT]  WITH CHECK ADD  CONSTRAINT [FK_POL_SUP_FORM_RESTAURANT_POL_BOP_PREMISES_INFO] FOREIGN KEY([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
REFERENCES [dbo].[POL_BOP_PREMISES_INFO] ([CUSTOMER_ID], [POLICY_ID], [POLICY_VERSION_ID], [LOCATION_ID], [PREMISES_ID])
GO

ALTER TABLE [dbo].[POL_SUP_FORM_RESTAURANT] CHECK CONSTRAINT [FK_POL_SUP_FORM_RESTAURANT_POL_BOP_PREMISES_INFO]
GO


