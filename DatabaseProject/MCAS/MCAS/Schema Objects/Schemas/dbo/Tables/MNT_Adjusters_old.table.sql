CREATE TABLE [dbo].[MNT_Adjusters_old](
	[AdjusterCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AdjusterName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AdjusterTypeCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address1] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Province] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PostCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelNoOff] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelNoRes] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EMail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VATPer] [decimal](18, 2) NULL,
	[VATNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Classification] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Memo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ConPer] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PvEmail] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WHTPer] [decimal](18, 2) NULL,
	[VAT] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WHT] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdjType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_TM_Adjusters_AdjType]  DEFAULT (N'EXT'),
	[AdjSrc] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Product_Code] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [pk_TM_Adjusters] PRIMARY KEY CLUSTERED 
(
	[AdjusterCode] ASC,
	[AdjusterTypeCode] ASC,
	[Product_Code] ASC,
	[Address1] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


