CREATE TABLE [dbo].[STG_TAC_ACC_REP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IpNumber] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReportDate] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentDateTime] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PlaceOfAccident] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatingHours] [int] NULL,
	[DamageToBus] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FactsOfIncident] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileId] [int] NULL,
	[IsProcessed] [bit] NULL CONSTRAINT [DF_STG_TAC_ACC_REP_IsProcessed]  DEFAULT ((0)),
	[FileRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReportNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DutyIO] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[HasError] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessage] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HistoryId] [int] NULL
)


