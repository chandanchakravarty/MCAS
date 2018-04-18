CREATE TABLE [dbo].[MNT_FileUploadHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NULL,
	[FileRefNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Processed_Date] [datetime] NULL,
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Is_Processed] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ScheduleStartDateTime] [datetime] NULL,
 CONSTRAINT [PK_MNT_FileUploadHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


