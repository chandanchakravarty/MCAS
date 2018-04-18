IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_ServiceProvider_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_ServiceProvider]'))
ALTER TABLE [dbo].[CLM_ServiceProvider] DROP CONSTRAINT [FK_CLM_ServiceProvider_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_ServiceProvider]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_ServiceProvider]
GO

CREATE TABLE [dbo].[CLM_ServiceProvider](
	[ServiceProviderId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimTypeId] [int] NOT NULL,
	[ClaimantNameId] [int] NOT NULL,
	[PartyTypeId] [int] NOT NULL,
	[ServiceProviderTypeId] [int] NOT NULL,
	[CompanyNameId] [int] NOT NULL,
	[AppointedDate] [datetime] NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[CountryId] [nvarchar](5) NOT NULL,
	[PostalCode] [bigint] NOT NULL,
	[Reference] [nvarchar](150) NOT NULL,
	[ContactPersonName] [nvarchar](250) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[OfficeNo] [int] NULL,
	[Mobile] [int] NULL,
	[Fax] [int] NULL,
	[ContactPersonName2nd] [nvarchar](250) NULL,
	[EmailAddress2nd] [nvarchar](100) NULL,
	[OfficeNo2nd] [int] NULL,
	[Mobile2nd] [int] NULL,
	[Fax2nd] [int] NULL,
	[StatusId] [int] NOT NULL,
	[Remarks] [nvarchar](800) NULL,
	[Createdby] [nvarchar](max) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[Modifieddate] [datetime] NULL,
	[AccidentId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[IsActive] [char](1) NOT NULL,
	[ClaimRecordNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_CLM_ServiceProvider] PRIMARY KEY CLUSTERED 
(
	[ServiceProviderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CLM_ServiceProvider]  WITH CHECK ADD  CONSTRAINT [FK_CLM_ServiceProvider_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_ServiceProvider] CHECK CONSTRAINT [FK_CLM_ServiceProvider_ClaimAccidentDetails]
GO


