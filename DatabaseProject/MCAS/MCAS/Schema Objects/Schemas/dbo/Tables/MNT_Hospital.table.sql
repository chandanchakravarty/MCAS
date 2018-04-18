CREATE TABLE [dbo].[MNT_Hospital](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HospitalName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HospitalAddress] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HospitalContactNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HospitalFaxNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPersonName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[officeNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[HospitalAddress2] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HospitalAddress3] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PostalCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo1] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecondContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OffNo2] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MobileNo2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HospitalType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
	[Remarks] [nvarchar](800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Hospital] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


