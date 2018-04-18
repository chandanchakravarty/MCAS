CREATE TABLE [dbo].[STG_TEMP_UploadedFileData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IpNumber] [bigint] NULL,
	[ReportDate] [datetime] NULL,
	[AccidentDateTime] [datetime] NULL,
	[PlaceOfAccident] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatingHours] [int] NULL,
	[DamageToBus] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FactsOfIncident] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiabilityDate] [datetime] NULL,
	[FinalFinding] [int] NULL,
	[ReportNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServiceNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiability] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BOIResults] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StaffNo] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileId] [int] NULL,
	[Status] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExceptionDetails] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsProcessed] [bit] NULL
)


