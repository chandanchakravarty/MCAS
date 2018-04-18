CREATE TABLE [dbo].[STG_TAC_IP_BUS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IpNumber] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ServiceNo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalLiability] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BOIResults] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StaffNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileId] [int] NULL,
	[IsProcessed] [bit] NULL CONSTRAINT [DF_STG_TAC_IP_BUS_IsProcessed]  DEFAULT ((0)),
	[ReportNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[FileRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasError] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessage] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HistoryId] [int] NULL
)


