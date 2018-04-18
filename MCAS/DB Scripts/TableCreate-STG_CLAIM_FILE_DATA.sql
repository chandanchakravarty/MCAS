GO

/****** Object:  Table [dbo].[STG_CLAIM_FILE_DATA]    Script Date: 03/04/2015 15:27:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STG_CLAIM_FILE_DATA]') AND type in (N'U'))
DROP TABLE [dbo].[STG_CLAIM_FILE_DATA]
GO


/****** Object:  Table [dbo].[STG_CLAIM_FILE_DATA]    Script Date: 03/04/2015 15:27:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[STG_CLAIM_FILE_DATA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReportNo] [varchar](50) NULL,
	[IpNumber] [varchar](20) NULL,
	[ReportDate] [varchar](20) NULL,
	[AccidentDateTime] [varchar](20) NULL,
	[PlaceOfAccident] [nvarchar](1000) NULL,
	[OperatingHours] [varchar](20) NULL,
	[DamageToBus] [varchar](max) NULL,
	[FactsOfIncident] [nvarchar](max) NULL,
	[FinalLiabilityDate] [varchar](20) NULL,
	[FinalFinding] [varchar](20) NULL,
	[BusNumber] [varchar](50) NULL,
	[ServiceNo] [varchar](20) NULL,
	[FinalLiability] [varchar](50) NULL,
	[StaffNo] [varchar](30) NULL,
	[FileRefId] [int] NOT NULL,
	[Status] [varchar](10) NULL,
	[ErrorMessage] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
	[IsProcessed] [bit] NULL,
	[DutyIO] [varchar](10) NULL,
	[FileRefNo] [varchar](20) NULL,
	[HistoryIds] [varchar](20) NULL,
	[HasError] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


