
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_FileUpload]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_FileUpload]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_FileUpload](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileRefNo] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[FileType] [nvarchar](10) NOT NULL,
	[UploadType] [nvarchar](10) NOT NULL,
	[UploadPath] [nvarchar](100) NOT NULL,
	[UploadedDate] [datetime] NOT NULL,
	[TotalRecords] [int] NULL,
	[SuccessRecords] [int] NULL,
	[FailedRecords] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[Is_Processed] [varchar](1) NULL,
	[Processed_Date] [datetime] NULL,
	[Is_Active] [varchar](1) NULL,
	[HasError] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_MNT_FileUpload] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


