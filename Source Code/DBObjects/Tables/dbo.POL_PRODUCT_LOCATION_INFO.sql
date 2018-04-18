IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_PRODUCT_LOCATION_INFO]') AND type in (N'U'))
DROP TABLE [dbo].[POL_PRODUCT_LOCATION_INFO]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_PRODUCT_LOCATION_INFO](
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [smallint] NOT NULL,
	[PRODUCT_RISK_ID] [int] NOT NULL,
	[LOCATION] [int] NOT NULL,
	[VALUE_AT_RISK] [decimal] (18,2) NULL,
	[BUILDING_VALUE] [decimal] (18,2) NULL,
	[CONTENTS_VALUE] [decimal] (18,2) NULL,
	[RAW_MATERIAL_VALUE] [decimal] (18,2) NULL,
	[CONTENTS_RAW_VALUES] [decimal] (12,2) NULL,
	[MRI_VALUE] [decimal] (18,2) NULL,
	[MAXIMUM_LIMIT] [decimal] (18,2) NULL,
	[POSSIBLE_MAX_LOSS] [decimal] (12,2) NULL,
	[MULTIPLE_DEDUCTIBLE] [int] NULL,
	[PARKING_SPACES] [nvarchar] (20) NULL,
	[ACTIVITY_TYPE] [int] NULL,
	[OCCUPIED_AS] [int] NULL,
	[CONSTRUCTION] [nvarchar] (50) NULL,
	[RUBRICA] [nvarchar] (6) NULL,
	[ASSIST24] [int] NULL,
	[REMARKS] [nvarchar] (4000) NULL,
	[IS_ACTIVE] [nchar] (1) NOT NULL,
	[CREATED_BY] [int] NOT NULL,
	[CREATED_DATETIME] [datetime] NOT NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[CLAIM_RATIO] [decimal] (12,2) NULL,
	[BONUS] [decimal] (12,2) NULL,
	[CO_APPLICANT_ID] [int] NULL CONSTRAINT [DF_POL_PRODUCT_LOCATION_INFO_CO_APPLICANT_ID] DEFAULT ((0)),
	[CLASS_FIELD] [int] NULL,
	[LOCATION_NUMBER] [int] NULL,
	[ITEM_NUMBER] [int] NULL,
	[ACTUAL_INSURED_OBJECT] [nvarchar] (250) NULL,
	[ORIGINAL_VERSION_ID] [int] NULL,
	[PORTABLE_EQUIPMENT] [int] NULL,
	[CO_RISK_ID] [int] NULL
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'max + 1 against Policy ID, Customer iD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'PRODUCT_RISK_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Use to set the location id against Policy id ,Customer id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'LOCATION'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'VALUE_AT_RISK'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'BUILDING_VALUE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'CONTENTS_VALUE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'RAW_MATERIAL_VALUE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'CONTENTS_RAW_VALUES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'MRI_VALUE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'MAXIMUM_LIMIT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'POSSIBLE_MAX_LOSS'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'MULTIPLE_DEDUCTIBLE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'PARKING_SPACES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'ACTIVITY_TYPE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'OCCUPIED_AS'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'CONSTRUCTION'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains value against Risk Location information' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'RUBRICA'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the value against 26 hr Assistance' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'ASSIST24'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains any additional information ,which is 500 characters' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'REMARKS'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'The Value of Is Active is Y or N' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'IS_ACTIVE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Store User id, who is created' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Store Date and Time at the creation of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Store User id at the Updatation of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Store Date and Time at the Updation of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_PRODUCT_LOCATION_INFO', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

ALTER TABLE [dbo].[POL_PRODUCT_LOCATION_INFO] ADD CONSTRAINT [PK_POL_PRODUCT_LOCATION_INFO_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_PRODUCT_RISK_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[PRODUCT_RISK_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_PRODUCT_LOCATION_INFO] WITH NOCHECK ADD CONSTRAINT [FK_POL_PRODUCT_LOCATION_INFO_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] FOREIGN KEY
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

