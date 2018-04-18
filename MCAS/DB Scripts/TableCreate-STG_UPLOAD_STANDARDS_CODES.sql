GO

/****** Object:  Table [dbo].[STG_UPLOAD_STANDARDS_CODES]    Script Date: 03/04/2015 15:28:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STG_UPLOAD_STANDARDS_CODES]') AND type in (N'U'))
DROP TABLE [dbo].[STG_UPLOAD_STANDARDS_CODES]
GO


/****** Object:  Table [dbo].[STG_UPLOAD_STANDARDS_CODES]    Script Date: 03/04/2015 15:28:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[STG_UPLOAD_STANDARDS_CODES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[STD_CODE_TYPE] [varchar](10) NULL,
	[STD_CODE] [varchar](10) NULL,
	[Description] [nvarchar](150) NULL,
	[Active_flag] [nvarchar](10) NULL,
	[CreatedBy_File] [varchar](20) NULL,
	[CreatedDate_File] [varchar](20) NULL,
	[FileId] [int] NULL,
	[IsProcessed] [bit] NULL,
	[FileRefNo] [varchar](20) NULL,
	[IsActive] [char](1) NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[HasError] [char](1) NULL,
	[ErrorMessage] [nvarchar](100) NULL,
	[HistoryId] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


