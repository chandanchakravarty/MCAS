CREATE TABLE [dbo].[Clm_DriverAddressInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NULL,
	[Name] [nvarchar](250) NULL,
	[DriverType] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Gender] [nvarchar](1) NULL,
	[NRIC_PPNo] [nvarchar](100) NULL,
	[ContactNo] [nvarchar](100) NULL,
	[Country] [varchar](100) NULL,
	[FaxNo] [nvarchar](100) NULL,
	[PostalCode] [nvarchar](25) NULL,
	[Email] [nvarchar](100) NULL,
	[AddressOf] [varchar](2) NULL,
 CONSTRAINT [PK_Clm_DriverAddressInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
