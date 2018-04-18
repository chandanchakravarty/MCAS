CREATE TABLE [dbo].[MNT_Broker](
	[BrokerId] [int] IDENTITY(1,1) NOT NULL,
	[BrokerCode] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BrokerName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MobileNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AreaCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelephoneNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactPerson] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[dtAppointment] [datetime] NULL,
	[dtRegistration] [datetime] NULL,
	[RegistrationNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GSTNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatementAC] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatementType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryShortCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Remarks] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Broker_BrokerId] PRIMARY KEY CLUSTERED 
(
	[BrokerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Broker_BrokerCode] UNIQUE NONCLUSTERED 
(
	[BrokerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


