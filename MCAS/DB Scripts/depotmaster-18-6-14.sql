CREATE TABLE [dbo].[MNT_DepotMaster](
	[DepotId] [int] IDENTITY(1,1) NOT NULL,
	[DepotCode] [varchar](25) NOT NULL,
	[DepotReference] [varchar](50) NOT NULL,
	[Address1] [nvarchar](200)NOT NULL,
	[Address2] [nvarchar](200) NULL,
	[Address3] [nvarchar](200) NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](50) NOT NULL,
	[PostalCode] [varchar](50)NOT NULL,
	[TelephoneOff] [varchar](50)  NULL,
	[MobileNo] [varchar](50)  NULL,
	[Email] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[PersonInCharge] [varchar](50) NULL,
	[Status] [nvarchar](1) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	
 CONSTRAINT [PK_MNT_DepotMaster] PRIMARY KEY CLUSTERED 
(
	[DepotId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO