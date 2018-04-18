
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_UploadFileSchedule]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_UploadFileSchedule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_UploadFileSchedule](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[FileRefNo] [nvarchar](50) NOT NULL,
	[ScheduleStartDateTime] [datetime] NOT NULL,
	[Download_File] [nvarchar](100) NULL,
	[Log_File] [nvarchar](100) NULL,
	[Status] [nvarchar](50) NULL,
	[Is_Processed] [varchar](1) NULL,
	[Is_Active] [varchar](1) NULL,
	[HasError] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[JobStartDateTime] [datetime] NULL,
	[JobEndDateTime] [datetime] NULL,
	[ScheduleId]  AS ('JOB-'+right(CONVERT([varchar](5),[JobId],0),(5))) PERSISTED NOT NULL,
 CONSTRAINT [PK_MNT_UploadFileSchedule] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


SET ANSI_PADDING OFF
GO


