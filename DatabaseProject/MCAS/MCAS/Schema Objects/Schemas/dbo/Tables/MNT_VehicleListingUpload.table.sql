CREATE TABLE [dbo].[MNT_VehicleListingUpload](
	[UploadFileId] [int] IDENTITY(1,1) NOT NULL,
	[UploadFileRefNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadFileName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadedDate] [datetime] NOT NULL,
	[TotalRecords] [int] NULL,
	[UplodedSuccess] [int] NULL,
	[UploadedFailed] [int] NULL,
	[Status] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IS_PROCESSED] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PROCESSED_DATE] [datetime] NULL,
	[IS_ACTIVE] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HASERROR] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UploadPath] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_VehicleListingUpload] PRIMARY KEY CLUSTERED 
(
	[UploadFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


