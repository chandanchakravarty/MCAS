CREATE TABLE [dbo].[STG_FileUpload_ERRORS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NULL,
	[FileRefNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorNumber] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorLine] [int] NULL,
	[ErrorMessage] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[createDateTime] [datetime] NULL,
	[createBy] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


