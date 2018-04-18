IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_AGENCY_IMPORT_NEW]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_AGENCY_IMPORT_NEW]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_AGENCY_IMPORT_NEW](
	[Agency Name] [nvarchar] (255) NULL,
	[Agency Code] [nvarchar] (255) NULL,
	[Numeric Agency Code] [nvarchar] (255) NULL,
	[Combined Agency Code] [nvarchar] (255) NULL,
	[Address 1] [nvarchar] (255) NULL,
	[City] [nvarchar] (255) NULL,
	[State] [nvarchar] (255) NULL,
	[Phone #1] [nvarchar] (255) NULL,
	[Fax] [nvarchar] (255) NULL,
	[Address 2] [nvarchar] (255) NULL,
	[Country] [nvarchar] (255) NULL,
	[Zip] [nvarchar] (255) NULL,
	[Phone #2] [nvarchar] (255) NULL,
	[Speed Dial #] [float] (53) NULL,
	[Address 11] [nvarchar] (255) NULL,
	[City1] [nvarchar] (255) NULL,
	[State1] [nvarchar] (255) NULL,
	[Address 21] [nvarchar] (255) NULL,
	[Country1] [nvarchar] (255) NULL,
	[Zip1] [nvarchar] (255) NULL,
	[Allows Commission EFT] [nvarchar] (255) NULL,
	[Bank Name 1] [nvarchar] (255) NULL,
	[DFI Account Number 1 *] [nvarchar] (255) NULL,
	[Account Type 1] [float] (53) NULL,
	[Bank Branch 1] [nvarchar] (255) NULL,
	[Public Email] [nvarchar] (255) NULL,
	[Bill Type] [float] (53) NULL,
	[Primary Contact] [nvarchar] (255) NULL,
	[Current Contract Date] [datetime] NULL,
	[Termination Date - New Business] [datetime] NULL,
	[SUSEP Number] [nvarchar] (255) NULL,
	[SUSEP Number1] [nvarchar] (255) NULL,
	[Website] [nvarchar] (255) NULL,
	[No of Licenses] [float] (53) NULL,
	[Other Contact] [nvarchar] (255) NULL,
	[Termination Date - Renewal] [datetime] NULL,
	[Notes] [nvarchar] (255) NULL,
	[Broker Currency] [float] (53) NULL,
	[Broker Type] [float] (53) NULL,
	[Broker ID] [nvarchar] (255) NULL,
	[Broker Date of Birth] [nvarchar] (255) NULL,
	[Broker Regional ID] [nvarchar] (255) NULL,
	[Broker Regional ID issuance] [nvarchar] (255) NULL,
	[Broker Regional ID issuance date] [nvarchar] (255) NULL,
	[Marital Status] [nvarchar] (255) NULL,
	[Gender] [nvarchar] (255) NULL,
	[Agency Type] [float] (53) NULL
) ON [PRIMARY]
GO

