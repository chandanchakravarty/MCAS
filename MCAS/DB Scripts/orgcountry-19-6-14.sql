
/****** Object:  Table [dbo].[MNT_OrgCountry]    Script Date: 06/19/2014 10:19:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_OrgCountry]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_OrgCountry]
GO


/****** Object:  Table [dbo].[MNT_OrgCountry]    Script Date: 06/19/2014 10:19:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_OrgCountry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryOrgazinationCode] [nvarchar](4) NOT NULL,
	[OrganizationName] [nvarchar](100) NULL,
	[Address1] [nvarchar](200) NOT NULL,
	[Address2] [nvarchar](200) NULL,
	[Address3] [nvarchar](200) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[CountryInCorporate] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[TelNo] [nvarchar](20) NULL,
	[MobileNo] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[Remarks] [nvarchar](300) NULL,
 CONSTRAINT [PK_MNT_OrgCountry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


