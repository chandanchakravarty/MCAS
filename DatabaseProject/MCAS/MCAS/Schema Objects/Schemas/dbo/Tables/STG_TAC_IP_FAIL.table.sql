CREATE TABLE [dbo].[STG_TAC_IP_FAIL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IpNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentDateTime] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiabilityDate] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalFinding] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PlaceOfAccident] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatingHours] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DamageToBus] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FactsOfIncident] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReportNo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileId] [int] NULL
)


