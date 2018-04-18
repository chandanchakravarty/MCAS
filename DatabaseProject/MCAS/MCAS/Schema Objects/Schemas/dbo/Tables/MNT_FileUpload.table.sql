CREATE TABLE [dbo].[MNT_FileUpload](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileRefNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadPath] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadedDate] [datetime] NOT NULL,
	[TotalRecords] [int] NULL,
	[SuccessRecords] [int] NULL,
	[FailedRecords] [int] NULL,
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Is_Processed] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Processed_Date] [datetime] NULL,
	[Is_Active] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HasError] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrganizationType] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrganizationName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessage] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UploadHistoryId] [int] NULL,
	[LastModifiedDateTime] [datetime] NULL,
	[ModifiedBy] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_FileUpload] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


