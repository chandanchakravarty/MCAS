CREATE TABLE [dbo].[STG_UPLOAD_STANDARDS_CODES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[STD_CODE_TYPE] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[STD_CODE] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Active_flag] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy_File] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate_File] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileId] [int] NULL,
	[IsProcessed] [bit] NULL,
	[FileRefNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[HasError] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessage] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HistoryId] [int] NULL
)


