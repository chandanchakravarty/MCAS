CREATE TABLE [dbo].[STG_UploadedFileData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IpNumber] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReportDate] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentDateTime] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PlaceOfAccident] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatingHours] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DamageToBus] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FactsOfIncident] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiabilityDate] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalFinding] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReportNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServiceNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiability] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BOIResults] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StaffNo] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileRefId] [int] NOT NULL,
	[Status] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExceptionDetails] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF__STG_Uploa__Creat__230A1C49]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsProcessed] [bit] NULL,
	[DutyIO] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HistoryIds] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InitialLiability] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CollisionType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK__STG_Uplo__3214EC272121D3D7] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK__STG_Uploa__FileR__23FE4082] FOREIGN KEY([FileRefId])
REFERENCES [dbo].[MNT_FileUpload] ([FileId])
)


