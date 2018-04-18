IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[abcs]') AND type in (N'U'))
DROP TABLE [dbo].[abcs]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[abcs](
	[GL_ID] [int] NOT NULL,
	[ACCOUNT_ID] [int] NOT NULL,
	[BANK_NAME] [nvarchar] (100) NULL,
	[BANK_ADDRESS1] [nvarchar] (150) NULL,
	[BANK_ADDRESS2] [nvarchar] (150) NULL,
	[BANK_CITY] [nvarchar] (40) NULL,
	[BANK_COUNTRY] [nvarchar] (10) NULL,
	[BANK_STATE] [nvarchar] (10) NULL,
	[BANK_ZIP] [nvarchar] (10) NULL,
	[BANK_ACC_TITLE] [nvarchar] (50) NULL,
	[BANK_NUMBER] [nvarchar] (25) NULL,
	[STARTING_DEPOSIT_NUMBER] [int] NULL,
	[IS_CHECK_ISSUED] [nchar] (1) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[CHECK_COUNTER] [int] NULL,
	[END_CHECK_NUMBER] [int] NULL,
	[START_CHECK_NUMBER] [int] NULL,
	[ROUTE_POSITION_CODE1] [nvarchar] (5) NULL,
	[ROUTE_POSITION_CODE2] [nvarchar] (5) NULL,
	[ROUTE_POSITION_CODE3] [nvarchar] (5) NULL,
	[ROUTE_POSITION_CODE4] [nvarchar] (10) NULL,
	[BANK_MICR_CODE] [nvarchar] (19) NULL,
	[SIGN_FILE_1] [nvarchar] (200) NULL,
	[SIGN_FILE_2] [nvarchar] (200) NULL,
	[TRANSIT_ROUTING_NUMBER] [nvarchar] (9) NULL,
	[COMPANY_ID] [nvarchar] (10) NULL,
	[Bank_Id] [smallint] NOT NULL IDENTITY (1,1)
) ON [PRIMARY]
GO

