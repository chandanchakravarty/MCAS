CREATE TABLE [dbo].[MNT_DepotMaster](
	[DepotId] [int] IDENTITY(1,1) NOT NULL,
	[DepotCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DepotReference] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address1] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PostalCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TelephoneOff] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PersonInCharge] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[ContactPerson] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OffNo2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WorkShopType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
	[Remarks] [nvarchar](800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompanyName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_DepotMaster] PRIMARY KEY CLUSTERED 
(
	[DepotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


