/****** Object:  Table [dbo].[POL_BOP_PREMISESLOCATIONS]    Script Date: 11/24/2011 14:01:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_BOP_PREMISESLOCATIONS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_BOP_PREMISESLOCATIONS]
GO


/****** Object:  Table [dbo].[POL_BOP_PREMISESLOCATIONS]    Script Date: 11/24/2011 14:00:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[POL_BOP_PREMISESLOCATIONS](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[LOCATION_ID] [smallint] NOT NULL,
	[BUILDING] [int] NULL,
	[PREMLOC_ID] [int] NULL,
	[STREET_ADDR] [nvarchar](150) NULL,
	[CITY] [nvarchar](150) NULL,
	[STATE] [nvarchar](10) NULL,
	[COUNTY] [nvarchar](150) NULL,
	[ZIP] [nvarchar](150) NULL,
	[INTEREST] [nvarchar](10) NULL,
	[FL_TM_EMP] [nvarchar](150) NULL,
	[PT_TM_EMP] [nvarchar](150) NULL,
	[ANN_REVENUE] [decimal](18, 2) NULL,
	[OCC_AREA] [decimal](18, 2) NULL,
	[OPEN_AREA] [decimal](18, 2) NULL,
	[TOT_AREA] [decimal](18, 2) NULL,
	[AREA_LEASED] [nvarchar](10) NULL,
 CONSTRAINT [PK_BOP_PREMISESLOCATIONS] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[LOCATION_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'LOCATION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'BUILDING'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'BUILDING'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'BUILDING'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'PREMLOC_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'PREMLOC_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'PREMLOC_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'STREET_ADDR'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'STREET_ADDR'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'STREET_ADDR'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'CITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'CITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'CITY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'STATE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'STATE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'STATE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'COUNTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'COUNTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'COUNTY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'ZIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'ZIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'ZIP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'INTEREST'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'INTEREST'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'INTEREST'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'FL_TM_EMP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'FL_TM_EMP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'FL_TM_EMP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'PT_TM_EMP'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'PT_TM_EMP'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'PT_TM_EMP'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'ANN_REVENUE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'ANN_REVENUE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'ANN_REVENUE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'OCC_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'OCC_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'OCC_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'OPEN_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'OPEN_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'OPEN_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'TOT_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'TOT_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'TOT_AREA'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'AREA_LEASED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'AREA_LEASED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_BOP_PREMISESLOCATIONS', @level2type=N'COLUMN',@level2name=N'AREA_LEASED'
GO


