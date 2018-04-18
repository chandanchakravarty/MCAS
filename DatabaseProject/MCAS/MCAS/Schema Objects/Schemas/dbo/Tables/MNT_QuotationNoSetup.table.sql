CREATE TABLE [dbo].[MNT_QuotationNoSetup](
	[TypeCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[QuotationCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GenerateType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalLength] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[Type] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KeyInValue] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_TM_QuotationNoSetup_1] PRIMARY KEY CLUSTERED 
(
	[TypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


