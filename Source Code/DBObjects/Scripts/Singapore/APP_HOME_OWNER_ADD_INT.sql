/****** Object:  Table [dbo].[APP_HOME_OWNER_ADD_INT]    Script Date: 02/09/2012 13:35:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[APP_HOME_OWNER_ADD_INT](
	[CUSTOMER_ID] [int] NOT NULL,
	[APP_ID] [smallint] NOT NULL,
	[APP_VERSION_ID] [smallint] NOT NULL,
	[DWELLING_ID] [smallint] NOT NULL,
	[ADD_INT_ID] [int] NOT NULL,
	[MEMO] [nvarchar](250) NULL,
	[NATURE_OF_INTEREST] [int] NULL,
	[RANK] [smallint] NULL,
	[LOAN_REF_NUMBER] [nvarchar](75) NULL,
	[IS_ACTIVE] [nchar](1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[HOLDER_ID] [int] NULL,
	[HOLDER_NAME] [nvarchar](70) NULL,
	[HOLDER_ADD1] [nvarchar](140) NULL,
	[HOLDER_ADD2] [nvarchar](140) NULL,
	[HOLDER_CITY] [nvarchar](80) NULL,
	[HOLDER_COUNTRY] [int] NULL,
	[HOLDER_STATE] [smallint] NULL,
	[HOLDER_ZIP] [varchar](11) NULL,
	[BILL_MORTAGAGEE] [smallint] NULL,
 CONSTRAINT [PK_APP_HOME_OWNER_ADD_INT_CUSTOMER_ID_APP_ID_VERSION_ID_DWELLING_ID_ADD_INT_ID] PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_ID] ASC,
	[APP_ID] ASC,
	[APP_VERSION_ID] ASC,
	[DWELLING_ID] ASC,
	[ADD_INT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


