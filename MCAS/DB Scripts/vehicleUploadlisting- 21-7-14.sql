
/****** Object:  Table [dbo].[MNT_VehicleListingUpload]    Script Date: 07/21/2014 13:30:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_VehicleListingUpload]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_VehicleListingUpload]
GO



/****** Object:  Table [dbo].[MNT_VehicleListingUpload]    Script Date: 07/21/2014 13:30:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_VehicleListingUpload](
	[UploadFileId] [int] IDENTITY(1,1) NOT NULL,
	[UploadFileRefNo] [nvarchar](50) NOT NULL,
	[UploadFileName] [nvarchar](100) NOT NULL,
	[UploadedDate] [datetime] NOT NULL,
	[TotalRecords] [int] NULL,
	[UplodedSuccess] [int] NULL,
	[UploadedFailed] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[IS_PROCESSED] [varchar](1) NULL,
	[PROCESSED_DATE] [datetime] NULL,
	[IS_ACTIVE] [varchar](1) NULL,
	[HASERROR] [varchar](50) NULL,
 CONSTRAINT [PK_MNT_VehicleListingUpload] PRIMARY KEY CLUSTERED 
(
	[UploadFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


