
/****** Object:  Table [dbo].[MNT_FileUploadHistory]    Script Date: 02/06/2015 16:32:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_FileUploadHistory]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_FileUploadHistory]
GO



/****** Object:  Table [dbo].[MNT_FileUploadHistory]    Script Date: 02/06/2015 16:32:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[MNT_FileUploadHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NULL,
	[FileRefNo] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[Processed_Date] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[Is_Processed] [varchar](1) NULL,
 CONSTRAINT [PK_MNT_FileUploadHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


