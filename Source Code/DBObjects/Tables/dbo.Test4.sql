IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Test4]') AND type in (N'U'))
DROP TABLE [dbo].[Test4]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Test4](
	[Customer Code] [nvarchar] (255) NULL,
	[Status] [nvarchar] (255) NULL,
	[Type] [int] NULL,
	[Parent ] [nvarchar] (255) NULL,
	[Customer Name] [nvarchar] (255) NULL,
	[Middle Name] [nvarchar] (255) NULL,
	[Last Name] [nvarchar] (255) NULL,
	[Politically Exposed] [nvarchar] (255) NULL,
	[Broker] [float] (53) NULL,
	[Title] [nvarchar] (255) NULL,
	[Zip Code] [nvarchar] (255) NULL,
	[Address ] [nvarchar] (255) NULL,
	[Number] [nvarchar] (255) NULL,
	[Compliment ] [nvarchar] (255) NULL,
	[District] [nvarchar] (255) NULL,
	[City] [nvarchar] (255) NULL,
	[Country] [float] (53) NULL,
	[State ] [float] (53) NULL,
	[CPF/CNPJ] [nvarchar] (255) NULL,
	[Id Type] [nvarchar] (255) NULL,
	[WebSite] [nvarchar] (255) NULL,
	[Business Type] [float] (53) NULL,
	[Reg Id Issue] [nvarchar] (255) NULL,
	[Original Issue] [nvarchar] (255) NULL,
	[Regional Identification] [nvarchar] (255) NULL,
	[Email Address ] [nvarchar] (255) NULL,
	[Marital Status ] [nvarchar] (255) NULL,
	[Creation Date] [float] (53) NULL,
	[Date of Birth] [nvarchar] (255) NULL,
	[Business Description] [nvarchar] (255) NULL,
	[Gender ] [nvarchar] (255) NULL,
	[Home Phone] [nvarchar] (255) NULL,
	[CADEMP Number] [nvarchar] (255) NULL,
	[Net Assets amount] [nvarchar] (255) NULL,
	[Total amount in assets or monthly income] [nvarchar] (255) NULL,
	[Amount Type] [nvarchar] (255) NULL,
	[Account Type] [float] (53) NULL,
	[Account Number ] [float] (53) NULL,
	[Bank Name ] [nvarchar] (255) NULL,
	[Bank Branch ] [nvarchar] (255) NULL,
	[Bank Number ] [float] (53) NULL,
	[Contact Code ] [nvarchar] (255) NULL,
	[Address 1] [nvarchar] (255) NULL,
	[Compliment 1] [nvarchar] (255) NULL,
	[CPF] [nvarchar] (255) NULL,
	[Mobile] [nvarchar] (255) NULL,
	[Business Phone] [nvarchar] (255) NULL,
	[Ext] [float] (53) NULL,
	[Fax ] [float] (53) NULL,
	[Remarks] [nvarchar] (255) NULL,
	[Regional Identification Type] [nvarchar] (255) NULL,
	[Nationality] [nvarchar] (255) NULL,
	[Email Address 1] [nvarchar] (255) NULL,
	[Title 1] [float] (53) NULL,
	[First Name 1] [nvarchar] (255) NULL,
	[Middle Name 1] [nvarchar] (255) NULL,
	[Last Name 1] [nvarchar] (255) NULL,
	[Zip Code 1] [nvarchar] (255) NULL,
	[Number 1] [nvarchar] (255) NULL,
	[District 1] [nvarchar] (255) NULL,
	[City 1] [nvarchar] (255) NULL,
	[State 1] [float] (53) NULL,
	[Country 1] [float] (53) NULL,
	[Data Conversion.Type] [int] NULL,
	[ErrorCode] [int] NULL,
	[ErrorColumn] [int] NULL,
	[HasErrors] [varchar] (50) NULL
) ON [PRIMARY]
GO

