CREATE TABLE [dbo].[STG_CLAIM_FILE_DATA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReportNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IpNumber] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReportDate] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentDateTime] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PlaceOfAccident] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatingHours] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DamageToBus] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FactsOfIncident] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiabilityDate] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalFinding] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServiceNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiability] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StaffNo] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileRefId] [int] NOT NULL,
	[Status] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessage] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsProcessed] [bit] NULL,
	[DutyIO] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HistoryIds] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasError] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InitialLiability] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CollisionType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__STG_CLAI__3214EC273647D946] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


