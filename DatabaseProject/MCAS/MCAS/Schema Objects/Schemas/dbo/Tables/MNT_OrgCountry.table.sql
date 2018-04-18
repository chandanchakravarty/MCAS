CREATE TABLE [dbo].[MNT_OrgCountry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryOrgazinationCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OrganizationName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryInCorporate] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PostalCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPerson] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[Remarks] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress2] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OffNo2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkShopType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
	[FirstContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecondContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InsurerType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_OrgCountry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


