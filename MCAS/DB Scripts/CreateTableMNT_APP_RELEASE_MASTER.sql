/****** Object:  Table [dbo].[MNT_APP_RELEASE_MASTER]    Script Date: 03/12/2015 16:46:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_APP_RELEASE_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_APP_RELEASE_MASTER]
GO

/****** Object:  Table [dbo].[MNT_APP_RELEASE_MASTER]    Script Date: 03/12/2015 16:46:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_APP_RELEASE_MASTER](
	[ReleaseID] [int] IDENTITY(1,1) NOT NULL,
	[AppVersion] [nvarchar](20) NULL,
	[AssemblyVersion] [nvarchar](20) NULL,
	[ReleaseNumber] [nvarchar](20) NULL,
	[ReleaseName] [nvarchar](50) NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleasedBy] [nvarchar](20) NULL,
	[Remarks] [nvarchar](250) NULL,
 CONSTRAINT [PK_MNT_APP_RELEASE_MASTER] PRIMARY KEY CLUSTERED 
(
	[ReleaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


