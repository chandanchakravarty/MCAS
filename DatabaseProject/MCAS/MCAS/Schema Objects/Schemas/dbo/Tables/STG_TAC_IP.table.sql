CREATE TABLE [dbo].[STG_TAC_IP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IpNumber] [nvarchar](200) NULL,
	[AccidentDateTime] [varchar](20) NULL,
	[FinalLiabilityDate] [varchar](20) NULL,
	[FinalFinding] [varchar](20) NULL,
	[PlaceOfAccident] [nvarchar](max) NULL,
	[OperatingHours] [varchar](10) NULL,
	[DamageToBus] [nvarchar](max) NULL,
	[FactsOfIncident] [nvarchar](max) NULL,
	[ReportNo] [nvarchar](max) NULL,
	[FileId] [int] NULL,
	[IsProcessed] [bit] NULL,
	[FileRefNo] [varchar](20) NULL,
	[DutyIO] [varchar](20) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [varchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[HasError] [char](1) NULL,
	[ErrorMessage] [nvarchar](400) NULL,
	[HistoryId] [int] NULL,
	[InitialLiability] [nvarchar](20) NULL,
	[CollisionType] [nvarchar](10) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[STG_TAC_IP] ADD  CONSTRAINT [DF_STG_TAC_IP_IsProcessed]  DEFAULT ((0)) FOR [IsProcessed]
GO
