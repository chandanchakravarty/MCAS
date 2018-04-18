CREATE TABLE [dbo].[MNT_Cedant](
	[CedantId] [int] IDENTITY(1,1) NOT NULL,
	[CedantCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CedantName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Province] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelephoneNoOff] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelephoneNoRes] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryIncorporate] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryGroup] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RIContactPerson] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Division] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinContactPerson] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PostalCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreditRating] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RatingIssued] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Remarks] [nvarchar](800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[Address2] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OfficeNo1] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo1] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNo1] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecondContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OfficeNo2] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo2] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNo2] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InsurerType] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
 CONSTRAINT [PK_MNT_Cedant_CedantId] PRIMARY KEY CLUSTERED 
(
	[CedantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Cedant_CedantCode] UNIQUE NONCLUSTERED 
(
	[CedantCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


