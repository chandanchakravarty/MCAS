CREATE TABLE [dbo].[MNT_UploadFileSchedule](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[FileRefNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ScheduleStartDateTime] [datetime] NOT NULL,
	[Download_File] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Log_File] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Is_Processed] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Is_Active] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasError] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NOT NULL,
	[JobStartDateTime] [datetime] NULL,
	[JobEndDateTime] [datetime] NULL,
	[ScheduleId]  AS ('JOB-'+right(CONVERT([varchar](5),[JobId],(0)),(5))) PERSISTED NOT NULL,
	[Attempts] [int] NULL,
	[ErrorDesc] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_UploadFileSchedule] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


