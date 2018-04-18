CREATE TABLE [dbo].[MNT_APP_RELEASE_MASTER](
	[ReleaseID] [int] IDENTITY(1,1) NOT NULL,
	[AppVersion] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AssemblyVersion] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReleaseNumber] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReleaseName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleasedBy] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Remarks] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_APP_RELEASE_MASTER] PRIMARY KEY CLUSTERED 
(
	[ReleaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


