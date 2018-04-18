IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QQ_INVOICE_PARTICULAR_MARINE]') AND type in (N'U'))
DROP TABLE [dbo].[QQ_INVOICE_PARTICULAR_MARINE]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
 /*----------------------------------------------------------      
TABLE  Name       : [QQ_INVOICE_PARTICULAR_MARINE]      
Created by      : Kuldeep Saxena     
Date            : 17/03/2012      
Purpose   : Demo      
Revison History :      
Used In        : Singapore  to STORE INVOICE DETAILS FOR QQ CUSTOMER
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------
*/

CREATE TABLE [dbo].[QQ_INVOICE_PARTICULAR_MARINE](
	[ID] [int] NOT NULL,
	[CUSTOMER_ID] [int] NOT NULL,
	[QUOTE_ID] [int] NOT NULL,
	[CUSTOMER_TYPE] [int] NULL,
	[COMPANY_NAME] [nvarchar](100) NULL,
	[BUSINESS_TYPE] [int] NULL,
	[OPEN_COVER_NO] [NVARCHAR](10) NULL,
	[INVOICE_TYPE] [NVARCHAR](20) NULL,
	[INVOICE_AMOUNT] [DECIMAL](18,2) NULL,
	[CURRENCY_TYPE] [INT]NULL,
	[BILLING_CURRENCY] [INT] NULL,
	[MARK_UP_RATE_PERC] [DECIMAL](5,2) NULL,
	[IS_ACTIVE] [nchar](1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[DATE_OF_QUOTATION] [varchar](20) NULL,
	[SUM_INSURED] DECIMAL(18,2) NULL,
	[CALCULATED_PREMIUM] DECIMAL(18,2) NULL,
	[POLICY_ID] INT NULL,
	[POLICY_VERSION_ID] INT  NULL
 CONSTRAINT [PK_QQ_INVOICE_PARTICULAR_MARINE_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
	
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

