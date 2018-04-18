USE [EBX-ADV-DEV-SINGAPORE]
GO

/****** Object:  Table [dbo].[QQ_MARINECARGO_RISK_DETAILS]    Script Date: 03/21/2012 15:09:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QQ_MARINECARGO_RISK_DETAILS](
	[QQ_MARINECARGO_RISK_ID] [int] NOT NULL,
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [int] NOT NULL,
	[QUOTE_ID] [int] NOT NULL,
	[VOYAGE_TO] [int] NULL,
	[VOYAGE_FROM] [int] NULL,
	[THENCE_TO] [int] NULL,
	[VESSEL] [int] NULL,
	[AIRCRAFT_NUMBER] [nvarchar](20) NULL,
	[LAND_TRANSPORT] [nvarchar](20) NULL,
	[VOYAGE_FROM_DATE] [datetime] NULL,
	[QUANTITY_DESCRIPTION] [nvarchar](1000) NULL,
	[INSURANCE_CONDITIONS1] [decimal](18, 2) NULL,
	[INSURANCE_CONDITIONS2] [decimal](18, 2) NULL,
	[INSURANCE_CONDITIONS3] [decimal](18, 2) NULL,
	[INSURANCE_CONDITIONS1_SELECTION] [nvarchar](20) NULL,
	[MARINE_RATE] [decimal](18, 2) NULL,
	[IS_ACTIVE] [nchar](1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
 CONSTRAINT [PK_QQ_MARINECARGO_RISK_DETAILS] PRIMARY KEY CLUSTERED 
(
	[QQ_MARINECARGO_RISK_ID] ASC,
	[CUSTOMER_ID] ASC,
	[POLICY_ID] ASC,
	[POLICY_VERSION_ID] ASC,
	[QUOTE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--drop table QQ_MARINECARGO_RISK_DETAILS


