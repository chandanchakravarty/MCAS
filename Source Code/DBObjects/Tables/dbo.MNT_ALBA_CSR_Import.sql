IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_ALBA_CSR_Import]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_ALBA_CSR_Import]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_ALBA_CSR_Import](
	[IsUser] [nvarchar] (255) NULL,
	[CSR ID] [float] (53) NULL,
	[User Status] [nvarchar] (255) NULL,
	[First Name] [nvarchar] (255) NULL,
	[Last Name] [nvarchar] (255) NULL,
	[User Title] [nvarchar] (255) NULL,
	[Date of Birth] [float] (53) NULL,
	[Pink Slip Notification] [nvarchar] (255) NULL,
	[Address1] [nvarchar] (255) NULL,
	[City] [nvarchar] (255) NULL,
	[State] [nvarchar] (255) NULL,
	[User Initials] [nvarchar] (255) NULL,
	[Extn] [nvarchar] (255) NULL,
	[Mobile] [nvarchar] (255) NULL,
	[Brics User] [nvarchar] (255) NULL,
	[Address2] [nvarchar] (255) NULL,
	[Country] [nvarchar] (255) NULL,
	[Zip] [nvarchar] (255) NULL,
	[Phone] [nvarchar] (255) NULL,
	[Fax] [nvarchar] (255) NULL,
	[Email] [nvarchar] (255) NULL,
	[Login ID] [nvarchar] (255) NULL,
	[Password] [nvarchar] (255) NULL,
	[Confirm Password] [nvarchar] (255) NULL,
	[Change Password on Next Logon] [nvarchar] (255) NULL,
	[Supervisor Equivalent] [nvarchar] (255) NULL,
	[Manager] [nvarchar] (255) NULL,
	[User Account Locked] [nvarchar] (255) NULL,
	[User Type] [float] (53) NULL,
	[Default Hierarchy] [float] (53) NULL,
	[Time Zone] [nvarchar] (255) NULL,
	[Notes] [nvarchar] (255) NULL,
	[Product] [nvarchar] (255) NULL,
	[Brokers] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

