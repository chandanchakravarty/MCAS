CREATE TABLE [dbo].[MNT_ProductClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProdCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClassCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClassDesc] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClassOrder] [int] NULL,
	[PolicyDescription] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsRenewable] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LedgerPosting] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GST] [int] NULL,
	[GLCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GLDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClassDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UPR] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShrtCal] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MinPremium] [decimal](18, 2) NULL,
	[IsAccumulated] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccumTemp] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClsTemp] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LloydCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RiskTypeCode] [int] NULL,
	[Status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Remarks] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusinessType] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BinderRefNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BindYear] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[CountryType] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_ProductClass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


