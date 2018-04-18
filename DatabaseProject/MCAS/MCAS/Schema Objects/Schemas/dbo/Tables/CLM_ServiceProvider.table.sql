CREATE TABLE [dbo].[CLM_ServiceProvider](
	[ServiceProviderId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimTypeId] [int] NOT NULL,
	[ClaimantNameId] [int] NOT NULL,
	[PartyTypeId] [int] NOT NULL,
	[ServiceProviderTypeId] [int] NOT NULL,
	[CompanyNameId] [int] NOT NULL,
	[AppointedDate] [datetime] NULL,
	[Address1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryId] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PostalCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Reference] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContactPersonName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OfficeNo] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mobile] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPersonName2nd] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress2nd] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OfficeNo2nd] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mobile2nd] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax2nd] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatusId] [int] NOT NULL,
	[Remarks] [nvarchar](800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createdby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Modifieddate] [datetime] NULL,
	[AccidentId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimRecordNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CLM_ServiceProvider] PRIMARY KEY CLUSTERED 
(
	[ServiceProviderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_CLM_ServiceProvider_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
)


