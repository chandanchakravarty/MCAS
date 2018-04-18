CREATE TABLE [dbo].[MNT_Adjusters](
	[AdjusterId] [int] IDENTITY(1,1) NOT NULL,
	[AdjusterCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AdjusterName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AdjusterTypeCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address1] [nvarchar](210) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [nvarchar](210) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](210) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Province] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
	[AdjType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdjSrc] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress2] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OffNo2] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InsurerType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
	[Remarks] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Adjusters_AdjusterId] PRIMARY KEY CLUSTERED 
(
	[AdjusterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Adjusters_AdjusterCode] UNIQUE NONCLUSTERED 
(
	[AdjusterCode] ASC,
	[AdjusterTypeCode] ASC,
	[ProductCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


