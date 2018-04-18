CREATE TABLE [dbo].[MNT_Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProductDisplayName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductEffectiveDate] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductRenewable] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductUPRMethod] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCalMethod] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductPremiumRate] [decimal](18, 2) NULL,
	[ProductSDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductLDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OnClickExecuteFile] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductLongText] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductPropTable] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductPropHisTable] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductStatus] [int] NULL CONSTRAINT [DF_TM_Products_Product_Status]  DEFAULT ((0)),
	[ProductTableName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PremiumLevel] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LedgerCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DispProductCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Accumulation] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductUPRReguMethod] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductType] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[TemplateCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Products_ProductId] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Products_ProductCode] UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


