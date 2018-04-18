IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_CARRIER_IMPORT]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_CARRIER_IMPORT]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_CARRIER_IMPORT](
	[Carrier Name] [nvarchar] (255) NULL,
	[Carrier Type] [float] (53) NULL,
	[Carrier Code] [float] (53) NULL,
	[Address 1] [nvarchar] (255) NULL,
	[City] [nvarchar] (255) NULL,
	[State] [nvarchar] (255) NULL,
	[Phone 1] [nvarchar] (255) NULL,
	[Fax] [nvarchar] (255) NULL,
	[Address 2] [nvarchar] (255) NULL,
	[Country] [nvarchar] (255) NULL,
	[Zip] [nvarchar] (255) NULL,
	[Phone 2] [nvarchar] (255) NULL,
	[Speed Dial] [nvarchar] (255) NULL,
	[Address 1 (Mailing Address)] [nvarchar] (255) NULL,
	[City1] [nvarchar] (255) NULL,
	[State1] [nvarchar] (255) NULL,
	[Address 21] [nvarchar] (255) NULL,
	[Country1] [nvarchar] (255) NULL,
	[Zip1] [nvarchar] (255) NULL,
	[Phone 11] [nvarchar] (255) NULL,
	[Fax1] [nvarchar] (255) NULL,
	[Phone 21] [nvarchar] (255) NULL,
	[Mobile] [nvarchar] (255) NULL,
	[Website] [nvarchar] (255) NULL,
	[Principal Contact] [nvarchar] (255) NULL,
	[Effective Date] [datetime] NULL,
	[Termination Date] [float] (53) NULL,
	[Company Note] [nvarchar] (255) NULL,
	[Comments] [nvarchar] (255) NULL,
	[SUSEP Number] [float] (53) NULL,
	[General Email] [nvarchar] (255) NULL,
	[Other Contact] [nvarchar] (255) NULL,
	[Termination Reason] [nvarchar] (255) NULL,
	[Company Type] [nvarchar] (255) NULL,
	[Company Account Number] [nvarchar] (255) NULL,
	[Bank number] [float] (53) NULL,
	[Bank branch number] [float] (53) NULL,
	[Carrier ID ] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

