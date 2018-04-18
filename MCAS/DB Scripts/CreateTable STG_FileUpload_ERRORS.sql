

/****** Object:  Table [dbo].[STG_FileUpload_ERRORS]    Script Date: 02/26/2015 15:30:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STG_FileUpload_ERRORS]') AND type in (N'U'))
DROP TABLE [dbo].[STG_FileUpload_ERRORS]
GO



/****** Object:  Table [dbo].[STG_FileUpload_ERRORS]    Script Date: 02/26/2015 15:30:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[STG_FileUpload_ERRORS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NULL,
	[FileRefNo] [nvarchar](20) NULL,
	[ErrorNumber] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [nvarchar](1000) NULL,
	[ErrorLine] [int] NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[createDateTime] [datetime] NULL,
	[createBy] [nvarchar](20) NULL
) ON [PRIMARY]

GO


